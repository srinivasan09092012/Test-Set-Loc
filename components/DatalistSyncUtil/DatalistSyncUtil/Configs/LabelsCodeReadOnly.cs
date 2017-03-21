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
        private const string LabelsCacheKey = "UtilityLabelCodeTableKey";
        public ICacheManager cachemanager { get; set;}
        private int cacheTimeInMins = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheRefreshTime"]) ? 1440 : int.Parse(ConfigurationManager.AppSettings["CacheRefreshTime"]);
        public LabelsCodeReadOnly(DbSession session)
        {
            this.cachemanager = new RedisCacheManager();
            this.ConnectionString = new ConnectionStringSettings("TenantConfig", session.ConnectionString, session.ProviderInvariantName);
        }

        public ConnectionStringSettings ConnectionString { get; set; }

        public List<CodeListModel> SearchLabels(string contentID = null, string tenantID = null)
        {
            List<CodeListModel> result = new List<CodeListModel>();
            List<Task> tasks = new List<Task>();
            List<Languages> languages = new List<Languages>();

            if (!this.cachemanager.IsSet(LabelsCacheKey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        result = new SearchDataListItemsDaoHelper(new DataListsDbContext(session, true)).ExecuteLabelProcedure();
                    }));

                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        languages = new SearchDataListLanguagesDaoHelper(new DataListsDbContext(session, true)).ExecuteLabelProcedure();
                    }));

                    Task.WaitAll(tasks.ToArray());
                    tasks.Clear();
                }

                result.ForEach(x => x.LanguageList = languages.FindAll(c => c.CodeID == x.ID));

                this.cachemanager.Set(LabelsCacheKey, result, this.cacheTimeInMins);

                result = result.Where(x => (string.IsNullOrEmpty(contentID) || x.ContentID == contentID)
                                        && (string.IsNullOrEmpty(tenantID) || x.TenantID.ToString() == tenantID))
                                .ToList();
            }

            else
            {
                result = this.cachemanager.Get<List<CodeListModel>>(LabelsCacheKey)
                    .Where(x => (string.IsNullOrEmpty(contentID) || x.ContentID == contentID)
                             && (string.IsNullOrEmpty(tenantID) || x.TenantID.ToString() == tenantID))
                    .ToList();
            }

             
            return result;
        }
        
    }
}
