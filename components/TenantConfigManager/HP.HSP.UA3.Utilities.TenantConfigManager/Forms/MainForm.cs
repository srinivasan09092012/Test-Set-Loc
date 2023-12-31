﻿using HP.HSP.UA3.Core.UX.Common;
using HP.HSP.UA3.Core.UX.Common.Utilities;
using HP.HSP.UA3.Core.UX.Data.Configuration;
using HP.HSP.UA3.Core.UX.Data.Messaging;
using HP.HSP.UA3.Core.UX.Data.Navigation;
using HP.HSP.UA3.Core.UX.Data.Security;
using HP.HSP.UA3.Core.UX.Providers;
using HP.HSP.UA3.Utilities.TenantConfigManager.Common;
using HP.HSP.UA3.Utilities.TenantConfigManager.Data;
using HP.HSP.UA3.Utilities.TenantConfigManager.Services;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    public partial class MainForm : Form
    {
        public static UserConfigModel UserConfig = null;

        private string _coreTenantConfigPath = string.Empty;
        private TenantConfigurationModel _coreTenantConfig = null;
        private string _currentModelDataAssemblyFile = string.Empty;
        private string _currentModelDataBinPath = string.Empty;
        private string _currentModelNamespace = string.Empty;
        private List<DisplaySizeConfigurationModel> _displaySizes = null;
        private bool _isDataDrity = false;
        private Dictionary<string, List<string>> _modules = new Dictionary<string, List<string>>();
        private Dictionary<string, string> _moduleConfigs = new Dictionary<string, string>();
        private string _appTierName = string.Empty;
        private string _businessModuleName = string.Empty;
        private string _tenantName = string.Empty;
        private Dictionary<string, TenantConfigurationModel> _tenantConfigs = new Dictionary<string, TenantConfigurationModel>();
        private Dictionary<string, TenantConfigurationModel> _originalTenantConfigs = new Dictionary<string, TenantConfigurationModel>();
        private TenantConfigurationModel _tenantConfig = null;

        #region Main Form Events
        public MainForm()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if(!IsUserConfigLoaded())
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
                if (_isDataDrity)
                {
                    DialogResult result =
                        MessageBox.Show("Would you like to save your changes before exiting?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if(!IsDataSaved())
                        {
                            e.Cancel = true;
                        }
                    }
                    else if (result == System.Windows.Forms.DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void AppTierDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                TenantDropdown.SelectedIndex = -1;
                TenantDropdown.Enabled = false;

                ClearEditor();

                if (!string.IsNullOrEmpty(AppTierDropdown.SelectedItem.ToString()))
                {
                    _appTierName = AppTierDropdown.SelectedItem.ToString();
                    _currentModelDataBinPath = UserConfig.SourcePath
                        + "\\" + _businessModuleName
                        + "\\Dev\\" + _appTierName
                        + "\\" + string.Format("HP.HSP.UA3.{0}.{1}.Data", _businessModuleName, _appTierName)
                        + "\\bin\\{0}\\";
                    _currentModelDataAssemblyFile = string.Format("HP.HSP.UA3.{0}.{1}.Data.dll", _businessModuleName, _appTierName);
                    _currentModelNamespace = string.Format("HP.HSP.UA3.{0}.{1}.Data.", _businessModuleName, _appTierName);

                    if (AreTenantConfigsLoaded(_businessModuleName))
                    {
                        foreach (string name in _tenantConfigs.Keys)
                        {
                            TenantDropdown.Items.Add(name);
                        }
                        LoadDisplaySizes();
                    }

                    if (_tenantConfigs.Keys.Count == 1)
                    {
                        TenantLabel.Visible = false;
                        TenantDropdown.Visible = false;
                        TenantDropdown.SelectedIndex = 0;
                    }
                    else
                    {
                        TenantLabel.Visible = true;
                        TenantDropdown.Visible = true;
                        TenantDropdown.Focus();
                    }
                }
                else
                {
                    _appTierName = string.Empty;
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
            try
            {
                Cursor = Cursors.WaitCursor;

                AppTierDropdown.Items.Clear();
                AppTierDropdown.Enabled = false;
                TenantDropdown.Items.Clear();
                TenantDropdown.Enabled = false;
                TenantConfigTabControl.Enabled = false;

                ClearEditor();

                if (!string.IsNullOrEmpty(BusinessModuleDropdown.SelectedItem.ToString()))
                {
                    _businessModuleName = BusinessModuleDropdown.SelectedItem.ToString();

                    List<string> appTiers = _modules[_businessModuleName];
                    foreach (string appTier in appTiers)
                    {
                        AppTierDropdown.Items.Add(appTier);
                    }
                    AppTierDropdown.Enabled = true;
                    AppTierDropdown.Focus();
                }
                else
                {
                    _businessModuleName = string.Empty;
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

        private void ResetButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                object module = BusinessModuleDropdown.SelectedItem;
                object appTier = AppTierDropdown.SelectedItem;
                object tenant = TenantDropdown.SelectedItem;

                DialogResult result =
                    MessageBox.Show("Resetting the configuration back to its last saved state will lose all unsaved pending changes.\n\nDo you wish to perform the reset?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    InitializeForm();
                    BusinessModuleDropdown.SelectedItem = module;
                    AppTierDropdown.SelectedItem = appTier;
                    TenantDropdown.SelectedItem = tenant;
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

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                IsDataSaved();
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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                IsDataSaved();
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

        private void ShowIdsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ToggleShowIds(ShowIdsCheckBox.Checked);
        }

        private void TenantDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                TenantConfigTabControl.Enabled = false;
                _tenantName = TenantDropdown.SelectedItem.ToString();
                _tenantConfig = _tenantConfigs[_tenantName];
                LoadTenantConfiguration(_tenantConfig);
                TenantConfigTabControl.Enabled = true;
                ToggleShowIds(ShowIdsCheckBox.Checked);
                ToggleDirtyData(false);

                FileInfo fi = new FileInfo(_moduleConfigs[_businessModuleName]);
                if (fi.IsReadOnly)
                {
                    MessageBox.Show("The selected config file is currently set to read only.\n\nEnsure that you have the latest checked out tenant configuration file from TFS before attempting to save any changes.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void configurationItemModelBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (_tenantConfigs != null && _tenantConfigs.Count > 0)
            {
                ToggleDirtyData(true);
            }
        }
      
        #region Display Sizes Tab Events
        private void DisplaySizesGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == DisplaySizesGridView.NewRowIndex)
            {
                if (DisplaySizesGridView.CurrentCell.EditType == typeof(DataGridViewTextBoxEditingControl))
                {
                    DisplaySizesGridView.BeginEdit(false);
                    TextBox textBox = (TextBox)DisplaySizesGridView.EditingControl;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void DisplaySizesGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Row.Cells[0].Value = Common.Utilities.GenerateNewID();
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

        private void DisplaySizesGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Cancel = !ConfirmDeleteRow(e.Row.Cells[1].Value.ToString());
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

        private void displaySizeBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (_tenantConfigs != null && _tenantConfigs.Count > 0)
            {
                ToggleDirtyData(true);
            }
        }
        #endregion

        #region Model Definition Tab Events
        private void AutoGenModelButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                ShowAutoGenModelDefinition();
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

        private void modelDefsBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (_tenantConfigs != null && _tenantConfigs.Count > 0)
            {
                ToggleDirtyData(true);
            }
        }

        private void ModelDefinitionsGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == ModelDefinitionsGridView.NewRowIndex)
            {
                if (ModelDefinitionsGridView.CurrentCell.EditType == typeof(DataGridViewTextBoxEditingControl))
                {
                    ModelDefinitionsGridView.BeginEdit(false);
                    TextBox textBox = (TextBox)ModelDefinitionsGridView.EditingControl;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void ModelDefinitionsGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Row.Cells[0].Value = Common.Utilities.GenerateNewID();
                e.Row.Cells[1].Value = "HP.HSP.UA3." + _businessModuleName + "." + _appTierName + ".";
                e.Row.Cells[2].Value = "*";
                e.Row.Cells[3].Value = "*";
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

        private void ModelDefinitionsGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Cancel = !ConfirmDeleteRow(e.Row.Cells[2].Value.ToString());
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

        private void ModelDefinitionsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (e.ColumnIndex == 4)
                {
                    string id = Convert.ToString(ModelDefinitionsGridView.Rows[e.RowIndex].Cells[0].Value);
                    ShowModelProperties(id);
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
        #endregion

        #region Private Methods
        private bool AreTenantConfigsLoaded(string module)
        {
            _tenantConfigs.Clear();
            _originalTenantConfigs.Clear();

            TenantConfigurationModel tenantConfig = 
                Serializer.XmlDeserialize<TenantConfigurationModel>(File.ReadAllText(_moduleConfigs[module]), CoreConstants.Xml.NamespacePrefixCore);

            _tenantConfigs.Add(tenantConfig.TenantId, tenantConfig);
            _originalTenantConfigs.Add(tenantConfig.TenantId, tenantConfig.Clone() as TenantConfigurationModel);

            //Load core tenant config
            _coreTenantConfigPath = _moduleConfigs[module].Replace(_businessModuleName, "Core");
            _coreTenantConfig =
                Serializer.XmlDeserialize<TenantConfigurationModel>(File.ReadAllText(_coreTenantConfigPath), CoreConstants.Xml.NamespacePrefixCore);

            return true;
        }

        private void ClearEditor()
        {
            TenantConfigurationModel tenantConfig = new TenantConfigurationModel();
            tenantConfig.Modules = new List<ModuleConfigurationModel>();
            tenantConfig.Modules.Add(
                new ModuleConfigurationModel()
                {
                    ApplicationSettings = new List<ConfigurationItemModel>(),
                    DisplayConfiguration = new DisplayConfigurationModel(),
                    LocalizationConfiguration = new LocalizationConfigurationModel(),
                    Menus = new List<MenuModel>(),
                    ModelDefinitionConfiguration = new List<ModelDefinitionModel>(),
                    SecurityRoles = new List<SecurityRoleModel>(),
                    Services = new List<ServiceItemModel>()
                }
            );
            tenantConfig.Modules[0].LocalizationConfiguration.Locales = new List<LocaleConfigurationModel>();

            LoadTenantConfiguration(tenantConfig);
            TenantConfigTabControl.SelectTab(0);
        }

        private bool ConfirmDeleteRow(string name)
        {
            DialogResult result =
                MessageBox.Show(string.Format("Do you wish to delete the row '{0}'?", name), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return (result == System.Windows.Forms.DialogResult.Yes);
        }

        private void FormatGuids(TenantConfigurationModel tenantConfig)
        {
            if(tenantConfig != null)
            {
                tenantConfig.TenantId = tenantConfig.TenantId.ToLower();

                if(tenantConfig.Modules != null && tenantConfig.Modules.Count > 0)
                {
                    foreach(ModuleConfigurationModel module in tenantConfig.Modules)
                    {
                        module.ModuleId = module.ModuleId.ToLower();

                        if(module.DisplayConfiguration != null && module.DisplayConfiguration.DisplaySizes.Count > 0)
                        {
                            foreach(DisplaySizeConfigurationModel displaySize in module.DisplayConfiguration.DisplaySizes)
                            {
                                displaySize.Id = displaySize.Id.ToLower();
                            }
                        }

                        if(module.LocalizationConfiguration != null && module.LocalizationConfiguration.Locales.Count > 0)
                        {
                            foreach (LocaleConfigurationModel locale in module.LocalizationConfiguration.Locales)
                            {
                                locale.Id = locale.Id.ToLower();

                                if(locale.LocaleDataLists != null && locale.LocaleDataLists.Count > 0)
                                {
                                    foreach (LocaleConfigurationDataListModel datalist in locale.LocaleDataLists)
                                    {
                                        datalist.Id = datalist.Id.ToLower();

                                        if(datalist.LocaleDataListItems != null && datalist.LocaleDataListItems.Count > 0)
                                        {
                                            foreach (LocaleConfigurationDataListItemModel item in datalist.LocaleDataListItems)
                                            {
                                                item.Id = item.Id.ToLower();
                                            }
                                        }
                                    }
                                }

                                if (locale.LocaleEmailTemplates != null && locale.LocaleEmailTemplates.Count > 0)
                                {
                                    foreach (LocaleConfigurationEmailTemplateModel template in locale.LocaleEmailTemplates)
                                    {
                                        template.Id = template.Id.ToLower();

                                        if(template.Addresses != null && template.Addresses.Count > 0)
                                        {
                                            foreach (LocaleConfigurationEmailTemplateAddressModel address in template.Addresses)
                                            {
                                                address.Id = address.Id.ToLower();
                                            }
                                        }
                                    }
                                }

                                if (locale.LocaleLabels != null && locale.LocaleLabels.Count > 0)
                                {
                                    foreach (LocaleConfigurationLabelModel label in locale.LocaleLabels)
                                    {
                                        label.Id = label.Id.ToLower();
                                    }
                                }

                                if (locale.LocaleMessages != null && locale.LocaleMessages.Count > 0)
                                {
                                    foreach (LocaleConfigurationMessageModel msg in locale.LocaleMessages)
                                    {
                                        msg.Id = msg.Id.ToLower();
                                    }
                                }
                            }
                        }

                        if (module.Menus != null && module.Menus.Count > 0)
                        {
                            foreach (MenuModel menu in module.Menus)
                            {
                                menu.Id = menu.Id.ToLower();
                                menu.SecurityRightContentId = menu.SecurityRightContentId.ToLower();

                                if(menu.Items != null && menu.Items.Count > 0)
                                {
                                    foreach (MenuItemModel item in menu.Items)
                                    {
                                        item.Id = item.Id.ToLower();

                                        if (item.SecurityRightContentId != null)
                                        {
                                            item.SecurityRightContentId = item.SecurityRightContentId.ToLower();
                                        }
                                    }
                                }
                            }
                        }

                        if (module.ModelDefinitionConfiguration != null && module.ModelDefinitionConfiguration.Count > 0)
                        {
                            foreach (ModelDefinitionModel model in module.ModelDefinitionConfiguration)
                            {
                                model.Id = model.Id.ToLower();

                                if (model.ModelProperties != null && model.ModelProperties.Count > 0)
                                {
                                    foreach (ModelPropertyModel prop in model.ModelProperties)
                                    {
                                        prop.Id = prop.Id.ToLower();

                                        if (prop.EditSecurityRightContentId != null)
                                        {
                                            prop.EditSecurityRightContentId = prop.EditSecurityRightContentId.ToLower();
                                        }

                                        if (prop.ViewSecurityRightContentId != null)
                                        {
                                            prop.ViewSecurityRightContentId = prop.ViewSecurityRightContentId.ToLower();
                                        }
                                    }
                                }
                            }
                        }

                        if (module.Services != null && module.Services.Count > 0)
                        {
                            foreach (ServiceItemModel service in module.Services)
                            {
                                service.Id = service.Id.ToLower();

                                if (service.SecurityRightContentId != null)
                                {
                                    service.SecurityRightContentId = service.SecurityRightContentId.ToLower();
                                }
                            }
                        }

                        if (module.SecurityRoles != null && module.SecurityRoles.Count > 0)
                        {
                            foreach (SecurityRoleModel role in module.SecurityRoles)
                            {
                                role.Id = role.Id.ToLower();

                                if (role.AdminSecurityRightId != null)
                                {
                                    role.AdminSecurityRightId = role.AdminSecurityRightId.ToLower();
                                }

                                if (role.Functions != null && role.Functions.Count > 0)
                                {
                                    foreach (SecurityFunctionModel function in role.Functions)
                                    {
                                        function.Id = function.Id.ToLower();

                                        if (function.Rights != null && function.Rights.Count > 0)
                                        {
                                            foreach (SecurityRightModel right in function.Rights)
                                            {
                                                right.Id = right.Id.ToLower();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void InitializeDataGrids()
        {
        }

        private void InitializeForm()
        {
            //Set styles
            BusinessModuleDropdown.Items.Clear();
            BusinessModuleDropdown.Enabled = false;
            AppTierDropdown.Items.Clear();
            AppTierDropdown.Enabled = false;
            TenantDropdown.Items.Clear();
            TenantDropdown.Enabled = false;
            InitializeDataGrids();
            ToggleDirtyData(false);

            if(Directory.Exists(UserConfig.SourcePath))
            {
                LoadBusinessModules();
            }
            if (_modules != null && _modules.Count > 0)
            {
                BusinessModuleDropdown.Enabled = true;
            }
            else
            {
                throw new ApplicationException("No tenant configurations found.");
            }
        }

        private bool IsDataSaved()
        {
            bool isSaved = false;

            if (IsValidTenantConfigData())
            {
                //Format Guids to conform to lower case standard (ensure manually entered IDs follow the format)
                FormatGuids(_tenantConfig);

                //Sort data
                SortConfigurationData(_tenantConfig);

                //Configure file properties
                string existingFilePath = _moduleConfigs[_businessModuleName];
                FileInfo fi = new FileInfo(existingFilePath);
                string backupFilePath = fi.DirectoryName + "\\backup";
                if(!Directory.Exists(backupFilePath))
                {
                    Directory.CreateDirectory(backupFilePath);
                }
                string newFilePath = backupFilePath + "\\" + fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length) + "." + DateTime.Now.ToString("yyyyMMdd-hhmmss") + fi.Extension;

                //Backup existing file
                File.Copy(existingFilePath, newFilePath);

                //Save new file
                System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings()
                {
                    Indent = true,
                    OmitXmlDeclaration = true
                };
                string xmlConfig = Serializer.XmlSerialize<TenantConfigurationModel>(_tenantConfig, settings);
                File.WriteAllText(existingFilePath, xmlConfig, System.Text.Encoding.Unicode);

                _originalTenantConfigs[_tenantConfig.TenantId] = _tenantConfig.Clone() as TenantConfigurationModel;                

                isSaved = true;
                MessageBox.Show("Tenant configuration files have been successfully saved.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ToggleDirtyData(false);
            }

            return isSaved;
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

        private bool IsValidBusinessModuleDir(DirectoryInfo di, out List<string> appTiers)
        {
            bool isValid = false;

            appTiers = new List<string>();
            if (!di.Name.StartsWith("_"))
            {
                foreach (DirectoryInfo cdi in di.GetDirectories())
                {
                    if (cdi.Name == "Dev")
                    {
                        foreach (DirectoryInfo ccdi in cdi.GetDirectories())
                        {
                            if (Directory.Exists(ccdi.FullName + @"\Data"))
                            {
                                string configFile = ccdi.FullName + @"\Data\tenantConfiguration." + di.Name + ".xml";
                                if(File.Exists(configFile))
                                {
                                    _moduleConfigs.Add(di.Name, configFile);
                                    appTiers.Add(ccdi.Name);
                                    isValid = true;
                                }
                            }
                        }
                    }
                }
            }

            return isValid;
        }

        private bool IsValidDisplaySizes()
        {
            int idx = 0;
            foreach (DisplaySizeConfigurationModel item in _tenantConfig.Modules[0].DisplayConfiguration.DisplaySizes)
            {
                //Check for ID value
                if (string.IsNullOrEmpty(item.Id))
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[0];
                    DisplaySizesGridView.CurrentCell = DisplaySizesGridView.Rows[idx].Cells[0];
                    DisplaySizesGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show("ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique ID value
                if (_tenantConfig.Modules[0].DisplayConfiguration.DisplaySizes.FindAll(i => string.Compare(i.Id, item.Id, true) == 0).Count > 1)
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[0];
                    DisplaySizesGridView.CurrentCell = DisplaySizesGridView.Rows[idx].Cells[0];
                    DisplaySizesGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show(string.Format("ID must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Id), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for name value
                if (string.IsNullOrEmpty(item.Name))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[0];
                    DisplaySizesGridView.CurrentCell = DisplaySizesGridView.Rows[idx].Cells[1];
                    DisplaySizesGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show("Name is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique names
                if (_tenantConfig.Modules[0].DisplayConfiguration.DisplaySizes.FindAll(i => string.Compare(i.Name, item.Name, true) == 0).Count > 1)
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[0];
                    DisplaySizesGridView.CurrentCell = DisplaySizesGridView.Rows[idx].Cells[1];
                    DisplaySizesGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show(string.Format("Name must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Name), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique default
                int defaults = _tenantConfig.Modules[0].DisplayConfiguration.DisplaySizes.FindAll(i => i.IsDefault).Count;
                if (defaults == 0 || defaults > 1)
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[0];
                    DisplaySizesGridView.CurrentCell = DisplaySizesGridView.Rows[idx].Cells[2];
                    DisplaySizesGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show("You must select a single display type to be a default.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                idx++;
            }
            return true;
        }

        private bool IsValidModelDefinitions()
        {
            int idx = 0;
            foreach (ModelDefinitionModel item in _tenantConfig.Modules[0].ModelDefinitionConfiguration)
            {
                //Check for ID value
                if (string.IsNullOrEmpty(item.Id))
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[2];
                    ModelDefinitionsGridView.CurrentCell = ModelDefinitionsGridView.Rows[idx].Cells[0];
                    ModelDefinitionsGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show("ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique ID value
                if (_tenantConfig.Modules[0].ModelDefinitionConfiguration.FindAll(i => string.Compare(i.Id, item.Id, true) == 0).Count > 1)
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[2];
                    ModelDefinitionsGridView.CurrentCell = ModelDefinitionsGridView.Rows[idx].Cells[0];
                    ModelDefinitionsGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show(string.Format("ID must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Id), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for type value
                if (string.IsNullOrEmpty(item.Type))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[2];
                    ModelDefinitionsGridView.CurrentCell = ModelDefinitionsGridView.Rows[idx].Cells[1];
                    ModelDefinitionsGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show("Type is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for scope value
                if (string.IsNullOrEmpty(item.Scope))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[2];
                    ModelDefinitionsGridView.CurrentCell = ModelDefinitionsGridView.Rows[idx].Cells[2];
                    ModelDefinitionsGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show("Scope is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for display size value
                if (string.IsNullOrEmpty(item.DisplaySize))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[2];
                    ModelDefinitionsGridView.CurrentCell = ModelDefinitionsGridView.Rows[idx].Cells[3];
                    ModelDefinitionsGridView.Rows[idx].Cells[3].Selected = true;
                    MessageBox.Show("Display Size is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                idx++;
            }
            return true;
        }

        private bool IsValidTenantConfigData()
        {
            if (!IsValidDisplaySizes())
            {
                return false;
            }

            if (!IsValidModelDefinitions())
            {
                return false;
            }

            return true;
        }

        private void LoadBusinessModules()
        {
            BusinessModuleDropdown.Items.Clear();
            AppTierDropdown.Items.Clear();
            _modules.Clear();
            _moduleConfigs.Clear();
            _tenantConfigs.Clear();

            DirectoryInfo di = new DirectoryInfo(UserConfig.SourcePath);
            foreach (DirectoryInfo cdi in di.GetDirectories())
            {
                List<string> appTiers = null;
                if (IsValidBusinessModuleDir(cdi, out appTiers))
                {
                    _modules.Add(cdi.Name, appTiers);
                    BusinessModuleDropdown.Items.Add(cdi.Name);
                }
            }
        }

        private void LoadDisplaySizes()
        {
            _displaySizes = new List<DisplaySizeConfigurationModel>();

            foreach (string name in _tenantConfigs.Keys)
            {
                _displaySizes.AddRange(_tenantConfigs[name].Modules[0].DisplayConfiguration.DisplaySizes);
            }
        }

        private void LoadLocaleConfiguration(object selectedLocaleId)
        {
            dataListBindingSource.DataSource = typeof(LocaleConfigurationDataListModel);
            emailTemplatesBindingSource.DataSource = typeof(LocaleConfigurationEmailTemplateModel);
            labelBindingSource.DataSource = typeof(LocaleConfigurationLabelModel);
            messagesBindingSource.DataSource = typeof(LocaleConfigurationMessageModel);

            string localeId = selectedLocaleId.ToString();
            if (selectedLocaleId != null && selectedLocaleId is LocaleConfigurationModel)
            {
                localeId = ((LocaleConfigurationModel)selectedLocaleId).LocaleId;
            }
            LocaleConfigurationModel locale = _tenantConfig.Modules[0].LocalizationConfiguration.Locales.Find(i => i.LocaleId == localeId);
            if (locale != null)
            {
                dataListBindingSource.DataSource = locale.LocaleDataLists;
                emailTemplatesBindingSource.DataSource = locale.LocaleEmailTemplates;
                labelBindingSource.DataSource = locale.LocaleLabels;
                messagesBindingSource.DataSource = locale.LocaleMessages;
            }
        }

        private void LoadTenantConfiguration(TenantConfigurationModel tenantConfig)
        {
            SortConfigurationData(tenantConfig);

            //Application settings
            configurationItemModelBindingSource.DataSource = tenantConfig.Modules[0].ApplicationSettings;

            //Display Sizes
            displaySizeBindingSource.DataSource = tenantConfig.Modules[0].DisplayConfiguration.DisplaySizes;

            //Model Definitions
            modelDefsBindingSource.DataSource = tenantConfig.Modules[0].ModelDefinitionConfiguration;

            //Menus
            menuBindingSource.DataSource = tenantConfig.Modules[0].Menus;

            //Services
            servicesBindingSource.DataSource = tenantConfig.Modules[0].Services;

            //Security Roles
            securityRolesBindingSource.DataSource = tenantConfig.Modules[0].SecurityRoles;
        }

        private void ShowAbout()
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        private void ShowAutoGenModelDefinition()
        {
            AutoGenModelDefForm form = new AutoGenModelDefForm()
            {
                BusinessModelName = _businessModuleName,
                CoreTenantConfig = _coreTenantConfig,
                CoreModelDataAssembly = _currentModelDataAssemblyFile.Replace(_businessModuleName, "Core"),
                CoreModelDataBinPath = _currentModelDataBinPath.Replace(_businessModuleName, "Core"),
                CoreModelNamespace = _currentModelNamespace.Replace(_businessModuleName, "Core"),
                CurrentModelDataAssembly = _currentModelDataAssemblyFile,
                CurrentModelDataBinPath = _currentModelDataBinPath,
                CurrentModelNamespace = _currentModelNamespace,
                DisplaySizes = _displaySizes,
                LocalConfig = _tenantConfig.Modules[0].LocalizationConfiguration,
                ModelDefinitions = _tenantConfig.Modules[0].ModelDefinitionConfiguration
            };
            form.ShowDialog();
            if (form.HasDataChanged)
            {
                SortConfigurationData(_tenantConfig);
                labelBindingSource.ResetBindings(false);
                modelDefsBindingSource.ResetBindings(false);
                ToggleDirtyData(true);
                MessageBox.Show("Model definition successfully generated.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            form.Dispose();
        }

        private void ShowLabelHelper(ref string id, string prefix)
        {
            LocaleLabelHelperForm form = new LocaleLabelHelperForm()
            {
                LabelContentId = id,
                LabelContentIdPrefix = prefix,
                LocalizationConfig = _tenantConfig.Modules[0].LocalizationConfiguration,
                //ShowIds = this.ShowIds
            };
            form.ShowDialog();
            if (form.HasDataChanged)
            {
                id = form.LabelContentId;
                if (!_isDataDrity)
                {
                    ToggleDirtyData(true);
                }
            }
            form.Dispose();
        }

        private void ShowMenuItems(string id)
        {
            MenuModel menu = _tenantConfig.Modules[0].Menus.Find(i => i.Id == id);

            MenuItemsForm form = new MenuItemsForm()
            {
                BusinessModule = _businessModuleName,
                LocalizationConfig = _tenantConfig.Modules[0].LocalizationConfiguration,
                MainMenu = menu,
                MenuItem = null,
                ModuleConfig = _tenantConfig.Modules[0],
                ShowIds = ShowIdsCheckBox.Checked
            };
            form.ShowDialog();
            if (form.HasDataChanged)
            {
                ToggleDirtyData(true);
            }
            form.Dispose();
        }

        private void ShowModelProperties(string id)
        {
            ModelDefinitionModel model = _tenantConfig.Modules[0].ModelDefinitionConfiguration.Find(i => i.Id == id);
            ModelPropertyForm form = new ModelPropertyForm()
            {
                BusinessModule = _businessModuleName,
                LocalizationConfig = _tenantConfig.Modules[0].LocalizationConfiguration,
                Model = model,
                ModuleConfig = _tenantConfig.Modules[0],
                Roles = _tenantConfig.Modules[0].SecurityRoles,
                ShowIds = ShowIdsCheckBox.Checked
            };
            form.ShowDialog();
            if (form.HasDataChanged)
            {
                ToggleDirtyData(true);
            }
            form.Dispose();
        }

        private void ShowSecurityFunctions(string id)
        {
            SecurityRoleModel role = _tenantConfig.Modules[0].SecurityRoles.Find(i => i.Id == id);

            SecurityFunctionsForm form = new SecurityFunctionsForm()
            {
                BusinessModule = _businessModuleName,
                LocalizationConfig = _tenantConfig.Modules[0].LocalizationConfiguration,
                Role = role,
                ShowIds = ShowIdsCheckBox.Checked
            };
            form.ShowDialog();
            if (form.HasDataChanged)
            {
                ToggleDirtyData(true);
            }
            form.Dispose();
        }

        private void ShowSecurityRightHelper(ref string id, SecurityRightModel.RightType rightType, string prefix, string name)
        {
            SecurityRightHelperForm form = new SecurityRightHelperForm()
            {
                ModuleConfig = _tenantConfig.Modules[0],
                RightId = id,
                RightName = name,
                RightNamePrefix = prefix,
                RightType = rightType
                //ShowIds = this.ShowIds
            };
            form.ShowDialog();
            if (form.HasDataChanged)
            {
                id = form.RightId;
                if (!_isDataDrity)
                {
                    ToggleDirtyData(true);
                }
            }
            form.Dispose();
        }

        private void ShowUserConfig()
        {
            UserConfigForm form = new UserConfigForm();
            form.ShowDialog();
            if(form.HasDataChanged)
            {
                InitializeForm();
            }
            form.Dispose();
        }

        private void SortConfigurationData(TenantConfigurationModel tenantConfig)
        {
            if (tenantConfig.Modules[0].ApplicationSettings != null)
            {
                tenantConfig.Modules[0].ApplicationSettings.Sort(
                    delegate(ConfigurationItemModel i1, ConfigurationItemModel i2) { return string.Compare(i1.Key, i2.Key); }
                );
            }

            if (tenantConfig.Modules[0].DisplayConfiguration != null)
            {
                if (tenantConfig.Modules[0].DisplayConfiguration.DisplaySizes != null)
                {
                    tenantConfig.Modules[0].DisplayConfiguration.DisplaySizes.Sort(
                        delegate(DisplaySizeConfigurationModel i1, DisplaySizeConfigurationModel i2) { return string.Compare(i1.Name, i2.Name); }
                    );
                }
            }

            if (tenantConfig.Modules[0].LocalizationConfiguration != null)
            {
                if (tenantConfig.Modules[0].LocalizationConfiguration.Locales != null)
                {
                    foreach (LocaleConfigurationModel locale in tenantConfig.Modules[0].LocalizationConfiguration.Locales)
                    {
                        locale.LocaleDataLists.Sort(
                            delegate(LocaleConfigurationDataListModel i1, LocaleConfigurationDataListModel i2) { return string.Compare(i1.ContentId, i2.ContentId); }
                        );
                        foreach (LocaleConfigurationDataListModel item in locale.LocaleDataLists)
                        {
                            item.LocaleDataListItems.Sort(
                                delegate(LocaleConfigurationDataListItemModel i1, LocaleConfigurationDataListItemModel i2) { return i1.Order.CompareTo(i2.Order); }
                            );
                        }

                        locale.LocaleEmailTemplates.Sort(
                            delegate(LocaleConfigurationEmailTemplateModel i1, LocaleConfigurationEmailTemplateModel i2) { return string.Compare(i1.ContentId, i2.ContentId); }
                        );

                        locale.LocaleLabels.Sort(
                            delegate(LocaleConfigurationLabelModel i1, LocaleConfigurationLabelModel i2) { return string.Compare(i1.ContentId, i2.ContentId); }
                        );

                        locale.LocaleMessages.Sort(
                            delegate(LocaleConfigurationMessageModel i1, LocaleConfigurationMessageModel i2) { return string.Compare(i1.ContentId, i2.ContentId); }
                        );
                    }
                }
            }

            if (tenantConfig.Modules[0].ModelDefinitionConfiguration != null)
            {
                tenantConfig.Modules[0].ModelDefinitionConfiguration.Sort(
                    delegate(ModelDefinitionModel i1, ModelDefinitionModel i2) { return string.Compare(i1.Type, i2.Type); }
                );
                foreach (ModelDefinitionModel item in tenantConfig.Modules[0].ModelDefinitionConfiguration)
                {
                    if (item.ModelProperties != null)
                    {
                        item.ModelProperties.Sort(
                            delegate(ModelPropertyModel i1, ModelPropertyModel i2) { return string.Compare(i1.Name, i2.Name); }
                        );
                    }
                }
            }

            if (tenantConfig.Modules[0].Menus != null)
            {
                tenantConfig.Modules[0].Menus.Sort(
                    delegate(MenuModel i1, MenuModel i2) { return string.Compare(i1.Name, i2.Name); }
                );
                foreach (MenuModel item in tenantConfig.Modules[0].Menus)
                {
                    item.Items.Sort(
                        delegate(MenuItemModel i1, MenuItemModel i2) { return string.Compare(i1.Name, i2.Name); }
                    );
                    if (item.Items != null)
                    {
                        foreach (MenuItemModel childItem in item.Items)
                        {
                            if (childItem.Items != null)
                            {
                                childItem.Items.Sort(
                                    delegate(MenuItemModel i1, MenuItemModel i2) { return string.Compare(i1.Name, i2.Name); }
                                );
                            }
                        }
                    }
                }
            }

            if (tenantConfig.Modules[0].Services != null)
            {
                tenantConfig.Modules[0].Services.Sort(
                    delegate(ServiceItemModel i1, ServiceItemModel i2) { return string.Compare(i1.Name, i2.Name); }
                );
            }

            if (tenantConfig.Modules[0].SecurityRoles != null)
            {
                tenantConfig.Modules[0].SecurityRoles.Sort(
                    delegate(SecurityRoleModel i1, SecurityRoleModel i2) { return string.Compare(i1.Name, i2.Name); }
                );
                foreach (SecurityRoleModel role in tenantConfig.Modules[0].SecurityRoles)
                {
                    if (role.Functions != null)
                    {
                        role.Functions.Sort(
                            delegate(SecurityFunctionModel i1, SecurityFunctionModel i2) { return string.Compare(i1.Name, i2.Name); }
                        );
                        foreach (SecurityFunctionModel function in role.Functions)
                        {
                            if (function.Rights != null)
                            {
                                function.Rights.Sort(
                                    delegate(SecurityRightModel i1, SecurityRightModel i2) { return string.Compare(i1.Name, i2.Name); }
                                );
                            }
                        }
                    }
                }
            }
        }

        private void ToggleDirtyData(bool enabled)
        {
            _isDataDrity = enabled;
            ResetButton.Enabled = enabled;
            SaveButton.Enabled = enabled;
            ViewChangesButton.Enabled = enabled;
            saveToolStripMenuItem.Enabled = enabled;
        }

        private void ToggleShowIds(bool showIds)
        {
            DisplaySizesGridView.Columns[0].Visible = showIds;

            ModelDefinitionsGridView.Columns[0].Visible = showIds;
            
        }
        #endregion

        private void ViewChangesButton_Click(object sender, EventArgs e)
        {
            if (IsValidTenantConfigData())
            {
                var originalConfigs = _originalTenantConfigs.Select(p => p.Value).ToList();
                var updatedConfigs = _tenantConfigs.Select(p => p.Value).ToList();

                using (var historyForm = new ChangeHistoryForm(originalConfigs, updatedConfigs))
                {
                    historyForm.ShowDialog();
                }
            }
        }
    }
}
