//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using DatalistSyncUtil.Configs;
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
using System.Threading.Tasks;

namespace DatalistSyncUtil
{
    public class SourceTenantHelper
    {
        public SourceTenantHelper()
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["SourceDataList"];
            this.Cache = new RedisCacheManager();
        }

        public SourceTenantHelper(ConnectionStringSettings connection)
        {
            this.ConnectionString = connection;
            this.Cache = new RedisCacheManager();
        }

        public ConnectionStringSettings ConnectionString { get; set; }

        public ICacheManager Cache { get; set; }

        public List<TenantModel> GetTenants()
        {
            List<TenantModel> result = null;

            if (!this.Cache.IsSet("SourceTenants"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new GetTenantDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
                }

                this.Cache.Set("SourceTenants", result.OrderBy(o => o.TenantName).ToList(), 1440);
            }
            else
            {
                result = this.Cache.Get<List<TenantModel>>("SourceTenants").ToList();
            }

            return result;
        }

        public List<TenantModuleModel> LoadModules(Guid tenantID)
        {
            List<TenantModuleModel> result = null;
            if (!this.Cache.IsSet("SourceTenantModules" + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new GetTenantModuleDaoHelper(new TenantModuleDbContext(session, true)).ExecuteProcedure(tenantID);
                }

                this.Cache.Set("SourceTenantModules" + tenantID.ToString(), result, 1440);
            }
            else
            {
                result = this.Cache.Get<List<TenantModuleModel>>("SourceTenantModules" + tenantID.ToString()).ToList();
            }

            return result;
        }

        public List<DataList> GetDataList(Guid tenantID)
        {
            List<DataList> result = null;
            if (!this.Cache.IsSet("SourceDataLists" + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new SearchDataListDaoHelper(new DataListsDbContext(session, true)).ExecuteDataListsProcedure(tenantID);
                }

                this.Cache.Set("SourceDataLists" + tenantID.ToString(), result.OrderBy(o => o.ContentID).ToList(), 1440);
            }
            else
            {
                result = this.Cache.Get<List<DataList>>("SourceDataLists" + tenantID.ToString()).ToList();
            }

            return result;
        }

        public List<CodeListModel> GetDataListItems(Guid tenantID)
        {
            if (this.Cache.IsSet("SourceDataListItems" + tenantID.ToString()))
            {
                return this.Cache.Get<List<CodeListModel>>("SourceDataListItems" + tenantID.ToString());
            }

            List<Task> tasks = new List<Task>();
            List<CodeListModel> result = null;
            List<Languages> languages = null;
            List<CodeListModel> resultmsg = new List<CodeListModel>();
            List<CodeListModel> resultlbl = new List<CodeListModel>();
            List<CodeListModel> resultsecrights = new List<CodeListModel>();

            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    result = new SearchDataListItemsDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(tenantID, string.Empty);
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    languages = new SearchDataListLanguagesDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(tenantID);
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    resultmsg = new MessageCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchMessages(tenantID);
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    resultlbl = new LabelsCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchLabels(tenantID);
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    resultsecrights = new SecurityCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString)).SearchCodeTables(tenantID, "Source");
                }));
            }

            Task.WaitAll(tasks.ToArray());
            result.ForEach(x =>
            {
                x.LanguageList = languages.FindAll(c => c.CodeID == x.ID);
            });
            result.AddRange(resultmsg);
            result.AddRange(resultlbl);
            result.AddRange(resultsecrights);
           
            this.Cache.Set("SourceDataListItems" + tenantID.ToString(), result, 1440);

            return result;
        }

        public List<DataList> GetAttributesList(Guid tenantID)
        {
            List<DataList> result = null;
            List<CodeListModel> resultitems = null;

            if (!this.Cache.IsSet("SourceDataListAttributes" + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    resultitems = new SearchDataListItemsDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(tenantID, string.Empty);

                    result = new DataListAttributesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchCodeTables(resultitems, tenantID);
                    this.Cache.Set("SourceDataListAttributes" + tenantID.ToString(), result, 1440);
                }
            }
            else
            {
                result = this.Cache.Get<List<DataList>>("SourceDataListAttributes" + tenantID.ToString());
            }

            return result;
        }

        //public List<DataListItemAttributeModel> GetItemAttributeList()
        //{
        //    List<DataListItemAttributeModel> resultitems = null;
        //    if (!this.Cache.IsSet("SourceDataListItemAttributes"))
        //    {
        //        using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
        //        {
        //            resultitems = new DataListAttributesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").GetDataListItemAttributes();
        //            this.Cache.Set("SourceDataListItemAttributes", resultitems, 1440);
        //        }
        //    }
        //    else
        //    {
        //        resultitems = this.Cache.Get<List<DataListItemAttributeModel>>("SourceDataListItemAttributes");
        //    }

        //    return resultitems;
        //}
    }
}