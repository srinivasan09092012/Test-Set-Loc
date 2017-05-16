//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using DatalistSyncUtil.Configs;
using DatalistSyncUtil.Views;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Config;
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

        public List<MenuListModel> SourceMenus { get; set; }

        public List<MenuItemModel> SourceMenuItems { get; set; }

        public List<HtmlBlockModel> SourceHtmlListItems { get; set; }

        public List<CodeLinkTable> SourceLinks { get; set; }

        public DataTable DataListQueryDetails { get; set; }

        public ICacheManager Cache { get; set; }

        public string DataListsQueryPath { get; set; }

        public string DataListItemsQueryPath { get; set; }

        public string QueryFilePath { get; set; }

        public List<ItemDataListItemAttributeVal> resultitems { get; set; }
  

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
            if (!this.Cache.IsSet("TenantModules"))
            {
                this.TenantModules = this.GetTenantModules(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);
                this.Cache.Set("TenantModules", this.TenantModules, 1440);
            }

            this.ModuleList.DataSource = this.Cache.Get<List<TenantModuleModel>>("TenantModules").Where(w => w.TenantId == tenantID).GroupBy(i => i.ModuleName)
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
            Cursor.Current = Cursors.WaitCursor;
            this.SourceListItems = this.LoadDataListItems(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);
            List<CodeListModel> test = this.SourceListItems.Where(x => x.ContentID.Contains("Core.DataList.ProviderTypes")).ToList();
            this.resultitems = this.LoadDataListItemAttributes();
            
            this.LoadSearchCriteria();
            Cursor.Current = Cursors.Default;
            ////this.SourceLinks = this.GetDataListItemLinks(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);
            ////this.Cache.Set("DataListItemLinks", this.SourceLinks, 1440);
        }

        private List<ItemDataListItemAttributeVal> LoadDataListItemAttributes()
        {
            if (!this.Cache.IsSet("SourceSyncDataListItemAttributes"))
            {
                using (IDbSession session = new DbSession(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString))
                {
                    resultitems = new DataListAttributesReadOnly(new DbSession(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString), "Source").GetDataListItemAttributes();
                    this.Cache.Set("SourceDataListItemAttributes", resultitems, 1440);
                }
            }
            else
            {
                resultitems = this.Cache.Get<List<ItemDataListItemAttributeVal>>("SourceSyncDataListItemAttributes");
            }

            return resultitems;
        }

        private void LoadSearchCriteria()
        {
            this.NoOfDays = (int)this.Age.Value;
            string caseSwitch = this.ControlName.SelectedItem.ToString();
            switch (caseSwitch)
            {
                case "Datalist":
                    this.SourceListItems = this.LoadDataListItems(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);
                    this.DataListView.Columns[1].Visible = true;
                    this.DataListView.Columns[2].Visible = false;
                    this.BindDataList();
                    this.ModuleListSelectedItems();
                    break;
                case "Menus":
                    this.DataListView.Columns[2].Visible = true;
                    this.DataListView.Columns[1].Visible = false;
                    this.BindMenus();
                    this.ModuleMenuListSelectedItems();
                    break;
                case "HtmlBlock":
                    this.SourceHtmlListItems = this.LoadHTMLBlocks(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);
                    this.DataListView.Columns[1].Visible = true;
                    this.DataListView.Columns[2].Visible = false;
                    this.BindHTMLBlock();
                    this.ModuleHtmlListSelectedItems();
                    break;
                default:
                    break;
            }
        }

        private List<TenantModuleModel> GetTenantModules(string providerName, string connectionString)
        {
            List<TenantModuleModel> result = null;

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                result = new GetTenantModuleDaoHelper(new TenantModuleDbContext(session, true)).ExecuteProcedure();
            }

            return result;
        }

        private void BindDataList()
        {
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            List<DataListAttribute> datalistattribute = new List<DataListAttribute>();
            if (!this.Cache.IsSet("DataLists"))
            {
                this.SourceLists = this.LoadDataList(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);
               
                if (!this.Cache.IsSet("SourceDataSyncAttributeList"))
                {
                    using (IDbSession session = new DbSession(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString))
                    {
                        datalistattribute = new GetDataListAttributesDaoHelper(new DataListAttributeDbContext(session, true)).ExecuteProcedure();
                        this.Cache.Set("SourceDataSyncAttributeList", datalistattribute, 1440);
                    }
                }
                else
                {
                    datalistattribute = this.Cache.Get<List<DataListAttribute>>("SourceDataSyncAttributeList");
                }

                this.SourceLists.ForEach(x => x.DataListAttributes = datalistattribute.FindAll(c => c.DataListID == x.ID));
                this.SourceLists.ForEach(x => x.DataListAttributes.ForEach(y => y.DataListTypeName = this.SourceLists.Find(p => p.ID == y.DataListTypeID).ContentID));
                this.SourceLists.ForEach(x => x.DataListAttributes.ForEach(y => y.DefaultTypeText = this.SourceListItems.Find(c => c.ID == y.DefaultTypeValue).Code));
                this.Cache.Set("DataLists", this.SourceLists, 1440);
            }
            else
            {
                this.SourceLists = this.Cache.Get<List<DataList>>("DataLists");
            }

            this.DataListView.AutoGenerateColumns = false;

            if (this.SkipNoOfDays)
            {
                this.DataListView.DataSource = new BindingList<DataList>(this.SourceLists.Where(w => w.TenantID == tenantID).ToList());
            }
            else
            {
                this.DataListView.DataSource = new BindingList<DataList>(this.SourceLists.Where(w => w.TenantID == tenantID && w.LastModified.Value >= DateTime.UtcNow.AddDays(this.NoOfDays)).ToList());
            }
        }

        private void BindMenus()
        {
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            this.SourceMenus = this.LoadMenu(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);
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

        private void BindHTMLBlock()
        {
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());
            List<HtmlBlockModel> htmlblocklist = new List<HtmlBlockModel>();
            List<HtmlBlockLanguagesModel> htmlBlockLangs = new List<HtmlBlockLanguagesModel>();
            if (!this.Cache.IsSet("HtmlBlock"))
            {
                this.SourceHtmlListItems = this.LoadHTMLBlockList(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString);

                if (!this.Cache.IsSet("HtmlBlock"))
                {
                    using (IDbSession session = new DbSession(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString))
                    {
                        htmlblocklist = new HtmlBlocksReadOnly(new DbSession(this.SourceConnectionString.ProviderName, this.SourceConnectionString.ConnectionString), "Source").SearchHtmlBlocks(true);
                        this.Cache.Set("HtmlBlock", htmlblocklist, 1440);
                    }
                }
                else
                {
                    htmlblocklist = this.Cache.Get<List<HtmlBlockModel>>("HtmlBlock");
                }

                this.Cache.Set("HtmlBlock", this.SourceHtmlListItems, 1440);
            }
            else
            {
                this.SourceHtmlListItems = this.Cache.Get<List<HtmlBlockModel>>("HtmlBlock");
            }

            this.DataListView.AutoGenerateColumns = false;

            if (this.SkipNoOfDays)
            {
                this.DataListView.DataSource = new BindingList<HtmlBlockModel>(this.SourceHtmlListItems.Where(w => w.TenantId == tenantID).ToList());
            }
            else
            {
                this.DataListView.DataSource = new BindingList<HtmlBlockModel>(this.SourceHtmlListItems.Where(w => w.TenantId == tenantID && w.LastModifiedTS >= DateTime.UtcNow.AddDays(this.NoOfDays)).ToList());
            }
        }

        private List<HtmlBlockModel> LoadHTMLBlockList(string providerName, string connectionString)
        {
            List<HtmlBlockModel> result = null;

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                result = new HtmlBlocksReadOnly(new DbSession(providerName, connectionString), "Source").SearchHtmlBlocks(true);
            }

            this.Cache.Set("HTMLBlocks", result.OrderBy(o => o.ID).ToList(), 1440);

            return result;
        }

        private List<DataList> LoadDataList(string providerName, string connectionString)
        {
            List<DataList> result = null;

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                result = new SearchDataListDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
            }

            this.Cache.Set("DataLists", result.OrderBy(o => o.ContentID).ToList(), 1440);

            return result;
        }

        private List<CodeListModel> LoadDataListItems(string providerName, string connectionString)
        {
            if (this.Cache.IsSet("DataListItems"))
            {
                return this.Cache.Get<List<CodeListModel>>("DataListItems");
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
                    result = new SearchDataListItemsDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure(string.Empty);
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    languages = new SearchDataListLanguagesDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    resultmsg = new MessageCodeReadOnly(new DbSession(providerName, connectionString), "Source").SearchMessages();
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    resultlbl = new LabelsCodeReadOnly(new DbSession(providerName, connectionString), "Source").SearchLabels();
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    resultsecrights = new SecurityCodeReadOnly(new DbSession(providerName, connectionString)).SearchCodeTables("Source");
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
      
            this.Cache.Set("DataListItems", result, 1440);

            return result;
        }

        private List<MenuListModel> LoadMenu(string providerName, string connectionString)
        {
            if (this.Cache.IsSet("Menus"))
            {
                return this.Cache.Get<List<MenuListModel>>("Menus");
            }

            List<MenuListModel> resultmenu = new List<MenuListModel>();

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                resultmenu = new MenusReadOnly(new DbSession(providerName, connectionString), "Source").SearchMenus(true);
            }

            this.Cache.Set("Menus", resultmenu, 1440);

            return resultmenu;
        }

        private List<HtmlBlockModel> LoadHTMLBlocks(string providerName, string connectionString)
        {
            if (this.Cache.IsSet("HtmlBlock"))
            {
                return this.Cache.Get<List<HtmlBlockModel>>("HtmlBlock");
            }

            List<HtmlBlockModel> result = null;

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                result = new HtmlBlocksReadOnly(new DbSession(providerName, connectionString), "Source").SearchHtmlBlocks(true);
            }

            this.Cache.Set("HTMLBlock", result, 1440);

            return result;
        }

        private List<CodeLinkTable> GetDataListItemLinks(string providerName, string connectionString)
        {
            List<CodeLinkTable> linkers = null;

            using (IDbSession session = new DbSession(providerName, connectionString))
            {
                linkers = new GetDataListItemLinksDaoHelper(new DataListsDbContext(session, true)).ExecuteProcedure();
            }

            return linkers;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            bool selected = false;
            this.ListContents = new List<string>();
            this.ListItemContents = new List<string>();
            List<DataList> selectedDataList = null;
            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());

            foreach (DataGridViewRow row in this.DataListView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells["Select"].Value);

                if (selected)
                {
                    this.ListContents.Add(row.Cells["ContentID"].Value.ToString());
                }
            }

            this.SourceLists = this.Cache.Get<List<DataList>>("DataLists");
            this.SourceListItems = this.Cache.Get<List<CodeListModel>>("DataListItems");

            ///this.SourceLinks = this.Cache.Get<List<CodeLinkTable>>("DataListItemLinks");
            ///this.ListContents.Add("PlanManagement.DataList.SP29Test29123");
            ///this.ListContents.Add("ProviderManagement.DataList.RateType");
            ///this.ListContents.Add("ProviderManagement.DataList.RelationshipTypes");

            selectedDataList = this.SourceLists.Where(w => this.ListContents.Contains(w.ContentID) && w.TenantID == tenantID).ToList();

            File.WriteAllText(this.DataListsQueryPath + DateTime.UtcNow.Ticks + ".sql", this.GenerateDataList(selectedDataList));

            File.WriteAllText(this.DataListItemsQueryPath + DateTime.UtcNow.Ticks + ".sql", this.GenerateDataListItems(selectedDataList));
            if (MessageBox.Show("Datalist and Datalist Items SQL files generated successfully. Do you want to close?", "Success", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Close();
            }

            Process.Start("explorer.exe", this.QueryFilePath);
            ////this.GenerateDataListItemLinks(selectedDataList);
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

                            this.DataListView.DataSource = new BindingList<DataList>(filteredModuleList.Where(w => w.TenantID == tenantID).ToList());              
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

            this.DataListView.DataSource = new BindingList<MenuListModel>(filteredModuleList1.Where(w => w.TenantId == tenantID).ToList());
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

            this.DataListView.DataSource = new BindingList<HtmlBlockModel>(filteredModuleList.Where(w => w.TenantId == tenantID).ToList());
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
            string caseSwitch = this.ControlName.SelectedItem.ToString();
            switch (caseSwitch)
            {
                case "Datalist":
                    ListItems itemsPage = new ListItems((this.DataListView.Rows[e.RowIndex].DataBoundItem as DataList).ContentID, this.NoOfDays);
                    itemsPage.ShowDialog();
                    break;
                case "Menus":
                    ListMenuItem menusPage = new ListMenuItem((this.DataListView.Rows[e.RowIndex].DataBoundItem as MenuListModel).Name, this.NoOfDays);
                    menusPage.ShowDialog();
                    break;
                case "HtmlBlock":
                    break;
                default:
                    break;
            }       
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear items selected?", "Clear Items", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Cache.ClearPatternMatch("CodeList_*");
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
            string caseSwitch = this.ControlName.SelectedItem.ToString();
            switch (caseSwitch)
            {
                case "Datalist":
                    List<DataListMainModel> listsMain = this.ConvertToCustomDataList();
                    File.WriteAllText(this.QueryFilePath + "\\" + (this.TenantList.SelectedItem as TenantModel).TenantName + ".list", JsonConvert.SerializeObject(listsMain));
                    break;
                case "Menus":
                    List<MenuListModel> listsMenu = this.ConvertToCustomMenus();
                    File.WriteAllText(this.QueryFilePath + "\\" + (this.TenantList.SelectedItem as TenantModel).TenantName + ".list", JsonConvert.SerializeObject(listsMenu));
                    break;
                case "HtmlBlock":
                    List<HtmlBlockMainModel> htmlBlks = this.ConvertToCustomHtmlBlks();
                    File.WriteAllText(this.QueryFilePath + "\\" + (this.TenantList.SelectedItem as TenantModel).TenantName + ".list", JsonConvert.SerializeObject(htmlBlks));
                    break;
                default:
                    break;
            }

            MessageBox.Show("Download completed!");
            Process.Start("explorer.exe", this.QueryFilePath);
        }

        private List<DataListMainModel> ConvertToCustomDataList()
        {
            List<DataList> lists = null;
            List<DataListMainModel> listsMain = new List<DataListMainModel>();
            DataListMainModel list1 = null;

            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());

            lists = this.SourceLists.Where(w => w.TenantID == tenantID).ToList();
            List<TenantModuleModel> modules = this.Cache.Get<List<TenantModuleModel>>("TenantModules");
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
                    Items = this.ConvertToCustomDataListItems(list.ContentID, list.TenantID,this.SourceListItems,attributes),
                    DataListAttributes=this.ConverttoAttributes(list.DataListAttributes, list.ContentID, list.TenantID, list.ID),
                    TenantID = list.TenantID,
                    Name=list.DataListsName,
                    TenantModuleID=list.TenantModuleID                   
                   
                };

                listsMain.Add(list1);
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

            lists = this.SourceHtmlListItems.Where(w => w.TenantId == tenantID).ToList();
            List<TenantModuleModel> modules = this.Cache.Get<List<TenantModuleModel>>("TenantModules");

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

        private List<MenuListModel> ConvertToCustomMenus()
        {
            List<MenuListModel> lists = null;
            List<MenuListModel> listsMain = new List<MenuListModel>();
            MenuListModel list1 = null;

            Guid tenantID = new Guid(this.TenantList.SelectedValue.ToString());

            lists = this.SourceMenus.Where(w => w.TenantId == tenantID).ToList();
            List<TenantModuleModel> modules = this.Cache.Get<List<TenantModuleModel>>("TenantModules");
            foreach (MenuListModel list in lists)
            {
                list1 = new MenuListModel()
                {
                    ID = list.ID,
                    TenantModuleID = list.TenantModuleID,
                    IsActive = list.IsActive,
                    Name = list.Name,
                    SecurityRightItemID = list.SecurityRightItemID,
                    DisplaySize = list.DisplaySize,
                    OperatorID = list.OperatorID,
                    Level = list.Level,
                    TenantId = list.TenantId,
                    Children = this.ConvertToCustomMenuListItems(list.ID),
                };

                listsMain.Add(list1);
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
                    SecurityRightItemID = e.SecurityRightItemID,
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
                item.Add(itemList);
            }

            return item;
        }
        private List<CodeItemModel> ConvertToCustomDataListItems(string contentID, Guid tenantID, List<CodeListModel> listdatalistitems, List<DataListAttribute> datalistattribute)
        {
            List<CodeListModel> listItems = null;
            List<CodeItemModel> items = new List<CodeItemModel>();
            CodeItemModel item = null;
            listItems = this.SourceListItems.Where(w => w.ContentID == contentID && w.TenantID == tenantID).ToList();
            

       
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
                    Attributes = this.GetCustomItemAttributes(e, listdatalistitems, datalistattribute, resultitems, e.ContentID)
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
            DatalistComparer compare = new DatalistComparer();
            compare.ShowDialog();
            this.Close();
        }

        private void ControlName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadModules();
        }
        
        private void LoadControls()
        { 
            List<string> controlNames = new List<string>(new string[] { "Datalist", "HtmlBlock", "Images", "Menus" });
            for (int i = 0; i <= controlNames.Count - 1; i++)
            {
                this.ControlName.Items.Add(controlNames[i]);
            }

        }


        private List<ItemDataListItemAttributeVal> GetCustomItemAttributes(CodeListModel toExpand, List<CodeListModel> items, List<DataListAttribute> listAttributes, List<ItemDataListItemAttributeVal> itemAttribues, string ContentID)
        {
            List<ItemDataListItemAttributeVal> itemAttrValues = new List<ItemDataListItemAttributeVal>();
            itemAttrValues = itemAttribues.FindAll(x => x.DataListItemID == toExpand.ID).ToList();
            if (itemAttrValues.Count > 0)
            {
                itemAttrValues.ForEach(y => y.DataListAttributeValue = items.Find(c => c.ID == y.DataListValueID).Code);
                itemAttrValues.ForEach(y => {
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
    }
}
