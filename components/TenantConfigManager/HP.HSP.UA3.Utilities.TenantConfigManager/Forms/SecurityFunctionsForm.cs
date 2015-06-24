using HP.HSP.UA3.Core.UX.Common;
using HP.HSP.UA3.Core.UX.Common.Utilities;
using HP.HSP.UA3.Core.UX.Data.Configuration;
using HP.HSP.UA3.Core.UX.Data.Security;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    public partial class SecurityFunctionsForm : Form
    {
        public string BusinessModule = string.Empty;
        public LocalizationConfigurationModel LocalizationConfig = null;
        public SecurityRoleModel Role = null;
        public bool HasDataChanged = false;
        public bool ShowIds = false;

        private bool _isDataDrity = false;
        private List<SecurityFunctionModel> _functions = null;

        public SecurityFunctionsForm()
        {
            InitializeComponent();
        }

        private void SecurityFunctionForm_Load(object sender, EventArgs e)
        {

        }

        private void SecurityFunctionForm_Shown(object sender, EventArgs e)
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

        private void SecurityFunctionForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void SecurityFunctionsGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Row.Cells[0].Value = Common.Utilities.GenerateNewID();
                e.Row.Cells[2].Value = this.BusinessModule + ".Label.Security.Functions.";
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

        private void SecurityFunctionsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                switch(e.ColumnIndex)
                {
                    case 2:
                        string id = SecurityFunctionsGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                        ShowLabelHelper(ref id);
                        SecurityFunctionsGridView.Rows[e.RowIndex].Cells[2].Value = id;
                        break;

                    case 6:
                        id = SecurityFunctionsGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                        ShowSecurityRights(id);
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

        private void SecurityFunctionsGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == SecurityFunctionsGridView.NewRowIndex)
            {
                if (SecurityFunctionsGridView.CurrentCell.EditType == typeof(DataGridViewTextBoxEditingControl))
                {
                    SecurityFunctionsGridView.BeginEdit(false);
                    TextBox textBox = (TextBox)SecurityFunctionsGridView.EditingControl;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void SecurityFunctionsGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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

        private void securityFunctionsBindingSource_CurrentCellChanged(object sender, EventArgs e)
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
            ShowIdsCheckBox.Checked = this.ShowIds;
            LoadSecurityFunctions();
            ToggleDirtyData(false);
            SecurityFunctionsGridView.Focus();
        }

        private bool IsValidSecurityFunctions()
        {
            int idx = 0;
            foreach (SecurityFunctionModel item in _functions)
            {
                //Check for ID value
                if (string.IsNullOrEmpty(item.Id))
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    SecurityFunctionsGridView.CurrentCell = SecurityFunctionsGridView.Rows[idx].Cells[0];
                    SecurityFunctionsGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show("ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique ID value
                if (_functions.FindAll(i => string.Compare(i.Id, item.Id, true) == 0).Count > 1)
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    SecurityFunctionsGridView.CurrentCell = SecurityFunctionsGridView.Rows[idx].Cells[0];
                    SecurityFunctionsGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show(string.Format("ID must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Id), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for name
                if (string.IsNullOrEmpty(item.Name))
                {
                    SecurityFunctionsGridView.CurrentCell = SecurityFunctionsGridView.Rows[idx].Cells[1];
                    SecurityFunctionsGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show("Name is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique name
                if (_functions.FindAll(i => string.Compare(i.Name, item.Name, true) == 0).Count > 1)
                {
                    SecurityFunctionsGridView.CurrentCell = SecurityFunctionsGridView.Rows[idx].Cells[1];
                    SecurityFunctionsGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show(string.Format("Name must be a unqiue value. There are more than 1 rows with a value of '{0}'.", item.Name), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for label content id
                if (string.IsNullOrEmpty(item.ContentId))
                {
                    SecurityFunctionsGridView.CurrentCell = SecurityFunctionsGridView.Rows[idx].Cells[2];
                    SecurityFunctionsGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show("Label Content ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for proper named label content id
                string prefix = this.BusinessModule + ".Label.Security.Functions.";
                if (!item.ContentId.StartsWith(prefix))
                {
                    SecurityFunctionsGridView.CurrentCell = SecurityFunctionsGridView.Rows[idx].Cells[2];
                    SecurityFunctionsGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show(string.Format("Label Content ID must start with the prefix '{0}'.", prefix), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                idx++;
            }

            return true;
        }

        private void LoadSecurityFunctions()
        {
            SecurityRoleNameTextBox.Text = this.Role.Name;

            if (this.Role.Functions == null)
            {
                this.Role.Functions = new List<SecurityFunctionModel>();
            }
            _functions = Serializer.Clone<List<SecurityFunctionModel>>(this.Role.Functions);
            securityFunctionsBindingSource.DataSource = _functions;
        }

        private bool IsDataSaved()
        {
            bool isSaved = false;

            if (IsValidSecurityFunctions())
            {
                this.Role.Functions = _functions;
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
                LabelContentIdPrefix = string.Format("{0}.Label.Security.Functions.", this.BusinessModule),
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

        private void ShowSecurityRights(string id)
        {
            SecurityFunctionModel function = _functions.Find(i => i.Id == id);

            SecurityRightsForm form = new SecurityRightsForm()
            {
                BusinessModule = this.BusinessModule,
                LocalizationConfig = this.LocalizationConfig,
                Function = function,
                ShowIds = this.ShowIds
            };
            form.ShowDialog();
            if (!_isDataDrity && form.HasDataChanged)
            {
                ToggleDirtyData(true);
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
            SecurityFunctionsGridView.Columns[0].Visible = showIds;
        }
    }
}
