using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logAnalyticsSearch1
{
    public class HppTenants
    {
        public String environmentName = "";
        public string sharedResourceGroupName = "";
        public string sharedStorageconnectionstring = "";
        public string sharedBlobContainerName = "";
        public List<HppTenant> hppTenants { get; set; }
    }
    public class HppTenant
    {
        public string hppTenantID = "";
        public string hppTenantName = "";
        public string resourceGroupName = "";
        public string storageconnectionstring = "";
        public string blobContainerName = "";
    }
}
