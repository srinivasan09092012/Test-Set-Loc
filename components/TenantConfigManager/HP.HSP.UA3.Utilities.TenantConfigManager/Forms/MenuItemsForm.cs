using HP.HSP.UA3.Core.UX.Common.Utilities;
using HP.HSP.UA3.Core.UX.Data.Navigation;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    public partial class MenuItemsForm : Form
    {
        public string BusinessModule = string.Empty;
        public MenuModel MainMenu = null;
        public MenuItemModel MenuItem = null;
        public bool HasDataChanged = false;
        public bool ShowIds = false;

        private bool _isDataDrity = false;
        private List<MenuItemModel> _menuItems = null;

        public MenuItemsForm()
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
        private void MenuItemsGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == MenuItemsGridView.NewRowIndex)
            {
                if (MenuItemsGridView.CurrentCell.EditType == typeof(DataGridViewTextBoxEditingControl))
                {
                    MenuItemsGridView.BeginEdit(false);
                    TextBox textBox = (TextBox)MenuItemsGridView.EditingControl;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void MenuItemsGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                e.Row.Cells[0].Value = Common.Utilities.GenerateNewID();
                e.Row.Cells[1].Value = MenuItemNameTextBox.Text + ".";
                e.Row.Cells[5].Value = this.BusinessModule + ".Label.Menu.";
                e.Row.Cells[11].Value = this.BusinessModule;
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

        private void MenuItemsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (e.ColumnIndex == 12)
                {
                    string id = MenuItemsGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                    ShowMenuItems(id);
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

        private void MenuItemsGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            ToggleDirtyData(true);
        }

        private void MenuItemsGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if(e.RowIndex != -1)
                {
                    string id = string.Empty;
                    if(MenuItemsGridView.Rows[e.RowIndex].Cells[0].Value != null)
                    {
                        id = MenuItemsGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                        MenuItemModel menuItem = _menuItems.Find(mi => mi.Id == id);
                        object value;
                        switch (e.ColumnIndex)
                        {
                            case 9:
                                value = MenuItemsGridView.Rows[e.RowIndex].Cells[9].Value;
                                if (value != null)
                                {
                                    menuItem.PageHelpContentIdSpecified = !string.IsNullOrEmpty(value.ToString().Trim());
                                }
                                else
                                {
                                    menuItem.PageHelpContentIdSpecified = false;
                                }
                                break;

                            case 10:
                                value = MenuItemsGridView.Rows[e.RowIndex].Cells[10].Value;
                                if (value != null)
                                {
                                    menuItem.MitaHelpContentIdSpecified = !string.IsNullOrEmpty(value.ToString().Trim());
                                }
                                else
                                {
                                    menuItem.MitaHelpContentIdSpecified = false;
                                }
                                break;
                        }
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

        private void MenuItemsGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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
            LoadMenuItems();
            ToggleDirtyData(false);
            MenuItemsGridView.Focus();
        }

        private bool IsDataSaved()
        {
            bool isSaved = false;

            if (IsValidModelPropertyItems())
            {
                if (this.MenuItem != null)
                {
                    this.MenuItem.Items = _menuItems;
                }
                else
                {
                    this.MainMenu.Items = _menuItems;
                }
                isSaved = true;
                ToggleDirtyData(false);
                this.HasDataChanged = true;
                this.Close();
            }

            return isSaved;
        }

        private bool IsValidModelPropertyItems()
        {
            int idx = 0;
            foreach (MenuItemModel item in _menuItems)
            {
                //Check for ID value
                if (string.IsNullOrEmpty(item.Id))
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    MenuItemsGridView.CurrentCell = MenuItemsGridView.Rows[idx].Cells[0];
                    MenuItemsGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show("ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique ID value
                if (_menuItems.FindAll(i => string.Compare(i.Id, item.Id, true) == 0).Count > 1)
                {
                    ShowIdsCheckBox.Checked = true;
                    ToggleShowIds(true);
                    MenuItemsGridView.CurrentCell = MenuItemsGridView.Rows[idx].Cells[0];
                    MenuItemsGridView.Rows[idx].Cells[0].Selected = true;
                    MessageBox.Show(string.Format("ID must be a unqiue value. There are more than 1 rows with a name value of '{0}'.", item.Id), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for name
                if (string.IsNullOrEmpty(item.Name))
                {
                    MenuItemsGridView.CurrentCell = MenuItemsGridView.Rows[idx].Cells[1];
                    MenuItemsGridView.Rows[idx].Cells[1].Selected = true;
                    MessageBox.Show("Name is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique order
                if (_menuItems.FindAll(i => i.OrderIndex == item.OrderIndex).Count > 1)
                {
                    MenuItemsGridView.CurrentCell = MenuItemsGridView.Rows[idx].Cells[2];
                    MenuItemsGridView.Rows[idx].Cells[2].Selected = true;
                    MessageBox.Show(string.Format("Order must be a unqiue value. There are more than 1 rows with a value of '{0}'.", item.Name), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for label content id value
                if (string.IsNullOrEmpty(item.LabelContentId))
                {
                    MenuItemsGridView.CurrentCell = MenuItemsGridView.Rows[idx].Cells[5];
                    MenuItemsGridView.Rows[idx].Cells[5].Selected = true;
                    MessageBox.Show("Label Content ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for proper named label content id
                string prefix = this.BusinessModule + ".Label.Menu.";
                string altPrefix = "Core.Label.Menu.";
                if (!item.LabelContentId.StartsWith(prefix) && !item.LabelContentId.StartsWith(altPrefix))
                {
                    MenuItemsGridView.CurrentCell = MenuItemsGridView.Rows[idx].Cells[5];
                    MenuItemsGridView.Rows[idx].Cells[5].Selected = true;
                    if (prefix != altPrefix)
                    {
                        MessageBox.Show(string.Format("Label Content ID must start with the prefix '{0}'.", prefix), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Label Content ID must start with the prefix '{0}' or alt prefix '{1}'.", prefix, altPrefix), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;
                }

                //Check for default text value
                if (string.IsNullOrEmpty(item.DefaultText))
                {
                    MenuItemsGridView.CurrentCell = MenuItemsGridView.Rows[idx].Cells[6];
                    MenuItemsGridView.Rows[idx].Cells[6].Selected = true;
                    MessageBox.Show("Default Text is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for proper named page help content id
                prefix = this.BusinessModule + ".HtmlBlock.Page.Help.";
                altPrefix = "Core.HtmlBlock.Page.Help.";
                if (item.PageHelpContentIdSpecified && !item.PageHelpContentId.StartsWith(prefix) && !item.PageHelpContentId.StartsWith(altPrefix))
                {
                    MenuItemsGridView.CurrentCell = MenuItemsGridView.Rows[idx].Cells[9];
                    MenuItemsGridView.Rows[idx].Cells[9].Selected = true;
                    if (prefix != altPrefix)
                    {
                        MessageBox.Show(string.Format("Page Help Content ID must start with the prefix '{0}'.", prefix), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Page Help Content ID must start with the prefix '{0}' or alt prefix '{1}'.", prefix, altPrefix), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;
                }

                //Check for proper named mita help content id
                prefix = this.BusinessModule + ".HtmlBlock.MITA.Help.";
                altPrefix = "Core.HtmlBlock.MITA.Help.";
                if (item.MitaHelpContentIdSpecified && !item.MitaHelpContentId.StartsWith(prefix) && !item.MitaHelpContentId.StartsWith(altPrefix))
                {
                    MenuItemsGridView.CurrentCell = MenuItemsGridView.Rows[idx].Cells[10];
                    MenuItemsGridView.Rows[idx].Cells[10].Selected = true;
                    if (prefix != altPrefix)
                    {
                        MessageBox.Show(string.Format("MITA Help Content ID must start with the prefix '{0}'.", prefix), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("MITA Help Content ID must start with the prefix '{0}' or alt prefix '{1}'.", prefix, altPrefix), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;
                }
                idx++;
            }

            return true;
        }

        private void LoadMenuItems()
        {
            if(this.MenuItem != null)
            {
                MenuItemNameTextBox.Text = this.MenuItem.Name;
                if (this.MenuItem.Items == null)
                {
                    this.MenuItem.Items = new List<MenuItemModel>();
                }
                _menuItems = Serializer.Clone<List<MenuItemModel>>(this.MenuItem.Items);
            }
            else
            {
                MenuItemNameTextBox.Text = this.MainMenu.Name;
                if (this.MainMenu.Items == null)
                {
                    this.MainMenu.Items = new List<MenuItemModel>();
                }
                _menuItems = Serializer.Clone<List<MenuItemModel>>(this.MainMenu.Items);
            }
            MenuItemDisplayTextBox.Text = this.MainMenu.DisplaySize;

            menuItemsBindingSource.DataSource = _menuItems;
        }

        private void ShowMenuItems(string id)
        {
            MenuItemModel menuItem = null;

            if (this.MenuItem != null)
            {
                menuItem = _menuItems.Find(i => i.Id == id);
            }
            else
            {
                menuItem = _menuItems.Find(i => i.Id == id);
            }

            MenuItemsForm form = new MenuItemsForm()
            {
                BusinessModule = this.BusinessModule,
                MainMenu = this.MainMenu,
                MenuItem = menuItem,
                ShowIds = this.ShowIds
            };
            form.ShowDialog();
            if(form.HasDataChanged)
            {
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
            MenuItemsGridView.Columns[0].Visible = showIds;
        }
    }
}
