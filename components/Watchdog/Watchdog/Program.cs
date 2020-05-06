//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
//-----------------------------------------------------------------------------------------

using HPE.HSP.UA3.Core.API.Logger;
using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watchdog.Domain;
using Watchdog.EnvironmentMonitor;
using Watchdog.Monitor;

namespace Watchdog
{
    public class WatchDogProgram
    {
        public static void Main(string[] args)
        {
            try
            {
                LoggerManager.Logger.LogInformational("--------------Monitoring started-----------");

                ConfigurationProvider.LoadConfiguration();

                LoadUnityConfiguration();

                Tenant tenant = new Tenant();

                List<Task> tasks = new List<Task>();

                tasks.Add(Task.Run(() => MonitorWindowsServices()));

                tasks.Add(Task.Run(() => MonitorBASApplicationPool()));

                tasks.Add(Task.Run(() => MonitorBAS(tenant)));

                tasks.Add(Task.Run(() => MonitorK2Service(tenant)));

                tasks.Add(Task.Run(() => MonitorInRule(tenant)));

                tasks.Add(Task.Run(() => MonitorUXServices(tenant)));

                tasks.Add(Task.Run(() => MonitorAddressDoctor(tenant)));

                Task.WaitAll(tasks.ToArray());

                LoggerManager.Logger.LogInformational("--------------Monitoring ended-----------");
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogFatal("Error occured during health check. Please check the log files for more details.", ex);
            }
        }

        private static void MonitorBAS(Tenant tenant)
        {
            ConfigurationProvider.WatchdogConfiguration.BASConfiguration?.BASServiceList.FindAll(s => s.Monitor == true).ForEach(config =>
            {
                BASMonitor basMonitor = new BASMonitor(config, ConfigurationProvider.WatchdogConfiguration.BASConfiguration.ServerName, ConfigurationProvider.WatchdogConfiguration.BASConfiguration.SiteName,
                    config.GetEndpointURL(ConfigurationProvider.WatchdogConfiguration.BASConfiguration.BaseAddress), tenant.Id, LoggerManager.Logger);
                basMonitor.Monitor();
            });
        }

        private static void MonitorBASApplicationPool()
        {
            ConfigurationProvider.WatchdogConfiguration.BASConfiguration?.BASApplicationPoolList.FindAll(s => s.Monitor == true).ForEach(config =>
            {
                List<ApplicationPool> applicationPools = ListOfApplicationPools(config.ServerName, config.Sitename);
                foreach (ApplicationPool appPoolName in applicationPools)
                {
                    ApplicationPoolMonitor basApplicationPoolMonitor = new ApplicationPoolMonitor((ServiceConfigMetaData)config, LoggerManager.Logger, appPoolName, Convert.ToInt32(config.Time));
                    ServiceHealthInformation info = basApplicationPoolMonitor.Monitor();
                }
            });
        }

        private static void MonitorAddressDoctor(Tenant tenant)
        {
            ConfigurationProvider.WatchdogConfiguration.AddressDoctorConfiguration?.AddressDoctorServices.FindAll(s => s.Monitor == true).ForEach(config =>
            {
                AddressDoctorMonitor addressDoctor = new AddressDoctorMonitor(config, tenant.Id, LoggerManager.Logger);
                addressDoctor.Monitor();
            });
        }

        private static void MonitorK2Service(Tenant tenant)
        {
            ConfigurationProvider.WatchdogConfiguration.K2ServiceConfiguration?.K2ServiceList.FindAll(s => s.Monitor == true).ForEach(config =>
            {
                K2ServiceMonitor k2serviceMonitor = new K2ServiceMonitor(config, ConfigurationProvider.WatchdogConfiguration.K2ServiceConfiguration.ServerName, ConfigurationProvider.WatchdogConfiguration.K2ServiceConfiguration.SiteName,
                    config.GetEndpointURL(ConfigurationProvider.WatchdogConfiguration.K2ServiceConfiguration.BaseAddress), tenant.Id, LoggerManager.Logger);
                k2serviceMonitor.Monitor();
            });
        }

        private static List<ApplicationPool> ListOfApplicationPools(string serverName, string applicationPoolName)
        {
            List<ApplicationPool> ListofApplicationPools = new List<ApplicationPool>();
            using (ServerManager manager = ServerManager.OpenRemote(serverName))
            {

                if (manager.ApplicationPools != null && manager.ApplicationPools.Count > 0)
                {
                    var result = manager.ApplicationPools.Where(s => s.Name.Contains(applicationPoolName));
                    if (result != null && result.Count() > 0)
                    {
                        var listOfApplicationPoolsFromServer = result;
                        foreach (ApplicationPool application in listOfApplicationPoolsFromServer)
                        {
                            ListofApplicationPools.Add(application);
                        }
                    }
                }
            }
            return ListofApplicationPools;
        }

        private static void MonitorWindowsServices()
        {
            ConfigurationProvider.WatchdogConfiguration.WindowsServiceConfiguration?.WindowsServiceList.FindAll(s => s.Monitor == true).ForEach(config =>
            {
                WindowsServiceMonitor windowsServicesMonitor = new WindowsServiceMonitor(config, LoggerManager.Logger, string.Empty);
                windowsServicesMonitor.Monitor();
            });
        }   
        
        private static void MonitorInRule(Tenant tenant)
        {
            ConfigurationProvider.WatchdogConfiguration.InRuleConfiguration?.InRuleServiceList.FindAll(s => s.Monitor == true).ForEach(config =>
            {
                InRuleMonitor inRuleMonitor = new InRuleMonitor(config, ConfigurationProvider.WatchdogConfiguration.InRuleConfiguration.ServerName, ConfigurationProvider.WatchdogConfiguration.InRuleConfiguration.SiteName,
                    config.GetEndpointURL(ConfigurationProvider.WatchdogConfiguration.InRuleConfiguration.BaseAddress), tenant.Id, LoggerManager.Logger);
                inRuleMonitor.Monitor();
            });
        }

        private static void MonitorUXServices(Tenant tenant)
        {
            ConfigurationProvider.WatchdogConfiguration.UXMonitoring.WebServers?.FindAll(s => s.Monitor == true).ForEach(config =>
            {
                config.Applications.ForEach(appPoolConfig =>
                {
                    List<ApplicationPool> applicationPools = ListOfApplicationPools(config.Servername, appPoolConfig.Applicationpool);
                    foreach (ApplicationPool appPoolName in applicationPools)
                    {
                        ApplicationPoolMonitor uxapplicationPoolMonitor = new ApplicationPoolMonitor((ServiceConfigMetaData)config, LoggerManager.Logger, appPoolName, appPoolConfig.Sleeptime);
                        ServiceHealthInformation info = uxapplicationPoolMonitor.Monitor();
                        if (info.Status == Constants.Status.Running)
                        {
                            appPoolConfig.UXUrls.ForEach(urlConfig =>
                            {
                                UXMonitor uxMonitor = new UXMonitor(urlConfig, info.RestartStatus, LoggerManager.Logger);
                                uxMonitor.Monitor();
                            });
                        }
                    }
                });
            });
        }

        private static void LoadUnityConfiguration()
        {
            if (WatchdogContainer.IocContainer == null)
            {
                WatchdogContainer.InitializeConfiguration();
            }
        }
    }
}