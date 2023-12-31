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
using System.Linq;

namespace DatalistSyncUtil.Configs
{
    public class ServicesReadOnly
    {
        private string serviceTableKey = "UtilityFullServiceTableKey";
        
        private int cacheTimeInMins = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheRefreshTime"]) ? 1440 : int.Parse(ConfigurationManager.AppSettings["CacheRefreshTime"]);

        public ServicesReadOnly(DbSession session, string envlocation)
        {
            this.Cachemanager = new RedisCacheManager();
            this.ConnectionString = new ConnectionStringSettings("TenantConfig", session.ConnectionString, session.ProviderInvariantName);
            this.serviceTableKey = envlocation + this.serviceTableKey;
        }

        public ConnectionStringSettings ConnectionString { get; set; }

        private ICacheManager Cachemanager { get; set; }

        /// <summary>
        /// Searches the Service Table.
        /// </summary>
        /// <returns>List<ServiceListModel></returns>
        public List<ServiceListModel> SearchServices(Guid tenantID)
        {
            List<ServiceListModel> result = null;
            if (!this.Cachemanager.IsSet(this.serviceTableKey + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new GetServiceDaoHelper(
                        new ServiceDbContext(session, true),
                        this.Cachemanager).ExecuteProcedure(tenantID);
                }

                this.Cachemanager.Set(this.serviceTableKey + tenantID.ToString(), result, this.cacheTimeInMins);
            }
            else
            {
                result = this.Cachemanager.Get<List<ServiceListModel>>(this.serviceTableKey + tenantID.ToString()).ToList();
            }

            return result;
        }
    }
}
