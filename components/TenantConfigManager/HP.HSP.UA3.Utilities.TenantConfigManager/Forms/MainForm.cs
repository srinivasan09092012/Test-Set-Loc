using HP.HSP.UA3.Core.UX.Common;
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
                    TenantDropdown.Enabled = true;
                    TenantDropdown.Focus();
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

        #region IOC Configuration Tab Events
        private void IocTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_tenantConfigs != null && _tenantConfigs.Count > 0)
            {
                _tenantConfig.Modules[0].IocConfigurationString = IocTextBox.Text;
                ToggleDirtyData(true);
            }
        }
        #endregion

        #region Localization DataLists Tab Events
        private void LocaleDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (LocaleDropdown.SelectedValue != null)
                {
                    LoadLocaleConfiguration(LocaleDropdown.SelectedValue);
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

        private void DataListsGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Row.Cells[0].Value = Common.Utilities.GenerateNewID();
                e.Row.Cells[1].Value = LocaleDropdown.SelectedValue;
                e.Row.Cells[3].Value = BusinessModuleDropdown.Text + ".DataList.";
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

        private void DataListsGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Cancel = !ConfirmDeleteRow(e.Row.Cells[3].Value.ToString());
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

        private void dataListBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (_tenantConfigs != null && _tenantConfigs.Count > 0)
            {
                ToggleDirtyData(true);
            }
        }
        #endregion

        #region Localization EmailTemplates Tab Events
        private void EmailTemplatesGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == EmailTemplatesGridView.NewRowIndex)
            {
                if (EmailTemplatesGridView.CurrentCell.EditType == typeof(DataGridViewTextBoxEditingControl))
                {
                    EmailTemplatesGridView.BeginEdit(false);
                    TextBox textBox = (TextBox)EmailTemplatesGridView.EditingControl;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void emailTemplatesBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (_tenantConfigs != null && _tenantConfigs.Count > 0)
            {
                ToggleDirtyData(true);
            }
        }

        private void EmailTemplatesGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Row.Cells[0].Value = Common.Utilities.GenerateNewID();
                e.Row.Cells[1].Value = LocaleDropdown.SelectedValue;
                e.Row.Cells[2].Value = BusinessModuleDropdown.Text + ".EmailTemplate.";
                e.Row.Cells[5].Value = "Normal";
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

        private void EmailTemplatesGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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

        private void EmailTemplatesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (e.ColumnIndex == 6)
                {
                    string id = Convert.ToString(EmailTemplatesGridView.Rows[e.RowIndex].Cells[0].Value);
                    ShowEmailTemplateEditor(id);
                }
                else if (e.ColumnIndex == 9)
                {
                    string id = Convert.ToString(EmailTemplatesGridView.Rows[e.RowIndex].Cells[0].Value);
                    ShowEmailTemplateAddresses(id);
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

        #region Localization HtmlBlocks Tab Events
        private void HtmlBlocksGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == HtmlBlocksGridView.NewRowIndex)
            {
                if (HtmlBlocksGridView.CurrentCell.EditType == typeof(DataGridViewTextBoxEditingControl))
                {
                    HtmlBlocksGridView.BeginEdit(false);
                    TextBox textBox = (TextBox)HtmlBlocksGridView.EditingControl;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void htmlBlockBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (_tenantConfigs != null && _tenantConfigs.Count > 0)
            {
                ToggleDirtyData(true);
            }
        }

        private void HtmlBlocksGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Row.Cells[0].Value = Common.Utilities.GenerateNewID();
                e.Row.Cells[1].Value = LocaleDropdown.SelectedValue;
                e.Row.Cells[2].Value = BusinessModuleDropdown.Text + ".HtmlBlock.";
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

        private void HtmlBlocksGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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

        private void HtmlBlocksGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (e.ColumnIndex == 3)
                {
                    string id = Convert.ToString(HtmlBlocksGridView.Rows[e.RowIndex].Cells[0].Value);
                    ShowHtmlBlockEditor(id);
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

        #region Localization Labels Tab Events
        private void labelBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (_tenantConfigs != null && _tenantConfigs.Count > 0)
            {
                ToggleDirtyData(true);
            }
        }

        private void LabelsGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == LabelsGridView.NewRowIndex)
            {
                if (LabelsGridView.CurrentCell.EditType == typeof(DataGridViewTextBoxEditingControl))
                {
                    LabelsGridView.BeginEdit(false);
                    TextBox textBox = (TextBox)LabelsGridView.EditingControl;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void LabelsGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Row.Cells[0].Value = Common.Utilities.GenerateNewID();
                e.Row.Cells[1].Value = LocaleDropdown.SelectedValue;
                e.Row.Cells[2].Value = BusinessModuleDropdown.Text + ".Label.";
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

        private void LabelsGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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
        #endregion

        #region Localization Messages Tab Events
        private void messagesBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (_tenantConfigs != null && _tenantConfigs.Count > 0)
            {
                ToggleDirtyData(true);
            }
        }

        private void MessagesGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == MessagesGridView.NewRowIndex)
            {
                if (MessagesGridView.CurrentCell.EditType == typeof(DataGridViewTextBoxEditingControl))
                {
                    MessagesGridView.BeginEdit(false);
                    TextBox textBox = (TextBox)MessagesGridView.EditingControl;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void MessagesGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Row.Cells[0].Value = Common.Utilities.GenerateNewID();
                e.Row.Cells[1].Value = LocaleDropdown.SelectedValue;
                e.Row.Cells[2].Value = BusinessModuleDropdown.Text + ".Msg.";
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

        private void MessagesGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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

        #region Menu Tab Events
        private void MenuBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (_tenantConfigs != null && _tenantConfigs.Count > 0)
            {
                ToggleDirtyData(true);
            }
        }

        private void MenusGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == MenusGridView.NewRowIndex)
            {
                if (MenusGridView.CurrentCell.EditType == typeof(DataGridViewTextBoxEditingControl))
                {
                    MenusGridView.BeginEdit(false);
                    TextBox textBox = (TextBox)MenusGridView.EditingControl;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void MenusGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Row.Cells[0].Value = Common.Utilities.GenerateNewID();
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

        private void MenusGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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

        private void MenusGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                switch(e.ColumnIndex)
                {
                    case 2:
                        string id = Convert.ToString(MenusGridView.Rows[e.RowIndex].Cells[2].Value);
                        ShowSecurityRightHelper(ref id, SecurityRightModel.RightType.Page, "Page.", "Page." + MenusGridView.Rows[e.RowIndex].Cells[1].Value.ToString());
                        MenusGridView.Rows[e.RowIndex].Cells[2].Value = id;
                        break;

                    case 4:
                        id = Convert.ToString(MenusGridView.Rows[e.RowIndex].Cells[0].Value);
                        ShowMenuItems(id);
                        break;
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

        #region Service Tab Events
        private void servicesBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (_tenantConfigs != null && _tenantConfigs.Count > 0)
            {
                ToggleDirtyData(true);
            }
        }

        private void ServicesGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == ServicesGridView.NewRowIndex)
            {
                if (ServicesGridView.CurrentCell.EditType == typeof(DataGridViewTextBoxEditingControl))
                {
                    ServicesGridView.BeginEdit(false);
                    TextBox textBox = (TextBox)ServicesGridView.EditingControl;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void ServicesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                switch(e.ColumnIndex)
                {
                    case 2:
                        string id = Convert.ToString(ServicesGridView.Rows[e.RowIndex].Cells[2].Value);
                        ShowSecurityRightHelper(ref id, SecurityRightModel.RightType.Service, _businessModuleName + ".Service.", _businessModuleName + "." + ServicesGridView.Rows[e.RowIndex].Cells[1].Value.ToString());
                        ServicesGridView.Rows[e.RowIndex].Cells[2].Value = id;
                        break;

                    case 3:
                        id = Convert.ToString(ServicesGridView.Rows[e.RowIndex].Cells[3].Value);
                        ShowLabelHelper(ref id, string.Format("{0}.Label.Service.", _businessModuleName));
                        ServicesGridView.Rows[e.RowIndex].Cells[3].Value = id;
                        break;
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

        private void ServicesDataGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Row.Cells[0].Value = Common.Utilities.GenerateNewID();
                e.Row.Cells[1].Value = "Service.";
                e.Row.Cells[3].Value = BusinessModuleDropdown.Text + ".Label.Service.";
                e.Row.Cells[6].Value = BusinessModuleDropdown.Text;
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

        private void ServicesDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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
        #endregion

        #region Security Roles Tab Events
        private void securityRolesBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (_tenantConfigs != null && _tenantConfigs.Count > 0)
            {
                ToggleDirtyData(true);
            }
        }

        private void SecurityRolesGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex == SecurityRolesGridView.NewRowIndex)
            {
                if (SecurityRolesGridView.CurrentCell.EditType == typeof(DataGridViewTextBoxEditingControl))
                {
                    SecurityRolesGridView.BeginEdit(false);
                    TextBox textBox = (TextBox)SecurityRolesGridView.EditingControl;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void SecurityRolesGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Row.Cells[0].Value = Common.Utilities.GenerateNewID();
                e.Row.Cells[4].Value = 1;
                e.Row.Cells[6].Value = true;
                e.Row.Cells[7].Value = true;
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

        private void SecurityRolesGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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

        private void SecurityRolesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                switch(e.ColumnIndex)
                {
                    case 2:
                        string id = Convert.ToString(SecurityRolesGridView.Rows[e.RowIndex].Cells[2].Value);
                        ShowLabelHelper(ref id, string.Format("{0}.Label.Security.Roles.", _businessModuleName));
                        SecurityRolesGridView.Rows[e.RowIndex].Cells[2].Value = id;
                        break;

                    case 5:
                        id = Convert.ToString(SecurityRolesGridView.Rows[e.RowIndex].Cells[5].Value);
                        ShowSecurityRightHelper(ref id, SecurityRightModel.RightType.Other, _businessModuleName + ".Other.", _businessModuleName + ".Other." + SecurityRolesGridView.Rows[e.RowIndex].Cells[1].Value.ToString());
                        SecurityRolesGridView.Rows[e.RowIndex].Cells[5].Value = id;
                        break;

                    case 8:
                        id = Convert.ToString(SecurityRolesGridView.Rows[e.RowIndex].Cells[0].Value);
                        ShowSecurityFunctions(id);
                        break;
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

            _tenantConfigs.Add(tenantConfig.Name, tenantConfig);
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
            tenantConfig.Contacts = new List<Core.UX.Data.Common.ContactModel>();
            tenantConfig.Modules = new List<ModuleConfigurationModel>();
            tenantConfig.Modules.Add(
                new ModuleConfigurationModel()
                {
                    ApplicationSettings = new List<ConfigurationItemModel>(),
                    DisplayConfiguration = new DisplayConfigurationModel(),
                    IocConfigurationString = string.Empty,
                    LocalizationConfiguration = new LocalizationConfigurationModel(),
                    Menus = new List<MenuModel>(),
                    ModelDefinitionConfiguration = new List<ModelDefinitionModel>(),
                    SecurityRoles = new List<SecurityRoleModel>(),
                    Services = new List<ServiceItemModel>()
                }
            );
            tenantConfig.Modules[0].LocalizationConfiguration.Locales = new List<LocaleConfigurationModel>();

            LoadTenantConfiguration(tenantConfig);
            LocalizationTabControl.SelectTab(0);
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

                                if (locale.LocaleHtmlBlocks != null && locale.LocaleHtmlBlocks.Count > 0)
                                {
                                    foreach (LocaleConfigurationHtmlBlockModel block in locale.LocaleHtmlBlocks)
                                    {
                                        block.Id = block.Id.ToLower();
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
                                menu.SecurityRightId = menu.SecurityRightId.ToLower();

                                if(menu.Items != null && menu.Items.Count > 0)
                                {
                                    foreach (MenuItemModel item in menu.Items)
                                    {
                                        item.Id = item.Id.ToLower();

                                        if (item.SecurityRightId != null)
                                        {
                                            item.SecurityRightId = item.SecurityRightId.ToLower();
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

                                        if (prop.EditRestrictionSecurityRightId != null)
                                        {
                                            prop.EditRestrictionSecurityRightId = prop.EditRestrictionSecurityRightId.ToLower();
                                        }

                                        if (prop.ViewRestrictionSecurityRightId != null)
                                        {
                                            prop.ViewRestrictionSecurityRightId = prop.ViewRestrictionSecurityRightId.ToLower();
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

                                if (service.SecurityRightId != null)
                                {
                                    service.SecurityRightId = service.SecurityRightId.ToLower();
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
            ((DataGridViewComboBoxColumn)EmailTemplatesGridView.Columns[5]).DataSource = Enum.GetValues(typeof(CoreEnumerations.Notifications.PriorityType));
            ((DataGridViewComboBoxColumn)MessagesGridView.Columns[3]).DataSource = Enum.GetValues(typeof(CoreEnumerations.Messaging.MessageType));
        }

        private void InitializeForm()
        {
            //Set styles
            TenantConfigManager.Common.Utilities.StyleXmlEditor(IocTextBox);

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
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[1];
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
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[1];
                    DisplaySizesGridView.CurrentCell = DisplaySizesGridView.Rows[idx].Cells[0];
                    DisplaySizesGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show(string.Format("ID must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Id), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for name value
                if (string.IsNullOrEmpty(item.Name))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[1];
                    DisplaySizesGridView.CurrentCell = DisplaySizesGridView.Rows[idx].Cells[1];
                    DisplaySizesGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show("Name is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique names
                if (_tenantConfig.Modules[0].DisplayConfiguration.DisplaySizes.FindAll(i => string.Compare(i.Name, item.Name, true) == 0).Count > 1)
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[1];
                    DisplaySizesGridView.CurrentCell = DisplaySizesGridView.Rows[idx].Cells[1];
                    DisplaySizesGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show(string.Format("Name must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Name), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique default
                int defaults = _tenantConfig.Modules[0].DisplayConfiguration.DisplaySizes.FindAll(i => i.IsDefault).Count;
                if (defaults == 0 || defaults > 1)
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[1];
                    DisplaySizesGridView.CurrentCell = DisplaySizesGridView.Rows[idx].Cells[2];
                    DisplaySizesGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show("You must select a single display type to be a default.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                idx++;
            }
            return true;
        }

        private bool IsValidIOC()
        {
            return true;
        }

        private bool IsValidLocalization()
        {
            int idx = 0;
            foreach (LocaleConfigurationModel item in _tenantConfig.Modules[0].LocalizationConfiguration.Locales)
            {
                if (!IsValidLocalizationEmailTemplates(item.LocaleEmailTemplates, idx))
                {
                    return false;
                }

                if (!IsValidLocalizationHtmlBlocks(item.LocaleHtmlBlocks, idx))
                {
                    return false;
                }

                if (!IsValidLocalizationLabels(item.LocaleLabels, idx))
                {
                    return false;
                }

                if (!IsValidLocalizationMessages(item.LocaleMessages, idx))
                {
                    return false;
                }

                idx++;
            }
            return true;
        }

        private bool IsValidLocalizationEmailTemplates(List<LocaleConfigurationEmailTemplateModel> items, int localeIdx)
        {
            int idx = 0;
            foreach (LocaleConfigurationEmailTemplateModel item in items)
            {
                //Check for ID value
                if (string.IsNullOrEmpty(item.Id))
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = TenantConfigTabControl.TabPages[1];
                    EmailTemplatesGridView.CurrentCell = EmailTemplatesGridView.Rows[idx].Cells[0];
                    EmailTemplatesGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show("ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique ID value
                if (_tenantConfig.Modules[0].LocalizationConfiguration.Locales[localeIdx].LocaleEmailTemplates.FindAll(i => string.Compare(i.Id, item.Id, true) == 0).Count > 1)
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[1];
                    EmailTemplatesGridView.CurrentCell = EmailTemplatesGridView.Rows[idx].Cells[0];
                    EmailTemplatesGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show(string.Format("ID must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Id), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for content id
                if (string.IsNullOrEmpty(item.ContentId))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[1];
                    EmailTemplatesGridView.CurrentCell = EmailTemplatesGridView.Rows[idx].Cells[2];
                    EmailTemplatesGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show("Content ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for proper named content id
                string prefix = BusinessModuleDropdown.Text + ".EmailTemplate.";
                if (!item.ContentId.StartsWith(prefix))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[1];
                    EmailTemplatesGridView.CurrentCell = EmailTemplatesGridView.Rows[idx].Cells[2];
                    EmailTemplatesGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show(string.Format("Content ID must start with the prefix '{0}'.", prefix), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique content id
                if (items.FindAll(i => string.Compare(i.ContentId, item.ContentId, true) == 0).Count > 1)
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[1];
                    EmailTemplatesGridView.CurrentCell = EmailTemplatesGridView.Rows[idx].Cells[2];
                    EmailTemplatesGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show(string.Format("Content ID must be a unqiue value. There are more than 1 rows with a content ID value of '{0}'.", item.ContentId), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for name
                if (string.IsNullOrEmpty(item.Name))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[1];
                    EmailTemplatesGridView.CurrentCell = EmailTemplatesGridView.Rows[idx].Cells[3];
                    EmailTemplatesGridView.Rows[idx].Cells[3].Selected = true;
                    MessageBox.Show("Name is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for subject
                if (string.IsNullOrEmpty(item.Subject))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[1];
                    EmailTemplatesGridView.CurrentCell = EmailTemplatesGridView.Rows[idx].Cells[4];
                    EmailTemplatesGridView.Rows[idx].Cells[4].Selected = true;
                    MessageBox.Show("Subject is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for body
                if (string.IsNullOrEmpty(item.BodyString))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[1];
                    EmailTemplatesGridView.CurrentCell = EmailTemplatesGridView.Rows[idx].Cells[6];
                    EmailTemplatesGridView.Rows[idx].Cells[6].Selected = true;
                    MessageBox.Show("Body is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for start date less than end date
                if(DateTime.Compare(item.StartDate, item.EndDate) > 0)
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[1];
                    EmailTemplatesGridView.CurrentCell = EmailTemplatesGridView.Rows[idx].Cells[8];
                    EmailTemplatesGridView.Rows[idx].Cells[8].Selected = true;
                    MessageBox.Show("Start date must be before or on end date.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                idx++;
            }
            return true;
        }

        private bool IsValidLocalizationHtmlBlocks(List<LocaleConfigurationHtmlBlockModel> items, int localeIdx)
        {
            int idx = 0;
            foreach (LocaleConfigurationHtmlBlockModel item in items)
            {
                //Check for ID value
                if (string.IsNullOrEmpty(item.Id))
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = TenantConfigTabControl.TabPages[2];
                    HtmlBlocksGridView.CurrentCell = HtmlBlocksGridView.Rows[idx].Cells[0];
                    HtmlBlocksGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show("ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique ID value
                if (_tenantConfig.Modules[0].LocalizationConfiguration.Locales[localeIdx].LocaleHtmlBlocks.FindAll(i => string.Compare(i.Id, item.Id, true) == 0).Count > 1)
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[2];
                    HtmlBlocksGridView.CurrentCell = HtmlBlocksGridView.Rows[idx].Cells[0];
                    HtmlBlocksGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show(string.Format("ID must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Id), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for content id
                if (string.IsNullOrEmpty(item.ContentId))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[2];
                    HtmlBlocksGridView.CurrentCell = HtmlBlocksGridView.Rows[idx].Cells[2];
                    HtmlBlocksGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show("Content ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for proper named content id
                string prefix = BusinessModuleDropdown.Text + ".HtmlBlock.";
                if (!item.ContentId.StartsWith(prefix))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[2];
                    HtmlBlocksGridView.CurrentCell = HtmlBlocksGridView.Rows[idx].Cells[2];
                    HtmlBlocksGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show(string.Format("Content ID must start with the prefix '{0}'.", prefix), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique content id
                if (items.FindAll(i => string.Compare(i.ContentId, item.ContentId, true) == 0).Count > 1)
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[2];
                    HtmlBlocksGridView.CurrentCell = HtmlBlocksGridView.Rows[idx].Cells[2];
                    HtmlBlocksGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show(string.Format("Content ID must be a unqiue value. There are more than 1 rows with a content ID value of '{0}'.", item.ContentId), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for html
                if (string.IsNullOrEmpty(item.HtmlString))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[2];
                    HtmlBlocksGridView.CurrentCell = HtmlBlocksGridView.Rows[idx].Cells[3];
                    HtmlBlocksGridView.Rows[idx].Cells[3].Selected = true;
                    MessageBox.Show("Html is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                idx++;
            }
            return true;
        }

        private bool IsValidLocalizationLabels(List<LocaleConfigurationLabelModel> items, int localeIdx)
        {
            int idx = 0;
            foreach (LocaleConfigurationLabelModel item in items)
            {
                //Check for ID value
                if (string.IsNullOrEmpty(item.Id))
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = TenantConfigTabControl.TabPages[4];
                    LabelsGridView.CurrentCell = LabelsGridView.Rows[idx].Cells[0];
                    LabelsGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show("ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique ID value
                if (_tenantConfig.Modules[0].LocalizationConfiguration.Locales[localeIdx].LocaleLabels.FindAll(i => string.Compare(i.Id, item.Id, true) == 0).Count > 1)
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[4];
                    LabelsGridView.CurrentCell = LabelsGridView.Rows[idx].Cells[0];
                    LabelsGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show(string.Format("ID must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Id), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for content id
                if (string.IsNullOrEmpty(item.ContentId))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[4];
                    LabelsGridView.CurrentCell = LabelsGridView.Rows[idx].Cells[2];
                    LabelsGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show("Content ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for proper named content id
                string prefix = BusinessModuleDropdown.Text + ".Label.";
                if (!item.ContentId.StartsWith(prefix))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[4];
                    LabelsGridView.CurrentCell = LabelsGridView.Rows[idx].Cells[2];
                    LabelsGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show(string.Format("Content ID must start with the prefix '{0}'.", prefix), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique content id
                if (items.FindAll(i => string.Compare(i.ContentId, item.ContentId, true) == 0).Count > 1)
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[4];
                    LabelsGridView.CurrentCell = LabelsGridView.Rows[idx].Cells[2];
                    LabelsGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show(string.Format("Content ID must be a unqiue value. There are more than 1 rows with a content ID value of '{0}'.", item.ContentId), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for text
                if (string.IsNullOrEmpty(item.Text))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[4];
                    LabelsGridView.CurrentCell = LabelsGridView.Rows[idx].Cells[3];
                    LabelsGridView.Rows[idx].Cells[3].Selected = true;
                    MessageBox.Show("Text is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                idx++;
            }
            return true;
        }

        private bool IsValidLocalizationMessages(List<LocaleConfigurationMessageModel> items, int localeIdx)
        {
            int idx = 0;
            foreach (LocaleConfigurationMessageModel item in items)
            {
                //Check for ID value
                if (string.IsNullOrEmpty(item.Id))
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = TenantConfigTabControl.TabPages[5];
                    MessagesGridView.CurrentCell = MessagesGridView.Rows[idx].Cells[0];
                    MessagesGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show("ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique ID value
                if (_tenantConfig.Modules[0].LocalizationConfiguration.Locales[localeIdx].LocaleMessages.FindAll(i => string.Compare(i.Id, item.Id, true) == 0).Count > 1)
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[5];
                    MessagesGridView.CurrentCell = MessagesGridView.Rows[idx].Cells[0];
                    MessagesGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show(string.Format("ID must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Id), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for content id
                if (string.IsNullOrEmpty(item.ContentId))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[5];
                    MessagesGridView.CurrentCell = MessagesGridView.Rows[idx].Cells[2];
                    MessagesGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show("Content ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for proper named content id
                string prefix = BusinessModuleDropdown.Text + ".Msg.";
                if (!item.ContentId.StartsWith(prefix))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[5];
                    MessagesGridView.CurrentCell = MessagesGridView.Rows[idx].Cells[2];
                    MessagesGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show(string.Format("Content ID must start with the prefix '{0}'.", prefix), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique content id
                if (items.FindAll(i => string.Compare(i.ContentId, item.ContentId, true) == 0).Count > 1)
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[5];
                    MessagesGridView.CurrentCell = MessagesGridView.Rows[idx].Cells[2];
                    MessagesGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show(string.Format("Content ID must be a unqiue value. There are more than 1 rows with a content ID value of '{0}'.", item.ContentId), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for text
                if (string.IsNullOrEmpty(item.Text))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[3];
                    LocaleDropdown.SelectedIndex = localeIdx;
                    LocalizationTabControl.SelectedTab = LocalizationTabControl.TabPages[5];
                    MessagesGridView.CurrentCell = MessagesGridView.Rows[idx].Cells[4];
                    MessagesGridView.Rows[idx].Cells[4].Selected = true;
                    MessageBox.Show("Text is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[4];
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
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[4];
                    ModelDefinitionsGridView.CurrentCell = ModelDefinitionsGridView.Rows[idx].Cells[0];
                    ModelDefinitionsGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show(string.Format("ID must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Id), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for type value
                if (string.IsNullOrEmpty(item.Type))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[4];
                    ModelDefinitionsGridView.CurrentCell = ModelDefinitionsGridView.Rows[idx].Cells[1];
                    ModelDefinitionsGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show("Type is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for scope value
                if (string.IsNullOrEmpty(item.Scope))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[4];
                    ModelDefinitionsGridView.CurrentCell = ModelDefinitionsGridView.Rows[idx].Cells[2];
                    ModelDefinitionsGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show("Scope is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for display size value
                if (string.IsNullOrEmpty(item.DisplaySize))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[4];
                    ModelDefinitionsGridView.CurrentCell = ModelDefinitionsGridView.Rows[idx].Cells[3];
                    ModelDefinitionsGridView.Rows[idx].Cells[3].Selected = true;
                    MessageBox.Show("Display Size is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                idx++;
            }
            return true;
        }

        private bool IsValidMenus()
        {
            int idx = 0;
            foreach (MenuModel item in _tenantConfig.Modules[0].Menus)
            {
                //Check for ID value
                if (string.IsNullOrEmpty(item.Id))
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[5];
                    MenusGridView.CurrentCell = MenusGridView.Rows[idx].Cells[0];
                    MenusGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show("ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique ID value
                if (_tenantConfig.Modules[0].Menus.FindAll(i => string.Compare(i.Id, item.Id, true) == 0).Count > 1)
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[5];
                    MenusGridView.CurrentCell = MenusGridView.Rows[idx].Cells[0];
                    MenusGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show(string.Format("ID must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Id), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for name value
                if (string.IsNullOrEmpty(item.Name))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[5];
                    MenusGridView.CurrentCell = MenusGridView.Rows[idx].Cells[1];
                    MenusGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show("Name is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique name value
                if (_tenantConfig.Modules[0].Menus.FindAll(i => string.Compare(i.Name, item.Name, true) == 0).Count > 1)
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[5];
                    MenusGridView.CurrentCell = MenusGridView.Rows[idx].Cells[1];
                    MenusGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show(string.Format("Name must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Name), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for security right value
                if (string.IsNullOrEmpty(item.SecurityRightId))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[5];
                    MenusGridView.CurrentCell = MenusGridView.Rows[idx].Cells[2];
                    MenusGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show("Security Right is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for display size value
                if (string.IsNullOrEmpty(item.DisplaySize))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[5];
                    MenusGridView.CurrentCell = MenusGridView.Rows[idx].Cells[3];
                    MenusGridView.Rows[idx].Cells[3].Selected = true;
                    MessageBox.Show("Display Size is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                idx++;
            }
            return true;
        }

        private bool IsValidServices()
        {
            int idx = 0;
            foreach (ServiceItemModel item in _tenantConfig.Modules[0].Services)
            {
                //Check for ID value
                if (string.IsNullOrEmpty(item.Id))
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[6];
                    ServicesGridView.CurrentCell = ServicesGridView.Rows[idx].Cells[0];
                    ServicesGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show("ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique ID value
                if (_tenantConfig.Modules[0].Menus.FindAll(i => string.Compare(i.Id, item.Id, true) == 0).Count > 1)
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[6];
                    ServicesGridView.CurrentCell = ServicesGridView.Rows[idx].Cells[0];
                    ServicesGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show(string.Format("ID must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Id), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for name value
                if (string.IsNullOrEmpty(item.Name))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[6];
                    ServicesGridView.CurrentCell = ServicesGridView.Rows[idx].Cells[1];
                    ServicesGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show("Name is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique name value
                if (_tenantConfig.Modules[0].Menus.FindAll(i => string.Compare(i.Name, item.Name, true) == 0).Count > 1)
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[6];
                    ServicesGridView.CurrentCell = ServicesGridView.Rows[idx].Cells[1];
                    ServicesGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show(string.Format("Name must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Name), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for security right value
                if (string.IsNullOrEmpty(item.SecurityRightId))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[6];
                    ServicesGridView.CurrentCell = ServicesGridView.Rows[idx].Cells[2];
                    ServicesGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show("Security Right is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for base url value
                if (string.IsNullOrEmpty(item.BaseUrl))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[6];
                    ServicesGridView.CurrentCell = ServicesGridView.Rows[idx].Cells[5];
                    ServicesGridView.Rows[idx].Cells[5].Selected = true;
                    MessageBox.Show("Base URL is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                idx++;
            }
            return true;
        }

        private bool IsValidSecurtyRoles()
        {
            int idx = 0;
            foreach (SecurityRoleModel item in _tenantConfig.Modules[0].SecurityRoles)
            {
                //Check for ID value
                if (string.IsNullOrEmpty(item.Id))
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[7];
                    SecurityRolesGridView.CurrentCell = SecurityRolesGridView.Rows[idx].Cells[0];
                    SecurityRolesGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show("ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique ID value
                if (_tenantConfig.Modules[0].SecurityRoles.FindAll(i => string.Compare(i.Id, item.Id, true) == 0).Count > 1)
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[7];
                    SecurityRolesGridView.CurrentCell = SecurityRolesGridView.Rows[idx].Cells[0];
                    SecurityRolesGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show(string.Format("ID must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Id), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for name value
                if (string.IsNullOrEmpty(item.Name))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[7];
                    SecurityRolesGridView.CurrentCell = SecurityRolesGridView.Rows[idx].Cells[1];
                    SecurityRolesGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show("Name is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique name value
                if (_tenantConfig.Modules[0].Menus.FindAll(i => string.Compare(i.Name, item.Name, true) == 0).Count > 1)
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[7];
                    SecurityRolesGridView.CurrentCell = SecurityRolesGridView.Rows[idx].Cells[1];
                    SecurityRolesGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show(string.Format("Name must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Name), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for label content id value
                if (string.IsNullOrEmpty(item.ContentId))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[7];
                    SecurityRolesGridView.CurrentCell = SecurityRolesGridView.Rows[idx].Cells[2];
                    SecurityRolesGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show("Label Content ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for admin security right id value
                if (string.IsNullOrEmpty(item.AdminSecurityRightId))
                {
                    TenantConfigTabControl.SelectedTab = TenantConfigTabControl.TabPages[7];
                    SecurityRolesGridView.CurrentCell = SecurityRolesGridView.Rows[idx].Cells[5];
                    SecurityRolesGridView.Rows[idx].Cells[5].Selected = true;
                    MessageBox.Show("Admin Security Right ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (!IsValidIOC())
            {
                return false;
            }

            if (!IsValidLocalization())
            {
                return false;
            }

            if (!IsValidModelDefinitions())
            {
                return false;
            }

            if (!IsValidMenus())
            {
                return false;
            }

            if (!IsValidServices())
            {
                return false;
            }

            if (!IsValidSecurtyRoles())
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
            htmlBlockBindingSource.DataSource = typeof(LocaleConfigurationHtmlBlockModel);
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
                htmlBlockBindingSource.DataSource = locale.LocaleHtmlBlocks;
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

            //IOC Configuration
            IocTextBox.Text = tenantConfig.Modules[0].IocConfigurationString;

            //Localization
            LocaleDropdown.DataSource = tenantConfig.Modules[0].LocalizationConfiguration.Locales;
            LocaleDropdown.DisplayMember = "Name";
            LocaleDropdown.ValueMember = "LocaleId";

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

        private void ShowDataListItems(string id)
        {
            object selectedLocaleId = LocaleDropdown.SelectedValue;
            string localeId = selectedLocaleId.ToString();
            if (selectedLocaleId != null && selectedLocaleId is LocaleConfigurationModel)
            {
                localeId = ((LocaleConfigurationModel)selectedLocaleId).LocaleId;
            }
            LocaleConfigurationModel locale = _tenantConfig.Modules[0].LocalizationConfiguration.Locales.Find(i => i.LocaleId == localeId);
            LocaleConfigurationDataListModel dataList = locale.LocaleDataLists.Find(i => i.Id == id);

            DataListItemsForm form = new DataListItemsForm()
            {
                BusinessModule = _businessModuleName,
                DataList = dataList,
                ShowIds = ShowIdsCheckBox.Checked
            };
            form.ShowDialog();
            if (form.HasDataChanged)
            {
                ToggleDirtyData(true);
            }
            form.Dispose();
        }

        private void ShowEmailTemplateEditor(string id)
        {
            object selectedLocaleId = LocaleDropdown.SelectedValue;
            string localeId = selectedLocaleId.ToString();
            if (selectedLocaleId != null && selectedLocaleId is LocaleConfigurationModel)
            {
                localeId = ((LocaleConfigurationModel)selectedLocaleId).LocaleId;
            }
            LocaleConfigurationModel locale = _tenantConfig.Modules[0].LocalizationConfiguration.Locales.Find(i => i.LocaleId == localeId);
            LocaleConfigurationEmailTemplateModel emailTemplate = locale.LocaleEmailTemplates.Find(i => i.Id == id);

            XmlEditorForm form = new XmlEditorForm()
            {
                ContentId = emailTemplate.ContentId,
                EditorText = emailTemplate.BodyString
            };
            form.ShowDialog();
            if (form.HasDataChanged)
            {
                emailTemplate.BodyString = form.EditorText;
                ToggleDirtyData(true);
            }
            form.Dispose();
        }

        private void ShowEmailTemplateAddresses(string id)
        {
            object selectedLocaleId = LocaleDropdown.SelectedValue;
            string localeId = selectedLocaleId.ToString();
            if (selectedLocaleId != null && selectedLocaleId is LocaleConfigurationModel)
            {
                localeId = ((LocaleConfigurationModel)selectedLocaleId).LocaleId;
            }
            LocaleConfigurationModel locale = _tenantConfig.Modules[0].LocalizationConfiguration.Locales.Find(i => i.LocaleId == localeId);
            LocaleConfigurationEmailTemplateModel emailTemplate = locale.LocaleEmailTemplates.Find(i => i.Id == id);

            EmailTemplateAddressForm form = new EmailTemplateAddressForm()
            {
                BusinessModule = _businessModuleName,
                EmailTemplate = emailTemplate,
                ShowIds = ShowIdsCheckBox.Checked
            };
            form.ShowDialog();
            if (form.HasDataChanged)
            {
                ToggleDirtyData(true);
            }
            form.Dispose();
        }

        private void ShowHtmlBlockEditor(string id)
        {
            object selectedLocaleId = LocaleDropdown.SelectedValue;
            string localeId = selectedLocaleId.ToString();
            if (selectedLocaleId != null && selectedLocaleId is LocaleConfigurationModel)
            {
                localeId = ((LocaleConfigurationModel)selectedLocaleId).LocaleId;
            }
            LocaleConfigurationModel locale = _tenantConfig.Modules[0].LocalizationConfiguration.Locales.Find(i => i.LocaleId == localeId);
            LocaleConfigurationHtmlBlockModel htmlBlock = locale.LocaleHtmlBlocks.Find(i => i.Id == id);

            XmlEditorForm form = new XmlEditorForm()
            {
                ContentId = htmlBlock.ContentId,
                EditorText = htmlBlock.HtmlString
            };
            form.ShowDialog();
            if (form.HasDataChanged)
            {
                htmlBlock.HtmlString = form.EditorText;
                ToggleDirtyData(true);
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

                        locale.LocaleHtmlBlocks.Sort(
                            delegate(LocaleConfigurationHtmlBlockModel i1, LocaleConfigurationHtmlBlockModel i2) { return string.Compare(i1.ContentId, i2.ContentId); }
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

            EmailTemplatesGridView.Columns[0].Visible = showIds;

            HtmlBlocksGridView.Columns[0].Visible = showIds;

            LabelsGridView.Columns[0].Visible = showIds;

            MessagesGridView.Columns[0].Visible = showIds;

            ModelDefinitionsGridView.Columns[0].Visible = showIds;
            
            MenusGridView.Columns[0].Visible = showIds;

            ServicesGridView.Columns[0].Visible = showIds;

            SecurityRolesGridView.Columns[0].Visible = showIds;
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
