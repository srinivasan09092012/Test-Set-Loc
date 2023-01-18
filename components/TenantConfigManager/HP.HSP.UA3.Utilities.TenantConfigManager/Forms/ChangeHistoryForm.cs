using HP.HSP.UA3.Core.UX.Common.Utilities;
using System.Linq;
using HP.HSP.UA3.Core.UX.Data.Configuration;
using HP.HSP.UA3.Utilities.TenantConfigManager.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    public partial class ChangeHistoryForm : Form
    {
        public bool ShowIds = false;
        private List<ConfigurationChange> _dataListItems = null;

        protected List<TenantConfigurationModel> Original { get; private set; }
        protected List<TenantConfigurationModel> Changed { get; private set; }
        protected List<ConfigurationChange> ChangeSet { get; private set; }

        public ChangeHistoryForm(List<TenantConfigurationModel> originalConfigurations, List<TenantConfigurationModel> changedConfigurations)
        {
            InitializeComponent();
            this.Original = originalConfigurations;
            this.Changed = changedConfigurations;
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
        }

        private void DataListItemsGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataListItemsGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
        }

        private void dataListItemBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
        }

        private void DataListItemsGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
        }

        private void ShowIdsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ToggleShowIds(ShowIdsCheckBox.Checked);
        }

        private void InitializeForm()
        {
            ShowIdsCheckBox.Checked = this.ShowIds;
            LoadDataList();
            DataListItemsGridView.Focus();
        }

        private void LoadDataList()
        {
            this.ChangeSet = new TenantConfigurationChangeDetector().GetChanges(this.Original, this.Changed);
            this.DataListItemsGridView.DataSource = this.ChangeSet;
        }

        private void ToggleShowIds(bool showIds)
        {
            DataListItemsGridView.Columns[0].Visible = showIds;
            DataListItemsGridView.Columns[1].Visible = showIds;
        }

        private void dataListItemBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void ShowSavedChangesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void DataListItemsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                var change = this.DataListItemsGridView.Rows[e.RowIndex].DataBoundItem as ConfigurationChange;
                using(var form = ConfigurationChangeFormFactory.Create(change))
                {
                    form.ShowDialog();
                }
            }
        }
    }
}
