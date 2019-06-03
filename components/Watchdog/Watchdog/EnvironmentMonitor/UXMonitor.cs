using HPE.HSP.UA3.Core.API.Logger;
using HPE.HSP.UA3.Core.API.Logger.Interfaces;
using System;
using System.Management;
using System.Net;
using Watchdog.Domain;
using Watchdog.Monitor;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Microsoft.Web.Administration;
using System.Linq;
using System.Threading;

namespace Watchdog.EnvironmentMonitor
{
    class UXMonitor : HealthMonitorBase
    {
        private string url = string.Empty;
        private string username = string.Empty;
        private string password = string.Empty;
        private string loggedInUsername = string.Empty;
        private string baseaddress = string.Empty;        
        private UXConfig serviceConfigData = null;
        private string iisServerName = string.Empty;               
        private HttpWebResponse responseUX;
        private HttpWebResponse response;
        private ApplicationPool appPool = null;
        private int serverTime = 0;

        public UXMonitor(UXConfig serviceData, string Username, string Password, string LoggedInUsername, ILogger logger)
            : base(serviceData, logger)
        {
            this.serviceConfigData = serviceData;            
            this.username = Username;
            this.password = Password;
            this.loggedInUsername = LoggedInUsername;
            this.iisServerName = serviceConfigData.ServerName;
            this.applicationHealthInformation.ServiceType = "UXServices";
            this.applicationHealthInformation.ServiceName = serviceConfigData.Name;
            this.url = serviceData.HealthUrl;
            this.baseaddress = serviceData.BaseAddress;
            this.serverTime = Convert.ToInt32(serviceData.SleepTime);
        }

        public override bool CheckServiceAvailabilityAsync()
        {               
            bool isServiceHealthy = false;
            bool isServiceActiveForHealthCheck = false;
            bool isServiceActiveForADFSLogin = false;
            bool isApplicationPoolActiveOrNot = false;

            isApplicationPoolActiveOrNot = this.RestartServiceForApplicationPool(isApplicationPoolActiveOrNot);
            isServiceActiveForHealthCheck = this.ServiceCheckForHealthCheck(isServiceActiveForHealthCheck);
            isServiceActiveForADFSLogin = this.ServiceCheckForADFSLogin(isServiceActiveForADFSLogin);
            if (isServiceActiveForHealthCheck && isServiceActiveForADFSLogin && isApplicationPoolActiveOrNot)
            {
                LoggerManager.Logger.LogInformational("---Response was Successful for UX Monitoring---" + "Status:" + "Ok");
                isServiceHealthy = true;
            }
            else
            {
                LoggerManager.Logger.LogFatal("--------Response was Unsuccessful for UX Monitoring--------");
            }
            
            return isServiceHealthy;            
        }

