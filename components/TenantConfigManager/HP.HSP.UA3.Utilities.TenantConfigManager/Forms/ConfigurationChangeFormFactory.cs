using HP.HSP.UA3.Utilities.TenantConfigManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    public class ConfigurationChangeFormFactory
    {
        public static Form Create(CreateItemChange change)
        {
            return new BasicChangeDetailsForm(change.ChangeType, change.Added);
        }

        public static Form Create(DeleteItemChange change)
        {
            return new BasicChangeDetailsForm(change.ChangeType, change.Deleted);
        }

        public static Form Create(UpdateItemChange change)
        {
            return new AdvancedChangeDetailsForm(change.Original, change.Updated);
        }

        public static Form Create(ConfigurationChange change)
        {
            Form result = null;
            if (change is CreateItemChange)
            {
                result = Create(change as CreateItemChange);
            }
            else if (change is DeleteItemChange)
            {
                result = Create(change as DeleteItemChange);
            }
            else
            {
                result = Create(change as UpdateItemChange);
            }

            return result;
        }
    }
}
