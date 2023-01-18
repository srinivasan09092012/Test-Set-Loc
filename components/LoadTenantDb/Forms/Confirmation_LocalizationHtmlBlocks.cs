using HP.HSP.UA3.Utilities.LoadTenantDb.Data;
using HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    public partial class Confirmation_LocalizationHtmlBlocks : Form
    {
        private int loadHtmlBlocksSuccessful = 0;
        private int loadHtmlBlockErrors = 0;
        private List<DataListsItems> messageTypeItems = new List<DataListsItems>();

        public Confirmation_LocalizationHtmlBlocks()
        {
            InitializeComponent();
        }

        public MainForm MainForm { get; set; }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.LoadGrid();

            Cursor.Current = Cursors.Default;
            MessageBox.Show("Tenant Configuration load complete. " + this.loadHtmlBlocksSuccessful + " Html Blocks loaded and " + this.loadHtmlBlockErrors + " errors reported.", "Tenant Load Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Confirmation_LocalizationHtmlBlocks_Load(object sender, EventArgs e)
        {
            this.RefreshGrid();

            if (this.MainForm.LocalizationHtmlBlocks.Count > 0)
            {
                this.loadButton.Enabled = true;
            }
            else
            {
                this.loadButton.Enabled = false;
            }
        }

        private void RefreshGrid()
        {
            List<string> row = new List<string>();

            this.htmlBlocksGridView.Rows.Clear();
            this.htmlBlocksGridView.Refresh();

            for (int i = 0; i < this.MainForm.LocalizationHtmlBlocks.Count; i++)
            {
                for (int j = 0; j < this.MainForm.LocalizationHtmlBlocks[i].HtmlBlocks.Count; j++)
                {
                    row.Clear();
                    row.Add(this.MainForm.LocalizationHtmlBlocks[i].HtmlBlocks[j].Action);
                    row.Add(this.MainForm.LocalizationHtmlBlocks[i].Module.Name);
                    row.Add(this.MainForm.LocalizationHtmlBlocks[i].HtmlBlocks[j].ContentId);
                    row.Add(this.MainForm.LocalizationHtmlBlocks[i].LocaleId.ToLower());
                    string html = this.MainForm.LocalizationHtmlBlocks[i].HtmlBlocks[j].Html;
                    row.Add(html.Length < 100 ? html:html.Substring(0,100));
                    this.htmlBlocksGridView.Rows.Add(row.ToArray());
                }
            }
        }

        private HtmlBlock CreateHtmlBlock(string contentId)
        {
            // Build the HtmlBlock object.
            HtmlBlock htmlBlock = new HtmlBlock();
            htmlBlock.MainForm = this.MainForm;
            htmlBlock.ContentId = contentId;
            htmlBlock.TenantModuleId = ConfigurationManager.AppSettings[this.MainForm.LocalizationHtmlBlocks[0].Module.Name + "TenantModuleId"];

            htmlBlock.IsActive = true;
            // Query the database to see if the item already exists. If it exists, the Id is set to the GUID. Otherwise, it will be null.
            htmlBlock.Id = htmlBlock.GetHtmlBlockId(htmlBlock);
            return htmlBlock;
        }

        private void CreateHtmlBlockLanguages(HtmlBlock htmlBlock, string locale, string html)
        {
            // If the datalist item doesn't exist on the database yet, add the language to it.
            if (htmlBlock.Id == null)
            {
                htmlBlock.HtmlBlockLanguages = new List<HtmlBlockLanguage>();
                htmlBlock.HtmlBlockLanguages.Add(new HtmlBlockLanguage());
                htmlBlock.HtmlBlockLanguages[0].HtmlBlockId = null;
                htmlBlock.HtmlBlockLanguages[0].Locale = locale;
                htmlBlock.HtmlBlockLanguages[0].Html = html;
                htmlBlock.HtmlBlockLanguages[0].IsActive = true;
            }
            else
            {
                // The datalist item exists, so obtain any datalist item languages that already exist for this datalist item on the database.
                List<Object> existingLanguages = htmlBlock.GetHtmlBlockLanguages(htmlBlock.Id);
                // Assume the language being passed in is not already associated with the item.
                bool newLanguage = true;

                // Process the languages already associated with this datalist item on the database.
                for (int l = 0; l < existingLanguages.Count; l++)
                {
                    // Compare the locale being processed to the one that exists on the database.
                    if (existingLanguages[l].ToString().Contains(locale))
                    {
                        newLanguage = false;
                        break;
                    }
                }

                // Language is not associated with the item, so add it to the item.
                htmlBlock.HtmlBlockLanguages = new List<HtmlBlockLanguage>();
                htmlBlock.HtmlBlockLanguages.Add(new HtmlBlockLanguage());
                htmlBlock.HtmlBlockLanguages[0].Locale = locale;
                htmlBlock.HtmlBlockLanguages[0].Html = html;
                htmlBlock.HtmlBlockLanguages[0].IsActive = true;
                if (newLanguage)
                {
                    htmlBlock.HtmlBlockLanguages[0].HtmlBlockId = null;
                }
                else
                {
                    htmlBlock.HtmlBlockLanguages[0].HtmlBlockId = htmlBlock.Id;
                }
            }
        }

        private HtmlBlock AddHtmlBlock(HtmlBlock htmlBlock)
        {
            // Add the HtmlBlock
            try
            {
                htmlBlock = htmlBlock.AddHtmlBlock(htmlBlock);
            }
            catch
            {
                htmlBlock = null;
            }

            return htmlBlock;
        }

        private HtmlBlock UpdateHtmlBlock(HtmlBlock htmlBlock)
        {
            // Update the HtmlBlockItem
            try
            {
                htmlBlock = htmlBlock.UpdateHtmlBlock(htmlBlock);
            }
            catch
            {
                htmlBlock = null;
            }

            return htmlBlock;
        }

        private void LoadGrid()
        {
            Cursor.Current = Cursors.WaitCursor;

            int currentRow = 0;

            // Process each locale (i.e. English, Spanish, etc.)
            for (int i = 0; i < this.MainForm.LocalizationHtmlBlocks.Count; i++)
            {
                // Process each HtmlBlock for a given locale.
                for (int j = 0; j < this.MainForm.LocalizationHtmlBlocks[i].HtmlBlocks.Count; j++)
                {
                    this.htmlBlocksGridView.Rows[currentRow].Cells[0].Value = this.MainForm.LocalizationHtmlBlocks[i].HtmlBlocks[j].Action;
                    this.htmlBlocksGridView.Refresh();

                    // Build the HtmlBlock 
                    HtmlBlock htmlBlock = CreateHtmlBlock(this.MainForm.LocalizationHtmlBlocks[i].HtmlBlocks[j].ContentId);

                    // Process the HtmlBlockItemLanguages.
                    CreateHtmlBlockLanguages(htmlBlock, this.MainForm.LocalizationHtmlBlocks[i].LocaleId.ToLower(), this.MainForm.LocalizationHtmlBlocks[i].HtmlBlocks[j].Html);

                    // Determine if the htmlBlock Item should be added or updated.
                    if (htmlBlock.Id == null)
                    {
                        // Add the HtmlBlockItem
                        htmlBlock = htmlBlock.AddHtmlBlock(htmlBlock);

                        if (htmlBlock != null)
                        {
                            this.MainForm.LocalizationHtmlBlocks[i].HtmlBlocks[j].Action = "Added";
                            loadHtmlBlocksSuccessful++;
                        }
                        else
                        {
                            this.MainForm.LocalizationHtmlBlocks[i].HtmlBlocks[j].Action = "Add Error";
                            loadHtmlBlockErrors++;
                        }
                    }
                    else
                    {
                        string action = string.IsNullOrEmpty(htmlBlock.HtmlBlockLanguages[0].HtmlBlockId) ? "Added" : "Updated";
                        htmlBlock = htmlBlock.UpdateHtmlBlock(htmlBlock);

                        if (htmlBlock != null)
                        {
                            this.MainForm.LocalizationHtmlBlocks[i].HtmlBlocks[j].Action = action;
                            loadHtmlBlocksSuccessful++;
                        }
                        else
                        {
                            this.MainForm.LocalizationHtmlBlocks[i].HtmlBlocks[j].Action = "Update Error";
                            loadHtmlBlockErrors++;
                        }
                    }

                    this.htmlBlocksGridView.Rows[currentRow].Cells[0].Value = this.MainForm.LocalizationHtmlBlocks[i].HtmlBlocks[j].Action;

                    if (string.IsNullOrEmpty((string)this.htmlBlocksGridView.Rows[currentRow].Cells[0].Value))
                    {
                        this.htmlBlocksGridView.Rows[currentRow].Cells[0].Value = "  ----";
                    }

                    if (this.htmlBlocksGridView.Rows.Count > currentRow + 1)
                    {
                        this.htmlBlocksGridView.CurrentCell = this.htmlBlocksGridView.Rows[currentRow + 1].Cells[0];
                        this.htmlBlocksGridView.Rows[currentRow + 1].Selected = true;
                    }

                    this.htmlBlocksGridView.Refresh();
                    currentRow++;
                }
            }

            Cursor.Current = Cursors.Default;
        }
    }
}