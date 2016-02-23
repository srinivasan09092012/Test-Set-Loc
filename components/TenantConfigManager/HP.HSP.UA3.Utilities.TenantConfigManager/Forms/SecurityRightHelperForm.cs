using HP.HSP.UA3.Core.UX.Common;
using HP.HSP.UA3.Core.UX.Common.Utilities;
using HP.HSP.UA3.Core.UX.Data.Configuration;
using HP.HSP.UA3.Core.UX.Data.Security;
using HP.HSP.UA3.Utilities.TenantConfigManager.Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    public partial class SecurityRightHelperForm : Form
    {
        public string RightId = string.Empty;
        public string RightName = string.Empty;
        public string RightNamePrefix = string.Empty;
        public ModuleConfigurationModel ModuleConfig = null;
        public bool HasDataChanged = false;
        public SecurityRightModel.RightType RightType = SecurityRightModel.RightType.Other;
        public bool ShowIds = false;

        private List<SecurityRightHelperModel> _rightHelpers = new List<SecurityRightHelperModel>();
        private bool _isDataDrity = false;
        
        public SecurityRightHelperForm()
        {
            InitializeComponent();
        }

        private void SecurityRightsHelperForm_Load(object sender, EventArgs e)
        {

        }

        private void SecurityRightsHelperForm_Shown(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                SecurityRightIdTextBox.Text = this.RightId;
                SecurityRightNameTextBox.Text = this.RightName;
                if (this.RightName.Length == 0)
                {
                    SecurityRightNameTextBox.Text = RightNamePrefix;
                }

                if (SecurityRightIdTextBox.Text.Length > 0 && SecurityRightNameTextBox.Text != RightNamePrefix)
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

        private void SecurityRightsHelperForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void rightsBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            ToggleDirtyData(true);
        }

        private void RightsGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            ToggleDirtyData(true);
            if (e.RowIndex == RightsGridView.NewRowIndex)
            {
                if (RightsGridView.CurrentCell.EditType == typeof(DataGridViewTextBoxEditingControl))
                {
                    RightsGridView.BeginEdit(false);
                    TextBox textBox = (TextBox)RightsGridView.EditingControl;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void RightsGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
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

        private void SecurityRightNameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                RightsGridView.Enabled = false;
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

        private void EditButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                SecurityRightNameTextBox.Text = SecurityRightNameTextBox.Text.Trim();

                if (!SecurityRightNameTextBox.Text.ToLower().StartsWith(RightNamePrefix.ToLower()))
                {
                    SecurityRightNameTextBox.Focus();
                    throw new Exception(string.Format("Name must start with the prefix '{0}'", RightNamePrefix));
                }
                else if (SecurityRightNameTextBox.Text.ToLower() == RightNamePrefix.ToLower())
                {
                    SecurityRightNameTextBox.Focus();
                    throw new Exception(string.Format("You must enter a Name.", RightNamePrefix));
                }
                InitializeForm();
                ToggleDirtyData(true);
                RightsGridView.Enabled = true;
                RightsGridView.Focus();
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

        //private void ShowIdsCheckBox_CheckedChanged(object sender, EventArgs e)
        //{
        //    ToggleShowIds(ShowIdsCheckBox.Checked);
        //}

        private bool ConfirmDeleteRow(string name)
        {
            DialogResult result =
                MessageBox.Show(string.Format("Do you wish to delete the row '{0}'?", name), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return (result == System.Windows.Forms.DialogResult.Yes);
        }

        private void InitializeForm()
        {
            //ShowIdsCheckBox.Checked = this.ShowIds;
            LoadRight();
            ToggleDirtyData(false);
            RightsGridView.Enabled = true;
            RightsGridView.Focus();
        }

        private bool IsValidRights()
        {
            int idx = 0;
            foreach (SecurityRightHelperModel item in _rightHelpers)
            {
                //Nothing to validate since we just have a checkbox
                idx++;
            }

            return true;
        }

        private void LoadRight()
        {
            _rightHelpers = new List<SecurityRightHelperModel>();
            foreach (SecurityRoleModel securityRole in this.ModuleConfig.SecurityRoles)
            {
                if (securityRole.Functions != null && securityRole.Functions.Count > 0)
                {

                    foreach (SecurityFunctionModel securityFunction in securityRole.Functions)
                    {
                        SecurityRightHelperModel rightHelper = new SecurityRightHelperModel()
                        {
                            RoleId = securityRole.Id,
                            RoleName = securityRole.Name,
                            FunctionId = securityFunction.Id,
                            FunctionName = securityFunction.Name,
                            isIncluded = false
                        };

                        SecurityRightModel securityRight = securityFunction.Rights.Find(sr => sr.Id == SecurityRightIdTextBox.Text);
                        if (securityRight != null)
                        {
                            rightHelper.isIncluded = true;
                            SecurityRightNameTextBox.Text = securityRight.Name;
                        }
                        else
                        {
                            securityRight = securityFunction.Rights.Find(sr => string.Compare(sr.Name, SecurityRightNameTextBox.Text, true) == 0);
                            if (securityRight != null)
                            {
                                rightHelper.isIncluded = true;
                                SecurityRightIdTextBox.Text = securityRight.Id;
                            }
                        }

                    _rightHelpers.Add(rightHelper);
                    }
                }
            }

            rightsBindingSource.DataSource = _rightHelpers;
        }

        private bool IsDataSaved()
        {
            bool isSaved = false;

            if (IsValidRights())
            {
                if (string.IsNullOrEmpty(SecurityRightIdTextBox.Text))
                {
                    SecurityRightIdTextBox.Text = Common.Utilities.GenerateNewID();
                }
                string id = SecurityRightIdTextBox.Text;
                bool isInUse = false;
                foreach (SecurityRightHelperModel rightHelper in _rightHelpers)
                {
                    SecurityRoleModel securityRole = this.ModuleConfig.SecurityRoles.Find(sr => sr.Id == rightHelper.RoleId);
                    if (securityRole != null)
                    {
                        SecurityFunctionModel securityFunction = securityRole.Functions.Find(sf => sf.Id == rightHelper.FunctionId);
                        if (securityRole != null)
                        {
                            SecurityRightModel securityRight = securityFunction.Rights.Find(sr => sr.Id == SecurityRightIdTextBox.Text);
                            if (rightHelper.isIncluded)
                            {
                                isInUse = true;
                                if (securityRight == null)
                                {
                                    securityFunction.Rights.Add(
                                        new SecurityRightModel()
                                        {
                                            Id = id,
                                            IsActive = true,
                                            Name = SecurityRightNameTextBox.Text,
                                            Type = this.RightType
                                        });
                                }
                            }
                            else
                            {
                                if (securityRight != null)
                                {
                                    securityFunction.Rights.Remove(securityRight);
                                }
                            }
                        }
                    }
                }
                isSaved = true;
                ToggleDirtyData(false);
                if (isInUse)
                {
                    this.RightId = SecurityRightIdTextBox.Text;
                }
                else
                {
                    this.RightId = "";
                }
                this.RightName = SecurityRightNameTextBox.Text;
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
            RightsGridView.Columns[0].Visible = showIds;
        }
    }
}
