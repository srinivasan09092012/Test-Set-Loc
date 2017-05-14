//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Config.DAOHelpers;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DatalistSyncUtil.Configs
{
    public class HtmlBlocksReadOnly
    {
        private string htmlBlockLanguageKey = "UtilityHtmlBlockLanguageTableKey";

        private string htmlBlockTablesKey = "UtilityHtmlBlockTableKey";

        private int cacheTimeInMins = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheRefreshTime"]) ? 1440 : int.Parse(ConfigurationManager.AppSettings["CacheRefreshTime"]);

        public HtmlBlocksReadOnly(DbSession session, string envlocation)
        {
            this.Cachemanager = new RedisCacheManager();
            this.ConnectionString = new ConnectionStringSettings("TenantConfig", session.ConnectionString, session.ProviderInvariantName);
            this.htmlBlockTablesKey = envlocation + this.htmlBlockTablesKey;
            this.htmlBlockLanguageKey = envlocation + this.htmlBlockLanguageKey;
        }

        public ConnectionStringSettings ConnectionString { get; set; }

        public ICacheManager Cachemanager { get; set; }

        /// <summary>
        /// Searches the HtmlBlock Table.
        /// </summary>
        /// <returns>List<HtmlBlockModel></returns>
        public List<HtmlBlockModel> SearchHtmlBlocks(bool expandHtmlBlockLanguages = false)
        {
            List<HtmlBlockModel> result = null;
            if (!this.Cachemanager.IsSet(this.htmlBlockTablesKey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                      result = new GetHtmlBlockDaoHelper(
                          new HtmlBlockDbContext(session, true),
                          new DataListsDbContext(session, true),
                          this.Cachemanager).ExecuteProcedure();
                }

                this.Cachemanager.Set(this.htmlBlockTablesKey, result, this.cacheTimeInMins);
            }
            else
            {
                result = this.Cachemanager.Get<List<HtmlBlockModel>>(this.htmlBlockTablesKey).ToList();
            }

            if (expandHtmlBlockLanguages)
            {
                List<HtmlBlockLanguagesModel> languages = this.GetHtmlBlockLanguages();
                result.ForEach(x => x.HtmlBlockLanguages = this.ExpandHtmlBlockLanguages(x, languages));
            }

            return result;
        }

        public List<HtmlBlockLanguagesModel> ExpandHtmlBlockLanguages(HtmlBlockModel toExpand, List<HtmlBlockLanguagesModel> languages)
        {
            return languages.FindAll(x => x.ID.Equals(toExpand.ID));
        }

        public List<HtmlBlockLanguagesModel> GetHtmlBlockLanguages()
        {
            List<HtmlBlockLanguagesModel> languages = null;

            if (!this.Cachemanager.IsSet(this.htmlBlockLanguageKey))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    languages = new GetHtmlBlockLanguagesDaoHelper(new HtmlBlockDbContext(session, true)).ExecuteProcedure();
                }

                this.Cachemanager.Set(this.htmlBlockLanguageKey, languages, this.cacheTimeInMins);
            }
            else
            {
                languages = this.Cachemanager.Get<List<HtmlBlockLanguagesModel>>(this.htmlBlockLanguageKey);
            }

            return languages;
        }
    }
}
