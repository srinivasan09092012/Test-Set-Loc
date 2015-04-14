using HP.HSP.UA3.Core.UX.Common.Utilities;
using HP.HSP.UA3.Utilities.ProjectSetupWizard.Common;
using HP.HSP.UA3.Utilities.ProjectSetupWizard.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.ProjectSetupWizard.Forms
{
    public partial class MainForm : Form
    {
        public static UserConfigModel UserConfig = null;

        private Dictionary<string, string> _modules = new Dictionary<string, string>();
        private Dictionary<string, string> _basServices = new Dictionary<string, string>();
        private Dictionary<string, string> _batchServices = new Dictionary<string, string>();

        #region Main Form Events
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void Main_Shown(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (!IsUserConfigLoaded())
                {
                    ShowUserConfig();
                }
                InitializeForm();
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

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
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

        private void CreateNewButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                ShowCreateNewModule();
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

        private void CreateBASButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                ShowCreateNewBAS();
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

        private void CreateBatchButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                ShowCreateNewBatch();
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

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string module = BusinessModuleDropdown.SelectedItem.ToString();
                string msg = string.Format("You are requesting to delete the business module '{0}'.\nThis action will only delete it from disk and not TFS.\n\nDo you wish to delete this module?", module);
                DialogResult result = MessageBox.Show(msg,this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (IsBusinessModuleDeleted(module))
                    {
                        MessageBox.Show("Business module was successfully deleted.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        InitializeForm();
                    }
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

        private void DeleteBASButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string service = BASDropdown.SelectedItem.ToString();
                string msg = string.Format("You are requesting to delete the BAS service '{0}'.\nThis action will only delete it from disk and not TFS.\n\nDo you wish to delete this service?", service);
                DialogResult result = MessageBox.Show(msg, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (IsBASServiceDeleted(service))
                    {
                        MessageBox.Show("Service was successfully deleted.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBASServices();
                    }
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

        private void DeleteBatchButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string service = BatchDropdown.SelectedItem.ToString();
                string msg = string.Format("You are requesting to delete the batch service '{0}'.\nThis action will only delete it from disk and not TFS.\n\nDo you wish to delete this service?", service);
                DialogResult result = MessageBox.Show(msg, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (IsBatchServiceDeleted(service))
                    {
                        MessageBox.Show("Service was successfully deleted.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBatchServices();
                    }
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

        private void BusinessModuleDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleDetails(false);
            string module = BusinessModuleDropdown.SelectedItem.ToString();
            DeleteButton.Enabled = !string.IsNullOrEmpty(module);
            if(DeleteButton.Enabled)
            {
                LoadBusinessModule(module);
            }
        }

        private void BASDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string service = BASDropdown.SelectedItem.ToString();
            DeleteBASButton.Enabled = !string.IsNullOrEmpty(service);
        }

        private void BatchDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string service = BatchDropdown.SelectedItem.ToString();
            DeleteBatchButton.Enabled = !string.IsNullOrEmpty(service);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                this.Close();
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

        private void userConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                ShowUserConfig();
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

        private void aboutTenentConfigurationManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                ShowAbout();
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
        #endregion

        #region Private Methods
        private void InitializeForm()
        {
            BusinessModuleDropdown.Enabled = false;
            ToggleDetails(false);

            if (Directory.Exists(UserConfig.SourcePath))
            {
                LoadBusinessModules();
            }
        }

        private bool IsBASServiceDeleted(string service)
        {
            string path = _basServices[service];
            FileIOUtility.DeleteDirectory(path);
            Directory.Delete(path);

            return true;
        }

        private bool IsBatchServiceDeleted(string service)
        {
            string path = _batchServices[service];
            FileIOUtility.DeleteDirectory(path);
            Directory.Delete(path);

            return true;
        }

        private bool IsBusinessModuleDeleted(string module)
        {
            string path = _modules[module];
            FileIOUtility.DeleteDirectory(path);
            Directory.Delete(path);

            return true;
        }

        private bool IsUserConfigLoaded()
        {
            if (File.Exists(Constants.Config.User.FileName))
            {
                MainForm.UserConfig = Serializer.XmlDeserialize<UserConfigModel>(File.ReadAllText(Constants.Config.User.FileName), Constants.Config.User.Namespace);
                return true;
            }
            else
            {
                MainForm.UserConfig = new UserConfigModel();
            }
            return false;
        }

        private bool IsValidBusinessModuleDir(DirectoryInfo di)
        {
            bool isValid = false;

            if (!di.Name.StartsWith("_"))
            {
                foreach (DirectoryInfo cdi in di.GetDirectories())
                {
                    if (cdi.Name == "Dev")
                    {
                        foreach (DirectoryInfo ccdi in cdi.GetDirectories())
                        {
                            if (ccdi.Name == "BAS")
                            {
                                isValid = true;
                                break;
                            }
                        }
                    }
                    if(isValid)
                    {
                        break;
                    }
                }
            }

            return isValid;
        }

        private bool IsValidServiceDir(DirectoryInfo di)
        {
            bool isValid = false;

            foreach (DirectoryInfo cdi in di.GetDirectories())
            {
                if (cdi.Name == "Data")
                {
                    isValid = true;
                    break;
                }
                if (isValid)
                {
                    break;
                }
            }

            return isValid;
        }

        private void LoadBASServices()
        {
            BASDropdown.Items.Clear();
            DeleteBASButton.Enabled = false;
            _basServices.Clear();

            string path = _modules[BusinessModuleDropdown.SelectedItem.ToString()] + "\\Dev\\BAS";
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (DirectoryInfo cdi in di.GetDirectories())
            {
                if (IsValidServiceDir(cdi))
                {
                    _basServices.Add(cdi.Name, cdi.FullName);
                    BASDropdown.Items.Add(cdi.Name);
                }
            }

            BASDropdown.Enabled = BASDropdown.Items.Count > 0;
        }

        private void LoadBatchServices()
        {
            BatchDropdown.Items.Clear();
            DeleteBatchButton.Enabled = false;
            _batchServices.Clear();

            string path = _modules[BusinessModuleDropdown.SelectedItem.ToString()] + "\\Dev\\Batch";
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (DirectoryInfo cdi in di.GetDirectories())
            {
                if (IsValidServiceDir(cdi))
                {
                    _batchServices.Add(cdi.Name, cdi.FullName);
                    BatchDropdown.Items.Add(cdi.Name);
                }
            }

            BatchDropdown.Enabled = BatchDropdown.Items.Count > 0;
        }

        private void LoadBusinessModule(string module)
        {
            LoadBASServices();
            LoadBatchServices();
            ToggleDetails(true);
        }

        private void LoadBusinessModules()
        {
            BusinessModuleDropdown.Items.Clear();
            DeleteButton.Enabled = false;
            _modules.Clear();

            DirectoryInfo di = new DirectoryInfo(UserConfig.SourcePath);
            foreach (DirectoryInfo cdi in di.GetDirectories())
            {
                if (IsValidBusinessModuleDir(cdi))
                {
                    _modules.Add(cdi.Name, cdi.FullName);
                    BusinessModuleDropdown.Items.Add(cdi.Name);
                }
            }

            BusinessModuleDropdown.Enabled = BusinessModuleDropdown.Items.Count > 0;
        }

        private void ShowCreateNewBAS()
        {
            CreateNewBASForm form = new CreateNewBASForm();
            form.ModuleName = BusinessModuleDropdown.SelectedItem.ToString();
            form.ModulePath = _modules[form.ModuleName] + "\\Dev\\BAS";
            form.TemplatePath = UserConfig.SourcePath + "\\_ProjectTemplate\\Dev\\BAS";
            form.ShowDialog();
            if (form.WasCreated)
            {
                MessageBox.Show("BAS service project structure was successfully created.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBASServices();
            }
            form.Dispose();
        }

        private void ShowCreateNewBatch()
        {
            CreateNewBatchForm form = new CreateNewBatchForm();
            form.ModuleName = BusinessModuleDropdown.SelectedItem.ToString();
            form.ModulePath = _modules[form.ModuleName] + "\\Dev\\Batch";
            form.TemplatePath = UserConfig.SourcePath + "\\_ProjectTemplate\\Dev\\Batch";
            form.ShowDialog();
            if (form.WasCreated)
            {
                MessageBox.Show("Batch service project structure was successfully created.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBatchServices();
            }
            form.Dispose();
        }

        private void ShowCreateNewModule()
        {
            CreateNewModuleForm form = new CreateNewModuleForm();
            form.ShowDialog();
            if(form.WasCreated)
            {
                MessageBox.Show("Business module project structure was successfully created.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                InitializeForm();
                BusinessModuleDropdown.SelectedItem = form.ModuleName;
            }
            form.Dispose();
        }

        private void ShowUserConfig()
        {
            UserConfigForm form = new UserConfigForm();
            form.ShowDialog();
        }

        private void ShowAbout()
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        private void ToggleDetails(bool enabled)
        {
            if (!enabled)
            {
                BASDropdown.Items.Clear();
                BatchDropdown.Items.Clear();
            }
            DetailsGroupBox.Enabled = enabled;
        }
        #endregion
    }
}
