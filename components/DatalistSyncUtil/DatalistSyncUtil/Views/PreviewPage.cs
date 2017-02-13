using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DatalistSyncUtil.Views
{
    public partial class PreviewPage : Form
    {
        public PreviewPage()
        {
            InitializeComponent();
        }

        public PreviewPage(List<DataListMainModel> finalList, List<CodeItemModel> finalListItems, List<ItemLanguage> finalLanguages)
        {
            InitializeComponent();

            this.Cache = new RedisCacheManager();
            this.FinalList = finalList;
            this.FinalListItems = finalListItems;
            this.FinalItemLanguages = finalLanguages;
            this.LoadHelper = new TenantHelper();
            this.TargetDataList = this.LoadHelper.GetDataList();
            this.LoadTreeView(PreviewTreeList, this.FinalList);
        }

        public ICacheManager Cache { get; set; }

        public List<DataListMainModel> FinalList { get; set; }

        public List<CodeItemModel> FinalListItems { get; set; }

        public List<ItemLanguage> FinalItemLanguages { get; set; }

        public List<DataList> TargetDataList { get; set; }

        public TenantHelper LoadHelper { get; set; }

        private void Submit_Click(object sender, EventArgs e)
        {
            this.SaveDataList();

            this.SaveDatalistItems();
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

                this.Cache.Remove("TargetDataListItems");
            }
        }

        private void SaveDataList()
        {
            try
            {
                if(this.FinalList != null)
                {
                    List<TenantModuleModel> modules = this.LoadHelper.LoadModules();

                    foreach (DataListMainModel list in this.FinalList)
                    {
                        list.TenantModuleID = modules.Find(f => f.TenantId == list.TenantID && f.ModuleName == list.ModuleName).TenantModuleId;
                        if(list.ID == null)
                        {
                            this.LoadHelper.AddDatalist(list);
                        }
                        else
                        {
                            this.LoadHelper.UpdateDatalist(list);
                        }
                    }

                    this.Cache.Remove("TargetDataLists");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message);
            }
        }

        private void LoadTreeView(TreeView treeView, List<DataListMainModel> lists)
        {
            TreeNode listNode = null;
            List<TreeNode> itemNodes = null;
            string contentID = string.Empty;

            try
            {
                treeView.Nodes.Clear();
                List<string> parentlists = this.GetAllContentID();

                parentlists.ForEach(f =>
                {
                    itemNodes = new List<TreeNode>();
                    if (this.FinalListItems != null)
                    {
                        this.GetTreeItems(itemNodes, f.Trim());
                    }
                    
                    listNode = new TreeNode(f.Trim(), itemNodes.ToArray());
                    treeView.Nodes.Add(listNode);
                });

                treeView.ExpandAll();
            }
            finally
            {
                listNode = null;
                itemNodes = null;
            }
        }

        private void GetTreeItems(List<TreeNode> itemNodes, string contentID)
        {
            TreeNode node = null;
            List<TreeNode> langNodes = null;

            List<CodeItemModel> items = this.FinalListItems.FindAll(f => f.ContentID.Trim() == contentID);

            items.ForEach(f =>
            {
                langNodes = new List<TreeNode>();
                if (this.FinalItemLanguages != null)
                {
                    this.GetTreeItemLanguages(langNodes, f.ContentID, f.Code);
                }
                
                node = new TreeNode(f.Code, langNodes.ToArray());
                itemNodes.Add(node);
            });
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

            return listContents;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
