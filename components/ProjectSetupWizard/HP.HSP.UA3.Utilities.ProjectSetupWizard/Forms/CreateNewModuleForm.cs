using HP.HSP.UA3.Utilities.ProjectSetupWizard.Common;
using System;
using System.IO;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.ProjectSetupWizard.Forms
{
    public partial class CreateNewModuleForm : Form
    {
        public bool WasCreated = false;
        public string ModuleName = string.Empty;

        public CreateNewModuleForm()
        {
            InitializeComponent();
        }

        private void CreateNewModuleForm_Shown(object sender, EventArgs e)
        {
            ModuleNameTextbox.Focus();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if(IsModuleCreated())
                {
                    this.WasCreated = true;
                    this.ModuleName = ModuleNameTextbox.Text;
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
            string moduleName = ModuleNameTextbox.Text.Trim();
            if(string.IsNullOrEmpty(moduleName))
            {
                MessageBox.Show("Module Name is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ModuleNameTextbox.Focus();
                return false;
            }

            return true;
        }

        private bool IsModuleCreated()
        {
            if(IsFormValid())
            {
                if (IsModuleFolderRootAvailable())
                {
                    if(IsProjectDirectoryStructureCreated())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsModuleFolderRootAvailable()
        {
            string moduleName = ModuleNameTextbox.Text.Trim();
            string moduleRootPath = MainForm.UserConfig.SourcePath + "\\" + moduleName;
            if (Directory.Exists(moduleRootPath))
            {
                MessageBox.Show(string.Format("Module path '{0}' already exists. Select a new Module Name or delete existing module path.", moduleRootPath.ToLower()), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ModuleNameTextbox.Focus();
                return false;
            }
            return true;
        }

        private bool IsProjectDirectoryStructureCreated()
        {
            string moduleName = ModuleNameTextbox.Text.Trim();
            string moduleRootPath = MainForm.UserConfig.SourcePath + "\\" + moduleName;
            string templateRootPath = MainForm.UserConfig.SourcePath + "\\_ProjectTemplate";

            FileIOUtility.CreateBusinessModuleDirectory(templateRootPath, moduleRootPath, moduleName);

            return true;
        }
    }
}
