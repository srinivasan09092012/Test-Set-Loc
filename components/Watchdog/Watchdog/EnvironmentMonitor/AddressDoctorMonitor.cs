//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//-----------------------------------------------------------------------------------------
using HPE.HSP.UA3.Core.API.Logger.Interfaces;
using System;
using System.Configuration;
using System.Diagnostics;
using Watchdog.Domain;
using Watchdog.Monitor;
using HP.HSP.UA3.Core.API.AddressValidation.Data;
using HP.HSP.UA3.Core.API.AddressValidation.Interfaces;
using Microsoft.Practices.Unity;

namespace Watchdog.EnvironmentMonitor
{
    public class AddressDoctorMonitor : HealthMonitorBase
    {
        private string endpointAddress = string.Empty;
        private string tenantId = string.Empty;
        private AddressDoctorConfigDataItem serviceConfigData;
        private string registerType = string.Empty;

        public IAddressDataProvider AddressDataProvider { get; set; }

        public AddressDoctorMonitor(AddressDoctorConfigDataItem serviceData, string tenantId, ILogger logger)
                   : base(serviceData, logger)
        {
            this.serviceConfigData = serviceData;
            this.applicationHealthInformation.ServiceType = "AddressDoctor";
            this.applicationHealthInformation.Endpoint = this.endpointAddress;
            this.applicationHealthInformation.ServiceName = serviceConfigData.Name;
            this.applicationHealthInformation.ApplicationTenantId = this.tenantId;

            if (serviceData.Name.ToUpper() == "ADDRESS DOCTOR INTERACTIVE")
            {
                registerType = BusinessConstants.AddressInteractiveServiceDataProvider;
            }
            else
            {
                registerType = BusinessConstants.AddressServicseDataProvider;
            }

            this.AddressDataProvider = this.ResolveContainer();
        }

        public override bool CheckServiceAvailabilityAsync()
        {
            bool isServiceHealthy = true;
            if (string.Compare(serviceConfigData.Type, "API", StringComparison.OrdinalIgnoreCase) == 0)
            {
                isServiceHealthy = IsServiceAvailable();
            }

            return isServiceHealthy;
        }

        public virtual IAddressDataProvider ResolveContainer()
        {
            return WatchdogContainer.IocContainer.Resolve<IAddressDataProvider>(this.registerType);
        }

        protected override void RestartService()
        {
        }

        protected override void BuildServiceHealthInformation(Tuple<double, double, float, double> cpuMemData)
        {
            this.applicationHealthInformation.Status = "Running";
        }

        protected override Process GetProcessIdForService()
        {
            return null;
        }

        private bool IsServiceAvailable()
        {
            bool isServiceAvailable = true;

            try
            {
                HP.HSP.UA3.Core.API.AddressValidation.Data.AddressModel locationAddress = new HP.HSP.UA3.Core.API.AddressValidation.Data.AddressModel()
                {
                    Line1 = ConfigurationManager.AppSettings["AddressLine1"] ?? string.Empty,
                    Line2 = string.Empty,
                    City = ConfigurationManager.AppSettings["City"] ?? string.Empty,
                    State = ConfigurationManager.AppSettings["State"] ?? string.Empty,
                    Country = ConfigurationManager.AppSettings["Country"] ?? string.Empty,
                    PostalCode = ConfigurationManager.AppSettings["PostalCode"] ?? string.Empty
                };

                this.ValidateAddress(locationAddress);
            }
            catch (Exception ex)
            {
                isServiceAvailable = false;
                this.applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                logger.LogError("Error occured during Address validation : " + serviceConfigData.Name, ex);
            }
            return isServiceAvailable;
        }

        private void ValidateAddress(AddressModel address, double resultPercentageThreshold = BusinessConstants.DefaultResultPercentageThreshold)
        {
            ResultModel response = this.AddressDataProvider.ValidateAddress(address);

            if (response.StatusCode == Enumerations.StatusCodeType.OK && response.ResultData != null)
            {
                if (response.ResultData.ResultPercentage < resultPercentageThreshold)
                {
                    throw new Exception("The physical address is not recognized as a valid address.");
                }
            }
            else
            {
                throw new Exception("Address Doctor URL is not responding or is not valid.");
            }
        }
    }
}