//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of Gainwell Technologies, Copyright (c) 2020. All rights reserved.
//-----------------------------------------------------------------------------------------

using HPE.HSP.UA3.Core.API.Logger;
using HPE.HSP.UA3.Core.API.Logger.Interfaces;
using Microsoft.Web.Administration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using Watchdog.Domain;
using Watchdog.Monitor;

namespace Watchdog.EnvironmentMonitor
{
    class UXMonitor : HealthMonitorBase
    {
        private string url = string.Empty;
        private string username = string.Empty;
        private string password = string.Empty;
        private string loggedInUsername = string.Empty;
        private string baseaddress = string.Empty;
        private UXApplicationConfig serviceConfigData = null;
        private string iisServerName = string.Empty;
        private HttpWebResponse response;
        private int serverTime = 0;
        private string restartStatus = string.Empty;
        

        public UXMonitor(UXApplicationConfig serviceData, string serverName, string siteName, string restartStatus, ILogger logger)
            : base(serviceData, logger)
        {
            this.serviceConfigData = serviceData;
            this.iisServerName = serverName;
            this.serviceConfigData.SiteName = siteName;
            this.applicationHealthInformation.ServiceType = "UXServices";
            this.applicationHealthInformation.ServiceName = serviceConfigData.Name;
            this.url = serviceData.Healthurl;
            this.serverTime = serviceData.SleepInterval;
            this.restartStatus = restartStatus;
        }

        public override bool CheckServiceAvailabilityAsync()
        {
            bool isServiceHealthy = false;
            bool isServiceActiveOrNot = false;
            bool isServiceActiveForHealthCheck = false;
            bool isServiceActiveForADFSLogin = false;

            isServiceActiveOrNot = this.RestartServiceForSite(isServiceActiveOrNot);
            if (isServiceActiveOrNot)
            {
                isServiceActiveForHealthCheck = this.ServiceCheckForHealthCheck(isServiceActiveForHealthCheck);
                isServiceActiveForADFSLogin = this.ServiceCheckForADFSLogin(isServiceActiveForADFSLogin);
            }

            if (isServiceActiveForHealthCheck && isServiceActiveForADFSLogin)
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

        protected override void SetServiceUnavailableStatus()
        {
            this.applicationHealthInformation.Status = UXMonitoringConstants.LoginFailed;
        }


        private bool ServiceCheckForHealthCheck(bool isServiceActiveOrNot)
        {
            WebRequest wr = WebRequest.Create(url);
            if (String.Compare(this.restartStatus, Constants.Status.Restarted, true) == 0)
            {
                wr.Timeout = 300000;
            }
            else
            {
                wr.Timeout = 5000;
            }

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

        private void KillBrowser()
        {
            Process[] ps = Process.GetProcessesByName("chrome");

            foreach (Process p in ps)
            {
                if (!p.HasExited)
                {
                    p.Kill();
                }
            }
        }

        private void KillDriver()
        {
            Process[] ps = Process.GetProcessesByName("chromedriver");

            foreach (Process p in ps)
            {
                if (!p.HasExited)
                {
                    p.Kill();
                }
            }
        }

        private bool ServiceCheckForADFSLogin(bool isLoginSuccessOrNot)
        {
            KillBrowser();
            KillDriver();
            ChromeOptions options = new ChromeOptions();
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ChromeDriver web = new ChromeDriver(dir, options, TimeSpan.FromMinutes(5));
            try
            {
                options.AddArgument("--headless");

                web.Url = this.serviceConfigData.URLValue;
                LoggerManager.Logger.LogInformational($"Base Address : {this.serviceConfigData.URLValue}" + $"Web url : {web.Url}");
                web.Manage().Window.Maximize();
                web.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                web.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
                web.FindElement(By.XPath(UXMonitoringConstants.LoginXpath)).Click();
                web.FindElement(By.Id(UXMonitoringConstants.UserName)).SendKeys(this.serviceConfigData.Username);
                web.FindElement(By.Id(UXMonitoringConstants.Password)).SendKeys(this.serviceConfigData.Password + Keys.Enter);
                var wait = new WebDriverWait(web, TimeSpan.FromSeconds(300));
                wait.Until(d => d.Title.Equals(UXMonitoringConstants.SuccessfullyLoggedInPageTitle));
                IWebElement res = web.FindElement(By.Id(UXMonitoringConstants.SuccessfullyLoggedInWelcomeID));
                isLoginSuccessOrNot = res.Text.ToLower().Contains(this.serviceConfigData.LoggedInUsername.ToLower());
                LoggerManager.Logger.LogInformational("----Logged-in Successfully----");
                web.Quit();
                return isLoginSuccessOrNot;
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogFatal("Error occured during health check. Please check the log files for more details.", ex);

                return isLoginSuccessOrNot;
            }
            finally
            {
                web.Quit();
            }
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
                            logger.LogInformational(serviceConfigData.SiteName + " site Status :" + siteName.State.ToString());
                            isServiceActiveOrNot = this.StartSiteNameService(siteName, siteNameStopped);
                        }
                        else
                        {
                            this.applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                            throw new Exception("Site Name doesn't exist : " + serviceConfigData.SiteName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.applicationHealthInformation.RestartStatus = Constants.Status.Failed;
                    logger.LogError("Error occured while attempting to restart the site : " + serviceConfigData.SiteName, ex);
                    throw ex;
                }
            }

            return isServiceActiveOrNot;
        }

        private bool StartSiteNameService(Site siteName, bool siteNameStopped)
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
                    logger.LogError("Error occured while starting the site: " + siteName, ex);
                }

                if (siteName.State == ObjectState.Started)
                {
                    this.applicationHealthInformation.RestartStatus = Constants.Status.Restarted;
                    siteNameStopped = false;
                    logger.LogInformational("Site Name: " + siteName + " has been started successfully");
                }
            }

            return !siteNameStopped;
        }

        protected override Process GetProcessIdForService()
        {
            return null;
        }

        protected override void BuildServiceHealthInformation(Tuple<double, double, float, double> cpuMemData)
        {
            this.applicationHealthInformation.CPUUsagePercent = cpuMemData.Item1;
            this.applicationHealthInformation.MemoryUsagePercent = cpuMemData.Item2;
            this.applicationHealthInformation.processMemInKB = cpuMemData.Item3;
            this.applicationHealthInformation.processMemInGB = cpuMemData.Item4;
            this.applicationHealthInformation.Status = UXMonitoringConstants.LoginSuccess;
        }

        protected override void RestartService()
        {
        }
    }
}