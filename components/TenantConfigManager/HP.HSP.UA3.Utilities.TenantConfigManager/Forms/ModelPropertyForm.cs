using HP.HSP.UA3.Core.UX.Common;
using HP.HSP.UA3.Core.UX.Common.Utilities;
using HP.HSP.UA3.Core.UX.Data.Configuration;
using HP.HSP.UA3.Core.UX.Data.Security;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    public partial class ModelPropertyForm : Form
    {
        public string BusinessModule = string.Empty;
        public ModelDefinitionModel Model = null;
        public List<SecurityRoleModel> Roles = null;
        public LocalizationConfigurationModel LocalizationConfig = null;
        public bool HasDataChanged = false;
        public bool ShowIds = false;

        private bool _isDataDrity = false;
        private List<ModelPropertyModel> _modelProperties = null;

        public ModelPropertyForm()
        {
            InitializeComponent();
        }

        private void ModelPropertyForm_Load(object sender, EventArgs e)
        {

        }

        private void ModelPropertyForm_Shown(object sender, EventArgs e)
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

        private void ModelPropertyForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void ModelPropertiesGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == ModelPropertiesGridView.NewRowIndex)
            {
                if (ModelPropertiesGridView.CurrentCell.EditType == typeof(DataGridViewTextBoxEditingControl))
                {
                    ModelPropertiesGridView.BeginEdit(false);
                    TextBox textBox = (TextBox)ModelPropertiesGridView.EditingControl;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void ModelPropertiesGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void ModelPropertiesGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 13:
                    if (ModelPropertiesGridView.Rows[e.RowIndex].Cells[14].Value == null)
                    {
                        ModelPropertiesGridView.Rows[e.RowIndex].Cells[15].Value = "Add";
                    }
                    else
                    {
                        ModelPropertiesGridView.Rows[e.RowIndex].Cells[15].Value = "Delete";
                    }
                    break;

                case 15:
                    if (ModelPropertiesGridView.Rows[e.RowIndex].Cells[16].Value == null)
                    {
                        ModelPropertiesGridView.Rows[e.RowIndex].Cells[17].Value = "Add";
                    }
                    else
                    {
                        ModelPropertiesGridView.Rows[e.RowIndex].Cells[17].Value = "Delete";
                    }
                    break;
            }
        }

        private void ModelPropertiesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch(e.ColumnIndex)
            {
                case 13:
                    if (ModelPropertiesGridView.Rows[e.RowIndex].Cells[12].Value == null)
                    {
                        //Add view restriction
                        SecurityRightModel securityRight = new SecurityRightModel()
                        {
                            Id = Common.Utilities.GenerateNewID(),
                            Name = "Property.View." + ModelPropertiesGridView.Rows[e.RowIndex].Cells[1].Value.ToString(),
                            ContentId = this.BusinessModule + ".Label.Securty.Rights.Property.View." + ModelPropertiesGridView.Rows[e.RowIndex].Cells[1].Value.ToString(),
                            IsActive = true,
                            Type = SecurityRightModel.RightType.Property
                        };

                        ModelPropertiesGridView.Rows[e.RowIndex].Cells[12].Value = securityRight.Id;
                    }
                    else
                    {
                        //Delete view restriction
                    }
                    break;

                case 15:
                    if (ModelPropertiesGridView.Rows[e.RowIndex].Cells[14].Value == null)
                    {
                        //Add edit restriction
                        SecurityRightModel securityRight = new SecurityRightModel()
                        {
                            Id = Common.Utilities.GenerateNewID(),
                            Name = "Property.Edit." + ModelPropertiesGridView.Rows[e.RowIndex].Cells[1].Value.ToString(),
                            ContentId = this.BusinessModule + ".Label.Securty.Rights.Property.Edit." + ModelPropertiesGridView.Rows[e.RowIndex].Cells[1].Value.ToString(),
                            IsActive = true,
                            Type = SecurityRightModel.RightType.Property
                        };

                        ModelPropertiesGridView.Rows[e.RowIndex].Cells[14].Value = securityRight.Id;
                    }
                    else
                    {
                        //Delete edit restriction
                    }
                    break;
            }
        }

        private void ModelPropertiesGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Row.Cells[0].Value = Common.Utilities.GenerateNewID();
                e.Row.Cells[7].Value = 0;
                e.Row.Cells[10].Value = this.BusinessModule + ".Label.Field.";
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

        private void ModelPropertiesGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            ToggleDirtyData(true);
        }

        private void ModelPropertiesGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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
            ((DataGridViewComboBoxColumn)ModelPropertiesGridView.Columns[2]).DataSource = Enum.GetValues(typeof(System.ComponentModel.DataAnnotations.DataType));
            ((DataGridViewComboBoxColumn)ModelPropertiesGridView.Columns[3]).DataSource = Enum.GetValues(typeof(CoreEnumerations.DataAnnotations.DataRestrictionType));
            ((DataGridViewComboBoxColumn)ModelPropertiesGridView.Columns[4]).DataSource = Enum.GetValues(typeof(CoreEnumerations.DataAnnotations.DataDisplayType));

            ShowIdsCheckBox.Checked = this.ShowIds;
            LoadModelProperties();
            ToggleDirtyData(false);
            ModelPropertiesGridView.Focus();
        }

        private bool IsValidModelPropertyItems()
        {
            int idx = 0;
            foreach (ModelPropertyModel item in _modelProperties)
            {
                //Check for ID value
                if (string.IsNullOrEmpty(item.Id))
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    ModelPropertiesGridView.CurrentCell = ModelPropertiesGridView.Rows[idx].Cells[0];
                    ModelPropertiesGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show("ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique ID value
                if (_modelProperties.FindAll(i => string.Compare(i.Id, item.Id, true) == 0).Count > 1)
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    ModelPropertiesGridView.CurrentCell = ModelPropertiesGridView.Rows[idx].Cells[0];
                    ModelPropertiesGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show(string.Format("ID must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Id), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for name
                if (string.IsNullOrEmpty(item.Name))
                {
                    ModelPropertiesGridView.CurrentCell = ModelPropertiesGridView.Rows[idx].Cells[1];
                    ModelPropertiesGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show("Name is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique name
                if (_modelProperties.FindAll(i => string.Compare(i.Name, item.Name, true) == 0).Count > 1)
                {
                    ModelPropertiesGridView.CurrentCell = ModelPropertiesGridView.Rows[idx].Cells[1];
                    ModelPropertiesGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show(string.Format("Name must be a unqiue value. There are more than 1 rows with a value of '{0}'.", item.Name), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                
                //Check for properly named label content id
                string prefix = this.BusinessModule + ".Label.Field.";
                if (string.IsNullOrEmpty(item.LabelContentId) || !item.LabelContentId.StartsWith(prefix))
                {
                    ModelPropertiesGridView.CurrentCell = ModelPropertiesGridView.Rows[idx].Cells[10];
                    ModelPropertiesGridView.Rows[idx].Cells[10].Selected = true;
                    MessageBox.Show(string.Format("Label Content ID must start with the prefix '{0}'.", prefix), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for default text
                if (string.IsNullOrEmpty(item.DefaultText))
                {
                    ModelPropertiesGridView.CurrentCell = ModelPropertiesGridView.Rows[idx].Cells[11];
                    ModelPropertiesGridView.Rows[idx].Cells[11].Selected = true;
                    MessageBox.Show("Default Text is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique access key
                if (!string.IsNullOrEmpty(item.AccessKey))
                {
                    if (_modelProperties.FindAll(i => string.Compare(i.AccessKey, item.AccessKey, true) == 0).Count > 1)
                    {
                        ModelPropertiesGridView.CurrentCell = ModelPropertiesGridView.Rows[idx].Cells[13];
                        ModelPropertiesGridView.Rows[idx].Cells[13].Selected = true;
                        MessageBox.Show(string.Format("Access Key must be a unqiue value. There are more than 1 rows with an acces key value of '{0}'.", item.AccessKey), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                //Set specifieds
                item.CompareToSpecified = !string.IsNullOrEmpty(item.CompareTo);
                item.CompareToMsgContentIdSpecified = !string.IsNullOrEmpty(item.CompareToMsgContentId);
                item.EditRestrictionSecurityRightIdSpecified = !string.IsNullOrEmpty(item.EditRestrictionSecurityRightId);
                item.ViewRestrictionSecurityRightIdSpecified = !string.IsNullOrEmpty(item.ViewRestrictionSecurityRightId);
            }

            return true;
        }

        private void LoadModelProperties()
        {
            ModelTypeTextBox.Text = this.Model.Type;
            ModelScopeTextBox.Text = this.Model.Scope;
            ModelDisplayTextBox.Text = this.Model.DisplaySize;

            if (this.Model.ModelProperties == null)
            {
                this.Model.ModelProperties = new List<ModelPropertyModel>();
            }
            _modelProperties = Serializer.Clone<List<ModelPropertyModel>>(this.Model.ModelProperties);
            modelPropertiesBindingSource.DataSource = _modelProperties;
        }

        private bool IsDataSaved()
        {
            bool isSaved = false;

            if (IsValidModelPropertyItems())
            {
                this.Model.ModelProperties = _modelProperties;
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
            ModelPropertiesGridView.Columns[0].Visible = showIds;
        }
    }
}
