//-----------------------------------------------------------------------------------------
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
    public class SecurityCodeReadOnly
    {
        private const string LocalLanguageKey = "UtilitySecurityLanguagesKey";
        private const string CodeTablesKey = "UtilitySecurityCodeTableKey";
        private const string DataListsKey = "UtilitySecurityDataListKey";
        private const string PlainDataListsKey = "SecurityDataListsKey";
        private const string ItemLinkerKey = "UtilityItemLinkerKey";
        private const string DataListAttrKey = "UtilityDataListAttrKey";
        private const string DataListItemAttrKey = "UtilitySecurityItemAttrKey";
        private ICacheManager cachemanager;
        private int cacheTimeInMins = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheRefreshTime"]) ? 1440 : int.Parse(ConfigurationManager.AppSettings["CacheRefreshTime"]);


        public SecurityCodeReadOnly(DbSession session)
        {
            this.ConnectionString = new ConnectionStringSettings("TenantConfig", session.ConnectionString, session.ProviderInvariantName);
            this.cachemanager = new RedisCacheManager();
        }

        public ConnectionStringSettings ConnectionString { get; set; }


        

        /// <summary>
        /// Searches the CodeTables.
        /// </summary>
        /// <param name="query">CodeTablesQuery</param>
        /// <returns>List<CodeTablesModel></returns>
        public List<CodeListModel> SearchCodeTables(string contentID = null, string tenantID = null, bool expandChildren = false, bool expandParents = false, bool expandAttributes = false)
        {
            List<CodeListModel> result = new List<CodeListModel>();
            List<Task> tasks = new List<Task>();
            List<Languages> languages = null;

            if (!this.cachemanager.IsSet(CodeTablesKey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        result = new SearchDataListItemsDaoHelper(new DataListsDbContext(session, true)).ExecuteSecurityProcedure(contentID);
                    }));

                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        languages = this.GetLocalizedLanguages(session);
                    }));

                    if (!this.cachemanager.IsSet(ItemLinkerKey) && (expandChildren || expandParents))
                    {
                        tasks.Add(Task.Factory.StartNew(() =>
                        {
                            var linkers = new GetDataListItemLinksDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
                            this.cachemanager.Set(ItemLinkerKey, linkers, this.cacheTimeInMins);
                        }));
                    }

                    if (!this.cachemanager.IsSet(PlainDataListsKey) && expandAttributes)
                    {
                        tasks.Add(Task.Factory.StartNew(() =>
                        {
                            var dataList = new SearchDataListDaoHelper(new DataListsDbContext(session, true)).ExecuteSecurityProcedure(tenantID);
                            this.cachemanager.Set(PlainDataListsKey, dataList, this.cacheTimeInMins);
                        }));
                    }

                    if (!this.cachemanager.IsSet(DataListAttrKey) && expandAttributes)
                    {
                        tasks.Add(Task.Factory.StartNew(() =>
                        {
                            this.GetDataListAttributes();
                        }));
                    }

                    if (!this.cachemanager.IsSet(DataListItemAttrKey) && expandAttributes)
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

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    List<CodeLinkTable> itemLinks = this.GetDataListItemLinks();
                    result.ForEach(x => x.Children = this.ExpandChildren(x, result, itemLinks));
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    List<DataListAttribute> listAttributes = this.GetDataListAttributes();
                    List<DataListItemAttributeModel> itemAttributes = this.GetDataListItemAttributes();
                    result.ForEach(x => x.Attributes = this.ExpandAttributes(x, result, listAttributes, itemAttributes));
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    List<CodeLinkTable> itemLinks = this.GetDataListItemLinks();
                    result.ForEach(x => x.Parents = this.ExpandParent(x, result, itemLinks));
                }));

                Task.WaitAll(tasks.ToArray());
                tasks.Clear();

                this.cachemanager.Set(CodeTablesKey, result, this.cacheTimeInMins);

                result = result.Where(x => (string.IsNullOrEmpty(contentID) || x.ContentID == contentID)
                                        && (string.IsNullOrEmpty(tenantID) || x.TenantID.ToString() == tenantID))
                                .ToList();
            }
            else
            {
                result = this.cachemanager.Get<List<CodeListModel>>(CodeTablesKey)
                    .Where(x => (string.IsNullOrEmpty(contentID) || x.ContentID == contentID)
                             && (string.IsNullOrEmpty(tenantID) || x.TenantID.ToString() == tenantID))
                    .ToList();
            }

            return result;
        }

        /// <summary>
        /// Gets Data Lists
        /// </summary>
        /// <param name="tenantID">string</param>
        /// <param name="moduleID">string</param>
        /// <returns>List<DataList></returns>
        public List<DataList> SearchDataList(string tenantID = null, string moduleID = null)
        {
            List<DataList> result = new List<DataList>();

            if (!this.cachemanager.IsSet(DataListsKey))
            {
                this.SearchCodeTables();
                if (!this.cachemanager.IsSet(PlainDataListsKey))
                {
                    using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                    {
                        result = new SearchDataListDaoHelper(new DataListsDbContext(session, true)).ExecuteSecurityProcedure(tenantID);
                    }

                    this.cachemanager.Set(PlainDataListsKey, result, this.cacheTimeInMins);
                }
                else
                {
                    result = this.cachemanager.Get<List<DataList>>(PlainDataListsKey);
                }

                List<CodeListModel> items = this.cachemanager.Get<List<CodeListModel>>(CodeTablesKey);
                var attributes = this.GetDataListAttributes();
                result.ForEach(x => x.DataListAttributes = attributes.FindAll(c => c.DataListID == x.ID));
                result.ForEach(x => x.DataListAttributes.ForEach(y => y.DataListTypeName = result.Find(p => p.ID == y.DataListTypeID).ContentID));
                result.ForEach(x => x.DataListAttributes.ForEach(y => y.DefaultTypeText = items.Find(c => c.ID == y.DefaultTypeValue).Code));
                this.cachemanager.Set(DataListsKey, result, this.cacheTimeInMins);

                result = result.Where(x => (string.IsNullOrEmpty(moduleID) || x.ModuleID.ToString() == moduleID)
                                        && (string.IsNullOrEmpty(tenantID) || x.TenantID.ToString() == tenantID))
                                .ToList();
            }
            else
            {
                result = this.cachemanager.Get<List<DataList>>(DataListsKey)
                    .Where(x => (string.IsNullOrEmpty(moduleID) || x.ModuleID.ToString() == moduleID)
                             && (string.IsNullOrEmpty(tenantID) || x.TenantID.ToString() == tenantID))
                    .ToList();
            }

            return result;
        }

        public List<Languages> GetLocalizedLanguages(IDbSession session = null)
        {
            List<Languages> result = new List<Languages>();
            if (!this.cachemanager.IsSet(LocalLanguageKey))
            {
                if (session != null)
                {
                    result = new SearchDataListLanguagesDaoHelper(new DataListsDbContext(session, true)).ExecuteSecurityProcedure();
                }
                else
                {
                    using (IDbSession session1 = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                    {
                        result = new SearchDataListLanguagesDaoHelper(new DataListsDbContext(session1, true)).ExecuteSecurityProcedure();
                    }
                }

                this.cachemanager.Set(LocalLanguageKey, result, this.cacheTimeInMins);
            }
            else
            {
                result = this.cachemanager.Get<List<Languages>>(LocalLanguageKey);
            }

            return result;
        }

        public List<CodeListModel> ExpandChildren(CodeListModel toExpand, List<CodeListModel> items, List<CodeLinkTable> itemLinks)
        {
            CodeListModel listItem = null;
            CodeListModel newCodeListModel = null;
            var links = itemLinks.FindAll(x => x.ParentID == toExpand.ID);
            toExpand.Children = new List<CodeListModel>();

            foreach (CodeLinkTable link in links)
            {
                listItem = items.Find(x => x.ID == link.ChildID);

                if (listItem != null)
                {
                    newCodeListModel = new CodeListModel()
                    {
                        ID = listItem.ID,
                        ContentID = listItem.ContentID,
                        TenantID = listItem.TenantID,
                        OrderIndex = listItem.OrderIndex,
                        LanguageList = listItem.LanguageList,
                        Children = listItem.Children,
                        Attributes = listItem.Attributes,
                        Parents = listItem.Parents,
                        EffectiveStartDate = listItem.EffectiveStartDate,
                        EffectiveEndDate = listItem.EffectiveEndDate,
                        IsActive = link.IsActive,
                        Code = listItem.Code
                    };

                    toExpand.Children.Add(newCodeListModel);
                }
            }

            return toExpand.Children.ToList();
        }

        public List<CodeListModel> ExpandParent(CodeListModel toExpand, List<CodeListModel> items, List<CodeLinkTable> itemLinks)
        {
            CodeListModel listItem = null;
            CodeListModel newCodeListModel = null;

            var links = itemLinks.FindAll(x => x.ChildID == toExpand.ID);
            toExpand.Parents = new List<CodeListModel>();
            foreach (CodeLinkTable link in links)
            {
                listItem = items.Find(x => x.ID == link.ParentID);

                if (listItem != null)
                {
                    newCodeListModel = new CodeListModel()
                    {
                        ID = listItem.ID,
                        ContentID = listItem.ContentID,
                        TenantID = listItem.TenantID,
                        OrderIndex = listItem.OrderIndex,
                        LanguageList = listItem.LanguageList,
                        Children = listItem.Children,
                        Attributes = listItem.Attributes,
                        Parents = listItem.Parents,
                        EffectiveStartDate = listItem.EffectiveStartDate,
                        EffectiveEndDate = listItem.EffectiveEndDate,
                        IsActive = link.IsActive,
                        Code = listItem.Code
                    };

                    toExpand.Parents.Add(newCodeListModel);
                }
            }

            return toExpand.Parents.ToList();
        }

        public List<DataListItemAttributeModel> ExpandAttributes(CodeListModel toExpand, List<CodeListModel> items, List<DataListAttribute> listAttributes, List<DataListItemAttributeModel> itemAttribues)
        {
            if (!this.cachemanager.IsSet(DataListItemAttrKey))
            {
                this.SearchDataList();
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

        private List<CodeLinkTable> GetDataListItemLinks()
        {
            List<CodeLinkTable> linkers = null;

            if (!this.cachemanager.IsSet(ItemLinkerKey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    linkers = new GetDataListItemLinksDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
                }

                this.cachemanager.Set(ItemLinkerKey, linkers, this.cacheTimeInMins);
            }
            else
            {
                linkers = this.cachemanager.Get<List<CodeLinkTable>>(ItemLinkerKey);
            }

            return linkers;
        }
    }
}
