using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;

namespace DatalistSyncUtil
{
    public partial class DatalistComparer : Form
    {
        public DatalistComparer()
        {
            InitializeComponent();
            this.TargetConnectionString = ConfigurationManager.ConnectionStrings["SourceDataList"];
            this.LoadHelper = new TenantHelper(this.TargetConnectionString);
            txtTargetConnection.Text = this.TargetConnectionString.ConnectionString;
            this.LoadTenant();
            this.LoadModules();
        }

        public List<DataListMainModel> SourceList { get; set; }

        public List<DataListMainModel> TargetList { get; set; }

        public TenantHelper LoadHelper { get; set; }

        public ConnectionStringSettings TargetConnectionString { get; set; }

        private void btnSourceFile_Click(object sender, EventArgs e)
        {
            DialogResult result = OpenDatalistFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = OpenDatalistFile.FileName;
                txtSourceFile.Text = file;
                try
                {
                    this.SourceList = JsonConvert.DeserializeObject<List<DataListMainModel>>(File.ReadAllText(file));
                    List<TenantModuleModel> targetModules = ModuleList.DataSource as List<TenantModuleModel>;
                    List<DataListMainModel> sourceDataList = this.SourceList.Where(w => targetModules.Select(s => s.ModuleName).Contains(w.ModuleName)).ToList();
                }
                catch (IOException)
                {
                }

                Cursor.Current = Cursors.WaitCursor;
                this.LoadTreeView(SourceTreeList, this.SourceList.OrderBy(o=>o.ContentID).ToList());
                Cursor.Current = Cursors.Default;
            }
        }

        private void LoadTreeView(TreeView treeView, List<DataListMainModel> lists)
        {
            TreeNode listNode = null;
            TreeNode itemNode = null;
            List<TreeNode> itemNodes = null;
            TreeNode langNode = null;
            List<TreeNode> langNodes = null;
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

        private void btnLoadTarget_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Guid tenantModuleId = (ModuleList.SelectedItem as TenantModuleModel).TenantModuleId;
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

            this.LoadTreeView(TargetTreeList, filteredDataList.OrderBy(o=>o.ContentID).ToList());
            Cursor.Current = Cursors.Default;
        }

        private List<DataListMainModel> LoadTargetDatalist()
        {
            List<DataList> lists = null;
            List<CodeListModel> listItems = null;
            List<DataListMainModel> listsMain = new List<DataListMainModel>();
            DataListMainModel list1 = null;

            Guid tenantID = new Guid(TenantList.SelectedValue.ToString());

            lists = this.LoadHelper.GetDataList().Where(w => w.TenantID == tenantID).ToList();
            listItems = this.LoadHelper.GetDataListItems();

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
                    ID = list.ID
                };

                listsMain.Add(list1);
            }

            return listsMain;
        }

        private List<CodeItemModel> ConvertToCustomDataListItems(string contentID, Guid tenantID, List<CodeListModel> listItems)
        {
            List<CodeItemModel> items = new List<CodeItemModel>();
            CodeItemModel item = null;
            listItems = listItems.Where(w => w.ContentID == contentID && w.TenantID == tenantID).ToList();

            listItems.ForEach(e =>
            {
                item = new CodeItemModel()
                {
                    Code = e.Code,
                    ContentID = e.ContentID,
                    EffectiveEndDate = e.EffectiveEndDate,
                    EffectiveStartDate = e.EffectiveStartDate,
                    IsActive = e.IsActive,
                    IsEditable = e.IsEditable,
                    LanguageList = this.GetLanguageListCustom(e.LanguageList),
                    OrderIndex = e.OrderIndex,
                    TenantID = e.TenantID,
                    ID = e.ID
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
                    ItemID = e.CodeID
                };
                languages.Add(language);
            });

            return languages;
        }

        private void LoadTenant()
        {
            TenantList.DataSource = this.LoadHelper.GetTenants().ToList();
            TenantList.DisplayMember = "TenantName";
        }

        private void LoadModules()
        {
            Guid tenantID = (TenantList.SelectedItem as TenantModel).TenantID;
            List<TenantModuleModel> modules = this.LoadHelper.LoadModules();
            modules.Insert(0, new TenantModuleModel()
            {
                ModuleName = "---All Modules---",
                TenantModuleId = Guid.Empty,
                TenantId = tenantID
            });
            ModuleList.DataSource = modules.Where(w => w.TenantId == tenantID).GroupBy(i => i.ModuleName)
                  .Select(group =>
                        new
                        {
                            Key = group.Key,
                            Items = group.OrderByDescending(x => x.ModuleName)
                        })
                  .Select(g => g.Items.First()).OrderBy(o => o.ModuleName).ToList();
            ModuleList.DisplayMember = "ModuleName";
            ModuleList.SelectAll();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void datalistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatalistDiff diffPage = new DatalistDiff(new Guid(TenantList.SelectedValue.ToString()), "DATALIST", this.SourceList, this.TargetList);
            diffPage.ShowDialog();
        }

        private void datalistItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatalistDiff diffPage = new DatalistDiff(new Guid(TenantList.SelectedValue.ToString()), "ITEMS", this.SourceList, this.TargetList);
            diffPage.ShowDialog();
        }

        private void TenantList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadModules();
        }
    }
}
