//-----------------------------------------------------------------------------------------
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

        private List<MenuItemModel> GetNewMenuItemsFromNewList()
        {
            List<MenuItemModel> newmenuitems = new List<MenuItemModel>();
            List<string> menuitems = this.SourceMenuList.Select(c => c.Name).Except(this.TargetMenuList.Select(c => c.Name)).ToList();
            List<MenuListModel> newmenus = this.SourceMenuList.Where(c => menuitems.Contains(c.Name)).ToList();
            newmenus.ForEach(f =>
            {
                newmenuitems.AddRange(f.Children.Where(a => a.IsActive = true));
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
            menuItems = this.SourceMenuItem.Where(a => !this.TargetMenuItem.Any(b => b.Name == a.Name)).ToList();
            if (menuItems != null && menuItems.Count > 0)
            {
                newMenuItemsFromUpdateList.AddRange(menuItems.Where(a => a.IsActive = true));
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
            if (sourceSecurity != null && targetSecurity != null)
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
            if ((this.UpdateList == null || this.UpdateList.Count() == 0) && (this.UpdateListItems == null || this.UpdateListItems.Count() == 0))
            {
                MessageBox.Show("Error:Please include some rows before moving to preview screen");
                return;
            }

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
            if (this.MenuSelectAllChkBox.Checked)
            {
                bool msg = false;
                foreach (DataGridViewRow row in this.MenuListNewGrid.Rows)
                {
                    bool rightpresent = this.CheckRightsforSelectAllmenu(row.DataBoundItem as MenuListModel);
                    if (rightpresent == true)
                    {
                        row.ReadOnly = true;
                        row.Cells[0].Selected = false;
                        row.Cells[0].Value = false;
                        msg = true;
                    }
                    else
                    {
                        row.Cells[0].Value = true;
                    }
                }

                if (msg)
                {
                    MessageBox.Show("The Security Rights are not present  ", "Security Right", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                foreach (DataGridViewRow row in this.MenuListNewGrid.Rows)
                {
                    row.Cells[0].Value = false;
                }
            }
        }

        private void MenuItemNewSelectAllChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.MenuItemNewSelectAllChkBox.Checked)
            {
                bool msg = false;
                foreach (DataGridViewRow row in this.MenuItemNewGrid.Rows)
                {
                    bool rightpresent = this.CheckRightsforSelectAllmenuitems(row.DataBoundItem as MenuItemModel);
                    bool labelpresent = this.CheckLabelsSelectAll(row.DataBoundItem as MenuItemModel);
                    if (rightpresent == true || labelpresent == true)
                    {
                        row.ReadOnly = true;
                        row.Cells[0].Selected = false;
                        row.Cells[0].Value = false;
                        msg = true;
                    }
                    else
                    {
                        row.Cells[0].Value = true;
                    }
                }

                if (msg)
                {
                    MessageBox.Show("The Security Rights and Labels are not present  ", "Security Right", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                foreach (DataGridViewRow row in this.MenuItemNewGrid.Rows)
                {
                    row.Cells[0].Value = false;
                }
            }
        }

        private void MenuItemUpdateSrcSelectAllChkBox_CheckedChanged(object sender, EventArgs e)
        {
            bool msg = false;
            if (this.MenuItemUpdateSrcSelectAllChkBox.Checked)
            {
                foreach (DataGridViewRow row in this.MenuItemSrcUpdateGrid.Rows)
            {
                bool rightpresent = this.CheckRightsforSelectAllmenuitems(row.DataBoundItem as MenuItemModel);
                bool labelpresent = this.CheckLabelsSelectAll(row.DataBoundItem as MenuItemModel);
                if (rightpresent == true || labelpresent == true)
                {
                    row.ReadOnly = true;
                    row.Cells[0].Selected = false;
                    row.Cells[0].Value = false;
                        msg = true;
                }
                else
                {
                    row.Cells[0].Value = true;
                }
            }

                if (msg)
                {
                    MessageBox.Show("The Security Rights and Labels are not present  ", "Security Right", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                foreach (DataGridViewRow row in this.MenuItemSrcUpdateGrid.Rows)
                {
                    row.Cells[0].Value = false;
                }
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
                       str = str + " " + menuitem.LabelItemContentID;
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
            CodeListModel sourceSecurity = null;
            sourceSecurity = this.Sourceitems.Where(c => c.ID == menuitem.SecurityRightItemID).FirstOrDefault();
            if (sourceSecurity != null)
            {
                securityRight = sourceSecurity.Code;
            }

            ///securityRight = this.Sourceitems.Find(c => c.ID == menuitem.SecurityRightItemID).Code;
                right = this.Items.Any(a => a.Code == securityRight);
                if (!right)
                {
                syncscreen = true;
                str = str + " " + securityRight;
                 right = false;
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
            CodeListModel sourceSecurity = null;
            sourceSecurity = this.Sourceitems.Where(c => c.ID == menu.SecurityRightItemID).FirstOrDefault();
            if (sourceSecurity != null)
            {
                securityRight = sourceSecurity.Code;
            }

            ///securityRight = this.Sourceitems.Find(c => c.ID == menu.SecurityRightItemID).Code;
            right = this.Items.Any(a => a.Code == securityRight);
            if (!right)
            {
                syncscreen = true;
                str = str + " " + securityRight;
                right = false;
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

        private bool CheckRightsforSelectAllmenu(MenuListModel menu)
        {
            string securityRight = null;
            bool right = false;
            bool syncscreen = false;
            CodeListModel sourceSecurity = null;
            sourceSecurity = this.Sourceitems.Where(c => c.ID == menu.SecurityRightItemID).FirstOrDefault();
            if (sourceSecurity != null)
            {
                securityRight = sourceSecurity.Code;
            }

           /// securityRight = this.Sourceitems.Find(c => c.ID == menu.SecurityRightItemID).Code;
            right = this.Items.Any(a => a.Code == securityRight);
            if (!right)
            {
                syncscreen = true;
                right = false;
            }

            return syncscreen;
        }

        private bool CheckRightsforSelectAllmenuitems(MenuItemModel menuitem)
        {
            string securityRight = null;
            bool right = false;
            bool syncscreen = false;
            CodeListModel sourceSecurity = null;
            sourceSecurity = this.Sourceitems.Where(c => c.ID == menuitem.SecurityRightItemID).FirstOrDefault();
            if (sourceSecurity != null)
            {
                securityRight = sourceSecurity.Code;
            }

           /// securityRight = this.Sourceitems.Find(c => c.ID == menuitem.SecurityRightItemID).Code;
            right = this.Items.Any(a => a.Code == securityRight);
            if (!right)
            {
                syncscreen = true;
                right = false;
            }

            return syncscreen;
        }

        private bool CheckLabelsSelectAll(MenuItemModel menuitem)
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
                str = str + " " + menuitem.LabelItemContentID;
            }

            return syncscreen;
        }
    }
}