        private bool RestartServiceForApplicationPool(bool isApplicationPoolActiveOrNot)
        {
            bool isApplicationPoolSuccessfullyStarted = false;
            if (!string.IsNullOrEmpty(this.iisServerName) && !string.IsNullOrEmpty(serviceConfigData.ApplicationPoolName))
            {
                try
                {
                    using (ServerManager manager = ServerManager.OpenRemote(this.iisServerName))
                    {
                        appPool = manager.ApplicationPools.FirstOrDefault(ap => ap.Name == serviceConfigData.ApplicationPoolName);
                        
                        if (appPool != null)
                        {
                            this.applicationHealthInformation.ApplicationPool = this.appPool.Name;
                            bool appPoolStopped = appPool.State == ObjectState.Stopped || appPool.State == ObjectState.Stopping;
                            if (appPoolStopped)
                                isApplicationPoolActiveOrNot = this.StartingApplicationPool(appPool, appPoolStopped, isApplicationPoolSuccessfullyStarted, serverTime);
                            else
                                isApplicationPoolActiveOrNot = true;
                            logger.LogInformational("Application pool : " + appPool.Name + " is running successfully");
                        }
                        else
                        {
                            this.applicationHealthInformation.RestartStatus = "Failed";
                            throw new Exception("Application Pool doesn't exist : " + serviceConfigData.ApplicationPoolName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.applicationHealthInformation.RestartStatus = "Failed";
                    logger.LogError("Error occured while attempting to restart the application pool : " + serviceConfigData.ApplicationPoolName, ex);
                    throw ex;
                }
            }

            return isApplicationPoolActiveOrNot;
        }

        private bool StartingApplicationPool(ApplicationPool appPool, bool isAppPoolStopped, bool isApplicationPoolSuccessfullyStarted,int time)
        {
            if (isAppPoolStopped)
            {
                try
                {
                    appPool.Start();
                    Thread.Sleep(time);
                }
                catch (Exception ex)
                {
                    this.applicationHealthInformation.RestartStatus = "Failed";
                    logger.LogError("Error occured while recycling application pool", ex);
                }

                if (appPool.State == ObjectState.Started)
                {
                    this.applicationHealthInformation.RestartStatus = "Restarted";
                    isApplicationPoolSuccessfullyStarted = true;
                    logger.LogInformational("Application pool: " + serviceConfigData.ApplicationPoolName + " has been recycled successfully");
                }
            }
            return isApplicationPoolSuccessfullyStarted;
        }

        private bool ServiceCheckForHealthCheck(bool isServiceActiveOrNot)
        {
            WebRequest wr = WebRequest.Create(url);
            wr.Timeout = 10000;            
            try
            {
                response = (HttpWebResponse)wr.GetResponse();
                if (response != null)
                {
                    LoggerManager.Logger.LogInformational("---Response was Successful for UX Monitoring Health Check---" + "Status:" + response.StatusCode);
                    isServiceActiveOrNot = true;
                }                
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogFatal("--------Response was Unsuccessful for UX Monitoring Health Check--------" + "Status:" + ex.Message);
            }
            return isServiceActiveOrNot;
        }

        private bool ServiceCheckForADFSLogin(bool isLoginSuccessOrNot)
        {
            try
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--headless");
                using (var web = new ChromeDriver(options))
                {
                    web.Url = baseaddress;
                    web.Manage().Window.Maximize();
                    web.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    web.FindElement(By.XPath(UXMonitoringConstants.LoginXpath)).Click();
                    web.FindElement(By.Id(UXMonitoringConstants.UserName)).SendKeys(username);
                    web.FindElement(By.Id(UXMonitoringConstants.Password)).SendKeys(password + Keys.Enter);
                    var wait = new WebDriverWait(web, TimeSpan.FromSeconds(100));
                    wait.Until(d => d.Title.Equals(UXMonitoringConstants.SuccessfullyLoggedInPageTitle));
                    IWebElement res = web.FindElement(By.Id(UXMonitoringConstants.SuccessfullyLoggedInWelcomeID));
                    isLoginSuccessOrNot = res.Text.Contains(loggedInUsername);
                    LoggerManager.Logger.LogInformational("----Logged-in Successfully----");
                    if (isLoginSuccessOrNot)
                        web.Quit();
                    return isLoginSuccessOrNot;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogFatal("Error occured during health check. Please check the log files for more details.", ex);

                return isLoginSuccessOrNot;
            }
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

        protected override void BuildServiceHealthInformation(Tuple<double, double, float, double> cpuMemData)
        {
            this.applicationHealthInformation.CPUUsagePercent = cpuMemData.Item1;
            this.applicationHealthInformation.MemoryUsagePercent = cpuMemData.Item2;
            this.applicationHealthInformation.processMemInKB = cpuMemData.Item3;
            this.applicationHealthInformation.processMemInGB = cpuMemData.Item4;
            this.applicationHealthInformation.Status = "Running";
        }

        protected override void RestartService()
        {            
        }                      
    }
}
