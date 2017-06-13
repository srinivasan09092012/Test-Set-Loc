//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
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

        public List<HtmlBlockModel> GetHTMLList()
        {
            List<HtmlBlockModel> result = null;
            if (!this.Cache.IsSet("SourceHtmlBlock"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new HtmlBlocksReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchHtmlBlocks();
                }

                this.Cache.Set("SourceHtmlBlock", result.OrderBy(o => o.ContentId).ToList(), 1440);
            }
            else
            {
                result = this.Cache.Get<List<HtmlBlockModel>>("SourceHtmlBlock").ToList();
            }

            return result;
        }

        public List<HelpNodeModel> GetHelpList()
        {
            List<HelpNodeModel> result = null;
            if (!this.Cache.IsSet(SourceHelpCache))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new HelpReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchHelp();
                }

                this.Cache.Set(SourceHelpCache, result.OrderBy(o => o.HelpNodeNM).ToList(), 1440);
            }
            else
            {
                result = this.Cache.Get<List<HelpNodeModel>>(SourceHelpCache).ToList();
            }

            return result;
        }

        public List<ImageListModel> GetImagesList()
        {
            List<ImageListModel> result = null;
            if (!this.Cache.IsSet("SourceImages"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new ImagesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchImages(true);
                }

                this.Cache.Set("SourceImages", result.OrderBy(o => o.ContentId).ToList(), 1440);
            }
            else
            {
                result = this.Cache.Get<List<ImageListModel>>("SourceImages").ToList();
            }

            return result;
        }

        public List<ServiceListModel> GetServicesList()
        {
            List<ServiceListModel> result = null;
            if (!this.Cache.IsSet("SourceServices"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new ServicesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchServices();
                }

                this.Cache.Set("SourceServices", result.OrderBy(o => o.Name).ToList(), 1440);
            }
            else
            {
                result = this.Cache.Get<List<ServiceListModel>>("SourceServices").ToList();
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
                    resultmsg = new MessageCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchMessages();
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    resultlbl = new LabelsCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchLabels();
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    resultsecrights = new SecurityCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString)).SearchCodeTables("Source");
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
            this.Cache.Set("SourceDataListItems", result, 1440);

            return result;
        }

        public List<CodeListModel> GetSecRightsAndLabels()
        {
            List<Task> tasks = new List<Task>();
            List<CodeListModel> result = new List<CodeListModel>();
            List<CodeListModel> resultsecrights = new List<CodeListModel>();

            if (!this.Cache.IsSet("SourceSecRightsAndLabels"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        result = new LabelsCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchLabels();
                    }));

                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        resultsecrights = new SecurityCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString)).SearchCodeTables("Source");
                    }));
                }

                Task.WaitAll(tasks.ToArray());
                result.AddRange(resultsecrights);

                this.Cache.Set("SourceSecRightsAndLabels", result, 1440);
            }
            else
            {
                result = this.Cache.Get<List<CodeListModel>>("SourceSecRightsAndLabels");
            }

            return result;
        }

        public List<HtmlBlockLanguagesModel> GetHtmlListLangs()
        {
            List<Task> tasks = new List<Task>();
            List<HtmlBlockModel> result = null;
            List<HtmlBlockLanguagesModel> languages = null;

            if (this.Cache.IsSet("SourceHtmlLangs"))
            {
                languages = this.Cache.Get<List<HtmlBlockLanguagesModel>>("SourceHtmlLangs");
            }

            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    result = new HtmlBlocksReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchHtmlBlocks();
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    languages = new HtmlBlocksReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").GetHtmlBlockLanguages();
                }));
            }

            Task.WaitAll(tasks.ToArray());
            result.ForEach(x =>
            {
                x.HtmlBlockLanguages = languages.FindAll(c => c.ID == x.ID);
            });

            this.Cache.Set("SourceHtmlLangs", result, 1440);

            return languages;
        }

        public List<ImageLanguagesModel> GetImageLangs()
        {
            List<Task> tasks = new List<Task>();
            List<ImageListModel> result = null;
            List<ImageLanguagesModel> languages = null;

            if (this.Cache.IsSet("SourceImageLangs"))
            {
                languages = this.Cache.Get<List<ImageLanguagesModel>>("SourceImageLangs");
            }

            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    result = new ImagesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchImages(true);
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    languages = new ImagesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").GetImageLanguages();
                }));
            }

            Task.WaitAll(tasks.ToArray());
            result.ForEach(x =>
            {
                x.ImageLanguages = languages.FindAll(c => c.ID == x.ID);
            });

            this.Cache.Set("SourceImageLangs", result, 1440);

            return languages;
        }

        public List<MenuListModel> GetMenu()
        {
            if (this.Cache.IsSet("Menus"))
            {
                return this.Cache.Get<List<MenuListModel>>("Menus");
            }

            List<MenuListModel> resultmenu = new List<MenuListModel>();

            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                resultmenu = new MenusReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchMenus(true);
            }

            this.Cache.Set("Menus", resultmenu, 1440);

            return resultmenu;
        }

        public List<DataList> GetAttributesList()
        {
            List<DataList> result = null;
            List<CodeListModel> resultitems = null;

            if (!this.Cache.IsSet("SourceDataListAttributes"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    resultitems = new SearchDataListItemsDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(string.Empty);

                    result = new DataListAttributesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchCodeTables(resultitems);
                    this.Cache.Set("SourceDataListAttributes", result, 1440);
                }
            }
            else
            {
                result = this.Cache.Get<List<DataList>>("SourceDataListAttributes");
            }

            return result;
        }
       
        public List<CodeLinkTable> GetDataListLinks(string key)
        {
            {
                List<CodeLinkTable> result = null;

                if (!this.Cache.IsSet(key))
                {
                    using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                    {
                        result = new GetDataListItemLinksDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
                        this.Cache.Set(key, result, 1440);
                   }
                }
                else
                {
                    result = this.Cache.Get<List<CodeLinkTable>>(key);
                }

                return result;
            }
        }

        public List<ItemDataListItemAttributeVal> GetItemAttributeList()
        { 
            List<ItemDataListItemAttributeVal> resultitems = null;
            if (!this.Cache.IsSet("SourceDataListItemAttributes"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    resultitems = new DataListAttributesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").GetDataListItemAttributes();
                    this.Cache.Set("SourceDataListItemAttributes", resultitems, 1440);
                }
            }
            else
            {
                resultitems = this.Cache.Get<List<ItemDataListItemAttributeVal>>("SourceDataListItemAttributes");
            }

            return resultitems;
        }

        public List<AppSettingsModel> GetAppSetting()
        {
            List<AppSettingsModel> result = null;
            if (!this.Cache.IsSet("AppSettings"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new AppSettingsReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Source").SearchAppSetting();
                }

                this.Cache.Set("AppSettings", result.OrderBy(o => o.AppSettingKey).ToList(), 1440);
            }
            else
            {
                result = this.Cache.Get<List<AppSettingsModel>>("AppSettings").ToList();
            }

            return result;
        }
    }
}