//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Config.DAOHelpers;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;

namespace DatalistSyncUtil.Configs
{
    public class MenusReadOnly 
    {
        private string menuTableKey = "FullMenuTableKey";
        private string menuItemTableKey = "MenuItemTableKey";
        private ICacheManager cachemanager;
        private int cacheTimeInMins = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheRefreshTime"]) ? 1440 : int.Parse(ConfigurationManager.AppSettings["CacheRefreshTime"]);

        public MenusReadOnly(DbSession session, string envlocation)
        {
            this.cachemanager = new RedisCacheManager();
            this.ConnectionString = new ConnectionStringSettings("TenantConfig", session.ConnectionString, session.ProviderInvariantName);
            this.menuTableKey = this.menuTableKey + envlocation;
            this.menuItemTableKey = this.menuItemTableKey + this.menuTableKey + envlocation;
        }

        public ICacheManager Cachemanager { get; set; }

        public ConnectionStringSettings ConnectionString { get; set; }

        /// <summary>
        /// Searches the Menu Table.
        /// </summary>
        /// <returns>List<MenuListModel></returns>
        public List<MenuListModel> SearchMenus(Guid tenantID, bool expandChildren)
        {
            List<MenuListModel> result = null;
            if (!this.cachemanager.IsSet(this.menuTableKey + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new GetMenuDaoHelper(
                        new MenuDbContext(session, true),
                         this.cachemanager).ExecuteProcedure(tenantID);
                }

                this.cachemanager.Set(this.menuTableKey + tenantID.ToString(), result, this.cacheTimeInMins);
            }
            else
            {
                result = this.cachemanager.Get<List<MenuListModel>>(this.menuTableKey + tenantID.ToString()).ToList();
            }

            if (expandChildren)
            {
                List<MenuItemModel> languages = null;
                if (!this.cachemanager.IsSet(this.menuItemTableKey + tenantID.ToString()))
                {
                    using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                    {
                        languages = new GetMenuItemDaoHelper(new MenuDbContext(session, true)).ExecuteProcedure(tenantID);
                    }

                    this.cachemanager.Set(this.menuItemTableKey + tenantID.ToString(), languages, this.cacheTimeInMins);
                }
                else
                {
                    languages = this.cachemanager.Get<List<MenuItemModel>>(this.menuItemTableKey + tenantID.ToString());
                }

                result.ForEach(x => x.Children = this.ExpandChildren(x, languages));
            }

            this.ManageODataCache(tenantID, this.menuTableKey, false);
            this.ManageODataCache(tenantID, this.menuItemTableKey, false);
            return result;
            ///return languages;
        }

        public List<MenuItemModel> ExpandChildren(MenuListModel toExpand, List<MenuItemModel> languages)
        {
            var menuItemValues = languages.FindAll(x => x.MenuID.Equals(toExpand.ID));
            this.FindAndSetLevels(menuItemValues.ToList<MenuItemModel>(), null, 1, languages);
            toExpand.Children = menuItemValues;

            return toExpand.Children.ToList();
        }

        public bool ManageODataCache(Guid tenantID, string cacheKey, bool reloadCache = false)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(cacheKey + tenantID.ToString()))
            {
                this.cachemanager.Remove(cacheKey + tenantID.ToString());

                if (reloadCache)
                {
                    this.ReloadCache(tenantID, cacheKey);
                }

                return true;
            }

            return result;
        }

        private void ReloadCache(Guid tenantID, string cacheKey)
        {
            if (cacheKey.ToLower().Equals(this.menuTableKey.ToLower() + tenantID.ToString()))
            {
                this.cachemanager.Remove(this.menuItemTableKey + tenantID.ToString());
                this.SearchMenus(tenantID, true);
            }
        }

        private void FindAndSetLevels(List<MenuItemModel> menuItemValues, string stringID, int level, List<MenuItemModel> languages)
        {
            Guid id;
            List<MenuItemModel> menuItems = null;
            if (stringID != null)
            {
                id = new Guid(stringID);
                menuItems = languages.FindAll(x => x.ParentMenuItemID.Equals(id));
            }
            else
            {
                menuItems = languages.FindAll(x => x.ParentMenuItemID.Equals(null));
            }

            foreach (MenuItemModel menuItem in menuItems)
            {
                menuItem.Level = level;
                this.FindAndSetLevels(menuItemValues, menuItem.ID.ToString(), level + 1, languages);
            }
        }
    }
}
