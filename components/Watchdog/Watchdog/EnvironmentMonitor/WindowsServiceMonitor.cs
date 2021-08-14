//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of Gainwell Technologies, Copyright (c) 2020. All rights reserved.
//-----------------------------------------------------------------------------------------

using HPE.HSP.UA3.Core.API.Logger.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Threading;
using Watchdog.Domain;

namespace Watchdog.Monitor
{
    public class WindowsServiceMonitor : HealthMonitorBase
    {
        private WindowsServiceConfigItem serviceConfigData = null;
        private string tenantId = string.Empty;

        public WindowsServiceMonitor(WindowsServiceConfigItem serviceData, ILogger logger, string tenantId) :
            base(serviceData, logger)
        {
            this.serviceConfigData = serviceData;
            this.tenantId = tenantId;
            this.applicationHealthInformation.ServiceType = "Windows";
            this.applicationHealthInformation.ServiceName = serviceConfigData.Name;
        }

        public override bool CheckServiceAvailabilityAsync()
        {
            bool isServiceAvailable = true;

            ServiceController service = ServiceController.GetServices().ToList().FirstOrDefault(s => s.ServiceName.Equals(this.serviceConfigData.Name, StringComparison.OrdinalIgnoreCase));

            if (service == null)
            {
                isServiceAvailable = false;
                this.applicationHealthInformation.ErrorMessage = "Service unavailable to monitor";
            }
            else
            {
                if (service.Status == ServiceControllerStatus.Stopped || service.Status == ServiceControllerStatus.Paused || service.Status == ServiceControllerStatus.StopPending)
                {
                    isServiceAvailable = false;
                }
            }

            return isServiceAvailable;
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
            ServiceController service = ServiceController.GetServices().ToList().FirstOrDefault(s => s.ServiceName.Equals(this.serviceConfigData.Name, StringComparison.OrdinalIgnoreCase));
            if (service == null)
            {                
                this.applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                this.applicationHealthInformation.ErrorMessage = "Service unavailable to monitor";
                return;
            }

            try
            {
              
                service.Start();
                this.applicationHealthInformation.RestartStatus = Constants.Status.Restarted;
                this.applicationHealthInformation.Status = "Service Started Successfully";
                Thread.Sleep(1000);
            }
            catch(Exception ex)
            {
                this.applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                this.applicationHealthInformation.ErrorMessage = "Error occured while restarting service : " + ex.Message;
                logger.LogError("Error occured while restarting service", ex);
            }

            service = ServiceController.GetServices().ToList().FirstOrDefault(s => s.ServiceName.Equals(this.serviceConfigData.Name, StringComparison.OrdinalIgnoreCase));
            if (service.Status == ServiceControllerStatus.Running)
            {
                this.applicationHealthInformation.RestartStatus = Constants.Status.Restarted;

                logger.LogInformational(serviceConfigData.Name + " has been restarted successfully");
            }
        }
    }
}
