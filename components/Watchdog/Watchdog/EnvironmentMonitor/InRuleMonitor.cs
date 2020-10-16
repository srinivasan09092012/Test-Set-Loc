//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
//-----------------------------------------------------------------------------------------

using HPE.HSP.UA3.Core.API.Logger.Interfaces;
using System;
using System.Diagnostics;
using System.Management;
using Watchdog.Domain;
using Watchdog.Monitor;


namespace Watchdog.EnvironmentMonitor
{
    public class InRuleMonitor : HealthMonitorBase
    {
        private InRuleConfigDataItem serviceConfigData = null;
        private string iisServerName = string.Empty;
        private string endpointAddress = string.Empty;
        private string tenantId = string.Empty;

        public InRuleMonitor(InRuleConfigDataItem serviceData, string iisServerName, string siteName, string endpointAddress, string tenantId, ILogger logger)
            : base(serviceData, logger)
        {
            this.serviceConfigData = serviceData;
            this.iisServerName = iisServerName;
            this.endpointAddress = endpointAddress;
            this.tenantId = tenantId;
            this.serviceConfigData.SiteName = siteName;
            this.applicationHealthInformation.ServiceType = "InRule - " + serviceData.Type;
            this.applicationHealthInformation.ApplicationPool = serviceConfigData.ApplicationPoolName;
            this.applicationHealthInformation.Endpoint = this.endpointAddress;
            this.applicationHealthInformation.ServiceName = serviceConfigData.Name;
            this.applicationHealthInformation.ApplicationTenantId = this.tenantId;
        }

        public override bool CheckServiceAvailabilityAsync()
        {
            bool isServiceAvailable = false;
            try
            {
                WCFUtility.GetEndpointAddress(endpointAddress);
                var endpoints = WCFUtility.GetEndpointsForContracts();
                if (endpoints != null)
                {
                    isServiceAvailable = true;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("InRule Service - Unavailable to download contracts: " + serviceConfigData.Name, ex);
            }

            return isServiceAvailable;
        }

        protected override void BuildServiceHealthInformation(Tuple<double, double, float, double> cpuMemData)
        {
            this.applicationHealthInformation.CPUUsagePercent = cpuMemData.Item1;
            this.applicationHealthInformation.MemoryUsagePercent = cpuMemData.Item2;
            this.applicationHealthInformation.processMemInKB = cpuMemData.Item3;
            this.applicationHealthInformation.processMemInGB = cpuMemData.Item4;
            this.applicationHealthInformation.Status = Constants.Status.Running;
        }

        protected override Process GetProcessIdForService()
        {
            string query = string.Format("SELECT ProcessId FROM Win32_Service WHERE Name='{0}'", serviceConfigData.Name);
            Process process = null;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            uint processId = uint.MinValue;
            foreach (ManagementObject obj in searcher.Get())
            {
                processId = (uint)obj["ProcessId"];

                if (processId > 0)
                {
                    process = Process.GetProcessById(Convert.ToInt32(processId));
                    break;
                }
            }

            return process;
        }

        protected override void RestartService()
        {
            IISHelper.RestartServiceForApplicationPool(iisServerName, serviceConfigData.ApplicationPoolName, logger, ref applicationHealthInformation);
            IISHelper.RestartServiceForSite(iisServerName, logger, ref applicationHealthInformation, serviceConfigData.Name, serviceConfigData.SiteName, serviceConfigData.ApplicationPoolName);
        }
    }
}