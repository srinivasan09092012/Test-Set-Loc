using HP.HSP.UA3.Utilities.BusinessModuleProjectSetupWizard.Common;
using System;
using System.IO;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.BusinessModuleProjectSetupWizard.Forms
{
    public partial class CreateNewBatchForm : Form
    {
        public string ModuleName = string.Empty;
        public string ModulePath = string.Empty;
        public string TemplatePath = string.Empty;
        public bool WasCreated = false;

        public CreateNewBatchForm()
        {
            InitializeComponent();
        }

        private void CreateNewBatchForm_Shown(object sender, EventArgs e)
        {
            ServiceNameTextbox.Focus();
        }

    
        private void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (IsServiceCreated())
                {
                    this.WasCreated = true;
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private bool IsFormValid()
        {
            string serviceName = ServiceNameTextbox.Text.Trim();
            if (string.IsNullOrEmpty(serviceName))
            {
                MessageBox.Show("Service Name is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ServiceNameTextbox.Focus();
                return false;
            }

            return true;
        }

        private bool IsServiceCreated()
        {
            if (IsFormValid())
            {
                if (IsServiceFolderRootAvailable())
                {
                    if (IsProjectDirectoryStructureCreated())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsServiceFolderRootAvailable()
        {
            string serviceName = ServiceNameTextbox.Text.Trim();
            string serviceRootPath = this.ModulePath + "\\" + serviceName;
            if (Directory.Exists(serviceRootPath))
            {
                MessageBox.Show(string.Format("Service path '{0}' already exists. Select a new Service Name or delete existing service path.", serviceRootPath.ToLower()), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ServiceNameTextbox.Focus();
                return false;
            }
            return true;
        }

        private bool IsProjectDirectoryStructureCreated()
        {
            string serviceName = ServiceNameTextbox.Text.Trim();
            string serviceRootPath = this.ModulePath + "\\" + serviceName;
            string templateRootPath = this.TemplatePath + "\\[ServiceName]";

            FileIOUtility.CreateServiceDirectory(templateRootPath, serviceRootPath, this.ModuleName, serviceName);

            return true;
        }
    }
}
