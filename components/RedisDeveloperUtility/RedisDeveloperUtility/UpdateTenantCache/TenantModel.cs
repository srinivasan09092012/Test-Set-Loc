using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDeveloperUtility.UpdateTenantCache
{
    public class TenantModel
    {

        public Guid TenantID { get; set; }


        public string TenantName { get; set; }


        public Guid? ParentTenantID { get; set; }


        public bool IsActive { get; set; }


        public DateTime LastModifiedDate { get; set; }


        public string Subdomain { get; set; }
    }
}
