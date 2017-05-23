//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using DatalistSyncUtil.Views;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DataListService = DatalistSyncUtil.DataListService;

namespace DatalistSyncUtil
{
    public partial class DatalistComparer : Form
    {
        private string rolesContentID = "Core.SecurityRoles";
        private string functionsContentID = "Core.SecurityFunctions";
        private string rightsContentID = "Core.SecurityRights";
         private List<CodeLinkTable> sourceListlink = new List<CodeLinkTable>();
        private List<CodeLinkTable> targetListlink = new List<CodeLinkTable>();

        public DatalistComparer()
        {
            this.InitializeComponent();
            this.TargetConnectionString = ConfigurationManager.ConnectionStrings["TargetDataList"];
            this.SourceConnectionString = ConfigurationManager.ConnectionStrings["SourceDataList"];
            this.LoadHelper = new TenantHelper(this.TargetConnectionString);
            this.SourceLoadHelper = new SourceTenantHelper(this.SourceConnectionString);
            this.txtTargetConnection.Text = this.GetDataSourceName(this.TargetConnectionString);
            this.txtSourceConnection.Text = this.GetDataSourceName(this.SourceConnectionString);
            this.LoadTenant();
            this.LoadModules();
            this.LoadControls();
            this.LoadSourceControls();
            this.LoadSourceTenant();
            this.LoadSourceModules();
            this.Cachemanager = new RedisCacheManager();
        }
  
        public ICacheManager Cachemanager { get; set; }

        public List<DataListMainModel> SourceList { get; set; }

        public List<CodeLinkTable> SourceLink { get; set; }

        public List<MenuListModel> SourceMenus { get; set; }

        public List<MenuListModel> SourceMenuList { get; set; }

        public List<MenuItemModel> SourceMenuItems { get; set; }

        public List<HtmlBlockMainModel> SourceHtmlList { get; set; }

        public List<ImagesMainModel> SourceImagesList { get; set; }

        public List<DataListMainModel> TargetList { get; set; }

        public List<MenuListModel> TargetMenuList { get; set; }

        public List<HtmlBlockMainModel> TargetHtmlList { get; set; }

        public List<ImagesMainModel> TargetImagesList { get; set; }

        public TenantHelper LoadHelper { get; set; }

        public SourceTenantHelper SourceLoadHelper { get; set; }

        public List<AppSettingsModel> SourceAppSetting { get; set; }

        public List<AppSettingsModel> TargetAppSetting { get; set; }

        public ConnectionStringSettings TargetConnectionString { get; set; }

        public ConnectionStringSettings SourceConnectionString { get; set; }

        public List<DataListItemAttributeModel> SourceDataListItemAttributes { get; set; }

        public List<DataListItemAttributeModel> TargetDataListItemAttributes { get; set; }
            
        private void BtnSourceFile_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openDatalistFile.ShowDialog();

            if (result == DialogResult.OK)
            {
                string file = this.openDatalistFile.FileName;
                this.txtSourceConnection.Text = file;
                string filename = Path.GetFileNameWithoutExtension(file);
                try
                {
                    List<TenantModuleModel> targetModules = this.moduleList.DataSource as List<TenantModuleModel>;
                    this.SourceList = JsonConvert.DeserializeObject<List<DataListMainModel>>(File.ReadAllText(file));
                    List<DataListMainModel> sourceDataList = this.SourceList.Where(w => targetModules.Select(s => s.ModuleName).Contains(w.ModuleName)).ToList();
                }
                catch (IOException)
                {
                }

                switch (filename)
                {
                    case "AppSetting":
                        this.SourceAppSetting = JsonConvert.DeserializeObject<List<AppSettingsModel>>(File.ReadAllText(file));
                        this.LoadAppSettingTreeView(this.sourceTreeList, this.SourceAppSetting.OrderBy(o => o.AppSettingKey).ToList());
                        break;

                    case "Datalist":
                        this.LoadTreeView(this.sourceTreeList, this.SourceList.OrderBy(o => o.ContentID).ToList());
                        break;

                    case "Menus":
                        this.SourceMenus = JsonConvert.DeserializeObject<List<MenuListModel>>(File.ReadAllText(file));
                        this.LoadMenuTreeView(this.sourceTreeList, this.SourceMenus.OrderBy(o => o.Name).ToList());
                        break;
                    case "HtmlBlock":
                        this.SourceHtmlList = JsonConvert.DeserializeObject<List<HtmlBlockMainModel>>(File.ReadAllText(file));
                        this.LoadHtmlTreeView(this.sourceTreeList, this.SourceHtmlList.OrderBy(o => o.ContentId).ToList());
                        break;
                    case "Security":
                        this.LoadTreeViewforSecurity(this.sourceTreeList, this.SourceList.OrderBy(x => x.ContentID).ToList());
                        break;
                    case "Image":
                        this.SourceImagesList = JsonConvert.DeserializeObject<List<ImagesMainModel>>(File.ReadAllText(file));
                        this.LoadImagesTreeView(this.sourceTreeList, this.SourceImagesList.OrderBy(o => o.ContentId).ToList());
                        break;
                    default:
                        break;
                }
            }
        }

        private void LoadAppSettingTreeView(TreeView treeView, List<AppSettingsModel> lists)
        {
            TreeNode listNode = null;
            List<TreeNode> appNodes = new List<TreeNode>();       

            try
            {
                treeView.Nodes.Clear();
                foreach (AppSettingsModel list in lists)
                {
                    listNode = new TreeNode();
                    listNode = new TreeNode(list.AppSettingKey, appNodes.ToArray());
                    listNode = new TreeNode(list.Value, appNodes.ToArray());
                    treeView.Nodes.Add(listNode);                  
                }
            }
            finally
            {
                listNode = null;
                appNodes = null;               
            }
        }

