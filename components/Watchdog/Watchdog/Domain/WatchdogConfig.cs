//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of Gainwell Technologies, Copyright (c) 2020. All rights reserved.
//-----------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Watchdog.Domain
{
    public class WatchdogConfig
    {
        public WatchdogConfig()
        {
            WindowsServiceConfiguration = new WindowsServices();
            BASConfiguration = new BASConfig();
            AddressDoctorConfiguration = new AddressDoctorconfig();
            K2ServiceConfiguration = new K2ServiceConfig();
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

        public K2ServiceConfig K2ServiceConfiguration { get; set; }

        public InRuleConfig InRuleConfiguration { get; set; }

        public UXMonitoring UXMonitoring { get; set; }

        public AddressDoctorconfig AddressDoctorConfiguration { get; set; }
    }
}