using System;
using System.Collections.Generic;

namespace Watchdog.Domain
{
    public class WatchdogConfig
    {
        public WatchdogConfig()
        {
            WindowsServiceConfiguration = new WindowsServices();
            BASConfiguration = new BASConfig();
            UXConfiguration = new UXConfig();
            K2Configuration = new K2Config();
        }

        public string Environment { get; set; }

        public int PollInterval { get; set; }

        public string ApplicationDownAction { get; set; }

        public int MaxRetryCount { get; set; }

        public int PerformanceSampleCount { get; set; }

        public string LogAnalyticsWorkspaceId { get; set; }

        public string LogAnalyticsURL { get; set; }        

        public string SharedKey { get; set; }

        public List<Tenant> TenantsConfig { get; set; }

        public WindowsServices WindowsServiceConfiguration { get; set; }

        public BASConfig BASConfiguration { get; set; }

        public UXConfig UXConfiguration { get; set; }

        public K2Config K2Configuration { get; set; }

        public InRuleConfig InRuleConfiguration { get; set; }
    }
}
