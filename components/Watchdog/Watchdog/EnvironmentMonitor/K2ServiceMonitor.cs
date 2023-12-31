﻿//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//-----------------------------------------------------------------------------------------
using HPE.HSP.UA3.Core.API.Logger.Interfaces;
using System;
using System.Diagnostics;
using Watchdog.Domain;

namespace Watchdog.EnvironmentMonitor
{
    public class K2ServiceMonitor : Monitor.HealthMonitorBase
    {
        private K2ServiceConfigDataItem serviceConfigData = null;
        private string iisServerName = string.Empty;
        private string endpointAddress = string.Empty;
        private string tenantId = string.Empty;

        public K2ServiceMonitor(K2ServiceConfigDataItem serviceData, string iisServerName, string siteName, string endpointAddress, string tenantId, ILogger logger)
            : base(serviceData, logger)
        {
            this.serviceConfigData = serviceData;
            this.iisServerName = iisServerName;
            this.endpointAddress = endpointAddress;
            this.tenantId = tenantId;
            this.serviceConfigData.SiteName = siteName;
            this.applicationHealthInformation.ServiceType = "K2 Service - " + serviceConfigData.ApplicationPoolName;
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
                WCFUtility.GetEndpointAddress(this.endpointAddress);
                var endpoints = WCFUtility.GetEndpointsForContracts();
                if (endpoints != null)
                {
                    isServiceAvailable = true;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("K2 Service - Unavailable to download contracts: " + serviceConfigData.Name, ex);
            }

            return isServiceAvailable;
        }
        
        protected override void RestartService()
        {
            IISHelper.RestartServiceForApplicationPool(iisServerName,serviceConfigData.ApplicationPoolName,logger,ref applicationHealthInformation);            
            IISHelper.RestartServiceForSite(iisServerName,logger,ref applicationHealthInformation,serviceConfigData.Name,serviceConfigData.SiteName,serviceConfigData.ApplicationPoolName);            
        }

        protected override Process GetProcessIdForService()
        {
            return IISHelper.GetProcessIdForService(serviceConfigData.ApplicationPoolName);
        }

        protected override void BuildServiceHealthInformation(Tuple<double, double, float, double> cpuMemData)
        {
            this.applicationHealthInformation.CPUUsagePercent = cpuMemData.Item1;
            this.applicationHealthInformation.MemoryUsagePercent = cpuMemData.Item2;
            this.applicationHealthInformation.processMemInKB = cpuMemData.Item3;
            this.applicationHealthInformation.processMemInGB = cpuMemData.Item4;
            this.applicationHealthInformation.Status = Constants.Status.Running;
        }
    }
}