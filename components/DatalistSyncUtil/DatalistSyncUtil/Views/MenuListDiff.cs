﻿//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatalistSyncUtil.Views
{
    public partial class MenuListDiff : Form
    {
        public MenuListDiff()
        {
            this.InitializeComponent();
        }

        public MenuListDiff(Guid tenantID, string type, List<MenuListModel> sourceList, List<MenuListModel> targetList)
        {
            this.InitializeComponent();
            this.TargetConnectionString = ConfigurationManager.ConnectionStrings["TargetDataList"];
            this.LoadHelper = new TenantHelper(this.TargetConnectionString);
            this.TenantID = tenantID;
            this.DeltaType = type;
            this.SourceLoadHelper = new SourceTenantHelper();
            this.SourceMenuList = sourceList;
            this.TargetMenuList = targetList;
            this.Items = this.LoadHelper.GetDataListItems();
            this.Sourceitems = this.SourceLoadHelper.GetDataListItems();
            this.LoadDelta();
        }

        public TenantHelper LoadHelper { get; set; }

        public ConnectionStringSettings TargetConnectionString { get; set; }

        public Guid TenantID { get; set; }

        public string DeltaType { get; set; }

        public List<MenuListModel> SourceMenuList { get; set; }

        public SourceTenantHelper SourceLoadHelper { get; set; }

        public List<MenuListModel> TargetMenuList { get; set; }

        public List<MenuListModel> UpdateList { get; set; }

        public List<MenuItemModel> SourceMenuItem { get; set; }

        public List<CodeListModel> Items { get; set; }

        public List<CodeListModel> Sourceitems { get; set; }

        public List<MenuItemModel> UpdateListItems { get; set; }

        public List<MenuItemModel> TargetMenuItem { get; set; }

        private void LoadDelta()
        {
            switch (this.DeltaType.ToUpper())
            {
                case "MENUS":
                    this.LoadMenuDelta();
                    this.LoadMenuItemDelta();
                    break;

                default:
                    break;
            }
        }

        private void LoadMenuDelta()
        {
            List<MenuListModel> newMenus = null;
            this.MenuListNewGrid.AutoGenerateColumns = false;
            newMenus = this.GetNewMenus();
            this.MenuListNewGrid.DataSource = new BindingList<MenuListModel>(newMenus);
        }

        private List<MenuListModel> GetNewMenus()
        {
            List<string> menuLists = this.SourceMenuList.Select(c => c.Name).Except(this.TargetMenuList.Select(c => c.Name)).ToList();
            List<MenuListModel> newMenuList = this.SourceMenuList.Where(c => menuLists.Contains(c.Name)).ToList();
            return newMenuList.OrderBy(o => o.Name).ToList();
        }

        private void LoadMenuItemDelta()
        {
            List<MenuItemModel> newMenuItemsFromNewList = null;
            List<MenuItemModel> newMenuItemsFromExistingList = null;
            List<MenuItemModel> newMenuItems = new List<MenuItemModel>();
            this.MenuItemNewGrid.AutoGenerateColumns = false;
            newMenuItemsFromNewList = this.GetNewMenuItemsFromNewList();
            newMenuItems.AddRange(newMenuItemsFromNewList);
            newMenuItemsFromExistingList = this.GetNewMenuItemsFromExistingList();
            newMenuItems.AddRange(newMenuItemsFromExistingList);
            this.MenuItemNewGrid.DataSource = new BindingList<MenuItemModel>(newMenuItems);
            this.UpdatedMenuItems();
        }

        private List<MenuItemModel> GetNewMenuItems()
        {
            List<string> menuitems = this.SourceMenuItem.Select(c => c.Name).Except(this.TargetMenuItem.Select(c => c.Name)).ToList();
            List<MenuItemModel> newmenus = this.SourceMenuItem.Where(c => menuitems.Contains(c.Name)).ToList();
            return newmenus.OrderBy(o => o.Name).ToList();
        }

        private List<MenuItemModel> GetNewMenuItemsFromNewList()
        {
            List<MenuItemModel> newmenuitems = new List<MenuItemModel>();
            List<string> menuitems = this.SourceMenuList.Select(c => c.Name).Except(this.TargetMenuList.Select(c => c.Name)).ToList();
            List<MenuListModel> newmenus = this.SourceMenuList.Where(c => menuitems.Contains(c.Name)).ToList();
            newmenus.ForEach(f =>
            {
                newmenuitems.AddRange(f.Children);
            });
            return newmenuitems.OrderBy(o => o.Name).ToList();
        }

        private List<MenuItemModel> GetNewMenuItemsFromExistingList()
        {
            List<MenuItemModel> newMenuItemsFromUpdateList = new List<MenuItemModel>();
            List<MenuItemModel> sourceMenuItems = new List<MenuItemModel>();
            List<MenuItemModel> targetMenuItems = new List<MenuItemModel>();
            List<MenuItemModel> menuItems = null;
            List<string> menuitems = this.SourceMenuList.Select(c => c.Name).Intersect(this.TargetMenuList.Select(c => c.Name)).ToList();
            menuitems.ForEach(f =>
            {
                sourceMenuItems.AddRange(this.SourceMenuList.Find(e => e.Name == f).Children);
                targetMenuItems.AddRange(this.TargetMenuList.Find(e => e.Name == f).Children);
            });
            this.SourceMenuItem = sourceMenuItems;
            this.TargetMenuItem = targetMenuItems;
            menuItems = this.SourceMenuItem.Where(a => !this.TargetMenuItem.Any(b => b.Name == a.Name && b.IsActive == a.IsActive)).ToList();
            if (menuItems != null && menuItems.Count > 0)
            {
                newMenuItemsFromUpdateList.AddRange(menuItems);
            }

            return newMenuItemsFromUpdateList.OrderBy(o => o.Name).ToList();
        }

        private void UpdatedMenuItems()
        {
            List<MenuItemModel> updatedMenuItems = new List<MenuItemModel>();
            List<MenuItemModel> updatedtargetMenuItems = new List<MenuItemModel>();
            MenuItemModel targetMenuItems = null;
            bool itemChanged = false;
            this.SourceMenuItem.ForEach(b =>
            {
                itemChanged = false;
                targetMenuItems = TargetMenuItem.Find(a => b.Name == a.Name);
                if (targetMenuItems != null)
                {
                    itemChanged = this.CheckUpdateItemChanged(ref b, ref targetMenuItems);
                    if (itemChanged)
                    {
                        updatedMenuItems.Add(b);
                        updatedtargetMenuItems.Add(targetMenuItems);
                    }
                }
            });

            this.MenuItemSrcUpdateGrid.AutoGenerateColumns = false;
            this.MenuItemTgtUpdateGrid.AutoGenerateColumns = false;
            this.MenuItemSrcUpdateGrid.DataSource = new BindingList<MenuItemModel>(updatedMenuItems.OrderBy(o => o.Name).ToList());
            this.MenuItemTgtUpdateGrid.DataSource = new BindingList<MenuItemModel>(updatedtargetMenuItems.OrderBy(o => o.Name).ToList());
        }

        private bool CheckUpdateItemChanged(ref MenuItemModel t, ref MenuItemModel targetItem)
        {
            bool itemChanged = false;
            Guid sourceSecurityRightID = t.SecurityRightItemID;
            Guid targetSecurityRightID = targetItem.SecurityRightItemID;
            CodeListModel sourceSecurity = null;
            CodeListModel targetSecurity = null;
            string sourceSecurityRight = null;
            string targetSecurityRight = null;
            sourceSecurity = this.Sourceitems.Where(c => c.ID == sourceSecurityRightID).FirstOrDefault();
            targetSecurity = this.Items.Where(c => c.ID == targetSecurityRightID).FirstOrDefault();
            if (sourceSecurity != null || targetSecurity != null)
            {
                sourceSecurityRight = sourceSecurity.Code;
                targetSecurityRight = targetSecurity.Code;
                if (sourceSecurityRight != targetSecurityRight)
                {
                    itemChanged = true;
                }
            }

            if (t.LabelItemContentID != targetItem.LabelItemContentID)
            {
                itemChanged = true;
            }

            if (t.IsActive != targetItem.IsActive)
            {
                itemChanged = true;
            }

            return itemChanged;
        }

        private void PreviewUpdate_Click(object sender, EventArgs e)
        {
           MenuPreviewPage previewPage = new MenuPreviewPage(this.UpdateList, this.UpdateListItems);
           previewPage.ShowDialog();
        }

        private void BtnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Include_click(object sender, EventArgs e)
        {
            bool selected = false;
            this.UpdateList = new List<MenuListModel>();
            foreach (DataGridViewRow row in this.MenuListNewGrid.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateList.Add(row.DataBoundItem as MenuListModel);
                }
            }
        }

        private void MenuSelectAllChkBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.MenuListNewGrid.Rows)
            {
                row.Cells[0].Value = this.MenuSelectAllChkBox.Checked;
            }
        }

        private void MenuItemNewSelectAllChkBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.MenuItemNewGrid.Rows)
            {
                row.Cells[0].Value = this.MenuItemNewSelectAllChkBox.Checked;
            }
        }

        private void MenuItemUpdateSrcSelectAllChkBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.MenuItemSrcUpdateGrid.Rows)
            {
                row.Cells[0].Value = this.MenuItemUpdateSrcSelectAllChkBox.Checked;
            }
        }

        private void MenuItemUpdateTgtSelectAllChkBox_CheckedChange(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.MenuItemTgtUpdateGrid.Rows)
            {
                row.Cells[0].Value = this.MenuItemUpdateTgtSelectAllChkBox.Checked;
            }
        }

        private void IncludeUpdateMenuItems_Click(object sender, EventArgs e)
        {
            bool selected = false;
            this.UpdateListItems = new List<MenuItemModel>();
            List<MenuItemModel> menuSecRght = new List<MenuItemModel>();

            foreach (DataGridViewRow row in this.MenuItemNewGrid.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateListItems.Add(row.DataBoundItem as MenuItemModel);
                }
            }

            foreach (DataGridViewRow row in this.MenuItemSrcUpdateGrid.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateListItems.Add(row.DataBoundItem as MenuItemModel);
                }
            }
        }

        private bool CheckLabels(MenuItemModel menuitem)
        {
            bool right = false;
            bool syncscreen = false;
            CodeListModel modelListItem = null;
            string str = null;
            List<string> list = new List<string>();
                    right = this.Items.Any(a => a.Code == menuitem.LabelItemContentID);
                    if (!right)
                    {
                      syncscreen = true;
                     modelListItem = this.Sourceitems.Where(a => a.Code == menuitem.LabelItemContentID).FirstOrDefault();
                        str = str + " " + modelListItem.Code;
                    }

                if (!right)
                {
                    MessageBox.Show("The Label is not present : " + str, "Label", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }

            return syncscreen;
        }

        private void SelectColumn_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = MenuItemNewGrid.Rows[rowIndex];
            bool rightpresent = this.CheckRightsformenuitems(row.DataBoundItem as MenuItemModel);
            bool labelpresent = this.CheckLabels(row.DataBoundItem as MenuItemModel);
            if (rightpresent == true || labelpresent == true)
            {
                MenuItemNewGrid.Rows[rowIndex].ReadOnly = true;
                MenuItemNewGrid.Rows[rowIndex].Cells[0].Selected = false;
                MenuItemNewGrid.Rows[rowIndex].Cells[0].Value = false;
            }
        }

        private bool CheckRightsformenuitems(MenuItemModel menuitem)
        {
            string securityRight = null;
            bool right = false;
            string str = null;
            bool syncscreen = false;
            CodeListModel modelListItem = null;
            securityRight = this.Sourceitems.Find(c => c.ID == menuitem.SecurityRightItemID).Code;
                right = this.Items.Any(a => a.Code == securityRight);
                if (!right)
                {
                syncscreen = true;
                modelListItem = this.Sourceitems.Where(a => a.ID == menuitem.SecurityRightItemID).FirstOrDefault();
                    str = str + " " + modelListItem.Code;
                    right = false;
                }
                else
                {
                menuitem.SecurityRightItemID = this.Items.Find(c => c.Code == securityRight).ID;
                }

            if (!right)
            {
                MessageBox.Show("The Security Right is not present : " + str, "Security Right", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }

            return syncscreen;
        }

        private void SelectColumnformenuitems_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = MenuItemSrcUpdateGrid.Rows[rowIndex];
            bool rightpresent = this.CheckRightsformenuitems(row.DataBoundItem as MenuItemModel);
            bool labelpresent = this.CheckLabels(row.DataBoundItem as MenuItemModel);
            if (rightpresent == true || labelpresent == true)
            {
                MenuItemSrcUpdateGrid.Rows[rowIndex].ReadOnly = true;
                MenuItemSrcUpdateGrid.Rows[rowIndex].Cells[0].Selected = false;
                MenuItemSrcUpdateGrid.Rows[rowIndex].Cells[0].Value = false;
            }
        }

        private bool CheckRightsformenu(MenuListModel menu)
        {
            string securityRight = null;
            bool right = false;
            string str = null;
            bool syncscreen = false;
            CodeListModel modelListItem = null;
            securityRight = this.Sourceitems.Find(c => c.ID == menu.SecurityRightItemID).Code;
            right = this.Items.Any(a => a.Code == securityRight);
            if (!right)
            {
                syncscreen = true;
                modelListItem = this.Sourceitems.Where(a => a.ID == menu.SecurityRightItemID).FirstOrDefault();
                str = str + " " + modelListItem.Code;
                right = false;
            }
            else
            {
                menu.SecurityRightItemID = this.Items.Find(c => c.Code == securityRight).ID;
            }

            if (!right)
            {
                MessageBox.Show("The Security Right is not present : " + str, "Security Right", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }

            return syncscreen;
        }

        private void SelectColumnforNewMenu_clicked(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = MenuListNewGrid.Rows[rowIndex];
            bool rightpresent = this.CheckRightsformenu(row.DataBoundItem as MenuListModel);
            if (rightpresent == true)
            {
                MenuListNewGrid.Rows[rowIndex].ReadOnly = true;
                MenuListNewGrid.Rows[rowIndex].Cells[0].Selected = false;
                MenuListNewGrid.Rows[rowIndex].Cells[0].Value = false;
            }
        }
    }
}
