//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using DatalistSyncUtil.DaoHelpers;
using DatalistSyncUtil.Domain;
using DatalistSyncUtil.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DatalistSyncUtil.Configs
{
    public class HelpReadOnly
    {
        private string helpTablesKey = "UtilityHelpTableKey";
        private string helpLanguageKey = "UtilityHelpLanguageTableKey";

        private int cacheTimeInMins = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheRefreshTime"]) ? 1440 : int.Parse(ConfigurationManager.AppSettings["CacheRefreshTime"]);

        public HelpReadOnly(DbSession session, string envlocation)
        {
            this.Cachemanager = new RedisCacheManager();
            this.ConnectionString = new ConnectionStringSettings("TenantConfig", session.ConnectionString, session.ProviderInvariantName);
            this.helpTablesKey = envlocation + this.helpTablesKey;
            this.helpLanguageKey = envlocation + this.helpLanguageKey;
        }

        public ConnectionStringSettings ConnectionString { get; set; }

        public ICacheManager Cachemanager { get; set; }

        /// <summary>
        /// Searches the Help Table.
        /// </summary>
        /// <returns>List<HelpModel></returns>
        public List<HelpNodeModel> SearchHelp()
        {
            List<HelpNodeModel> result = null;

            if (!this.Cachemanager.IsSet(this.helpTablesKey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new GetHelpContentDaoHelper(new HelpDbContext(session, true)).ExecuteProcedure();
                }

                this.Cachemanager.Set(this.helpTablesKey, result, this.cacheTimeInMins);
            }
            else
            {
                result = this.Cachemanager.Get<List<HelpNodeModel>>(this.helpTablesKey).ToList();
            }
           
            List<HelpContentLanguageModel> languages = this.GetHelpLanguages();
            result.ForEach(x => x.HelpContentLanguages = this.FindLanguages(x, languages));

            return result;
        }

        public List<HelpContentLanguageModel> FindLanguages(HelpNodeModel toExpand, List<HelpContentLanguageModel> languages)
        {
            return languages.FindAll(x => x.HelpNodeId.Equals(toExpand.HelpNodeId));
        }

        public List<HelpContentLanguageModel> GetHelpLanguages()
        {
            List<HelpContentLanguageModel> languages = null;

            if (!this.Cachemanager.IsSet(this.helpLanguageKey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    languages = new GetHelpLanguagesDaoHelper(new HelpDbContext(session, true)).ExecuteProcedure();
                }

                this.Cachemanager.Set(this.helpLanguageKey, languages, this.cacheTimeInMins);
            }
            else
            {
                languages = this.Cachemanager.Get<List<HelpContentLanguageModel>>(this.helpLanguageKey);
            }

            return languages;
        }
    }
}
