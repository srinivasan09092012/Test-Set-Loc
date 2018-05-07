//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using DatalistSyncUtil.Configs;
using DatalistSyncUtil.Domain;
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
        private const string SourceHelpCache = "SourceHelpNode";

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

        public List<ApplicationModel> LoadApplicationName()
        {            
            List<ApplicationModel> result = null;
            if (!this.Cache.IsSet("SourceTenantApplication"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new GetApplicationDaoHelper(new ApplicationDbContext(session, true)).ExecuteProcedure();
                }

                this.Cache.Set("SourceTenantApplication", result, 1440);
            }
            else
            {
                result = this.Cache.Get<List<ApplicationModel>>("SourceTenantApplication").ToList();
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

        public List<HtmlBlockModel> GetHTMLList(Guid tenantID)
        {
            List<HtmlBlockModel> result = null;
            if (!this.Cache.IsSet("SourceHtmlBlock" + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new HtmlBlocksReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchHtmlBlocks(tenantID);
                }

                this.Cache.Set("SourceHtmlBlock" + tenantID.ToString(), result.OrderBy(o => o.ContentId).ToList(), 1440);
            }
            else
            {
                result = this.Cache.Get<List<HtmlBlockModel>>("SourceHtmlBlock" + tenantID.ToString()).ToList();
            }

            return result;
        }

        public List<HelpNodeModel> GetHelpList(Guid tenantID)
        {
            List<HelpNodeModel> result = null;
            if (!this.Cache.IsSet(SourceHelpCache + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new HelpReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchHelp(tenantID);
                }

                this.Cache.Set(SourceHelpCache + tenantID.ToString(), result.OrderBy(o => o.HelpNodeNM).ToList(), 1440);
            }
            else
            {
                result = this.Cache.Get<List<HelpNodeModel>>(SourceHelpCache + tenantID.ToString()).ToList();
            }

            return result;
        }

        public List<ImageListModel> GetImagesList(Guid tenantID)
        {
            List<ImageListModel> result = null;
            if (!this.Cache.IsSet("SourceImages" + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new ImagesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchImages(tenantID, true);
                }

                this.Cache.Set("SourceImages" + tenantID.ToString(), result.OrderBy(o => o.ContentId).ToList(), 1440);
            }
            else
            {
                result = this.Cache.Get<List<ImageListModel>>("SourceImages" + tenantID.ToString()).ToList();
            }

            return result;
        }

        public List<ServiceListModel> GetServicesList(Guid tenantID)
        {
            List<ServiceListModel> result = null;
            if (!this.Cache.IsSet("SourceServices" + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new ServicesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchServices(tenantID);
                }

                this.Cache.Set("SourceServices" + tenantID.ToString(), result.OrderBy(o => o.Name).ToList(), 1440);
            }
            else
            {
                result = this.Cache.Get<List<ServiceListModel>>("SourceServices" + tenantID.ToString()).ToList();
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
            List<CodeListModel> finalSecurity = new List<CodeListModel>();
            resultsecrights.ForEach(x =>
            {
                CodeListModel list = result.Find(c => c.Code == x.Code && c.ContentID == x.ContentID);
                if (list == null)
                {
                    finalSecurity.Add(x);
                }
            });
            result.AddRange(finalSecurity);
            this.Cache.Set("SourceDataListItems" + tenantID.ToString(), result, 1440);

            return result;
        }

        public List<CodeListModel> GetSecRightsAndLabels(Guid tenantID)
        {
            List<Task> tasks = new List<Task>();
            List<CodeListModel> result = new List<CodeListModel>();
            List<CodeListModel> resultsecrights = new List<CodeListModel>();

            if (!this.Cache.IsSet("SourceSecRightsAndLabels" + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        result = new LabelsCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchLabels(tenantID);
                    }));

                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        resultsecrights = new SecurityCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString)).SearchCodeTables(tenantID, "Source");
                    }));
                }

                Task.WaitAll(tasks.ToArray());
                result.AddRange(resultsecrights);

                this.Cache.Set("SourceSecRightsAndLabels" + tenantID.ToString(), result, 1440);
            }
            else
            {
                result = this.Cache.Get<List<CodeListModel>>("SourceSecRightsAndLabels" + tenantID.ToString());
            }

            return result;
        }

        public List<HtmlBlockLanguagesModel> GetHtmlListLangs(Guid tenantID)
        {
            List<Task> tasks = new List<Task>();
            List<HtmlBlockModel> result = null;
            List<HtmlBlockLanguagesModel> languages = null;

            if (this.Cache.IsSet("SourceHtmlLangs" + tenantID.ToString()))
            {
                languages = this.Cache.Get<List<HtmlBlockLanguagesModel>>("SourceHtmlLangs" + tenantID.ToString());
            }

            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    result = new HtmlBlocksReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchHtmlBlocks(tenantID);
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    languages = new HtmlBlocksReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").GetHtmlBlockLanguages(tenantID);
                }));
            }

            Task.WaitAll(tasks.ToArray());
            result.ForEach(x =>
            {
                x.HtmlBlockLanguages = languages.FindAll(c => c.ID == x.ID);
            });

            this.Cache.Set("SourceHtmlLangs" + tenantID.ToString(), result, 1440);

            return languages;
        }

        public List<ImageLanguagesModel> GetImageLangs(Guid tenantID)
        {
            List<Task> tasks = new List<Task>();
            List<ImageListModel> result = null;
            List<ImageLanguagesModel> languages = null;

            if (this.Cache.IsSet("SourceImageLangs" + tenantID.ToString()))
            {
                languages = this.Cache.Get<List<ImageLanguagesModel>>("SourceImageLangs" + tenantID.ToString());
            }

            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    result = new ImagesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchImages(tenantID, true);
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    languages = new ImagesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").GetImageLanguages(tenantID);
                }));
            }

            Task.WaitAll(tasks.ToArray());
            result.ForEach(x =>
            {
                x.ImageLanguages = languages.FindAll(c => c.ID == x.ID);
            });

            this.Cache.Set("SourceImageLangs" + tenantID.ToString(), result, 1440);

            return languages;
        }

        public List<MenuListModel> GetMenu(Guid tenantID)
        {
            if (this.Cache.IsSet("Menus" + tenantID.ToString()))
            {
                return this.Cache.Get<List<MenuListModel>>("Menus" + tenantID.ToString());
            }

            List<MenuListModel> resultmenu = new List<MenuListModel>();

            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                resultmenu = new MenusReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchMenus(tenantID, true);
            }

            this.Cache.Set("Menus" + tenantID.ToString(), resultmenu, 1440);

            return resultmenu;
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

        public List<CodeLinkTable> GetDataListLinks(Guid tenantID, string key)
        {
            {
                List<CodeLinkTable> result = null;

                if (!this.Cache.IsSet(key + tenantID.ToString()))
                {
                    using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                    {
                        result = new GetDataListItemLinksDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(tenantID);
                        this.Cache.Set(key + tenantID.ToString(), result, 1440);
                   }
                }
                else
                {
                    result = this.Cache.Get<List<CodeLinkTable>>(key + tenantID.ToString());
                }

                return result;
            }
        }

        public List<ItemDataListItemAttributeVal> GetItemAttributeList(Guid tenantID)
        { 
            List<ItemDataListItemAttributeVal> resultitems = null;
            if (!this.Cache.IsSet("SourceDataListItemAttributes" + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    resultitems = new DataListAttributesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").GetDataListItemAttributes(tenantID);
                    this.Cache.Set("SourceDataListItemAttributes" + tenantID.ToString(), resultitems, 1440);
                }
            }
            else
            {
                resultitems = this.Cache.Get<List<ItemDataListItemAttributeVal>>("SourceDataListItemAttributes" + tenantID.ToString());
            }

            return resultitems;
        }

        public List<AppSettingsModel> GetAppSetting(Guid tenantID)
        {
            List<AppSettingsModel> result = null;
            if (!this.Cache.IsSet("AppSettings" + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new AppSettingsReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchAppSetting(tenantID);
                }

                this.Cache.Set("AppSettings" + tenantID.ToString(), result.OrderBy(o => o.AppSettingKey).ToList(), 1440);
            }
            else
            {
                result = this.Cache.Get<List<AppSettingsModel>>("AppSettings" + tenantID.ToString()).ToList();
            }

            return result;
        }
    }
}
