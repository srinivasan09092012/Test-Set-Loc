﻿using HP.HSP.UA3.Core.UX.Common.Utilities;
using HP.HSP.UA3.Core.UX.Data.Navigation;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    public partial class MenuItemsForm : Form
    {
        public string BusinessModule = string.Empty;
        public MenuModel Menu = null;
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
                e.Row.Cells[0].Value = Guid.NewGuid().ToString("D").ToUpper();
                e.Row.Cells[5].Value = this.BusinessModule + ".Label.Menu.";
                e.Row.Cells[9].Value = this.BusinessModule;
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
                if (e.ColumnIndex == 10)
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
                    this.Menu.Items = _menuItems;
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
                    MenuItemsGridView.CurrentCell = MenuItemsGridView.Rows[idx].Cells[0];
                    MenuItemsGridView.Rows[idx].Cells[0].Selected = true;
                    ShowIdsCheckBox.Checked = true;
                    MessageBox.Show("ID is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Check for unique ID value
                if (_menuItems.FindAll(i => string.Compare(i.Id, item.Id, true) == 0).Count > 1)
                {
                    MenuItemsGridView.CurrentCell = MenuItemsGridView.Rows[idx].Cells[0];
                    MenuItemsGridView.Rows[idx].Cells[0].Selected = true;
                    ShowIdsCheckBox.Checked = true;
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

                //Check for base url value
                if (string.IsNullOrEmpty(item.BaseUrl))
                {
                    MenuItemsGridView.CurrentCell = MenuItemsGridView.Rows[idx].Cells[4];
                    MenuItemsGridView.Rows[idx].Cells[4].Selected = true;
                    MessageBox.Show("Base URL is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (!item.LabelContentId.StartsWith(prefix))
                {
                    MenuItemsGridView.CurrentCell = MenuItemsGridView.Rows[idx].Cells[5];
                    MenuItemsGridView.Rows[idx].Cells[5].Selected = true;
                    MessageBox.Show(string.Format("Label Content ID must start with the prefix '{0}'.", prefix), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MenuItemNameTextBox.Text = this.Menu.Name;
                if (this.Menu.Items == null)
                {
                    this.Menu.Items = new List<MenuItemModel>();
                }
                _menuItems = Serializer.Clone<List<MenuItemModel>>(this.Menu.Items);
            }
            MenuItemDisplayTextBox.Text = this.Menu.DisplaySize;

            menuItemsBindingSource.DataSource = _menuItems;
        }

        private void ShowMenuItems(string id)
        {
            MenuItemModel menuItem = null;

            if (this.MenuItem != null)
            {
                menuItem = this.MenuItem.Items.Find(i => i.Id == id);
            }
            else
            {
                menuItem = this.Menu.Items.Find(i => i.Id == id);
            }

            MenuItemsForm form = new MenuItemsForm()
            {
                BusinessModule = this.BusinessModule,
                Menu = this.Menu,
                MenuItem = menuItem,
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
            MenuItemsGridView.Columns[0].Visible = showIds;
        }
    }
}
