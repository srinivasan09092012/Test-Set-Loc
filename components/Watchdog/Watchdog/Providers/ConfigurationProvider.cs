using HPE.HSP.UA3.Core.API.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using Watchdog.Domain;

namespace Watchdog
{
    public class ConfigurationProvider
    {
        public static WatchdogConfig WatchdogConfiguration { get; private set; }

        public static void LoadConfiguration()
        {
            try
            {
                WatchdogConfiguration = new WatchdogConfig();

                XElement xmlDocument = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + Constants.FileName.WatchdogConfigFile);
                WatchdogConfiguration.Environment = GetAttributeValue<string>(xmlDocument, "environment", string.Empty);
                WatchdogConfiguration.PollInterval = GetElementValue<int>(xmlDocument, "PollInterval");
                WatchdogConfiguration.MaxRetryCount = GetElementValue<int>(xmlDocument, "MaxRetryCount");
                WatchdogConfiguration.PerformanceSampleCount = GetElementValue<int>(xmlDocument, "PerformanceSampleCount");
                WatchdogConfiguration.ApplicationDownAction = GetElementValue<string>(xmlDocument, "ApplicationDownAction");
                WatchdogConfiguration.LogAnalyticsWorkspaceId = GetElementValue<string>(xmlDocument, "LogAnalyticsWorkspaceId");                
                WatchdogConfiguration.LogAnalyticsURL = GetLogAnalyticsURL(xmlDocument, WatchdogConfiguration.LogAnalyticsWorkspaceId);
                WatchdogConfiguration.SharedKey = GetElementValue<string>(xmlDocument, "SharedKey");
                WatchdogConfiguration.TenantsConfig = GetTenantConfiguration(xmlDocument);
                WatchdogConfiguration.WindowsServiceConfiguration.WindowsServiceList = GetWindowsServicesConfiguration(xmlDocument);
                WatchdogConfiguration.BASConfiguration = GetBASConfiguration(xmlDocument);
                WatchdogConfiguration.UXConfiguration = GetUXConfiguration(xmlDocument);
                WatchdogConfiguration.K2Configuration = GetK2Configuration(xmlDocument);
                WatchdogConfiguration.InRuleConfiguration = GetInRuleConfiguration(xmlDocument);
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogError("Error occured while loading Watchdog configuration", ex);

                throw ex;
            }
        }

        private static string GetLogAnalyticsURL(XElement xmlDocument, string workspaceId)
        {
            string logAnalyticsURL = GetElementValue<string>(xmlDocument, "LogAnalyticsApiURL");

            if(!string.IsNullOrEmpty(logAnalyticsURL) && !string.IsNullOrEmpty(workspaceId))
            {
                logAnalyticsURL = logAnalyticsURL.Replace("LogAnalyticsWorkspaceId", workspaceId);
            }

            return logAnalyticsURL;
        }

        private static List<Tenant> GetTenantConfiguration(XElement xmlDocument)
        {
            XElement nodes = xmlDocument.Descendants("Tenants").FirstOrDefault();
            List<Tenant> tenantsConfig = new List<Tenant>();

            if (nodes != null)
            {
                tenantsConfig = (from node in nodes.Descendants()
                                 select new Tenant
                                 {
                                     Name = GetAttributeValue<string>(node, "name", string.Empty),
                                     Id = GetAttributeValue<string>(node, "Id", string.Empty),
                                 }).ToList();
            }

            return tenantsConfig;
        }

        private static List<WindowsServiceConfigItem> GetWindowsServicesConfiguration(XElement xmlDocument)
        {
            XElement nodes = xmlDocument.Descendants("WindowsServices").FirstOrDefault();
            List<WindowsServiceConfigItem> windowsServiceList = new List<WindowsServiceConfigItem>();
            if (nodes != null)
            {
                windowsServiceList = (from node in nodes.Descendants()
                                      select new WindowsServiceConfigItem
                                      {
                                          Name = GetAttributeValue<string>(node, "name", string.Empty),
                                          ApplicationDownAction = GetAttributeValue<string>(node, "ApplicationDownAction", WatchdogConfiguration.ApplicationDownAction),
                                          Monitor = GetAttributeValue<bool>(node, "monitor", true),
                                          MaxRetryCount = GetAttributeValue<int>(node, "maxRetryCount", WatchdogConfiguration.MaxRetryCount),
                                          PerformanceSampleCount = GetAttributeValue<int>(node, "performanceSampleCount", WatchdogConfiguration.PerformanceSampleCount)
                                      }).ToList();
            }

            return windowsServiceList;
        }

