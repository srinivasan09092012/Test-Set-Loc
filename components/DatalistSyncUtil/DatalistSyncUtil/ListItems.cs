using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DatalistSyncUtil
{
    public partial class ListItems : Form
    {
        private readonly string listKeyword = "CodeList_";

        public List<CodeListModel> SourceListItems { get; set; }
        public ICacheManager Cache { get; set; }

        public string ContentID { get; set; }

        public int NoOfDays { get; set; }

        public ListItems(string contentID, int numberOfDays)
        {
            InitializeComponent();
            this.ContentID = contentID;
            this.NoOfDays = numberOfDays;
            this.Cache = new RedisCacheManager();
            this.SourceListItems = this.Cache.Get<List<CodeListModel>>("DataListItems");
            lblContentID.Text = this.ContentID;
            this.LoadDataListItems(numberOfDays);
        }

        private void LoadDataListItems(int numberOfDays)
        {
            string key = this.listKeyword + this.ContentID;
            List<SelectedItem> items = null;
            ListItemView.AutoGenerateColumns = false;

            if (this.Cache.IsSet(key))
            {
                items = this.Cache.Get<List<SelectedItem>>(key);
                ListItemView.DataSource = new BindingList<SelectedItem>(items);
            }
            else
            {
                if(numberOfDays > 0)
                {
                    ListItemView.DataSource = new BindingList<CodeListModel>(this.SourceListItems.Where(w => w.ContentID == this.ContentID && w.ModifiedDate >= DateTime.UtcNow.AddDays(this.NoOfDays * -1)).ToList());
                }
                else
                {
                    ListItemView.DataSource = new BindingList<CodeListModel>(this.SourceListItems.Where(w => w.ContentID == this.ContentID).ToList());
                }
            }
        }

        private void ItemViewClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            SelectedItem itemSelected = null;
            SelectedItem item = null;
            List<SelectedItem> items = new List<SelectedItem>();
            CodeListModel codeItem = null;

            foreach (DataGridViewRow row in ListItemView.Rows)
            {
                if(row.DataBoundItem is SelectedItem)
                {
                    item = row.DataBoundItem as SelectedItem;
                    itemSelected = new SelectedItem()
                    {
                        Code = item.Code,
                        ContentID = item.ContentID,
                        IsActive = item.IsActive,
                        ModifiedDate = item.ModifiedDate,
                        Selected = Convert.ToBoolean(row.Cells["SelectItem"].Value),
                        ID = item.ID
                    };
                }
                else
                {
                    codeItem = row.DataBoundItem as CodeListModel;
                    itemSelected = new SelectedItem()
                    {
                        Code = codeItem.Code,
                        ContentID = codeItem.ContentID,
                        IsActive = codeItem.IsActive,
                        ModifiedDate = codeItem.ModifiedDate,
                        Selected = Convert.ToBoolean(row.Cells["SelectItem"].Value),
                        ID = codeItem.ID
                    };
                }

                items.Add(itemSelected);
            }

            this.Cache.Set(this.listKeyword + this.ContentID, items, 1440);
            this.Close();
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            this.Cache.Remove(this.listKeyword + this.ContentID);
            this.LoadDataListItems(this.NoOfDays);
        }

        private void SelectAllChkBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in ListItemView.Rows)
            {
                row.Cells["SelectItem"].Value = SelectAllChkBox.Checked;
            }
        }

        private void ChkDateOverride_CheckedChanged(object sender, EventArgs e)
        {
            if(ChkDateOverride.Checked)
            {
                this.LoadDataListItems(0);
            }
            else
            {
                this.LoadDataListItems(this.NoOfDays);
            }
        }

        private void InactiveCB_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
