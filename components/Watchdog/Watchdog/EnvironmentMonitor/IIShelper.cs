//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
//-----------------------------------------------------------------------------------------
using HPE.HSP.UA3.Core.API.Logger.Interfaces;
using Microsoft.Web.Administration;
using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading;
using Watchdog.Domain;

namespace Watchdog.EnvironmentMonitor
{
    public class IISHelper
    {
        public static Process GetProcessIdForService(string applicationPoolName)
        {
            Process process = null;
            int processId = 0;
            string commandLine = String.Empty;
            ManagementClass MgmtClass = new ManagementClass("Win32_Process");
            foreach (ManagementObject mo in MgmtClass.GetInstances())
            {
                if (mo["Name"].ToString() == "w3wp.exe")
                {
                    processId = Convert.ToInt32(mo["ProcessId"]);
                    commandLine = mo["CommandLine"].ToString();
                    if (commandLine.ToLower().Contains(applicationPoolName.ToLower()))
                    {
                        process = Process.GetProcessById(processId);
                        break;
                    }
                }
            }

            return process;
        }

        public static void StartApplicationPool(ApplicationPool appPool, bool isAppPoolStopped, string applicationPoolName, ILogger logger, ref ServiceHealthInformation applicationHealthInformation)
        {
            if (isAppPoolStopped)
            {
                try
                {
                    appPool.Start();
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                    logger.LogError("Error occured while recycling application pool", ex);
                }

                if (appPool.State == ObjectState.Started)
                {
                    applicationHealthInformation.RestartStatus = Constants.Status.Restarted;
                    logger.LogInformational("Application pool: " + applicationPoolName + " has been recycled successfully");
                }
            }
        }

        public static void RestartServiceForSite(string iisServerName,ILogger logger,ref ServiceHealthInformation applicationHealthInformation,K2ServiceConfigDataItem serviceConfigData)
        {
            if (!string.IsNullOrEmpty(iisServerName) && !string.IsNullOrEmpty(serviceConfigData.Name))
            {
                try
                {
                    using (ServerManager manager = ServerManager.OpenRemote(iisServerName))
                    {
                        Site siteName = manager.Sites.FirstOrDefault(s => s.Name.Equals(serviceConfigData.SiteName));

                        if (siteName != null)
                        {
                            bool siteNameStopped = siteName.State == ObjectState.Stopped || siteName.State == ObjectState.Stopping;
                            StartSiteNameService(siteName, siteNameStopped,serviceConfigData,ref applicationHealthInformation,logger);
                        }
                        else
                        {
                            applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                            throw new Exception("Site doesn't exist : " + serviceConfigData.Name);
                        }
                    }
                }
                catch (Exception ex)
                {
                   applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                    logger.LogError("Error occured while attempting to restart the site : " + serviceConfigData.ApplicationPoolName, ex);
                    throw ex;
                }
            }
        }

        private static void StartSiteNameService(Site siteName, bool siteNameStopped,K2ServiceConfigDataItem serviceConfigData, ref ServiceHealthInformation applicationHealthInformation,ILogger logger)     
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
                    applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                    logger.LogError("Error occured while recycling site service", ex);
                }

                if (siteName.State == ObjectState.Started)
                {
                    applicationHealthInformation.RestartStatus = Constants.Status.Restarted;
                    logger.LogInformational("Site Name: " + serviceConfigData.Name + " has been recycled successfully");
                }
            }
        }
        public static void RestartServiceForApplicationPool(string iisServerName,string applicationPoolName,ILogger logger, ref ServiceHealthInformation applicationHealthInformation)
        {
            if (!string.IsNullOrEmpty(iisServerName) && !string.IsNullOrEmpty(applicationPoolName))
            {
                try
                {
                    using (ServerManager manager = ServerManager.OpenRemote(iisServerName))
                    {
                        ApplicationPool appPool = manager.ApplicationPools.FirstOrDefault(ap => ap.Name == applicationPoolName);

                        if (appPool != null)
                        {
                            applicationHealthInformation.ApplicationPool = appPool.Name;
                            logger.LogInformational("Application pool status : " + appPool.State.ToString());
                            //Get the current state of the app pool
                            bool appPoolRunning = appPool.State == ObjectState.Started || appPool.State == ObjectState.Starting;
                            bool appPoolStopped = appPool.State == ObjectState.Stopped || appPool.State == ObjectState.Stopping;
                            StartApplicationPool(appPool, appPoolStopped, applicationPoolName, logger, ref applicationHealthInformation);
                        }
                        else
                        {
                            applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                            throw new Exception("Application Pool doesn't exist : " + applicationPoolName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                    logger.LogError("Error occured while attempting to restart the application pool : " + applicationPoolName, ex);
                    throw ex;
                }
            }
        }

    }
}