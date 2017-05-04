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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatalistSyncUtil
{
    public class TenantHelper
    {
        public TenantHelper()
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["TargetDataList"];
            this.Cache = new RedisCacheManager();
        }

        public TenantHelper(ConnectionStringSettings connection)
        {
            this.ConnectionString = connection;
            this.Cache = new RedisCacheManager();
        }

        public ConnectionStringSettings ConnectionString { get; set; }

        public ICacheManager Cache { get; set; }

        public List<TenantModel> GetTenants()
        {
            List<TenantModel> result = null;

            if (!this.Cache.IsSet("TargetTenants"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new GetTenantDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
                }

                this.Cache.Set("TargetTenants", result.OrderBy(o => o.TenantName).ToList(), 1440);
            }
            else
            {
                result = this.Cache.Get<List<TenantModel>>("TargetTenants").ToList();
            }

            return result;
        }

        public List<TenantModuleModel> LoadModules()
        {
            List<TenantModuleModel> result = null;
            if (!this.Cache.IsSet("TargetTenantModules"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new GetTenantModuleDaoHelper(new TenantModuleDbContext(session, true)).ExecuteProcedure();
                }

                this.Cache.Set("TargetTenantModules", result, 1440);
            }
            else
            {
                result = this.Cache.Get<List<TenantModuleModel>>("TargetTenantModules").ToList();
            }

            return result;
        }

        public List<DataList> GetDataList()
        {
            List<DataList> result = null;
            if (!this.Cache.IsSet("TargetDataLists"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new SearchDataListDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
                }

                this.Cache.Set("TargetDataLists", result.OrderBy(o => o.ContentID).ToList(), 1440);
            }
            else
            {
                result = this.Cache.Get<List<DataList>>("TargetDataLists").ToList();
            }

            return result;
        }

        public List<CodeListModel> GetDataListItems()
        {
            if (this.Cache.IsSet("TargetDataListItems"))
            {
                return this.Cache.Get<List<CodeListModel>>("TargetDataListItems");
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
                    resultmsg = new MessageCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Target").SearchMessages();
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    resultlbl = new LabelsCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Target").SearchLabels();
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    resultsecrights = new SecurityCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString)).SearchCodeTables("Target");
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
            this.Cache.Set("TargetDataListItems", result, 1440);

            return result;
        }

        public bool AddDatalist(DataListMainModel cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new AddDataListDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool UpdateDatalist(DataListMainModel cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new UpdateDataListDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool AddDatalistItem(CodeItemModel cmd)
        {
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new AddDataListItemDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }

            return true;
        }

        public bool AddDatalistItemLanguage(ItemLanguage cmd)
        {
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new AddDataListItemLanguageDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);  
                }
            }

            return true;
        }

        public bool AddDataListAttributes(ItemAttribute cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new AddDataAttributesDaoHelper(new DataListAttributeDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool AddDataListLink(DataListItemLink cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new AddDataListLinkDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

       public bool UpdateDatalistItem(CodeItemModel cmd)
        {
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new UpdateDataListItemDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch
                {
                }
            }

            return true;
        }

        public bool UpdateDatalistItemLanguage(ItemLanguage cmd)
        {
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new UpdateDataListItemLanguageDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch
                {
                }
            }

            return true;
        }

        public bool UpdateDatalistAttribute(ItemAttribute cmd)
        {
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new UpdateDataListAttributesDaoHelper(new DataListAttributeDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch
                {
                }
            }

            return true;
        }

        public bool UpdateDatalistLink(DataListItemLink cmd)
        {
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new UpdateDataListLinkDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch
                {
                }
            }

            return true;
        }

        public List<DataList> GetAttributesList()
        {
            List<DataList> result = null;
            List<CodeListModel> resultitems = null;

            if (!this.Cache.IsSet("TargetDataListAttributes"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    resultitems = new SearchDataListItemsDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(string.Empty);
                    result = new DataListAttributesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "target").SearchCodeTables(resultitems);
                    this.Cache.Set("TargetDataListAttributes", result, 1440);
                }
            }
            else
            {
                result = this.Cache.Get<List<DataList>>("TargetDataListAttributes");
            }

            return result;
        }
    }
}