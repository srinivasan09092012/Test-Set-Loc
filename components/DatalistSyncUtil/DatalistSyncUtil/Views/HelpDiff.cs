//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using DatalistSyncUtil.Domain;
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
    public partial class HelpDiff : Form
    {
        public HelpDiff()
        {
            this.InitializeComponent();
        }

        public HelpDiff(Guid tenantID, string type, List<HelpNodeModel> sourceList, List<HelpNodeModel> targetList)
        {
            this.InitializeComponent();
            this.TargetConnectionString = ConfigurationManager.ConnectionStrings["TargetDataList"];
            this.LoadHelper = new TenantHelper(this.TargetConnectionString);
            this.TenantID = tenantID;
            this.DeltaType = type;
            this.SourceHelpList = sourceList;
            this.TargetHelpList = targetList;
            this.LoadModules();
            ////this.LoadDelta();
        }

        public TenantHelper LoadHelper { get; set; }

        public ConnectionStringSettings TargetConnectionString { get; set; }

        public Guid TenantID { get; set; }

        public string DeltaType { get; set; }

        public List<HelpNodeModel> SourceHelpList { get; set; }

        public List<HelpNodeModel> TargetHelpList { get; set; }

        public List<HelpNodeModel> UpdateNewHelp { get; set; }

        public List<HelpNodeModel> UpdatedTargetHelp { get; set; }

        public List<HelpNodeModel> UpdateHelp { get; set; }

        public List<HelpContentLanguageModel> UpdateHelpLanguages { get; set; }

        public List<HelpContentLanguageModel> NewHelpLanguages { get; set; }

        public List<HelpContentLanguageModel> UpdateNewHelpLanguages { get; set; }

        public List<HelpContentLanguageModel> UpdatedTargetHelpLanguages { get; set; }

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
                    this.LoadHelplistDelta();
                    this.LoadNewLanguages();
                    this.diffHelpMain.SelectedTab = this.Help;
                    break;

                default:
                    break;
            }
        }

        private void LoadNewLanguages()
        {
            List<HelpContentLanguageModel> newHelpLanguages = new List<HelpContentLanguageModel>();
            List<HelpContentLanguageModel> newHelpLanguagesExisting = new List<HelpContentLanguageModel>();
            List<string> helpLists = null;

            helpLists = this.SourceHelpList.Select(c => c.HelpNodeNM).Except(this.TargetHelpList.Select(c => c.HelpNodeNM)).ToList();
            List<HelpNodeModel> newHelplists = this.SourceHelpList.Where(c => helpLists.Contains(c.HelpNodeNM)).ToList();

            newHelplists.ForEach(i =>
            {
                i.HelpContentLanguages.ForEach(f =>
                {
                    f.Status = "HTML_NEW";
                    f.HelpNodeNM = i.HelpNodeNM;
                    newHelpLanguages.Add(f);
                });
            });

            newHelpLanguagesExisting = this.GetNewLanguagesFromExistingItems();
            newHelpLanguages.AddRange(newHelpLanguagesExisting);

            this.newHelpLangView.AutoGenerateColumns = false;

            Guid tenantModuleId = (this.moduleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string moduleName = (this.moduleList.SelectedItem as TenantModuleModel).ModuleName;

            if (tenantModuleId != Guid.Empty)
            {
                newHelpLanguages = newHelpLanguages.Where(w => w.HelpNodeNM.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
            }

            this.NewHelpLanguages = newHelpLanguages;
            this.newHelpLangView.DataSource = new BindingList<HelpContentLanguageModel>(newHelpLanguages);
            this.LoadUpdateLanguagesFromExistingItems();
        }

        private List<HelpContentLanguageModel> GetNewLanguagesFromExistingItems()
        {
            List<HelpContentLanguageModel> newHelpLanguages = new List<HelpContentLanguageModel>();
            List<HelpNodeModel> sourceHelpLangs = null;
            List<HelpNodeModel> targetHelpLangs = null;
            List<HelpNodeModel> helpLangs = null;
            List<HelpContentLanguageModel> targetHelpLanguages = null;
            List<HelpContentLanguageModel> finalHelpLanguages = null;

            List<string> hhelpNodes = this.SourceHelpList.Select(c => c.HelpNodeNM).Intersect(this.TargetHelpList.Select(c => c.HelpNodeNM)).ToList();
            hhelpNodes.ForEach(f =>
            {
                sourceHelpLangs = this.SourceHelpList.FindAll(e => e.HelpNodeNM == f);
                targetHelpLangs = this.TargetHelpList.FindAll(e => e.HelpNodeNM == f);

                helpLangs = sourceHelpLangs.Where(b => targetHelpLangs.Any(a => a.HelpNodeNM == b.HelpNodeNM)).ToList();

                if (helpLangs != null && helpLangs.Count > 0)
                {
                    helpLangs.ForEach(l =>
                    {
                        targetHelpLanguages = targetHelpLangs.Find(i => i.HelpNodeNM == l.HelpNodeNM && i.HelpNodeTypeCD == l.HelpNodeTypeCD).HelpContentLanguages;
                        finalHelpLanguages = l.HelpContentLanguages.Where(b => !targetHelpLanguages.Any(a => a.Language == b.Language)).ToList();
                        finalHelpLanguages.ForEach(h =>
                        {
                            h.Status = "LANG_NEW";
                            h.HelpNodeId = l.HelpNodeId;
                            newHelpLanguages.Add(h);
                        });
                    });
                }
            });

            return newHelpLanguages.OrderBy(o => o.HelpNodeId).ToList();
        }

        private void LoadUpdateLanguagesFromExistingItems()
        {
            List<HelpContentLanguageModel> updateSourceHelpLanguages = new List<HelpContentLanguageModel>();
            List<HelpContentLanguageModel> updateTargetHelpLanguages = new List<HelpContentLanguageModel>();
            List<HelpNodeModel> sourceHelplist = null;
            List<HelpNodeModel> targetHelplist = null;
            List<HelpNodeModel> helpItems = null;
            List<HelpContentLanguageModel> targetHelpLanguages = null;
            HelpContentLanguageModel targetHelpLanguage = null;
            List<HelpContentLanguageModel> finalHelpLanguages = null;
            bool isChanged = false;

            List<string> htmlBlks = this.SourceHelpList.Select(c => c.HelpNodeNM).Intersect(this.TargetHelpList.Select(c => c.HelpNodeNM)).ToList();
            htmlBlks.ForEach(f =>
            {
                sourceHelplist = this.SourceHelpList.FindAll(e => e.HelpNodeNM == f);
                targetHelplist = this.TargetHelpList.FindAll(e => e.HelpNodeNM == f);

                helpItems = sourceHelplist.Where(b => targetHelplist.Any(a => a.HelpNodeNM == b.HelpNodeNM)).ToList();

                if (helpItems != null && helpItems.Count > 0)
                {
                    helpItems.ForEach(l =>
                    {
                        targetHelpLanguages = targetHelplist.Find(i => i.HelpNodeNM == l.HelpNodeNM).HelpContentLanguages;
                        finalHelpLanguages = l.HelpContentLanguages.Where(b => targetHelpLanguages.Any(a => a.Language == b.Language)).ToList();
                        finalHelpLanguages.ForEach(h =>
                        {
                            isChanged = false;
                            targetHelpLanguage = targetHelpLanguages.Find(n => n.Language == h.Language);
                            if (targetHelpLanguage.HtmlContent != null && h.HtmlContent != null && targetHelpLanguage.HtmlContent.Trim() != h.HtmlContent.Trim())
                            {
                                isChanged = true;
                                h.HelpModified = true;
                            }

                            if (targetHelpLanguage.Title != h.Title)
                            {
                                isChanged = true;
                                h.TitleModified = true;
                            }

                            if (isChanged)
                            {
                                h.Status = "UPDATE";
                                h.HelpNodeId = targetHelpLanguage.HelpNodeId;
                                ////targetHelpLanguage.HelpNodeId = l.HelpNodeId;
                                updateSourceHelpLanguages.Add(h);
                                updateTargetHelpLanguages.Add(targetHelpLanguage);
                            }
                        });
                    });
                }
            });

            this.sourceHelpLangView.AutoGenerateColumns = this.targetHelpLangView.AutoGenerateColumns = false;
            Guid tenantModuleId = (this.moduleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string moduleName = (this.moduleList.SelectedItem as TenantModuleModel).ModuleName;

            if (tenantModuleId != Guid.Empty)
            {
                updateSourceHelpLanguages = updateSourceHelpLanguages.Where(w => w.HelpNodeNM.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
                updateTargetHelpLanguages = updateTargetHelpLanguages.Where(w => w.HelpNodeNM.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
            }

            this.UpdateNewHelpLanguages = updateSourceHelpLanguages;
            this.UpdatedTargetHelpLanguages = updateTargetHelpLanguages;
            this.sourceHelpLangView.DataSource = new BindingList<HelpContentLanguageModel>(updateSourceHelpLanguages.OrderBy(o => o.HelpNodeNM).ToList());
            this.targetHelpLangView.DataSource = new BindingList<HelpContentLanguageModel>(updateTargetHelpLanguages.OrderBy(o => o.HelpNodeNM).ToList());
        }

        private void LoadHelplistDelta()
        {
            List<HelpNodeModel> newHelplists = null;
            this.newHelpView.AutoGenerateColumns = false;
            newHelplists = this.GetNewHelplist();
            Guid tenantModuleId = (this.moduleList.SelectedItem as TenantModuleModel).TenantModuleId;

            if (tenantModuleId != Guid.Empty)
            {
                newHelplists = newHelplists.Where(w => w.TenantModuleId == tenantModuleId).ToList();
            }

            this.newHelpView.DataSource = new BindingList<HelpNodeModel>(newHelplists);
            this.LoadUpdateHelp();
        }

        private List<HelpNodeModel> GetNewHelplist()
        {
            List<string> helpLists = this.SourceHelpList.Select(c => c.HelpNodeNM).Except(this.TargetHelpList.Select(c => c.HelpNodeNM)).ToList();
            List<HelpNodeModel> newhelplists = this.SourceHelpList.Where(c => helpLists.Contains(c.HelpNodeNM)).ToList();
            newhelplists.ForEach(i =>
            {
                i.Status = "NEW";
            });
            return newhelplists.OrderBy(o => o.HelpNodeNM).ToList();
        }

        private List<HelpNodeModel> UpdatedHelplist()
        {
            List<HelpNodeModel> updatedHelpists = new List<HelpNodeModel>();
            HelpNodeModel sourceHelpNodelist = null;
            HelpNodeModel targetHelpNodelist = null;
            List<string> help = this.SourceHelpList.Select(c => c.HelpNodeNM).Intersect(this.TargetHelpList.Select(c => c.HelpNodeNM)).ToList();
            help.ForEach(f =>
            {
                sourceHelpNodelist = this.SourceHelpList.Find(e => e.HelpNodeNM == f);
                targetHelpNodelist = this.TargetHelpList.Find(e => e.HelpNodeNM == f);

                if (sourceHelpNodelist.IsActive != targetHelpNodelist.IsActive)
                {
                    sourceHelpNodelist.Status = "UPDATE";
                    sourceHelpNodelist.HelpNodeId = targetHelpNodelist.HelpNodeId;
                    sourceHelpNodelist.IsActive = targetHelpNodelist.IsActive;
                    updatedHelpists.Add(sourceHelpNodelist);
                }
            });
            return updatedHelpists.OrderBy(o => o.HelpNodeNM).ToList();
        }

        private void LoadUpdateHelp()
        {
            List<HelpNodeModel> updateSourceHelp = new List<HelpNodeModel>();
            List<HelpNodeModel> updateTargetHelp = new List<HelpNodeModel>();
            List<HelpNodeModel> updatedHelpists = new List<HelpNodeModel>();
            HelpNodeModel sourceHelplist = null;
            HelpNodeModel targetHelplist = null;
            List<string> helpNodes = this.SourceHelpList.Select(c => c.HelpNodeNM).Intersect(this.TargetHelpList.Select(c => c.HelpNodeNM)).ToList();
            helpNodes.ForEach(f =>
            {
                sourceHelplist = this.SourceHelpList.Find(e => e.HelpNodeNM == f);
                targetHelplist = this.TargetHelpList.Find(e => e.HelpNodeNM == f);

                if (sourceHelplist.IsActive != targetHelplist.IsActive)
                {
                    sourceHelplist.Status = "UPDATE";
                    sourceHelplist.HelpNodeId = targetHelplist.HelpNodeId;
                    updateSourceHelp.Add(sourceHelplist);
                    updateTargetHelp.Add(targetHelplist);
                }
            });

            this.SourceHelp.AutoGenerateColumns = this.TargetHelp.AutoGenerateColumns = false;
            Guid tenantModuleId = (this.moduleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string moduleName = (this.moduleList.SelectedItem as TenantModuleModel).ModuleName;

            if (tenantModuleId != Guid.Empty)
            {
                updateSourceHelp = updateSourceHelp.Where(w => w.HelpNodeNM.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
                updateTargetHelp = updateTargetHelp.Where(w => w.HelpNodeNM.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
            }

            this.UpdateNewHelp = updateSourceHelp;
            this.UpdatedTargetHelp = updateTargetHelp;
            this.SourceHelp.DataSource = new BindingList<HelpNodeModel>(updateSourceHelp.OrderBy(o => o.HelpNodeNM).ToList());
            this.TargetHelp.DataSource = new BindingList<HelpNodeModel>(updateTargetHelp.OrderBy(o => o.HelpNodeNM).ToList());
        }

        private void ModuleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadHelplistDelta();
            this.LoadNewLanguages();
        }

        private void NewHelpSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.newHelpView.Rows)
            {
                row.Cells[0].Value = this.NewHelpSelectAllCB.Checked;
            }
        }

        private void UpdateHelpSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.SourceHelp.Rows)
            {
                row.Cells[0].Value = this.UpdateHelpSelectAllCB.Checked;
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
            foreach (DataGridViewRow row in this.newHtmlLangView.Rows)
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
            if ((this.UpdateHelp == null || this.UpdateHelp.Count() == 0) && (this.UpdateHelpLanguages == null || this.UpdateHelpLanguages.Count() == 0))
            {
                MessageBox.Show("Error:Please include select rows before moving to preview screen");
                return;
            }

            PreviewPage previewPage = new PreviewPage(this.UpdateHelp, this.UpdateHelpLanguages);
            previewPage.ShowDialog();
        }

        private void BtnIncludeHelp_Click(object sender, EventArgs e)
        {
            bool selected = false;
            this.UpdateHelp = new List<HelpNodeModel>();
            foreach (DataGridViewRow row in this.newHelpView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells["Select"].Value);

                if (selected)
                {
                    this.UpdateHelp.Add(row.DataBoundItem as HelpNodeModel);
                }
            }

            foreach (DataGridViewRow row in this.SourceHelp.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateHelp.Add(row.DataBoundItem as HelpNodeModel);
                }
            }
        }

        private void BtnIncludeHelpLang_Click(object sender, EventArgs e)
        {
            bool selected = false;
            this.UpdateHelpLanguages = new List<HelpContentLanguageModel>();
            foreach (DataGridViewRow row in this.newHelpLangView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateHelpLanguages.Add(row.DataBoundItem as HelpContentLanguageModel);
                }
            }

            foreach (DataGridViewRow row in this.sourceHelpLangView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateHelpLanguages.Add(row.DataBoundItem as HelpContentLanguageModel);
                }
                }
            }

        private void NewHelpLangView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in this.newHelpLangView.Rows)
            {
                string rowStatus = row.Cells[7].Value != null ? row.Cells[7].Value.ToString() : string.Empty;

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

        private void SourceHelpLangView_Scroll(object sender, ScrollEventArgs e)
        {
            this.targetHelpLangView.FirstDisplayedScrollingRowIndex = this.sourceHelpLangView.FirstDisplayedScrollingRowIndex;
        }

        private void TargetHelpLangView_Scroll(object sender, ScrollEventArgs e)
        {
            this.sourceHelpLangView.FirstDisplayedScrollingRowIndex = this.targetHelpLangView.FirstDisplayedScrollingRowIndex;
        }

        private void SourceHelpLangView_RowAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in this.sourceHelpLangView.Rows)
            {
                bool nodetxtModified = row.Cells[5].Value != null ? Convert.ToBoolean(row.Cells[5].Value) : false;

                if (nodetxtModified == true)
                {
                    row.Cells[3].Style.BackColor = Color.LightBlue;
                    row.Cells[3].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[3].Style.BackColor = Color.White;
                    row.Cells[3].Style.ForeColor = Color.Black;
                }

                bool titleModified = row.Cells[6].Value != null ? Convert.ToBoolean(row.Cells[6].Value) : false;

                if (titleModified == true)
                {
                    row.Cells[2].Style.BackColor = Color.LightBlue;
                    row.Cells[2].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[2].Style.BackColor = Color.White;
                    row.Cells[2].Style.ForeColor = Color.Black;
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewHelpLangSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.newHelpLangView.Rows)
            {
                row.Cells[0].Value = this.NewHelpLangSelectAllCB.Checked;
            }
        }

        private void NewLangNewHelpCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in newHelpLangView.Rows)
            {
                string rowStatus = row.Cells[7].Value != null ? row.Cells[7].Value.ToString() : string.Empty;

                if (rowStatus == "HTML_NEW")
                {
                    row.Cells[0].Value = this.NewLangNewHelpCB.Checked;
                }
            }
        }

        private void NewLangExistingHelpCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.newHelpLangView.Rows)
            {
                string rowStatus = row.Cells[7].Value != null ? row.Cells[7].Value.ToString() : string.Empty;
                if (rowStatus == "LANG_NEW")
                {
                    row.Cells[0].Value = this.NewLangExistingHelpCB.Checked;
                }
            }
        }

        private void UpdateHelpLangSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.sourceHelpLangView.Rows)
            {
                row.Cells[0].Value = this.UpdateHelpLangSelectAllCB.Checked;
            }
        }
    }
}
