using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using System;
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

            this.FinalList = finalList;
            this.FinalListItems = finalListItems;
            this.FinalItemLanguages = finalLanguages;
            this.LoadHelper = new TenantHelper();
            this.TargetDataList = this.LoadHelper.GetDataList();
            this.LoadTreeView(PreviewTreeList, this.FinalList);
        }

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
            
        }

        private void SaveDataList()
        {
            try
            {
                foreach (DataListMainModel list in this.FinalList)
                {
                    this.LoadHelper.AddDatalist(list);
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
                    this.GetTreeItems(itemNodes,f.Trim());
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
                this.GetTreeItemLanguages(langNodes, f.ContentID);
                node = new TreeNode(f.Code, langNodes.ToArray());
                itemNodes.Add(node);
            });
        }

        private void GetTreeItemLanguages(List<TreeNode> langNodes, string contentID)
        {
            TreeNode node = null;
            string languageSeparator = " - ";

            List<ItemLanguage> items = this.FinalItemLanguages.FindAll(f => f.ContentID.Trim() == contentID);

            items.ForEach(f =>
            {
                node = new TreeNode(f.LocaleID + languageSeparator + f.Description + (!string.IsNullOrEmpty(f.LongDescription) ? (languageSeparator + f.LongDescription) : string.Empty));
                langNodes.Add(node);
            });
        }

        private List<string> GetAllContentID()
        {
            List<string> listContents = new List<string>();

            this.FinalList.ForEach(f => {
                listContents.Add(f.ContentID.Trim());
            });

            this.FinalListItems.ForEach(f => {
                if(!listContents.Contains(f.ContentID.Trim()))
                {
                    listContents.Add(f.ContentID.Trim());
                }
            });

            this.FinalItemLanguages.ForEach(f => {
                if (!listContents.Contains(f.ContentID.Trim()))
                {
                    listContents.Add(f.ContentID.Trim());
                }
            });

            return listContents;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
