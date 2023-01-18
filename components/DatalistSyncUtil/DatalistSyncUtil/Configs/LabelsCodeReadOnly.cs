//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Config.DAOHelpers;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace DatalistSyncUtil.Configs
{
    public class LabelsCodeReadOnly
    {
        private string labelsCacheKey = "UtilityLabelCodeTableKey";

        private int cacheTimeInMins = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheRefreshTime"]) ? 1440 : int.Parse(ConfigurationManager.AppSettings["CacheRefreshTime"]);

        public LabelsCodeReadOnly(DbSession session, string envlocation)
        {
            this.Cachemanager = new RedisCacheManager();
            this.ConnectionString = new ConnectionStringSettings("TenantConfig", session.ConnectionString, session.ProviderInvariantName);
            this.labelsCacheKey = envlocation + this.labelsCacheKey;
        }

        public ConnectionStringSettings ConnectionString { get; set; }

        public ICacheManager Cachemanager { get; set; }

        public List<CodeListModel> SearchLabels(Guid tenantID, string contentID = null)
        {
            List<CodeListModel> result = new List<CodeListModel>();
            List<Task> tasks = new List<Task>();
            List<Languages> languages = new List<Languages>();

            if (!this.Cachemanager.IsSet(this.labelsCacheKey + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        result = new SearchDataListItemsDaoHelper(new DataListsDbContext(session, true)).ExecuteLabelProcedure(tenantID);
                    }));

                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        languages = new SearchDataListLanguagesDaoHelper(new DataListsDbContext(session, true)).ExecuteLabelProcedure(tenantID);
                    }));

                    Task.WaitAll(tasks.ToArray());
                    tasks.Clear();
                }

                result.ForEach(x => x.LanguageList = languages.FindAll(c => c.CodeID == x.ID));

                this.Cachemanager.Set(this.labelsCacheKey + tenantID.ToString(), result, this.cacheTimeInMins);

                result = result.Where(x => (string.IsNullOrEmpty(contentID) || x.ContentID == contentID))
                                .ToList();
            }
            else
            {
                result = this.Cachemanager.Get<List<CodeListModel>>(this.labelsCacheKey + tenantID.ToString())
                    .Where(x => (string.IsNullOrEmpty(contentID) || x.ContentID == contentID))
                    .ToList();
            }

            return result;
        }
    }
}
