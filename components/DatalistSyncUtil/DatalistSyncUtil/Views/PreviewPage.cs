//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using DatalistSyncUtil.Commands;
using DatalistSyncUtil.Configs;
using DatalistSyncUtil.DataListService;
using DatalistSyncUtil.Domain;
using DatalistSyncUtil.Security;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using HP.HSP.UA3.Core.BAS.CQRS.UserMeta;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace DatalistSyncUtil.Views
{
    public partial class PreviewPage : Form
    {
        private static readonly AuthController AuthController = new AuthController();
        private TenantHelper objTargetHelper = new TenantHelper();
        private AppSettingsReadOnly target;

        public PreviewPage()
        {
            this.InitializeComponent();
        }

        public PreviewPage(Guid tenantID, List<DataListMainModel> finalList, List<CodeItemModel> finalListItems, List<ItemLanguage> finalLanguages, List<ItemAttribute> finalAttributes, List<DataListItemLink> finalLinkList, List<ItemDataListItemAttributeVal> finalItemAttributes)
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
            this.TenantID = tenantID;
            this.TargetDataList = this.LoadHelper.GetDataList(tenantID);
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

        public PreviewPage(List<HelpNodeModel> finalHelp, List<HelpContentLanguageModel> finalHelpLanguages)
        {
            this.InitializeComponent();
            this.Cache = new RedisCacheManager();
            this.FinalHelp = finalHelp;
            this.FinalHelpLanguages = finalHelpLanguages;
            this.LoadHelper = new TenantHelper();
            this.LoadTreeView(this.PreviewTreeList, this.FinalHelp);
        }

        public PreviewPage(List<AppSettingsModel> finalAppSetting)
        {
            this.InitializeComponent();
            this.target = new AppSettingsReadOnly();
            this.Cache = new RedisCacheManager();
            this.LoadHelper = new TenantHelper();
            this.TargetAppsetting = this.LoadHelper.GetAppSetting(this.TenantID);
            this.FinalAppSetting = finalAppSetting;           
            
            this.LoadTreeView(this.PreviewTreeList, this.FinalAppSetting);
        }   
        
        public PreviewPage(List<ServicesMainModel> finalServices)
        {
            this.InitializeComponent();
            this.Cache = new RedisCacheManager();
            this.FinalServices = finalServices;
            this.LoadHelper = new TenantHelper();
            this.SourceLoadHelper = new SourceTenantHelper();
            this.SourceSecRights = this.SourceLoadHelper.GetSecRightsAndLabels(this.TenantID);
            this.TargetSecRights = this.LoadHelper.GetSecRightsAndLabels(this.TenantID);
            this.LoadTreeView(this.PreviewTreeList, this.FinalServices);
        }

        public PreviewPage(List<ImagesMainModel> finalImages, List<ImageLanguage> finalImageLanguages)
        {
            this.InitializeComponent();
            this.Cache = new RedisCacheManager();
            this.FinalImages = finalImages;
            this.FinalImageLanguages = finalImageLanguages;
            this.LoadHelper = new TenantHelper();
            this.LoadTreeView(this.PreviewTreeList, this.FinalImages);
        }

        public List<AppSettingsModel> Applist { get; set; }

        public ICacheManager Cache { get; set; }

        public List<AppSettingsModel> FinalAppSetting { get; set; }
        
        public List<DataListMainModel> FinalList { get; set; }

        public List<CodeItemModel> FinalListItems { get; set; }

        public List<DataListItemLink> FinalListLinkItems { get; set; }

        public List<ItemLanguage> FinalItemLanguages { get; set; }

        public List<ItemAttribute> FinalAttributes { get; set; }

        public List<ItemDataListItemAttributeVal> FinalItemAttributes { get; set; }

        public Guid TenantID { get; set; }

        public List<DataList> TargetDataList { get; set; }

        public List<HtmlBlockMainModel> FinalHtmlBlks { get; set; }

        public List<HtmlBlockLanguage> FinalHtmlBlkLanguages { get; set; }

        public List<HelpNodeModel> FinalHelp { get; set; }

        public List<HelpContentLanguageModel> FinalHelpLanguages { get; set; }

        public List<ImagesMainModel> FinalImages { get; set; }

        public List<ImageLanguage> FinalImageLanguages { get; set; }

        public List<ServicesMainModel> FinalServices { get; set; }

        public List<CodeListModel> SourceSecRights { get; set; }

        public List<CodeListModel> TargetSecRights { get; set; }

        public TenantHelper LoadHelper { get; set; }

        public SourceTenantHelper SourceLoadHelper { get; set; }

        public List<AppSettingsModel> TargetAppsetting { get; set; }    
                  
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

                if (this.FinalImages != null)
                {
                    this.SaveImages();
                }

                if (this.FinalImageLanguages != null)
                {
                    this.SaveImageLanguages();
                }

                if (this.FinalAppSetting != null)
                {
                    this.SaveAppSettings();
                }

                if (this.FinalHelp != null)
                {
                    this.SaveHelp();
                }

                if (this.FinalHelpLanguages != null)
                {
                    this.SaveHelpLanguages();
                }

                if (this.FinalListLinkItems != null)
                {
                    this.SaveDataItemLink();
                }

                if (this.FinalServices != null)
                {
                    this.SaveServices();
                }
                else
                {
                    this.SaveDataListWithDataListAttributes();

                    this.SaveDataListItemsWithAttributesValandLinks();
                }

                Cursor.Current = Cursors.Default;
                this.Close();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Failed to Save:" + Convert.ToString(ex.InnerException));
            }                
        }      

       private void SaveAppSettings()
        {
            this.Applist = this.LoadHelper.GetAppSetting(this.TenantID);
            bool success = false;
            try
            {
                List<TenantModuleModel> modules = this.LoadHelper.LoadModules(this.TenantID);
                if (this.FinalAppSetting != null)
                {
                    foreach (AppSettingsModel list in this.FinalAppSetting)
                    {
                        list.TenantModuleID = modules.Find(f => f.ModuleName == list.ModuleName).TenantModuleId;
                        if (!this.Applist.Any(a => a.TenantModuleAppSettingId == list.TenantModuleAppSettingId))
                        {
                            success = this.LoadHelper.AddAppSetting(list);                            
                        }                       
                    }

                    if (success)
                    {
                        MessageBox.Show("AppSetting Added Successfully !! ");
                        this.Cache.Remove("TargetAppSetting" + this.TenantID.ToString());
                    }
                    else
                    {
                        MessageBox.Show("AppSetting Insertion Failed !! ");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message);
            }
        }

        private void SaveServices()
        {
            try
            {
                string securityRight = null;
                if (this.FinalServices != null)
                {
                    List<TenantModuleModel> modules = this.LoadHelper.LoadModules(this.TenantID);

                    foreach (ServicesMainModel list in this.FinalServices)
                    {
                        list.TenantModuleID = modules.Find(f => f.TenantModuleId == list.TenantModuleID).TenantModuleId;
                        securityRight = this.SourceSecRights.Find(c => c.ID == list.SecurityRightItemID).Code;
                        list.SecurityRightItemID = this.TargetSecRights.Find(c => c.Code == securityRight).ID;
                        if (list.Status == "NEW")
                        {
                            this.LoadHelper.AddServices(list);
                        }
                        else
                        {
                            this.LoadHelper.UpdateServices(list);
                        }
                    }

                    this.Cache.Remove("TargetServices");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message);
            }
        }

        private void SaveDataItemLink()
        {
            List<DataList> dataList = this.LoadHelper.GetDataList(this.TenantID);
            DataList parentlist = null;
            DataList childlist = null;
            if (this.FinalListLinkItems != null)
            {
                this.FinalListLinkItems.ForEach(f =>
                {
                    parentlist = dataList.Where(e => e.ContentID == f.ParentDataList).FirstOrDefault();
                    childlist = dataList.Where(e => e.ContentID == f.ChildDataList).FirstOrDefault();
                    if (parentlist != null && childlist != null)
                    {
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

            this.Cache.Remove("TargetDataListLinks" + this.TenantID.ToString());
        }

        private void SaveHtmlBlks()
        {
            try
            {
                if (this.FinalHtmlBlks != null)
                {
                    List<TenantModuleModel> modules = this.LoadHelper.LoadModules(this.TenantID);

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

                    this.Cache.Remove("TargetHtmlBlock" + this.TenantID.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message);
            }
        }

        private void SaveHtmlBlkLanguages()
        {
            List<HtmlBlockModel> htmlBlkLangs = this.LoadHelper.GetHTMLList(this.TenantID);
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

                this.Cache.Remove("TargetHtmlLangs" + this.TenantID.ToString());
            }
        }

        private void SaveHelp()
        {
            try
            {
                if (this.FinalHelp != null)
                {
                    List<TenantModuleModel> modules = this.LoadHelper.LoadModules(this.TenantID);
                    this.FinalHelp = this.FinalHelp.OrderBy(x => x.NodeDepth).ToList();
                    foreach (HelpNodeModel list in this.FinalHelp)
                    {
                        if (list.Status == "NEW")
                        {
                            AddHelpContentCommand addHelpContentCommand = new AddHelpContentCommand()
                            {
                                HelpNodeModel = list
                            };
                            this.LoadHelper.AddHelp(addHelpContentCommand);
                        }
                        else
                        {
                            UpdateHelpContentCommand updateHelpContentCommand = new UpdateHelpContentCommand()
                            {
                                HelpNodeModel = list
                            };
                            this.LoadHelper.Updatehelp(updateHelpContentCommand);
                        }
                    }

                    this.Cache.Remove("TargetHtmlBlock" + this.TenantID.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message);
            }
        }

        private void SaveHelpLanguages()
        {
            List<HelpNodeModel> helpLangs = this.LoadHelper.GetHelpList(this.TenantID);
            HelpNodeModel lang = null;

            if (this.FinalHelpLanguages != null)
            {
                this.FinalHelpLanguages.ForEach(f =>
                {
                    lang = helpLangs.Find(e => e.HelpNodeId == f.HelpNodeId);
                    if (lang != null)
                    {
                        f.HelpNodeId = lang.HelpNodeId;
                        if (f.Status == "LANG_NEW")
                        {
                            this.LoadHelper.AddHelpLanguage(f);
                        }
                        else
                        {
                            this.LoadHelper.UpdateHelpLanguage(f);
                        }
                    }
                });

                this.Cache.Remove("TargetHtmlLangs" + this.TenantID.ToString());
            }
        }

        private void SaveImages()
        {
            try
            {
                if (this.FinalImages != null)
                {
                    List<TenantModuleModel> modules = this.LoadHelper.LoadModules(this.TenantID);

                    foreach (ImagesMainModel list in this.FinalImages)
                    {
                        if (list.Status == "NEW")
                        {
                            this.LoadHelper.AddImage(list);
                            if (this.FinalImageLanguages != null)
                            {
                                this.FinalImageLanguages.ForEach(f =>
                                {
                                    if (f.ImageId == list.ImageId)
                                    {
                                        this.LoadHelper.AddImageLanguage(f);
                                    }
                                });
                            }
                        }
                        else
                        {
                            this.LoadHelper.UpdateImage(list);
                        }
                    }

                    this.Cache.Remove("TargetImages" + this.TenantID.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message);
            }
        }

        private void SaveImageLanguages()
        {
            List<ImageListModel> imageLangs = this.LoadHelper.GetImagesList(this.TenantID);
            ImageListModel lang = null;

            if (this.FinalImageLanguages != null)
            {
                this.FinalImageLanguages.ForEach(f =>
                {
                    lang = imageLangs.Find(e => e.ID == f.ImageId);
                    if (lang != null)
                    {
                        f.ImageId = lang.ID;
                        if (f.Status == "IMG_NEW" || f.Status == "LANG_NEW")
                        {
                            this.LoadHelper.AddImageLanguage(f);
                        }
                        else
                        {
                            this.LoadHelper.UpdateImageLanguage(f);
                        }
                    }
                });

                this.Cache.Remove("TargetImageLangs" + this.TenantID.ToString());
            }
        }

        private void SaveDataListAttributes()
        {
            List<DataList> dataList = this.LoadHelper.GetDataList(this.TenantID);
            DataList list = null;
            if (this.FinalAttributes != null)
            {
                this.FinalAttributes.ForEach(f =>
                {
                    list = dataList.Where(e => e.ContentID == f.ParentContentId).FirstOrDefault();
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

            // this.Cache.Remove("TargetUtilityDataAttrKey" + this.TenantID.ToString());
            // this.Cache.Remove("TargetUtilityDataListItemAttrKey" + this.TenantID.ToString());
            // this.Cache.Remove("TargetUtilityCombineAttributes" + this.TenantID.ToString());
        }

        private void SaveDatalistItems()
        {
            List<DataList> dataList = this.LoadHelper.GetDataList(this.TenantID);
            DataList list = null;

            if (this.FinalListItems != null)
            {
                this.FinalListItems.ForEach(f =>
                {
                    list = dataList.Where(e => e.ContentID == f.ContentID).FirstOrDefault();
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

                this.Cache.Remove("TargetDataListItems" + this.TenantID.ToString());
            }
        }

        private void SaveDatalistItemLanguages()
        {
            List<CodeListModel> dataListItems = this.LoadHelper.GetDataListItems(this.TenantID);
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

                //  this.Cache.Remove("TargetDataListItems" + this.TenantID.ToString());
                //  this.Cache.Remove("TargetUtilityDataAttrKey" + this.TenantID.ToString());
                //  this.Cache.Remove("TargetUtilityDataListItemAttrKey" + this.TenantID.ToString());
                //  this.Cache.Remove("TargetUtilityCombineAttributes" + this.TenantID.ToString());
            }
        }

        private void SaveDataList()
        {
            try
            {
                if (this.FinalList != null)
                {
                    List<TenantModuleModel> modules = this.LoadHelper.LoadModules(this.TenantID);

                    foreach (DataListMainModel list in this.FinalList)
                    {
                        list.TenantModuleID = modules.Find(f => f.ModuleName == list.ModuleName).TenantModuleId;
                        if (list.ID == null || list.ID == Guid.Empty)
                        {
                            this.LoadHelper.AddDatalist(list);
                        }
                        else
                        {
                            this.LoadHelper.UpdateDatalist(list);
                        }
                    }

                    //  this.Cache.Remove("TargetDataLists" + this.TenantID.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message);
            }
        }

        private void SaveDataListItemsWithAttributesValandLinks()
        {
            List<DataList> dataList = this.LoadHelper.GetDataList(this.TenantID);
            DataList list = null;
            DataListItemAdded itemadded = new DataListItemAdded();
            DataListItemUpdated itemupdated = new DataListItemUpdated();
            int fedBindingValue = Convert.ToInt32(ConfigurationManager.AppSettings["WSHttpBindingType"]);
            int basicBindingValue = Convert.ToInt32(ConfigurationManager.AppSettings["BasicHttpBindingType"]);
            if (basicBindingValue == 1)
            {
                try
                {
                    if (this.FinalListItems != null)
                    {
                        string basicHttpBinding = ConfigurationManager.AppSettings["BasicHttpBinding"];
                        DataListsServiceClient client = new DataListsServiceClient(basicHttpBinding);
                        this.FinalListItems.ForEach(f =>
                        {
                            list = dataList.Where(e => e.ContentID == f.ContentID && e.TenantID == f.TenantID).FirstOrDefault();
                            if (list != null)
                            {
                                RequestorModel requestObject = new RequestorModel() { TenantId = list.TenantID.ToString() };
                                if (list != null)
                                {
                                    f.DatalistID = list.ID;
                                    if (f.Status == "DATALIST_NEW" || f.Status == "NEW")
                                    {
                                        AddDataListItemCommand addListitem = new AddDataListItemCommand();
                                        addListitem.Requestor = requestObject;
                                        addListitem.AddDataListItem = this.ConvertServiceAddDataListItems(f);
                                        itemadded = client.AddDataListItem(addListitem);
                                    }
                                    else
                                    {
                                        UpdateDataListItemCommand updateListitem = new UpdateDataListItemCommand();
                                        updateListitem.Requestor = requestObject;
                                        updateListitem.UpdateDataListItem = this.ConvertUpdateServiceDataListItems(f);
                                        itemupdated = client.UpdateDataListItem(updateListitem);
                                    }
                                }
                            }
                        });

                        if (itemadded.DataListItemId != null || itemupdated.DataListItemId != null)
                        {
                            MessageBox.Show("Data Added Successfully DataListItems/ItemAttribute");
                            this.Cache.Remove("TargetDataListItems" + this.TenantID.ToString());
                            this.Cache.Remove("UtilityDataItemLinkerKeytarget" + this.TenantID.ToString());
                            this.Cache.Remove("UtilityDataAttrKeytarget" + this.TenantID.ToString());
                            this.Cache.Remove("UtilityDataListItemAttrKeytarget" + this.TenantID.ToString());
                            this.Cache.Remove("UtilityCombineAttributestarget" + this.TenantID.ToString());
                            this.Cache.Remove("TargetDataListItemAttributes" + this.TenantID.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }
            else
            {
                try
                {
                    if (this.FinalListItems != null)
                    {
                        string fed2007Binding = ConfigurationManager.AppSettings["WSBinding"];
                        string fedUserName = ConfigurationManager.AppSettings["WSUserName"];
                        string fedPassword = ConfigurationManager.AppSettings["WSPassword"];
                        var serviceApi = new ServiceApiFactory(AuthController.GeToken(@fedUserName, fedPassword));
                        var objService = serviceApi.GetService<IDataListsService>(fed2007Binding);
                        this.FinalListItems.ForEach(f =>
                        {
                            list = dataList.Where(e => e.ContentID == f.ContentID).FirstOrDefault();
                            if (list != null)
                            {
                                RequestorModel requestObject = new RequestorModel() { TenantId = list.TenantID.ToString() };
                                if (list != null)
                                {
                                    f.DatalistID = list.ID;
                                    if (f.Status == "DATALIST_NEW" || f.Status == "NEW")
                                    {
                                        AddDataListItemCommand addListitem = new AddDataListItemCommand();
                                        addListitem.Requestor = requestObject;
                                        addListitem.AddDataListItem = this.ConvertServiceAddDataListItems(f);
                                        itemadded = objService.AddDataListItem(addListitem);
                                    }
                                    else
                                    {
                                        UpdateDataListItemCommand updateListitem = new UpdateDataListItemCommand();
                                        updateListitem.Requestor = requestObject;
                                        updateListitem.UpdateDataListItem = this.ConvertUpdateServiceDataListItems(f);
                                        itemupdated = objService.UpdateDataListItem(updateListitem);
                                    }
                                }
                            }
                        });

                        if (itemadded.DataListItemId != null || itemupdated.DataListItemId != null)
                        {
                            MessageBox.Show("Data Added Successfully DataListItems/ItemAttribute" + this.TenantID.ToString());
                            this.Cache.Remove("TargetDataListItems" + this.TenantID.ToString());
                            this.Cache.Remove("UtilityDataItemLinkerKeytarget" + this.TenantID.ToString());
                            this.Cache.Remove("UtilityDataAttrKeytarget" + this.TenantID.ToString());
                            this.Cache.Remove("UtilityDataListItemAttrKeytarget" + this.TenantID.ToString());
                            this.Cache.Remove("UtilityCombineAttributestarget" + this.TenantID.ToString());
                            this.Cache.Remove("TargetDataListItemAttributes" + this.TenantID.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
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
            if (f.Attributes != null)
            {
                listitem.DataListAttributeValues = this.ConvertToCustomAttributeVal(f.Attributes);
            }

            return listitem;
        }

        private AddDataListItem ConvertServiceAddDataListItems(CodeItemModel f)
        {           
            AddDataListItem listitem = new AddDataListItem();
            listitem.DataListId = f.DatalistID.ToString();
            ///listitem.DataListItemId = f.ID.ToString();
            listitem.Key = f.Code;
            listitem.OrderIndex = f.OrderIndex ?? default(int);
            listitem.ItemIsActive = f.IsActive;
            listitem.EffectiveDate = f.EffectiveStartDate ?? default(DateTime);
            listitem.EndDate = f.EffectiveEndDate ?? default(DateTime);
            listitem.ItemLastModified = DateTime.UtcNow;
            listitem.ItemIsEditable = f.IsEditable;
            listitem.DataListItemLanguages = this.ConvertCustomLang(f.LanguageList);
            if (f.Attributes != null)
            {
                listitem.DataListAttributeValues = this.ConvertToCustomAttributeVal(f.Attributes);
            }

            ///listitem.DataListItemLinks=f.L
            if (f.Attributes != null)
            {
                listitem.DataListAttributeValues = this.ConvertToCustomAttributeVal(f.Attributes);
            }

            return listitem;
        }

        private DataListAttributeValue[] ConvertToCustomAttributeVal(List<ItemDataListItemAttributeVal> attributes)
        {
            DataListService.DataListAttributeValue[] dataListItemAttr = null;
            if (attributes != null)
            {
                dataListItemAttr = new DataListService.DataListAttributeValue[attributes.Count()];
                int count = 0;
                attributes.ForEach(x =>
                {
                    DataListService.DataListAttributeValue itemattr = new DataListService.DataListAttributeValue();
                    itemattr.DataListAttributeId = x.DataListAttributeID.ToString();
                    itemattr.DataListsItemValueId = x.DataListValueID.ToString();
                    dataListItemAttr[count] = itemattr;
                    count++;
                });
            }

            return dataListItemAttr;
        }

        private DataListService.DataListItemLanguage[] ConvertCustomLang(List<ItemLanguage> languageList)
        {
            DataListService.DataListItemLanguage[] dataListlistlang = new DataListService.DataListItemLanguage[languageList.Count()];
            int count = 0;
            languageList.ForEach(x =>
            {
                DataListService.DataListItemLanguage lang = new DataListService.DataListItemLanguage();
                lang.DataListItemId = x.ItemID.ToString();
                lang.Local = x.LocaleID;
                lang.Description = x.Description;
                lang.LongDescription = x.LongDescription;
                lang.IsActive = x.IsActive;
                lang.LastModified = DateTime.UtcNow;
                dataListlistlang[count] = lang;
                count++;
            });
            return dataListlistlang;
        }

        private void SaveDataListWithDataListAttributes()
        {
            int fedBindingValue = Convert.ToInt32(ConfigurationManager.AppSettings["WSHttpBindingType"]);
            int basicBindingValue = Convert.ToInt32(ConfigurationManager.AppSettings["BasicHttpBindingType"]);
            if (basicBindingValue == 1)
            {
                try
                {
                    if (this.FinalList != null)
                    {
                        string basicHttpBinding = ConfigurationManager.AppSettings["BasicHttpBinding"];
                        DataListsServiceClient client = new DataListsServiceClient(basicHttpBinding);

                        DataListsAdded datalistadd = new DataListsAdded();
                        DataListsUpdated datalistupdated = new DataListsUpdated();
                        foreach (DataListMainModel list in this.FinalList)
                        {
                            List<TenantModuleModel> modules = this.LoadHelper.LoadModules(this.TenantID);

                            RequestorModel requestObject = new RequestorModel() { TenantId = list.TenantID.ToString() };
                            list.TenantModuleID = modules.Find(f => f.ModuleName == list.ModuleName).TenantModuleId;
                            if (list.Status == "NEW")
                            {
                                AddDataListCommand addList = new AddDataListCommand();
                                addList.Requestor = requestObject;
                                addList.AddDataList = this.ConvertAddServiceDataList(list);
                                datalistadd = client.AddDataList(addList);
                            }
                            else
                            {
                                UpdateDataListCommand updatelist = new UpdateDataListCommand();
                                updatelist.Requestor = requestObject;
                                updatelist.UpdateDataList = (UpdateDataList)this.ConvertServiceUpdateDataList(list);
                                datalistupdated = client.UpdateDataList(updatelist);
                            }
                        }

                        if (datalistadd.DataListId != null || datalistupdated.DataListId != null)
                        {
                            MessageBox.Show("Data Added Successfully For DataList/DataListAttribute" + this.TenantID.ToString());
                            this.Cache.Remove("TargetDataList" + this.TenantID.ToString());
                            this.Cache.Remove("UtilityDataItemLinkerKeytarget" + this.TenantID.ToString());
                            this.Cache.Remove("UtilityDataAttrKeytarget" + this.TenantID.ToString());
                            this.Cache.Remove("UtilityDataListItemAttrKeytarget" + this.TenantID.ToString());
                            this.Cache.Remove("UtilityCombineAttributestarget" + this.TenantID.ToString());
                            this.Cache.Remove("TargetDataListAttributes" + this.TenantID.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }
            else
            {
                try
                {
                    if (this.FinalList != null)
                    {
                        string fed2007Binding = ConfigurationManager.AppSettings["WSBinding"];
                        string fedUserName = ConfigurationManager.AppSettings["WSUserName"];
                        string fedPassword = ConfigurationManager.AppSettings["WSPassword"];
                        var serviceApi = new ServiceApiFactory(AuthController.GeToken(@fedUserName, fedPassword));
                        var objService = serviceApi.GetService<IDataListsService>(fed2007Binding);
                        DataListsAdded datalistadd = new DataListsAdded();
                        DataListsUpdated datalistupdated = new DataListsUpdated();
                        foreach (DataListMainModel list in this.FinalList)
                        {
                            List<TenantModuleModel> modules = this.LoadHelper.LoadModules(this.TenantID);

                        RequestorModel requestObject = new RequestorModel() { TenantId = list.TenantID.ToString() };
                        list.TenantModuleID = modules.Find(f => f.ModuleName == list.ModuleName).TenantModuleId;
                        if (list.ID == null || list.ID == Guid.Empty)
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
                            datalistupdated = objService.UpdateDataList(updatelist);
                        }
                    }

                        if (datalistadd.DataListId != null || datalistupdated.DataListId != null)
                        {
                            MessageBox.Show("Data Added Successfully For DataLists/DataListAttribute" + this.TenantID.ToString());
                            this.Cache.Remove("TargetDataList" + this.TenantID.ToString());
                            this.Cache.Remove("UtilityDataItemLinkerKeytarget" + this.TenantID.ToString());
                            this.Cache.Remove("UtilityDataAttrKeytarget" + this.TenantID.ToString());
                            this.Cache.Remove("UtilityDataListItemAttrKeytarget" + this.TenantID.ToString());
                            this.Cache.Remove("UtilityCombineAttributestarget" + this.TenantID.ToString());
                            this.Cache.Remove("TargetDataListAttributes" + this.TenantID.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
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
                int count = 0;
                listattributes.ForEach(x =>
                {
                    DataListService.DataListAttribute attribute = new DataListService.DataListAttribute();
                    attribute.TypeName = x.TypeName;
                    attribute.TypeDataListId = x.DataListTypeID.ToString();
                    attribute.TypeDefaultItemId = x.DefaultTypeValue.ToString();
                    attribute.IsActive = x.IsActive;
                    dataListlistattributes[count] = attribute;
                    count++;
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
                        f = itemNodesAttribute[0].Text;
                        itemNodesAttribute.RemoveAt(0);
                        listNode = new TreeNode(f.Trim(), itemNodesAttribute.ToArray());
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

        private void LoadTreeView(TreeView treeView, List<ServicesMainModel> lists)
        {
            TreeNode listNode = null;
            List<TreeNode> svcNodes = null;

            try
            {
                treeView.Nodes.Clear();
                List<string> services = this.GetAllContentID();

                services.ForEach(f =>
                {
                    svcNodes = new List<TreeNode>();
                    if (this.FinalServices != null)
                    {
                        this.GetTreeServices(svcNodes, f.Trim());
                        listNode = new TreeNode(f.Trim(), svcNodes.ToArray());
                        if (svcNodes.Count != 0)
                        {
                            treeView.Nodes.Add(listNode);
                        }
                    }
                });

                treeView.ExpandAll();
            }
            finally
            {
                listNode = null;
                svcNodes = null;
            }
        }

        private void LoadTreeView(TreeView treeView, List<HelpNodeModel> lists)
        {
            TreeNode listNode = null;
            List<TreeNode> langNodes = null;
            TreeNode helpNode = null;
            string helpNodeNM = string.Empty;

            try
            {
                treeView.Nodes.Clear();
                List<string> helpNodes = this.GetAllContentID();

                helpNodes.ForEach(f =>
                {
                    langNodes = new List<TreeNode>();
                    listNode = new TreeNode(f.Trim(), langNodes.ToArray());
                    bool isParentModeAdded = false;
                    if (this.FinalHelp != null)
                    {
                        if (this.FinalHelpLanguages != null)
                        {
                            this.GetTreeHelpLanguages(langNodes, f.Trim());
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
                        helpNode = new TreeNode(f.Trim());
                        treeView.Nodes.Add(helpNode);
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

        private void LoadTreeView(TreeView treeView, List<AppSettingsModel> lists)
        {
            TreeNode listNode = null;
            List<TreeNode> appNodes = null;            
            string contentID = string.Empty;

            try
            {
                treeView.Nodes.Clear();
                List<string> appSetting = this.GetAllContentID();

                appSetting.ForEach(f =>
                {
                appNodes = new List<TreeNode>();
                    if (this.FinalAppSetting != null)
                    {
                         this.GetAppSetting(appNodes, f.Trim());
                        listNode = new TreeNode(f.Trim(), appNodes.ToArray());
                        if (appNodes.Count != 0)
                        {
                            treeView.Nodes.Add(listNode);
                        }
                    }
                });

                treeView.ExpandAll();
            }
            finally
            {
                listNode = null;
                appNodes = null;
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

        private void LoadTreeView(TreeView treeView, List<ImagesMainModel> lists)
        {
            TreeNode listNode = null;
            List<TreeNode> langNodes = null;
            TreeNode imageNode = null;
            string contentID = string.Empty;

            try
            {
                treeView.Nodes.Clear();
                List<string> images = this.GetAllContentID();

                images.ForEach(f =>
                {
                    langNodes = new List<TreeNode>();
                    bool isParentModeAdded = false;
                    if (this.FinalImages != null)
                    {
                        if (this.FinalImageLanguages != null)
                        {
                            this.GetTreeImageLanguages(langNodes, f.Trim());
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
                        imageNode = new TreeNode(f.Trim());
                        treeView.Nodes.Add(imageNode);
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

        private void GetTreeImageLanguages(List<TreeNode> langNodes, string contentID)
        {
            TreeNode node = null;
            string languageSeparator = " - ";
            string width = " , Width : ";
            string height = " , Height : ";

            List<ImageLanguage> items = this.FinalImageLanguages.FindAll(f => f.ContentId.Trim() == contentID);

            items.ForEach(f =>
            {
                node = new TreeNode(f.LocaleId + languageSeparator + f.Source + languageSeparator + f.ToolTip + width + f.Width + height + f.Height);
                langNodes.Add(node);
            });
        }

        private void GetAppSetting(List<TreeNode> langNodes, string appSetting)
        {
            TreeNode node = null;
            
            List<AppSettingsModel> items = this.FinalAppSetting.FindAll(f => f.AppSettingKey.Trim() == appSetting);

            items.ForEach(f =>
            {
                node = new TreeNode(f.TargetValue);
                langNodes.Add(node);
            });
        }

        private void GetTreeServices(List<TreeNode> svcNodes, string services)
        {
            TreeNode node = null;
            string separator = " - ";
            List<ServicesMainModel> svc = this.FinalServices.FindAll(f => f.Name.Trim() == services);

            svc.ForEach(f =>
            {
                node = new TreeNode(f.Name + separator + " Base URL : " + f.BaseURL + separator + f.IsActive);
                svcNodes.Add(node);
            });
        }

        private void GetTreeHelpLanguages(List<TreeNode> langNodes, string helpNodeNM)
        {
            TreeNode node = null;
            string languageSeparator = " - ";

            List<HelpContentLanguageModel> items = this.FinalHelpLanguages.FindAll(f => f.HelpNodeNM.Trim() == helpNodeNM);

            items.ForEach(f =>
            {
                node = new TreeNode(f.Language + languageSeparator + f.HtmlContent);
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
            string separator = " - ";

            List<ItemDataListItemAttributeVal> items = this.FinalItemAttributes.FindAll(f => f.ParentContentId.Trim() == contentID && f.ItemCode == code);

            items.ForEach(f =>
            {
                node = new TreeNode(f.DataListAttributeName + separator + f.DataListAttributeValue);
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
                node = new TreeNode(f.ContentID + separator + f.TypeName);
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

            if (this.FinalHelp != null)
            {
                this.FinalHelp.ForEach(f =>
                {
                    if (!listContents.Contains(f.HelpNodeNM.Trim()))
                    {
                        listContents.Add(f.HelpNodeNM.Trim());
                    }
                });
            }

            if (this.FinalHelpLanguages != null)
            {
                this.FinalHelpLanguages.ForEach(f =>
                {
                    if (!listContents.Contains(f.HelpNodeNM.Trim()))
                    {
                        listContents.Add(f.HelpNodeNM.Trim());
                    }
                });
            }

            if (this.FinalAppSetting != null)
            {
                this.FinalAppSetting.ForEach(f =>
                {
                    listContents.Add(f.AppSettingKey.ToString());                    
                });
            }

            if (this.FinalImages != null)
            {
                this.FinalImages.ForEach(f =>
                {
                    if (!listContents.Contains(f.ContentId.Trim()))
                    {
                        listContents.Add(f.ContentId.Trim());
                    }
                });
            }

            if (this.FinalImageLanguages != null)
            {
                this.FinalImageLanguages.ForEach(f =>
                {
                    if (!listContents.Contains(f.ContentId.Trim()))
                    {
                        listContents.Add(f.ContentId.Trim());
                    }
                });
            }

            if (this.FinalServices != null)
            {
                this.FinalServices.ForEach(f =>
                {
                    if (!listContents.Contains(f.Name.Trim()))
                    {
                        listContents.Add(f.Name.Trim());
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