//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Administration.UX.Common;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using HP.HSP.UA3.Utilities.LoadTenantDb.Forms;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using WebRefMenuItemsMaint = HP.HSP.UA3.Utilities.LoadTenantDb.MenuService;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Data
{
    public class MenuItem
    {
        private WebRefMenuItemsMaint.MenuServiceClient clientLicense = new WebRefMenuItemsMaint.MenuServiceClient();

        public MenuItem()
            : base()
        {
        }

        public MainForm MainForm { get; set; }

        public Guid MenuItemId { get; set; }

        public Guid MenuId { get; set; }

        public Guid ParentMenuItemId { get; set; }

        public Guid SecurityRightItemId { get; set; }

        public string Name { get; set; }

        public string OrderIndex { get; set; }

        public bool IsVisible { get; set; }

        public string DefaultText { get; set; }

        public string CssClass { get; set; }

        public string IocContainer { get; set; }

        public string LabelItemContentId { get; set; }

        public string ModuleSectionContentId { get; set; }

        public string BaseUrl { get; set; }

        public string ReportsContentUrl { get; set; }

        public string PrintPreviewContentUrl { get; set; }

        public string PageHelpContentId { get; set; }

        public string MitaHelpContentId { get; set; }

        public Guid TenantModuleId { get; set; }

        private IDbSession session;

        public ConnectionStringSettings ConnectionStringSettings { get; set; }

        public string GetMenuItemId(MenuItem MenuItem)
        {
            this.ConnectionStringSettings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            this.session = new DbSession(this.ConnectionStringSettings.ProviderName, this.ConnectionStringSettings.ConnectionString);

            MenuDbContext MenuDbContext = new MenuDbContext(this.session);

            var MenuItemId = (from mi in MenuDbContext.Set<HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities.MenuItem>() where mi.MenuItemID == MenuItem.MenuItemId select mi.MenuItemID).FirstOrDefault();
                                                               
            if (MenuItemId.Equals(new Guid("{00000000-0000-0000-0000-000000000000}")))
            {
                return null;
            }
            else
            {
                return MenuItemId.ToString();
            } 
        } 

        public bool UpdateMenuItem(MenuItem MenuItem)
        {
            MenuService.MenuItemUpdated response = null;

            string parentMenuItemId = string.Empty;
            if (MenuItem.ParentMenuItemId != Guid.Parse("{00000000-0000-0000-0000-000000000000}"))
            {
                parentMenuItemId = MenuItem.ParentMenuItemId.ToString();
            }
            
            MenuService.UpdateMenuItemCommand command = new MenuService.UpdateMenuItemCommand()
            {
                UpdateMenuItem = new MenuService.UpdateMenuItem()
                {
                    MenuItemId = MenuItem.MenuItemId.ToString(),
                    MenuId = MenuItem.MenuId.ToString(),
                    ParentMenuItemId = parentMenuItemId,  
                    SecurityRightItemId = MenuItem.SecurityRightItemId.ToString(),
                    Name = MenuItem.Name,
                    OrderIndex = Convert.ToInt32(MenuItem.OrderIndex),
                    IsVisible = MenuItem.IsVisible,
                    DefaultText = MenuItem.DefaultText,
                    CssClass = MenuItem.CssClass,
                    IocContainter = MenuItem.IocContainer,
                    LabelItemKey = MenuItem.LabelItemContentId,
                    ModuleSectionItemKey = MenuItem.ModuleSectionContentId,
                    BaseUrl = MenuItem.BaseUrl,
                    ReportsContentUrl = MenuItem.ReportsContentUrl,
                    PrintPreviewContentUrl = MenuItem.PrintPreviewContentUrl,
                    PageHelpHtmlContentId = MenuItem.PageHelpContentId,
                    MitaHelpHtmlContentId = MenuItem.MitaHelpContentId,
                    IsActive = true
                },
                Requestor = new Core.BAS.CQRS.UserMeta.RequestorModel()
                {
                    IdentifierId = "User1",
                    IdentifierIdType = Core.BAS.CQRS.UserMeta.CoreEnumerations.Messaging.IdentifierIdType.User,
                    TenantId = this.MainForm.TenantId,
                    RequestDate = DateTime.UtcNow
                }
            };

            try
            {
                response = this.clientLicense.UpdateMenuItem(command);
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheFullMenuTableKey);
            }
            catch
            {
                return false;
            }

            if (response.MenuItemId != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public MenuItem AddMenuItem(MenuItem MenuItem)
        {
            MenuService.MenuItemAdded response = null;

            string parentMenuItemId = string.Empty;
            if (MenuItem.ParentMenuItemId != Guid.Parse("{00000000-0000-0000-0000-000000000000}"))
            {
                parentMenuItemId = MenuItem.ParentMenuItemId.ToString();
            }

            MenuService.AddMenuItemCommand command = new MenuService.AddMenuItemCommand()
            {
                AddMenuItem = new MenuService.AddMenuItem()
                {
                    MenuItemId = MenuItem.MenuItemId.ToString(),
                    MenuId = MenuItem.MenuId.ToString(),
                    ParentMenuItemId = parentMenuItemId,
                    SecurityRightItemId = MenuItem.SecurityRightItemId.ToString(),
                    Name = MenuItem.Name,
                    OrderIndex = Convert.ToInt32(MenuItem.OrderIndex),
                    IsVisible = MenuItem.IsVisible,
                    DefaultText = MenuItem.DefaultText,
                    CssClass = MenuItem.CssClass,
                    IocContainter = MenuItem.IocContainer,
                    LabelItemKey = MenuItem.LabelItemContentId,
                    ModuleSectionItemKey = MenuItem.ModuleSectionContentId,
                    BaseUrl = MenuItem.BaseUrl,
                    ReportsContentUrl = MenuItem.ReportsContentUrl,
                    PrintPreviewContentUrl = MenuItem.PrintPreviewContentUrl,
                    PageHelpHtmlContentId = MenuItem.PageHelpContentId,
                    MitaHelpHtmlContentId = MenuItem.MitaHelpContentId,
                    IsActive = true
                },
                Requestor = new Core.BAS.CQRS.UserMeta.RequestorModel()
                {
                    IdentifierId = "User1",
                    IdentifierIdType = Core.BAS.CQRS.UserMeta.CoreEnumerations.Messaging.IdentifierIdType.User,
                    TenantId = this.MainForm.TenantId,
                    RequestDate = DateTime.UtcNow
                }
            };

            try
            {
                response = this.clientLicense.AddMenuItem(command);
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheFullMenuTableKey);
            }
            catch
            {
                return null;
            }

            if (response.MenuItemId != null)
            {
                MenuItem.MenuItemId = Guid.Parse(response.MenuItemId); 
                return MenuItem;
            }
            else
            {
                return null;
            }
        }


        private void RefreshCache(string cacheKey, string reloadCache = "true", string cacheType = "Menu")
        {
            string objDataQuery = string.Format("CacheRefreshTable(CacheKey='{0}',ReloadCache={1},CacheType='{2}')", cacheKey, reloadCache, cacheType);
            string baseUrl = MainForm.ODataEndpointAddress;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(objDataQuery).Result;
        }
    }
}
