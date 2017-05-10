//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using DatalistSyncUtil.Models;
using DatalistSyncUtil.Views;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace DatalistSyncUtil
{
    public partial class DatalistComparer : Form
    {
        TreeSyncData syncdata = new TreeSyncData();

        public DatalistComparer()
        {
            this.InitializeComponent();
            this.TargetConnectionString = ConfigurationManager.ConnectionStrings["TargetDataList"];
            this.SourceConnectionString = ConfigurationManager.ConnectionStrings["SourceDataList"];
            this.LoadHelper = new TenantHelper(this.TargetConnectionString);
            this.SourceLoadHelper = new SourceTenantHelper(this.SourceConnectionString);
            this.txtTargetConnection.Text = this.TargetConnectionString.ConnectionString;
            this.txtSourceConnection.Text = this.SourceConnectionString.ConnectionString;
            this.LoadTenant();
            this.LoadModules();
            this.LoadControls();
            this.LoadTargetControls();
            this.LoadSourceTenant();
            this.LoadSourceModules();
        }

        public List<DataListMainModel> SourceList { get; set; }
        public List<CodeLinkTable> SourceLink { get; set; }

        public List<MenuListModel> SourceMenus { get; set; }

        public List<MenuListModel> SourceMenuList { get; set; }

        public List<MenuItemModel> SourceMenuItems { get; set; }

        public string control { get; set; }

        public List<DataListMainModel> TargetList { get; set; }

        public List<MenuListModel> TargetMenuList { get; set; }

        public TenantHelper LoadHelper { get; set; }

        public SourceTenantHelper SourceLoadHelper { get; set; }

        public ConnectionStringSettings TargetConnectionString { get; set; }

        public ConnectionStringSettings SourceConnectionString { get; set; }

        public List<CodeLinkTable> listlink = new List<CodeLinkTable>();

        private void BtnSourceFile_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openDatalistFile.ShowDialog();

            if (result == DialogResult.OK)
            {
                string file = this.openDatalistFile.FileName;
                this.txtSourceConnection.Text = file;
                try
                {
                    List<TenantModuleModel> targetModules = this.moduleList.DataSource as List<TenantModuleModel>;
                    this.SourceMenus = JsonConvert.DeserializeObject<List<MenuListModel>>(File.ReadAllText(file));
                    List<MenuListModel> sourceMenuList = this.SourceMenus.ToList();
                    this.SourceList = JsonConvert.DeserializeObject<List<DataListMainModel>>(File.ReadAllText(file));
                    List<DataListMainModel> sourceDataList = this.SourceList.Where(w => targetModules.Select(s => s.ModuleName).Contains(w.ModuleName)).ToList();
                }
                catch (IOException)
                {
                }

                Cursor.Current = Cursors.WaitCursor;
                switch (control)
                {
                    case "Datalist":
                        syncdata.DataListItems = this.SourceList.OrderBy(x => x.ContentID).ToList();
                        break;

                    case "Menus":
                        syncdata.MenuListItems = this.SourceMenus.OrderBy(o => o.Name).ToList();
                        break;
                    default:
                        break;
                }
                this.LoadTreeView(this.sourceTreeList, syncdata);
                Cursor.Current = Cursors.Default;
            }
        }

        private void LoadTreeView(TreeView treeView, TreeSyncData itemList)
        {
            TreeNode listNode = null;
            string languageSeparator = " - ";
            treeView.Nodes.Clear();
            if (itemList.DataListItems != null)
            {
                TreeNode itemNode = null;
                List<TreeNode> itemNodes = null;
                TreeNode langNode = null;
                List<TreeNode> langNodes = null;
                List<TreeNode> attributenodes = null;
                TreeNode attributenode = null;

                try
                {
                    treeView.Nodes.Clear();
                    foreach (DataListMainModel list in itemList.DataListItems)
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
                            treeView.Nodes.Add(listNode);
                        }
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

            else
            {
                List<TreeNode> menuNodes = null;
                TreeNode menuNode = null;
                List<MenuItemModel> childnode = null;
                try
                {
                    foreach (MenuListModel list in itemList.MenuListItems)
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


        }

        private void BtnLoadTarget_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Guid tenantModuleId = (this.moduleList.SelectedItem as TenantModuleModel).TenantModuleId;
            switch (control)
            {
                case "Menus":
                    {
                        this.TargetMenuList = this.LoadTargetMenus();
                        List<MenuListModel> filteredDataList1 = null;
                        filteredDataList1 = this.TargetMenuList;
                        syncdata.MenuListItems = filteredDataList1.OrderBy(w => w.Name).ToList();
                        this.LoadTreeView(this.targetTreeList, syncdata);
                        break;
                     }
                case "Datalist":
                    {
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
                        syncdata.DataListItems = filteredDataList.OrderBy(o => o.ContentID).ToList();
                        this.LoadTreeView(this.targetTreeList, syncdata);
                        break;
                    }
                default:
                    break;
            }

            Cursor.Current = Cursors.Default;
        }

        private List<MenuListModel> LoadTargetMenus()
        {
            List<MenuListModel> lists = null;
            List<MenuListModel> listsMain = new List<MenuListModel>();
            MenuListModel list1 = null;
            List<MenuItemModel> ChildMenuItems = new List<MenuItemModel>();

            Guid tenantID = new Guid(this.sourceTenantList.SelectedValue.ToString());
            lists = this.LoadHelper.GetMenu().Where(w => w.TenantId == tenantID).ToList();
            lists.ForEach(x =>
            {
                ChildMenuItems.AddRange(x.Children.ToList());
            }
           );
            this.SourceMenuItems = ChildMenuItems;
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

            Guid tenantID = new Guid(this.tenantList.SelectedValue.ToString());

            lists = this.LoadHelper.GetDataList().Where(w => w.TenantID == tenantID).ToList();
            lists = this.LoadHelper.GetAttributesList().Where(w => w.TenantID == tenantID).ToList();
            listItems = this.LoadHelper.GetDataListItems();
            listlink = this.SourceLoadHelper.GetDataListLinks("TargetDataListLinks");

            foreach (DataList list in lists)
            {
                list1 = new DataListMainModel()
                {
                    ContentID = list.ContentID,
                    Description = list.Description,
                    IsActive = list.IsActive,
                    IsEditable = list.IsEditable,
                    ReleaseStatus = list.ReleaseStatus,
                    Items = this.ConvertToCustomDataListItems(list.ContentID, list.TenantID, listItems),
                    TenantID = list.TenantID,
                    TenantModuleID = list.TenantModuleID,
                    ID = list.ID,
                    ModuleName = list.ModuleName,
                    DataListAttributes = this.ConverttoAttributes(list.DataListAttributes, list.ContentID, list.TenantID, list.ID)
                };

                listsMain.Add(list1);
            }

            return listsMain;
        }

        private List<MenuListModel> LoadSourceMenus()
        {
            List<MenuListModel> lists = null;
            List<MenuListModel> listsMain = new List<MenuListModel>();
            MenuListModel list1 = null;
            List<MenuItemModel> ChildMenuItems = new List<MenuItemModel>();

            Guid tenantID = new Guid(this.sourceTenantList.SelectedValue.ToString());
            lists = this.SourceLoadHelper.GetMenu().ToList();
            lists.ForEach(x =>
            {
                ChildMenuItems.AddRange(x.Children.ToList());
            }
           );
            this.SourceMenuItems = ChildMenuItems;
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
            DataListMainModel list1 = null;
            Guid tenantID = new Guid(this.sourceTenantList.SelectedValue.ToString());
            lists = this.SourceLoadHelper.GetDataList().Where(w => w.TenantID == tenantID).ToList();
            lists = this.SourceLoadHelper.GetAttributesList().Where(w => w.TenantID == tenantID).ToList();
            listItems = this.SourceLoadHelper.GetDataListItems();
            listlink = this.SourceLoadHelper.GetDataListLinks("SourceDataListLinks");
            foreach (DataList list in lists)
            {
                list1 = new DataListMainModel()
                {
                    ContentID = list.ContentID,
                    Description = list.Description,
                    IsActive = list.IsActive,
                    IsEditable = list.IsEditable,
                    ReleaseStatus = list.ReleaseStatus,
                    Items = this.ConvertToCustomDataListItems(list.ContentID, list.TenantID, listItems),
                    TenantID = list.TenantID,
                    TenantModuleID = list.TenantModuleID,
                    ID = list.ID,
                    DataListAttributes = this.ConverttoAttributes(list.DataListAttributes, list.ContentID, list.TenantID, list.ID)

                };
                listsMain.Add(list1);
            }

            return listsMain;

        }

        private List<CodeItemModel> ConvertToCustomDataListItems(string contentID, Guid tenantID, List<CodeListModel> listItems)
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


                };
                items.Add(item);
            });

            return items;
        }
        private List<DataListItemLink> GetcoustmItemLink(string ContentID, Guid id, List<CodeLinkTable> link, int? OrderIndex, List<Languages> Language, List<CodeListModel> listItems, Guid TenantID)
        {
            List<DataListItemLink> listlink = new List<DataListItemLink>();
            DataListItemLink itemlink = null;

            List<CodeListModel> listItemschild = new List<CodeListModel>();
            link = link.Where(w => w.ParentID == id).ToList();



            link.ForEach(e =>
            {
                listItemschild = listItems.Where(w => w.ID == e.ChildID).ToList();
                if (listItemschild.Count() > 0)
                    itemlink = new DataListItemLink()
                    {
                        ParentDataList = ContentID,
                        ChildDataList = listItemschild[0].ContentID,
                        ParentCode = Convert.ToString(OrderIndex) + "-" + Language[0].Description,
                        ParentID = e.ParentID,
                        ChildID = e.ChildID,
                        IsActive = e.IsActive,
                        Description = listItemschild[0].OrderIndex + "-" + listItemschild[0].LanguageList[0].Description,
                        TenantID = TenantID

                    };
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
                    ItemID = e.CodeID
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


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void datalistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DatalistDiff diffPage = new DatalistDiff(new Guid(this.tenantList.SelectedValue.ToString()), "DATALIST", this.SourceList, this.TargetList);
            diffPage.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void datalistItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DatalistDiff diffPage = new DatalistDiff(new Guid(this.tenantList.SelectedValue.ToString()), "ITEMS", this.SourceList, this.TargetList);
            diffPage.ShowDialog();
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

        private void btnSourceLoad_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Guid tenantModuleId = (this.sourceModuleList.SelectedItem as TenantModuleModel).TenantModuleId;

            switch (control)
            {
                case "Menus":
                    {
                        this.SourceMenuList = this.LoadSourceMenus();
                        List<MenuListModel> filteredDataList1 = null;
                        filteredDataList1 = this.SourceMenuList;
                        syncdata.MenuListItems = filteredDataList1.OrderBy(w => w.Name).ToList();
                        this.LoadTreeView(this.sourceTreeList, syncdata);
                        break;
                    }
                case "Datalist":
                    {
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
                        syncdata.DataListItems = filteredDataList.OrderBy(o => o.ContentID).ToList();
                        this.LoadTreeView(this.sourceTreeList, syncdata);
                        break;
                    }
                default:
                    break;
            }
                    Cursor.Current = Cursors.Default;
        }

        private void menusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuListDiff menudiffPage = new MenuListDiff(new Guid(this.tenantList.SelectedValue.ToString()), "MENUS", this.SourceMenuList, this.TargetMenuList);
            menudiffPage.ShowDialog();
            this.Close();
        }

        private void LoadControls()
        {
            List<string> ControlNames = new List<string>(new string[] { "Datalist", "HtmlBlock", "Images", "Menus" });
            for (int i = 0; i <= ControlNames.Count - 1; i++)
            {
                this.ControlName.Items.Add(ControlNames[i]);
            }
        }

        private void LoadTargetControls()
        {
            List<string> ControlNames = new List<string>(new string[] { "Datalist", "HtmlBlock", "Images", "Menus" });
            for (int i = 0; i <= ControlNames.Count - 1; i++)
            {
                this.ControlsNames.Items.Add(ControlNames[i]);
            }
        }

        private void Control_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.control = this.ControlName.SelectedItem.ToString();
        }

        private void Controls_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.control = this.ControlName.SelectedItem.ToString();
        }
    }
}