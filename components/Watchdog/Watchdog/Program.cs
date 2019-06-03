using HPE.HSP.UA3.Core.API.Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Watchdog.Domain;
using Watchdog.EnvironmentMonitor;
using Watchdog.Monitor;
using System.Linq;

namespace Watchdog
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                LoggerManager.Logger.LogInformational("--------------Monitoring started-----------");

                ConfigurationProvider.LoadConfiguration();

                Tenant tenant = new Tenant();                

                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(() => MonitorWindowsServices()));

                tasks.Add(Task.Run(() => MonitorBASApplicationPool()));

                tasks.Add(Task.Run(() => MonitorBAS(tenant)));

                tasks.Add(Task.Run(() => MonitorK2(tenant)));

                tasks.Add(Task.Run(() => MonitorInRule(tenant)));

                tasks.Add(Task.Run(() => MonitorUXServices(tenant)));

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
            ConfigurationProvider.WatchdogConfiguration.BASConfiguration.BASServiceList.FindAll(s => s.Monitor == true).ForEach(config =>
            {
                BASMonitor basMonitor = new BASMonitor(config, ConfigurationProvider.WatchdogConfiguration.BASConfiguration.ServerName, ConfigurationProvider.WatchdogConfiguration.BASConfiguration.SiteName,
                    config.GetEndpointURL(ConfigurationProvider.WatchdogConfiguration.BASConfiguration.BaseAddress), tenant.Id, LoggerManager.Logger);
                basMonitor.Monitor();
            });
        }

        private static void MonitorBASApplicationPool()
        {
            ConfigurationProvider.WatchdogConfiguration.BASConfiguration.BASApplicationPoolList.FindAll(s => s.Monitor == true).ForEach(config =>
            {
                List<ApplicationPool> applicationPools = ListOfApplicationPools(config);
                foreach(ApplicationPool appPoolName in applicationPools)
                {
                    ApplicationPoolMonitor basApplicationPoolMonitor = new ApplicationPoolMonitor(config, LoggerManager.Logger, appPoolName);
                    basApplicationPoolMonitor.Monitor();
                }                
            });
        }

        private static List<ApplicationPool> ListOfApplicationPools(ApplicationPoolConfigDataItem serviceConfigData)
        {
            List<ApplicationPool> ListofApplicationPools = new List<ApplicationPool>();
            using (ServerManager manager = ServerManager.OpenRemote(serviceConfigData.ServerName))
            {

                if (manager.ApplicationPools != null && manager.ApplicationPools.Count > 0)
                {
                    var result = manager.ApplicationPools.Where(s => s.Name.Contains("HP.DataServices"));
                   if(result!= null && result.Count() > 0)
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
            ConfigurationProvider.WatchdogConfiguration.WindowsServiceConfiguration.WindowsServiceList.FindAll(s => s.Monitor == true).ForEach(config =>
            {
                WindowsServiceMonitor windowsServicesMonitor = new WindowsServiceMonitor(config, LoggerManager.Logger, string.Empty);
                windowsServicesMonitor.Monitor();
            });
        }

        private static void MonitorK2(Tenant tenant)
        {
            ConfigurationProvider.WatchdogConfiguration.K2Configuration.K2ServiceList.FindAll(s => s.Monitor == true).ForEach(config =>
            {
                K2Monitor k2Monitor = new K2Monitor(config, ConfigurationProvider.WatchdogConfiguration.K2Configuration.ServerName, ConfigurationProvider.WatchdogConfiguration.K2Configuration.SiteName,
                    config.GetEndpointURL(ConfigurationProvider.WatchdogConfiguration.K2Configuration.BaseAddress), tenant.Id, LoggerManager.Logger);
                k2Monitor.Monitor();
            });
        }

        private static void MonitorInRule(Tenant tenant)
        {
            ConfigurationProvider.WatchdogConfiguration.InRuleConfiguration.InRuleServiceList.FindAll(s => s.Monitor == true).ForEach(config =>
            {
                InRuleMonitor inRuleMonitor = new InRuleMonitor(config, ConfigurationProvider.WatchdogConfiguration.InRuleConfiguration.ServerName, ConfigurationProvider.WatchdogConfiguration.InRuleConfiguration.SiteName,
                    config.GetEndpointURL(ConfigurationProvider.WatchdogConfiguration.InRuleConfiguration.BaseAddress), tenant.Id, LoggerManager.Logger);
                inRuleMonitor.Monitor();
            });
        }

        private static void MonitorUXServices(Tenant tenant)
        {
            if (ConfigurationProvider.WatchdogConfiguration.UXConfiguration.Monitor)
            {
                UXMonitor uxMonitor = new UXMonitor(ConfigurationProvider.WatchdogConfiguration.UXConfiguration, ConfigurationProvider.WatchdogConfiguration.UXConfiguration.Username, ConfigurationProvider.WatchdogConfiguration.UXConfiguration.Password, ConfigurationProvider.WatchdogConfiguration.UXConfiguration.LoggedInUsername, LoggerManager.Logger);
                uxMonitor.Monitor();
            }
        }
    }
}
