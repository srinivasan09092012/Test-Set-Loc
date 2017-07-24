using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAvailabilityMonitor
{
    public class Config
    {
        public string Address { get; set; }

        public string Message { get; set; }

        public string BaseAddress { get; set; }

        public string WsdlAddress
        {
            get
            {
                return this.BaseAddress + this.Address + @"?wsdl";
            }
        }

        public string SvcAddress
        {
            get
            {
                return this.BaseAddress + this.Address;
            }
        }
    }
}
