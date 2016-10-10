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
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
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

        public bool IsActive { get; set; }

        public string OperatorId { get; set; }

        public DateTime LastModified { get; set; }

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

        public string GetMenuItem(MenuItem MenuItem)
        {
            GetMenuItemId(MenuItem);

            int retryCount = 0;
            string objDataQuery = string.Format("MenuItem?$filter=ContentID%20eq%20%27{0}%27", MenuItem.MenuItemId);
            string baseUrl = MainForm.ODataEndpointAddress;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(objDataQuery).Result;
            while (!response.IsSuccessStatusCode && retryCount < 9999)
            {
                response = client.GetAsync(objDataQuery).Result;
                retryCount++;
            }

            if (response.IsSuccessStatusCode)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Dictionary<string, object> parameters = serializer.Deserialize<Dictionary<string, object>>(response.Content.ReadAsStringAsync().Result);

                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    if (parameter.Value is IList)
                    {
                        IList MenuItems = (IList)parameter.Value;

                        foreach (Dictionary<string, object> MenuItemDetail in MenuItems)
                        {
                            MenuItem.MenuItemId = (Guid)MenuItemDetail["ID"];
                            MenuItem.MenuId = (Guid)MenuItemDetail["MenuID"];
                            MenuItem.ParentMenuItemId = (Guid)MenuItemDetail["ParentMenuItemId"];
                            MenuItem.SecurityRightItemId = (Guid)MenuItemDetail["SecurityRightItemId"];
                            MenuItem.Name = MenuItemDetail["Name"].ToString();
                            MenuItem.OrderIndex = MenuItemDetail["OrderIndex"].ToString();
                            MenuItem.IsVisible = (bool)MenuItemDetail["IsVisible"];
                            MenuItem.DefaultText = (string)MenuItemDetail["DefaultText"];
                            MenuItem.CssClass = (string)MenuItemDetail["CssClass"];
                            MenuItem.IocContainer = (string)MenuItemDetail["IocContainer"];
                            MenuItem.LabelItemContentId = (string)MenuItemDetail["LabelItemContentId"];
                            MenuItem.ModuleSectionContentId = (string)MenuItemDetail["ModuleSectionContentId"];
                            MenuItem.BaseUrl = (string)MenuItemDetail["BaseUrl"];
                            MenuItem.ReportsContentUrl = (string)MenuItemDetail["ReportsContentUrl"];
                            MenuItem.PrintPreviewContentUrl = (string)MenuItemDetail["PrintPreviewContentUrl"];
                            MenuItem.PageHelpContentId = (string)MenuItemDetail["PageHelpContentId"];
                            MenuItem.MitaHelpContentId = (string)MenuItemDetail["MitaHelpContentId"];
                            MenuItem.IsActive = (bool)MenuItemDetail["IsActive"];
                            MenuItem.OperatorId = MenuItemDetail["OperatorId"].ToString();
                            MenuItem.LastModified = (DateTime)MenuItemDetail["LastModified"];
                            MenuItem.TenantModuleId = (Guid)MenuItemDetail["TenantModuleID"];
                            
                            return MenuItem.MenuItemId.ToString();
                        }
                    }
                }
            }

            return null;
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
                    IsActive = true,
                    OperatorId = "USER1",
                    ItemLastModified = System.DateTime.UtcNow,
                },
                Requestor = new MenuService.RequestorModel()
                {
                    IdentifierId = MenuItem.MenuItemId.ToString(),
                    IdentifierIdType = HP.HSP.UA3.Utilities.LoadTenantDb.MenuService.CoreEnumerationsMessagingIdentifierIdType.User,
                    TenantId = MenuItem.TenantModuleId.ToString()
                }
            };

            try
            {
                response = this.clientLicense.UpdateMenuItem(command);
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheFullMenuTableKey, "false", "false", "false");
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
                    IsActive = true,
                    OperatorId = "USER1",
                    ItemLastModified = System.DateTime.UtcNow,
                },
                Requestor = new MenuService.RequestorModel()
                {
                    IdentifierId = MenuItem.MenuItemId.ToString(),
                    IdentifierIdType = HP.HSP.UA3.Utilities.LoadTenantDb.MenuService.CoreEnumerationsMessagingIdentifierIdType.User,
                    TenantId = MenuItem.TenantModuleId.ToString()
                }
            };

            try
            {
                response = this.clientLicense.AddMenuItem(command);
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheFullMenuTableKey, "false", "false", "false");
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


        private void RefreshCache(string cacheKey, string clearAllCodeTableCacheFlag = "false", string reloadCache = "true", string reloadAllCodeTableCache = "false")
        {
            string objDataQuery = string.Empty;

            objDataQuery = string.Format("CacheRefresh(CacheKey='{0}',ClearAllCodeTableCache={1},ReloadCache={2},ReloadAllCodeTableCache={3})", cacheKey, clearAllCodeTableCacheFlag, reloadCache, reloadAllCodeTableCache);
            string baseUrl = MainForm.ODataEndpointAddress;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(objDataQuery).Result;
        }
    }
}
