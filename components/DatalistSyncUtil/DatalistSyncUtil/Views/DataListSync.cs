//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using DatalistSyncUtil.Configs;
using DatalistSyncUtil.Domain;
using DatalistSyncUtil.Views;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Config.DAOHelpers;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatalistSyncUtil
{
    public partial class DataListSync : Form
    {
        private readonly string listKeyword = "CodeList_";

        //Common
        private readonly string commit = "Commit";

        private readonly string declarationEnd = "DeclarationEnd";

        //DataList variables
        private readonly string dataListInsert = "DataListInsert";

        private readonly string dataListUpdate = "DataListUpdate";
        private readonly string dataListCheck = "DataListCheck";
        private readonly string declarationStart = "DeclarationStart";
        private readonly string dataListIFCondition = "DataListIFCondition";
        private readonly string dataListELSECondition = "DataListELSECondition";
        private readonly string endifCondition = "ENDIFCondition";

        //DataList Item variables
        private readonly string itemDeclarationStart = "ItemDeclarationStart";

        private readonly string dataListItemExist = "DataListItemExist";
        private readonly string dataListItemAddUpdate = "DataListItemAddUpdate";
        private readonly string dataListItemLanguageCheck = "DataListItemLanguageCheck";
        private readonly string dataListItemLanguageAddUpdate = "DataListItemLanguageAddUpdate";

        //DataList Item Link variables
        private readonly string itemLinkDeclarationStart = "ItemLinkDeclarationStart";

        private readonly string dataListItemLinkAddUpdate = "DataListItemLinkAddUpdate";

        //Security Item variables
        private readonly string securityFunction = "Core.SecurityFunctions";
        private readonly string securityRoles = "Core.SecurityRoles";
        private readonly string securityRight = "Core.SecurityRights";

        public DataListSync()
        {
            this.InitializeComponent();
            this.SourceConnectionString = ConfigurationManager.ConnectionStrings["SourceDataList"];
            this.TargetConnectionString = ConfigurationManager.ConnectionStrings["TargetDataList"];
            this.NoOfDays = (int)this.Age.Value;
            this.SkipNoOfDays = bool.Parse(ConfigurationManager.AppSettings["SkipNoOfDays"]);
            DataSet querySet = new DataSet();
            querySet.ReadXml(@"Configs\DataListQueries.xml");
            this.DataListQueryDetails = querySet.Tables[0];
            this.Cache = new RedisCacheManager();
            this.LoadTenants();
            this.LoadControls();
            this.LoadModules();
            this.ModuleList.ClearSelected();
            this.DataListsQueryPath = ConfigurationManager.AppSettings["DataListsQueryFilePath"];
            this.DataListItemsQueryPath = ConfigurationManager.AppSettings["DataListItemsQueryFilePath"];
            this.QueryFilePath = ConfigurationManager.AppSettings["QueryFilePath"];
        }

        public ConnectionStringSettings SourceConnectionString { get; set; }

        public ConnectionStringSettings TargetConnectionString { get; set; }

        public int NoOfDays { get; set; }

        public bool SkipNoOfDays { get; set; }

        public List<TenantModuleModel> TenantModules { get; set; }

        public List<string> ListContents { get; set; }

        public List<string> ListItemContents { get; set; }

        public List<TenantModel> TenantLists { get; set; }

        public List<DataList> SourceLists { get; set; }

        public List<MenuListModel> ChildMenuItem { get; set; }

        public List<CodeListModel> SourceListItems { get; set; }

        public List<HelpNodeModel> SourceHelpNodeList { get; set; }

        public List<AppSettingsModel> SourceAppSettings { get; set; }

        public List<MenuListModel> SourceMenus { get; set; }

        public List<MenuItemModel> SourceMenuItems { get; set; }

        public List<HtmlBlockModel> SourceHtmlListItems { get; set; }

        public List<ImageListModel> SourceImagesList { get; set; }

        public List<ServiceListModel> SourceServicesList { get; set; }

        public List<CodeLinkTable> SourceLinks { get; set; }

        public DataTable DataListQueryDetails { get; set; }

        public ICacheManager Cache { get; set; }

        public string DataListsQueryPath { get; set; }

        public string DataListItemsQueryPath { get; set; }

        public string QueryFilePath { get; set; }

        public List<ItemDataListItemAttributeVal> Resultitems { get; set; }

        public List<CodeListModel> FilteredItems { get; set; }

        private void LoadTenants()
        {
            List<TenantModel> result = null;

            if (!this.Cache.IsSet("Tenants"))
            {
                using (IDbSession session = new DbSession(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString))
                {
                    result = new GetTenantDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
                }

                this.Cache.Set("Tenants", result.OrderBy(o => o.TenantName).ToList(), 1440);
                this.TenantLists = result;
            }

            this.TenantList.DataSource = this.Cache.Get<List<TenantModel>>("Tenants").ToList();
        }

        private void LoadModules()
        {
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            if (!this.Cache.IsSet("TenantModules" + tenantID.ToString()))
            {
                this.TenantModules = this.GetTenantModules(tenantID, this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);
                this.Cache.Set("TenantModules" + tenantID.ToString(), this.TenantModules, 1440);
            }

            this.ModuleList.DataSource = this.Cache.Get<List<TenantModuleModel>>("TenantModules" + tenantID.ToString()).GroupBy(i => i.ModuleName)
                  .Select(group =>
                        new
                        {
                            Key = group.Key,
                            Items = group.OrderByDescending(x => x.ModuleName)
                        })
                  .Select(g => g.Items.First()).OrderBy(o => o.ModuleName).ToList();
        }

        private void DataListLoad_Click(object sender, EventArgs e)
        {
            if (this.ControlName.SelectedItem == null)
            {
                MessageBox.Show("Please select control name from the drop down");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            this.LoadSearchCriteria();
            Cursor.Current = Cursors.Default;
            this.BtnDownloadToFile.Enabled = true;
        }

        private List<ItemDataListItemAttributeVal> LoadDataListItemAttributes(Guid tenantID)
        {
            if (!this.Cache.IsSet("SourceSyncDataListItemAttributes" + tenantID.ToString()))
            {
                using (IDbSession session = new DbSession(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString))
                {
                    this.Resultitems = new DataListAttributesReadOnly(new DbSession(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString), "Source").GetDataListItemAttributes(tenantID);
                    this.Cache.Set("SourceDataListItemAttributes" + tenantID.ToString(), this.Resultitems, 1440);
                }
            }
            else
            {
                this.Resultitems = this.Cache.Get<List<ItemDataListItemAttributeVal>>("SourceSyncDataListItemAttributes" + tenantID.ToString());
            }

            return this.Resultitems;
        }

        private void LoadSearchCriteria()
        {
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            this.NoOfDays = (int)this.Age.Value;
            string caseSwitch = this.ControlName.SelectedItem.ToString();
            switch (caseSwitch)
            {
                case "AppSetting":
                    this.DataListView.Columns[1].Visible = false;
                    this.DataListView.Columns[2].Visible = false;
                    this.DataListView.Columns[3].Visible = true;
                    this.DataListView.Columns[4].Visible = true;
                    this.DataListView.Columns[5].Visible = false;
                    this.BindAppSettings();
                    this.ModuleListSelectedAppSetting();
                    break;
                case "Datalist":
                    this.SourceListItems = this.LoadDataListItems(tenantID, this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);
                    this.Resultitems = this.LoadDataListItemAttributes(tenantID);
                    this.DataListView.Columns[1].Visible = true;
                    this.DataListView.Columns[2].Visible = false;
                    this.DataListView.Columns[3].Visible = false;
                    this.DataListView.Columns[4].Visible = false;
                    this.DataListView.Columns[5].Visible = false;
                    this.BindDataList();
                    this.ModuleListSelectedItems();
                    break;
                case "Menus":
                    this.DataListView.Columns[2].Visible = true;
                    this.DataListView.Columns[1].Visible = false;
                    this.DataListView.Columns[3].Visible = false;
                    this.DataListView.Columns[4].Visible = false;
                    this.DataListView.Columns[5].Visible = false;
                    this.BindMenus();
                    this.ModuleMenuListSelectedItems();
                    break;
                case "HtmlBlock":
                    this.SourceHtmlListItems = this.LoadHTMLBlocks(tenantID, this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);
                    this.DataListView.Columns[1].Visible = true;
                    this.DataListView.Columns[2].Visible = false;
                    this.DataListView.Columns[3].Visible = false;
                    this.DataListView.Columns[4].Visible = false;
                    this.DataListView.Columns[5].Visible = false;
                    this.BindHTMLBlock();
                    this.ModuleHtmlListSelectedItems();
                    break;
                case "Security":
                    this.SourceListItems = this.LoadDataListItems(tenantID, this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);
                    this.Resultitems = this.LoadDataListItemAttributes(tenantID);
                    this.DataListView.Columns[1].Visible = true;
                    this.DataListView.Columns[2].Visible = false;
                    this.DataListView.Columns[3].Visible = false;
                    this.DataListView.Columns[4].Visible = false;
                    this.DataListView.Columns[5].Visible = false;
                    this.BindDataList();
                    this.ModuleListSelectedSecurityItems();
                    break;
                case "Image":
                    this.SourceImagesList = this.LoadImages(tenantID, this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);
                    this.DataListView.Columns[1].Visible = true;
                    this.DataListView.Columns[2].Visible = false;
                    this.DataListView.Columns[3].Visible = false;
                    this.DataListView.Columns[4].Visible = false;
                    this.DataListView.Columns[5].Visible = false;
                    this.BindImages();
                    this.ModuleImageListSelectedItems();
                    break;
                case "Help":
                    this.DataListView.Columns[1].Visible = false;
                    this.DataListView.Columns[2].Visible = false;
                    this.DataListView.Columns[3].Visible = false;
                    this.DataListView.Columns[4].Visible = false;
                    this.DataListView.Columns[5].Visible = true;
                    this.SourceHelpNodeList = this.LoadHelpNodeLocale(tenantID, this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);
                    this.BindHelpNode();
                    this.ModuleHelpListSelectedItems();
                    break;
                case "Service":
                    this.SourceServicesList = this.LoadServices(tenantID, this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);
                    this.DataListView.Columns[1].Visible = false;
                    this.DataListView.Columns[2].Visible = true;
                    this.DataListView.Columns[3].Visible = false;
                    this.DataListView.Columns[4].Visible = false;
                    this.DataListView.Columns[5].Visible = false;
                    this.BindServices();
                    this.ModuleServicesListSelectedItems();
                    break;
                default:
                    break;
            }
        }

        private void BindAppSettings()
        {
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());

            if (!this.Cache.IsSet("Appsettings" + tenantID.ToString()))
            {
                this.SourceAppSettings = this.LoadAppSetting(tenantID, this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);
            }
            else
            {
                this.SourceAppSettings = this.Cache.Get<List<AppSettingsModel>>("Appsettings" + tenantID.ToString());
            }

            this.DataListView.AutoGenerateColumns = false;

            if (this.SkipNoOfDays)
            {
                this.DataListView.DataSource = new BindingList<AppSettingsModel>(this.SourceAppSettings.ToList());
            }
            else
            {
                this.DataListView.DataSource = new BindingList<AppSettingsModel>(this.SourceAppSettings.Where(w => w.LastModifiedTimeStamp.Value >= DateTime.UtcNow.AddDays(this.NoOfDays)).ToList());
            }
        }

        private List<AppSettingsModel> LoadAppSetting(Guid tenantID, string providerName, string connectionString)
        {
            List<AppSettingsModel> result = null;

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                result = new Configs.AppSettingsReadOnly(new DbSession(providerName, connectionString), "Source").SearchAppSetting(tenantID);
            }

            this.Cache.Set("Appsettings" + tenantID.ToString(), result.ToList(), 1440);

            return result;
        }

        private List<TenantModuleModel> GetTenantModules(Guid tenantID, string providerName, string connectionString)
        {
            List<TenantModuleModel> result = null;

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                result = new GetTenantModuleDaoHelper(new TenantModuleDbContext(session, true)).ExecuteProcedure(tenantID);
            }

            return result;
        }

        private void BindDataList()
        {
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            List<DataListAttribute> datalistattribute = new List<DataListAttribute>();
            if (!this.Cache.IsSet("DataLists" + tenantID.ToString()))
            {
                this.SourceLists = this.LoadDataList(tenantID, this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);
               
                if (!this.Cache.IsSet("SourceDataSyncAttributeList" + tenantID.ToString()))
                {
                    using (IDbSession session = new DbSession(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString))
                    {
                        datalistattribute = new GetDataListAttributesDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(tenantID);
                        this.Cache.Set("SourceDataSyncAttributeList" + tenantID.ToString(), datalistattribute, 1440);
                    }
                }
                else
                {
                    datalistattribute = this.Cache.Get<List<DataListAttribute>>("SourceDataSyncAttributeList" + tenantID.ToString());
                }

                this.SourceLists.ForEach(x => x.DataListAttributes = datalistattribute.FindAll(c => c.DataListID == x.ID));
                this.SourceLists.ForEach(x => x.DataListAttributes.ForEach(y => y.DataListTypeName = this.SourceLists.Find(p => p.ID == y.DataListTypeID).ContentID));
                this.SourceLists.ForEach(x => x.DataListAttributes.ForEach(y => y.DefaultTypeText = this.SourceListItems.Find(c => c.ID == y.DefaultTypeValue).Code));
                this.Cache.Set("DataLists" + tenantID.ToString(), this.SourceLists, 1440);
            }
            else
            {
                this.SourceLists = this.Cache.Get<List<DataList>>("DataLists" + tenantID.ToString());
            }

            this.DataListView.AutoGenerateColumns = false;

                if (this.SkipNoOfDays)
                {
                    this.DataListView.DataSource = new BindingList<DataList>(this.SourceLists.ToList());
                }
                else
                {
                    this.DataListView.DataSource = new BindingList<DataList>(this.SourceLists.Where(w => w.LastModified.Value >= DateTime.UtcNow.AddDays(this.NoOfDays)).ToList());
                }
            }

        private void BindMenus()
        {
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            this.SourceMenus = this.LoadMenu(tenantID, this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);
            List<MenuItemModel> childMenuItems = new List<MenuItemModel>();
            List<MenuListModel> childMenuItem1 = new List<MenuListModel>();
            this.SourceMenus.ForEach(x =>
            {
                childMenuItems.AddRange(x.Children.ToList());
            });

            this.SourceMenuItems = childMenuItems;
            this.SourceMenus.ForEach(f =>
            {
                childMenuItems = f.Children.ToList();
                childMenuItems.Where(x => x.LastModifiedDate >= DateTime.UtcNow.AddDays(this.NoOfDays)).ToList();
                if (childMenuItems.Count > 0)
                {
                    childMenuItem1.Add(f);
                }
            });
            this.ChildMenuItem = childMenuItem1;
            this.DataListView.AutoGenerateColumns = false;
            this.DataListView.DataSource = new BindingList<MenuListModel>(this.ChildMenuItem.Where(w => w.TenantId == tenantID).ToList());
        }

        private void BindHelpNode()
        {
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            List<HelpNodeLocaleModel> helpNodelist = new List<HelpNodeLocaleModel>();
            this.DataListView.AutoGenerateColumns = false;
            if (this.SkipNoOfDays)
            {
                this.DataListView.DataSource = new BindingList<HelpNodeModel>(this.SourceHelpNodeList.Where(w => w.TenantId == tenantID).ToList());
            }
            else
            {
                this.DataListView.DataSource = new BindingList<HelpNodeModel>(this.SourceHelpNodeList.Where(w => w.TenantId == tenantID && w.LastModifiedTS >= DateTime.UtcNow.AddDays(this.NoOfDays)).ToList());
            }
        }

        private void BindHTMLBlock()
        {
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            List<HtmlBlockModel> htmlblocklist = new List<HtmlBlockModel>();
            List<HtmlBlockLanguagesModel> htmlBlockLangs = new List<HtmlBlockLanguagesModel>();
            if (!this.Cache.IsSet("HtmlBlock" + tenantID.ToString()))
            {
                this.SourceHtmlListItems = this.LoadHTMLBlockList(tenantID, this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);

                if (!this.Cache.IsSet("HtmlBlock" + tenantID.ToString()))
                {
                    using (IDbSession session = new DbSession(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString))
                    {
                        htmlblocklist = new HtmlBlocksReadOnly(new DbSession(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString), "Source").SearchHtmlBlocks(tenantID, true);
                        this.Cache.Set("HtmlBlock" + tenantID.ToString(), htmlblocklist, 1440);
                    }
                }
                else
                {
                    htmlblocklist = this.Cache.Get<List<HtmlBlockModel>>("HtmlBlock" + tenantID.ToString());
                }

                this.Cache.Set("HtmlBlock" + tenantID.ToString(), this.SourceHtmlListItems, 1440);
            }
            else
            {
                this.SourceHtmlListItems = this.Cache.Get<List<HtmlBlockModel>>("HtmlBlock" + tenantID.ToString());
            }

            this.DataListView.AutoGenerateColumns = false;

            if (this.SkipNoOfDays)
            {
                this.DataListView.DataSource = new BindingList<HtmlBlockModel>(this.SourceHtmlListItems.ToList());
            }
            else
            {
                this.DataListView.DataSource = new BindingList<HtmlBlockModel>(this.SourceHtmlListItems.Where(w => w.LastModifiedTS >= DateTime.UtcNow.AddDays(this.NoOfDays)).ToList());
            }
        }

        private List<HtmlBlockModel> LoadHTMLBlockList(Guid tenantID, string providerName, string connectionString)
        {
            List<HtmlBlockModel> result = null;

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                result = new HtmlBlocksReadOnly(new DbSession(providerName, connectionString), "Source").SearchHtmlBlocks(tenantID, true);
            }

            this.Cache.Set("HTMLBlocks" + tenantID.ToString(), result.OrderBy(o => o.ID).ToList(), 1440);

            return result;
        }

        private void BindImages()
        {
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            List<ImageListModel> imagesList = new List<ImageListModel>();
            List<ImageLanguagesModel> imageLangs = new List<ImageLanguagesModel>();
            if (!this.Cache.IsSet("Images" + tenantID.ToString()))
            {
                this.SourceImagesList = this.LoadImagesList(tenantID, this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);

                if (!this.Cache.IsSet("Images" + tenantID.ToString()))
                {
                    using (IDbSession session = new DbSession(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString))
                    {
                        imagesList = new ImagesReadOnly(new DbSession(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString), "Source").SearchImages(tenantID, true);
                        this.Cache.Set("Images" + tenantID.ToString(), imagesList, 1440);
                    }
                }
                else
                {
                    imagesList = this.Cache.Get<List<ImageListModel>>("Images" + tenantID.ToString());
                }

                this.Cache.Set("Images" + tenantID.ToString(), this.SourceImagesList, 1440);
            }
            else
            {
                this.SourceImagesList = this.Cache.Get<List<ImageListModel>>("Images" + tenantID.ToString());
            }

            this.DataListView.AutoGenerateColumns = false;

            if (this.SkipNoOfDays)
            {
                this.DataListView.DataSource = new BindingList<ImageListModel>(this.SourceImagesList.ToList());
            }
            else
            {
                this.DataListView.DataSource = new BindingList<ImageListModel>(this.SourceImagesList.Where(w => w.LastModifiedTS >= DateTime.UtcNow.AddDays(this.NoOfDays)).ToList());
            }
        }

        private List<ImageListModel> LoadImagesList(Guid tenantID, string providerName, string connectionString)
        {
            List<ImageListModel> result = null;

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                result = new ImagesReadOnly(new DbSession(providerName, connectionString), "Source").SearchImages(tenantID, true);
            }

            this.Cache.Set("Images" + tenantID.ToString(), result.OrderBy(o => o.ID).ToList(), 1440);

            return result;
        }

        private void BindServices()
        {
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            List<ServiceListModel> servicesList = new List<ServiceListModel>();
            if (!this.Cache.IsSet("Services"))
            {
                this.SourceServicesList = this.LoadServicesList(tenantID, this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);

                if (!this.Cache.IsSet("Services"))
                {
                    using (IDbSession session = new DbSession(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString))
                    {
                        servicesList = new ServicesReadOnly(new DbSession(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString), "Source").SearchServices(tenantID);
                        this.Cache.Set("Services", servicesList, 1440);
                    }
                }
                else
                {
                    servicesList = this.Cache.Get<List<ServiceListModel>>("Services");
                }

                this.Cache.Set("Services", this.SourceImagesList, 1440);
            }
            else
            {
                this.SourceServicesList = this.Cache.Get<List<ServiceListModel>>("Services");
            }

            this.DataListView.AutoGenerateColumns = false;

            if (this.SkipNoOfDays)
            {
                this.DataListView.DataSource = new BindingList<ServiceListModel>(this.SourceServicesList.Where(w => w.TenantId == tenantID).ToList());
            }
            else
            {
                this.DataListView.DataSource = new BindingList<ServiceListModel>(this.SourceServicesList.Where(w => w.TenantId == tenantID && w.LastModifiedDate >= DateTime.UtcNow.AddDays(this.NoOfDays)).ToList());
            }
        }

        private List<ServiceListModel> LoadServicesList(Guid tenantID, string providerName, string connectionString)
        {
            List<ServiceListModel> result = null;

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                result = new ServicesReadOnly(new DbSession(providerName, connectionString), "Source").SearchServices(tenantID);
            }

            this.Cache.Set("Services" + tenantID.ToString(), result.OrderBy(o => o.ID).ToList(), 1440);

            return result;
        }

        private List<DataList> LoadDataList(Guid tenantID, string providerName, string connectionString)
        {
            List<DataList> result = null;

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                result = new SearchDataListDaoHelper(new DataListsDbContext(session, true)).ExecuteDataListsProcedure(tenantID);
            }

            this.Cache.Set("DataLists" + tenantID.ToString(), result.OrderBy(o => o.ContentID).ToList(), 1440);

            return result;
        }

        private List<CodeListModel> LoadDataListItems(Guid tenantID, string providerName, string connectionString)
        {
            if (this.Cache.IsSet("DataListItems" + tenantID.ToString()))
            {
                return this.Cache.Get<List<CodeListModel>>("DataListItems" + tenantID.ToString());
            }

            List<Task> tasks = new List<Task>();
            List<CodeListModel> result = null;
            List<Languages> languages = null;
            List<MenuListModel> resultmenu = new List<MenuListModel>();
            List<CodeListModel> resultmsg = new List<CodeListModel>();
            List<CodeListModel> resultlbl = new List<CodeListModel>();
            List<CodeListModel> resultsecrights = new List<CodeListModel>();

            using (IDbSession session = new DbSession(providerName, connectionString))
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
                    resultmsg = new MessageCodeReadOnly(new DbSession(providerName, connectionString), "Source").SearchMessages(tenantID);
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    resultlbl = new LabelsCodeReadOnly(new DbSession(providerName, connectionString), "Source").SearchLabels(tenantID);
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    resultsecrights = new SecurityCodeReadOnly(new DbSession(providerName, connectionString)).SearchCodeTables(tenantID, "Source");
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
    
            this.Cache.Set("DataListItems" + tenantID.ToString(), result, 1440);

            return result;
        }

        private List<MenuListModel> LoadMenu(Guid tenantID, string providerName, string connectionString)
        {
            if (this.Cache.IsSet("Menus" + tenantID.ToString()))
            {
                return this.Cache.Get<List<MenuListModel>>("Menus" + tenantID.ToString());
            }

            List<MenuListModel> resultmenu = new List<MenuListModel>();

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                resultmenu = new MenusReadOnly(new DbSession(providerName, connectionString), "Source").SearchMenus(tenantID, true);
            }

            this.Cache.Set("Menus" + tenantID.ToString(), resultmenu, 1440);

            return resultmenu;
        }

        private List<HelpNodeModel> LoadHelpNodeLocale(Guid tenantID, string providerName, string connectionString)
        {
            if (this.Cache.IsSet("HelpNode" + tenantID.ToString()))
            {
                return this.Cache.Get<List<HelpNodeModel>>("HelpNode" + tenantID.ToString());
            }

            List<HelpNodeModel> result = null;

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                result = new HelpReadOnly(new DbSession(providerName, connectionString), "Source").SearchHelp(tenantID);
            }

            this.Cache.Set("HelpNode" + tenantID.ToString(), result.OrderBy(x => x.HelpNodeNM).ToList(), 1440);

            return result;
        }

        private List<HtmlBlockModel> LoadHTMLBlocks(Guid tenantID, string providerName, string connectionString)
        {
            if (this.Cache.IsSet("HtmlBlock" + tenantID.ToString()))
            {
                return this.Cache.Get<List<HtmlBlockModel>>("HtmlBlock" + tenantID.ToString());
            }

            List<HtmlBlockModel> result = null;

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                result = new HtmlBlocksReadOnly(new DbSession(providerName, connectionString), "Source").SearchHtmlBlocks(tenantID, true);
            }

            this.Cache.Set("HTMLBlock" + tenantID.ToString(), result, 1440);

            return result;
        }

        private List<ImageListModel> LoadImages(Guid tenantID, string providerName, string connectionString)
        {
            if (this.Cache.IsSet("Images" + tenantID.ToString()))
            {
                return this.Cache.Get<List<ImageListModel>>("Images" + tenantID.ToString());
            }

            List<ImageListModel> result = null;

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                result = new ImagesReadOnly(new DbSession(providerName, connectionString), "Source").SearchImages(tenantID, true);
            }

            this.Cache.Set("Images" + tenantID.ToString(), result, 1440);

            return result;
        }

        private List<ServiceListModel> LoadServices(Guid tenantID, string providerName, string connectionString)
        {
            if (this.Cache.IsSet("Services" + tenantID.ToString()))
            {
                return this.Cache.Get<List<ServiceListModel>>("Services" + tenantID.ToString());
            }

            List<ServiceListModel> result = null;

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                result = new ServicesReadOnly(new DbSession(providerName, connectionString), "Source").SearchServices(tenantID);
            }

            this.Cache.Set("Services" + tenantID.ToString(), result, 1440);

            return result;
        }

        private List<CodeLinkTable> GetDataListItemLinks(Guid tenantID, string providerName, string connectionString)
        {
            List<CodeLinkTable> linkers = null;

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                linkers = new GetDataListItemLinksDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(tenantID);
            }

            return linkers;
        } 
             
        private string GenerateDataListItems(List<DataList> selectedDataList)
        {
            StringBuilder dataQuery = new StringBuilder();
            List<CodeListModel> itemList = null;
            dataQuery.AppendLine(this.GetQuery(this.itemDeclarationStart));
            string dataListItemID = string.Empty;

            foreach (DataList list in selectedDataList)
            {
                itemList = this.SourceListItems.FindAll(f => f.ContentID.ToLower() == list.ContentID.ToLower());

                this.GetSelectedItems(ref itemList, list.ContentID);

                foreach (CodeListModel item in itemList)
                {
                    dataListItemID = this.GuidToRAW(Guid.NewGuid().ToString());
                    dataQuery.AppendLine(string.Format(this.GetQuery(this.dataListItemExist), item.ContentID, item.Code));
                    dataQuery.AppendLine(string.Format(this.GetQuery(this.dataListItemAddUpdate), item.OrderIndex, this.BoolToBit(item.IsActive), item.EffectiveStartDate.Value.ToString("dd-MMM-yyyy"), item.EffectiveEndDate.Value.ToString("dd-MMM-yyyy"), this.GuidToRAW(item.ID.ToString()), item.Code, dataListItemID, this.BoolToBit(item.IsActive)));
                    dataQuery.AppendLine(this.GenerateLanguages(item, dataListItemID));
                }
            }

            dataQuery.AppendLine(this.GetQuery(this.commit));
            dataQuery.AppendLine(this.GetQuery(this.declarationEnd));

            return dataQuery.ToString();
        }

        private void GetSelectedItems(ref List<CodeListModel> itemList, string contentID)
        {
            List<SelectedItem> items = null;
            string key = this.listKeyword + contentID;

            if (this.Cache.IsSet(key))
            {
                items = this.Cache.Get<List<SelectedItem>>(key).Where(w => w.Selected == true).ToList();
                itemList = itemList.Where(w => items.Where(v => v.ID == w.ID).Count() > 0).ToList();
            }
        }

        private string GenerateDataListItemLinks(List<DataList> selectedDataList)
        {
            StringBuilder dataQuery = new StringBuilder();
            List<CodeListModel> itemList = null;
            List<CodeLinkTable> links = null;
            dataQuery.AppendLine(this.GetQuery(this.itemLinkDeclarationStart));
            string dataListItemID = string.Empty;

            foreach (DataList list in selectedDataList)
            {
                itemList = this.SourceListItems.FindAll(f => f.ContentID.ToLower() == list.ContentID.ToLower());

                foreach (CodeListModel item in itemList)
                {
                    links = this.SourceLinks.FindAll(x => x.ParentID == item.ID);
                    if (links != null)
                    {
                        dataQuery.AppendLine(this.GetItemLinks(links));
                    }
                }
            }

            dataQuery.AppendLine(this.GetQuery(this.commit));
            dataQuery.AppendLine(this.GetQuery(this.declarationEnd));

            return dataQuery.ToString();
        }

        private string GetItemLinks(List<CodeLinkTable> links)
        {
            StringBuilder dataQuery = new StringBuilder();
            string parentContentID = string.Empty, parentItemCode = string.Empty, childContentID = string.Empty, childItemCode = string.Empty;

            foreach (CodeLinkTable link in links)
            {
                this.GetDataListContentIDCode(link.ParentID, ref parentContentID, ref parentItemCode);
                this.GetDataListContentIDCode(link.ChildID, ref childContentID, ref childItemCode);

                if (!string.IsNullOrEmpty(parentContentID) && !string.IsNullOrEmpty(parentItemCode) && !string.IsNullOrEmpty(childContentID) && !string.IsNullOrEmpty(childItemCode))
                {
                    dataQuery.AppendLine(string.Format(this.GetQuery(this.dataListItemLinkAddUpdate), parentContentID, parentItemCode, childContentID, childItemCode, this.BoolToBit(link.IsActive)));
                }
            }

            return dataQuery.ToString();
        }

        private void GetDataListContentIDCode(Guid itemID, ref string contentID, ref string itemCode)
        {
            CodeListModel item = this.SourceListItems.Find(f => f.ID == itemID);

            if (item != null)
            {
                contentID = item.ContentID;
                itemCode = item.Code;
            }
        }

        private string GenerateLanguages(CodeListModel item, string itemID)
        {
            StringBuilder langQuery = new StringBuilder();

            foreach (Languages lang in item.LanguageList)
            {
                langQuery.AppendLine(string.Format(this.GetQuery(this.dataListItemLanguageCheck), item.ContentID, item.Code, lang.LocaleID));
                langQuery.AppendLine(string.Format(this.GetQuery(this.dataListItemLanguageAddUpdate), lang.Description, this.BoolToBit(lang.IsActive), lang.LongDescription, this.GuidToRAW(item.ID.ToString()), lang.LocaleID, itemID));
            }

            return langQuery.ToString();
        }

        private string GenerateDataList(List<DataList> selectedDataList)
        {
            StringBuilder dataQuery = new StringBuilder();
            dataQuery.AppendLine(this.GetQuery(this.declarationStart));

            foreach (DataList list in selectedDataList)
            {
                dataQuery.AppendLine(string.Format(this.GetQuery(this.dataListCheck), list.ContentID));
                dataQuery.AppendLine(this.GetQuery(this.dataListIFCondition));
                dataQuery.AppendLine(string.Format(this.GetQuery(this.dataListUpdate), list.ContentID, list.DataListsName, list.Description, this.BoolToBit(list.IsActive), this.BoolToBit(list.IsActive)));
                dataQuery.AppendLine(this.GetQuery(this.dataListELSECondition));
                dataQuery.AppendLine(string.Format(this.GetQuery(this.dataListInsert), this.GuidToRAW(Guid.NewGuid().ToString()), this.GuidToRAW(list.TenantModuleID.ToString()), list.ContentID, list.DataListsName, list.Description, this.BoolToBit(list.IsActive), this.GuidToRAW(list.TenantID.ToString()), this.BoolToBit(list.IsActive)));
                dataQuery.AppendLine(this.GetQuery(this.endifCondition));
            }

            dataQuery.AppendLine(this.GetQuery(this.commit));
            dataQuery.AppendLine(this.GetQuery(this.declarationEnd));

            return dataQuery.ToString();
        }

        private string GetQuery(string queryName)
        {
            var query = from row in this.DataListQueryDetails.AsEnumerable()
                        where row.Field<string>("Name").Trim() == queryName
                        select row[1];

            return query.FirstOrDefault().ToString();
        }

        private void SelectAllChkBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.DataListView.Rows)
            {
                row.Cells["Select"].Value = this.SelectAllChkBox.Checked;
            }
        }

        private string GuidToRAW(string text)
        {
            Guid guid = new Guid(text);
            return BitConverter.ToString(guid.ToByteArray()).Replace("-", string.Empty);
        }

        private string BoolToBit(bool flag)
        {
            return flag == true ? "1" : "0";
        }

        private void DataListView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.DataListView.Columns[e.ColumnIndex].Name == "Select")
            {
            }
        }

        private void ModuleListSelectedAppSetting()
        {
            List<TenantModuleModel> selectedModules = new List<TenantModuleModel>();
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            List<AppSettingsModel> filteredModuleList = null;
            TenantModuleModel module = null;
            if (this.ModuleList.SelectedItems.Count > 0)
            {
                foreach (object item in this.ModuleList.SelectedItems)
                {
                    module = item as TenantModuleModel;
                    selectedModules.Add(module);
                }
            }

            if (this.NoOfDays > 0)
            {
                var modulesQuery = from lists in this.SourceAppSettings
                                   join modules in selectedModules
                                        on lists.TenantModuleID equals modules.TenantModuleId
                                   where lists.LastModifiedTimeStamp >= DateTime.UtcNow.AddDays(this.NoOfDays * -1)
                                   select lists;
                filteredModuleList = modulesQuery.ToList();
            }
            else
            {
                var modulesQuery = from lists in this.SourceAppSettings
                                   join modules in selectedModules
                                        on lists.TenantModuleID equals modules.TenantModuleId
                                   select lists;
                filteredModuleList = modulesQuery.ToList();
            }

            this.DataListView.DataSource = new BindingList<AppSettingsModel>(filteredModuleList.Where(w => w.TenantID == tenantID).ToList());
        }

        private void ModuleListSelectedItems()
        {
            List<TenantModuleModel> selectedModules = new List<TenantModuleModel>();
            TenantModuleModel module = null;
            List<DataList> filteredModuleList = null;
            List<DataList> modifiedItems = null;

            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());

            if (this.ModuleList.SelectedItems.Count > 0)
            {
                foreach (object item in this.ModuleList.SelectedItems)
                {
                    module = item as TenantModuleModel;
                    selectedModules.Add(module);
                }
            }

            if (this.NoOfDays > 0)
            {
                var modulesQuery = from lists in this.SourceLists
                                   join modules in selectedModules
                                   on lists.ModuleName equals modules.ModuleName
                                   where lists.LastModified >= DateTime.UtcNow.AddDays(this.NoOfDays * -1)
                                   select lists;
                filteredModuleList = modulesQuery.ToList();

                modifiedItems = this.GetUpdatedListItems(selectedModules);

                filteredModuleList = filteredModuleList.Concat(modifiedItems)
                .GroupBy(item => item.ContentID)
                .Select(group => group.First()).ToList();
            }
            else
            {
                var modulesQuery = from lists in this.SourceLists
                                   join modules in selectedModules
                                   on lists.ModuleName equals modules.ModuleName
                                   select lists;
                filteredModuleList = modulesQuery.ToList();
            }

                            this.DataListView.DataSource = new BindingList<DataList>(filteredModuleList.ToList());              
        }

        private void ModuleMenuListSelectedItems()
        {
            List<TenantModuleModel> selectedModules = new List<TenantModuleModel>();
            List<MenuListModel> filteredModuleList1 = null;
            TenantModuleModel module = null;
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());

            if (this.ModuleList.SelectedItems.Count > 0)
            {
                foreach (object item in this.ModuleList.SelectedItems)
                {
                    module = item as TenantModuleModel;
                    selectedModules.Add(module);
                }
            }

            var modulesQuery = from lists in this.ChildMenuItem
                               join modules in selectedModules
                                    on lists.TenantModuleID equals modules.TenantModuleId
                               select lists;
            filteredModuleList1 = modulesQuery.ToList();

            this.DataListView.DataSource = new BindingList<MenuListModel>(filteredModuleList1.ToList());
        }

        private void ModuleHelpListSelectedItems()
        {
            List<TenantModuleModel> selectedModules = new List<TenantModuleModel>();
            TenantModuleModel module = null;
            List<HelpNodeModel> filteredModuleList = null;
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            string controlName = this.ControlName.SelectedItem.ToString();

            if (this.ModuleList.SelectedItems.Count > 0)
            {
                foreach (object item in this.ModuleList.SelectedItems)
                {
                    module = item as TenantModuleModel;
                    selectedModules.Add(module);
                }
            }

            if (this.NoOfDays > 0)
            {
                var modulesQuery = from lists in this.SourceHelpNodeList
                                   join modules in selectedModules
                                   on lists.TenantModuleId equals modules.TenantModuleId
                                   where lists.LastModifiedTS >= DateTime.UtcNow.AddDays(this.NoOfDays * -1)
                                   select lists;
                filteredModuleList = modulesQuery.ToList();
            }
            else
            {
                var modulesQuery = from lists in this.SourceHelpNodeList
                                   join modules in selectedModules
                                   on lists.TenantModuleId equals modules.TenantModuleId
                                   select lists;
                filteredModuleList = modulesQuery.ToList();
            }

            this.DataListView.DataSource = new BindingList<HelpNodeModel>(filteredModuleList.Where(w => w.TenantId == tenantID).ToList());
        }

        private void ModuleHtmlListSelectedItems()
        {
            List<TenantModuleModel> selectedModules = new List<TenantModuleModel>();
            TenantModuleModel module = null;
            List<HtmlBlockModel> filteredModuleList = null;
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            string controlName = this.ControlName.SelectedItem.ToString();

            if (this.ModuleList.SelectedItems.Count > 0)
            {
                foreach (object item in this.ModuleList.SelectedItems)
                {
                    module = item as TenantModuleModel;
                    selectedModules.Add(module);
                }
            }

            if (this.NoOfDays > 0)
            {
                var modulesQuery = from lists in this.SourceHtmlListItems
                                   join modules in selectedModules
                                   on lists.TenantModuleId equals modules.TenantModuleId
                                   where lists.LastModifiedTS >= DateTime.UtcNow.AddDays(this.NoOfDays * -1) && lists.ContentId.Contains(controlName)
                                   select lists;
                filteredModuleList = modulesQuery.ToList();
            }
            else
            {
                var modulesQuery = from lists in this.SourceHtmlListItems
                                   join modules in selectedModules
                                   on lists.TenantModuleId equals modules.TenantModuleId
                                   where lists.ContentId.Contains(controlName)
                                   select lists;
                filteredModuleList = modulesQuery.ToList();
            }

            this.DataListView.DataSource = new BindingList<HtmlBlockModel>(filteredModuleList.ToList());
        }

        private void ModuleImageListSelectedItems()
        {
            List<TenantModuleModel> selectedModules = new List<TenantModuleModel>();
            TenantModuleModel module = null;
            List<ImageListModel> filteredModuleList = null;
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            string controlName = this.ControlName.SelectedItem.ToString();

            if (this.ModuleList.SelectedItems.Count > 0)
            {
                foreach (object item in this.ModuleList.SelectedItems)
                {
                    module = item as TenantModuleModel;
                    selectedModules.Add(module);
                }
            }

            if (this.NoOfDays > 0)
            {
                var modulesQuery = from lists in this.SourceImagesList
                                   join modules in selectedModules
                                   on lists.TenantModuleId equals modules.TenantModuleId
                                   where lists.LastModifiedTS >= DateTime.UtcNow.AddDays(this.NoOfDays * -1) && lists.ContentId.Contains(controlName)
                                   select lists;
                filteredModuleList = modulesQuery.ToList();
            }
            else
            {
                var modulesQuery = from lists in this.SourceImagesList
                                   join modules in selectedModules
                                   on lists.TenantModuleId equals modules.TenantModuleId
                                   where lists.ContentId.Contains(controlName)
                                   select lists;
                filteredModuleList = modulesQuery.ToList();
            }

            this.DataListView.DataSource = new BindingList<ImageListModel>(filteredModuleList.Where(w => w.TenantId == tenantID).ToList());
        }

        private void ModuleServicesListSelectedItems()
        {
            List<TenantModuleModel> selectedModules = new List<TenantModuleModel>();
            TenantModuleModel module = null;
            List<ServiceListModel> filteredModuleList = null;
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            string controlName = this.ControlName.SelectedItem.ToString();

            if (this.ModuleList.SelectedItems.Count > 0)
            {
                foreach (object item in this.ModuleList.SelectedItems)
                {
                    module = item as TenantModuleModel;
                    selectedModules.Add(module);
                }
            }

            if (this.NoOfDays > 0)
            {
                var modulesQuery = from lists in this.SourceServicesList
                                   join modules in selectedModules
                                   on lists.TenantModuleID equals modules.TenantModuleId
                                   where lists.LastModifiedDate >= DateTime.UtcNow.AddDays(this.NoOfDays * -1) && lists.Name.Contains(controlName)
                                   select lists;
                filteredModuleList = modulesQuery.ToList();
            }
            else
            {
                var modulesQuery = from lists in this.SourceServicesList
                                   join modules in selectedModules
                                   on lists.TenantModuleID equals modules.TenantModuleId
                                   where lists.Name.Contains(controlName)
                                   select lists;
                filteredModuleList = modulesQuery.ToList();
            }

            this.DataListView.DataSource = new BindingList<ServiceListModel>(filteredModuleList.Where(w => w.TenantId == tenantID).ToList());
        }

        private List<DataList> GetUpdatedListItems(List<TenantModuleModel> selectedModules)
        {
            List<DataList> dataLists = null;

            var modulesQuery = from items in this.SourceListItems
                               join lists in this.SourceLists on items.ContentID equals lists.ContentID
                               join modules in selectedModules
                                    on lists.ModuleName equals modules.ModuleName
                               where items.ModifiedDate >= DateTime.UtcNow.AddDays(this.NoOfDays * -1)
                               select lists;

            dataLists = modulesQuery.GroupBy(i => i.ContentID)
                  .Select(group =>
                        new
                        {
                            Key = group.Key,
                            Items = group.OrderByDescending(x => x.ContentID)
                        })
                  .Select(g => g.Items.First()).OrderBy(o => o.ContentID).ToList();
            return dataLists;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataListView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            string caseSwitch = this.ControlName.SelectedItem.ToString();
            switch (caseSwitch)
            {
                case "AppSetting":
                    break;
                case "Datalist":
                    ListItems dataListitemsPage = new ListItems(tenantID, (DataListView.Rows[e.RowIndex].DataBoundItem as DataList).ContentID, this.NoOfDays);
                    dataListitemsPage.ShowDialog();
                    break;
                case "Security":
                    ListItems itemsPage = new ListItems(tenantID, (this.DataListView.Rows[e.RowIndex].DataBoundItem as DataList).ContentID, this.NoOfDays);
                    itemsPage.ShowDialog();
                    break;
                case "Menus":
                    ListMenuItem menusPage = new ListMenuItem(tenantID, (this.DataListView.Rows[e.RowIndex].DataBoundItem as MenuListModel).Name, this.NoOfDays);
                    menusPage.ShowDialog();
                    break;
                case "HtmlBlock":
                    break;
                case "Image":
                    break;
                case "Service":
                    break;
                default:
                    break;
            }
        }

        private void BtnClearCache_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear cache?", "Cache Clear", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Cache.Clear();
            }
        }

        private void BtnDownloadToFile_Click(object sender, EventArgs e)
        {
            if (this.ControlName.SelectedItem != null)
            {
                string caseSwitch = this.ControlName.SelectedItem.ToString();
                this.CheckPathExist(this.QueryFilePath);
                bool dataAvailable = true;
                switch (caseSwitch)
                {
                    case "AppSetting":
                        List<AppSettingsModel> listsAppSetting = this.ConvertToCustomAppSetting();
                        dataAvailable = listsAppSetting == null ? false : true;
                        File.WriteAllText(this.QueryFilePath + "\\" + caseSwitch + ".list", JsonConvert.SerializeObject(listsAppSetting));
                        break;
                    case "Datalist":
                        List<DataListMainModel> listsMain = this.ConvertToCustomDataList();
                        dataAvailable = listsMain == null ? false : true;
                        File.WriteAllText(this.QueryFilePath + "\\" + caseSwitch + ".list", JsonConvert.SerializeObject(listsMain));
                        break;
                    case "Menus":
                        List<MenuListModel> listsMenu = this.ConvertToCustomMenus();
                        dataAvailable = listsMenu == null ? false : true;
                        File.WriteAllText(this.QueryFilePath + "\\" + caseSwitch + ".list", JsonConvert.SerializeObject(listsMenu));
                        break;
                    case "HtmlBlock":
                        List<HtmlBlockMainModel> htmlBlks = this.ConvertToCustomHtmlBlks();
                        dataAvailable = htmlBlks == null ? false : true;
                        File.WriteAllText(this.QueryFilePath + "\\" + caseSwitch + ".list", JsonConvert.SerializeObject(htmlBlks));
                        break;
                    case "Security":
                        List<DataListMainModel> listsSecurityMain = this.ConvertToCustomSecurityDataList();
                        dataAvailable = listsSecurityMain == null ? false : true;
                        File.WriteAllText(this.QueryFilePath + "\\" + caseSwitch + ".list", JsonConvert.SerializeObject(listsSecurityMain));
                        break;
                    case "Image":
                        List<ImagesMainModel> images = this.ConvertToCustomImages();
                        dataAvailable = images == null ? false : true;
                        File.WriteAllText(this.QueryFilePath + "\\" + caseSwitch + ".list", JsonConvert.SerializeObject(images));
                        break;
                    case "Help":
                        List<HelpNodeModel> helpNodes = this.ConvertToCustomHelp();
                        dataAvailable = helpNodes == null ? false : true;
                        File.WriteAllText(this.QueryFilePath + "\\" + caseSwitch + ".list", JsonConvert.SerializeObject(helpNodes));
                        break;
                    case "Service":
                        List<ServicesMainModel> services = this.ConvertToCustomServices();
                        dataAvailable = services == null ? false : true;
                        File.WriteAllText(this.QueryFilePath + "\\" + caseSwitch + ".list", JsonConvert.SerializeObject(services));
                        break;
                    default:
                        break;
                }

                if (dataAvailable)
                {
                    MessageBox.Show("Download completed!");
                    BtnDownloadToFile.Enabled = false;
                    Process.Start("explorer.exe", this.QueryFilePath);
                }
            }
            else
            {
                MessageBox.Show("Please select control name from the drop down");
                return;
            }
        }

        private List<AppSettingsModel> ConvertToCustomAppSetting()
        {
            List<AppSettingsModel> lists = null;
            List<AppSettingsModel> listsMain = new List<AppSettingsModel>();
            AppSettingsModel list1 = null;

            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());

            if (this.SourceAppSettings != null)
            {
                lists = this.SourceAppSettings.ToList();
                List<TenantModuleModel> modules = this.Cache.Get<List<TenantModuleModel>>("TenantModules" + tenantID.ToString());
                foreach (AppSettingsModel list in lists)
                {
                    list1 = new AppSettingsModel()
                    {
                        TenantModuleAppSettingId = list.TenantModuleAppSettingId,
                        ApplicationId = list.ApplicationId,
                        AppSettingKey = list.AppSettingKey,
                        Value = list.Value,
                        TargetValue = list.Value,
                        SettingTypeItemKey = list.SettingTypeItemKey,
                        Description = list.Description,
                        TenantModuleID = list.TenantModuleID,
                        IsActive = list.IsActive,
                        ModuleName = list.ModuleName,
                        TenantID = list.TenantID,
                        OperatorID = Environment.UserName
                    };

                    listsMain.Add(list1);
                }
            }

            return listsMain;
        }

        private List<DataListMainModel> ConvertToCustomDataList()
        {
            List<DataList> lists = null;
            List<DataListMainModel> listsMain = new List<DataListMainModel>();
            DataListMainModel list1 = null;

            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());

            if (this.SourceLists != null)
            {
                lists = this.SourceLists.ToList();
                List<TenantModuleModel> modules = this.Cache.Get<List<TenantModuleModel>>("TenantModules" + tenantID.ToString());
                List<DataListAttribute> attributes = new List<DataListAttribute>();
                lists.ForEach(x => 
                {
                    if (x.DataListAttributes != null)
                    {
                        attributes.AddRange(x.DataListAttributes);
                    }
                });

                foreach (DataList list in lists)
                {
                    list1 = new DataListMainModel()
                    {
                        ContentID = list.ContentID,
                        Description = list.Description,
                        IsActive = list.IsActive,
                        IsEditable = list.IsEditable,
                        ReleaseStatus = list.ReleaseStatus,
                        ModuleName = modules.Find(f => f.TenantModuleId == list.TenantModuleID).ModuleName,
                        Items = this.ConvertToCustomDataListItems(list.ContentID, list.TenantID, this.SourceListItems, attributes),
                        DataListAttributes = this.ConverttoAttributes(list.DataListAttributes, list.ContentID, list.TenantID, list.ID),
                        TenantID = list.TenantID,
                        Name = list.DataListsName,
                        TenantModuleID = list.TenantModuleID
                    };

                    listsMain.Add(list1);
                }
            }

            return listsMain;
        }

        private List<HtmlBlockMainModel> ConvertToCustomHtmlBlks()
        {
            List<HtmlBlockModel> lists = null;
            List<HtmlBlockMainModel> listsMain = new List<HtmlBlockMainModel>();
            HtmlBlockMainModel list1 = null;
            List<HtmlBlockLanguagesModel> htmlLangs = new List<HtmlBlockLanguagesModel>();
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());

            if (this.SourceHtmlListItems != null)
            { 
                lists = this.SourceHtmlListItems.ToList();
                List<TenantModuleModel> modules = this.Cache.Get<List<TenantModuleModel>>("TenantModules" + tenantID.ToString());

                this.SourceHtmlListItems.ForEach(x =>
                {
                    htmlLangs.AddRange(x.HtmlBlockLanguages.ToList());
                });

                foreach (HtmlBlockModel list in lists)
                {
                    list1 = new HtmlBlockMainModel()
                    {
                        ContentId = list.ContentId,
                        Description = list.Description,
                        ID = list.ID,
                        HtmlBlockLanguages = this.ConvertToCustomHtmlLang(list.ContentId, list.ID, htmlLangs),
                        LastModifiedTS = list.LastModifiedTS,
                        OperatorId = list.OperatorId,
                        TenantModuleId = list.TenantModuleId
                    };

                    listsMain.Add(list1);
                }
            }

            return listsMain;
        }

        private List<HtmlBlockLanguage> ConvertToCustomHtmlLang(string contentID, Guid iD, List<HtmlBlockLanguagesModel> htmlLangs)
        {
            List<HtmlBlockLanguage> items = new List<HtmlBlockLanguage>();
            HtmlBlockLanguage item = null;

            htmlLangs = htmlLangs.Where(w => w.ID == iD).ToList();

            htmlLangs.ForEach(e =>
            {
                item = new HtmlBlockLanguage()
                {
                    HtmlBlockId = e.ID,
                    LocaleId = e.LocaleId,
                    Html = e.Html,
                    IsActive = e.IsActive,
                    OperatorId = e.OperatorId,
                    LastModifiedTS = e.LastModifiedTS
                };
                items.Add(item);
            });

            return items;
        }

        private List<ImagesMainModel> ConvertToCustomImages()
        {
            List<ImageListModel> lists = null;
            List<ImagesMainModel> listsMain = new List<ImagesMainModel>();
            ImagesMainModel list1 = null;
            List<ImageLanguagesModel> imageLangs = new List<ImageLanguagesModel>();
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());

            if (this.SourceImagesList != null)
            {
                lists = this.SourceImagesList.ToList();
                List<TenantModuleModel> modules = this.Cache.Get<List<TenantModuleModel>>("TenantModules" + tenantID.ToString());

                this.SourceImagesList.ForEach(x =>
                {
                    imageLangs.AddRange(x.ImageLanguages.ToList());
                });

                foreach (ImageListModel list in lists)
                {
                    list1 = new ImagesMainModel()
                    {
                        ContentId = list.ContentId,
                        Description = list.Description,
                        ImageId = list.ID,
                        ImageLanguages = this.ConvertToCustomImageLang(list.ContentId, list.ID, imageLangs),
                        LastModifiedTS = list.LastModifiedTS,
                        OperatorId = list.OperatorId,
                        TenantModuleId = list.TenantModuleId
                    };

                    listsMain.Add(list1);
                }
            }

            return listsMain;
        }

        private List<ImageLanguage> ConvertToCustomImageLang(string contentID, Guid iD, List<ImageLanguagesModel> htmlLangs)
        {
            List<ImageLanguage> items = new List<ImageLanguage>();
            ImageLanguage item = null;

            htmlLangs = htmlLangs.Where(w => w.ID == iD).ToList();

            htmlLangs.ForEach(e =>
            {
                item = new ImageLanguage()
                {
                    ImageId = e.ID,
                    LocaleId = e.LocalId,
                    Source = e.Source,
                    Width = e.Width,
                    Height = e.Height,
                    ToolTip = e.ToolTip,
                    IsActive = e.IsActive,
                    OperatorId = e.OperatorId,
                    LastModifiedTS = e.LastModifiedTS
                };
                items.Add(item);
            });

            return items;
        }

        private List<HelpContentLanguageModel> ConvertToCustomHelpLang(string helpNodeNM, Guid helpNodeID, List<HelpContentLanguageModel> helpLangs)
        {
            List<HelpContentLanguageModel> items = new List<HelpContentLanguageModel>();
            HelpContentLanguageModel item = null;

            helpLangs = helpLangs.Where(w => w.HelpNodeId == helpNodeID).ToList();

            helpLangs.ForEach(e =>
            {
                item = new HelpContentLanguageModel()
                {
                    HelpNodeId = e.HelpNodeId,
                    Language = e.Language,
                    HtmlContent = e.HtmlContent
                };
                items.Add(item);
            });

            return items;
        }

        private List<ServicesMainModel> ConvertToCustomServices()
        {
            List<ServiceListModel> lists = null;
            List<ServicesMainModel> listsMain = new List<ServicesMainModel>();
            ServicesMainModel list1 = null;
            
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());

            if (this.SourceServicesList != null)
            {
                lists = this.SourceServicesList.Where(w => w.TenantId == tenantID).ToList();
                List<TenantModuleModel> modules = this.Cache.Get<List<TenantModuleModel>>("TenantModules");

                foreach (ServiceListModel list in lists)
                {
                    list1 = new ServicesMainModel()
                    {
                        Name = list.Name,
                        SecurityRightItemContentID = list.SecurityRightItemContentID,
                        LabelItemKey = list.LabelContentID,
                        ServiceID = list.ID,
                        DefaultText = list.DefaultText,
                        BaseURL = list.BaseURL,
                        IOCContainer = list.IOCContainer,
                        IsActive = list.IsActive,
                        LastModifiedDate = list.LastModifiedDate,
                        OperatorID = list.OperatorID,
                        TenantModuleID = list.TenantModuleID
                    };

                    listsMain.Add(list1);
                }
            }

            return listsMain;
        }

        private List<MenuListModel> ConvertToCustomMenus()
        {
            List<MenuListModel> lists = null;
            List<MenuListModel> listsMain = new List<MenuListModel>();
            MenuListModel list1 = null;

            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());

            if (this.SourceMenus != null)
            {
                lists = this.SourceMenus.ToList();
                List<TenantModuleModel> modules = this.Cache.Get<List<TenantModuleModel>>("TenantModules" + tenantID.ToString());
                foreach (MenuListModel list in lists)
                {
                    list1 = new MenuListModel()
                    {
                        ID = list.ID,
                        TenantModuleID = list.TenantModuleID,
                        IsActive = list.IsActive,
                        Name = list.Name,
                        SecurityRightItemContentID = list.SecurityRightItemContentID,
                        DisplaySize = list.DisplaySize,
                        OperatorID = list.OperatorID,
                        Level = list.Level,
                        TenantId = list.TenantId,
                        Children = this.ConvertToCustomMenuListItems(list.ID),
                    };

                    listsMain.Add(list1);
                }
            }

            return listsMain;
        }

        private IList<MenuItemModel> ConvertToCustomMenuListItems(Guid iD)
        {
            List<MenuItemModel> listItems = null;
            List<MenuItemModel> items = new List<MenuItemModel>();
            MenuItemModel item = null;
            listItems = this.SourceMenuItems.Where(w => w.MenuID == iD).ToList();

            listItems.ForEach(e =>
            {
                item = new MenuItemModel()
                {
                    ID = e.ID,
                    MenuID = e.MenuID,
                    IsActive = e.IsActive,
                    ParentMenuItemID = e.ParentMenuItemID,
                    Name = e.Name,
                    OrderIndex = e.OrderIndex,
                    SecurityRightItemContentID = e.SecurityRightItemContentID,
                    LabelItemContentID = e.LabelItemContentID,
                    DefaultText = e.DefaultText,
                    BaseURL = e.BaseURL,
                    CSSClass = e.CSSClass,
                    IOCContainer = e.IOCContainer,
                    ModuleSectionContentID = e.ModuleSectionContentID,
                    PageHelpHTMLContentID = e.PageHelpHTMLContentID,
                    ReportContentsURL = e.ReportContentsURL,
                    PrintPreviewContentURL = e.PrintPreviewContentURL,
                    MITAHelpHTMLContentID = e.MITAHelpHTMLContentID,
                    IsVisible = e.IsVisible,
                    LastModifiedDate = e.LastModifiedDate,
                    OperatorID = e.OperatorID,
                    Level = e.Level,
                    AuditContentURL = e.AuditContentURL,
                    ContactUsContentURL = e.ContactUsContentURL,
                };
                items.Add(item);
            });

            return items;
        }

        private List<HelpNodeModel> ConvertToCustomHelp()
        {
            List<HelpNodeModel> helplists = null;
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            List<TenantModuleModel> selectedModules = new List<TenantModuleModel>();
            if (this.SourceHelpNodeList != null)
            {
                helplists = this.SourceHelpNodeList.ToList();
            }

            return helplists;
        }

        private List<ItemAttribute> ConverttoAttributes(List<DataListAttribute> list, string parentContentId, Guid tenantID, Guid dataListID)
        {
            List<ItemAttribute> item = new List<ItemAttribute>();

            foreach (DataListAttribute listattribute in list)
            {
                ItemAttribute itemList = new ItemAttribute();
                itemList.ContentID = listattribute.DataListTypeName;
                itemList.Code = listattribute.DefaultTypeText;
                itemList.ParentContentId = parentContentId;
                itemList.IsActive = listattribute.IsActive;
                itemList.DataListID = listattribute.DataListID;
                itemList.DataListTypeID = listattribute.DataListTypeID;
                itemList.ID = listattribute.ID;
                itemList.TenantID = tenantID;
                itemList.DefaultTypeValue = listattribute.DefaultTypeValue;
                itemList.TypeName = listattribute.TypeName;
                item.Add(itemList);
            }

            return item;
        }

        private List<CodeItemModel> ConvertToCustomDataListItems(string contentID, Guid tenantID, List<CodeListModel> listdatalistitems, List<DataListAttribute> datalistattribute)
        {
            List<CodeListModel> listItems = null;
            List<CodeItemModel> items = new List<CodeItemModel>();
            CodeItemModel item = null;
            listItems = this.SourceListItems.Where(w => w.ContentID == contentID).ToList();

            listItems.ForEach(e =>
            {
                item = new CodeItemModel()
                {
                    Code = e.Code,
                    ContentID = e.ContentID,
                    EffectiveEndDate = e.EffectiveEndDate == null ? DateTime.UtcNow.AddYears(1) : e.EffectiveEndDate,
                    EffectiveStartDate = e.EffectiveStartDate == null ? DateTime.UtcNow : e.EffectiveStartDate,
                    IsActive = e.IsActive,
                    IsEditable = e.IsEditable,
                    LanguageList = this.GetLanguageListCustom(e.LanguageList),
                    OrderIndex = e.OrderIndex,
                    TenantID = e.TenantID,
                    ID = e.ID,
                    Attributes = this.GetCustomItemAttributes(e, listdatalistitems, datalistattribute, Resultitems, e.ContentID)
                };
                items.Add(item);
            });

            return items;
        }

        private List<ItemLanguage> GetLanguageListCustom(List<Languages> languageList)
        {
            List<ItemLanguage> languages = new List<ItemLanguage>();
            ItemLanguage language = null;

            languageList.ForEach(e =>
            {
                language = new ItemLanguage()
                {
                    Description = e.Description,
                    LocaleID = e.LocaleID,
                    LongDescription = e.LongDescription,
                    IsActive = e.IsActive
                };
                languages.Add(language);
            });

            return languages;
        }

        private void TenantList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadModules();
        }

        private void BtnCompare_Click(object sender, EventArgs e)
        {
            if (this.ControlName.SelectedItem != null)
            {
                string selectedvalue = this.ControlName.SelectedItem.ToString();
                string selectedTenant = this.TenantList.Text.ToString();
                DatalistComparer compare = new DatalistComparer(selectedvalue, selectedTenant);
                compare.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select control name from the drop down");
                return;
            }                    
        }

        private void ControlName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadModules();
            BtnDownloadToFile.Enabled = false;
        }

        private void LoadControls()
        {
            List<string> controlNames = new List<string>(new string[] { "AppSetting", "Datalist", "HtmlBlock", "Image", "Menus", "Security", "Help", "Service" });
            for (int i = 0; i <= controlNames.Count - 1; i++)
            {
                this.ControlName.Items.Add(controlNames[i]);
            }
        }

        private List<ItemDataListItemAttributeVal> GetCustomItemAttributes(CodeListModel toExpand, List<CodeListModel> items, List<DataListAttribute> listAttributes, List<ItemDataListItemAttributeVal> itemAttribues, string contentID)
        {
            List<ItemDataListItemAttributeVal> itemAttrValues = new List<ItemDataListItemAttributeVal>();
            itemAttrValues = itemAttribues.FindAll(x => x.DataListItemID == toExpand.ID).ToList();
            if (itemAttrValues.Count > 0)
            {
                itemAttrValues.ForEach(y => y.DataListAttributeValue = items.Find(c => c.ID == y.DataListValueID).Code);
                itemAttrValues.ForEach(y =>
                {
                    DataListAttribute attributes = new DataListAttribute();
                    attributes = listAttributes.Find(d => d.ID == y.DataListAttributeID);
                    if (attributes != null)
                    {
                        y.DataListAttributeName = attributes.TypeName;
                        y.DataListTypeName = attributes.DataListTypeName;
                    }
                });
            }

            return itemAttrValues;
        }

        private void ModuleListSelectedSecurityItems()
        {
            List<TenantModuleModel> selectedModules = new List<TenantModuleModel>();
            TenantModuleModel module = null;
            List<DataList> filteredModuleList = null;
            List<DataList> modifiedItems = null;

            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());

            if (this.ModuleList.SelectedItems.Count > 0)
            {
                foreach (object item in this.ModuleList.SelectedItems)
                {
                    module = item as TenantModuleModel;
                    selectedModules.Add(module);
                }
            }

            if (this.NoOfDays > 0)
            {
                var modulesQuery = from lists in this.SourceLists
                                   join modules in selectedModules
                                   on lists.ModuleName equals modules.ModuleName
                                   where lists.LastModified >= DateTime.UtcNow.AddDays(this.NoOfDays * -1)
                                   select lists;
                filteredModuleList = modulesQuery.ToList();

                modifiedItems = this.GetUpdatedListItems(selectedModules);

                filteredModuleList = filteredModuleList.Concat(modifiedItems)
                .GroupBy(item => item.ContentID)
                .Select(group => group.First()).ToList();
            }
            else
            {
                var modulesQuery = from lists in this.SourceLists
                                   join modules in selectedModules
                                   on lists.ModuleName equals modules.ModuleName
                                   select lists;
                filteredModuleList = modulesQuery.ToList();
            }

            this.DataListView.DataSource = new BindingList<DataList>(filteredModuleList.Where(w => w.TenantID == tenantID && (w.ContentID == this.securityFunction || w.ContentID == this.securityRoles || w.ContentID == this.securityRight)).ToList());
        }

        private List<DataListMainModel> ConvertToCustomSecurityDataList()
        {
            List<DataList> lists = null;
            List<DataListMainModel> listsMain = new List<DataListMainModel>();
            DataListMainModel list1 = null;

            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());

            if (this.SourceLists != null)
            {
                lists = this.SourceLists.Where(w => (w.ContentID == this.securityFunction || w.ContentID == this.securityRoles || w.ContentID == this.securityRight)).ToList();
                List<TenantModuleModel> modules = this.Cache.Get<List<TenantModuleModel>>("TenantModules" + tenantID.ToString());
                List<DataListAttribute> attributes = new List<DataListAttribute>();
                lists.ForEach(x => { attributes.AddRange(x.DataListAttributes); });

                foreach (DataList list in lists)
                {
                    list1 = new DataListMainModel()
                    {
                        ContentID = list.ContentID,
                        Description = list.Description,
                        IsActive = list.IsActive,
                        IsEditable = list.IsEditable,
                        ReleaseStatus = list.ReleaseStatus,
                        ModuleName = modules.Find(f => f.TenantModuleId == list.TenantModuleID).ModuleName,
                        Items = this.ConvertToCustomDataListItems(list.ContentID, list.TenantID, this.SourceListItems, attributes),
                        DataListAttributes = this.ConverttoAttributes(list.DataListAttributes, list.ContentID, list.TenantID, list.ID),
                        TenantID = list.TenantID
                    };

                    listsMain.Add(list1);
                }
            }

            return listsMain;
        }

        private void CheckPathExist(string filepath)
        {
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
        }

        private void InactiveCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.DataListView.Rows)
            {
                bool visible = Convert.ToBoolean(row.Cells["IsActive"].Value);
                CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[DataListView.DataSource];
                currencyManager1.SuspendBinding();
                if (InactiveCB.Checked)
                {
                    if (!visible)
                    {
                        DataListView.Rows[row.Index].Visible = true; 
                    }
                    else
                    {
                        DataListView.Rows[row.Index].Visible = false;
                    }
                }
                else
                {
                    DataListView.Rows[row.Index].Visible = true;
                }

                currencyManager1.ResumeBinding();
            }
        }
    }
}