        private void LoadTreeView(TreeView treeView, List<DataListMainModel> lists)
        {
            TreeNode listNode = null;
            TreeNode itemNode = null;
            List<TreeNode> itemNodes = null;
            TreeNode langNode = null;
            List<TreeNode> langNodes = null;
            List<TreeNode> attributenodes = null;
            TreeNode attributenode = null;
            string languageSeparator = " - ";

            try
            {
                treeView.Nodes.Clear();

                foreach (DataListMainModel list in lists)
                {
                    itemNodes = new List<TreeNode>();
                    list.Items.ForEach(i =>
                    {
                        langNodes = new List<TreeNode>();
                        i.LanguageList.ForEach(l =>
                        {
                            langNode = new TreeNode(l.LocaleID + languageSeparator + l.Description);
                            langNodes.Add(langNode);
                        });
                        itemNode = new TreeNode(i.Code, langNodes.ToArray());
                        itemNodes.Add(itemNode);
                    });
                    listNode = new TreeNode(list.ContentID, itemNodes.ToArray());

                    attributenodes = new List<TreeNode>();
                    if (list.DataListAttributes != null)
                    {
                        list.DataListAttributes.ForEach(f =>
                        {
                            attributenode = new TreeNode(f.ParentContentId + "." + f.Code);
                            attributenodes.Add(attributenode);
                        });

                        listNode = new TreeNode(list.ContentID, attributenodes.ToArray());
                    }

                    treeView.Nodes.Add(listNode);
                }
            }
            finally
            {
                listNode = null;
                itemNode = null;
                itemNodes = null;
                langNode = null;
                langNodes = null;
            }
        }

        private void LoadMenuTreeView(TreeView treeView, List<MenuListModel> itemlist)
        {
            TreeNode listNode = null;
            List<TreeNode> menuNodes = null;
            TreeNode menuNode = null;
            List<MenuItemModel> childnode = null;
            try
            {
                treeView.Nodes.Clear();
                foreach (MenuListModel list in itemlist)
                {
                    menuNodes = new List<TreeNode>();
                    childnode = list.Children.ToList();
                    childnode.ForEach(f =>
                    {
                        menuNode = new TreeNode(f.Name);
                        menuNodes.Add(menuNode);
                    });
                    listNode = new TreeNode(list.Name, menuNodes.ToArray());
                    treeView.Nodes.Add(listNode);
                }
            }
            finally
            {
                menuNodes = null;
                menuNode = null;
            }
        }

        private void LoadHtmlTreeView(TreeView treeView, List<HtmlBlockMainModel> lists)
        {
            TreeNode listNode = null;

            TreeNode langNode = null;
            List<TreeNode> langNodes = null;
            string languageSeparator = " - ";

            try
            {
                treeView.Nodes.Clear();

                foreach (HtmlBlockMainModel list in lists)
                {
                    langNodes = new List<TreeNode>();
                    list.HtmlBlockLanguages.ForEach(l =>
                    {
                        langNode = new TreeNode(l.LocaleId + languageSeparator + l.Html);
                        langNodes.Add(langNode);
                    });
                                        
                    listNode = new TreeNode(list.ContentId, langNodes.ToArray());

                    treeView.Nodes.Add(listNode);
                }
            }
            finally
            {
                listNode = null;
                langNode = null;
                langNodes = null;
            }
        }

        private void LoadImagesTreeView(TreeView treeView, List<ImagesMainModel> lists)
        {
            TreeNode listNode = null;

            TreeNode langNode = null;
            List<TreeNode> langNodes = null;
            string languageSeparator = " - ";
            string width = " , Width : ";
            string height = " , Height : ";

            try
            {
                treeView.Nodes.Clear();

                foreach (ImagesMainModel list in lists)
                {
                    langNodes = new List<TreeNode>();
                    list.ImageLanguages.ForEach(l =>
                    {
                        langNode = new TreeNode(l.LocaleId + languageSeparator + l.Source + languageSeparator + l.ToolTip + width + l.Width + height + l.Height);
                        langNodes.Add(langNode);
                    });

                    listNode = new TreeNode(list.ContentId, langNodes.ToArray());

                    treeView.Nodes.Add(listNode);
                }
            }
            finally
            {
                listNode = null;
                langNode = null;
                langNodes = null;
            }
        }

