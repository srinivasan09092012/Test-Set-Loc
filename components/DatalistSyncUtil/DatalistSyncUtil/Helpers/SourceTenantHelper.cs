//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using DatalistSyncUtil.Configs;
using DatalistSyncUtil.DaoHelpers;
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
using System.Text;
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

        public List<TenantModuleModel> LoadModules()
        {
            List<TenantModuleModel> result = null;
            if (!this.Cache.IsSet("SourceTenantModules"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new GetTenantModuleDaoHelper(new TenantModuleDbContext(session, true)).ExecuteProcedure();
                }

                this.Cache.Set("SourceTenantModules", result, 1440);
            }
            else
            {
                result = this.Cache.Get<List<TenantModuleModel>>("SourceTenantModules").ToList();
            }

            return result;
        }

        public List<DataList> GetDataList()
        {
            List<DataList> result = null;
            if (!this.Cache.IsSet("SourceDataLists"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new SearchDataListDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
                }

                this.Cache.Set("SourceDataLists", result.OrderBy(o => o.ContentID).ToList(), 1440);
            }
            else
            {
                result = this.Cache.Get<List<DataList>>("SourceDataLists").ToList();
            }

            return result;
        }

        public List<CodeListModel> GetDataListItems()
        {
            if (this.Cache.IsSet("SourceDataListItems"))
            {
                return this.Cache.Get<List<CodeListModel>>("SourceDataListItems");
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
                    result = new SearchDataListItemsDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(string.Empty);
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    languages = new SearchDataListLanguagesDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    resultmsg = new MessageCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString)).SearchMessages();
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    resultlbl = new LabelsCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString)).SearchLabels();
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    resultsecrights = new SecurityCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString)).SearchCodeTables();
                }));

            }

            Task.WaitAll(tasks.ToArray());
            result.AddRange(resultmsg);
            result.AddRange(resultlbl);
            result.AddRange(resultsecrights);

            result.ForEach(x =>
            {
                x.LanguageList = languages.FindAll(c => c.CodeID == x.ID);
            });

            this.Cache.Set("SourceDataListItems", result, 1440);

            return result;
        }
    }
}