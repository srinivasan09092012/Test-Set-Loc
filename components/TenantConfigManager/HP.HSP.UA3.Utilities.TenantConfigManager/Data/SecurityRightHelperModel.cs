using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Data
{
    [Serializable()]
    public class SecurityRightHelperModel
    {
        public string RoleId { get; set; }

        public string FunctionId { get; set; }

        public string RoleName { get; set; }

        public string FunctionName { get; set; }

        public bool isIncluded { get; set; }
    }
}
