//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using DatalistSyncUtil.DaoHelpers;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Config;
using HP.HSP.UA3.Core.BAS.CQRS.Config.DAOHelpers;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;

namespace DatalistSyncUtil.Configs
{
    public class AppSettingsReadOnly
    {
        private string cacheAppSettingKey = "FullAppSettingTableKey";
        private string cacheApplicationKey = "FullApplicationTableKey";

        private int cacheTimeInMins = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheRefreshTime"]) ? 1440 : int.Parse(ConfigurationManager.AppSettings["CacheRefreshTime"]);

        private ICacheManager cachemanager;

        public AppSettingsReadOnly(DbSession session, string envLocation)
        {
            this.ConnectionString = new ConnectionStringSettings("TenantConfig", session.ConnectionString, session.ProviderInvariantName);
            this.cachemanager = new RedisCacheManager();
            this.cacheAppSettingKey = envLocation + this.cacheAppSettingKey;
        }

        public AppSettingsReadOnly()
        {
        }

        public ConnectionStringSettings ConnectionString { get; set; }

        public List<AppSettingsModel> SearchAppSetting(Guid tenantID)
        {          
            List<AppSettingsModel> result = null;
            if (!this.cachemanager.IsSet(this.cacheAppSettingKey + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {                    
                    result = new SearchAppSettingsDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(tenantID);
                }

                this.cachemanager.Set(this.cacheAppSettingKey + tenantID.ToString(), result, this.cacheTimeInMins);
            }
            else
            {
                result = this.cachemanager.Get<List<AppSettingsModel>>(this.cacheAppSettingKey + tenantID.ToString()).ToList();
            }

            return result;
        }

        public List<ApplicationModel> GetApplicationName()
        {
            List<ApplicationModel> applicationList = new List<ApplicationModel>();
            if (!this.cachemanager.IsSet(this.cacheApplicationKey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    applicationList = new GetApplicationDaoHelper(new ApplicationDbContext(session, true)).ExecuteProcedure();
                }

                this.cachemanager.Set(this.cacheApplicationKey, applicationList, this.cacheTimeInMins);
            }
            else
            {
                applicationList = this.cachemanager.Get<List<ApplicationModel>>(this.cacheApplicationKey).ToList();
            }

            return applicationList;
        }

        public bool ManageODataCache(Guid tenantID, string cacheAppSettingKey, bool reloadCache = false)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(cacheAppSettingKey + tenantID.ToString()))
            {
                this.cachemanager.Remove(cacheAppSettingKey + tenantID.ToString());

                if (reloadCache)
                {
                    this.ReloadCache(cacheAppSettingKey, tenantID);
                }

                return true;
            }

            return result;
        }

        private void ReloadCache(string cacheKey, Guid tenantID)
        {
            if (cacheKey.ToLower().Equals(this.cacheAppSettingKey.ToLower()))
            {
                this.SearchAppSetting(tenantID);
            }
        }
    }
}
