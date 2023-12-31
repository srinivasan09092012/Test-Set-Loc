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
    public class MessageCodeReadOnly
    {
        private string messagesCacheKey = "UtilityMessageCodeTableKey";
        private string dataListAttrKey = "UtilityMessageAttrKey";
        private string dataListItemAttrKey = "UtilityMessageItemAttrKey";
        private ICacheManager cachemanager;
        private int cacheTimeInMins = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheRefreshTime"]) ? 1440 : int.Parse(ConfigurationManager.AppSettings["CacheRefreshTime"]);

        public MessageCodeReadOnly(DbSession session, string envLocation)
        {
            this.cachemanager = new RedisCacheManager();
            this.ConnectionString = new ConnectionStringSettings("TenantConfig", session.ConnectionString, session.ProviderInvariantName);
            this.messagesCacheKey = envLocation + this.messagesCacheKey;
            this.dataListAttrKey = envLocation + this.dataListAttrKey;
            this.dataListItemAttrKey = envLocation + this.dataListItemAttrKey;
        }

        public ConnectionStringSettings ConnectionString { get; set; }

        public List<CodeListModel> SearchMessages(Guid tenantID, string contentID = null)
        {
            List<CodeListModel> result = new List<CodeListModel>();
            List<Task> tasks = new List<Task>();
            List<Languages> languages = new List<Languages>();

            if (!this.cachemanager.IsSet(this.messagesCacheKey + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        result = new SearchDataListItemsDaoHelper(new DataListsDbContext(session, true)).ExecuteMessageProcedure(tenantID);
                    }));

                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        languages = new SearchDataListLanguagesDaoHelper(new DataListsDbContext(session, true)).ExecuteMessageProcedure(tenantID);
                    }));

                    Task.WaitAll(tasks.ToArray());
                    tasks.Clear();

                    if (!this.cachemanager.IsSet(this.dataListAttrKey + tenantID.ToString()))
                    {
                        tasks.Add(Task.Factory.StartNew(() =>
                        {
                            this.GetDataListAttributes(tenantID);
                        }));
                    }

                    if (!this.cachemanager.IsSet(this.dataListItemAttrKey + tenantID.ToString()))
                    {
                        tasks.Add(Task.Factory.StartNew(() =>
                        {
                            this.GetDataListItemAttributes(tenantID);
                        }));
                    }

                    Task.WaitAll(tasks.ToArray());
                    tasks.Clear();
                }

                result.ForEach(x => x.LanguageList = languages.FindAll(c => c.CodeID == x.ID));

                List<DataListAttribute> listAttributes = this.GetDataListAttributes(tenantID);
                List<DataListItemAttributeModel> itemAttributes = this.GetDataListItemAttributes(tenantID);
                result.ForEach(x => x.Attributes = this.ExpandAttributes(x, result, listAttributes, itemAttributes, tenantID));

                this.cachemanager.Set(this.messagesCacheKey + tenantID.ToString(), result, this.cacheTimeInMins);
                result = result.Where(x => (string.IsNullOrEmpty(contentID) || x.ContentID == contentID))
                                .ToList();
            }
            else
            {
                result = this.cachemanager.Get<List<CodeListModel>>(this.messagesCacheKey + tenantID.ToString())
                     .Where(x => (string.IsNullOrEmpty(contentID) || x.ContentID == contentID))
                     .ToList();
            }

            return result;
        }

        public List<DataListItemAttributeModel> ExpandAttributes(CodeListModel toExpand, List<CodeListModel> items, List<DataListAttribute> listAttributes, List<DataListItemAttributeModel> itemAttribues, Guid tenantID)
        {
            if (!this.cachemanager.IsSet(this.dataListItemAttrKey + tenantID.ToString()))
            {
                this.GetDataListItemAttributes(tenantID);
            }

            var itemAttrValues = itemAttribues.FindAll(x => x.DataListItemID == toExpand.ID);
            itemAttrValues.ForEach(y => y.DataListAttributeValue = items.Find(c => c.ID == y.DataListValueID).Code);
            itemAttrValues.ForEach(y => y.DataListAttributeName = listAttributes.Find(d => d.ID == y.DataListAttributeID).TypeName);
            itemAttrValues.ForEach(y => y.DataListTypeID = listAttributes.Find(d => d.ID == y.DataListAttributeID).DataListTypeID);
            itemAttrValues.ForEach(y => y.DataListTypeName = listAttributes.Find(d => d.ID == y.DataListAttributeID).DataListTypeName);
            toExpand.Attributes = itemAttrValues;

            return toExpand.Attributes.ToList();
        }

        private List<DataListAttribute> GetDataListAttributes(Guid tenantID)
        {
            List<DataListAttribute> result = new List<DataListAttribute>();
            if (!this.cachemanager.IsSet(this.dataListAttrKey + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new GetDataListAttributesDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(tenantID);
                    this.cachemanager.Set(this.dataListAttrKey + tenantID.ToString(), result, this.cacheTimeInMins);
                }
            }
            else
            {
                result = this.cachemanager.Get<List<DataListAttribute>>(this.dataListAttrKey + tenantID.ToString());
            }

            return result;
        }

        private List<DataListItemAttributeModel> GetDataListItemAttributes(Guid tenantID)
        {
            List<DataListItemAttributeModel> itemAtttributes = new List<DataListItemAttributeModel>();

            if (!this.cachemanager.IsSet(this.dataListItemAttrKey + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    itemAtttributes = new GetDataListItemAttributesDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(tenantID);
                }

                this.cachemanager.Set(this.dataListItemAttrKey + tenantID.ToString(), itemAtttributes, this.cacheTimeInMins);
            }
            else
            {
                itemAtttributes = this.cachemanager.Get<List<DataListItemAttributeModel>>(this.dataListItemAttrKey + tenantID.ToString());
            }

            return itemAtttributes;
        }
    }
}
