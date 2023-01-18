//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//-----------------------------------------------------------------------------------------

using System.Net;
using System.Linq;

namespace Watchdog.Domain
{
    public class ServiceHealthInformation
    {
        public ServiceHealthInformation()
        {
            Computer = Dns.GetHostName();
            RestartStatus = Constants.Status.None;
            IpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
        }

        public string Computer { get; set; }

        public string IpAddress { get; set; }

        public string Environment { get; set; }

        public string ApplicationTenantId { get; set; }

        public string ServiceName { get; set; }

        public string  ServiceType { get; set; }

        public string Status { get; set; }

        public string ApplicationPool { get; set; }

        public string Endpoint { get; set; }

        public double CPUUsagePercent { get; set; }

        public double MemoryUsagePercent { get; set; }

        public float processMemInKB { get; set; }

        public double processMemInGB { get; set; }

        public string ErrorMessage { get; set; }

        public string RestartStatus { get; set; }
    }
}