        private static BASConfig GetBASConfiguration(XElement xmlDocument)
        {
            XElement nodes = xmlDocument.Descendants("BusinessApplicationServices").FirstOrDefault();
            BASConfig basConfiguration = new BASConfig();
            if (nodes != null)
            {
                basConfiguration.BaseAddress = GetAttributeValue<string>(nodes, "baseAddress", string.Empty);
                basConfiguration.Type = GetAttributeValue<string>(nodes, "type", "WCF");                
                basConfiguration.ServerName = GetAttributeValue<string>(nodes, "serverName", string.Empty);
                basConfiguration.SiteName = GetAttributeValue<string>(nodes, "sitename", string.Empty);
                
                basConfiguration.BASApplicationPoolList = (from node in nodes.Descendants("ApplicationPool")
                                                           select new ApplicationPoolConfigDataItem
                                                           {
                                                               Name = GetAttributeValue<string>(node, "name", string.Empty),
                                                               ApplicationDownAction = GetAttributeValue<string>(node, "ApplicationDownAction", WatchdogConfiguration.ApplicationDownAction),
                                                               Monitor = GetAttributeValue<bool>(node, "Monitor", true),
                                                               MaxRetryCount = GetAttributeValue<int>(node, "maxRetryCount", WatchdogConfiguration.MaxRetryCount),
                                                               Time = GetAttributeValue<string>(node, "time", string.Empty),
                                                               ServerName = GetAttributeValue<string>(node, "serverName", string.Empty),                                                               
                                                               PerformanceSampleCount = GetAttributeValue<int>(node, "performanceSampleCount", WatchdogConfiguration.PerformanceSampleCount)
                                                           }).ToList();
                basConfiguration.BASServiceList = (from node in nodes.Descendants("Service")
                                                   select new BASConfigDataItem
                                                   {
                                                       Name = GetAttributeValue<string>(node, "name", string.Empty),                                                       
                                                       ApplicationDownAction = GetAttributeValue<string>(node, "ApplicationDownAction", WatchdogConfiguration.ApplicationDownAction),
                                                       Monitor = GetAttributeValue<bool>(node, "Monitor", true),
                                                       MaxRetryCount = GetAttributeValue<int>(node, "maxRetryCount", WatchdogConfiguration.MaxRetryCount),
                                                       ApplicationPoolName = GetAttributeValue<string>(node, "applicationPoolName", string.Empty),
                                                       Type = GetAttributeValue<string>(node, "type", WatchdogConfiguration.BASConfiguration.Type),
                                                       Endpoint = GetAttributeValue<string>(node, "endpoint", string.Empty),
                                                       PerformanceSampleCount = GetAttributeValue<int>(node, "performanceSampleCount", WatchdogConfiguration.PerformanceSampleCount)
                                                   }).ToList();
            }
            
            return basConfiguration;
        }

        private static UXConfig GetUXConfiguration(XElement xmlDocument)
        {
            XElement nodes = xmlDocument.Descendants("UXApplication").FirstOrDefault();
            UXConfig uxConfiguration = new UXConfig();
            if (nodes != null)
            {                
                uxConfiguration.HealthUrl = GetAttributeValue<string>(nodes, "healthUrl", string.Empty);
                uxConfiguration.BaseAddress = GetAttributeValue<string>(nodes, "baseAddress", string.Empty);
                uxConfiguration.ApplicationPoolName = GetAttributeValue<string>(nodes, "ApplicationPool", string.Empty);                
                uxConfiguration.Monitor = GetAttributeValue<bool>(nodes, "monitor", true);
                uxConfiguration.Username = GetAttributeValue<string>(nodes, "Username", string.Empty);
                uxConfiguration.SleepTime = GetAttributeValue<string>(nodes, "SleepTime", string.Empty);
                uxConfiguration.ServerName = GetAttributeValue<string>(nodes, "ServerName", string.Empty);
                uxConfiguration.Password = GetAttributeValue<string>(nodes, "Password", string.Empty);
                uxConfiguration.LoggedInUsername = GetAttributeValue<string>(nodes, "LoggedInUsername", string.Empty);
                uxConfiguration.MaxRetryCount = GetAttributeValue<int>(nodes, "maxRetryCount", WatchdogConfiguration.MaxRetryCount);
                uxConfiguration.PerformanceSampleCount = GetAttributeValue<int>(nodes, "performanceSampleCount", WatchdogConfiguration.PerformanceSampleCount);
            }

            return uxConfiguration;
        }

