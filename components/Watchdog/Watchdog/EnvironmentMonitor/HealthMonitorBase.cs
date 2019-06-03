using HPE.HSP.UA3.Core.API.Logger.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Watchdog.Domain;
using Watchdog.Providers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace Watchdog.Monitor
{
    public abstract class HealthMonitorBase : IHealthMonitor
    {
        protected ILogger logger;

        protected ServiceConfigMetaData serviceDetail;

        protected ServiceHealthInformation applicationHealthInformation = null;

        public HealthMonitorBase(ServiceConfigMetaData serviceConfig, ILogger logger)
        {
            this.serviceDetail = serviceConfig;
            this.logger = logger;
            this.applicationHealthInformation = new ServiceHealthInformation { ServiceName = serviceConfig.Name, Environment = ConfigurationProvider.WatchdogConfiguration.Environment };
        }
        public virtual ServiceHealthInformation Monitor()
        {
            ServiceHealthInformation serviceHealthData = null;

            try
            {
                logger.LogInformational("Monitoring started for : " + serviceDetail.Name);

                bool isServiceAvailable = this.GetServiceAvailabilityStatus();

                if (isServiceAvailable)
                {
                    this.CollectServicePerformanceData();
                }
                else
                {
                    logger.LogError(string.Format("{0} unavailble to monitor.", serviceDetail.Name));
                    this.applicationHealthInformation.Status = "Not Running";
                }

                this.LogServiceHealthInformation();

                logger.LogInformational("Monitoring ends for : " + serviceDetail.Name);
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format("Error occured while monitoring {0}.", serviceDetail.Name), ex);
            }

            return serviceHealthData;
        }

        protected bool GetServiceAvailabilityStatus()
        {
            bool isServiceAvailable = this.CheckServiceAvailabilityAsync();
            int retryCount = 0;

            while (!isServiceAvailable && retryCount < serviceDetail.MaxRetryCount)
            {
                if (string.Compare(serviceDetail.ApplicationDownAction, "Restart", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    logger.LogInformational(serviceDetail.Name + " Service unavailable to monitor, attempting to restart.");
                    this.RestartService();
                }

                isServiceAvailable = this.CheckServiceAvailabilityAsync();

                retryCount++;
            }

            return isServiceAvailable;
        }

        protected abstract void RestartService();

        public abstract bool CheckServiceAvailabilityAsync();

        private void CollectServicePerformanceData()
        {
            Process process = this.GetProcessIdForService();            
            List<Tuple<double, double, float, double>> cpuMemDataList = new List<Tuple<double, double, float, double>>();
            if (process != null)
            {
                for (int i = 0; i < serviceDetail.PerformanceSampleCount; i++)
                {
                    cpuMemDataList.Add(CPUAndMemUsageProvider.GetProcessCPUAndMemoryUsagePercent(logger, process));
                }

                Tuple<double, double, float, double> averageCPUMemPerfSample = new Tuple<double, double, float, double>(Math.Round(cpuMemDataList.Select(x => x.Item1).Average(), 3), Math.Round(cpuMemDataList.Select(x => x.Item2).Average(), 2), (cpuMemDataList.Select(x => x.Item3).Average()), Math.Round(cpuMemDataList.Select(x => x.Item4).Average(), 3));
                this.BuildServiceHealthInformation(averageCPUMemPerfSample);
            }
        }

        protected abstract Process GetProcessIdForService();

        protected abstract void BuildServiceHealthInformation(Tuple<double, double, float, double> cpuMemData);

        private void LogServiceHealthInformation()
        {
            this.LogServiceHealthInfoToLogFile();
            if (string.IsNullOrEmpty(ConfigurationProvider.WatchdogConfiguration.LogAnalyticsURL))
            {
                logger.LogWarning("LogAnalyticsURL is empty");
            }
            else
            {
                this.LogServiceHealthInfoToLogAnalytics();
            }
        }

        private HttpWebRequest GetLogAnalyticsRequest(int contentLength, string date)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationProvider.WatchdogConfiguration.LogAnalyticsURL);
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Headers["Log-Type"] = "HPP_Watchdog_Statistics";
            request.Headers["x-ms-date"] = date;
            request.Headers["Authorization"] = this.HashSignature("POST", contentLength, "application/json", date, "/api/logs");
            request.Proxy = null;

            return request;
        }

        private string HashSignature(string method, int contentLength, string contentType, string date, string resource)
        {
            byte[] sharedKeyBytes = Convert.FromBase64String(ConfigurationProvider.WatchdogConfiguration.SharedKey);
            var stringtoHash = method + "\n" + contentLength + "\n" + contentType + "\nx-ms-date:" + date + "\n" + resource;
            var bytesToHash = new System.Text.ASCIIEncoding().GetBytes(stringtoHash);
            using (var sha256 = new HMACSHA256(sharedKeyBytes))
            {
                var calculatedHash = sha256.ComputeHash(bytesToHash);
                var stringHash = Convert.ToBase64String(calculatedHash);
                return "SharedKey " + ConfigurationProvider.WatchdogConfiguration.LogAnalyticsWorkspaceId + ":" + stringHash;
            }
        }

        private void LogServiceHealthInfoToLogFile()
        {
            logger.LogInformational(string.Format("---------------Health Information for {0} Service ---------------", serviceDetail.Name));
            logger.LogInformational("Environment : " + this.applicationHealthInformation.Environment);
            logger.LogInformational("Computer : " + this.applicationHealthInformation.Computer);
            logger.LogInformational("TenantID : " + this.applicationHealthInformation.ApplicationTenantId);
            logger.LogInformational("Service Name : " + this.applicationHealthInformation.ServiceName);
            logger.LogInformational("Service status : " + this.applicationHealthInformation.Status);
            logger.LogInformational("Service type : " + this.applicationHealthInformation.ServiceType);
            logger.LogInformational("Restart status : " + this.applicationHealthInformation.RestartStatus);
            logger.LogInformational("CPU Usage percentage: " + this.applicationHealthInformation.CPUUsagePercent);
            logger.LogInformational("Memory Usage percentage: " + this.applicationHealthInformation.MemoryUsagePercent);
            logger.LogInformational("------------------------------------------------------------------");
        }

        private void LogServiceHealthInfoToLogAnalytics()
        {
            HttpWebResponse response = null;
            try
            {
                var utf8Encoding = new UTF8Encoding();
                Byte[] content = utf8Encoding.GetBytes(JsonConvert.SerializeObject(this.applicationHealthInformation));
                var rfcDate = DateTime.Now.ToUniversalTime().ToString("r");
                HttpWebRequest request = this.GetLogAnalyticsRequest(content.Length, rfcDate);

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(content, 0, content.Length);
                response = (HttpWebResponse)request.GetResponse();
                if (!response.StatusCode.Equals(HttpStatusCode.OK) && !response.StatusCode.Equals(HttpStatusCode.Accepted))
                {
                    Stream dataStream = response.GetResponseStream();
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        string errorMessage = reader.ReadToEnd();
                        logger.LogError("Error returned from log analytics API : " + errorMessage);
                    }
                }

                logger.LogInformational("Response code from Log Analytics API : " + response.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured while sending data to log analytics", ex);
            }
            finally
            {
                if(response != null)
                {
                    response.Close();
                }
            }
        }
    }
}
