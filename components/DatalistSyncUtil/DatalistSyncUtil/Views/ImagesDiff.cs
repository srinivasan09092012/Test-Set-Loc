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
    public partial class ImagesDiff : Form
    {
        public ImagesDiff()
        {
            this.InitializeComponent();
        }

        public ImagesDiff(Guid tenantID, string type, List<ImagesMainModel> sourceList, List<ImagesMainModel> targetList)
        {
            this.InitializeComponent();
            this.TargetConnectionString = ConfigurationManager.ConnectionStrings["TargetDataList"];
            this.LoadHelper = new TenantHelper(this.TargetConnectionString);
            this.TenantID = tenantID;
            this.DeltaType = type;
            this.SourceImagesList = sourceList;
            this.TargetImagesList = targetList;
            this.LoadModules();
            this.LoadDelta();
        }

        public TenantHelper LoadHelper { get; set; }

        public ConnectionStringSettings TargetConnectionString { get; set; }

        public Guid TenantID { get; set; }

        public string DeltaType { get; set; }

        public List<ImagesMainModel> SourceImagesList { get; set; }

        public List<ImagesMainModel> TargetImagesList { get; set; }

        public List<ImagesMainModel> UpdateNewImage { get; set; }

        public List<ImagesMainModel> UpdatedTargetImage { get; set; }

        public List<ImageLanguage> NewImageLanguages { get; set; }

        public List<ImageLanguage> UpdateNewImageLanguages { get; set; }

        public List<ImageLanguage> UpdatedTargetImageLanguages { get; set; }

        public List<ImagesMainModel> UpdateImages { get; set; }

        public List<ImageLanguage> UpdateImageLanguages { get; set; }

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
                case "IMAGES":
                    this.LoadImagelistDelta();
                    this.LoadNewLanguages();
                    this.diffImages.SelectedTab = this.Images;
                    break;

                default:
                    break;
            }
        }

        private void LoadImagelistDelta()
        {
            List<ImagesMainModel> newImagelists = null;
            this.newImagesView.AutoGenerateColumns = false;
            newImagelists = this.GetNewImageslist();
            Guid tenantModuleId = (this.moduleList.SelectedItem as TenantModuleModel).TenantModuleId;

            if (tenantModuleId != Guid.Empty)
            {
                newImagelists = newImagelists.Where(w => w.TenantModuleId == tenantModuleId).ToList();
            }

            this.newImagesView.DataSource = new BindingList<ImagesMainModel>(newImagelists);
            this.LoadUpdatedImages();
        }

        private List<ImagesMainModel> GetNewImageslist()
        {
            List<string> imageLists = this.SourceImagesList.Select(c => c.ContentId).Except(this.TargetImagesList.Select(c => c.ContentId)).ToList();
            List<ImagesMainModel> newImagelists = this.SourceImagesList.Where(c => imageLists.Contains(c.ContentId)).ToList();
            newImagelists.ForEach(i =>
            {
                i.Status = "NEW";
            });
            return newImagelists.OrderBy(o => o.ContentId).ToList();
        }

        private void LoadUpdatedImages()
        {
            List<ImagesMainModel> updateSourceImages = new List<ImagesMainModel>();
            List<ImagesMainModel> updateTargetImages = new List<ImagesMainModel>();
            List<ImagesMainModel> updatedImages = new List<ImagesMainModel>();
            ImagesMainModel sourceImagelist = null;
            ImagesMainModel targetImagelist = null;
            List<string> images = this.SourceImagesList.Select(c => c.ContentId).Intersect(this.TargetImagesList.Select(c => c.ContentId)).ToList();
            images.ForEach(f =>
            {
                sourceImagelist = this.SourceImagesList.Find(e => e.ContentId == f);
                targetImagelist = this.TargetImagesList.Find(e => e.ContentId == f);

                if (sourceImagelist.Description != targetImagelist.Description || sourceImagelist.IsActive != targetImagelist.IsActive)
                {
                    sourceImagelist.Status = "UPDATE";
                    sourceImagelist.ImageId = targetImagelist.ImageId;
                    updateSourceImages.Add(sourceImagelist);
                    updateTargetImages.Add(targetImagelist);
                }
            });

            this.SourceImages.AutoGenerateColumns = this.TargetImages.AutoGenerateColumns = false;
            Guid tenantModuleId = (this.moduleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string moduleName = (this.moduleList.SelectedItem as TenantModuleModel).ModuleName;

            if (tenantModuleId != Guid.Empty)
            {
                updateSourceImages = updateSourceImages.Where(w => w.ContentId.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
                updateTargetImages = updateTargetImages.Where(w => w.ContentId.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
            }

            this.UpdateNewImage = updateSourceImages;
            this.UpdatedTargetImage = updateTargetImages;
            this.SourceImages.DataSource = new BindingList<ImagesMainModel>(updateSourceImages.OrderBy(o => o.ContentId).ToList());
            this.TargetImages.DataSource = new BindingList<ImagesMainModel>(updateTargetImages.OrderBy(o => o.ContentId).ToList());
        }

        private void LoadNewLanguages()
        {
            List<ImageLanguage> newImageLanguages = new List<ImageLanguage>();
            List<ImageLanguage> newImageLanguagesExisting = new List<ImageLanguage>();
            List<string> imageLists = null;

            imageLists = this.SourceImagesList.Select(c => c.ContentId).Except(this.TargetImagesList.Select(c => c.ContentId)).ToList();
            List<ImagesMainModel> newHtmllists = this.SourceImagesList.Where(c => imageLists.Contains(c.ContentId)).ToList();

            newHtmllists.ForEach(i =>
            {
                i.ImageLanguages.ForEach(f =>
                {
                    f.Status = "IMG_NEW";
                    f.ContentId = i.ContentId;
                    newImageLanguages.Add(f);
                });
            });

            newImageLanguagesExisting = this.GetNewLanguagesFromExistingImages();
            newImageLanguages.AddRange(newImageLanguagesExisting);

            this.newImageLangView.AutoGenerateColumns = false;

            Guid tenantModuleId = (this.moduleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string moduleName = (this.moduleList.SelectedItem as TenantModuleModel).ModuleName;

            if (tenantModuleId != Guid.Empty)
            {
                newImageLanguages = newImageLanguages.Where(w => w.ContentId.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
            }

            this.NewImageLanguages = newImageLanguages;
            this.newImageLangView.DataSource = new BindingList<ImageLanguage>(newImageLanguages);
            this.LoadUpdatedLanguagesFromExistingImages();
        }

        private List<ImageLanguage> GetNewLanguagesFromExistingImages()
        {
            List<ImageLanguage> newImageLanguages = new List<ImageLanguage>();
            List<ImagesMainModel> sourceImageLangs = null;
            List<ImagesMainModel> targetImageLangs = null;
            List<ImagesMainModel> imageLangs = null;
            List<ImageLanguage> targetImageLanguages = null;
            List<ImageLanguage> finalImageLanguages = null;

            List<string> htmlBlks = this.SourceImagesList.Select(c => c.ContentId).Intersect(this.TargetImagesList.Select(c => c.ContentId)).ToList();
            htmlBlks.ForEach(f =>
            {
                sourceImageLangs = this.SourceImagesList.FindAll(e => e.ContentId == f);
                targetImageLangs = this.TargetImagesList.FindAll(e => e.ContentId == f);

                imageLangs = sourceImageLangs.Where(b => targetImageLangs.Any(a => a.ContentId == b.ContentId)).ToList();

                if (imageLangs != null && imageLangs.Count > 0)
                {
                    imageLangs.ForEach(l =>
                    {
                        targetImageLanguages = targetImageLangs.Find(i => i.ContentId == l.ContentId).ImageLanguages;
                        finalImageLanguages = l.ImageLanguages.Where(b => !targetImageLanguages.Any(a => a.LocaleId == b.LocaleId)).ToList();
                        finalImageLanguages.ForEach(h =>
                        {
                            h.Status = "LANG_NEW";
                            h.ContentId = l.ContentId;
                            newImageLanguages.Add(h);
                        });
                    });
                }
            });

            return newImageLanguages.OrderBy(o => o.ContentId).ToList();
        }

        private void LoadUpdatedLanguagesFromExistingImages()
        {
            List<ImageLanguage> updateSourceImageLanguages = new List<ImageLanguage>();
            List<ImageLanguage> updateTargetImageLanguages = new List<ImageLanguage>();
            List<ImagesMainModel> sourceImagelist = null;
            List<ImagesMainModel> targetImagelist = null;
            List<ImagesMainModel> imageItems = null;
            List<ImageLanguage> targetImageLanguages = null;
            ImageLanguage targetImageLanguage = null;
            List<ImageLanguage> finalImageLanguages = null;
            bool isChanged = false;

            List<string> htmlBlks = this.SourceImagesList.Select(c => c.ContentId).Intersect(this.TargetImagesList.Select(c => c.ContentId)).ToList();
            htmlBlks.ForEach(f =>
            {
                sourceImagelist = this.SourceImagesList.FindAll(e => e.ContentId == f);
                targetImagelist = this.TargetImagesList.FindAll(e => e.ContentId == f);

                imageItems = sourceImagelist.Where(b => targetImagelist.Any(a => a.ContentId == b.ContentId)).ToList();

                if (imageItems != null && imageItems.Count > 0)
                {
                    imageItems.ForEach(l =>
                    {
                        targetImageLanguages = targetImagelist.Find(i => i.ContentId == l.ContentId).ImageLanguages;
                        finalImageLanguages = l.ImageLanguages.Where(b => targetImageLanguages.Any(a => a.LocaleId == b.LocaleId)).ToList();
                        finalImageLanguages.ForEach(h =>
                        {
                            isChanged = false;
                            targetImageLanguage = targetImageLanguages.Find(n => n.LocaleId == h.LocaleId);
                            if (targetImageLanguage.Source != null && h.Source != null && targetImageLanguage.Source.Trim() != h.Source.Trim())
                            {
                                isChanged = true;
                                h.SourceModified = true;
                            }

                            if (targetImageLanguage.Width != h.Width)
                            {
                                isChanged = true;
                                h.WidthModified = true;
                            }

                            if (targetImageLanguage.Height != h.Height)
                            {
                                isChanged = true;
                                h.HeightModified = true;
                            }

                            if (targetImageLanguage.ToolTip != h.ToolTip)
                            {
                                isChanged = true;
                                h.ToolTipModified = true;
                            }

                            if (targetImageLanguage.IsActive != h.IsActive)
                            {
                                isChanged = true;
                                h.IsActiveModified = true;
                            }

                            if (isChanged)
                            {
                                h.Status = "UPDATE";
                                h.ContentId = l.ContentId;
                                targetImageLanguage.ContentId = l.ContentId;
                                updateSourceImageLanguages.Add(h);
                                updateTargetImageLanguages.Add(targetImageLanguage);
                            }
                        });
                    });
                }
            });

            this.sourceImageLangView.AutoGenerateColumns = this.targetImageLangView.AutoGenerateColumns = false;
            Guid tenantModuleId = (this.moduleList.SelectedItem as TenantModuleModel).TenantModuleId;
            string moduleName = (this.moduleList.SelectedItem as TenantModuleModel).ModuleName;

            if (tenantModuleId != Guid.Empty)
            {
                updateSourceImageLanguages = updateSourceImageLanguages.Where(w => w.ContentId.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
                updateTargetImageLanguages = updateTargetImageLanguages.Where(w => w.ContentId.StartsWith(moduleName.Replace(" ", string.Empty))).ToList();
            }

            this.UpdateNewImageLanguages = updateSourceImageLanguages;
            this.UpdatedTargetImageLanguages = updateTargetImageLanguages;
            this.sourceImageLangView.DataSource = new BindingList<ImageLanguage>(updateSourceImageLanguages.OrderBy(o => o.ContentId).ToList());
            this.targetImageLangView.DataSource = new BindingList<ImageLanguage>(updateTargetImageLanguages.OrderBy(o => o.ContentId).ToList());
        }

        private void BtnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewImagesSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.newImagesView.Rows)
            {
                row.Cells[0].Value = this.NewImagesSelectAllCB.Checked;
            }
        }

        private void UpdateImagesSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.SourceImages.Rows)
            {
                row.Cells[0].Value = this.UpdateImagesSelectAllCB.Checked;
            }
        }

        private void NewImageLangSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.newImageLangView.Rows)
            {
                row.Cells[0].Value = this.NewImageLangSelectAllCB.Checked;
            }
        }

        private void UpdateImageLangSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.sourceImageLangView.Rows)
            {
                row.Cells[0].Value = this.UpdateImageLangSelectAllCB.Checked;
            }
        }

        private void NewLangNewImageCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in newImageLangView.Rows)
            {
                string rowStatus = row.Cells[9].Value != null ? row.Cells[9].Value.ToString() : string.Empty;

                if (rowStatus == "IMG_NEW")
                {
                    row.Cells[0].Value = this.NewLangNewImageCB.Checked;
                }
            }
        }

        private void NewLangExistingImageCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.newImageLangView.Rows)
            {
                string rowStatus = row.Cells[9].Value != null ? row.Cells[9].Value.ToString() : string.Empty;
                if (rowStatus == "LANG_NEW")
                {
                    row.Cells[0].Value = this.NewLangExistingImageCB.Checked;
                }
            }
        }

        private void PreviewUpdate_Click(object sender, EventArgs e)
        {
            if ((this.UpdateImages == null || this.UpdateImages.Count() == 0) && (this.UpdateImageLanguages == null || this.UpdateImageLanguages.Count() == 0))
            {
                MessageBox.Show("Error:Please include some rows before moving to preview screen");
                return;
            }

            PreviewPage previewPage = new PreviewPage(this.UpdateImages, this.UpdateImageLanguages);
            previewPage.ShowDialog();
        }

        private void BtnIncludeImages_Click(object sender, EventArgs e)
        {
            bool selected = false;
            this.UpdateImages = new List<ImagesMainModel>();
            foreach (DataGridViewRow row in this.newImagesView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells["Select"].Value);

                if (selected)
                {
                    this.UpdateImages.Add(row.DataBoundItem as ImagesMainModel);
                }
            }

            foreach (DataGridViewRow row in this.SourceImages.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateImages.Add(row.DataBoundItem as ImagesMainModel);
                }
            }
        }
        
        private void SourceImageLangView_Scroll(object sender, ScrollEventArgs e)
        {
            this.targetImageLangView.FirstDisplayedScrollingRowIndex = this.sourceImageLangView.FirstDisplayedScrollingRowIndex;
        }

        private void TargetImageLangView_Scroll(object sender, ScrollEventArgs e)
        {
            this.sourceImageLangView.FirstDisplayedScrollingRowIndex = this.targetImageLangView.FirstDisplayedScrollingRowIndex;
        }

        private void SourceImageLangView_RowAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in this.sourceImageLangView.Rows)
            {
                bool sourceModified = row.Cells[9].Value != null ? Convert.ToBoolean(row.Cells[9].Value) : false;

                if (sourceModified == true)
                {
                    row.Cells[2].Style.BackColor = Color.LightBlue;
                    row.Cells[2].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[2].Style.BackColor = Color.White;
                    row.Cells[2].Style.ForeColor = Color.Black;
                }

                bool widthModified = row.Cells[10].Value != null ? Convert.ToBoolean(row.Cells[10].Value) : false;

                if (widthModified == true)
                {
                    row.Cells[3].Style.BackColor = Color.LightBlue;
                    row.Cells[3].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[3].Style.BackColor = Color.White;
                    row.Cells[3].Style.ForeColor = Color.Black;
                }

                bool heightModified = row.Cells[11].Value != null ? Convert.ToBoolean(row.Cells[11].Value) : false;

                if (heightModified == true)
                {
                    row.Cells[4].Style.BackColor = Color.LightBlue;
                    row.Cells[4].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[4].Style.BackColor = Color.White;
                    row.Cells[4].Style.ForeColor = Color.Black;
                }

                bool tooltipModified = row.Cells[12].Value != null ? Convert.ToBoolean(row.Cells[12].Value) : false;

                if (tooltipModified == true)
                {
                    row.Cells[5].Style.BackColor = Color.LightBlue;
                    row.Cells[5].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[5].Style.BackColor = Color.White;
                    row.Cells[5].Style.ForeColor = Color.Black;
                }

                bool isActiveModified = row.Cells[14].Value != null ? Convert.ToBoolean(row.Cells[14].Value) : false;

                if (isActiveModified == true)
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

        private void NewImageLangView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in this.newImageLangView.Rows)
            {
                string rowStatus = row.Cells[9].Value != null ? row.Cells[9].Value.ToString() : string.Empty;

                if (rowStatus == "IMG_NEW")
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

        private void BtnIncludeImageLangs_Click(object sender, EventArgs e)
        {
            bool selected = false;
            this.UpdateImageLanguages = new List<ImageLanguage>();
            foreach (DataGridViewRow row in this.newImageLangView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateImageLanguages.Add(row.DataBoundItem as ImageLanguage);
                }
            }

            foreach (DataGridViewRow row in this.sourceImageLangView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateImageLanguages.Add(row.DataBoundItem as ImageLanguage);
                }
            }
        }

        private void ModuleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadImagelistDelta();
            this.LoadNewLanguages();
        }
    }
}