        private static InRuleConfig GetInRuleConfiguration(XElement xmlDocument)
        {
            XElement nodes = xmlDocument.Descendants("InRuleApplication").FirstOrDefault();
            InRuleConfig inRuleConfiguration = new InRuleConfig();
            if (nodes != null)
            {
                inRuleConfiguration.BaseAddress = GetAttributeValue<string>(nodes, "baseAddress", string.Empty);
                inRuleConfiguration.Type = GetAttributeValue<string>(nodes, "type", "WCF");
                inRuleConfiguration.ServerName = GetAttributeValue<string>(nodes, "serverName", string.Empty);
                inRuleConfiguration.SiteName = GetAttributeValue<string>(nodes, "sitename", string.Empty);
                inRuleConfiguration.InRuleServiceList = (from node in nodes.Descendants()
                                                         select new InRuleConfigDataItem
                                                         {
                                                             Name = GetAttributeValue<string>(node, "name", string.Empty),
                                                             ApplicationDownAction = GetAttributeValue<string>(node, "ApplicationDownAction", WatchdogConfiguration.ApplicationDownAction),
                                                             Monitor = GetAttributeValue<bool>(node, "Monitor", true),
                                                             MaxRetryCount = GetAttributeValue<int>(node, "maxRetryCount", WatchdogConfiguration.MaxRetryCount),
                                                             ApplicationPoolName = GetAttributeValue<string>(node, "applicationPoolName", string.Empty),
                                                             Type = GetAttributeValue<string>(node, "type", WatchdogConfiguration.BASConfiguration.Type),
                                                             Endpoint = GetAttributeValue<string>(node, "endpoint", string.Empty),
                                                             PerformanceSampleCount = GetAttributeValue<int>(node, "performanceSampleCount", WatchdogConfiguration.PerformanceSampleCount)
                                                         }).ToList();
            }

            return inRuleConfiguration;
        }

        private static K2Config GetK2Configuration(XElement xmlDocument)
        {
            XElement nodes = xmlDocument.Descendants("K2Application").FirstOrDefault();
            K2Config k2Configuration = new K2Config();
            if (nodes != null)
            {
                k2Configuration.BaseAddress = GetAttributeValue<string>(nodes, "baseAddress", string.Empty);
                k2Configuration.Type = GetAttributeValue<string>(nodes, "type", "WCF");
                k2Configuration.ServerName = GetAttributeValue<string>(nodes, "serverName", string.Empty);
                k2Configuration.SiteName = GetAttributeValue<string>(nodes, "sitename", string.Empty);
                k2Configuration.K2ServiceList = (from node in nodes.Descendants()
                                                 select new K2ConfigDataItem
                                                 {
                                                     Name = GetAttributeValue<string>(node, "name", string.Empty),
                                                     ApplicationDownAction = GetAttributeValue<string>(node, "ApplicationDownAction", WatchdogConfiguration.ApplicationDownAction),
                                                     Monitor = GetAttributeValue<bool>(node, "Monitor", true),
                                                     MaxRetryCount = GetAttributeValue<int>(node, "maxRetryCount", WatchdogConfiguration.MaxRetryCount),
                                                     ApplicationPoolName = GetAttributeValue<string>(node, "applicationPoolName", string.Empty),
                                                     Type = GetAttributeValue<string>(node, "type", WatchdogConfiguration.BASConfiguration.Type),
                                                     Endpoint = GetAttributeValue<string>(node, "endpoint", string.Empty),
                                                     PerformanceSampleCount = GetAttributeValue<int>(node, "performanceSampleCount", WatchdogConfiguration.PerformanceSampleCount)
                                                 }).ToList();
            }

            return k2Configuration;
        }

        private static T GetElementValue<T>(XElement node, string nodeName)
        {
            T nodeValue = default(T);
            Type type = typeof(T);
            XElement specificNode = node.Elements().Where(x => x.Name.LocalName.Equals(nodeName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (specificNode != null)
            {
                ParseNodeValue<T>(type, specificNode.Value, ref nodeValue);
            }

            return nodeValue;
        }
        private static T GetAttributeValue<T>(XElement node, string attributeName, T defaultValue)
        {
            T nodeValue = defaultValue;
            Type type = typeof(T);
            XAttribute specificNode = node.Attributes().Where(x => x.Name.LocalName.Equals(attributeName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (specificNode != null)
            {
                ParseNodeValue<T>(type, specificNode.Value, ref nodeValue);
            }

            return nodeValue;
        }

        private static void ParseNodeValue<T>(Type type, string value, ref T nodeValue)
        {
            if (type.IsGenericType && Nullable.GetUnderlyingType(type) != null)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    NullableConverter converter = new NullableConverter(type);
                    nodeValue = (T)Convert.ChangeType(value, converter.UnderlyingType);
                }
            }
            else
            {
                nodeValue = (T)Convert.ChangeType(value, type);
            }
        }
    }
}