        private void BtnLoadTarget_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Guid tenantModuleId = (this.moduleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string caseSwitch = this.TargetControlNames.SelectedItem.ToString();
            switch (caseSwitch)
            {
                case "AppSetting":
                    this.TargetAppSetting = this.LoadTargetAppSetting();
                    List<AppSettingsModel> filteredAppSetting = null;
                    if (tenantModuleId == Guid.Empty)
                    {
                        filteredAppSetting = this.TargetAppSetting;
                    }
                    else
                    {
                        filteredAppSetting = this.TargetAppSetting.Where(w => w.TenantModuleID == tenantModuleId).ToList();
                    }

                    this.LoadAppSettingTreeView(this.targetTreeList, filteredAppSetting.OrderBy(o => o.AppSettingKey).ToList());
                    break;
                case "Datalist":
                    this.TargetList = this.LoadTargetDatalist();
                    List<DataListMainModel> filteredDataList = null;
                    if (tenantModuleId == Guid.Empty)
                    {
                        filteredDataList = this.TargetList;
                    }
                    else
                    {
                        filteredDataList = this.TargetList.Where(w => w.TenantModuleID == tenantModuleId).ToList();
                    }

                    this.LoadTreeView(this.targetTreeList, filteredDataList.OrderBy(o => o.ContentID).ToList());
                    break;
                case "HtmlBlock":
                    this.TargetHtmlList = this.LoadTargetHTMLlist();
                    List<HtmlBlockMainModel> filteredHtmlList = null;

                    if (tenantModuleId == Guid.Empty)
                    {
                        filteredHtmlList = this.TargetHtmlList;
                    }
                    else
                    {
                        filteredHtmlList = this.TargetHtmlList.Where(w => w.TenantModuleId == tenantModuleId).ToList();
                    }

                    this.LoadHtmlTreeView(this.targetTreeList, filteredHtmlList.OrderBy(o => o.ContentId).ToList());
                    break;
                case "Menus":
                    this.TargetMenuList = this.LoadTargetMenus();
                    List<MenuListModel> filteredDataList1 = null;
                    filteredDataList1 = this.TargetMenuList;
                    this.LoadMenuTreeView(this.targetTreeList, filteredDataList1.OrderBy(w => w.Name).ToList());
                    break;
                case "Security":
                    List<DataListMainModel> filteredSecurityTargetDataList = null;
                    this.TargetList = filteredSecurityTargetDataList = this.LoadTargetSecurityDatalist(tenantModuleId);
                    this.LoadTreeViewforSecurity(this.targetTreeList, filteredSecurityTargetDataList);
                    break;
                case "Image":
                    this.TargetImagesList = this.LoadTargetImagesList();
                    List<ImagesMainModel> filteredImageList = null;

                    if (tenantModuleId == Guid.Empty)
                    {
                        filteredImageList = this.TargetImagesList;
                    }
                    else
                    {
                        filteredImageList = this.TargetImagesList.Where(w => w.TenantModuleId == tenantModuleId).ToList();
                    }

                    this.LoadImagesTreeView(this.targetTreeList, filteredImageList.OrderBy(o => o.ContentId).ToList());
                    break;
                default:
                    break;
            }

            Cursor.Current = Cursors.Default;
        }

         private List<AppSettingsModel> LoadTargetAppSetting()
        {
            List<AppSettingsModel> lists = null;
            List<AppSettingsModel> listsMain = new List<AppSettingsModel>();
            AppSettingsModel list1 = null;
                       
            Guid tenantID = new Guid(this.tenantList.SelectedValue.ToString());
            lists = this.LoadHelper.GetAppSetting().Where(w => w.TenantID == tenantID).ToList();
                                  
            foreach (AppSettingsModel list in lists)
            {
                list1 = new AppSettingsModel()
                {
                    TenantModuleAppSettingId = list.TenantModuleAppSettingId,
                    ApplicationId = list.ApplicationId,
                    AppSettingKey = list.AppSettingKey,
                    Value = list.Value,
                    TargetValue = null,
                    SettingTypeItemKey = list.SettingTypeItemKey,
                    Description = list.Description,
                    ModuleName = list.ModuleName,
                    TenantID = list.TenantID,
                    OperatorID = list.OperatorID,
                    TenantModuleID = list.TenantModuleID,
                    LastModifiedTimeStamp = list.LastModifiedTimeStamp == null ? DateTime.UtcNow : list.LastModifiedTimeStamp,
                    IsActive = list.IsActive
                };

                listsMain.Add(list1);
            }

            return listsMain;
        }

