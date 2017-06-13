//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using DatalistSyncUtil.Commands;
using DatalistSyncUtil.Configs;
using DatalistSyncUtil.DaoHelpers;
using DatalistSyncUtil.Domain;
using DatalistSyncUtil.Entities;
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
        private const string TagrgetHelpCache = "TargetHelp";
        private int cacheTimeout = int.Parse(ConfigurationManager.AppSettings["CacheTimeOut"]);

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

                this.Cache.Set("TargetTenants", result.OrderBy(o => o.TenantName).ToList(), this.cacheTimeout);
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

                this.Cache.Set("TargetTenantModules", result, this.cacheTimeout);
            }
            else
            {
                result = this.Cache.Get<List<TenantModuleModel>>("TargetTenantModules").ToList();
            }

            return result;
        }      

        public List<AppSettingsModel> GetAppSetting()
        {
            List<AppSettingsModel> result = null;
            if (!this.Cache.IsSet("TargetAppSetting"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new SearchAppSettingsDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
                }

                this.Cache.Set("TargetAppSetting", result.OrderBy(o => o.AppSettingKey).ToList(), this.cacheTimeout);
            }
            else
            {
                result = this.Cache.Get<List<AppSettingsModel>>("TargetAppSetting").ToList();
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

                this.Cache.Set("TargetDataLists", result.OrderBy(o => o.ContentID).ToList(), this.cacheTimeout);
            }
            else
            {
                result = this.Cache.Get<List<DataList>>("TargetDataLists").ToList();
            }

            return result;
        }

        public List<HtmlBlockModel> GetHTMLList()
        {
            List<HtmlBlockModel> result = null;
            if (!this.Cache.IsSet("TargetHtmlBlock"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new HtmlBlocksReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Target").SearchHtmlBlocks();
                }

                this.Cache.Set("TargetHtmlBlock", result.OrderBy(o => o.ContentId).ToList(), this.cacheTimeout);
            }
            else
            {
                result = this.Cache.Get<List<HtmlBlockModel>>("TargetHtmlBlock").ToList();
            }

            return result;
        }

        public List<HelpNodeModel> GetHelpList()
        {
            List<HelpNodeModel> result = null;
            if (!this.Cache.IsSet(TagrgetHelpCache))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new HelpReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Target").SearchHelp();
                }

                this.Cache.Set(TagrgetHelpCache, result.OrderBy(o => o.HelpNodeNM).ToList(), this.cacheTimeout);
            }
            else
            {
                result = this.Cache.Get<List<HelpNodeModel>>(TagrgetHelpCache).ToList();
            }

            return result;
        }

        public List<ImageListModel> GetImagesList()
        {
            List<ImageListModel> result = null;
            if (!this.Cache.IsSet("TargetImages"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new ImagesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Target").SearchImages();
                }

                this.Cache.Set("TargetImages", result.OrderBy(o => o.ContentId).ToList(), this.cacheTimeout);
            }
            else
            {
                result = this.Cache.Get<List<ImageListModel>>("TargetImages").ToList();
            }

            return result;
        }

        public List<ServiceListModel> GetServicesList()
        {
            List<ServiceListModel> result = null;
            if (!this.Cache.IsSet("TargetServices"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    result = new ServicesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Target").SearchServices();
                }

                this.Cache.Set("TargetServices", result.OrderBy(o => o.Name).ToList(), this.cacheTimeout);
            }
            else
            {
                result = this.Cache.Get<List<ServiceListModel>>("TargetServices").ToList();
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
            this.Cache.Set("TargetDataListItems", result, this.cacheTimeout);

            return result;
        }

        public List<CodeListModel> GetSecRightsAndLabels()
        {
            List<Task> tasks = new List<Task>();
            List<CodeListModel> result = new List<CodeListModel>();
            List<CodeListModel> resultsecrights = new List<CodeListModel>();

            if (!this.Cache.IsSet("TargetSecRightsAndLabels"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        result = new LabelsCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Target").SearchLabels();
                    }));

                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        resultsecrights = new SecurityCodeReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString)).SearchCodeTables("Target");
                    }));
                }

                Task.WaitAll(tasks.ToArray());
                result.AddRange(resultsecrights);

                this.Cache.Set("TargetSecRightsAndLabels", result, 1440);
            }
            else
            {
                result = this.Cache.Get<List<CodeListModel>>("TargetSecRightsAndLabels");
            }
                
            return result;
        }

        public List<MenuListModel> GetMenu()
        {
            if (!this.Cache.IsSet("TargetMenus"))
            {
                List<MenuListModel> resultmenu = new List<MenuListModel>();

                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    resultmenu = new MenusReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Target").SearchMenus(true);
                }

                this.Cache.Set("TargetMenus", resultmenu, this.cacheTimeout);

                return resultmenu;
            }
            else
            {
                return this.Cache.Get<List<MenuListModel>>("TargetMenus");
            }
         }

        public bool UpdateMenuItem(MenuItemModel cmd)
        {
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new UpdateMenuItemDaoHelper(new MenuDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }

            return true;
        }

        public bool AddMenuItem(MenuItemModel cmd)
        {
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new AddMenuItemDaoHelper(new MenuDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }

            return true;
        }

        public List<HtmlBlockLanguagesModel> GetHtmlListLangs()
        {
            List<Task> tasks = new List<Task>();
            List<HtmlBlockModel> result = null;
            List<HtmlBlockLanguagesModel> languages = null;

            if (this.Cache.IsSet("TargetHtmlLangs"))
            {
                languages = this.Cache.Get<List<HtmlBlockLanguagesModel>>("TargetHtmlLangs");
            }

            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    result = new HtmlBlocksReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Target").SearchHtmlBlocks();
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    languages = new HtmlBlocksReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Target").GetHtmlBlockLanguages();
                }));
            }

            Task.WaitAll(tasks.ToArray());
            result.ForEach(x =>
            {
                x.HtmlBlockLanguages = languages.FindAll(c => c.ID == x.ID);
            });

            this.Cache.Set("TargetHtmlLangs", result, this.cacheTimeout);

            return languages;
        }

        public List<ImageLanguagesModel> GetImageLangs()
        {
            List<Task> tasks = new List<Task>();
            List<ImageListModel> result = null;
            List<ImageLanguagesModel> languages = null;

            if (this.Cache.IsSet("TargetImageLangs"))
            {
                languages = this.Cache.Get<List<ImageLanguagesModel>>("TargetImageLangs");
            }

            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    result = new ImagesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Target").SearchImages(true);
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    languages = new ImagesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Target").GetImageLanguages();
                }));
            }

            Task.WaitAll(tasks.ToArray());
            result.ForEach(x =>
            {
                x.ImageLanguages = languages.FindAll(c => c.ID == x.ID);
            });

            this.Cache.Set("TargetImageLangs", result, this.cacheTimeout);

            return languages;
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

        public bool AddAppSetting(AppSettingsModel cmd)
        {
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new AddAppSettingDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }

            return true;
        }      

        public bool AddMenus(MenuListModel cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new AddMenuDaoHelper(new MenuDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool AddHtmlBlk(HtmlBlockMainModel cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new AddHtmlBlockDaoHelper(new HtmlBlockDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool UpdateHtmlBlk(HtmlBlockMainModel cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new UpdateHtmlBlockDaoHelper(new HtmlBlockDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool AddServices(ServicesMainModel cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new AddServicesDaoHelper(new ServiceDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool UpdateServices(ServicesMainModel cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new UpdateServicesDaoHelper(new ServiceDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool AddHtmlBlkLanguage(HtmlBlockLanguage cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new AddHtmlBlockLanguageDaoHelper(new HtmlBlockDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool UpdateHtmlBlkLanguage(HtmlBlockLanguage cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new UpdateHtmlBlockLanguageDaoHelper(new HtmlBlockDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool AddHelpLanguage(HelpContentLanguageModel cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new AddHelpLanguageDaoHelper(new HelpDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool UpdateHelpLanguage(HelpContentLanguageModel cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new UpdateHelpLanguageDaoHelper(new HelpDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool AddHelp(AddHelpContentCommand cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new AddHelpContentDaoHelper(new HelpDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool Updatehelp(UpdateHelpContentCommand cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new UpdateHelpContentDaoHelper(new HelpDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool AddImage(ImagesMainModel cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new AddImagesDaoHelper(new ImageDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool UpdateImage(ImagesMainModel cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new UpdateImagesDaoHelper(new ImageDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool AddImageLanguage(ImageLanguage cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new AddImageLanguageDaoHelper(new ImageDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool UpdateImageLanguage(ImageLanguage cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new UpdateImageLanguageDaoHelper(new ImageDbContext(session, true)).ExecuteProcedure(cmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        public bool UpdateMenus(MenuListModel cmd)
        {
            bool success = true;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                try
                {
                    new UpdateMenuDaoHelper(new MenuDbContext(session, true)).ExecuteProcedure(cmd);
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
                    this.Cache.Set("TargetDataListAttributes", result, this.cacheTimeout);
                }
            }
            else
            {
                result = this.Cache.Get<List<DataList>>("TargetDataListAttributes");
            }

            return result;
        }

        public List<ItemDataListItemAttributeVal> GetItemAttributeList()
        {
            List<ItemDataListItemAttributeVal> resultitems = null;
            if (!this.Cache.IsSet("TargetDataListItemAttributes"))
            {
                using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
                {
                    resultitems = new DataListAttributesReadOnly(new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString), "Target").GetDataListItemAttributes();
                    this.Cache.Set("TargetDataListItemAttributes", resultitems, this.cacheTimeout);
                }
            }
            else
            {
                resultitems = this.Cache.Get<List<ItemDataListItemAttributeVal>>("TargetDataListItemAttributes");
            }

            return resultitems;
        }

        public List<CodeLinkTable> GetDataListLinks(string key)
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

        public List<DataList> GetNewDataList()
        {
            List<DataList> result = null;
            using (IDbSession session = new DbSession(this.ConnectionString.ProviderName, this.ConnectionString.ConnectionString))
            {
                result = new SearchDataListDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
            }
           
            return result;
        }
    }
}