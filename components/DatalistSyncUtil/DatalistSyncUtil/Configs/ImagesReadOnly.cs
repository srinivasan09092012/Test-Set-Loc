//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
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
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DatalistSyncUtil.Configs
{
    public class ImagesReadOnly
    {
        private string imageTablesKey = "UtilityImageTableKey";

        private string imageLanguageKey = "UtilityImageLanguageTableKey";
        
        private int cacheTimeInMins = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheRefreshTime"]) ? 1440 : int.Parse(ConfigurationManager.AppSettings["CacheRefreshTime"]);
        
        public ImagesReadOnly(DbSession session, string envlocation)
        {
            this.Cachemanager = new RedisCacheManager();
            this.ConnectionString = new ConnectionStringSettings("TenantConfig", session.ConnectionString, session.ProviderInvariantName);
            this.imageTablesKey = envlocation + this.imageTablesKey;
            this.imageLanguageKey = envlocation + this.imageLanguageKey;
        }

        public ConnectionStringSettings ConnectionString { get; set; }

        public ICacheManager Cachemanager { get; set; }

        /// <summary>
        /// Searches the Images Table.
        /// </summary>
        /// <returns>List<ImageListModel></returns>
        public List<ImageListModel> SearchImages(Guid tenantID, bool expandImageLanguages = false)
        {
            List<ImageListModel> result = null;
            if (!this.Cachemanager.IsSet(this.imageTablesKey + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new GetImageDaoHelper(
                        new ImageDbContext(session, true),
                        this.Cachemanager).ExecuteProcedure(tenantID);
                }

                this.Cachemanager.Set(this.imageTablesKey + tenantID.ToString(), result, this.cacheTimeInMins);
            }
            else
            {
                result = this.Cachemanager.Get<List<ImageListModel>>(this.imageTablesKey + tenantID.ToString()).ToList();
            }

            if (expandImageLanguages)
            {
                List<ImageLanguagesModel> languages = null;
                if (!this.Cachemanager.IsSet(this.imageLanguageKey + tenantID.ToString()))
                {
                    using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                    {
                        languages = new GetImageLanguagesDaoHelper(new ImageDbContext(session, true)).ExecuteProcedure(tenantID);
                    }

                    this.Cachemanager.Set(this.imageLanguageKey + tenantID.ToString(), languages, this.cacheTimeInMins);
                }
                else
                {
                    languages = this.Cachemanager.Get<List<ImageLanguagesModel>>(this.imageLanguageKey + tenantID.ToString());
                }

                result.ForEach(x => x.ImageLanguages = this.ExpandImageLanguages(x, languages));
            }

            return result;
        }

        public List<ImageLanguagesModel> ExpandImageLanguages(ImageListModel toExpand, List<ImageLanguagesModel> languages)
        {
            return languages.FindAll(x => x.ID.Equals(toExpand.ID));
        }

        public List<ImageLanguagesModel> GetImageLanguages(Guid tenantID)
        {
            List<ImageLanguagesModel> languages = null;

            if (!this.Cachemanager.IsSet(this.imageLanguageKey + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    languages = new GetImageLanguagesDaoHelper(new ImageDbContext(session, true)).ExecuteProcedure(tenantID);
                }

                this.Cachemanager.Set(this.imageLanguageKey + tenantID.ToString(), languages, this.cacheTimeInMins);
            }
            else
            {
                languages = this.Cachemanager.Get<List<ImageLanguagesModel>>(this.imageLanguageKey + tenantID.ToString());
            }

            return languages;
        }
    }
}
