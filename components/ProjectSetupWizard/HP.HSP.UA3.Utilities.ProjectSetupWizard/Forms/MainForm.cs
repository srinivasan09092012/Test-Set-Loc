//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved. 
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
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
        private Dictionary<string, string> _apis = new Dictionary<string, string>();

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

        private void CreateApiButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                ShowCreateNewApi();
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
                DialogResult result = MessageBox.Show(msg, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                string msg = string.Format("You are requesting to delete the BAS service '{0}'.\nThis action will only delete it from disk and not TFS.\n\nDo you wish to continue?", service);
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
                string msg = string.Format("You are requesting to delete the batch service '{0}'.\nThis action will only delete it from disk and not TFS.\n\nDo you wish to continue?", service);
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

        private void DeleteApiButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string api = ApiDropdown.SelectedItem.ToString();
                string msg = string.Format("You are requesting to delete the API '{0}'.\nThis action will only delete it from disk and not TFS.\n\nDo you wish to continue?", api);
                DialogResult result = MessageBox.Show(msg, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (IsApiDeleted(api))
                    {
                        MessageBox.Show("API was successfully deleted.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadApis();
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
            if (DeleteButton.Enabled)
            {
                LoadBusinessModule(module);
            }
        }

        private void TargetBranchDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enabled = (TargetBranchDropdown.SelectedIndex != -1);

            BASGroupBox.Enabled = enabled;
            BatchGroupBox.Enabled = enabled;
            CreateBASButton.Enabled = enabled;
            ////CreateBatchButton.Enabled = enabled;
            CreateApiButton.Enabled = enabled;
            LoadBASServices();
            LoadBatchServices();
            LoadApis();
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

        private void ApiDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string service = ApiDropdown.SelectedItem.ToString();
            DeleteApiButton.Enabled = !string.IsNullOrEmpty(service);
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

        #endregion Main Form Events

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

        private bool IsApiDeleted(string api)
        {
            string path = _apis[api];
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

        private bool IsValidApiDir(DirectoryInfo di)
        {
            bool isValid = false;

            foreach (DirectoryInfo cdi in di.GetDirectories())
            {
                isValid = true;
                break;
            }

            return isValid;
        }


        private bool IsValidBusinessModuleDir(DirectoryInfo di)
        {
            bool isValid = false;

            if (!di.Name.StartsWith("_"))
            {
                foreach (DirectoryInfo cdi in di.GetDirectories())
                {
                    if (cdi.Name == "Main" || cdi.Name == "Dev")
                    {
                        foreach (DirectoryInfo ccdi in cdi.GetDirectories())
                        {
                            if (ccdi.Name == "BAS" || ccdi.Name == "Batch" || ccdi.Name == "UX")
                            {
                                isValid = true;
                                break;
                            }
                        }
                    }
                    if (isValid)
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
                if (cdi.Name.Contains(".BAS.") || cdi.Name.Contains(".Batch."))
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

        private void LoadApis()
        {
            ApiDropdown.Items.Clear();
            DeleteApiButton.Enabled = false;
            _apis.Clear();

            string path = _modules[BusinessModuleDropdown.SelectedItem.ToString()] + string.Format("\\{0}\\API", TargetBranchDropdown.SelectedItem.ToString());
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists == false)
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                foreach (DirectoryInfo cdi in di.GetDirectories())
                {
                    if (IsValidApiDir(cdi))
                    {
                        _apis.Add(cdi.Name, cdi.FullName);
                        ApiDropdown.Items.Add(cdi.Name);
                    }
                }
            }

            ApiDropdown.Enabled = ApiDropdown.Items.Count > 0;
        }

        private void LoadBASServices()
        {
            BASDropdown.Items.Clear();
            DeleteBASButton.Enabled = false;
            _basServices.Clear();

            string path = _modules[BusinessModuleDropdown.SelectedItem.ToString()] + string.Format("\\{0}\\BAS", TargetBranchDropdown.SelectedItem.ToString());
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists == false)
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                foreach (DirectoryInfo cdi in di.GetDirectories())
                {
                    if (IsValidServiceDir(cdi))
                    {
                        _basServices.Add(cdi.Name, cdi.FullName);
                        BASDropdown.Items.Add(cdi.Name);
                    }
                }
            }

            BASDropdown.Enabled = BASDropdown.Items.Count > 0;
        }

        private void LoadBatchServices()
        {
            BatchDropdown.Items.Clear();
            DeleteBatchButton.Enabled = false;
            _batchServices.Clear();

            string path = _modules[BusinessModuleDropdown.SelectedItem.ToString()] + string.Format("\\{0}\\Batch", TargetBranchDropdown.SelectedItem.ToString());
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists == false)
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                foreach (DirectoryInfo cdi in di.GetDirectories())
                {
                    if (IsValidServiceDir(cdi))
                    {
                        _batchServices.Add(cdi.Name, cdi.FullName);
                        BatchDropdown.Items.Add(cdi.Name);
                    }
                }
            }

            BatchDropdown.Enabled = BatchDropdown.Items.Count > 0;
        }

        private void LoadBusinessModule(string module)
        {
            LoadTargetBranches(module);
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

        private void LoadTargetBranches(string module)
        {
            TargetBranchDropdown.Items.Clear();

            string path = _modules[module];
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists)
            {
                foreach (DirectoryInfo cdi in di.GetDirectories())
                {
                    TargetBranchDropdown.Items.Add(cdi.Name);
                }
                TargetBranchDropdown.SelectedItem = 0;
            }
            else
            {
                MessageBox.Show("No branches found for currently selected business module.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ShowCreateNewBAS()
        {
            CreateNewBASForm form = new CreateNewBASForm();
            form.ModuleName = BusinessModuleDropdown.SelectedItem.ToString();
            form.ModulePath = _modules[form.ModuleName] + string.Format("\\{0}\\BAS", TargetBranchDropdown.SelectedItem.ToString());
            form.TemplatePath = UserConfig.SourcePath + "\\_ProjectTemplate\\Main\\BAS";
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
            form.ModulePath = _modules[form.ModuleName] + string.Format("\\{0}\\Batch", TargetBranchDropdown.SelectedItem.ToString());
            form.TemplatePath = UserConfig.SourcePath + "\\_ProjectTemplate\\Main\\Batch";
            form.ShowDialog();
            if (form.WasCreated)
            {
                MessageBox.Show("Batch service project structure was successfully created.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBatchServices();
            }
            form.Dispose();
        }

        private void ShowCreateNewApi()
        {
            CreateNewApiForm form = new CreateNewApiForm();
            form.ModuleName = BusinessModuleDropdown.SelectedItem.ToString();
            form.ModulePath = _modules[form.ModuleName] + string.Format("\\{0}\\API", TargetBranchDropdown.SelectedItem.ToString());
            form.TemplatePath = UserConfig.SourcePath + "\\_ProjectTemplate\\Main\\API";
            form.ShowDialog();
            if (form.WasCreated)
            {
                MessageBox.Show("API project structure was successfully created.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadApis();
            }
            form.Dispose();
        }

        private void ShowCreateNewModule()
        {
            CreateNewModuleForm form = new CreateNewModuleForm();
            form.ShowDialog();
            if (form.WasCreated)
            {
                string msg = string.Format("Business module project structure was successfully created." + Environment.NewLine + Environment.NewLine +
                                           "You must add the new module to TENANT in all databases." + Environment.NewLine +
                                           "Refer to the Project Setup Wizard document in Sharepoint.");
                MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                TargetBranchDropdown.SelectedValue = string.Empty;
                BASDropdown.Items.Clear();
                BatchDropdown.Items.Clear();
                ApiDropdown.Items.Clear();
                BASDropdown.Enabled = false;
                BatchDropdown.Enabled = false;
                ApiDropdown.Enabled = false;
            }
            DetailsGroupBox.Enabled = enabled;
        }

        #endregion Private Methods
    }
}