using HP.HSP.UA3.Administration.UX.Common;
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

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    public partial class Confirmation_MenuItems : Form
    {
        public Confirmation_MenuItems()
        {
            InitializeComponent();
        }

        public MainForm MainForm { get; set; }


        private void MenusGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void RefreshGrid()
        {
            List<string> row = new List<string>();

            for (int i = 0; i < MainForm.Menus.Count; i++)
            {
                if (string.IsNullOrEmpty(MainForm.Menus[i].Id))
                {
                    MainForm.Menus[i].Id = "<< NULL OR MISSING VALUE >>";
                }

                row.Clear();
                row.Add(MainForm.Menus[i].Action);
                row.Add(MainForm.Menus[i].Module.Name);
                row.Add(MainForm.Menus[i].Id);
                row.Add(MainForm.Menus[i].Name);
                row.Add(MainForm.Menus[i].SecurityRightId);
                row.Add(MainForm.Menus[i].DisplaySize);
            }

            for (int i = 0; i < MainForm.MenuItems.Count; i++)
            {
                if (string.IsNullOrEmpty(MainForm.MenuItems[i].Id))
                {
                    MainForm.MenuItems[i].Id = "<< NULL OR MISSING VALUE >>";
                }

                row.Clear();
                row.Add(MainForm.MenuItems[i].Action);
                row.Add(MainForm.MenuItems[i].Module.Name);
                row.Add(MainForm.MenuItems[i].MenuId);
                row.Add(MainForm.MenuItems[i].Id);
                row.Add(MainForm.MenuItems[i].ParentId);
                row.Add(MainForm.MenuItems[i].Name);
                row.Add(MainForm.MenuItems[i].Order);
                row.Add(MainForm.MenuItems[i].IsVisible);
                row.Add(MainForm.MenuItems[i].BaseURL);
                row.Add(MainForm.MenuItems[i].LabelConentID);
                row.Add(MainForm.MenuItems[i].DefaultText);
                row.Add(MainForm.MenuItems[i].CssClass);
                row.Add(MainForm.MenuItems[i].SecurityRightId);
                row.Add(MainForm.MenuItems[i].PageHelpContentId);
                row.Add(MainForm.MenuItems[i].MitaHelpContentId);
                row.Add(MainForm.MenuItems[i].ModuleSectionContentId);
                row.Add(MainForm.MenuItems[i].IocContainer);

                this.menuItemsGridView.Rows.Add(row.ToArray());
            }
        }

        private void ProcessLoad(object sender, EventArgs e)
        {
            string coreTenantModuleId;
            int loadMenusSuccessful = 0;
            int loadErrors = 0;

            Cursor.Current = Cursors.WaitCursor;
            coreTenantModuleId = ConfigurationManager.AppSettings["CoreTenantModuleId"];

            if (coreTenantModuleId != null)
            {
                this.LoadMenuAndMenuItems(coreTenantModuleId, ref loadMenusSuccessful, ref loadErrors);
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Tenant Configuration load complete. " + loadMenusSuccessful + " Menu Items loaded and " + loadErrors + " errors reported.", "Tenant Load Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void LoadMenuAndMenuItems(string tenantModuleId, ref int loadMenusSuccessful, ref int loadErrors)
        {
               int currentRow = 0;
               bool updated = true;

               for (int i = 0; i < MainForm.MenuItems.Count; i++)
               {
                   HP.HSP.UA3.Utilities.LoadTenantDb.Data.MenuItem menuItem = new HP.HSP.UA3.Utilities.LoadTenantDb.Data.MenuItem();
                   menuItem.MainForm = this.MainForm;
                   menuItem.MenuItemId = Guid.Parse(this.MainForm.MenuItems[i].Id);
                   if (this.MainForm.MenuItems[i].ParentId != "")
                       menuItem.ParentMenuItemId = Guid.Parse(this.MainForm.MenuItems[i].ParentId);
                   menuItem.Name = this.MainForm.MenuItems[i].Name;
                   menuItem.OrderIndex = this.MainForm.MenuItems[i].Order;
                   if (this.MainForm.MenuItems[i].SecurityRightId != "")
                       menuItem.SecurityRightItemId = Guid.Parse(this.MainForm.MenuItems[i].SecurityRightId);
                   menuItem.LabelItemContentId = this.MainForm.MenuItems[i].LabelConentID;
                   menuItem.DefaultText = this.MainForm.MenuItems[i].DefaultText;
                   menuItem.BaseUrl = this.MainForm.MenuItems[i].BaseURL;
                   menuItem.CssClass = this.MainForm.MenuItems[i].CssClass;
                   menuItem.IocContainer = this.MainForm.MenuItems[i].IocContainer;
                   menuItem.IsVisible = bool.Parse(this.MainForm.MenuItems[i].IsVisible);
                   menuItem.PageHelpContentId = this.MainForm.MenuItems[i].PageHelpContentId;
                   menuItem.MitaHelpContentId = this.MainForm.MenuItems[i].MitaHelpContentId;
                   menuItem.ModuleSectionContentId = this.MainForm.MenuItems[i].ModuleSectionContentId;
                   menuItem.MenuId = Guid.Parse(this.MainForm.MenuItems[i].MenuId);

                   if (menuItem.GetMenuItemId(menuItem) == null)
                   {
                       try
                       {
                           menuItem = menuItem.AddMenuItem(menuItem);
                       }
                       catch
                       {
                           menuItem = null;
                       }

                       if (menuItem != null)
                       {
                           this.MainForm.MenuItems[i].Action = "Added";
                           loadMenusSuccessful++;
                       }
                       else
                       {
                           this.MainForm.MenuItems[i].Action = "Add Error";
                           loadErrors++;
                       }
                   }
                   else
                   {
                       try
                       {
                           updated = menuItem.UpdateMenuItem(menuItem);
                       }
                       catch
                       {
                           updated = false;
                       }

                       if (updated)
                       {
                           this.MainForm.MenuItems[i].Action = "Updated";
                           loadMenusSuccessful++;
                       }
                       else
                       {
                           this.MainForm.MenuItems[i].Action = "Update Error";
                           loadErrors++;
                       }
                   }

           /*        menuItem.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheFullMenuTableKey, "false", "false", "false");
           */     
                   this.menuItemsGridView.Rows[currentRow].Cells[0].Value = this.MainForm.MenuItems[i].Action;

                   if (this.menuItemsGridView.Rows.Count > currentRow + 1)
                   {
                       this.menuItemsGridView.CurrentCell = this.menuItemsGridView.Rows[currentRow + 1].Cells[0];
                       this.menuItemsGridView.Rows[currentRow + 1].Selected = true;
                   }

                   this.menuItemsGridView.Refresh();
                   currentRow++;
               }
        }

        private void propertiesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
            private void rolesGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void Confirmation_MenuItems_Load(object sender, EventArgs e)
        {
            this.RefreshGrid();

            if (MainForm.Menus.Count > 0 &&
                MainForm.MenuItems.Count > 0)
            {
                this.loadPushButton.Enabled = true;
            }
            else
            {
                this.loadPushButton.Enabled = false;
            }
        }

        private void cancelPushButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
