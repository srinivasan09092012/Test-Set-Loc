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
    public partial class HtmlBlockDiff : Form
    {
        public HtmlBlockDiff()
        {
            this.InitializeComponent();
        }

        public HtmlBlockDiff(Guid tenantID, string type, List<HtmlBlockMainModel> sourceList, List<HtmlBlockMainModel> targetList)
        {
            this.InitializeComponent();
            this.TargetConnectionString = ConfigurationManager.ConnectionStrings["TargetDataList"];
            this.LoadHelper = new TenantHelper(this.TargetConnectionString);
            this.TenantID = tenantID;
            this.DeltaType = type;
            this.SourceHtmlList = sourceList;
            this.TargetHtmlList = targetList;
            this.LoadModules();
            this.LoadDelta();
        }

        public TenantHelper LoadHelper { get; set; }

        public ConnectionStringSettings TargetConnectionString { get; set; }

        public Guid TenantID { get; set; }

        public string DeltaType { get; set; }

        public List<HtmlBlockMainModel> SourceHtmlList { get; set; }

        public List<HtmlBlockMainModel> TargetHtmlList { get; set; }

        public List<HtmlBlockMainModel> UpdateNewHtmlBlk { get; set; }

        public List<HtmlBlockMainModel> UpdatedTargetHtmlBlk { get; set; }

        public List<HtmlBlockLanguage> NewHtmlLanguages { get; set; }

        public List<HtmlBlockLanguage> UpdateNewHtmlLanguages { get; set; }

        public List<HtmlBlockLanguage> UpdatedTargetHtmlLanguages { get; set; }

        public List<HtmlBlockMainModel> UpdateHtmlBlks { get; set; }

        public List<HtmlBlockLanguage> UpdateHtmlBlkLanguages { get; set; }       

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
            this.moduleList.DataSource = modules.Where(w => w.TenantId == this.TenantID).GroupBy(i => i.ModuleName)
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

        private void LoadDelta()
        {
            switch (this.DeltaType.ToUpper())
            {
                case "ITEMS":
                    this.LoadHtmllistDelta();
                    this.LoadNewLanguages();
                    this.diffHtmlBlk.SelectedTab = this.HtmlBlk;
                    break;

                default:
                    break;
            }
        }

        private void LoadNewLanguages()
        {
            List<HtmlBlockLanguage> newHtmlLanguages = new List<HtmlBlockLanguage>();
            List<HtmlBlockLanguage> newHtmlLanguagesExisting = new List<HtmlBlockLanguage>();
            List<string> htmlLists = null;

            htmlLists = this.SourceHtmlList.Select(c => c.ContentId).Except(this.TargetHtmlList.Select(c => c.ContentId)).ToList();
            List<HtmlBlockMainModel> newHtmllists = this.SourceHtmlList.Where(c => htmlLists.Contains(c.ContentId)).ToList();

            newHtmllists.ForEach(i =>
            {
                i.HtmlBlockLanguages.ForEach(f =>
                {
                        f.Status = "HTML_NEW";
                        f.ContentId = i.ContentId;
                        newHtmlLanguages.Add(f);
                });
            });

            newHtmlLanguagesExisting = this.GetNewLanguagesFromExistingItems(); 
            newHtmlLanguages.AddRange(newHtmlLanguagesExisting);

            this.newHtmlLangView.AutoGenerateColumns = false;

            Guid tenantModuleId = (this.moduleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string moduleName = (this.moduleList.SelectedItem as TenantModuleModel).ModuleName;

            if (tenantModuleId != Guid.Empty)
            {
                newHtmlLanguages = newHtmlLanguages.Where(w => w.ContentId.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
            }

            this.NewHtmlLanguages = newHtmlLanguages;
            this.newHtmlLangView.DataSource = new BindingList<HtmlBlockLanguage>(newHtmlLanguages);
            this.LoadUpdateLanguagesFromExistingItems();
        }

        private List<HtmlBlockLanguage> GetNewLanguagesFromExistingItems()
        {
            List<HtmlBlockLanguage> newHtmlLanguages = new List<HtmlBlockLanguage>();
            List<HtmlBlockMainModel> sourceHtmlLangs = null;
            List<HtmlBlockMainModel> targetHtmlLangs = null;
            List<HtmlBlockMainModel> htmlLangs = null;
            List<HtmlBlockLanguage> targetHtmlLanguages = null;
            List<HtmlBlockLanguage> finalHtmlLanguages = null;

            List<string> htmlBlks = this.SourceHtmlList.Select(c => c.ContentId).Intersect(this.TargetHtmlList.Select(c => c.ContentId)).ToList();
            htmlBlks.ForEach(f =>
            {
                sourceHtmlLangs = this.SourceHtmlList.FindAll(e => e.ContentId == f);
                targetHtmlLangs = this.TargetHtmlList.FindAll(e => e.ContentId == f);

                htmlLangs = sourceHtmlLangs.Where(b => targetHtmlLangs.Any(a => a.ContentId == b.ContentId)).ToList();

                if (htmlLangs != null && htmlLangs.Count > 0)
                {
                    htmlLangs.ForEach(l =>
                    {
                        targetHtmlLanguages = targetHtmlLangs.Find(i => i.ContentId == l.ContentId).HtmlBlockLanguages;
                        finalHtmlLanguages = l.HtmlBlockLanguages.Where(b => !targetHtmlLanguages.Any(a => a.LocaleId == b.LocaleId)).ToList();
                        finalHtmlLanguages.ForEach(h =>
                        {
                            h.Status = "LANG_NEW";
                            h.ContentId = l.ContentId;
                            newHtmlLanguages.Add(h);
                        });
                    });
                }
            });

            return newHtmlLanguages.OrderBy(o => o.ContentId).ToList();    
        }

        private void LoadUpdateLanguagesFromExistingItems()
        {
            List<HtmlBlockLanguage> updateSourceHtmlLanguages = new List<HtmlBlockLanguage>();
            List<HtmlBlockLanguage> updateTargetHtmlLanguages = new List<HtmlBlockLanguage>();
            List<HtmlBlockMainModel> sourceHtmllist = null;
            List<HtmlBlockMainModel> targetHtmllist = null;
            List<HtmlBlockMainModel> htmlBlkItems = null;
            List<HtmlBlockLanguage> targetHtmlLanguages = null;
            HtmlBlockLanguage targetHtmlLanguage = null;
            List<HtmlBlockLanguage> finalHtmlLanguages = null;
            bool isChanged = false;

            List<string> htmlBlks = this.SourceHtmlList.Select(c => c.ContentId).Intersect(this.TargetHtmlList.Select(c => c.ContentId)).ToList();
            htmlBlks.ForEach(f =>
            {
                sourceHtmllist = this.SourceHtmlList.FindAll(e => e.ContentId == f);
                targetHtmllist = this.TargetHtmlList.FindAll(e => e.ContentId == f);

                htmlBlkItems = sourceHtmllist.Where(b => targetHtmllist.Any(a => a.ContentId == b.ContentId)).ToList();

                if (htmlBlkItems != null && htmlBlkItems.Count > 0)
                {
                    htmlBlkItems.ForEach(l =>
                    {
                        targetHtmlLanguages = targetHtmllist.Find(i => i.ContentId == l.ContentId).HtmlBlockLanguages;
                        finalHtmlLanguages = l.HtmlBlockLanguages.Where(b => targetHtmlLanguages.Any(a => a.LocaleId == b.LocaleId)).ToList();
                        finalHtmlLanguages.ForEach(h =>
                        {
                            isChanged = false;
                            targetHtmlLanguage = targetHtmlLanguages.Find(n => n.LocaleId == h.LocaleId);
                            if (targetHtmlLanguage.Html != null && h.Html != null && targetHtmlLanguage.Html.Trim() != h.Html.Trim())
                            {
                                isChanged = true;
                                h.HtmlModified = true;
                            }

                            if (targetHtmlLanguage.IsActive != h.IsActive)
                            {
                                isChanged = true;
                                h.IsActiveModified = true;
                            }

                            if (isChanged)
                            {
                                h.Status = "UPDATE";
                                h.ContentId = l.ContentId;
                                targetHtmlLanguage.ContentId = l.ContentId;
                                updateSourceHtmlLanguages.Add(h);
                                updateTargetHtmlLanguages.Add(targetHtmlLanguage);
                            }
                        });
                    });
                }
            });

            this.sourceHtmlBlkLangView.AutoGenerateColumns = this.targetHtmlBlkLangView.AutoGenerateColumns = false;
            Guid tenantModuleId = (this.moduleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string moduleName = (this.moduleList.SelectedItem as TenantModuleModel).ModuleName;

            if (tenantModuleId != Guid.Empty)
            {
                updateSourceHtmlLanguages = updateSourceHtmlLanguages.Where(w => w.ContentId.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
                updateTargetHtmlLanguages = updateTargetHtmlLanguages.Where(w => w.ContentId.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
            }

            this.UpdateNewHtmlLanguages = updateSourceHtmlLanguages;
            this.UpdatedTargetHtmlLanguages = updateTargetHtmlLanguages;
            this.sourceHtmlBlkLangView.DataSource = new BindingList<HtmlBlockLanguage>(updateSourceHtmlLanguages.OrderBy(o => o.ContentId).ToList());
            this.targetHtmlBlkLangView.DataSource = new BindingList<HtmlBlockLanguage>(updateTargetHtmlLanguages.OrderBy(o => o.ContentId).ToList());
        }

        private void LoadHtmllistDelta()
        {
            List<HtmlBlockMainModel> newHtmllists = null;
            this.newHtmlBlkView.AutoGenerateColumns = false;
            newHtmllists = this.GetNewHtmllist();
            Guid tenantModuleId = (this.moduleList.SelectedItem as TenantModuleModel).TenantModuleId;

            if (tenantModuleId != Guid.Empty)
            {
                newHtmllists = newHtmllists.Where(w => w.TenantModuleId == tenantModuleId).ToList();
            }

            this.newHtmlBlkView.DataSource = new BindingList<HtmlBlockMainModel>(newHtmllists);
            this.LoadUpdateHtmlBlks();
        }

        private List<HtmlBlockMainModel> GetNewHtmllist()
        {
            List<string> htmlLists = this.SourceHtmlList.Select(c => c.ContentId).Except(this.TargetHtmlList.Select(c => c.ContentId)).ToList();
            List<HtmlBlockMainModel> newhtmllists = this.SourceHtmlList.Where(c => htmlLists.Contains(c.ContentId)).ToList();
            newhtmllists.ForEach(i =>
            {
                i.Status = "NEW";
            });
            return newhtmllists.OrderBy(o => o.ContentId).ToList();
        }

        private void LoadUpdateHtmlBlks()
        {
            List<HtmlBlockMainModel> updateSourceHtmlBlks = new List<HtmlBlockMainModel>();
            List<HtmlBlockMainModel> updateTargetHtmlBlks = new List<HtmlBlockMainModel>();
            List<HtmlBlockMainModel> updatedHtmlists = new List<HtmlBlockMainModel>();
            HtmlBlockMainModel sourceHtmllist = null;
            HtmlBlockMainModel targetHtmllist = null;
            List<string> htmlBlks = this.SourceHtmlList.Select(c => c.ContentId).Intersect(this.TargetHtmlList.Select(c => c.ContentId)).ToList();
            htmlBlks.ForEach(f =>
            {
                sourceHtmllist = this.SourceHtmlList.Find(e => e.ContentId == f);
                targetHtmllist = this.TargetHtmlList.Find(e => e.ContentId == f);

                if (sourceHtmllist.Description != targetHtmllist.Description || sourceHtmllist.IsActive != targetHtmllist.IsActive)
                {
                    sourceHtmllist.Status = "UPDATE";
                    sourceHtmllist.ID = targetHtmllist.ID;
                    updateSourceHtmlBlks.Add(sourceHtmllist);
                    updateTargetHtmlBlks.Add(targetHtmllist);
                }
            });

            this.SourceHtmlBlk.AutoGenerateColumns = this.TargetHtmlBlk.AutoGenerateColumns = false;
            Guid tenantModuleId = (this.moduleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string moduleName = (this.moduleList.SelectedItem as TenantModuleModel).ModuleName;

            if (tenantModuleId != Guid.Empty)
            {
                updateSourceHtmlBlks = updateSourceHtmlBlks.Where(w => w.ContentId.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
                updateTargetHtmlBlks = updateTargetHtmlBlks.Where(w => w.ContentId.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
            }

            this.UpdateNewHtmlBlk = updateSourceHtmlBlks;
            this.UpdatedTargetHtmlBlk = updateTargetHtmlBlks;
            this.SourceHtmlBlk.DataSource = new BindingList<HtmlBlockMainModel>(updateSourceHtmlBlks.OrderBy(o => o.ContentId).ToList());
            this.TargetHtmlBlk.DataSource = new BindingList<HtmlBlockMainModel>(updateTargetHtmlBlks.OrderBy(o => o.ContentId).ToList());
        }

        private void ModuleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadHtmllistDelta();
            this.LoadNewLanguages();
        }

        private void NewHtmlBlkSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.newHtmlBlkView.Rows)
            {
                row.Cells[0].Value = this.NewHtmlBlkSelectAllCB.Checked;
            }
        }

        private void UpdateHtmlBlkSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.SourceHtmlBlk.Rows)
            {
                row.Cells[0].Value = this.UpdateHtmlBlkSelectAllCB.Checked;
            }
        }

        private void NewHtmlBlkLangSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.newHtmlLangView.Rows)
            {
                row.Cells[0].Value = this.NewHtmlBlkLangSelectAllCB.Checked;
            }
        }

        private void NewLangNewHtmlBlkCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in newHtmlLangView.Rows)
            {
                string rowStatus = row.Cells[6].Value != null ? row.Cells[6].Value.ToString() : string.Empty;

                if (rowStatus == "HTML_NEW")
                {
                    row.Cells[0].Value = this.NewLangNewHtmlBlkCB.Checked;
                }
            }
        }

        private void NewLangExistingHtmlBlkCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.newHtmlLangView.Rows)
            {
                string rowStatus = row.Cells[6].Value != null ? row.Cells[6].Value.ToString() : string.Empty;
                if (rowStatus == "LANG_NEW")
                {
                    row.Cells[0].Value = this.NewLangExistingHtmlBlkCB.Checked;
                }
            }
        }

        private void UpdateHtmlBlkLangSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.sourceHtmlBlkLangView.Rows)
            {
                row.Cells[0].Value = this.UpdateHtmlBlkLangSelectAllCB.Checked;
            }
        }

        private void PreviewUpdate_Click(object sender, EventArgs e)
        {
            if ((this.UpdateHtmlBlks == null || this.UpdateHtmlBlks.Count() == 0) && (this.UpdateHtmlBlkLanguages == null || this.UpdateHtmlBlkLanguages.Count() == 0))
            {
                MessageBox.Show("Error:Please include some rows before moving to preview screen");
                return;
            }

            PreviewPage previewPage = new PreviewPage(this.UpdateHtmlBlks, this.UpdateHtmlBlkLanguages);
            previewPage.ShowDialog();
        }

        private void BtnIncludeHtmlBlk_Click(object sender, EventArgs e)
        {
            bool selected = false;
            this.UpdateHtmlBlks = new List<HtmlBlockMainModel>();
            foreach (DataGridViewRow row in this.newHtmlBlkView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells["Select"].Value);

                if (selected)
                {
                    this.UpdateHtmlBlks.Add(row.DataBoundItem as HtmlBlockMainModel);
                }
            }

            foreach (DataGridViewRow row in this.SourceHtmlBlk.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateHtmlBlks.Add(row.DataBoundItem as HtmlBlockMainModel);
                }
            }
        }

        private void BtnIncludeHtmlLang_Click(object sender, EventArgs e)
        {
            bool selected = false;
            this.UpdateHtmlBlkLanguages = new List<HtmlBlockLanguage>();
            foreach (DataGridViewRow row in this.newHtmlLangView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateHtmlBlkLanguages.Add(row.DataBoundItem as HtmlBlockLanguage);
                }
            }

            foreach (DataGridViewRow row in this.sourceHtmlBlkLangView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateHtmlBlkLanguages.Add(row.DataBoundItem as HtmlBlockLanguage);
                }
            }
        }

        private void NewHtmlLangView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in this.newHtmlLangView.Rows)
            {
                string rowStatus = row.Cells[6].Value != null ? row.Cells[6].Value.ToString() : string.Empty;

                if (rowStatus == "HTML_NEW")
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

        private void SourceHtmlBlkLangView_Scroll(object sender, ScrollEventArgs e)
        {
            this.targetHtmlBlkLangView.FirstDisplayedScrollingRowIndex = this.sourceHtmlBlkLangView.FirstDisplayedScrollingRowIndex;
        }

        private void TargetHtmlBlkLangView_Scroll(object sender, ScrollEventArgs e)
        {
            this.sourceHtmlBlkLangView.FirstDisplayedScrollingRowIndex = this.targetHtmlBlkLangView.FirstDisplayedScrollingRowIndex;
        }

        private void SourceHtmlBlkLangView_RowAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in this.sourceHtmlBlkLangView.Rows)
            {
                bool htmlModified = row.Cells[6].Value != null ? Convert.ToBoolean(row.Cells[6].Value) : false;

                if (htmlModified == true)
                {
                    row.Cells[2].Style.BackColor = Color.LightBlue;
                    row.Cells[2].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[2].Style.BackColor = Color.White;
                    row.Cells[2].Style.ForeColor = Color.Black;
                }

                bool isActiveModified = row.Cells[8].Value != null ? Convert.ToBoolean(row.Cells[8].Value) : false;

                if (isActiveModified == true)
                {
                    row.Cells[4].Style.BackColor = Color.LightBlue;
                    row.Cells[4].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[4].Style.BackColor = Color.White;
                    row.Cells[4].Style.ForeColor = Color.Black;
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
