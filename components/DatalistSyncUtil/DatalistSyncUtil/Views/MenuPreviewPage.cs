//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatalistSyncUtil.Views
{
    public partial class MenuPreviewPage : Form
    {
        public MenuPreviewPage()
        {
            this.InitializeComponent();
        }

        public MenuPreviewPage(List<MenuListModel> finalList, List<MenuItemModel> finalListItems)
        {
            this.InitializeComponent();

            this.Cache = new RedisCacheManager();
            this.FinalList = finalList;
            this.FinalListItems = finalListItems;
            this.LoadHelper = new TenantHelper();
            this.SourceLoadHelper = new SourceTenantHelper();
            this.TargetMenu = this.LoadHelper.GetMenu();
            this.LoadTreeView(this.PreviewTreeList, this.FinalList);
        }

        public ICacheManager Cache { get; set; }

        public List<MenuListModel> FinalList { get; set; }

        public List<MenuListModel> MenuList { get; set; }

        public List<MenuItemModel> FinalListItems { get; set; }

        public List<MenuItemModel> MenuItems { get; set; }

        public TenantHelper LoadHelper { get; set; }

        public List<CodeListModel> Sourceitems { get; set; }

        public SourceTenantHelper SourceLoadHelper { get; set; }

        public List<MenuListModel> TargetMenu { get; set; }

        private void LoadTreeView(TreeView treeView, List<MenuListModel> lists)
        {
            TreeNode listNode = null;
            List<TreeNode> itemNodes = null;
            TreeNode menuNode = null;
            string contentID = string.Empty;

            try
            {
                treeView.Nodes.Clear();
                List<string> parentlists = this.GetAllContentID();

                parentlists.ForEach(f =>
                {
                    itemNodes = new List<TreeNode>();
                    bool isParentModeAdded = false;
                    if (this.FinalListItems != null)
                    {
                        this.GetTreeItems(itemNodes, f.Trim());
                        listNode = new TreeNode(f.Trim(), itemNodes.ToArray());
                        if (itemNodes.Count != 0)
                        {
                            treeView.Nodes.Add(listNode);
                            isParentModeAdded = true;
                        }
                    }

                    if (!isParentModeAdded)
                    {
                        menuNode = new TreeNode(f.Trim());
                        treeView.Nodes.Add(menuNode);
                    }
                });

                treeView.ExpandAll();
            }
            finally
            {
                listNode = null;
                itemNodes = null;
            }
        }

        private List<string> GetAllContentID()
        {
            List<string> listContents = new List<string>();

            if (this.FinalList != null)
            {
                this.FinalList.ForEach(f =>
                {
                    listContents.Add(f.Name.Trim());
                });
            }

            if (this.FinalListItems != null)
            {
                this.FinalListItems.ForEach(f =>
                {
                    if (!listContents.Contains(f.Name.Trim()))
                    {
                        listContents.Add(f.Name.Trim());
                    }
                });
            }

            return listContents;
        }

        private void GetTreeItems(List<TreeNode> itemNodes, string contentID)
        {
            TreeNode node = null;
            string separator = " - ";
            List<MenuItemModel> items = this.FinalListItems.FindAll(f => f.Name.Trim() == contentID);

            items.ForEach(f =>
            {
                node = new TreeNode(f.Name + separator + f.IsActive);
                itemNodes.Add(node);
            });
        }

        private void Submit_btn(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {   
                this.SaveMenus();
               this.SaveMenuItems();
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Saved Sucessfully");
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Failed to Save");
            }
        }

        private void SaveMenus()
        {
            this.MenuList = this.LoadHelper.GetMenu();
            try
            {
                if (this.FinalList != null)
                {
                    List<TenantModuleModel> modules = this.LoadHelper.LoadModules();

                    foreach (MenuListModel list in this.FinalList)
                    {
                        list.TenantModuleID = modules.Find(f => f.TenantId == list.TenantId && f.TenantModuleId == list.TenantModuleID).TenantModuleId;
                        if (!this.MenuList.Any(a => a.Name == list.Name))
                        {
                            this.LoadHelper.AddMenus(list);
                        }
                        else
                        {
                            this.LoadHelper.UpdateMenus(list);
                        }
                    }

                    this.Cache.Remove("TargetMenus");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message);
            }
        }

        private void SaveMenuItems()
        {
            List<MenuItemModel> childMenuItems = new List<MenuItemModel>();
            MenuListModel list = null;
            this.MenuList = this.LoadHelper.GetMenu();
            List<MenuListModel> sourceMenuList = this.SourceLoadHelper.GetMenu();

            this.MenuList.ForEach(x =>
            {
                childMenuItems.AddRange(x.Children.ToList());
            });

            this.MenuItems = childMenuItems;

            if (this.FinalListItems != null)
            {
                this.FinalListItems.ForEach(f =>
                {
                    string sourcemenuName = sourceMenuList.Find(e => e.ID == f.MenuID).Name;
                    list = this.MenuList.Where(e => e.Name == sourcemenuName && e.IsActive == true).FirstOrDefault();
                    if (list != null)
                    {
                        f.MenuID = list.ID;
                        if (!this.MenuItems.Any(a => a.Name == f.Name))
                        {
                            this.LoadHelper.AddMenuItem(f);
                        }
                        else
                        {
                            f.ID = this.MenuItems.Find(a => a.Name == f.Name).ID;
                            this.LoadHelper.UpdateMenuItem(f);
                        }
                    }
                });

                this.Cache.Remove("TargetMenuItems");
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
