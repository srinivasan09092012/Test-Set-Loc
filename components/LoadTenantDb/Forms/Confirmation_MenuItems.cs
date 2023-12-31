﻿using HP.HSP.UA3.Administration.UX.Common;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            string tenantModuleId;
            
            int loadMenusSuccessful = 0;
            int loadErrors = 0;

            Cursor.Current = Cursors.WaitCursor;
            tenantModuleId = ConfigurationManager.AppSettings[this.MainForm.MenuItems[0].Module.Name + "TenantModuleId"];

            if (tenantModuleId != null)
            {
                this.LoadMenuAndMenuItems(tenantModuleId, ref loadMenusSuccessful, ref loadErrors);
                Cursor.Current = Cursors.Default;
                log.Info("Tenant Configuration load complete. " + loadMenusSuccessful + " Menu Items loaded and " + loadErrors + " errors reported.");
                MessageBox.Show("Tenant Configuration load complete. " + loadMenusSuccessful + " Menu Items loaded and " + loadErrors + " errors reported.", "Tenant Load Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void LoadMenuAndMenuItems(string tenantModuleId, ref int loadMenusSuccessful, ref int loadErrors)
        {
            int currentRow = 0;
            bool updated = true;
            bool added = false;
            bool menuAdded = false;
            bool skipProcessing = false;
            for (int j = 0; j < MainForm.Menus.Count; j++)
            {
                HP.HSP.UA3.Utilities.LoadTenantDb.Data.Menu menu = new HP.HSP.UA3.Utilities.LoadTenantDb.Data.Menu();
                menu.MainForm = this.MainForm;
                menu.MenuId = Guid.Parse(this.MainForm.Menus[j].Id);
                menu.DisplaySize = this.MainForm.Menus[j].DisplaySize;
                menu.IsActive = true;
                menu.SecurityRightItemId = Guid.Parse(this.MainForm.Menus[j].SecurityRightId);
                menu.OperatorID = "USER1";
                menu.Name = this.MainForm.Menus[j].Name;
                menu.TenantModuleId = Guid.Parse(tenantModuleId);
                if (!DoesMenuExist(this.MainForm.Menus[j].Id))
                {
                    try
                    {
                        menuAdded = menu.AddMenu(menu);
                    }
                    catch (Exception)
                    {
                        menuAdded = false;
                    }
                    if (menuAdded)
                    {
                        loadMenusSuccessful++;
                    }
                    else
                    {
                        log.Error("Error Confirmation_MenuItems.LoadMenuAndMenuItems Add Menu Error " +
                            "Menu=" + menu.ToString());
                        loadErrors++;
                    }
                }
            }

            for (int i = 0; i < MainForm.MenuItems.Count; i++)
            {
                skipProcessing = false;
                HP.HSP.UA3.Utilities.LoadTenantDb.Data.MenuItem menuItem = new HP.HSP.UA3.Utilities.LoadTenantDb.Data.MenuItem();
                menuItem.MainForm = this.MainForm;

                //Check to see if we have a valid GUID if not error off and skip process 
                Guid testNewGuid;
                if (!Guid.TryParse(this.MainForm.MenuItems[i].Id, out testNewGuid))
                {
                    log.Error("Error Confirmation_MenuItems.LoadMenuAndMenuItems Id Error " +
                        " Menu Name = " + this.MainForm.MenuItems[i].Name +
                        " INVALID Menu Item GUID=" + this.MainForm.MenuItems[i].Id);
                    skipProcessing = true;
                    this.MainForm.MenuItems[i].Action = "Add Error";
                    loadErrors++;
                }
                else
                {
                    menuItem.MenuItemId = Guid.Parse(this.MainForm.MenuItems[i].Id);
                }

                if (this.MainForm.MenuItems[i].ParentId != "")
                {
                    if (!Guid.TryParse(this.MainForm.MenuItems[i].ParentId, out testNewGuid))
                    {
                        log.Error("Error Confirmation_MenuItems.LoadMenuAndMenuItems Id Error " +
                            " Menu Name = " + this.MainForm.MenuItems[i].Name +
                            " INVALID ParentId=" + this.MainForm.MenuItems[i].ParentId);
                        skipProcessing = true;
                        this.MainForm.MenuItems[i].Action = "Add Error";
                        loadErrors++;
                    }
                    else
                    {
                        menuItem.ParentMenuItemId = Guid.Parse(this.MainForm.MenuItems[i].ParentId);
                    }
                }
                else
                {
                    string menuLevel1Id = getLevel1MenuId(menuItem.MenuItemId);
                    if (menuLevel1Id != null)
                    {
                        menuItem.MenuId = Guid.Parse(menuLevel1Id);
                    }
                    else
                    {
                        log.Error("Error Confirmation_MenuItems.LoadMenuAndMenuItems Menu Id Not Found Error " +
                        " Menu Item Id = " + this.MainForm.MenuItems[i].Id);
                        skipProcessing = true;
                        this.MainForm.MenuItems[i].Action = "Add Error";
                        loadErrors++;

                    }
                }

                menuItem.Name = this.MainForm.MenuItems[i].Name;
                menuItem.OrderIndex = this.MainForm.MenuItems[i].Order;
                if (this.MainForm.MenuItems[i].SecurityRightId != "")
                {
                    //Check to see if we have a valid GUID if not error off and skip process 
                    Guid testNewGuidSecurity;
                    if (!Guid.TryParse(this.MainForm.MenuItems[i].SecurityRightId, out testNewGuidSecurity))
                    {
                        log.Error("Error Confirmation_MenuItems.LoadMenuAndMenuItems Security Right Error " +
                            " Menu Name = " + this.MainForm.MenuItems[i].Name +
                            " INVALID Security Right Item GUID=" + this.MainForm.MenuItems[i].SecurityRightId);
                        skipProcessing = true;
                        this.MainForm.MenuItems[i].Action = "Add Error";
                        loadErrors++;
                    }
                    else
                    {
                        menuItem.SecurityRightItemId = Guid.Parse(this.MainForm.MenuItems[i].SecurityRightId);
                    }
                }

                if (!skipProcessing)
                {

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
                        added = menuItem.AddMenuItem(menuItem);

                        if (added)
                        {
                            this.MainForm.MenuItems[i].Action = "Added";
                            loadMenusSuccessful++;
                        }
                        else
                        {
                            this.MainForm.MenuItems[i].Action = "Add Error";
                            log.Error("Error Confirmation_MenuItems.LoadMenuAndMenuItems Add Error " +
                                "MenuItem=" + menuItem.ToString());
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
                            log.Error("Error Confirmation_MenuItems.LoadMenuAndMenuItems Update Error " +
                                "Name=" + menuItem.ToString());
                            loadErrors++;
                        }
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

        private string getLevel1MenuId(Guid menuItemId)
        {
            MainForm.MenuItemNode miNode;

            foreach (MainForm.MenuNode menuNode in this.MainForm.Menus)
            {
                miNode = menuNode.MenuItemNodes.Find(mi => mi.Id.ToUpper() == menuItemId.ToString("D").ToUpper());
                if (miNode != null)
                {
                    return miNode.MenuId;
                }
            }
            return null;
        }

        private bool DoesMenuExist(string menuId)
        {
            Guid menuIdGuid = Guid.Parse(menuId);
            IDbSession session;
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (session = new DbSession(connectionStringSettings.ProviderName, connectionStringSettings.ConnectionString))
            {
                MenuDbContext menuDbContext = new MenuDbContext(session);

                bool menuExists = false;

                var result = (from mi in menuDbContext.Set<Core.BAS.CQRS.DataAccess.Entities.Menu>() where mi.MenuID == menuIdGuid select mi.MenuID).FirstOrDefault();

                if (result.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
                {
                    menuExists = false;
                }
                else
                {
                    menuExists = true;
                }

                return menuExists;
            }
        }
    }
}
