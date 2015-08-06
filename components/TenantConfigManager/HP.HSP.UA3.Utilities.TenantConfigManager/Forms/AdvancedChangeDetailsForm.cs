using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    public partial class AdvancedChangeDetailsForm : Form
    {
        protected object Original { get; private set; }
        protected object Updated { get; private set; }

        public AdvancedChangeDetailsForm(object original, object updated)
        {
            InitializeComponent();
            this.Original = original;
            this.Updated = updated;

            TypeDescriptor.AddAttributes(this.Original, new Attribute[] { new ReadOnlyAttribute(true) });
            TypeDescriptor.AddAttributes(this.Updated, new Attribute[] { new ReadOnlyAttribute(true) });
        }

        private void AdvancedChangeDetailsForm_Load(object sender, EventArgs e)
        {
            this.originalObjectPropertyGrid.SelectedObject = this.Original;
            this.updatedObjectPropertyGrid.SelectedObject = this.Updated;

            this.originalObjectPropertyGrid.Refresh();
            this.updatedObjectPropertyGrid.Refresh();
        }
    }
}
