using HP.HSP.UA3.Core.BAS.CQRS.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatalistSyncUtil
{
    public partial class DatalistDiff : Form
    {
        public DatalistDiff()
        {
            InitializeComponent();
        }

        public DatalistDiff(Guid tenantID, string type, List<DataListMainModel> sourceList, List<DataListMainModel> targetList)
        {
            InitializeComponent();
            this.TenantID = tenantID;
            this.DeltaType = type;
            this.SourceList = sourceList;
            this.TargetList = targetList;
            this.LoadDelta();
        }

        public Guid TenantID { get; set; }

        public string DeltaType { get; set; }

        public List<DataListMainModel> SourceList { get; set; }

        public List<DataListMainModel> TargetList { get; set; }

        private void LoadDelta()
        {
            switch (this.DeltaType.ToUpper())
            {
                case "DATALIST":
                    this.LoadDatalistDelta();
                    DiffTab.SelectedTab = DatalistTabPage;
                    break;
                case "ITEMS":
                    this.LoadDatalistDelta();
                    this.LoadDatalistItemsDelta();
                    DiffTab.SelectedTab = DatalistTabPage;
                    break;
                case "LANGUAGES":
                    this.LoadDatalistItemLanguagesDelta();
                    break;
                default:
                    break;
            }
        }

        private void LoadDatalistDelta()
        {
            List<DataListMainModel> newDatalists = null;
            List<DataListMainModel> updatedDatalists = null;
            DataListView.AutoGenerateColumns = false;
            newDatalists = this.GetNewDatalist();
            updatedDatalists = this.UpdatedDatalist();
            newDatalists.AddRange(updatedDatalists);
            DataListView.DataSource = new BindingList<DataListMainModel>(newDatalists);
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
                    updatedDatalists.Add(sourceDatalist);
                }
            });
            return updatedDatalists;
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
            return newDatalists;
        }

        private void LoadDatalistItemsDelta()
        {
            List<CodeItemModel> newDatalistItemsFromNewList = null;
            List<CodeItemModel> newDatalistItemsFromUpdateList = null;
            List<CodeItemModel> newDatalistItems = new List<CodeItemModel>();
            NewItemsView.AutoGenerateColumns = false;
            newDatalistItemsFromNewList = this.GetNewDatalistItemsFromNewList();
            newDatalistItems.AddRange(newDatalistItemsFromNewList);
            newDatalistItemsFromUpdateList = this.GetNewDatalistItemsFromExistingList();
            newDatalistItems.AddRange(newDatalistItemsFromUpdateList);
            NewItemsView.DataSource = new BindingList<CodeItemModel>(newDatalistItems);
            this.UpdatedDatalistItems();
            this.UpdateLanguages();
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
                    newDatalistItemsFromUpdateList.AddRange(dataListItems);
                }
            });

            return newDatalistItemsFromUpdateList;
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

            UpdateSourceItemView.AutoGenerateColumns = false;
            UpdateTargetItemView.AutoGenerateColumns = false;

            UpdateSourceItemView.DataSource = new BindingList<CodeItemModel>(updatedDatalistItems);
            UpdateTargetItemView.DataSource = new BindingList<CodeItemModel>(updatedTargetDatalistItems);
        }

        private void UpdateLanguages()
        {
            this.LoadNewLanguages();
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
            NewLangView.DataSource = new BindingList<ItemLanguage>(newDatalistItemLanguages);
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

            return newDatalistItemLanguages;
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

            return newDatalistItemLanguages;
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

                            if (targetDatalistItemLanguage.LongDescription !=null && h.LongDescription != null && targetDatalistItemLanguage.LongDescription.Trim() != h.LongDescription.Trim())
                            {
                                isChanged = true;
                                h.LongDescriptionModified = true;
                            }

                            if (isChanged)
                            {
                                h.Status = "UPDATE";
                                h.Code = l.Code;
                                h.ContentID = l.ContentID;
                                updateSourceDatalistItemLanguages.Add(h);
                                updateTargetDatalistItemLanguages.Add(targetDatalistItemLanguage);
                            }
                        });
                    });
                }
            });

            SourceUpdateLangView.AutoGenerateColumns = TargetUpdateLangView.AutoGenerateColumns = false;
            SourceUpdateLangView.DataSource = new BindingList<ItemLanguage>(updateSourceDatalistItemLanguages);
            TargetUpdateLangView.DataSource = new BindingList<ItemLanguage>(updateTargetDatalistItemLanguages);
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
            return newDatalistItems;
        }

        private void LoadDatalistItemLanguagesDelta()
        {
            throw new NotImplementedException();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnListUpdate_Click(object sender, EventArgs e)
        {

        }

        private void UpdateSourceItemView_Scroll(object sender, ScrollEventArgs e)
        {
            UpdateTargetItemView.FirstDisplayedScrollingRowIndex = UpdateSourceItemView.FirstDisplayedScrollingRowIndex;
        }

        private void UpdateTargetItemView_Scroll(object sender, ScrollEventArgs e)
        {
            UpdateSourceItemView.FirstDisplayedScrollingRowIndex = UpdateTargetItemView.FirstDisplayedScrollingRowIndex;
        }

        private void NewItemsView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in NewItemsView.Rows)
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
            foreach (DataGridViewRow row in UpdateSourceItemView.Rows)
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
            TargetUpdateLangView.FirstDisplayedScrollingRowIndex = SourceUpdateLangView.FirstDisplayedScrollingRowIndex;
        }

        private void SourceUpdateLangView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in SourceUpdateLangView.Rows)
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
            SourceUpdateLangView.FirstDisplayedScrollingRowIndex = TargetUpdateLangView.FirstDisplayedScrollingRowIndex;
        }

        private void DatalistSelectAllChkBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DataListView.Rows)
            {
                row.Cells[0].Value = DatalistSelectAllChkBox.Checked;
            }
        }

        private void UpdateLangSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in SourceUpdateLangView.Rows)
            {
                row.Cells[0].Value = UpdateLangSelectAllCB.Checked;
            }
        }

        private void NewLangSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in NewLangView.Rows)
            {
                row.Cells[0].Value = NewLangSelectAllCB.Checked;
            }
        }

        private void UpdateItemsSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in UpdateSourceItemView.Rows)
            {
                row.Cells[0].Value = UpdateItemsSelectAllCB.Checked;
            }
        }

        private void NewItemsSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in NewItemsView.Rows)
            {
                row.Cells[0].Value = NewItemsSelectAllCB.Checked;
            }
        }

        private void NewLangView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in NewLangView.Rows)
            {
                string rowStatus = row.Cells[8].Value != null ? row.Cells[8].Value.ToString() : string.Empty;

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
    }
}