using HP.HSP.UA3.Core.UX.Common.Utilities;
using HP.HSP.UA3.Core.UX.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    public partial class DataListItemsForm : Form
    {
        public string BusinessModule = string.Empty;
        public LocaleConfigurationDataListModel DataList = null;
        public bool HasDataChanged = false;
        public bool ShowIds = false;

        private bool _isDataDrity = false;
        private List<LocaleConfigurationDataListItemModel> _dataListItems = null;

        public DataListItemsForm()
        {
            InitializeComponent();
        }

        private void DataListItemsForm_Load(object sender, EventArgs e)
        {

        }

        private void DataListItemsForm_Shown(object sender, EventArgs e)
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

        private void DataListItemsForm_FormClosing(object sender, FormClosingEventArgs e)
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
                        if(!IsDataSaved())
                        {
                            e.Cancel = true;
                        }
                    }
                    else if (result == System.Windows.Forms.DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }

                    if(!e.Cancel)
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

        private void DataListItemsGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == DataListItemsGridView.NewRowIndex)
            {
                if (DataListItemsGridView.CurrentCell.EditType == typeof(DataGridViewTextBoxEditingControl))
                {
                    DataListItemsGridView.BeginEdit(false);
                    TextBox textBox = (TextBox)DataListItemsGridView.EditingControl;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void DataListItemsGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Row.Cells[0].Value = Guid.NewGuid().ToString("D").ToUpper();
                e.Row.Cells[1].Value = this.DataList.LocaleId;
                e.Row.Cells[2].Value = this.DataList.ContentId + ".";
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

        private void dataListItemBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            ToggleDirtyData(true);
        }

        private void DataListItemsGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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

        private void ShowIdsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ToggleShowIds(ShowIdsCheckBox.Checked);
        }

        private bool ConfirmDeleteRow(string name)
        {
            DialogResult result =
                MessageBox.Show(string.Format("Do you wish to delete the row '{0}'?", name), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return (result == System.Windows.Forms.DialogResult.Yes);
        }

        private void InitializeForm()
        {
            ShowIdsCheckBox.Checked = this.ShowIds;
            LoadDataList();
            ToggleDirtyData(false);
            DataListItemsGridView.Focus();
        }

        private bool IsValidDataListItems()
        {
            int idx = 0;
            foreach (LocaleConfigurationDataListItemModel item in _dataListItems)
            {
                //Check for ID value
                if (string.IsNullOrEmpty(item.Id))
                {
                    DataListItemsGridView.CurrentCell = DataListItemsGridView.Rows[idx].Cells[0];
                    DataListItemsGridView.Rows[idx].Cells[0].Selected = true;
                    ShowIdsCheckBox.Checked = true;
                    MessageBox.Show("ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique ID value
                if (_dataListItems.FindAll(i => string.Compare(i.Id, item.Id, true) == 0).Count > 1)
                {
                    DataListItemsGridView.CurrentCell = DataListItemsGridView.Rows[idx].Cells[0];
                    DataListItemsGridView.Rows[idx].Cells[0].Selected = true;
                    ShowIdsCheckBox.Checked = true;
                    MessageBox.Show(string.Format("ID must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Id), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for content id
                if (string.IsNullOrEmpty(item.ContentId))
                {
                    DataListItemsGridView.CurrentCell = DataListItemsGridView.Rows[idx].Cells[2];
                    DataListItemsGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show("Content ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for proper named content id
                string prefix = this.BusinessModule + ".DataList.";
                if (!item.ContentId.StartsWith(prefix))
                {
                    DataListItemsGridView.CurrentCell = DataListItemsGridView.Rows[idx].Cells[2];
                    DataListItemsGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show(string.Format("Content ID must start with the prefix '{0}'.", prefix), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique content id
                if (_dataListItems.FindAll(i => string.Compare(i.ContentId, item.ContentId, true) == 0).Count > 1)
                {
                    DataListItemsGridView.CurrentCell = DataListItemsGridView.Rows[idx].Cells[2];
                    DataListItemsGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show(string.Format("Content ID must be a unqiue value. There are more than 1 rows with a content ID value of '{0}'.", item.ContentId), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for value
                if (string.IsNullOrEmpty(item.Value))
                {
                    DataListItemsGridView.CurrentCell = DataListItemsGridView.Rows[idx].Cells[3];
                    DataListItemsGridView.Rows[idx].Cells[3].Selected = true;
                    MessageBox.Show("Value is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique value
                if (_dataListItems.FindAll(i => string.Compare(i.Value, item.Value, true) == 0).Count > 1)
                {
                    DataListItemsGridView.CurrentCell = DataListItemsGridView.Rows[idx].Cells[3];
                    DataListItemsGridView.Rows[idx].Cells[3].Selected = true;
                    MessageBox.Show(string.Format("Value must be a unqiue value. There are more than 1 rows with a value of '{0}'.", item.Value), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for text
                if (string.IsNullOrEmpty(item.Text))
                {
                    DataListItemsGridView.CurrentCell = DataListItemsGridView.Rows[idx].Cells[4];
                    DataListItemsGridView.Rows[idx].Cells[4].Selected = true;
                    MessageBox.Show("Text is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique text
                if (_dataListItems.FindAll(i => string.Compare(i.Text, item.Text, true) == 0).Count > 1)
                {
                    DataListItemsGridView.CurrentCell = DataListItemsGridView.Rows[idx].Cells[4];
                    DataListItemsGridView.Rows[idx].Cells[4].Selected = true;
                    MessageBox.Show(string.Format("Text must be a unqiue value. There are more than 1 rows with a text value of '{0}'.", item.Text), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                 //Check for unique order
                if (_dataListItems.FindAll(i => i.Order == item.Order).Count > 1)
                {
                    DataListItemsGridView.CurrentCell = DataListItemsGridView.Rows[idx].Cells[5];
                    DataListItemsGridView.Rows[idx].Cells[5].Selected = true;
                    MessageBox.Show(string.Format("Order must be a unqiue value. There are more than 1 rows with an order value of '{0}'.", item.Text), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                idx++;
            }

            return true;
        }

        private void LoadDataList()
        {
            DataListTextBox.Text = this.DataList.ContentId;
            if (this.DataList.LocaleDataListItems == null)
            {
                this.DataList.LocaleDataListItems = new List<LocaleConfigurationDataListItemModel>();
            }
            _dataListItems = Serializer.Clone<List<LocaleConfigurationDataListItemModel>>(this.DataList.LocaleDataListItems);
            dataListItemBindingSource.DataSource = _dataListItems;
        }

        private bool IsDataSaved()
        {
            bool isSaved = false;

            if (IsValidDataListItems())
            {
                this.DataList.LocaleDataListItems = _dataListItems;
                isSaved = true;
                ToggleDirtyData(false);
                this.HasDataChanged = true;
                this.Close();
            }

            return isSaved;
        }

        private void ToggleDirtyData(bool enabled)
        {
            _isDataDrity = enabled;
            ResetButton.Enabled = enabled;
            SaveButton.Enabled = enabled;
        }

        private void ToggleShowIds(bool showIds)
        {
            DataListItemsGridView.Columns[0].Visible = showIds;
            DataListItemsGridView.Columns[1].Visible = showIds;
        }
    }
}
