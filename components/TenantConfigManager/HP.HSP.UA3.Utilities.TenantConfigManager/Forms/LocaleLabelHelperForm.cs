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
    public partial class LocaleLabelHelperForm : Form
    {
        public string LabelContentId = string.Empty;
        public string LabelContentIdPrefix = string.Empty;
        public LocalizationConfigurationModel LocalizationConfig = null;
        public bool HasDataChanged = false;
        public bool ShowIds = false;

        private List<LocaleLabelHelperModel> _labelHelpers = new List<LocaleLabelHelperModel>();
        private bool _isDataDrity = false;

        public LocaleLabelHelperForm()
        {
            InitializeComponent();
        }

        private void LocaleLabelHelperForm_Load(object sender, EventArgs e)
        {

        }

        private void LocaleLabelHelperForm_Shown(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                LabelContentIdTextBox.Text = this.LabelContentId;
                if(this.LabelContentId.Length == 0)
                {
                    LabelContentIdTextBox.Text = LabelContentIdPrefix;
                }
                else if (LabelContentIdTextBox.Text != LabelContentIdPrefix)
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

        private void LocaleLabelHelperForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void labelsBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            ToggleDirtyData(true);
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

        private void LabelContentIdTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                LabelsGridView.Enabled = false;
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

                LabelContentIdTextBox.Text = LabelContentIdTextBox.Text.Trim();

                if (!LabelContentIdTextBox.Text.ToLower().StartsWith(LabelContentIdPrefix.ToLower()))
                {
                    LabelContentIdTextBox.Focus();
                    throw new Exception(string.Format("Content ID must start with the prefix '{0}'", LabelContentIdPrefix));
                }
                else if (LabelContentIdTextBox.Text.ToLower() == LabelContentIdPrefix.ToLower())
                {
                    LabelContentIdTextBox.Focus();
                    throw new Exception(string.Format("You must enter a Content ID.", LabelContentIdPrefix));
                }
                InitializeForm();
                ToggleDirtyData(true);
                LabelsGridView.Enabled = true;
                LabelsGridView.Focus();
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
            LoadLabel();
            ToggleDirtyData(false);
            LabelsGridView.Enabled = true;
            LabelsGridView.Focus();
        }

        private bool IsValidLabels()
        {
            int idx = 0;
            foreach (LocaleLabelHelperModel item in _labelHelpers)
            {
                //Check for text
                if (string.IsNullOrEmpty(item.Text))
                {
                    LabelsGridView.CurrentCell = LabelsGridView.Rows[idx].Cells[2];
                    LabelsGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show("Text is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                idx++;
            }

            return true;
        }

        private void LoadLabel()
        {
            _labelHelpers = new List<LocaleLabelHelperModel>();
            foreach(LocaleConfigurationModel localeConfig in this.LocalizationConfig.Locales)
            {
                LocaleLabelHelperModel labelHelper = new LocaleLabelHelperModel()
                {
                    LocaleId = localeConfig.LocaleId
                };

                LocaleConfigurationLabelModel label = localeConfig.LocaleLabels.Find(l => string.Compare(l.ContentId, LabelContentIdTextBox.Text, true) == 0);
                if(label != null)
                {
                    labelHelper.LocaleLabelId = label.Id;
                    labelHelper.Text = label.Text;
                    labelHelper.Tooltip = label.Tooltip;
                }
                _labelHelpers.Add(labelHelper);
            }

            labelsBindingSource.DataSource = _labelHelpers;
        }

        private bool IsDataSaved()
        {
            bool isSaved = false;

            if (IsValidLabels())
            {
                foreach(LocaleLabelHelperModel labelHelper in _labelHelpers)
                {
                    LocaleConfigurationModel localeConfig = this.LocalizationConfig.Locales.Find(l => l.LocaleId == labelHelper.LocaleId);
                    if(localeConfig != null)
                    {
                        LocaleConfigurationLabelModel label = localeConfig.LocaleLabels.Find(l => string.Compare(l.ContentId, LabelContentIdTextBox.Text, true) == 0);
                        if (label != null)
                        {
                            label.Text = labelHelper.Text;
                            label.Tooltip = labelHelper.Tooltip;
                        }
                        else
                        {
                            label = new LocaleConfigurationLabelModel()
                            {
                                Id = Common.Utilities.GenerateNewID(),
                                LocaleId = labelHelper.LocaleId,
                                ContentId = LabelContentIdTextBox.Text,
                                Text = labelHelper.Text,
                                Tooltip = labelHelper.Tooltip
                            };
                            localeConfig.LocaleLabels.Add(label);
                        }
                    }
                }
                isSaved = true;
                ToggleDirtyData(false);
                this.LabelContentId = LabelContentIdTextBox.Text;
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
            LabelsGridView.Columns[0].Visible = showIds;
        }
    }
}
