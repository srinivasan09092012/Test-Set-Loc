//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of Gainwell Technologies, Copyright (c) 2020. All rights reserved.
//-----------------------------------------------------------------------------------------

using HPE.HSP.UA3.Core.API.Logger.Interfaces;
using Microsoft.Web.Administration;
using System;
using System.Diagnostics;
using System.Management;
using System.Threading;
using Watchdog.Domain;
using Watchdog.Monitor;

namespace Watchdog.EnvironmentMonitor
{
    public class ApplicationPoolMonitor : HealthMonitorBase
    {
        private int sleepTime = 0;
        private ApplicationPool appPool = null;
        public ApplicationPoolMonitor(ServiceConfigMetaData serviceData, ILogger logger, ApplicationPool applicationPool, int sleepTime)
            : base(serviceData, logger)
        {
            this.sleepTime = sleepTime;//Convert.ToInt32(serviceData.Time);
            this.applicationHealthInformation.ServiceType = "Application Pool";
            this.applicationHealthInformation.ServiceName = applicationPool.Name;
            this.appPool = applicationPool;
        }

        protected override void RestartService()
        {
        }

        public override bool CheckServiceAvailabilityAsync()
        {
            bool isServiceHealthy = false;

            bool isApplicationPoolActiveOrNot = false;

            isApplicationPoolActiveOrNot = this.GetAndRestartServiceForApplicationPool(isApplicationPoolActiveOrNot);
            if (isApplicationPoolActiveOrNot)
            {
                logger.LogInformational("All Application pools are running successfully");
                this.applicationHealthInformation.Status = Constants.Status.Running;
                isServiceHealthy = true;
            }
            return isServiceHealthy;
        }

        private bool GetAndRestartServiceForApplicationPool(bool isApplicationPoolActiveOrNot)
        {
            bool isApplicationPoolSuccessfullyStarted = false;
            try
            {
                if (appPool != null)
                {
                    this.applicationHealthInformation.ApplicationPool = this.appPool.Name;
                    bool appPoolStopped = appPool.State == ObjectState.Stopped || appPool.State == ObjectState.Stopping;
                    if (appPoolStopped)
                        isApplicationPoolActiveOrNot = this.StartingApplicationPool(appPool, appPoolStopped, isApplicationPoolSuccessfullyStarted, appPool.Name);
                    else
                        isApplicationPoolActiveOrNot = true;
                }
                else
                {
                    this.applicationHealthInformation.ErrorMessage = "Application Pool not available to monitor.";
                    throw new Exception("Application Pool doesn't exist");
                }
            }
            catch (Exception ex)
            {
                this.applicationHealthInformation.RestartStatus =Constants.Status.Failed;
                logger.LogError("Error occured while attempting to restart the application pool : " + appPool.Name, ex);
                throw ex;
            }

            return isApplicationPoolActiveOrNot;
        }

        private bool StartingApplicationPool(ApplicationPool appPool, bool isAppPoolStopped, bool isApplicationPoolSuccessfullyStarted, string appPoolName)
        {
            if (isAppPoolStopped)
            {
                try
                {
                    appPool.Start();
                    Thread.Sleep(this.sleepTime);
                }
                catch (Exception ex)
                {
                    this.applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                    logger.LogError("Error occured while starting application pool: " + appPoolName, ex);
                }

                if (appPool.State == ObjectState.Started)
                {
                    this.applicationHealthInformation.RestartStatus = Constants.Status.Restarted;
                    isApplicationPoolSuccessfullyStarted = true;
                    logger.LogInformational("Application pool: " + appPoolName + " has been started successfully");
                }
            }
            return isApplicationPoolSuccessfullyStarted;
        }

        protected override void BuildServiceHealthInformation(Tuple<double, double, float, double> cpuMemData)
        {
            this.applicationHealthInformation.CPUUsagePercent = cpuMemData.Item1;
            this.applicationHealthInformation.MemoryUsagePercent = cpuMemData.Item2;
            this.applicationHealthInformation.processMemInKB = cpuMemData.Item3;
            this.applicationHealthInformation.processMemInGB= cpuMemData.Item4;
            this.applicationHealthInformation.Status = Constants.Status.Running;
        }

        protected override Process GetProcessIdForService()
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
                    if (commandLine.ToLower().Contains(appPool.Name.ToLower()))
                    {
                        process = Process.GetProcessById(processId);
                        break;
                    }
                }
            }

            return process;
        }

    }
}