        private List<MenuListModel> LoadTargetMenus()
        {
            List<MenuListModel> lists = null;
            List<MenuListModel> listsMain = new List<MenuListModel>();
            MenuListModel list1 = null;
            List<MenuItemModel> childMenuItems = new List<MenuItemModel>();

            Guid tenantID = new Guid(this.tenantList.SelectedValue.ToString());
            lists = this.LoadHelper.GetMenu().Where(w => w.TenantId == tenantID).ToList();
            lists.ForEach(x =>
            {
                childMenuItems.AddRange(x.Children.ToList());
            });

            this.SourceMenuItems = childMenuItems;
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

        private List<DataListMainModel> LoadTargetDatalist()
        {
            List<DataList> lists = null;
            List<CodeListModel> listItems = null;
            List<DataListMainModel> listsMain = new List<DataListMainModel>();
            DataListMainModel list1 = null;
            List<ItemDataListItemAttributeVal> targetDataListItemAttributes = new List<ItemDataListItemAttributeVal>();

            Guid tenantID = new Guid(this.tenantList.SelectedValue.ToString());
            targetDataListItemAttributes = this.LoadHelper.GetItemAttributeList();
            lists = this.LoadHelper.GetDataList().Where(w => w.TenantID == tenantID).ToList();
            lists = this.LoadHelper.GetAttributesList().Where(w => w.TenantID == tenantID).ToList();
            listItems = this.LoadHelper.GetDataListItems();
            List<DataListAttribute> attributes = new List<DataListAttribute>();
            lists.ForEach(x => { attributes.AddRange(x.DataListAttributes); });

            this.targetListlink = this.LoadHelper.GetDataListLinks("TargetDataListLinks");

            foreach (DataList list in lists)
            {
                list1 = new DataListMainModel()
                {
                    ContentID = list.ContentID,
                    Description = list.Description,
                    IsActive = list.IsActive,
                    IsEditable = list.IsEditable,
                    ReleaseStatus = list.ReleaseStatus,
                    Items = this.ConvertToCustomDataListItems(list.ContentID, list.TenantID, listItems, attributes, targetDataListItemAttributes, this.targetListlink),
                    TenantID = list.TenantID,
                    TenantModuleID = list.TenantModuleID,
                    ID = list.ID,
                    ModuleName = list.ModuleName,
                    DataListAttributes = this.ConverttoAttributes(list.DataListAttributes, list.ContentID, list.TenantID, list.ID),
                    Name = list.DataListsName
                };

                listsMain.Add(list1);
            }

            return listsMain;
        }

        private List<HtmlBlockMainModel> LoadTargetHTMLlist()
        {
            List<HtmlBlockModel> lists = null;
            List<HtmlBlockLanguagesModel> listItems = null;
            List<HtmlBlockMainModel> listsMain = new List<HtmlBlockMainModel>();
            HtmlBlockMainModel list1 = null;

            Guid tenantID = new Guid(this.tenantList.SelectedValue.ToString());

            lists = this.LoadHelper.GetHTMLList().Where(w => w.TenantId == tenantID).ToList();
            listItems = this.LoadHelper.GetHtmlListLangs();

            foreach (HtmlBlockModel list in lists)
            {
                list1 = new HtmlBlockMainModel()
                {
                    ContentId = list.ContentId,
                    Description = list.Description,
                    ID = list.ID,
                    HtmlBlockLanguages = this.ConvertToCustomHtmlLang(list.ContentId, list.ID, listItems),
                    IsActive = list.IsActive,
                    LastModifiedTS = list.LastModifiedTS,
                    OperatorId = list.OperatorId,
                    TenantModuleId = list.TenantModuleId
                };

                listsMain.Add(list1);
            }

            return listsMain;
        }

        private List<HtmlBlockLanguage> ConvertToCustomHtmlLang(string contentID, Guid iD, List<HtmlBlockLanguagesModel> listItems)
        {
            List<HtmlBlockLanguage> items = new List<HtmlBlockLanguage>();
            HtmlBlockLanguage item = null;
            listItems = listItems.Where(w => w.ID == iD).ToList();

            listItems.ForEach(e =>
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

        private List<ImagesMainModel> LoadTargetImagesList()
        {
            List<ImageListModel> lists = null;
            List<ImageLanguagesModel> listItems = null;
            List<ImagesMainModel> listsMain = new List<ImagesMainModel>();
            ImagesMainModel list1 = null;

            Guid tenantID = new Guid(this.tenantList.SelectedValue.ToString());

            lists = this.LoadHelper.GetImagesList().Where(w => w.TenantId == tenantID).ToList();
            listItems = this.LoadHelper.GetImageLangs();

            foreach (ImageListModel list in lists)
            {
                list1 = new ImagesMainModel()
                {
                    ContentId = list.ContentId,
                    Description = list.Description,
                    ImageId = list.ID,
                    ImageLanguages = this.ConvertToCustomImagesLang(list.ContentId, list.ID, listItems),
                    IsActive = list.IsActive,
                    LastModifiedTS = list.LastModifiedTS,
                    OperatorId = list.OperatorId,
                    TenantModuleId = list.TenantModuleId
                };

                listsMain.Add(list1);
            }

            return listsMain;
        }

        private List<ImageLanguage> ConvertToCustomImagesLang(string contentID, Guid iD, List<ImageLanguagesModel> listItems)
        {
            List<ImageLanguage> items = new List<ImageLanguage>();
            ImageLanguage item = null;
            listItems = listItems.Where(w => w.ID == iD).ToList();

            listItems.ForEach(e =>
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

        private List<MenuListModel> LoadSourceMenus()
        {
            List<MenuListModel> lists = null;
            List<MenuListModel> listsMain = new List<MenuListModel>();
            MenuListModel list1 = null;
            List<MenuItemModel> childMenuItems = new List<MenuItemModel>();

            Guid tenantID = new Guid(this.sourceTenantList.SelectedValue.ToString());
            lists = this.SourceLoadHelper.GetMenu().ToList();
            lists.ForEach(x =>
            {
                childMenuItems.AddRange(x.Children.ToList());
            });

            this.SourceMenuItems = childMenuItems;
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

        private List<DataListMainModel> LoadSourceDatalist()
        {
            List<DataList> lists = null;
            List<CodeListModel> listItems = null;
            List<DataListMainModel> listsMain = new List<DataListMainModel>();
            List<ItemDataListItemAttributeVal> sourceDataListItemAttributes = new List<ItemDataListItemAttributeVal>();
            List<DataListAttribute> attributes = new List<DataListAttribute>();

            DataListMainModel list1 = null;
            Guid tenantID = new Guid(this.sourceTenantList.SelectedValue.ToString());
            lists = this.SourceLoadHelper.GetDataList().Where(w => w.TenantID == tenantID).ToList();
            lists = this.SourceLoadHelper.GetAttributesList().Where(w => w.TenantID == tenantID).ToList();
            listItems = this.SourceLoadHelper.GetDataListItems();
            sourceDataListItemAttributes = this.SourceLoadHelper.GetItemAttributeList();
            lists.ForEach(x => { attributes.AddRange(x.DataListAttributes); });
            this.sourceListlink = this.SourceLoadHelper.GetDataListLinks("SourceDataListLinks");
            foreach (DataList list in lists)
            {
                list1 = new DataListMainModel()
                {
                    ContentID = list.ContentID,
                    Description = list.Description,
                    IsActive = list.IsActive,
                    IsEditable = list.IsEditable,
                    ReleaseStatus = list.ReleaseStatus,
                    DataListAttributes = this.ConverttoAttributes(list.DataListAttributes, list.ContentID, list.TenantID, list.ID),
                    Items = this.ConvertToCustomDataListItems(list.ContentID, list.TenantID, listItems, attributes, sourceDataListItemAttributes, this.sourceListlink),
                    TenantID = list.TenantID,
                    TenantModuleID = list.TenantModuleID,
                    ID = list.ID,
                    Name = list.DataListsName,
                    ModuleName = list.ModuleName
                };

                listsMain.Add(list1);
            }

            return listsMain;
        }

        private List<HtmlBlockMainModel> LoadSourceHTMLlist()
        {
            List<HtmlBlockModel> lists = null;
            List<HtmlBlockLanguagesModel> listItems = null;
            List<HtmlBlockMainModel> listsMain = new List<HtmlBlockMainModel>();
            HtmlBlockMainModel list1 = null;

            Guid tenantID = new Guid(this.sourceTenantList.SelectedValue.ToString());

            lists = this.SourceLoadHelper.GetHTMLList().Where(w => w.TenantId == tenantID).ToList();
            listItems = this.SourceLoadHelper.GetHtmlListLangs();

            foreach (HtmlBlockModel list in lists)
            {
                list1 = new HtmlBlockMainModel()
                {
                    ContentId = list.ContentId,
                    Description = list.Description,
                    ID = list.ID,
                    HtmlBlockLanguages = this.GetHtmlLanguageListCustom(list.ID, listItems),
                    IsActive = list.IsActive,
                    LastModifiedTS = list.LastModifiedTS,
                    OperatorId = list.OperatorId,
                    TenantModuleId = list.TenantModuleId
                };

                listsMain.Add(list1);
            }

            return listsMain;
        }

        private List<HtmlBlockLanguage> GetHtmlLanguageListCustom(Guid id, List<HtmlBlockLanguagesModel> listItems)
        {
            List<HtmlBlockLanguage> languages = new List<HtmlBlockLanguage>();
            HtmlBlockLanguage language = null;

            listItems = listItems.Where(w => w.ID == id).ToList();

            listItems.ForEach(e =>
            {
                language = new HtmlBlockLanguage()
                {
                    HtmlBlockId = e.ID,
                    LocaleId = e.LocaleId,
                    Html = e.Html,
                    IsActive = e.IsActive,
                    OperatorId = e.OperatorId,
                    LastModifiedTS = e.LastModifiedTS
                };
                languages.Add(language);
            });

            return languages;
        }

        private List<ImagesMainModel> LoadSourceImageslist()
        {
            List<ImageListModel> lists = null;
            List<ImageLanguagesModel> listItems = null;
            List<ImagesMainModel> listsMain = new List<ImagesMainModel>();
            ImagesMainModel list1 = null;

            Guid tenantID = new Guid(this.sourceTenantList.SelectedValue.ToString());

            lists = this.SourceLoadHelper.GetImagesList().Where(w => w.TenantId == tenantID).ToList();
            listItems = this.SourceLoadHelper.GetImageLangs();

            foreach (ImageListModel list in lists)
            {
                list1 = new ImagesMainModel()
                {
                    ContentId = list.ContentId,
                    Description = list.Description,
                    ImageId = list.ID,
                    ImageLanguages = this.GetImagesLanguageListCustom(list.ID, listItems),
                    IsActive = list.IsActive,
                    LastModifiedTS = list.LastModifiedTS,
                    OperatorId = list.OperatorId,
                    TenantModuleId = list.TenantModuleId
                };

                listsMain.Add(list1);
            }

            return listsMain;
        }

        private List<ImageLanguage> GetImagesLanguageListCustom(Guid id, List<ImageLanguagesModel> listItems)
        {
            List<ImageLanguage> languages = new List<ImageLanguage>();
            ImageLanguage language = null;

            listItems = listItems.Where(w => w.ID == id).ToList();

            listItems.ForEach(e =>
            {
                language = new ImageLanguage()
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
                languages.Add(language);
            });

            return languages;
        }

        private List<CodeItemModel> ConvertToCustomDataListItems(string contentID, Guid tenantID, List<CodeListModel> listItems, List<DataListAttribute> listattributes, List<ItemDataListItemAttributeVal> itemattributes, List<CodeLinkTable> listlink)
        {
            List<CodeItemModel> items = new List<CodeItemModel>();
            List<CodeListModel> itemList = new List<CodeListModel>();
            CodeItemModel item = null;
            itemList = listItems.Where(w => w.ContentID == contentID && w.TenantID == tenantID).ToList();

            itemList.ForEach(e =>
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
                    DataListLink = this.GetcoustmItemLink(e.ContentID, e.ID, listlink, e.OrderIndex, e.LanguageList, listItems, e.TenantID),
                    Attributes = this.GetCustomItemAttributes(e, listItems, listattributes, itemattributes, e.ContentID)
                };
                items.Add(item);
            });

            return items;
        }

        private List<DataListItemLink> GetcoustmItemLink(string contentID, Guid id, List<CodeLinkTable> link, int? orderIndex, List<Languages> language, List<CodeListModel> listItems, Guid tenantID)
        {
            List<DataListItemLink> listlink = new List<DataListItemLink>();
            DataListItemLink itemlink = null;

            List<CodeListModel> listItemschild = new List<CodeListModel>();
            link = link.Where(w => w.ParentID == id).ToList();

            link.ForEach(e =>
            {
                listItemschild = listItems.Where(w => w.ID == e.ChildID).ToList();
                if (listItemschild.Count() > 0)
                {
                    itemlink = new DataListItemLink()
                    {
                        ParentDataList = contentID,
                        ChildDataList = listItemschild[0].ContentID,
                        ParentCode = Convert.ToString(orderIndex) + "-" + language[0].Description,
                        ParentID = e.ParentID,
                        ChildID = e.ChildID,
                        IsActive = e.IsActive,
                        Description = listItemschild[0].OrderIndex + "-" + listItemschild[0].LanguageList[0].Description,
                        TenantID = tenantID
                    };
                }

                listlink.Add(itemlink);
            });

            return listlink;
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
                    ItemID = e.CodeID,
                    IsActive = e.IsActive
                };
                languages.Add(language);
            });

            return languages;
        }

        private void LoadTenant()
        {
            this.tenantList.DataSource = this.LoadHelper.GetTenants().ToList();
            this.tenantList.DisplayMember = "TenantName";
        }

        private void LoadModules()
        {
            Guid tenantID = (this.tenantList.SelectedItem as TenantModel).TenantID;
            List<TenantModuleModel> modules = this.LoadHelper.LoadModules();
            modules.Insert(
                    0,
                    new TenantModuleModel()
                    {
                        ModuleName = "---All Modules---",
                        TenantModuleId = Guid.Empty,
                        TenantId = tenantID
                    });
            this.moduleList.DataSource = modules.Where(w => w.TenantId == tenantID).GroupBy(i => i.ModuleName)
                  .Select(group =>
                        new
                        {
                            Key = group.Key,
                            Items = group.OrderByDescending(x => x.ModuleName)
                        })
                  .Select(g => g.Items.First()).OrderBy(o => o.ModuleName).ToList();
            this.moduleList.DisplayMember = "ModuleName";
            this.moduleList.SelectAll();
        }

        private void LoadSourceTenant()
        {
            this.sourceTenantList.DataSource = this.SourceLoadHelper.GetTenants().ToList();
            this.sourceTenantList.DisplayMember = "TenantName";
        }

        private void LoadSourceModules()
        {
            Guid tenantID = (this.sourceTenantList.SelectedItem as TenantModel).TenantID;
            List<TenantModuleModel> modules = this.SourceLoadHelper.LoadModules();
            modules.Insert(
                0,
                new TenantModuleModel()
                {
                    ModuleName = "---All Modules---",
                    TenantModuleId = Guid.Empty,
                    TenantId = tenantID
                });
            this.sourceModuleList.DataSource = modules.Where(w => w.TenantId == tenantID).GroupBy(i => i.ModuleName)
                  .Select(group =>
                        new
                        {
                            Key = group.Key,
                            Items = group.OrderByDescending(x => x.ModuleName)
                        })
                  .Select(g => g.Items.First()).OrderBy(o => o.ModuleName).ToList();
            this.sourceModuleList.DisplayMember = "ModuleName";
            this.sourceModuleList.SelectAll();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DatalistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.SourceList != null)
            {
                DatalistDiff diffPage = new DatalistDiff(new Guid(this.tenantList.SelectedValue.ToString()), "DATALIST", this.SourceList, this.TargetList);
                diffPage.ShowDialog();
            }

            Cursor.Current = Cursors.Default;
        }

