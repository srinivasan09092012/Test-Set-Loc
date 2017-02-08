using DatalistSyncUtil.DaoHelpers;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Config.DAOHelpers;
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
    public class TenantHelper
    {
        public TenantHelper()
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["SourceDataList"];
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
            }

            Task.WaitAll(tasks.ToArray());

            result.ForEach(x =>
            {
                x.LanguageList = languages.FindAll(c => c.CodeID == x.ID);
            });

            this.Cache.Set("TargetDataListItems", result, 1440);

            return result;
        }

        public bool AddDatalist(DataListMainModel cmd)
        {
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                new AddDataListDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(cmd);
            }

            return true;
        }
    }
}