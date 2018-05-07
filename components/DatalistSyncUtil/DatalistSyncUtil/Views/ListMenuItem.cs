//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
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
using System.Linq;
using System.Windows.Forms;

namespace DatalistSyncUtil.Views
{
    public partial class ListMenuItem : Form
    {
        public ListMenuItem(Guid tenantID, string name, int numberOfDays)
        {
            this.InitializeComponent();
            this.Name = name;
            this.NoOfDays = numberOfDays;
            this.Cache = new RedisCacheManager();
            this.GetMenuItems(name);
            this.menulabel.Text = this.Name;
            this.TenantID = tenantID;
            this.LoadDataListItems();
        }

        public int NoOfDays { get; set; }

        public List<MenuItemModel> SourceMenuItems { get; set; }

        public List<string> SourceMenuName { get; set; }

        public List<MenuListModel> SourceMenu { get; set; }

        public ICacheManager Cache { get; set; }

        public string ContentID { get; set; }

        public Guid TenantID { get; set; }

        private void GetMenuItems(string name)
        {
            this.SourceMenu = this.Cache.Get<List<MenuListModel>>("Menus" + this.TenantID.ToString());
            this.SourceMenu.ForEach(f =>
            {
                if (f.Name == name)
                {
                    this.SourceMenuItems = f.Children.ToList();
                }
            });
        }

        private void LoadDataListItems()
        { 
                    this.menuItemView.DataSource = new BindingList<MenuItemModel>(this.SourceMenuItems.ToList()); 
         }

        private void MenuClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Clearmenu_Click(object sender, EventArgs e)
        {
            this.LoadDataListItems();
        }

       private void SelectAllChkBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.menuItemView.Rows)
            {
                row.Cells[0].Value = this.SelectAllChkBox.Checked;
            }
        }

        private void Savemenu_Click(object sender, EventArgs e)
        {
        }
    }
}
