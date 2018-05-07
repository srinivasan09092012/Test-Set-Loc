//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Administration.UX.Common;
using HP.HSP.UA3.Utilities.LoadTenantDb.Forms;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using WebRefMenuMaint = HP.HSP.UA3.Utilities.LoadTenantDb.MenuService;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Data
{
    public partial class Menu
    {
        private WebRefMenuMaint.MenuServiceClient clientLicense = new WebRefMenuMaint.MenuServiceClient();

        public Menu()
            : base()
        {
        }

        public MainForm MainForm { get; set; }

        public Guid MenuId { get; set; }

        public Guid TenantModuleId { get; set; }

        public Guid SecurityRightItemId { get; set; }

        public string Name { get; set; }

        public string DisplaySize { get; set; }

        public bool IsActive { get; set; }

        public string OperatorID { get; set; }

        public DateTime LastModifiedTimeStamp { get; set; }

        public virtual List<MenuItem> MenuItems { get; set; }

        public bool AddMenu(Menu Menu)
        {
            MenuService.MenuAdded response = null;
            MenuService.AddMenuCommand command = new MenuService.AddMenuCommand()
            {
                AddMenu = new MenuService.AddMenu()
                {
                    TenantModuleId = Menu.TenantModuleId.ToString("D").ToUpper(),
                    MenuId = Menu.MenuId.ToString("D").ToUpper(),
                    SecurityRightItemId = Menu.SecurityRightItemId.ToString("D").ToUpper(),
                    Name = Menu.Name,
                    DisplaySize = Menu.DisplaySize,
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
                response = this.clientLicense.AddMenu(command);
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheFullMenuTableKey);
            }
            catch
            {
                return false;
            }

            if (response.MenuId != null)
            {
                Menu.MenuId = Guid.Parse(response.MenuId);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void RefreshCache(string cacheKey, string reloadCache = "true", string cacheType = "Menu")
        {
            string objDataQuery = string.Format("CacheRefreshTable(CacheKey='{0}',ReloadCache={1},CacheType='{2}', TenantID={3})", cacheKey, reloadCache, cacheType, this.MainForm.TenantId);
            string baseUrl = MainForm.ODataEndpointAddress;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(objDataQuery).Result;
        }

        public override string ToString()
        {
            String returnString = string.Concat("MenuId=[", this.MenuId, "]\n",
                "SecurityRightItemId=[", this.SecurityRightItemId, "]\n",
                "Name=[", this.Name, "]\n",
                "DisplaySize=[", this.DisplaySize, "]\n",
                "TenantModuleId=[", this.TenantModuleId, "]\n");

            return returnString;
        }
    }
}