        private void DatalistItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.SourceList != null)
            {
                DatalistDiff diffPage = new DatalistDiff(new Guid(this.tenantList.SelectedValue.ToString()), "ITEMS", this.SourceList, this.TargetList);
                diffPage.ShowDialog();
            }

            Cursor.Current = Cursors.Default;
        }

        private void TenantList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadModules();
        }

        private void SourceTenantList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadSourceModules();
        }

        private void BtnSourceLoad_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Guid tenantModuleId = (this.sourceModuleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string caseSwitch = this.SourceControlNames.SelectedItem.ToString();
            switch (caseSwitch)
            {
                case "AppSetting":
                    this.SourceAppSetting = this.LoadSourceAppSetting();
                    List<AppSettingsModel> filteredAppSetting = null;
                    if (tenantModuleId == Guid.Empty)
                    {
                        filteredAppSetting = this.SourceAppSetting;
                    }
                    else
                    {
                        filteredAppSetting = this.SourceAppSetting.Where(w => w.TenantModuleID == tenantModuleId).ToList();
                    }

                    this.LoadAppSettingTreeView(this.sourceTreeList, filteredAppSetting.OrderBy(o => o.AppSettingKey).ToList());
                    break;
                case "Datalist":
                    this.SourceList = this.LoadSourceDatalist();
                    List<DataListMainModel> filteredDataList = null;

                    if (tenantModuleId == Guid.Empty)
                    {
                        filteredDataList = this.SourceList;
                    }
                    else
                    {
                        filteredDataList = this.SourceList.Where(w => w.TenantModuleID == tenantModuleId).ToList();
                    }

                    this.LoadTreeView(this.sourceTreeList, filteredDataList.OrderBy(o => o.ContentID).ToList());
                    break;
                case "HtmlBlock":
                    this.SourceHtmlList = this.LoadSourceHTMLlist();
                    List<HtmlBlockMainModel> filteredHtmlList = null;

                    if (tenantModuleId == Guid.Empty)
                    {
                        filteredHtmlList = this.SourceHtmlList;
                    }
                    else
                    {
                        filteredHtmlList = this.SourceHtmlList.Where(w => w.TenantModuleId == tenantModuleId).ToList();
                    }

                    this.LoadHtmlTreeView(this.sourceTreeList, filteredHtmlList.OrderBy(o => o.ContentId).ToList());
                    break;
                case "Menus":
                    this.SourceMenuList = this.LoadSourceMenus();
                    List<MenuListModel> filteredDataList1 = null;
                    filteredDataList1 = this.SourceMenuList;
                    this.LoadMenuTreeView(this.sourceTreeList, filteredDataList1.OrderBy(w => w.Name).ToList());
                    break;
                case "Security":
                    List<DataListMainModel> filteredSecuritySorceDataList = null;
                    this.SourceList = filteredSecuritySorceDataList = this.LoadSourceSecurityDatalist(tenantModuleId);
                    this.LoadTreeViewforSecurity(this.sourceTreeList, filteredSecuritySorceDataList);
                    break;
                case "Image":
                    this.SourceImagesList = this.LoadSourceImageslist();
                    List<ImagesMainModel> filteredImageList = null;

                    if (tenantModuleId == Guid.Empty)
                    {
                        filteredImageList = this.SourceImagesList;
                    }
                    else
                    {
                        filteredImageList = this.SourceImagesList.Where(w => w.TenantModuleId == tenantModuleId).ToList();
                    }

                    this.LoadImagesTreeView(this.sourceTreeList, filteredImageList.OrderBy(o => o.ContentId).ToList());
                    break;
                default:
                    break;
            }

            Cursor.Current = Cursors.Default;
        }

        private List<AppSettingsModel> LoadSourceAppSetting()
        {
            List<AppSettingsModel> lists = null;
            List<AppSettingsModel> listsMain = new List<AppSettingsModel>();
            AppSettingsModel list1 = null;

            Guid tenantID = new Guid(this.sourceTenantList.SelectedValue.ToString());
            lists = this.SourceLoadHelper.GetAppSetting().Where(w => w.TenantID == tenantID).ToList();

            foreach (AppSettingsModel list in lists)
            {
                list1 = new AppSettingsModel()
                {
                    TenantModuleAppSettingId = list.TenantModuleAppSettingId,
                    ApplicationId = list.ApplicationId,
                    AppSettingKey = list.AppSettingKey,
                    TargetValue = null,
                    Value = list.Value,
                    ModuleName = list.ModuleName,
                    TenantID = list.TenantID,
                    SettingTypeItemKey = list.SettingTypeItemKey,
                    Description = list.Description,
                    TenantModuleID = list.TenantModuleID,
                    OperatorID = list.OperatorID,
                    LastModifiedTimeStamp = list.LastModifiedTimeStamp == null ? DateTime.UtcNow : list.LastModifiedTimeStamp,
                    IsActive = list.IsActive
                };

                listsMain.Add(list1);
            }

            return listsMain;
        }

        private void MenusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.SourceMenuList != null)
            {
                MenuListDiff menudiffPage = new MenuListDiff(new Guid(this.tenantList.SelectedValue.ToString()), "MENUS", this.SourceMenuList, this.TargetMenuList);
                menudiffPage.ShowDialog();
                this.Close();
            }
        }

        private void LoadSourceControls()
        {
            List<string> controlNames = new List<string>(new string[] { "AppSetting", "Datalist", "HtmlBlock", "Image", "Menus", "Security" });
            for (int i = 0; i <= controlNames.Count - 1; i++)
            {
                this.SourceControlNames.Items.Add(controlNames[i]);
            }

            this.SourceControlNames.Text = "AppSetting";
        }

        private void LoadControls()
        {
            List<string> controlNames = new List<string>(new string[] { "AppSetting", "Datalist", "HtmlBlock", "Image", "Menus", "Security" });
            for (int i = 0; i <= controlNames.Count - 1; i++)
            {
                this.TargetControlNames.Items.Add(controlNames[i]);
            }

            this.TargetControlNames.Text = "AppSetting";
        }

        private void HtmlBlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.SourceHtmlList != null)
            {
                HtmlBlockDiff diffPage = new HtmlBlockDiff(new Guid(this.tenantList.SelectedValue.ToString()), "ITEMS", this.SourceHtmlList, this.TargetHtmlList);
                diffPage.ShowDialog();
            }

            Cursor.Current = Cursors.Default;
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

        private void ImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.SourceImagesList != null)
            {
                ImagesDiff imgdiffPage = new ImagesDiff(new Guid(this.tenantList.SelectedValue.ToString()), "IMAGES", this.SourceImagesList, this.TargetImagesList);
                imgdiffPage.ShowDialog();
            }

            Cursor.Current = Cursors.Default;
        }

        private void LoadTreeViewforSecurity(TreeView treeView, List<DataListMainModel> itemList)
        {
            TreeNode listNode = null;
            treeView.Nodes.Clear();
            if (itemList != null)
            {
                List<TreeNode> itemNodes = null;
                TreeNode itemNode = null;
                try
                {
                    treeView.Nodes.Clear();
                    foreach (DataListMainModel list in itemList)
                    {
                        itemNodes = new List<TreeNode>();

                        list.Items.ForEach(f =>
                        {
                            itemNode = new TreeNode(f.Code);
                            itemNodes.Add(itemNode);
                        });
                        listNode = new TreeNode(list.ContentID, itemNodes.ToArray());
                        treeView.Nodes.Add(listNode);
                    }
                }
                finally
                {
                    listNode = null;
                    itemNodes = null;
                }
            }
        }

        private List<DataListMainModel> LoadTargetSecurityDatalist(Guid tenantModuleId)
        {
            List<DataList> lists = null;
            List<CodeListModel> listItems = null;
            List<DataListMainModel> listsMain = new List<DataListMainModel>();
            DataListMainModel list1 = null;
            List<DataListAttribute> attributes = new List<DataListAttribute>();
            List<ItemDataListItemAttributeVal> targetDataListItemAttributes = new List<ItemDataListItemAttributeVal>();
            Guid tenantID = new Guid(this.tenantList.SelectedValue.ToString());

            if (tenantModuleId == Guid.Empty)
            {
                lists = this.LoadHelper.GetDataList().Where(w => w.TenantID == tenantID && (w.ContentID == this.functionsContentID || w.ContentID == this.rolesContentID || w.ContentID == this.rightsContentID)).OrderBy(o => o.ContentID).ToList();
            }
            else
            {
                lists = this.LoadHelper.GetDataList().Where(w => w.TenantID == tenantID && w.TenantModuleID == tenantModuleId && (w.ContentID == this.functionsContentID || w.ContentID == this.rolesContentID || w.ContentID == this.rightsContentID)).OrderBy(o => o.ContentID).ToList();
            }

            lists = this.LoadHelper.GetAttributesList().Where(w => w.TenantID == tenantID && (w.ContentID == this.functionsContentID || w.ContentID == this.rolesContentID || w.ContentID == this.rightsContentID)).ToList();
            listItems = this.LoadHelper.GetDataListItems().Where(w => w.TenantID == tenantID && (w.ContentID == this.functionsContentID || w.ContentID == this.rolesContentID || w.ContentID == this.rightsContentID)).OrderBy(o => o.ContentID).ToList();
            targetDataListItemAttributes = this.LoadHelper.GetItemAttributeList().Where(a => a.ParentContentId == this.functionsContentID || a.ParentContentId == this.rolesContentID || a.ParentContentId == this.rightsContentID).OrderBy(o => o.ParentContentId).ToList(); 
            lists.ForEach(x => { attributes.AddRange(x.DataListAttributes); });

            this.targetListlink = this.LoadHelper.GetDataListLinks("TargetDataListLinks");

            foreach (DataList list in lists)
            {
                list1 = new DataListMainModel()
                {
                    ContentID = list.ContentID,
                    Description = list.Description,
                    IsActive = list.IsActive,
                    IsEditable = list.IsEditable,
                    ReleaseStatus = list.ReleaseStatus,
                    Items = this.ConvertToCustomDataListItems(list.ContentID, list.TenantID, listItems, attributes, targetDataListItemAttributes, this.targetListlink),
                    TenantID = list.TenantID,
                    TenantModuleID = list.TenantModuleID,
                    ID = list.ID,
                    ModuleName = list.ModuleName,
                    DataListAttributes = this.ConverttoAttributes(list.DataListAttributes, list.ContentID, list.TenantID, list.ID),
                    Name = list.DataListsName
                };

                listsMain.Add(list1);
            }

            return listsMain;
        }

        private List<DataListMainModel> LoadSourceSecurityDatalist(Guid tenantModuleId)
        {
            List<DataList> lists = null;
            List<CodeListModel> listItems = null;
            List<DataListMainModel> listsMain = new List<DataListMainModel>();
            DataListMainModel list1 = null;
            List<ItemDataListItemAttributeVal> sourceDataListItemAttributes = new List<ItemDataListItemAttributeVal>();
            List<DataListAttribute> attributes = new List<DataListAttribute>();
            Guid tenantID = new Guid(this.sourceTenantList.SelectedValue.ToString());

            if (tenantModuleId == Guid.Empty)
            {
                lists = this.SourceLoadHelper.GetDataList().Where(w => w.TenantID == tenantID && (w.ContentID == this.functionsContentID || w.ContentID == this.rolesContentID || w.ContentID == this.rightsContentID)).OrderBy(o => o.ContentID).ToList();
            }
            else
            {
                lists = this.SourceLoadHelper.GetDataList().Where(w => w.TenantID == tenantID && w.TenantModuleID == tenantModuleId && (w.ContentID == this.functionsContentID || w.ContentID == this.rolesContentID || w.ContentID == this.rightsContentID)).OrderBy(o => o.ContentID).ToList();
            }

            lists = this.SourceLoadHelper.GetAttributesList().Where(w => w.TenantID == tenantID && (w.ContentID == this.functionsContentID || w.ContentID == this.rolesContentID || w.ContentID == this.rightsContentID)).ToList();
            listItems = this.SourceLoadHelper.GetDataListItems().Where(t => t.TenantID == tenantID && (t.ContentID == this.functionsContentID || t.ContentID == this.rolesContentID || t.ContentID == this.rightsContentID)).OrderBy(o => o.ContentID).ToList();
            lists = this.SourceLoadHelper.GetAttributesList().Where(a => a.TenantID == tenantID && (a.ContentID == this.functionsContentID || a.ContentID == this.rolesContentID || a.ContentID == this.rightsContentID)).OrderBy(o => o.ContentID).ToList();

            sourceDataListItemAttributes = this.SourceLoadHelper.GetItemAttributeList().Where(a => a.ParentContentId == this.functionsContentID || a.ParentContentId == this.rolesContentID || a.ParentContentId == this.rightsContentID).OrderBy(o => o.ParentContentId).ToList(); 
            lists.ForEach(x => { attributes.AddRange(x.DataListAttributes); });
            this.sourceListlink = this.SourceLoadHelper.GetDataListLinks("SourceDataListLinks");
            foreach (DataList list in lists)
            {
                list1 = new DataListMainModel()
                {
                    ContentID = list.ContentID,
                    Description = list.Description,
                    IsActive = list.IsActive,
                    IsEditable = list.IsEditable,
                    ReleaseStatus = list.ReleaseStatus,
                    DataListAttributes = this.ConverttoAttributes(list.DataListAttributes, list.ContentID, list.TenantID, list.ID),
                    Items = this.ConvertToCustomDataListItems(list.ContentID, list.TenantID, listItems, attributes, sourceDataListItemAttributes, this.sourceListlink),
                    TenantID = list.TenantID,
                    TenantModuleID = list.TenantModuleID,
                    ID = list.ID,
                    Name = list.DataListsName,
                    ModuleName = list.ModuleName
                };

                listsMain.Add(list1);
            }

            return listsMain;
        }

        private void AppSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.SourceAppSetting != null)
            {
                AppSettingDiff diffPage = new AppSettingDiff(new Guid(this.tenantList.SelectedValue.ToString()), "AppSetting", this.SourceAppSetting, this.TargetAppSetting);
                diffPage.ShowDialog();
            }

            Cursor.Current = Cursors.Default;
        }

        private string GetDataSourceName(ConnectionStringSettings connection)
        {
            string dataSource = string.Empty;
            string[] parts = connection.ConnectionString.Split(';');
            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i].Trim().ToUpper();
                if (part.Contains("SOURCE"))
                {
                    dataSource = part;
                    break;
                }
            }

            return dataSource;
        }

        private void SecurityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.SourceList != null)
            {
                DatalistDiff diffPage = new DatalistDiff(new Guid(this.tenantList.SelectedValue.ToString()), "DATALIST", this.SourceList, this.TargetList);
                diffPage.ShowDialog();
            }

            Cursor.Current = Cursors.Default;
        }
    }
}