﻿//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
//-----------------------------------------------------------------------------------------

using HPE.HSP.UA3.Core.API.Logger;
using HPE.HSP.UA3.Core.API.Logger.Interfaces;
using Microsoft.Web.Administration;
using System;
using System.Linq;
using System.Management;
using System.Threading;
using Watchdog.Domain;
using Watchdog.Monitor;
using System.Diagnostics;

namespace Watchdog.EnvironmentMonitor
{
    class K2Monitor : HealthMonitorBase
    {
        private K2ConfigDataItem serviceConfigData = null;
        private string iisServerName = string.Empty;
        private string endpointAddress = string.Empty;
        private string tenantId = string.Empty;

        public K2Monitor(K2ConfigDataItem serviceData, string iisServerName, string siteName, string endpointAddress, string tenantId, ILogger logger)
            : base(serviceData, logger)
        {
            this.serviceConfigData = serviceData;
            this.iisServerName = iisServerName;
            this.endpointAddress = endpointAddress;
            this.tenantId = tenantId;
            this.serviceConfigData.SiteName = siteName;
            this.applicationHealthInformation.ServiceType = "K2 - " + serviceData.Type;
            this.applicationHealthInformation.ApplicationPool = serviceConfigData.ApplicationPoolName;
            this.applicationHealthInformation.Endpoint = this.endpointAddress;
            this.applicationHealthInformation.ServiceName = serviceConfigData.Name;
            this.applicationHealthInformation.ApplicationTenantId = this.tenantId;
        }

        public override bool CheckServiceAvailabilityAsync()
        {
            bool isServiceHealthy = false;
            bool isApplicationPoolActiveOrNot = false;
            bool isServiceActiveOrNot = false;
            isServiceActiveOrNot = this.RestartServiceForSite(isServiceActiveOrNot);
            isApplicationPoolActiveOrNot = this.RestartServiceForApplicationPool(isApplicationPoolActiveOrNot);
            if (isServiceActiveOrNot && isApplicationPoolActiveOrNot)
            {
                LoggerManager.Logger.LogInformational("---Response was Successful for K2 Monitoring---" + "Status:" + "Ok");
                isServiceHealthy = true;
            }
            else
            {
                LoggerManager.Logger.LogFatal("--------Response was Unsuccessful for K2 Monitoring--------");
            }
                       
            return isServiceHealthy;            
        }

        private bool RestartServiceForSite(bool isServiceActiveOrNot)
        {
            if (!string.IsNullOrEmpty(this.iisServerName) && !string.IsNullOrEmpty(serviceConfigData.Name))
            {
                try
                {
                    using (ServerManager manager = ServerManager.OpenRemote(this.iisServerName))
                    {
                        Site siteName = manager.Sites.FirstOrDefault(s => s.Name.Equals(serviceConfigData.SiteName));

                        if (siteName != null)
                        {
                            bool siteNameStopped = siteName.State == ObjectState.Stopped || siteName.State == ObjectState.Stopping;
                            this.StartSiteNameService(siteName, siteNameStopped);
                            isServiceActiveOrNot = true;
                        }
                        else
                        {
                            this.applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                            throw new Exception("Site Name Pool doesn't exist : " + serviceConfigData.Name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                    logger.LogError("Error occured while attempting to restart the application pool : " + serviceConfigData.ApplicationPoolName, ex);
                    throw ex;
                }
            }

            return isServiceActiveOrNot;
        }

        private bool RestartServiceForApplicationPool(bool isApplicationPoolActiveOrNot)
        {
            if (!string.IsNullOrEmpty(this.iisServerName) && !string.IsNullOrEmpty(serviceConfigData.ApplicationPoolName))
            {
                try
                {
                    using (ServerManager manager = ServerManager.OpenRemote(this.iisServerName))
                    {
                        ApplicationPool appPool = manager.ApplicationPools.FirstOrDefault(ap => ap.Name == serviceConfigData.ApplicationPoolName);
                        
                        if (appPool != null)
                        {
                            this.applicationHealthInformation.ApplicationPool = appPool.Name;
                            bool appPoolStopped = appPool.State == ObjectState.Stopped || appPool.State == ObjectState.Stopping;
                            this.StartApplicationPool(appPool, appPoolStopped);
                            isApplicationPoolActiveOrNot = true;
                        }
                        else
                        {
                            this.applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                            throw new Exception("Application Pool doesn't exist : " + serviceConfigData.ApplicationPoolName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                    logger.LogError("Error occured while attempting to restart the application pool : " + serviceConfigData.ApplicationPoolName, ex);
                    throw ex;
                }
            }

            return isApplicationPoolActiveOrNot;
        }

        private void StartSiteNameService(Site siteName, bool siteNameStopped)
        {
            if (siteNameStopped)
            {
                try
                {
                    siteName.Start();
                    Thread.Sleep(10000);
                }
                catch (Exception ex)
                {
                    this.applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                    logger.LogError("Error occured while recycling site service", ex);
                }

                if (siteName.State == ObjectState.Started)
                {
                    this.applicationHealthInformation.RestartStatus = Constants.Status.Restarted;

                    logger.LogInformational("Site Name: " + serviceConfigData.Name + " has been recycled successfully");
                }
            }
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

        protected override void BuildServiceHealthInformation(Tuple<double, double, float, double> cpuMemData)
        {
            this.applicationHealthInformation.CPUUsagePercent = cpuMemData.Item1;
            this.applicationHealthInformation.MemoryUsagePercent = cpuMemData.Item2;
            this.applicationHealthInformation.processMemInKB = cpuMemData.Item3;
            this.applicationHealthInformation.processMemInGB = cpuMemData.Item4;
            this.applicationHealthInformation.Status = Constants.Status.Running;
        }

        protected override void RestartService()
        {
            if (!string.IsNullOrEmpty(this.iisServerName) && !string.IsNullOrEmpty(serviceConfigData.ApplicationPoolName))
            {
                try
                {
                    using (ServerManager manager = ServerManager.OpenRemote(this.iisServerName))
                    {
                        ApplicationPool appPool = manager.ApplicationPools.FirstOrDefault(ap => ap.Name == serviceConfigData.ApplicationPoolName);

                        if (appPool != null)
                        {
                            //Get the current state of the app pool                            
                            bool appPoolStopped = appPool.State == ObjectState.Stopped || appPool.State == ObjectState.Stopping;                            
                            this.StartApplicationPool(appPool, appPoolStopped);
                        }
                        else
                        {
                            this.applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                            throw new Exception("Application Pool doesn't exist : " + serviceConfigData.ApplicationPoolName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                    logger.LogError("Error occured while attempting to restart the application pool : " + serviceConfigData.ApplicationPoolName, ex);
                    throw ex;
                }
            }
        }        

        private void StartApplicationPool(ApplicationPool appPool, bool isAppPoolStopped)
        {
            if (isAppPoolStopped)
            {
                try
                {
                    appPool.Start();
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    this.applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                    logger.LogError("Error occured while recycling application pool", ex);
                }

                if (appPool.State == ObjectState.Started)
                {
                    this.applicationHealthInformation.RestartStatus = Constants.Status.Restarted;

                    logger.LogInformational("Application pool: " + serviceConfigData.ApplicationPoolName + " has been recycled successfully");
                }
            }
        }
    }
}
