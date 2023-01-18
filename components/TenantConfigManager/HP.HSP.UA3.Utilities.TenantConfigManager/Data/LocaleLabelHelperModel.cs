using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Data
{
    [Serializable()]
    public class LocaleLabelHelperModel
    {
        public string LocaleLabelId { get; set; }

        public string LocaleId { get; set; }

        public string Text { get; set; }

        public string Tooltip { get; set; }
    }
}
