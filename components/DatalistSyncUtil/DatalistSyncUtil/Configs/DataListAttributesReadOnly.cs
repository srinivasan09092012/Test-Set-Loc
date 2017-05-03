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
using System;
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

        public virtual List<DataList> SearchCodeTables(List<CodeListModel> items, Guid tenantID)
        {
            List<CodeListModel> result = new List<CodeListModel>();
            List<Task> tasks = new List<Task>();
            List<DataListAttribute> listAttributes = new List<DataListAttribute>();
            List<DataList> resultdatalist = new List<DataList>();

            if (!this.Cachemanager.IsSet(this.dataListAttrCombineKey + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        listAttributes = this.GetDataListAttributes(tenantID);
                    }));

                    Task.WaitAll(tasks.ToArray());
                    tasks.Clear();
                }

                resultdatalist = this.UpdateDataListAttributeProperties(listAttributes, items, tenantID);
                this.Cachemanager.Set(this.dataListAttrCombineKey + tenantID.ToString(), resultdatalist, this.cacheTimeInMins);
                resultdatalist = resultdatalist.ToList();
            }
            else
            {
                resultdatalist = this.Cachemanager.Get<List<DataList>>(this.dataListAttrCombineKey + tenantID.ToString()).ToList();
            }

            return resultdatalist;
        }

        public List<DataListItemAttributeModel> GetDataListItemAttributes(Guid tenantID)
        {
            List<DataListItemAttributeModel> itemAttributes = new List<DataListItemAttributeModel>();

            if (!this.Cachemanager.IsSet(this.dataListItemAttrKey + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    itemAttributes = new GetDataListItemAttributesDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(tenantID);
                }

                this.Cachemanager.Set(this.dataListItemAttrKey + tenantID.ToString(), itemAttributes, this.cacheTimeInMins);
            }
            else
            {
                itemAttributes = this.Cachemanager.Get<List<DataListItemAttributeModel>>(this.dataListItemAttrKey + tenantID.ToString());
            }

            return itemAttributes;
        }

        private List<DataList> UpdateDataListAttributeProperties(List<DataListAttribute> attributes, List<CodeListModel> items, Guid tenantID, string moduleID = null)
        {
            List<DataList> result = new List<DataList>();

            string cachedatalistkey = this.dataListAttrKey.Contains("target") == true ? "TargetDataLists" + tenantID.ToString() : "SourceDataLists" + tenantID.ToString();

            if (!this.Cachemanager.IsSet(cachedatalistkey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new SearchDataListDaoHelper(new DataListsDbContext(session, true)).ExecuteDataListsProcedure(tenantID);
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
            this.Cachemanager.Set(this.dataListAttrKey + tenantID.ToString(), result, this.cacheTimeInMins);

            result = result.Where(x => (string.IsNullOrEmpty(moduleID) || x.ModuleID.ToString() == moduleID))
                            .ToList();
            return result;
        }

        private List<DataListAttribute> GetDataListAttributes(Guid tenantID)
        {
            List<DataListAttribute> result = new List<DataListAttribute>();
            if (!this.Cachemanager.IsSet(this.dataListAttrKey + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new GetDataListAttributesDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(tenantID);
                    this.Cachemanager.Set(this.dataListAttrKey + tenantID.ToString(), result, this.cacheTimeInMins);
                }
            }
            else
            {
                result = this.Cachemanager.Get<List<DataListAttribute>>(this.dataListAttrKey + tenantID.ToString());
            }

            return result.Select(x => x).Distinct().ToList();
        }

        private List<DataListItemAttributeModel> ExpandAttributes(CodeListModel toExpand, List<CodeListModel> items, List<DataListAttribute> listAttributes, List<DataListItemAttributeModel> itemAttribues)
        {
            var itemAttrValues = itemAttribues.FindAll(x => x.DataListItemID == toExpand.ID);
            itemAttrValues.ForEach(y => y.DataListAttributeValue = items.Find(c => c.ID == y.DataListValueID).Code);
            itemAttrValues.ForEach(y => y.DataListAttributeName = listAttributes.Find(d => d.ID == y.DataListAttributeID).TypeName);
            itemAttrValues.ForEach(y => y.DataListTypeID = listAttributes.Find(d => d.ID == y.DataListAttributeID).DataListTypeID);
            itemAttrValues.ForEach(y => y.DataListTypeName = listAttributes.Find(d => d.ID == y.DataListAttributeID).DataListTypeName);
            toExpand.Attributes = itemAttrValues;

            return toExpand.Attributes.ToList();
        }
    }
}