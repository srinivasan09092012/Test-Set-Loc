using HP.HSP.UA3.Core.UX.Common;
using HP.HSP.UA3.Core.UX.Common.Utilities;
using HP.HSP.UA3.Core.UX.Data.Configuration;
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
    public partial class EmailTemplateAddressForm : Form
    {
        public string BusinessModule = string.Empty;
        public LocaleConfigurationEmailTemplateModel EmailTemplate = null;
        public bool HasDataChanged = false;
        public bool ShowIds = false;

        private bool _isDataDrity = false;
        private List<LocaleConfigurationEmailTemplateAddressModel> _addresses = null;

        public EmailTemplateAddressForm()
        {
            InitializeComponent();
        }

        private void EmailTemplateAddressFormLoad(object sender, EventArgs e)
        {

        }

        private void EmailTemplateAddressForm_Shown(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
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

        private void EmailTemplateAddressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (_isDataDrity && this.Visible)
                {
                    DialogResult result =
                        MessageBox.Show("Would you like to save your changes before exiting?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (!IsDataSaved())
                        {
                            e.Cancel = true;
                        }
                    }
                    else if (result == System.Windows.Forms.DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }

                    if (!e.Cancel)
                    {
                        e.Cancel = true;
                        this.Hide();
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

        private void AddressItemsGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == AddressItemsGridView.NewRowIndex)
            {
                if (AddressItemsGridView.CurrentCell.EditType == typeof(DataGridViewTextBoxEditingControl))
                {
                    AddressItemsGridView.BeginEdit(false);
                    TextBox textBox = (TextBox)AddressItemsGridView.EditingControl;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void AddressItemsGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
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

        private void AddressItemsGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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

        private void emailTemplateAddressBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            ToggleDirtyData(true);
        }

        private void ShowIdsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ToggleShowIds(ShowIdsCheckBox.Checked);
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

        private void CancelButton_Click(object sender, EventArgs e)
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

                DialogResult result =
                    MessageBox.Show("Resetting the configuration back to its last saved state will lose all unsaved pending changes.\n\nDo you wish to perform the reset?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    InitializeForm();
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

        private bool ConfirmDeleteRow(string name)
        {
            DialogResult result =
                MessageBox.Show(string.Format("Do you wish to delete the row '{0}'?", name), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return (result == System.Windows.Forms.DialogResult.Yes);
        }

        private void InitializeForm()
        {
            InitializeDataGrid();
            ShowIdsCheckBox.Checked = this.ShowIds;
            LoadAddressItems();
            ToggleDirtyData(false);
            AddressItemsGridView.Focus();
        }

        private void InitializeDataGrid()
        {
            ((DataGridViewComboBoxColumn)AddressItemsGridView.Columns[1]).DataSource = Enum.GetValues(typeof(CoreEnumerations.Notifications.AddressType));
        }

        private bool IsDataSaved()
        {
            bool isSaved = false;

            if (IsValidTemplateAddresses())
            {
                this.EmailTemplate.Addresses = _addresses;
                isSaved = true;
                ToggleDirtyData(false);
                this.HasDataChanged = true;
                this.Close();
            }

            return isSaved;
        }

        private bool IsValidTemplateAddresses()
        {
            int idx = 0;
            foreach (LocaleConfigurationEmailTemplateAddressModel item in _addresses)
            {
                //Check for ID value
                if (string.IsNullOrEmpty(item.Id))
                {
                    AddressItemsGridView.CurrentCell = AddressItemsGridView.Rows[idx].Cells[0];
                    AddressItemsGridView.Rows[idx].Cells[0].Selected = true;
                    ShowIdsCheckBox.Checked = true;
                    MessageBox.Show("ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique ID value
                if (_addresses.FindAll(i => string.Compare(i.Id, item.Id, true) == 0).Count > 1)
                {
                    AddressItemsGridView.CurrentCell = AddressItemsGridView.Rows[idx].Cells[0];
                    AddressItemsGridView.Rows[idx].Cells[0].Selected = true;
                    ShowIdsCheckBox.Checked = true;
                    MessageBox.Show(string.Format("ID must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Id), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for no more than 1 from type
                if (item.Type == CoreEnumerations.Notifications.AddressType.From)
                {
                    if (_addresses.FindAll(i => i.Type == item.Type).Count > 1)
                    {
                        AddressItemsGridView.CurrentCell = AddressItemsGridView.Rows[idx].Cells[1];
                        MessageBox.Show(string.Format("Only 1 from address is allowed."), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                //Check for display name
                if (string.IsNullOrEmpty(item.DisplayName))
                {
                    AddressItemsGridView.CurrentCell = AddressItemsGridView.Rows[idx].Cells[2];
                    AddressItemsGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show("Display Name is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for email address
                if (string.IsNullOrEmpty(item.EmailAddress))
                {
                    AddressItemsGridView.CurrentCell = AddressItemsGridView.Rows[idx].Cells[3];
                    AddressItemsGridView.Rows[idx].Cells[3].Selected = true;
                    MessageBox.Show("Email Address is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for valid email address
                if (!StringValidator.IsValidEmailAddress(item.EmailAddress))
                {
                    AddressItemsGridView.CurrentCell = AddressItemsGridView.Rows[idx].Cells[3];
                    AddressItemsGridView.Rows[idx].Cells[3].Selected = true;
                    MessageBox.Show("Email Address is not in a valid format.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                idx++;
            }

            return true;
        }

        private void LoadAddressItems()
        {
            TemplateTextBox.Text = this.EmailTemplate.ContentId;
            if (this.EmailTemplate.Addresses == null)
            {
                this.EmailTemplate.Addresses = new List<LocaleConfigurationEmailTemplateAddressModel>();
            }
            _addresses = Serializer.Clone<List<LocaleConfigurationEmailTemplateAddressModel>>(this.EmailTemplate.Addresses);
            emailTemplateAddressBindingSource.DataSource = _addresses;
        }

        private void ToggleDirtyData(bool enabled)
        {
            _isDataDrity = enabled;
            ResetButton.Enabled = enabled;
            SaveButton.Enabled = enabled;
        }

        private void ToggleShowIds(bool showIds)
        {
            AddressItemsGridView.Columns[0].Visible = showIds;
        }
    }
}
