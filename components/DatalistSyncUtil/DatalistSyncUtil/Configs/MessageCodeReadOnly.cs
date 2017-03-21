
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
        private const string MessagesCacheKey = "UtilityMessageCodeTableKey";
        private const string DataListAttrKey = "UtilityMessageAttrKey";
        private const string DataListItemAttrKey = "UtilityMessageItemAttrKey";
        private ICacheManager cachemanager;
        private int cacheTimeInMins = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheRefreshTime"]) ? 1440 : int.Parse(ConfigurationManager.AppSettings["CacheRefreshTime"]);

       

        public MessageCodeReadOnly(DbSession session) 
        {
            this.cachemanager = new RedisCacheManager();
            this.ConnectionString = new ConnectionStringSettings("TenantConfig", session.ConnectionString, session.ProviderInvariantName);
        }

        public ConnectionStringSettings ConnectionString { get; set; }

        public List<CodeListModel> SearchMessages(string contentID = null, string tenantID = null)
        {
            List<CodeListModel> result = new List<CodeListModel>();
            List<Task> tasks = new List<Task>();
            List<Languages> languages = new List<Languages>();
          

            if (!this.cachemanager.IsSet(MessagesCacheKey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        result = new SearchDataListItemsDaoHelper(new DataListsDbContext(session, true)).ExecuteMessageProcedure();
                    }));

                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        languages = new SearchDataListLanguagesDaoHelper(new DataListsDbContext(session, true)).ExecuteMessageProcedure();
                    }));

                    Task.WaitAll(tasks.ToArray());
                    tasks.Clear();

                    if (!this.cachemanager.IsSet(DataListAttrKey))
                    {
                        tasks.Add(Task.Factory.StartNew(() =>
                        {
                            this.GetDataListAttributes();
                        }));
                    }

                    if (!this.cachemanager.IsSet(DataListItemAttrKey))
                    {
                        tasks.Add(Task.Factory.StartNew(() =>
                        {
                            this.GetDataListItemAttributes();
                        }));
                    }

                    Task.WaitAll(tasks.ToArray());
                    tasks.Clear();
                }

                result.ForEach(x => x.LanguageList = languages.FindAll(c => c.CodeID == x.ID));

                List<DataListAttribute> listAttributes = this.GetDataListAttributes();
                List<DataListItemAttributeModel> itemAttributes = this.GetDataListItemAttributes();
                result.ForEach(x => x.Attributes = this.ExpandAttributes(x, result, listAttributes, itemAttributes));

                this.cachemanager.Set(MessagesCacheKey, result, this.cacheTimeInMins);
                result = result.Where(x => (string.IsNullOrEmpty(contentID) || x.ContentID == contentID)
                                        && (string.IsNullOrEmpty(tenantID) || x.TenantID.ToString() == tenantID))
                                .ToList();
            }
            else
            {

                result = this.cachemanager.Get<List<CodeListModel>>(MessagesCacheKey)
                     .Where(x => (string.IsNullOrEmpty(contentID) || x.ContentID == contentID)
                              && (string.IsNullOrEmpty(tenantID) || x.TenantID.ToString() == tenantID))
                     .ToList();
            }
              
            
            return result;
        }

        public List<DataListItemAttributeModel> ExpandAttributes(CodeListModel toExpand, List<CodeListModel> items, List<DataListAttribute> listAttributes, List<DataListItemAttributeModel> itemAttribues)
        {
            if (!this.cachemanager.IsSet(DataListItemAttrKey))
            {
                this.GetDataListItemAttributes();
            }

            var itemAttrValues = itemAttribues.FindAll(x => x.DataListItemID == toExpand.ID);
            itemAttrValues.ForEach(y => y.DataListAttributeValue = items.Find(c => c.ID == y.DataListValueID).Code);
            itemAttrValues.ForEach(y => y.DataListAttributeName = listAttributes.Find(d => d.ID == y.DataListAttributeID).TypeName);
            itemAttrValues.ForEach(y => y.DataListTypeID = listAttributes.Find(d => d.ID == y.DataListAttributeID).DataListTypeID);
            itemAttrValues.ForEach(y => y.DataListTypeName = listAttributes.Find(d => d.ID == y.DataListAttributeID).DataListTypeName);
            toExpand.Attributes = itemAttrValues;

            return toExpand.Attributes.ToList();
        }

        private List<DataListAttribute> GetDataListAttributes()
        {
            List<DataListAttribute> result = new List<DataListAttribute>();
            if (!this.cachemanager.IsSet(DataListAttrKey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new GetDataListAttributesDaoHelper(new DataListAttributeDbContext(session, true)).ExecuteProcedure();
                    this.cachemanager.Set(DataListAttrKey, result, this.cacheTimeInMins);
                }
            }
            else
            {
                result = this.cachemanager.Get<List<DataListAttribute>>(DataListAttrKey);
            }

            return result;
        }

        private List<DataListItemAttributeModel> GetDataListItemAttributes()
        {
            DataListItemAttributeModel dataItem = null;
            List<DataListItemAttributeModel> itemAtttributes = new List<DataListItemAttributeModel>();
            List<DataListItemAttribute> attrValues = null;

            if (!this.cachemanager.IsSet(DataListItemAttrKey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    attrValues = new GetDataListItemAttributesDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
                }

                foreach (DataListItemAttribute item in attrValues)
                {
                    dataItem = new DataListItemAttributeModel()
                    {
                        DataListAttributeID = item.DataListAttributeID,
                        DataListAttributeName = string.Empty,
                        DataListAttributeValue = string.Empty,
                        DataListItemID = item.DataListItemID,
                        DataListValueID = item.DataListValueID,
                        ID = item.ID,
                        LastModifiedDate = item.LastModifiedDate
                    };

                    itemAtttributes.Add(dataItem);
                }

                this.cachemanager.Set(DataListItemAttrKey, itemAtttributes, this.cacheTimeInMins);
            }
            else
            {
                itemAtttributes = this.cachemanager.Get<List<DataListItemAttributeModel>>(DataListItemAttrKey);
            }

            return itemAtttributes;
        }
    }
}
