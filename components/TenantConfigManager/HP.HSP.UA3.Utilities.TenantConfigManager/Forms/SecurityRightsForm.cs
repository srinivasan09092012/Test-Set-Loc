using HP.HSP.UA3.Core.UX.Common;
using HP.HSP.UA3.Core.UX.Common.Utilities;
using HP.HSP.UA3.Core.UX.Data.Configuration;
using HP.HSP.UA3.Core.UX.Data.Security;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    public partial class SecurityRightsForm : Form
    {
        public string BusinessModule = string.Empty;
        public LocalizationConfigurationModel LocalizationConfig = null;
        public SecurityFunctionModel Function = null;
        public bool HasDataChanged = false;
        public bool ShowIds = false;

        private bool _isDataDrity = false;
        private List<SecurityRightModel> _rights = null;

        public SecurityRightsForm()
        {
            InitializeComponent();
        }

        private void SecurityRightsForm_Load(object sender, EventArgs e)
        {

        }

        private void SecurityRightsForm_Shown(object sender, EventArgs e)
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

        private void SecurityRightsForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void SecurityRightsGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == SecurityRightsGridView.NewRowIndex)
            {
                if (SecurityRightsGridView.CurrentCell.EditType == typeof(DataGridViewTextBoxEditingControl))
                {
                    SecurityRightsGridView.BeginEdit(false);
                    TextBox textBox = (TextBox)SecurityRightsGridView.EditingControl;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void SecurityRightsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                switch (e.ColumnIndex)
                {
                    case 3:
                        string id = SecurityRightsGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                        ShowLabelHelper(ref id);
                        SecurityRightsGridView.Rows[e.RowIndex].Cells[3].Value = id;
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

        private void SecurityRightsGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Row.Cells[0].Value = Common.Utilities.GenerateNewID();
                e.Row.Cells[3].Value = this.BusinessModule + ".Label.Security.Rights.";
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

        private void SecurityRightsGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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

        private void securityRightsBindingSource_CurrentCellChanged(object sender, EventArgs e)
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
            ((DataGridViewComboBoxColumn)SecurityRightsGridView.Columns[2]).DataSource = Enum.GetValues(typeof(SecurityRightModel.RightType));

            ShowIdsCheckBox.Checked = this.ShowIds;
            LoadSecurityRights();
            ToggleDirtyData(false);
            SecurityRightsGridView.Focus();
        }

        private bool IsValidSecurityRights()
        {
            int idx = 0;
            foreach (SecurityRightModel item in _rights)
            {
                //Check for ID value
                if (string.IsNullOrEmpty(item.Id))
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    SecurityRightsGridView.CurrentCell = SecurityRightsGridView.Rows[idx].Cells[0];
                    SecurityRightsGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show("ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique ID value
                if (_rights.FindAll(i => string.Compare(i.Id, item.Id, true) == 0).Count > 1)
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    SecurityRightsGridView.CurrentCell = SecurityRightsGridView.Rows[idx].Cells[0];
                    SecurityRightsGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show(string.Format("ID must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Id), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for name
                if (string.IsNullOrEmpty(item.Name))
                {
                    SecurityRightsGridView.CurrentCell = SecurityRightsGridView.Rows[idx].Cells[1];
                    SecurityRightsGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show("Name is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for label content id
                if (string.IsNullOrEmpty(item.ContentId))
                {
                    SecurityRightsGridView.CurrentCell = SecurityRightsGridView.Rows[idx].Cells[3];
                    SecurityRightsGridView.Rows[idx].Cells[3].Selected = true;
                    MessageBox.Show("Label Content ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for proper named label content id
                string prefix = this.BusinessModule + ".Label.Security.Rights.";
                if (!item.ContentId.StartsWith(prefix))
                {
                    SecurityRightsGridView.CurrentCell = SecurityRightsGridView.Rows[idx].Cells[3];
                    SecurityRightsGridView.Rows[idx].Cells[3].Selected = true;
                    MessageBox.Show(string.Format("Label Content ID must start with the prefix '{0}'.", prefix), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                idx++;
            }

            return true;
        }

        private void LoadSecurityRights()
        {
            SecurityFunctionNameTextBox.Text = this.Function.Name;

            if (this.Function.Rights == null)
            {
                this.Function.Rights = new List<SecurityRightModel>();
            }
            _rights = Serializer.Clone<List<SecurityRightModel>>(this.Function.Rights);
            securityRightsBindingSource.DataSource = _rights;
        }

        private bool IsDataSaved()
        {
            bool isSaved = false;

            if (IsValidSecurityRights())
            {
                this.Function.Rights = _rights;
                isSaved = true;
                ToggleDirtyData(false);
                this.HasDataChanged = true;
                this.Close();
            }

            return isSaved;
        }

        private void ShowLabelHelper(ref string id)
        {
            LocaleLabelHelperForm form = new LocaleLabelHelperForm()
            {
                LabelContentId = id,
                LabelContentIdPrefix = string.Format("{0}.Label.Security.Rights.", this.BusinessModule),
                LocalizationConfig = this.LocalizationConfig,
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

        private void ToggleDirtyData(bool enabled)
        {
            _isDataDrity = enabled;
            ResetButton.Enabled = enabled;
            SaveButton.Enabled = enabled;
        }

        private void ToggleShowIds(bool showIds)
        {
            SecurityRightsGridView.Columns[0].Visible = showIds;
        }
    }
}
