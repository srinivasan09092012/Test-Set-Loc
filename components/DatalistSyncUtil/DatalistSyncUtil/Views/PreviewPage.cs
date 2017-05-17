//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using HP.HSP.UA3.Core.BAS.CQRS.UserMeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DatalistSyncUtil.DataListService;
using System.ServiceModel.Security;
using DatalistSyncUtil.Security;

namespace DatalistSyncUtil.Views
{
    public partial class PreviewPage : Form
    {
        private static readonly AuthController _authController = new AuthController();
        TenantHelper objTargetHelper = new TenantHelper();

        public PreviewPage()
        {
            this.InitializeComponent();
        }

        public PreviewPage(List<DataListMainModel> finalList, List<CodeItemModel> finalListItems, List<ItemLanguage> finalLanguages, List<ItemAttribute> finalAttributes,List<DataListItemLink> finalLinkList, List<ItemDataListItemAttributeVal> finalItemAttributes)
        {
            this.InitializeComponent();

            this.Cache = new RedisCacheManager();
            this.FinalList = finalList;
            this.FinalListItems = finalListItems;
            this.FinalItemLanguages = finalLanguages;
            this.FinalAttributes = finalAttributes;
            this.FinalListLinkItems = finalLinkList;
            this.FinalItemAttributes = finalItemAttributes;
            this.LoadHelper = new TenantHelper();
            this.TargetDataList = this.LoadHelper.GetDataList();
            this.LoadTreeView(this.PreviewTreeList, this.FinalList);
        }

        public PreviewPage(List<HtmlBlockMainModel> finalHtmlBlks, List<HtmlBlockLanguage> finalHtmlBlkLanguages)
        {
            this.InitializeComponent();
            this.Cache = new RedisCacheManager();
            this.FinalHtmlBlks = finalHtmlBlks;
            this.FinalHtmlBlkLanguages = finalHtmlBlkLanguages;
            this.LoadHelper = new TenantHelper();
            this.LoadTreeView(this.PreviewTreeList, this.FinalHtmlBlks);
        }

        public ICacheManager Cache { get; set; }

        public List<DataListMainModel> FinalList { get; set; }

        public List<CodeItemModel> FinalListItems { get; set; }

        public List<DataListItemLink> FinalListLinkItems { get; set; }

        public List<ItemLanguage> FinalItemLanguages { get; set; }

        public List<ItemAttribute> FinalAttributes { get; set; }

        public List<ItemDataListItemAttributeVal> FinalItemAttributes { get; set; }

        public List<DataList> TargetDataList { get; set; }

        public List<HtmlBlockMainModel> FinalHtmlBlks { get; set; }

        public List<HtmlBlockLanguage> FinalHtmlBlkLanguages { get; set; }

        public TenantHelper LoadHelper { get; set; }

        private void Submit_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (this.FinalHtmlBlks != null)
                {
                    this.SaveHtmlBlks();
                }

