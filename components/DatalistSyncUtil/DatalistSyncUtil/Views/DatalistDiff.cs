//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using DatalistSyncUtil.Views;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace DatalistSyncUtil
{
    public partial class DatalistDiff : Form
    {
        public DatalistDiff()
        {
            this.InitializeComponent();
        }

        public DatalistDiff(Guid tenantID, string type, List<DataListMainModel> sourceList, List<DataListMainModel> targetList)
        {
            this.InitializeComponent();
            this.TargetConnectionString = ConfigurationManager.ConnectionStrings["TargetDataList"];
            this.LoadHelper = new TenantHelper(this.TargetConnectionString);
            this.TenantID = tenantID;
            this.DeltaType = type;
            this.SourceList = sourceList;
            this.TargetList = targetList;
            this.LoadModules();
            this.LoadDelta();
        }

        public TenantHelper LoadHelper { get; set; }

        public ConnectionStringSettings TargetConnectionString { get; set; }

        public Guid TenantID { get; set; }

        public string DeltaType { get; set; }

        public List<DataListMainModel> SourceList { get; set; }

        public List<DataListMainModel> TargetList { get; set; }

        public List<DataListMainModel> UpdateList { get; set; }

        public List<CodeItemModel> UpdateListItems { get; set; }

        public List<CodeItemModel> NewListItems { get; set; }

        public List<CodeItemModel> UpdateNewListItems { get; set; }

        public List<CodeItemModel> UpdatedItems { get; set; }

        public List<ItemLanguage> NewItemLanguages { get; set; }

        public List<ItemLanguage> UpdateNewItemLanguages { get; set; }

        public List<ItemLanguage> UpdatedTargetLanguages { get; set; }

        public List<CodeItemModel> UpdatedTargetItems { get; set; }

        public List<ItemLanguage> UpdateItemLanguages { get; set; }

        public List<ItemAttribute> NewItemAttribute { get; set; }

        public List<ItemAttribute> UpdateAttribute { get; set; }

        public List<ItemAttribute> UpdatedTargetAttribute { get; set; }

        public List<ItemAttribute> UpdateItemAttributes { get; set; }
        public List<DataListItemLink> NewLinkItem { get; set; }

        public List<DataListItemLink> UpdateListLinkItems { get; set; }

        public List<DataListItemLink> UpdateLink { get; set; }

        public List<DataListItemLink> UpdatedTargetLink { get; set; }

        private void LoadDelta()
        {
            switch (this.DeltaType.ToUpper())
            {
                case "ITEMS":
                    this.LoadDatalistDelta();
                    this.LoadDatalistItemsDelta();
                    this.diffTab.SelectedTab = this.DatalistTabPage;
                    break;

                default:
                    break;
            }
        }

        private void LoadModules()
        {
            List<TenantModuleModel> modules = this.LoadHelper.LoadModules();
            modules.Insert(
                   0, 
                   new TenantModuleModel()
                   {
                    ModuleName = "---All Modules---",
                    TenantModuleId = Guid.Empty,
                    TenantId = this.TenantID
                   });
            this.ModuleList.DataSource = modules.Where(w => w.TenantId == this.TenantID).GroupBy(i => i.ModuleName)
                  .Select(group =>
                        new
                        {
                            Key = group.Key,
                            Items = group.OrderByDescending(x => x.ModuleName)
                        })
                  .Select(g => g.Items.First()).OrderBy(o => o.ModuleName).ToList();
            this.ModuleList.DisplayMember = "ModuleName";
            this.ModuleList.SelectAll();
        }

        private void LoadDatalistDelta()
        {
            List<DataListMainModel> newDatalists = null;
            List<DataListMainModel> updatedDatalists = null;
            this.DataListView.AutoGenerateColumns = false;
            newDatalists = this.GetNewDatalist();
            updatedDatalists = this.UpdatedDatalist();
            newDatalists.AddRange(updatedDatalists);
            Guid tenantModuleId = (this.ModuleList.SelectedItem as TenantModuleModel).TenantModuleId;

            if (tenantModuleId != Guid.Empty)
            {
                newDatalists = newDatalists.Where(w => w.TenantModuleID == tenantModuleId).ToList();
            }

            this.DataListView.DataSource = new BindingList<DataListMainModel>(newDatalists);
        }

        private List<DataListMainModel> UpdatedDatalist()
        {
            List<DataListMainModel> updatedDatalists = new List<DataListMainModel>();
            DataListMainModel sourceDatalist = null;
            DataListMainModel targetDatalist = null;
            List<string> dataLists = this.SourceList.Select(c => c.ContentID).Intersect(this.TargetList.Select(c => c.ContentID)).ToList();
            dataLists.ForEach(f =>
            {
                sourceDatalist = this.SourceList.Find(e => e.ContentID == f);
                targetDatalist = this.TargetList.Find(e => e.ContentID == f);

                if (sourceDatalist.Description.Trim().ToLower() != targetDatalist.Description.Trim().ToLower() ||
                   sourceDatalist.IsEditable != targetDatalist.IsEditable)
                {
                    sourceDatalist.Status = "UPDATE";
                    sourceDatalist.ItemsCount = sourceDatalist.Items.Count;
                    sourceDatalist.ID = targetDatalist.ID;
                    updatedDatalists.Add(sourceDatalist);
                }
            });
            return updatedDatalists.OrderBy(o => o.ContentID).ToList();
        }

        private List<DataListMainModel> GetNewDatalist()
        {
            List<string> dataLists = this.SourceList.Select(c => c.ContentID).Except(this.TargetList.Select(c => c.ContentID)).ToList();
            List<DataListMainModel> newDatalists = this.SourceList.Where(c => dataLists.Contains(c.ContentID)).ToList();
            newDatalists.ForEach(i =>
            {
                i.Status = "NEW";
                i.ItemsCount = i.Items.Count;
            });
            return newDatalists.OrderBy(o => o.ContentID).ToList();
        }

        private void LoadDatalistItemsDelta()
        {
            List<CodeItemModel> newDatalistItemsFromNewList = null;
            List<CodeItemModel> newDatalistItemsFromUpdateList = null;
            List<CodeItemModel> newDatalistItems = new List<CodeItemModel>();
            this.NewItemsView.AutoGenerateColumns = false;
            newDatalistItemsFromNewList = this.GetNewDatalistItemsFromNewList();
            newDatalistItems.AddRange(newDatalistItemsFromNewList);
            newDatalistItemsFromUpdateList = this.GetNewDatalistItemsFromExistingList();
            newDatalistItems.AddRange(newDatalistItemsFromUpdateList);
            Guid tenantModuleId = (this.ModuleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string moduleName = (this.ModuleList.SelectedItem as TenantModuleModel).ModuleName;

            if (tenantModuleId != Guid.Empty)
            {
                newDatalistItems = newDatalistItems.Where(w => w.ContentID.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
            }

            this.NewListItems = newDatalistItems;
            this.NewItemsView.DataSource = new BindingList<CodeItemModel>(newDatalistItems);
            this.UpdatedDatalistItems();
            this.Add_Udate_Link();
            this.UpdateLanguages();
            this.UpdateDataListAttributes();
        }

        private void Add_Udate_Link()
        {
            List<DataListItemLink> newDatalistItemsFromUpdateList = new List<DataListItemLink>();
            List<DataListItemLink> newDatalistItemsLink = new List<DataListItemLink>();
            CodeItemModel dataListItemsLink = new CodeItemModel();
            List<CodeItemModel> sourceDatalistItems = null;
            List<CodeItemModel> targetDatalistItems = null;
            List<CodeItemModel> dataListItems = null;
            List<DataListItemLink> SourcenewLinkItem = new List<DataListItemLink>();
            List<DataListItemLink> TargetnewLinkItem = new List<DataListItemLink>();
            List<DataListItemLink> updatedLink = new List<DataListItemLink>();
            List<DataListItemLink> updatedTargetLink = new List<DataListItemLink>();
            List<string> dataLists = this.SourceList.Select(c => c.ContentID).Intersect(this.TargetList.Select(c => c.ContentID)).ToList();
            dataLists.ForEach(f =>
            {
                sourceDatalistItems = this.SourceList.Find(e => e.ContentID == f).Items;
                targetDatalistItems = this.TargetList.Find(e => e.ContentID == f).Items;
                if (sourceDatalistItems.Count() == targetDatalistItems.Count())
                {
                    newDatalistItemsFromUpdateList = this.NewLinkItemList(sourceDatalistItems, targetDatalistItems);
                    newDatalistItemsLink.AddRange(newDatalistItemsFromUpdateList);
                }
                

            });
            for (int i = 0; i < sourceDatalistItems.Count(); i++)
            {
                SourcenewLinkItem = sourceDatalistItems[i].DataListLink;
                TargetnewLinkItem = targetDatalistItems[i].DataListLink;
                if (TargetnewLinkItem.Count() == SourcenewLinkItem.Count())
                {
                    KeyValuePair < List<DataListItemLink>, List < DataListItemLink >> result= this.LoadUpdateLinkItem(sourceDatalistItems, targetDatalistItems);
                    updatedLink.AddRange(result.Key);
                    updatedTargetLink.AddRange(result.Value);
                }
            }


            Guid tenantModuleId = (this.ModuleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string moduleName = (this.ModuleList.SelectedItem as TenantModuleModel).ModuleName;

            if (tenantModuleId != Guid.Empty)
            {
                newDatalistItemsLink = newDatalistItemsLink.Where(w => w.ContentID.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
            }
            // newDatalistItemsLink = newDatalistItemsLink.Select(x => new DataListItemLink() {ParentCode = x.ParentCode.ToString(), ParentDataList=x.ParentDataList.ToString(),ChildDataList=x.ChildDataList.ToString(), Description=x.Description.ToString()}).OrderBy(o=>o.Code).ToList();
            newDatalistItemsLink = newDatalistItemsLink.OrderBy(o => o.Code).ToList();
            this.NewLinkItem = newDatalistItemsLink;
            this.LinkgridView.AutoGenerateColumns = false;
            this.LinkgridView.DataSource = new BindingList<DataListItemLink>(newDatalistItemsLink);

            this.SourceLinkView.AutoGenerateColumns = false;
            this.TargetLinkView.AutoGenerateColumns = false;
            if (tenantModuleId != Guid.Empty)
            {
                updatedLink = updatedLink.Where(w => w.ContentID.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
                updatedTargetLink = updatedTargetLink.Where(w => w.ContentID.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
            }

            this.UpdateLink = updatedLink.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList();
            this.UpdatedTargetLink = updatedTargetLink.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList();
            this.SourceLinkView.DataSource = new BindingList<DataListItemLink>(updatedLink.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList());
            this.TargetLinkView.DataSource = new BindingList<DataListItemLink>(updatedTargetLink.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList());





        }
        private List<CodeItemModel> GetNewDatalistItemsFromExistingList()
        {
            List<CodeItemModel> newDatalistItemsFromUpdateList = new List<CodeItemModel>();
            List<CodeItemModel> sourceDatalistItems = null;
            List<CodeItemModel> targetDatalistItems = null;
            List<CodeItemModel> dataListItems = null;

            List<string> dataLists = this.SourceList.Select(c => c.ContentID).Intersect(this.TargetList.Select(c => c.ContentID)).ToList();
            dataLists.ForEach(f =>
            {
                sourceDatalistItems = this.SourceList.Find(e => e.ContentID == f).Items;
                targetDatalistItems = this.TargetList.Find(e => e.ContentID == f).Items;
                
               dataListItems = sourceDatalistItems.Where(b => !targetDatalistItems.Any(a => a.ContentID == b.ContentID && a.Code == b.Code && a.IsActive == b.IsActive)).ToList();

                if (dataListItems != null && dataListItems.Count > 0)
                {
                    dataListItems.ForEach(t =>
                    {
                        t.Status = "DATALIST_NEW";

                        newDatalistItemsFromUpdateList.Add(t);
                    });
                    
                }
            });
         
             return newDatalistItemsFromUpdateList.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList();
        }

        private KeyValuePair<List<DataListItemLink>, List<DataListItemLink>> LoadUpdateLinkItem(List<CodeItemModel> sourceDatalistItems, List<CodeItemModel> targetDatalistItems)
        {
            List<DataListItemLink> updatedLink = new List<DataListItemLink>();
            List<DataListItemLink> updatedTargetLink = new List<DataListItemLink>();
            List<DataListItemLink> sourceLink = null;
            List<DataListItemLink> targetLink = null;
            DataListItemLink targetItemLink = null;
            // sourceDatalistItems = sourceDatalistItems.Where(t=>t.IsActive==true).ToList();
            //  targetDatalistItems = targetDatalistItems.Where(t => t.IsActive == true).ToList();
            for (int i = 0; i < sourceDatalistItems.Count(); i++)
            {
                sourceLink = sourceDatalistItems[i].DataListLink;
                targetLink = targetDatalistItems[i].DataListLink;
                if (targetLink.Count() == sourceLink.Count())
                {
                    sourceLink.ForEach(t =>
                    {
                        targetItemLink = targetLink.Find(u => t.ParentID == u.ParentID && t.ChildID == u.ChildID);

                        if (targetItemLink != null)
                        {
                            t.Status = "Update";
                            updatedLink.Add(t);
                            updatedTargetLink.Add(targetItemLink);

                        }
                    });


                }
            }

           return new KeyValuePair<List<DataListItemLink>, List<DataListItemLink>>((updatedLink.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList()), updatedTargetLink.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList());
        }
        private List<DataListItemLink> NewLinkItemList(List<CodeItemModel> sourceDatalistItems, List<CodeItemModel> targetDatalistItems)
        {
            List<DataListItemLink> SourcenewLinkItem = new List<DataListItemLink>();
            List<DataListItemLink> TargetnewLinkItem = new List<DataListItemLink>();
            List<DataListItemLink> LinkResult = new List<DataListItemLink>();
            List<DataListItemLink> LinkItem = new List<DataListItemLink>();
            sourceDatalistItems = sourceDatalistItems.Where(t => t.IsActive == true).ToList();
            targetDatalistItems = targetDatalistItems.Where(t => t.IsActive == true).ToList();
            if (sourceDatalistItems.Count() == targetDatalistItems.Count())
            { 
                for (int i = 0; i < sourceDatalistItems.Count(); i++)
                {
                    SourcenewLinkItem = sourceDatalistItems[i].DataListLink;
                    TargetnewLinkItem = targetDatalistItems[i].DataListLink;
                    if (TargetnewLinkItem.Count() < SourcenewLinkItem.Count())
                    {
                        LinkResult = SourcenewLinkItem.Where(b => !TargetnewLinkItem.Any(a => a.ParentID == b.ParentID && a.ChildID == b.ChildID && a.IsActive == b.IsActive)).ToList();

                        if (LinkResult != null && LinkResult.Count > 0)
                        {
                            LinkResult.ForEach(h =>
                            {
                                h.Status = "DATALIST_NEW";
                                LinkItem.Add(h);
                            });
                        }


                    }
                }
        }
            else
            {

              //  MessageBox.Show("Insert Data List");
            }
           
            return LinkItem.OrderBy(o => o.ParentID).ThenBy(t => t.ChildID).ToList();
        }

        private void UpdatedDatalistItems()
        {
            List<CodeItemModel> updatedDatalistItems = new List<CodeItemModel>();
            List<CodeItemModel> updatedTargetDatalistItems = new List<CodeItemModel>();
            List<CodeItemModel> sourceDatalistItems = null;
            List<CodeItemModel> targetDatalistItems = null;
            CodeItemModel targetItem = null;
            bool itemChanged = false;

            List<string> dataLists = this.SourceList.Select(c => c.ContentID).Intersect(this.TargetList.Select(c => c.ContentID)).ToList();
            dataLists.ForEach(f =>
            {
                sourceDatalistItems = this.SourceList.Find(e => e.ContentID == f).Items;
                targetDatalistItems = this.TargetList.Find(e => e.ContentID == f).Items;

                sourceDatalistItems.ForEach(t =>
                {
                    itemChanged = false;
                    targetItem = targetDatalistItems.Find(u => t.ContentID == u.ContentID && t.Code == u.Code && t.IsActive == u.IsActive);

                    if (targetItem != null)
                    {
                        itemChanged = this.CheckUpdateItemChanged(ref t, ref targetItem);

                        if (itemChanged)
                        {
                            updatedDatalistItems.Add(t);
                            updatedTargetDatalistItems.Add(targetItem);
                        }
                    }
                });
            });

            this.UpdateSourceItemView.AutoGenerateColumns = false;
            this.UpdateTargetItemView.AutoGenerateColumns = false;

            Guid tenantModuleId = (this.ModuleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string moduleName = (this.ModuleList.SelectedItem as TenantModuleModel).ModuleName;

            if (tenantModuleId != Guid.Empty)
            {
                updatedDatalistItems = updatedDatalistItems.Where(w => w.ContentID.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
                updatedTargetDatalistItems = updatedTargetDatalistItems.Where(w => w.ContentID.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
            }

            this.UpdatedItems = updatedDatalistItems.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList();
            this.UpdatedTargetItems = updatedTargetDatalistItems.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList();
            this.UpdateSourceItemView.DataSource = new BindingList<CodeItemModel>(updatedDatalistItems.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList());
            this.UpdateTargetItemView.DataSource = new BindingList<CodeItemModel>(updatedTargetDatalistItems.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList());
        }

        private void UpdateLanguages()
        {
            this.LoadNewLanguages();
        }

        private void UpdateDataListAttributes()
        {
            this.LoadNewDataListAttributes();
        }

        private void LoadNewLanguages()
        {
            List<ItemLanguage> newDatalistItemLanguages = new List<ItemLanguage>();
            List<ItemLanguage> newDatalistItemLanguagesExisting = new List<ItemLanguage>();
            List<string> dataLists = null;

            dataLists = this.SourceList.Select(c => c.ContentID).Except(this.TargetList.Select(c => c.ContentID)).ToList();
            List<DataListMainModel> newDatalists = this.SourceList.Where(c => dataLists.Contains(c.ContentID)).ToList();

            newDatalists.ForEach(i =>
            {
                i.Items.ForEach(f =>
                {
                    f.LanguageList.ForEach(l =>
                    {
                        l.Status = "DATALIST_NEW";
                        l.Code = f.Code;
                        l.ContentID = f.ContentID;
                        newDatalistItemLanguages.Add(l);
                    });
                });
            });

            newDatalistItemLanguagesExisting = this.GetNewLanguagesFromNewItems();
            newDatalistItemLanguages.AddRange(newDatalistItemLanguagesExisting);
            newDatalistItemLanguagesExisting = this.GetNewLanguagesFromExistingItems();
            newDatalistItemLanguages.AddRange(newDatalistItemLanguagesExisting);

            Guid tenantModuleId = (this.ModuleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string moduleName = (this.ModuleList.SelectedItem as TenantModuleModel).ModuleName;

            if (tenantModuleId != Guid.Empty)
            {
                newDatalistItemLanguages = newDatalistItemLanguages.Where(w => w.ContentID.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
            }

            this.NewItemLanguages = newDatalistItemLanguages;
            this.NewLangView.DataSource = new BindingList<ItemLanguage>(newDatalistItemLanguages);
            this.LoadUpdateLanguagesFromExistingItems();
        }

        private List<ItemLanguage> GetNewLanguagesFromNewItems()
        {
            List<ItemLanguage> newDatalistItemLanguages = new List<ItemLanguage>();
            List<CodeItemModel> sourceDatalistItems = null;
            List<CodeItemModel> targetDatalistItems = null;
            List<CodeItemModel> dataListItems = null;

            List<string> dataLists = this.SourceList.Select(c => c.ContentID).Intersect(this.TargetList.Select(c => c.ContentID)).ToList();
            dataLists.ForEach(f =>
            {
                sourceDatalistItems = this.SourceList.Find(e => e.ContentID == f).Items;
                targetDatalistItems = this.TargetList.Find(e => e.ContentID == f).Items;

                dataListItems = sourceDatalistItems.Where(b => !targetDatalistItems.Any(a => a.ContentID == b.ContentID && a.Code == b.Code && a.IsActive == b.IsActive)).ToList();

                if (dataListItems != null && dataListItems.Count > 0)
                {
                    dataListItems.ForEach(l =>
                    {
                        l.LanguageList.ForEach(h =>
                        {
                            h.Status = "ITEM_NEW";
                            h.Code = l.Code;
                            h.ContentID = l.ContentID;
                            newDatalistItemLanguages.Add(h);
                        });
                    });
                }
            });

            return newDatalistItemLanguages.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ThenBy(t => t.Code).ToList();
        }

        private List<ItemLanguage> GetNewLanguagesFromExistingItems()
        {
            List<ItemLanguage> newDatalistItemLanguages = new List<ItemLanguage>();
            List<CodeItemModel> sourceDatalistItems = null;
            List<CodeItemModel> targetDatalistItems = null;
            List<CodeItemModel> dataListItems = null;
            List<ItemLanguage> targetDatalistItemLanguages = null;
            List<ItemLanguage> finalDatalistItemLanguages = null;

            List<string> dataLists = this.SourceList.Select(c => c.ContentID).Intersect(this.TargetList.Select(c => c.ContentID)).ToList();
            dataLists.ForEach(f =>
            {
                sourceDatalistItems = this.SourceList.Find(e => e.ContentID == f).Items;
                targetDatalistItems = this.TargetList.Find(e => e.ContentID == f).Items;

                dataListItems = sourceDatalistItems.Where(b => targetDatalistItems.Any(a => a.ContentID == b.ContentID && a.Code == b.Code && a.IsActive == b.IsActive)).ToList();

                if (dataListItems != null && dataListItems.Count > 0)
                {
                    dataListItems.ForEach(l =>
                    {
                        targetDatalistItemLanguages = targetDatalistItems.Find(i => i.ContentID == l.ContentID && i.Code == l.Code && i.IsActive == l.IsActive).LanguageList;
                        finalDatalistItemLanguages = l.LanguageList.Where(b => !targetDatalistItemLanguages.Any(a => a.LocaleID == b.LocaleID)).ToList();
                        finalDatalistItemLanguages.ForEach(h =>
                        {
                            h.Status = "LOCALE_NEW";
                            h.Code = l.Code;
                            h.ContentID = l.ContentID;
                            newDatalistItemLanguages.Add(h);
                        });
                    });
                }
            });

            return newDatalistItemLanguages.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList();
        }

        private void LoadUpdateLanguagesFromExistingItems()
        {
            List<ItemLanguage> updateSourceDatalistItemLanguages = new List<ItemLanguage>();
            List<ItemLanguage> updateTargetDatalistItemLanguages = new List<ItemLanguage>();
            List<CodeItemModel> sourceDatalistItems = null;
            List<CodeItemModel> targetDatalistItems = null;
            List<CodeItemModel> dataListItems = null;
            List<ItemLanguage> targetDatalistItemLanguages = null;
            ItemLanguage targetDatalistItemLanguage = null;
            List<ItemLanguage> finalDatalistItemLanguages = null;
            bool isChanged = false;

            List<string> dataLists = this.SourceList.Select(c => c.ContentID).Intersect(this.TargetList.Select(c => c.ContentID)).ToList();
            dataLists.ForEach(f =>
            {
                sourceDatalistItems = this.SourceList.Find(e => e.ContentID == f).Items;
                targetDatalistItems = this.TargetList.Find(e => e.ContentID == f).Items;

                dataListItems = sourceDatalistItems.Where(b => targetDatalistItems.Any(a => a.ContentID == b.ContentID && a.Code == b.Code && a.IsActive == b.IsActive)).ToList();

                if (dataListItems != null && dataListItems.Count > 0)
                {
                    dataListItems.ForEach(l =>
                    {
                        targetDatalistItemLanguages = targetDatalistItems.Find(i => i.ContentID == l.ContentID && i.Code == l.Code && i.IsActive == l.IsActive).LanguageList;
                        finalDatalistItemLanguages = l.LanguageList.Where(b => targetDatalistItemLanguages.Any(a => a.LocaleID == b.LocaleID)).ToList();
                        finalDatalistItemLanguages.ForEach(h =>
                        {
                            isChanged = false;
                            targetDatalistItemLanguage = targetDatalistItemLanguages.Find(n => n.LocaleID == h.LocaleID);
                            if (targetDatalistItemLanguage.Description != null && h.Description != null && targetDatalistItemLanguage.Description.Trim() != h.Description.Trim())
                            {
                                isChanged = true;
                                h.DescriptionModified = true;
                            }

                            if (targetDatalistItemLanguage.LongDescription != null && h.LongDescription != null && targetDatalistItemLanguage.LongDescription.Trim() != h.LongDescription.Trim())
                            {
                                isChanged = true;
                                h.LongDescriptionModified = true;
                            }

                            if (isChanged)
                            {
                                h.Status = "UPDATE";
                                h.Code = l.Code;
                                h.ContentID = l.ContentID;
                                targetDatalistItemLanguage.ContentID = l.ContentID;
                                updateSourceDatalistItemLanguages.Add(h);
                                updateTargetDatalistItemLanguages.Add(targetDatalistItemLanguage);
                            }
                        });
                    });
                }
            });

            this.SourceUpdateLangView.AutoGenerateColumns = this.TargetUpdateLangView.AutoGenerateColumns = false;
            Guid tenantModuleId = (this.ModuleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string moduleName = (this.ModuleList.SelectedItem as TenantModuleModel).ModuleName;

            if (tenantModuleId != Guid.Empty)
            {
                updateSourceDatalistItemLanguages = updateSourceDatalistItemLanguages.Where(w => w.ContentID.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
                updateTargetDatalistItemLanguages = updateTargetDatalistItemLanguages.Where(w => w.ContentID.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
            }

            this.UpdateNewItemLanguages = updateSourceDatalistItemLanguages;
            this.UpdatedTargetLanguages = updateTargetDatalistItemLanguages;
            this.SourceUpdateLangView.DataSource = new BindingList<ItemLanguage>(updateSourceDatalistItemLanguages.OrderBy(o => o.ContentID).ToList());
            this.TargetUpdateLangView.DataSource = new BindingList<ItemLanguage>(updateTargetDatalistItemLanguages.OrderBy(o => o.ContentID).ToList());
        }

        private void LoadNewDataListAttributes()
        {
            List<ItemAttribute> newDatalistAttributes = new List<ItemAttribute>();
            List<ItemAttribute> newDatalistAttributesExisting = new List<ItemAttribute>();
            List<string> dataLists = null;

            dataLists = this.SourceList.Select(c => c.ContentID).Except(this.TargetList.Select(c => c.ContentID)).ToList();
            List<DataListMainModel> newDatalists = this.SourceList.Where(c => dataLists.Contains(c.ContentID)).ToList();

            newDatalists.ForEach(i =>
            {
                i.DataListAttributes.ForEach(f =>
                {
                    f.Status = "DATALIST_NEW";
                    newDatalistAttributes.Add(f);
                });
            });

            newDatalistAttributesExisting = this.GetNewAttributesFromExistingItems();
            newDatalistAttributes.AddRange(newDatalistAttributesExisting);

            Guid tenantModuleId = (this.ModuleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string moduleName = (this.ModuleList.SelectedItem as TenantModuleModel).ModuleName;

            if (tenantModuleId != Guid.Empty)
            {
                newDatalistAttributes = newDatalistAttributes.Where(w => w.ContentID.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
            }

            newDatalistAttributes = newDatalistAttributes.OrderBy(o => o.ParentContentId).ThenBy(t => t.Code).ToList();
            this.NewItemAttribute = newDatalistAttributes;

            this.NewAttributesView.DataSource = new BindingList<ItemAttribute>(newDatalistAttributes);
            this.LoadUpdateAttributes();
        }

        private void LoadUpdateAttributes()
        {
            List<ItemAttribute> updatedAttributes = new List<ItemAttribute>();
            List<ItemAttribute> updatedTargetAttributes = new List<ItemAttribute>();
            List<ItemAttribute> sourceAttributes = null;
            List<ItemAttribute> targetAttributes = null;
            ItemAttribute targetItem = null;

            List<string> dataLists = this.SourceList.Select(c => c.ContentID).Intersect(this.TargetList.Select(c => c.ContentID)).ToList();
            dataLists.ForEach(f =>
            {
                sourceAttributes = this.SourceList.Find(e => e.ContentID == f).DataListAttributes;
                targetAttributes = this.TargetList.Find(e => e.ContentID == f).DataListAttributes;

                sourceAttributes.ForEach(t =>
                {
                    targetItem = targetAttributes.Find(u => t.ContentID == u.ContentID && t.Code == u.Code);

                    if (targetItem != null)
                    {
                        if (t.IsActive != targetItem.IsActive)
                        {
                           // t.ID=ta
                            updatedAttributes.Add(t);
                            updatedTargetAttributes.Add(targetItem);
                        }
                    }
                });
            });

            this.SourceUpdateAttributeView.AutoGenerateColumns = false;
            this.TargetUpdateAttributeView.AutoGenerateColumns = false;

            Guid tenantModuleId = (this.ModuleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string moduleName = (this.ModuleList.SelectedItem as TenantModuleModel).ModuleName;

            if (tenantModuleId != Guid.Empty)
            {
                updatedAttributes = updatedAttributes.Where(w => w.ContentID.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
                updatedTargetAttributes = updatedTargetAttributes.Where(w => w.ContentID.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
            }

            this.UpdateAttribute = updatedAttributes.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList();
            this.UpdatedTargetAttribute = updatedTargetAttributes.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList();
            this.SourceUpdateAttributeView.DataSource = new BindingList<ItemAttribute>(updatedAttributes.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList());
            this.TargetUpdateAttributeView.DataSource = new BindingList<ItemAttribute>(updatedTargetAttributes.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList());
        }

        private List<ItemAttribute> GetNewAttributesFromExistingItems()
        {
            List<ItemAttribute> newDataListAttribute = new List<ItemAttribute>();
            List<ItemAttribute> sourceDatalistItems = new List<ItemAttribute>();
            List<ItemAttribute> targetDatalistItems = new List<ItemAttribute>();
            List<ItemAttribute> dataListAttributes = new List<ItemAttribute>();

            List<string> dataLists = this.SourceList.Select(c => c.ContentID).Intersect(this.TargetList.Select(c => c.ContentID)).ToList();
            dataLists.ForEach(f =>
            {
                sourceDatalistItems = this.SourceList.Find(e => e.ContentID == f).DataListAttributes;
                targetDatalistItems = this.TargetList.Find(e => e.ContentID == f).DataListAttributes;

                dataListAttributes = sourceDatalistItems.Where(b => !targetDatalistItems.Any(a => a.ContentID == b.ContentID && a.Code == b.Code)).ToList();

                if (dataListAttributes != null && dataListAttributes.Count > 0)
                {
                    dataListAttributes.ForEach(h =>
                    {
                        h.Status = "DATALIST_NEW";
                        newDataListAttribute.Add(h);
                    });
                }
            });

            return newDataListAttribute.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList();
        }

        private bool CheckUpdateItemChanged(ref CodeItemModel t, ref CodeItemModel targetItem)
        {
            bool itemChanged = false;

            if (t.OrderIndex != targetItem.OrderIndex)
            {
                itemChanged = true;
                t.OrderIndexModified = targetItem.OrderIndexModified = true;
            }

            if (t.IsEditable != targetItem.IsEditable)
            {
                itemChanged = true;
                t.IsEditableModified = targetItem.IsEditableModified = true;
            }

            if ((!t.ContentID.Contains("Message")) && (!t.ContentID.Contains("Label") && (!t.ContentID.Contains("Msg"))))
            {
                if (t.EffectiveEndDate.Value.ToShortDateString() != targetItem.EffectiveEndDate.Value.ToShortDateString())
                {
                    itemChanged = true;
                    t.EffectiveEndDateModified = targetItem.EffectiveEndDateModified = true;
                }

                if (t.EffectiveStartDate.Value.ToShortDateString() != targetItem.EffectiveStartDate.Value.ToShortDateString())
                {
                    itemChanged = true;
                    t.EffectiveStartDateModified = targetItem.EffectiveStartDateModified = true;
                }
            }

            return itemChanged;
        }

        private List<CodeItemModel> GetNewDatalistItemsFromNewList()
        {
            List<CodeItemModel> newDatalistItems = new List<CodeItemModel>();

            List<string> dataLists = this.SourceList.Select(c => c.ContentID).Except(this.TargetList.Select(c => c.ContentID)).ToList();
            List<DataListMainModel> newDatalists = this.SourceList.Where(c => dataLists.Contains(c.ContentID)).ToList();

            newDatalists.ForEach(i =>
            {
                i.Items.ForEach(f => f.Status = "NEW");
                newDatalistItems.AddRange(i.Items);
            });
            return newDatalistItems.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList();
        }

        private void LoadDatalistItemLanguagesDelta()
        {
            throw new NotImplementedException();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateSourceItemView_Scroll(object sender, ScrollEventArgs e)
        {
            this.UpdateTargetItemView.FirstDisplayedScrollingRowIndex = this.UpdateSourceItemView.FirstDisplayedScrollingRowIndex;
        }

        private void UpdateTargetItemView_Scroll(object sender, ScrollEventArgs e)
        {
            this.UpdateSourceItemView.FirstDisplayedScrollingRowIndex = this.UpdateTargetItemView.FirstDisplayedScrollingRowIndex;
        }

        private void NewItemsView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in this.NewItemsView.Rows)
            {
                string rowStatus = row.Cells[12].Value != null ? row.Cells[12].Value.ToString() : string.Empty;

                if (rowStatus == "NEW")
                {
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void UpdateSourceItemView_RowAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in this.UpdateSourceItemView.Rows)
            {
                bool orderIndexModified = row.Cells[6].Value != null ? Convert.ToBoolean(row.Cells[6].Value) : false;

                if (orderIndexModified == true)
                {
                    row.Cells[3].Style.BackColor = Color.LightBlue;
                    row.Cells[3].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[3].Style.BackColor = Color.White;
                    row.Cells[3].Style.ForeColor = Color.Black;
                }

                bool editableModified = row.Cells[9].Value != null ? Convert.ToBoolean(row.Cells[9].Value) : false;

                if (editableModified == true)
                {
                    row.Cells[11].Style.BackColor = Color.LightBlue;
                    row.Cells[11].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[11].Style.BackColor = Color.White;
                    row.Cells[11].Style.ForeColor = Color.Black;
                }

                bool startDateModified = row.Cells[7].Value != null ? Convert.ToBoolean(row.Cells[7].Value) : false;

                if (startDateModified == true)
                {
                    row.Cells[4].Style.BackColor = Color.LightBlue;
                    row.Cells[4].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[4].Style.BackColor = Color.White;
                    row.Cells[4].Style.ForeColor = Color.Black;
                }

                bool endDateModified = row.Cells[8].Value != null ? Convert.ToBoolean(row.Cells[8].Value) : false;

                if (endDateModified == true)
                {
                    row.Cells[5].Style.BackColor = Color.LightBlue;
                    row.Cells[5].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[5].Style.BackColor = Color.White;
                    row.Cells[5].Style.ForeColor = Color.Black;
                }
            }
        }

        private void SourceUpdateLangView_Scroll(object sender, ScrollEventArgs e)
        {
            this.TargetUpdateLangView.FirstDisplayedScrollingRowIndex = this.SourceUpdateLangView.FirstDisplayedScrollingRowIndex;
        }

        private void SourceUpdateLangView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in this.SourceUpdateLangView.Rows)
            {
                bool descModified = row.Cells[5].Value != null ? Convert.ToBoolean(row.Cells[5].Value) : false;

                if (descModified)
                {
                    row.Cells[4].Style.BackColor = Color.LightBlue;
                    row.Cells[4].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[4].Style.BackColor = Color.White;
                    row.Cells[4].Style.ForeColor = Color.Black;
                }

                bool longDescModified = row.Cells[6].Value != null ? Convert.ToBoolean(row.Cells[6].Value) : false;

                if (longDescModified)
                {
                    row.Cells[7].Style.BackColor = Color.LightBlue;
                    row.Cells[7].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[7].Style.BackColor = Color.White;
                    row.Cells[7].Style.ForeColor = Color.Black;
                }
            }
        }

        private void TargetUpdateLangView_Scroll(object sender, ScrollEventArgs e)
        {
            this.SourceUpdateLangView.FirstDisplayedScrollingRowIndex = this.TargetUpdateLangView.FirstDisplayedScrollingRowIndex;
        }

        private void DatalistSelectAllChkBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.DataListView.Rows)
            {
                row.Cells[0].Value = this.DatalistSelectAllChkBox.Checked;
            }
        }

        private void UpdateLangSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.SourceUpdateLangView.Rows)
            {
                row.Cells[0].Value = this.UpdateLangSelectAllCB.Checked;
            }
        }

        private void NewLangSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.NewLangView.Rows)
            {
                row.Cells[0].Value = this.NewLangSelectAllCB.Checked;
            }
        }

        private void UpdateItemsSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.UpdateSourceItemView.Rows)
            {
                row.Cells[0].Value = this.UpdateItemsSelectAllCB.Checked;
            }
        }

        private void NewItemsSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.NewItemsView.Rows)
            {
                row.Cells[0].Value = this.NewItemsSelectAllCB.Checked;
            }
        }

        private void NewLangView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in this.NewLangView.Rows)
            {
                string rowStatus = row.Cells[9].Value != null ? row.Cells[9].Value.ToString() : string.Empty;

                if (rowStatus == "DATALIST_NEW")
                {
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (rowStatus == "ITEM_NEW")
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void btnListUpdate_Click(object sender, EventArgs e)
        {
            bool selected = false;
            this.UpdateList = new List<DataListMainModel>();
            foreach (DataGridViewRow row in this.DataListView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells["Select"].Value);

                if (selected)
                {
                    this.UpdateList.Add(row.DataBoundItem as DataListMainModel);
                }
            }
        }

        private void btnUpdateItems_Click(object sender, EventArgs e)
        {
            bool selected = false;
            this.UpdateListItems = new List<CodeItemModel>();

            foreach (DataGridViewRow row in this.NewItemsView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateListItems.Add(row.DataBoundItem as CodeItemModel);
                }
            }

            foreach (DataGridViewRow row in this.UpdateSourceItemView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateListItems.Add(row.DataBoundItem as CodeItemModel);
                }
            }
        }

        private void btnUpdateLanguages_Click(object sender, EventArgs e)
        {
            bool selected = false;
            this.UpdateItemLanguages = new List<ItemLanguage>();

            foreach (DataGridViewRow row in this.NewLangView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateItemLanguages.Add(row.DataBoundItem as ItemLanguage);
                }
            }

            foreach (DataGridViewRow row in this.SourceUpdateLangView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateItemLanguages.Add(row.DataBoundItem as ItemLanguage);
                }
            }
        }

        private void PreviewUpdate_Click(object sender, EventArgs e)
        {
            PreviewPage previewPage = new PreviewPage(this.UpdateList, this.UpdateListItems, this.UpdateItemLanguages, this.UpdateAttribute,this.UpdateListLinkItems);
            previewPage.ShowDialog();
        }

        private void ModuleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadDatalistDelta();
            this.LoadDatalistItemsDelta();
        }

        private void ItemRolesCB_CheckedChanged(object sender, EventArgs e)
        {
            this.FilterDataList();
        }

        private void ItemFunctionsCB_CheckedChanged(object sender, EventArgs e)
        {
            this.FilterDataList();
        }

        private void ItemRightsCB_CheckedChanged(object sender, EventArgs e)
        {
            this.FilterDataList();
        }

        private void FilterDataList()
        {
            List<CodeItemModel> filteredNewDatalistItems = new List<CodeItemModel>();
            List<CodeItemModel> filteredUpdatedNewDatalistItems = new List<CodeItemModel>();
            List<CodeItemModel> filteredUpdatedDatalistItems = new List<CodeItemModel>();
            string rolesContentID = "Core.SecurityRoles";
            string functionsContentID = "Core.SecurityFunctions";
            string rightsContentID = "Core.SecurityRights";
            string msgContentID = ".Msg";
            string labelContentID = ".Label";
            string datalistContentID = ".DataList";
            bool isChecked = false;

            if (this.ItemRolesCB.Checked)
            {
                isChecked = true;
                this.GetFilteredDataListItems(ref filteredNewDatalistItems, ref filteredUpdatedNewDatalistItems, ref filteredUpdatedDatalistItems, rolesContentID);
            }

            if (this.ItemFunctionsCB.Checked)
            {
                isChecked = true;
                this.GetFilteredDataListItems(ref filteredNewDatalistItems, ref filteredUpdatedNewDatalistItems, ref filteredUpdatedDatalistItems, functionsContentID);
            }

            if (this.ItemRightsCB.Checked)
            {
                isChecked = true;
                this.GetFilteredDataListItems(ref filteredNewDatalistItems, ref filteredUpdatedNewDatalistItems, ref filteredUpdatedDatalistItems, rightsContentID);
            }

            if (this.ItemMessagesCB.Checked)
            {
                isChecked = true;
                this.GetFilteredDataListItems(ref filteredNewDatalistItems, ref filteredUpdatedNewDatalistItems, ref filteredUpdatedDatalistItems, msgContentID);
            }

            if (this.ItemLabelsCB.Checked)
            {
                isChecked = true;
                this.GetFilteredDataListItems(ref filteredNewDatalistItems, ref filteredUpdatedNewDatalistItems, ref filteredUpdatedDatalistItems, labelContentID);
            }

            if (this.ItemDatalistCB.Checked)
            {
                isChecked = true;
                this.GetFilteredDataListItems(ref filteredNewDatalistItems, ref filteredUpdatedNewDatalistItems, ref filteredUpdatedDatalistItems, datalistContentID);
            }

            if (isChecked)
            {
                this.NewItemsView.DataSource = new BindingList<CodeItemModel>(filteredNewDatalistItems);
                this.UpdateSourceItemView.DataSource = new BindingList<CodeItemModel>(filteredUpdatedNewDatalistItems.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList());
                this.UpdateTargetItemView.DataSource = new BindingList<CodeItemModel>(filteredUpdatedDatalistItems.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList());
            }
            else
            {
                this.NewItemsView.DataSource = new BindingList<CodeItemModel>(this.NewListItems);
                this.UpdateSourceItemView.DataSource = new BindingList<CodeItemModel>(this.UpdatedItems.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList());
                this.UpdateTargetItemView.DataSource = new BindingList<CodeItemModel>(this.UpdatedTargetItems.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList());
            }
        }

        private void GetFilteredDataListItems(ref List<CodeItemModel> filteredNewDatalistItems, ref List<CodeItemModel> filteredUpdatedNewDatalistItems, ref List<CodeItemModel> filteredUpdatedDatalistItems, string contentID)
        {
            List<CodeItemModel> items = new List<CodeItemModel>();

            items = this.NewListItems.Where(f => f.ContentID.Contains(contentID)).ToList();
            if (items != null && items.Count > 0)
            {
                filteredNewDatalistItems.AddRange(items);
            }

            items = this.UpdatedItems.Where(f => f.ContentID.Contains(contentID)).ToList();
            if (items != null && items.Count > 0)
            {
                filteredUpdatedNewDatalistItems.AddRange(items);
            }

            items = this.UpdatedTargetItems.Where(f => f.ContentID.Contains(contentID)).ToList();
            if (items != null && items.Count > 0)
            {
                filteredUpdatedDatalistItems.AddRange(items);
            }
        }

        private void ItemCB_CheckedChanged(object sender, EventArgs e)
        {
            this.FilterDataList();
        }

        private void LangCB_CheckedChanged(object sender, EventArgs e)
        {
            this.FilterDataListLanguages();
        }

        private void FilterDataListLanguages()
        {
            List<ItemLanguage> filteredNewItemLanguages = new List<ItemLanguage>();
            List<ItemLanguage> filteredUpdatedNewItemLanguages = new List<ItemLanguage>();
            List<ItemLanguage> filteredUpdatedItemLanguages = new List<ItemLanguage>();
            string rolesContentID = "Core.SecurityRoles";
            string functionsContentID = "Core.SecurityFunctions";
            string rightsContentID = "Core.SecurityRights";
            string msgContentID = ".Msg";
            string labelContentID = ".Label";
            string datalistContentID = ".DataList";
            bool isChecked = false;

            if (this.LangRolesCB.Checked)
            {
                isChecked = true;
                this.GetFilteredItemLanguages(ref filteredNewItemLanguages, ref filteredUpdatedNewItemLanguages, ref filteredUpdatedItemLanguages, rolesContentID);
            }

            if (this.LangFunctionsCB.Checked)
            {
                isChecked = true;
                this.GetFilteredItemLanguages(ref filteredNewItemLanguages, ref filteredUpdatedNewItemLanguages, ref filteredUpdatedItemLanguages, functionsContentID);
            }

            if (this.LangRightsCB.Checked)
            {
                isChecked = true;
                this.GetFilteredItemLanguages(ref filteredNewItemLanguages, ref filteredUpdatedNewItemLanguages, ref filteredUpdatedItemLanguages, rightsContentID);
            }

            if (this.LangMessagesCB.Checked)
            {
                isChecked = true;
                this.GetFilteredItemLanguages(ref filteredNewItemLanguages, ref filteredUpdatedNewItemLanguages, ref filteredUpdatedItemLanguages, msgContentID);
            }

            if (LangLabelsCB.Checked)
            {
                isChecked = true;
                this.GetFilteredItemLanguages(ref filteredNewItemLanguages, ref filteredUpdatedNewItemLanguages, ref filteredUpdatedItemLanguages, labelContentID);
            }

            if (this.LangDatalistCB.Checked)
            {
                isChecked = true;
                this.GetFilteredItemLanguages(ref filteredNewItemLanguages, ref filteredUpdatedNewItemLanguages, ref filteredUpdatedItemLanguages, datalistContentID);
            }

            if (isChecked)
            {
                this.NewLangView.DataSource = new BindingList<ItemLanguage>(filteredNewItemLanguages);
                this.SourceUpdateLangView.DataSource = new BindingList<ItemLanguage>(filteredUpdatedNewItemLanguages.OrderBy(o => o.ContentID).ToList());
                this.TargetUpdateLangView.DataSource = new BindingList<ItemLanguage>(filteredUpdatedItemLanguages.OrderBy(o => o.ContentID).ToList());
            }
            else
            {
                this.NewLangView.DataSource = new BindingList<ItemLanguage>(this.NewItemLanguages);
                this.SourceUpdateLangView.DataSource = new BindingList<ItemLanguage>(this.UpdateNewItemLanguages.OrderBy(o => o.ContentID).ToList());
                this.TargetUpdateLangView.DataSource = new BindingList<ItemLanguage>(this.UpdatedTargetLanguages.OrderBy(o => o.ContentID).ToList());
            }
        }

        private void GetFilteredItemLanguages(ref List<ItemLanguage> filteredNewItemLanguages, ref List<ItemLanguage> filteredUpdatedNewItemLanguages, ref List<ItemLanguage> filteredUpdatedItemLanguages, string contentID)
        {
            List<ItemLanguage> languages = new List<ItemLanguage>();

            languages = this.NewItemLanguages.Where(f => f.ContentID.Contains(contentID)).ToList();
            if (languages != null && languages.Count > 0)
            {
                filteredNewItemLanguages.AddRange(languages);
            }

            languages = this.UpdateNewItemLanguages.Where(f => f.ContentID.Contains(contentID)).ToList();
            if (languages != null && languages.Count > 0)
            {
                filteredUpdatedNewItemLanguages.AddRange(languages);
            }

            languages = this.UpdatedTargetLanguages.Where(f => f.ContentID.Contains(contentID)).ToList();
            if (languages != null && languages.Count > 0)
            {
                filteredUpdatedItemLanguages.AddRange(languages);
            }
        }

        private void NewListNewItemCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.NewItemsView.Rows)
            {
                string rowStatus = row.Cells[12].Value != null ? row.Cells[12].Value.ToString() : string.Empty;

                if (rowStatus == "NEW")
                {
                    row.Cells[0].Value = this.NewListNewItemCB.Checked;
                }
            }
        }

        private void ExistingListNewItemCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.NewItemsView.Rows)
            {
                string rowStatus = row.Cells[12].Value != null ? row.Cells[12].Value.ToString() : string.Empty;

                if (rowStatus != "NEW")
                {
                    row.Cells[0].Value = this.ExistingListNewItemCB.Checked;
                }
            }
        }

        private void NewLangNewListCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in NewLangView.Rows)
            {
                string rowStatus = row.Cells[9].Value != null ? row.Cells[9].Value.ToString() : string.Empty;

                if (rowStatus == "DATALIST_NEW")
                {
                    row.Cells[0].Value = this.NewLangNewListCB.Checked;
                }
            }
        }

        private void NewLangNewItemCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.NewLangView.Rows)
            {
                string rowStatus = row.Cells[9].Value != null ? row.Cells[9].Value.ToString() : string.Empty;

                if (rowStatus == "ITEM_NEW")
                {
                    row.Cells[0].Value = this.NewLangNewItemCB.Checked;
                }
            }
        }

        private void NewLangExistingItemCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.NewLangView.Rows)
            {
                string rowStatus = row.Cells[9].Value != null ? row.Cells[9].Value.ToString() : string.Empty;

                if (rowStatus != "DATALIST_NEW" && rowStatus != "ITEM_NEW")
                {
                    row.Cells[0].Value = this.NewLangExistingItemCB.Checked;
                }
            }
        }

        private void AttributesRoleCB_CheckedChanged(object sender, EventArgs e)
        {
            this.FilterDataListAttributes();
        }

        private void FilterDataListAttributes()
        {
            List<ItemAttribute> filteredNewDatalistAttributes = new List<ItemAttribute>();
            List<ItemAttribute> filteredUpdatedNewDatalistAttributes = new List<ItemAttribute>();
            List<ItemAttribute> filteredUpdatedDatalistAttributes = new List<ItemAttribute>();
            string rolesContentID = "Core.SecurityRoles";
            string functionsContentID = "Core.SecurityFunctions";
            string rightsContentID = "Core.SecurityRights";
            string msgContentID = ".Msg";
            string datalistContentID = ".DataList";
            bool isChecked = false;

            if (this.AttributesRoleCB.Checked)
            {
                isChecked = true;
                this.GetFilteredDataListAttributes(ref filteredNewDatalistAttributes, ref filteredUpdatedNewDatalistAttributes, ref filteredUpdatedDatalistAttributes, rolesContentID);
            }

            if (this.AttributesFunctionCB.Checked)
            {
                isChecked = true;
                this.GetFilteredDataListAttributes(ref filteredNewDatalistAttributes, ref filteredUpdatedNewDatalistAttributes, ref filteredUpdatedDatalistAttributes, functionsContentID);
            }

            if (this.AttributesRoleCB.Checked)
            {
                isChecked = true;
                this.GetFilteredDataListAttributes(ref filteredNewDatalistAttributes, ref filteredUpdatedNewDatalistAttributes, ref filteredUpdatedDatalistAttributes, rightsContentID);
            }

            if (this.AttributesMsgCB.Checked)
            {
                isChecked = true;
                this.GetFilteredDataListAttributes(ref filteredNewDatalistAttributes, ref filteredUpdatedNewDatalistAttributes, ref filteredUpdatedDatalistAttributes, msgContentID);
            }

            if (this.AttributesDataListCB.Checked)
            {
                isChecked = true;
                this.GetFilteredDataListAttributes(ref filteredNewDatalistAttributes, ref filteredUpdatedNewDatalistAttributes, ref filteredUpdatedDatalistAttributes, datalistContentID);
            }

            if (isChecked)
            {
                this.NewAttributesView.DataSource = new BindingList<ItemAttribute>(filteredNewDatalistAttributes);
                this.UpdateSourceItemView.DataSource = new BindingList<ItemAttribute>(filteredUpdatedNewDatalistAttributes.OrderBy(o => o.ParentContentId).ThenBy(t => t.Code).ToList());
                this.UpdateTargetItemView.DataSource = new BindingList<ItemAttribute>(filteredUpdatedDatalistAttributes.OrderBy(o => o.ParentContentId).ThenBy(t => t.Code).ToList());
            }
            else
            {
                this.NewItemsView.DataSource = new BindingList<CodeItemModel>(this.NewListItems);
                this.UpdateSourceItemView.DataSource = new BindingList<CodeItemModel>(this.UpdatedItems.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList());
                this.UpdateTargetItemView.DataSource = new BindingList<CodeItemModel>(this.UpdatedTargetItems.OrderBy(o => o.ContentID).ThenBy(t => t.Code).ToList());
            }
        }

        private void GetFilteredDataListAttributes(ref List<ItemAttribute> filteredNewDataAttributes, ref List<ItemAttribute> filteredUpdatedNewAttributes, ref List<ItemAttribute> filteredUpdatedAttributes, string contentID)
        {
            List<ItemAttribute> itemattributes = new List<ItemAttribute>();

            itemattributes = this.NewItemAttribute.Where(f => f.ContentID.Contains(contentID)).ToList();
            if (itemattributes != null && itemattributes.Count > 0)
            {
                filteredNewDataAttributes.AddRange(itemattributes);
            }

            itemattributes = this.UpdateAttribute.Where(f => f.ContentID.Contains(contentID)).ToList();
            if (itemattributes != null && itemattributes.Count > 0)
            {
                filteredUpdatedNewAttributes.AddRange(itemattributes);
            }

            itemattributes = this.UpdatedTargetAttribute.Where(f => f.ContentID.Contains(contentID)).ToList();
            if (itemattributes != null && itemattributes.Count > 0)
            {
                filteredUpdatedAttributes.AddRange(itemattributes);
            }
        }

        private void NewItemsAttributeSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.NewAttributesView.Rows)
            {
                row.Cells[0].Value = this.NewItemsAttributeSelectAllCB.Checked;
            }
        }

        private void AttributesFunctionCB_CheckedChanged(object sender, EventArgs e)
        {
            this.FilterDataListAttributes();
        }

        private void AttributesRightsCB_CheckedChanged(object sender, EventArgs e)
        {
            this.FilterDataListAttributes();
        }

        private void AttributesMsgCB_CheckedChanged(object sender, EventArgs e)
        {
            this.FilterDataListAttributes();
        }

        private void AttributesDataListCB_CheckedChanged(object sender, EventArgs e)
        {
            this.FilterDataListAttributes();
        }

        private void AttributesRoleCB_CheckedChanged_1(object sender, EventArgs e)
        {
            this.FilterDataListAttributes();
        }

        private bool CheckUpdateAttributeChanged(ref ItemAttribute t, ref ItemAttribute targetItem)
        {
            bool itemChanged = false;

            if (t.IsActive != targetItem.IsActive)
            {
                itemChanged = true;
                t.IsActive = targetItem.IsActive = true;
            }

            return itemChanged;
        }

        private void btnUpdateAttribute_Click(object sender, EventArgs e)
        {
            bool selected = false;
            this.UpdateAttribute = new List<ItemAttribute>();

            foreach (DataGridViewRow row in this.NewAttributesView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateAttribute.Add(row.DataBoundItem as ItemAttribute);
                }
            }

            foreach (DataGridViewRow row in this.SourceUpdateAttributeView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateAttribute.Add(row.DataBoundItem as ItemAttribute);
                }
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void DatalistDiff_Load(object sender, EventArgs e)
        {

        }

        private void NewLinkSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.LinkgridView.Rows)
            {
                row.Cells[0].Value = this.NewLinkSelectAllCB.Checked;
            }
        }

        private void btnLinkitem_Click(object sender, EventArgs e)
        {
            bool selected = false;
            this.UpdateListLinkItems = new List<DataListItemLink>();

            foreach (DataGridViewRow row in this.LinkgridView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateListLinkItems.Add(row.DataBoundItem as DataListItemLink);
                }
            }

            foreach (DataGridViewRow row in this.SourceLinkView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateListLinkItems.Add(row.DataBoundItem as DataListItemLink);
                }
            }
        }

        private void TargetLinkView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}