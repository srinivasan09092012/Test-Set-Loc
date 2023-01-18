using HP.HSP.UA3.Utilities.TenantConfigManager.Services;
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
    public partial class BasicChangeDetailsForm : Form
    {
        protected object SourceObject { get; private set; }

        public BasicChangeDetailsForm(ChangeType changeType, object source)
        {
            InitializeComponent();

            if (changeType == ChangeType.Create)
                this.Text = "Created Item Details";
            else if (changeType == ChangeType.Delete)
                this.Text = "Deleted Item Details";
            else
                this.Text = "Item Details";

            this.SourceObject = source;

            TypeDescriptor.AddAttributes(this.SourceObject, new Attribute[] { new ReadOnlyAttribute(true) });
        }

        private void BasicChangeDetailsForm_Load(object sender, EventArgs e)
        {
            this.objectPropertyGrid.SelectedObject = this.SourceObject;
            this.objectPropertyGrid.Refresh();
        }
    }
}