                if (this.FinalHtmlBlkLanguages != null)
                {
                    this.SaveHtmlBlkLanguages();
                }
                else
                {
                    // this.SaveDataList();
                    this.SaveDataListWithDataListAttributes();

                    // this.SaveDatalistItems();
                    this.SaveDataListItemsWithAttributesValandLinks();

                    //  this.SaveDatalistItems();
                    // this.SaveDataListAttributes();
                    this.SaveDataItemLink();
                }
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Saved Sucessfully");
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Failed to Save");
            }
                
        }

        private void SaveDataItemLink()
        {
            List<DataList> dataList = this.LoadHelper.GetDataList();
            DataList parentlist = null;
            DataList childlist = null;
            if (this.FinalListLinkItems != null)
            {
                this.FinalListLinkItems.ForEach(f =>
                {
                    parentlist = dataList.Where(e => e.ContentID == f.ParentDataList && e.TenantID == f.TenantID).FirstOrDefault();
                    childlist = dataList.Where(e => e.ContentID == f.ChildDataList && e.TenantID == f.TenantID).FirstOrDefault();
                    if (parentlist != null && childlist != null)
                    {
                        f.ParentID = parentlist.ID;
                        f.ChildID = childlist.ID;
                        if (f.Status == "DATALIST_NEW")
                        {
                            this.LoadHelper.AddDataListLink(f);
                        }
                        else
                        {
                           this.LoadHelper.UpdateDatalistLink(f);
                        }
                    }
                });
            }

          //  this.Cache.Remove("TargetDataListLinks");
        }

        private void SaveHtmlBlks()
        {
            try
            {
                if (this.FinalHtmlBlks != null)
                {
                    List<TenantModuleModel> modules = this.LoadHelper.LoadModules();

                    foreach (HtmlBlockMainModel list in this.FinalHtmlBlks)
                    {
                        if (list.Status == "NEW")
                        {
                            this.LoadHelper.AddHtmlBlk(list);
                            if (this.FinalHtmlBlkLanguages != null)
                            {
                                this.FinalHtmlBlkLanguages.ForEach(f =>
                                {
                                    if (f.HtmlBlockId == list.ID)
                                    {
                                        this.LoadHelper.AddHtmlBlkLanguage(f);
                                    }
                                });
                            }

                            MessageBox.Show("Html Blocks Added successfully !!");
                        }
                        else
                        {
                           this.LoadHelper.UpdateHtmlBlk(list);
                           MessageBox.Show("Html Blocks successfully Updated !!");
                        }
                    }

                    this.Cache.Remove("TargetHtmlBlock");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message);
            }
        }

        private void SaveHtmlBlkLanguages()
        {
            List<HtmlBlockModel> htmlBlkLangs = this.LoadHelper.GetHTMLList();
            HtmlBlockModel lang = null;

            if (this.FinalHtmlBlkLanguages != null)
            {
                this.FinalHtmlBlkLanguages.ForEach(f =>
                {
                    lang = htmlBlkLangs.Find(e => e.ID == f.HtmlBlockId);
                    if (lang != null)
                    {
                        f.HtmlBlockId = lang.ID;
                        if (f.Status == "HTML_NEW" || f.Status == "LANG_NEW")
                        {
                            this.LoadHelper.AddHtmlBlkLanguage(f);
                        }
                        else
                        {
                            this.LoadHelper.UpdateHtmlBlkLanguage(f);
                        }
                    }
                });

                this.Cache.Remove("TargetHtmlLangs");
            }
        }

        private void SaveDataListAttributes()
        {
            List<DataList> dataList = this.LoadHelper.GetDataList();
            DataList list = null;
            if (this.FinalAttributes != null)
            {
                this.FinalAttributes.ForEach(f =>
                {
                    list = dataList.Where(e => e.ContentID == f.ParentContentId && e.TenantID == f.TenantID).FirstOrDefault();
                    if (list != null)
                    {
                        f.DataListID = list.ID;
                        f.DataListTypeID = f.DataListTypeID;
                        if (f.Status == "DATALIST_NEW")
                        {
                            this.LoadHelper.AddDataListAttributes(f);
                        }
                        else
                        {
                            this.LoadHelper.UpdateDatalistAttribute(f);
                        }
                    }
                });
            }

           // this.Cache.Remove("TargetUtilityDataAttrKey");
           // this.Cache.Remove("TargetUtilityDataListItemAttrKey");
           // this.Cache.Remove("TargetUtilityCombineAttributes");
        }

        private void SaveDatalistItems()
        {
            List<DataList> dataList = this.LoadHelper.GetDataList();
            DataList list = null;

            if (this.FinalListItems != null)
            {
                this.FinalListItems.ForEach(f =>
                {
                    list = dataList.Where(e => e.ContentID == f.ContentID && e.TenantID == f.TenantID).FirstOrDefault();
                    if (list != null)
                    {
                        f.DatalistID = list.ID;
                        if (f.ID == Guid.Empty)
                        {
                            this.LoadHelper.AddDatalistItem(f);
                        }
                        else
                        {
                            this.LoadHelper.UpdateDatalistItem(f);
                        }
                    }
                });

                this.Cache.Remove("TargetDataListItems");
            }
        }

        private void SaveDatalistItemLanguages()
        {
            List<CodeListModel> dataListItems = this.LoadHelper.GetDataListItems();
            CodeListModel item = null;

            if (this.FinalItemLanguages != null)
            {
                this.FinalItemLanguages.ForEach(f =>
                {
                    item = dataListItems.Find(e => e.ContentID == f.ContentID && e.Code == f.Code && e.IsActive == true);
                    if (item != null)
                    {
                        f.ItemID = item.ID;
                        if (f.Status == "DATALIST_NEW" || f.Status == "ITEM_NEW" || f.Status == "LOCALE_NEW")
                        {
                            this.LoadHelper.AddDatalistItemLanguage(f);
                        }
                        else
                        {
                            this.LoadHelper.UpdateDatalistItemLanguage(f);
                        }
                    }
                });

              //  this.Cache.Remove("TargetDataListItems");
              //  this.Cache.Remove("TargetUtilityDataAttrKey");
              //  this.Cache.Remove("TargetUtilityDataListItemAttrKey");
              //  this.Cache.Remove("TargetUtilityCombineAttributes");
            }
        }

        private void SaveDataList()
        {
            try
            {
                if (this.FinalList != null)
                {
                    List<TenantModuleModel> modules = this.LoadHelper.LoadModules();

                    foreach (DataListMainModel list in this.FinalList)
                    {
                        list.TenantModuleID = modules.Find(f => f.TenantId == list.TenantID && f.ModuleName == list.ModuleName).TenantModuleId;
                        if (list.ID == null || list.ID == Guid.Empty)
                        {
                            this.LoadHelper.AddDatalist(list);
                        }
                        else
                        {
                            this.LoadHelper.UpdateDatalist(list);
                        }
                    }

                  //  this.Cache.Remove("TargetDataLists");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message);
            }
        }

        private void SaveDataListItemsWithAttributesValandLinks()
        {
           

            List<DataList> dataList = this.LoadHelper.GetDataList();
            DataList list = null;
            DataListItemAdded itemadded = new DataListItemAdded();
            DataListItemUpdated itemupdated = new DataListItemUpdated();

            if (this.FinalListItems != null)
            {
                var serviceApi = new ServiceApiFactory(_authController.GeToken(@"UA3dev\PM02DevAdmin1", "Adminpm02"));
                var objService = serviceApi.GetService<IDataListsService>("WS2007FederationHttpBinding_IDataListsService");
                this.FinalListItems.ForEach(f =>
                {
                    list = dataList.Where(e => e.ContentID == f.ContentID && e.TenantID == f.TenantID).FirstOrDefault();
                    if (list != null)
                    {
                        

                        RequestorModel requestObject = new RequestorModel() { TenantId = list.TenantID.ToString() };
                        if (list != null)
                        {
                            f.DatalistID = list.ID;
                            if (f.Status == "DATALIST_NEW" || f.Status=="NEW")
                            {
                                AddDataListItemCommand addListitem = new AddDataListItemCommand();
                                addListitem.Requestor = requestObject;
                                addListitem.AddDataListItem = this.ConvertServiceAddDataListItems(f);
                                itemadded=objService.AddDataListItem(addListitem);
                            }
                            else
                            {
                                UpdateDataListItemCommand updateListitem = new UpdateDataListItemCommand();
                                updateListitem.Requestor = requestObject;
                                updateListitem.UpdateDataListItem = this.ConvertUpdateServiceDataListItems(f);
                                itemupdated=objService.UpdateDataListItem(updateListitem);
                            }
                        }
                    }
                });

                if (itemadded != null || itemupdated != null)
                {
                    MessageBox.Show("Data Added Successfully");
                    this.Cache.Remove("TargetDataListItems");
                    this.Cache.Remove("UtilityDataItemLinkerKeytarget");
                    this.Cache.Remove("UtilityDataAttrKeytarget");
                    this.Cache.Remove("UtilityDataListItemAttrKeytarget");
                    this.Cache.Remove("UtilityCombineAttributestarget");
                    this.Cache.Remove("TargetDataListItemAttributes");
                }


               // this.Cache.Remove("TargetDataListItems");
            }
        }

        private UpdateDataListItem ConvertUpdateServiceDataListItems(CodeItemModel f)
        {
            UpdateDataListItem listitem = new UpdateDataListItem();
            listitem.DataListId = f.DatalistID.ToString();
            listitem.DataListItemId = f.ID.ToString();
            listitem.Key = f.Code;
            listitem.OrderIndex = f.OrderIndex ?? default(int);
            listitem.ItemIsActive = f.IsActive;
            listitem.EffectiveDate = f.EffectiveStartDate ?? default(DateTime);
            listitem.EndDate = f.EffectiveEndDate ?? default(DateTime);
            listitem.ItemLastModified = DateTime.UtcNow;
            listitem.ItemIsEditable = f.IsEditable;
            listitem.DataListItemLanguages = this.ConvertCustomLang(f.LanguageList);

            //listitem.DataListItemLinks=f.L
            if(f.Attributes!=null)
            listitem.DataListAttributeValues = this.ConvertToCustomAttributeVal(f.Attributes);
            return listitem;
        }

        private AddDataListItem ConvertServiceAddDataListItems(CodeItemModel f)
        {           
            AddDataListItem listitem = new AddDataListItem();
            //listitem.DataListContentID = f.ContentID.ToString();
            listitem.DataListId = f.DatalistID.ToString();
            listitem.DataListItemId = f.ID.ToString();
            listitem.Key = f.Code;
            listitem.OrderIndex = f.OrderIndex ?? default(int);
            listitem.ItemIsActive = f.IsActive;
            listitem.EffectiveDate = f.EffectiveStartDate ?? default(DateTime);
            listitem.EndDate = f.EffectiveEndDate ?? default(DateTime);
            listitem.ItemLastModified = DateTime.UtcNow;
            listitem.ItemIsEditable = f.IsEditable;
            listitem.DataListItemLanguages = this.ConvertCustomLang(f.LanguageList);

            //listitem.DataListItemLinks=f.L
            if (f.Attributes != null)
            listitem.DataListAttributeValues = ConvertToCustomAttributeVal(f.Attributes);
            return listitem;
        }

        private DataListAttributeValue[] ConvertToCustomAttributeVal(List<ItemDataListItemAttributeVal> attributes)
        {
            DataListService.DataListAttributeValue[] DataListItemAttr = null;
            if (attributes != null)
            {
                DataListItemAttr=new DataListService.DataListAttributeValue[attributes.Count()];
                int iCount = 0;
                attributes.ForEach(x =>
                {
                    DataListService.DataListAttributeValue itemattr = new DataListService.DataListAttributeValue();
                    itemattr.DataListAttributeId = x.DataListAttributeID.ToString();
                    itemattr.DataListsItemValueId = x.DataListValueID.ToString();
                    DataListItemAttr[iCount] = itemattr;
                });
            }
            return DataListItemAttr;
        }

        private DataListService.DataListItemLanguage[] ConvertCustomLang(List<ItemLanguage> languageList)
        {
            DataListService.DataListItemLanguage[] dataListlistlang = new DataListService.DataListItemLanguage[languageList.Count()];
            int iCount = 0;
            languageList.ForEach(x =>
            {
                DataListService.DataListItemLanguage lang = new DataListService.DataListItemLanguage();
                lang.DataListItemId = x.ItemID.ToString();
                lang.Local = x.LocaleID;
                lang.Description = x.Description;
                lang.LongDescription = x.LongDescription;
                lang.IsActive = x.IsActive;
                lang.LastModified = DateTime.UtcNow;
                dataListlistlang[iCount] = lang;
                iCount++;
            });
            return dataListlistlang;
        }

        private void SaveDataListWithDataListAttributes()
        {
            
            try
            {
                if (this.FinalList != null)
                {
                    var serviceApi = new ServiceApiFactory(_authController.GeToken(@"UA3dev\PM02DevAdmin1", "Adminpm02"));
                    var objService = serviceApi.GetService<IDataListsService>("WS2007FederationHttpBinding_IDataListsService");

                    DataListsAdded datalistadd = new DataListsAdded();
                    DataListsUpdated datalistupdated = new DataListsUpdated();
                    foreach (DataListMainModel list in this.FinalList)
                    {
                        List<TenantModuleModel> modules = this.LoadHelper.LoadModules();

                        RequestorModel requestObject = new RequestorModel() { TenantId = list.TenantID.ToString() };
                        list.TenantModuleID = modules.Find(f => f.TenantId == list.TenantID && f.ModuleName == list.ModuleName).TenantModuleId;




                        if (list.Status == "NEW")
                        {
                            AddDataListCommand addList = new AddDataListCommand();
                            addList.Requestor = requestObject;
                            addList.AddDataList = this.ConvertAddServiceDataList(list);
                            datalistadd = objService.AddDataList(addList);
                        }
                        else
                        {
                            UpdateDataListCommand updatelist = new UpdateDataListCommand();
                            updatelist.Requestor = requestObject;
                            updatelist.UpdateDataList = (UpdateDataList)this.ConvertServiceUpdateDataList(list);
                            datalistupdated=objService.UpdateDataList(updatelist);
                        }
                    }

                    if (datalistadd != null || datalistupdated != null)
                    {
                        MessageBox.Show("Data Added Successfully");
                        this.Cache.Remove("TargetDataList");
                        this.Cache.Remove("UtilityDataItemLinkerKeytarget");
                        this.Cache.Remove("UtilityDataAttrKeytarget");
                        this.Cache.Remove("UtilityDataListItemAttrKeytarget");
                        this.Cache.Remove("UtilityCombineAttributestarget");
                        this.Cache.Remove("TargetDataListAttributes");

                    }
                }             
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message);
            }
        }

        private AddDataList ConvertAddServiceDataList(DataListMainModel list)
        {
            AddDataList dataList = new AddDataList();
            dataList.TenantModuleId = list.TenantModuleID.ToString();
            dataList.ContentId = list.ContentID;
            dataList.DataListsName = list.Name;
            dataList.Description = list.Description;
            dataList.IsActive = list.IsActive;
            dataList.IsEditable = list.IsEditable;
            dataList.ReleaseStatus = list.ReleaseStatus;
            dataList.DataListAttributes = this.ConvertToDataServiceAttributes(list.DataListAttributes);
            dataList.SortOrder = "OrderIndex";
            return dataList;
        }

        private UpdateDataList ConvertServiceUpdateDataList(DataListMainModel list)
        {
            UpdateDataList dataList = new UpdateDataList();
            dataList.TenantModuleId = list.TenantModuleID.ToString();
            dataList.ContentId = list.ContentID;
            dataList.DataListsName = list.Name;
            dataList.Description = list.Description;
            dataList.IsActive = list.IsActive;
            dataList.IsEditable = list.IsEditable;
            dataList.ReleaseStatus = list.ReleaseStatus;
            dataList.Id = list.ID.ToString();
            dataList.DataListAttributes = this.ConvertToDataServiceAttributes(list.DataListAttributes);
            dataList.SortOrder = "OrderIndex";
            return dataList;
        }

        private DataListService.DataListAttribute[] ConvertToDataServiceAttributes(List<ItemAttribute> listattributes)
        {
            DataListService.DataListAttribute[] dataListlistattributes = null;
            if (listattributes != null)
            {
                 dataListlistattributes = new DataListService.DataListAttribute[listattributes.Count()];
                int iCount = 0;
                listattributes.ForEach(x =>
                {
                    DataListService.DataListAttribute attribute = new DataListService.DataListAttribute();
                    attribute.TypeName = x.TypeName;
                    attribute.TypeDataListId = x.DataListTypeID.ToString();
                    attribute.TypeDefaultItemId = x.DefaultTypeValue.ToString();
                    attribute.IsActive = x.IsActive;
                    dataListlistattributes[iCount] = attribute;
                    iCount++;
                });
            }
            return dataListlistattributes;
        }

        private List<DataListService.DataListAttributeValue> GetCustomItemAttributes(List<DataListItemAttributeModel> attributes)
        {
            List<DataListService.DataListAttributeValue> itemattributes = new List<DataListService.DataListAttributeValue>();
            attributes.ForEach(x =>
            {
                DataListService.DataListAttributeValue objItemattribute = new DataListService.DataListAttributeValue();
                objItemattribute.DataListAttributeId = x.ID.ToString();
                objItemattribute.DataListsAttributeValueId = x.DataListAttributeID.ToString();
                objItemattribute.DataListsItemId = x.DataListItemID.ToString();
                objItemattribute.DataListsItemValueId = x.DataListValueID.ToString();
            });

            return itemattributes;
        }

        private void LoadTreeView(TreeView treeView, List<DataListMainModel> lists)
        {
            TreeNode listNode = null;
            TreeNode parentlistNode = null;
            List<TreeNode> itemNodes = null;
            List<TreeNode> itemNodesAttribute = null;
            TreeNode dataListNode = null;
            string contentID = string.Empty;

            try
            {
                treeView.Nodes.Clear();
                List<string> parentlists = this.GetAllContentID();
                bool isParentModeAdded = false;
                parentlists.ForEach(f =>
                {
                    itemNodes = new List<TreeNode>();
                    itemNodesAttribute = new List<TreeNode>();
                    
                    if (this.FinalListItems != null)
                    {
                        this.GetTreeItems(itemNodes, f.Trim());
                        listNode = new TreeNode(f.Trim(), itemNodes.ToArray());
                        if (itemNodes.Count != 0)
                        {
                            treeView.Nodes.Add(listNode);
                            isParentModeAdded = true;
                        }
                    }

                    if (this.FinalAttributes != null)
                    {
                        this.GetTreeAttributeItems(itemNodesAttribute, f.Trim());
                        listNode = new TreeNode(f.Trim(), itemNodesAttribute.ToArray());
                        if (itemNodesAttribute.Count != 0)
                        {
                            treeView.Nodes.Add(listNode);
                            isParentModeAdded = true;
                        }
                    }

                    if (this.FinalListLinkItems != null)
                    {
                        this.GetTreeLinkItems(itemNodesAttribute, f.Trim());
                        parentlistNode = new TreeNode(f.Trim());
                        treeView.Nodes.Add(listNode);
                        f = itemNodesAttribute[0].Text;
                        itemNodesAttribute.RemoveAt(0);
                        listNode = new TreeNode(f.Trim(), itemNodesAttribute.ToArray());
                       // listNode = new TreeNode(f.Trim(), itemNodesAttribute.ToArray());
                        if (itemNodesAttribute.Count != 0)
                        {
                            treeView.Nodes.Add(listNode);
                            isParentModeAdded = true;
                        }
                    }


                    if (!isParentModeAdded)
                    {
                        dataListNode = new TreeNode(f.Trim());
                        treeView.Nodes.Add(dataListNode);
                    }
                });

                treeView.ExpandAll();
            }
            finally
            {
                listNode = null;
                itemNodes = null;
            }
        }

        private void LoadTreeView(TreeView treeView, List<HtmlBlockMainModel> lists)
        {
            TreeNode listNode = null;
            List<TreeNode> langNodes = null;
            TreeNode htmlBlkNode = null;
            string contentID = string.Empty;

            try
            {
                treeView.Nodes.Clear();
                List<string> htmlBlks = this.GetAllContentID();

                htmlBlks.ForEach(f =>
                {
                    langNodes = new List<TreeNode>();
                    bool isParentModeAdded = false;
                    if (this.FinalHtmlBlks != null)
                    {
                        if (this.FinalHtmlBlkLanguages != null)
                        { 
                            this.GetTreeHtmlLanguages(langNodes, f.Trim());
                            listNode = new TreeNode(f.Trim(), langNodes.ToArray());
                            if (langNodes.Count != 0)
                            {
                                treeView.Nodes.Add(listNode);
                                isParentModeAdded = true;
                            }
                        }
                    }

                    if (!isParentModeAdded)
                    {
                        htmlBlkNode = new TreeNode(f.Trim());
                        treeView.Nodes.Add(htmlBlkNode);
                    }
                });

                treeView.ExpandAll();
            }
            finally
            {
                listNode = null;
                langNodes = null;
            }
        }

        private void GetTreeHtmlLanguages(List<TreeNode> langNodes, string contentID)
        {
            TreeNode node = null;
            string languageSeparator = " - ";

            List<HtmlBlockLanguage> items = this.FinalHtmlBlkLanguages.FindAll(f => f.ContentId.Trim() == contentID);

            items.ForEach(f =>
            {
                node = new TreeNode(f.LocaleId + languageSeparator + f.Html);
                langNodes.Add(node);
            });
        }

        private void GetTreeItems(List<TreeNode> itemNodes, string contentID)
        {
            TreeNode node = null;
            List<TreeNode> langNodes = null;
            List<TreeNode> itemAttrNodes = null;
            List<CodeItemModel> items = this.FinalListItems.FindAll(f => f.ContentID.Trim() == contentID);
            bool itemasparentadded = true;

            items.ForEach(f =>
            {
                langNodes = new List<TreeNode>();
                if (this.FinalItemLanguages != null)
                {
                    this.GetTreeItemLanguages(langNodes, f.ContentID, f.Code);
                }

                if (langNodes.ToArray().Count() != 0)
                {
                    node = new TreeNode(f.Code, langNodes.ToArray());
                    itemNodes.Add(node);
                    itemasparentadded = false;
                }

                itemAttrNodes = new List<TreeNode>();
                if (FinalItemAttributes != null)
                {
                    this.GetTreeItemAttributeVal(itemAttrNodes, f.ContentID, f.Code);
                }

                if (itemAttrNodes.ToArray().Count() != 0)
                {
                    node = new TreeNode(f.Code, itemAttrNodes.ToArray());
                    itemNodes.Add(node);
                    itemasparentadded = false;
                }

                if (itemasparentadded)
                {
                    node = new TreeNode(f.Code, itemAttrNodes.ToArray());
                    itemNodes.Add(node);
                }
            });
        }

        private void GetTreeItemAttributeVal(List<TreeNode> itemAttrNodes, string contentID, string code)
        {
            TreeNode node = null;
            string Separator = " - ";

            List<ItemDataListItemAttributeVal> items = this.FinalItemAttributes.FindAll(f => f.ParentContentId.Trim() == contentID && f.ItemCode == code);

            items.ForEach(f =>
            {
                node = new TreeNode(f.DataListAttributeName + Separator + f.DataListAttributeValue);
                itemAttrNodes.Add(node);
            });

        }
        private void GetTreeAttributeItems(List<TreeNode> itemNodes, string contentID)
        {
            TreeNode node = null;
            string separator = " - ";
            List<ItemAttribute> items = this.FinalAttributes.FindAll(f => f.ParentContentId.Trim() == contentID);

            items.ForEach(f =>
            {
                node = new TreeNode(f.ContentID + separator + f.Code);
                itemNodes.Add(node);
            });
        }

        private void GetTreeLinkItems(List<TreeNode> itemNodes, string contentID)
        {
            TreeNode node = null;
            string separator = " - ";
            List<DataListItemLink> items = this.FinalListLinkItems.FindAll(f => f.ParentDataList.Trim() == contentID);
            if (items.Count() > 0)
            {
                node = new TreeNode(items[0].ParentCode);
                itemNodes.Add(node);
                items.ForEach(f =>
                {
                    node = new TreeNode(f.ChildDataList + separator + f.Description);
                    itemNodes.Add(node);
                });
            }
        }

        private void GetTreeItemLanguages(List<TreeNode> langNodes, string contentID, string code)
        {
            TreeNode node = null;
            string languageSeparator = " - ";

            List<ItemLanguage> items = this.FinalItemLanguages.FindAll(f => f.ContentID.Trim() == contentID && f.Code == code);

            items.ForEach(f =>
            {
                node = new TreeNode(f.LocaleID + languageSeparator + f.Description + (!string.IsNullOrEmpty(f.LongDescription) ? (languageSeparator + f.LongDescription) : string.Empty));
                langNodes.Add(node);
            });
        }

        private List<string> GetAllContentID()
        {
            List<string> listContents = new List<string>();

            if (this.FinalList != null)
            {
                this.FinalList.ForEach(f =>
            {
                listContents.Add(f.ContentID.Trim());
            });
            }

            if (this.FinalListItems != null)
            {
                this.FinalListItems.ForEach(f =>
                {
                    if (!listContents.Contains(f.ContentID.Trim()))
                    {
                        listContents.Add(f.ContentID.Trim());
                    }
                });
            }

            if (this.FinalItemLanguages != null)
            {
                this.FinalItemLanguages.ForEach(f =>
                {
                    if (!listContents.Contains(f.ContentID.Trim()))
                    {
                        listContents.Add(f.ContentID.Trim());
                    }
                });
            }

            if (this.FinalListItems != null)
            {
                this.FinalListItems.ForEach(f =>
                {
                    if (!listContents.Contains(f.ContentID.Trim()))
                    {
                        listContents.Add(f.ContentID.Trim());
                    }
                });
            }

            if (this.FinalListLinkItems != null)
            {
                this.FinalListLinkItems.ForEach(f =>
                {
                    if (!listContents.Contains(f.ParentDataList.Trim()))
                    {
                        listContents.Add(f.ParentDataList.Trim());
                    }
                });
            }

            if (this.FinalAttributes != null)
            {
                this.FinalAttributes.ForEach(f =>
                {
                    if (!listContents.Contains(f.ParentContentId.Trim()))
                    {
                        listContents.Add(f.ParentContentId.Trim());
                    }
                });
            }

            if (this.FinalHtmlBlks != null)
            {
                this.FinalHtmlBlks.ForEach(f =>
                {
                    if (!listContents.Contains(f.ContentId.Trim()))
                    {
                        listContents.Add(f.ContentId.Trim());
                    }
                });
            }

            if (this.FinalHtmlBlkLanguages != null)
            {
                this.FinalHtmlBlkLanguages.ForEach(f =>
                {
                    if (!listContents.Contains(f.ContentId.Trim()))
                    {
                        listContents.Add(f.ContentId.Trim());
                    }
                });
            }

            return listContents;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}