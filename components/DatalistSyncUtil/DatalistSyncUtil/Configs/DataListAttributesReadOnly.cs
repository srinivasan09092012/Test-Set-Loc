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
    internal class DataListAttributesReadOnly
    {
        private string itemLinkerKey = "UtilityDataItemLinkerKey";
        private string dataListAttrKey = "UtilityDataAttrKey";
        private string dataListItemAttrKey = "UtilityDataListItemAttrKey";
        private string dataListAttrCombineKey = "UtilityCombineAttributes";     
        private int cacheTimeInMins = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheRefreshTime"]) ? 1440 : int.Parse(ConfigurationManager.AppSettings["CacheRefreshTime"]);
       
        public DataListAttributesReadOnly(DbSession session, string envlocation)
        {
            this.Cachemanager = new RedisCacheManager();
            this.ConnectionString = new ConnectionStringSettings("TenantConfig", session.ConnectionString, session.ProviderInvariantName);
            this.itemLinkerKey = this.itemLinkerKey + envlocation;
            this.dataListAttrCombineKey = this.dataListAttrCombineKey + envlocation;
            this.dataListItemAttrKey = this.dataListItemAttrKey + envlocation;
            this.dataListAttrKey = this.dataListAttrKey + envlocation;
        }

        public ICacheManager Cachemanager { get; set; }

        public ConnectionStringSettings ConnectionString { get; set; }

        public virtual List<DataList> SearchCodeTables(List<CodeListModel> items)
        {
            List<CodeListModel> result = new List<CodeListModel>();
            List<Task> tasks = new List<Task>();
            List<DataListAttribute> listAttributes = new List<DataListAttribute>();
            List<DataList> resultdatalist = new List<DataList>();

            if (!this.Cachemanager.IsSet(this.dataListAttrCombineKey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        listAttributes = this.GetDataListAttributes();
                    }));

                    Task.WaitAll(tasks.ToArray());
                    tasks.Clear();
                }

                resultdatalist = this.UpdateDataListAttributeProperties(listAttributes, items);
                this.Cachemanager.Set(this.dataListAttrCombineKey, resultdatalist, this.cacheTimeInMins);
                resultdatalist = resultdatalist.ToList();
            }
            else
            {
                resultdatalist = this.Cachemanager.Get<List<DataList>>(this.dataListAttrCombineKey).ToList();
            }

            return resultdatalist;
        }

        public List<ItemDataListItemAttributeVal> GetDataListItemAttributes()
        {
            ItemDataListItemAttributeVal dataItem = null;
            List<ItemDataListItemAttributeVal> itemAtttributes = new List<ItemDataListItemAttributeVal>();
            List<DataListItemAttribute> attrValues = null;

            if (!this.Cachemanager.IsSet(this.dataListItemAttrKey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    attrValues = new GetDataListItemAttributesDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
                }

                foreach (DataListItemAttribute item in attrValues)
                {
                    dataItem = new ItemDataListItemAttributeVal()
                    {
                        DataListAttributeID = item.DataListAttributeID,
                        DataListAttributeName = string.Empty,
                        DataListAttributeValue = string.Empty,
                        DataListItemID = item.DataListItemID,
                        DataListValueID = item.DataListValueID,
                        DataListTypeName = string.Empty,
                        ID = item.ID,
                        LastModifiedDate = item.LastModifiedDate
                    };

                    itemAtttributes.Add(dataItem);
                }

                this.Cachemanager.Set(this.dataListItemAttrKey, itemAtttributes, this.cacheTimeInMins);
            }
            else
            {
                itemAtttributes = this.Cachemanager.Get<List<ItemDataListItemAttributeVal>>(this.dataListItemAttrKey);
            }

            return itemAtttributes;
        }

        private List<DataList> UpdateDataListAttributeProperties(List<DataListAttribute> attributes, List<CodeListModel> items, string tenantID = null, string moduleID = null)
        {
            List<DataList> result = new List<DataList>();

            string cachedatalistkey = this.dataListAttrKey.Contains("target") == true ? "TargetDataLists" : "SourceDataLists";

            if (!this.Cachemanager.IsSet(cachedatalistkey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new SearchDataListDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
                }

                this.Cachemanager.Set(cachedatalistkey, result, this.cacheTimeInMins);
            }
            else
            {
                result = this.Cachemanager.Get<List<DataList>>(cachedatalistkey);
            }

            result.ForEach(x => x.DataListAttributes = attributes.FindAll(c => c.DataListID == x.ID));
            result.ForEach(x => x.DataListAttributes.ForEach(y => y.DataListTypeName = result.Find(p => p.ID == y.DataListTypeID).ContentID));
            result.ForEach(x => x.DataListAttributes.ForEach(y => y.DefaultTypeText = items.Find(c => c.ID == y.DefaultTypeValue).Code));
            this.Cachemanager.Set(this.dataListAttrKey, result, this.cacheTimeInMins);

            result = result.Where(x => (string.IsNullOrEmpty(moduleID) || x.ModuleID.ToString() == moduleID)
                                    && (string.IsNullOrEmpty(tenantID) || x.TenantID.ToString() == tenantID))
                            .ToList();
            return result;
        }

        private List<DataListAttribute> GetDataListAttributes()
        {
            List<DataListAttribute> result = new List<DataListAttribute>();
            if (!this.Cachemanager.IsSet(this.dataListAttrKey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new GetDataListAttributesDaoHelper(new DataListAttributeDbContext(session, true)).ExecuteProcedure();
                    this.Cachemanager.Set(this.dataListAttrKey, result, this.cacheTimeInMins);
                }
            }
            else
            {
                result = this.Cachemanager.Get<List<DataListAttribute>>(this.dataListAttrKey);
            }

            return result.Select(x => x).Distinct().ToList();
        }    
    }
}