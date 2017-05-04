// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Config.DAOHelpers;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace DatalistSyncUtil.Configs
{
    internal class DataListLinksReadOnly
    {
        private string itemLinkerKey = "UtilityDataItemLinkerKey";
        private string dataListLinkKey = "UtilityDataLinkKey";
        private string dataListItemLinkKey = "UtilityDataListItemLinkKey";
        private string dataListLinkCombineKey = "UtilityCombineLinks";     
        private int cacheTimeInMins = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheRefreshTime"]) ? 1440 : int.Parse(ConfigurationManager.AppSettings["CacheRefreshTime"]);
       
        public DataListLinksReadOnly(DbSession session, string envlocation)
        {
            this.Cachemanager = new RedisCacheManager();
            this.ConnectionString = new ConnectionStringSettings("TenantConfig", session.ConnectionString, session.ProviderInvariantName);
            this.itemLinkerKey = this.itemLinkerKey + envlocation;
            this.dataListLinkCombineKey = this.dataListLinkCombineKey + envlocation;
            this.dataListItemLinkKey = this.dataListItemLinkKey + envlocation;
            this.dataListLinkKey = this.dataListLinkKey + envlocation;
        }

        public ICacheManager Cachemanager { get; set; }

        public ConnectionStringSettings ConnectionString { get; set; }

        public virtual List<CodeLinkTable> SearchCodeTables(List<CodeListModel> items)
        {
            List<CodeListModel> result = new List<CodeListModel>();
            List<Task> tasks = new List<Task>();
            List<CodeLinkTable> listLink = new List<CodeLinkTable>();
            List<DataList> resultdatalist = new List<DataList>();

            if (!this.Cachemanager.IsSet(this.dataListLinkCombineKey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        listLink = this.GetDataListItemLinks();
                    }));
                    Task.WaitAll(tasks.ToArray());
                    tasks.Clear();
                }

                 this.Cachemanager.Set(this.dataListLinkCombineKey, listLink, this.cacheTimeInMins);
            }
            else
            {
                resultdatalist = this.Cachemanager.Get<List<DataList>>(this.dataListLinkCombineKey).ToList();
            }
          
            return listLink;
             }

        private List<CodeLinkTable> GetDataListItemLinks()
        {
            List<CodeLinkTable> result = new List<CodeLinkTable>();
            if (!this.Cachemanager.IsSet(this.dataListLinkKey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new GetDataListItemLinksDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
                    this.Cachemanager.Set(this.dataListLinkKey, result, this.cacheTimeInMins);
                }
            }
            else
            {
                result = this.Cachemanager.Get<List<CodeLinkTable>>(this.dataListLinkKey);
            }

            return result.Select(x => x).Distinct().ToList();
        }
     }
}