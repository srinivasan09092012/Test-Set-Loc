using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Common
{
    public static class Utilities
    {
        public static string GenerateNewID()
        {
            return Guid.NewGuid().ToString("D").ToUpper();
        }
    }
}
