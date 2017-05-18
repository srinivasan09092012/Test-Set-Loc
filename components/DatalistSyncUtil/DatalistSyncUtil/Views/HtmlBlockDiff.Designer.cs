//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace DatalistSyncUtil
{
    public partial class HtmlBlockDiff
    {
        private System.Windows.Forms.ComboBox moduleList;
        private System.Windows.Forms.Label label4;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.moduleList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.HtmlBlk = new System.Windows.Forms.TabPage();
            this.btnIncludeHtmlBlk = new System.Windows.Forms.Button();
            this.ItemsTab = new System.Windows.Forms.TabControl();
            this.NewItemsPage = new System.Windows.Forms.TabPage();
            this.newHtmlBlkView = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewHtmlBlkSelectAllCB = new System.Windows.Forms.CheckBox();
            this.UpdateItemsTab = new System.Windows.Forms.TabPage();
            this.TargetHtmlBlk = new System.Windows.Forms.DataGridView();
            this.UpdateHtmlBlkSelectAllCB = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SourceHtmlBlk = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn13 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn14 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn15 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.diffHtmlBlk = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnIncludeHtmlLang = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabNewLang = new System.Windows.Forms.TabPage();
            this.NewLangExistingHtmlBlkCB = new System.Windows.Forms.CheckBox();
            this.NewLangNewHtmlBlkCB = new System.Windows.Forms.CheckBox();
            this.newHtmlLangView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn16 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn54 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn56 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn55 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn17 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn18 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn63 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewHtmlBlkLangSelectAllCB = new System.Windows.Forms.CheckBox();
            this.tabUpdateLang = new System.Windows.Forms.TabPage();
            this.targetHtmlBlkLangView = new System.Windows.Forms.DataGridView();
            this.sourceHtmlBlkLangView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn8 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn9 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.htmlModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActiveModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateHtmlBlkLangSelectAllCB = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.UpdateLangTab = new System.Windows.Forms.TabPage();
            this.SourceUpdateLangView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongDescModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DesciptionModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn11 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TargetUpdateLangView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn12 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.UpdateLangSelectAllCB = new System.Windows.Forms.CheckBox();
            this.NewLangTab = new System.Windows.Forms.TabPage();
            this.NewLangView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Locale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn10 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NewLangSelectAllCB = new System.Windows.Forms.CheckBox();
            this.NewLangNewListCB = new System.Windows.Forms.CheckBox();
            this.NewLangNewItemCB = new System.Windows.Forms.CheckBox();
            this.NewLangExistingItemCB = new System.Windows.Forms.CheckBox();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn27 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn26 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn25 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PreviewUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dataGridViewCheckBoxColumn19 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn20 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn21 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn22 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn23 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn24 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HtmlBlk.SuspendLayout();
            this.ItemsTab.SuspendLayout();
            this.NewItemsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newHtmlBlkView)).BeginInit();
            this.UpdateItemsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetHtmlBlk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceHtmlBlk)).BeginInit();
            this.diffHtmlBlk.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabNewLang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newHtmlLangView)).BeginInit();
            this.tabUpdateLang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetHtmlBlkLangView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourceHtmlBlkLangView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceUpdateLangView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetUpdateLangView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewLangView)).BeginInit();
            this.SuspendLayout();
            // 
            // moduleList
            // 
            this.moduleList.DisplayMember = "ModuleName";
            this.moduleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moduleList.FormattingEnabled = true;
            this.moduleList.Location = new System.Drawing.Point(77, 12);
            this.moduleList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.moduleList.Name = "moduleList";
            this.moduleList.Size = new System.Drawing.Size(209, 21);
            this.moduleList.Sorted = true;
            this.moduleList.TabIndex = 30;
            this.moduleList.ValueMember = "TenantModuleID";
            this.moduleList.SelectedIndexChanged += new System.EventHandler(this.ModuleList_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 15);
            this.label4.TabIndex = 29;
            this.label4.Text = "Module:";
            // 
            // HtmlBlk
            // 
            this.HtmlBlk.Controls.Add(this.btnIncludeHtmlBlk);
            this.HtmlBlk.Controls.Add(this.ItemsTab);
            this.HtmlBlk.Location = new System.Drawing.Point(4, 22);
            this.HtmlBlk.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.HtmlBlk.Name = "HtmlBlk";
            this.HtmlBlk.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.HtmlBlk.Size = new System.Drawing.Size(1271, 513);
            this.HtmlBlk.TabIndex = 1;
            this.HtmlBlk.Text = "HtmlBlock";
            this.HtmlBlk.UseVisualStyleBackColor = true;
            // 
            // btnIncludeHtmlBlk
            // 
            this.btnIncludeHtmlBlk.Location = new System.Drawing.Point(4, 482);
            this.btnIncludeHtmlBlk.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnIncludeHtmlBlk.Name = "btnIncludeHtmlBlk";
            this.btnIncludeHtmlBlk.Size = new System.Drawing.Size(72, 25);
            this.btnIncludeHtmlBlk.TabIndex = 19;
            this.btnIncludeHtmlBlk.Text = "Include";
            this.btnIncludeHtmlBlk.UseVisualStyleBackColor = true;
            this.btnIncludeHtmlBlk.Click += new System.EventHandler(this.BtnIncludeHtmlBlk_Click);
            // 
            // ItemsTab
            // 
            this.ItemsTab.Controls.Add(this.NewItemsPage);
            this.ItemsTab.Controls.Add(this.UpdateItemsTab);
            this.ItemsTab.Location = new System.Drawing.Point(4, 6);
            this.ItemsTab.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ItemsTab.Name = "ItemsTab";
            this.ItemsTab.SelectedIndex = 0;
            this.ItemsTab.Size = new System.Drawing.Size(1265, 474);
            this.ItemsTab.TabIndex = 0;
            // 
            // NewItemsPage
            // 
            this.NewItemsPage.Controls.Add(this.newHtmlBlkView);
            this.NewItemsPage.Controls.Add(this.NewHtmlBlkSelectAllCB);
            this.NewItemsPage.Location = new System.Drawing.Point(4, 22);
            this.NewItemsPage.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewItemsPage.Name = "NewItemsPage";
            this.NewItemsPage.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewItemsPage.Size = new System.Drawing.Size(1257, 448);
            this.NewItemsPage.TabIndex = 0;
            this.NewItemsPage.Text = "New Html Block";
            this.NewItemsPage.UseVisualStyleBackColor = true;
            // 
            // newHtmlBlkView
            // 
            this.newHtmlBlkView.AllowUserToAddRows = false;
            this.newHtmlBlkView.AllowUserToDeleteRows = false;
            this.newHtmlBlkView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.newHtmlBlkView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.dataGridViewTextBoxColumn1,
            this.Code,
            this.dataGridViewCheckBoxColumn2,
            this.dataGridViewCheckBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.newHtmlBlkView.Location = new System.Drawing.Point(12, 27);
            this.newHtmlBlkView.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.newHtmlBlkView.Name = "newHtmlBlkView";
            this.newHtmlBlkView.RowTemplate.Height = 24;
            this.newHtmlBlkView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.newHtmlBlkView.Size = new System.Drawing.Size(997, 414);
            this.newHtmlBlkView.TabIndex = 6;
            // 
            // Select
            // 
            this.Select.HeaderText = "";
            this.Select.Name = "Select";
            this.Select.Width = 25;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn1.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 400;
            // 
            // Code
            // 
            this.Code.DataPropertyName = "Description";
            this.Code.HeaderText = "Description";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Width = 300;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn2.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.ReadOnly = true;
            this.dataGridViewCheckBoxColumn2.Width = 60;
            // 
            // dataGridViewCheckBoxColumn3
            // 
            this.dataGridViewCheckBoxColumn3.DataPropertyName = "IsEditable";
            this.dataGridViewCheckBoxColumn3.HeaderText = "Editable";
            this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
            this.dataGridViewCheckBoxColumn3.ReadOnly = true;
            this.dataGridViewCheckBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn3.Width = 70;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn4.HeaderText = "Status";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 60;
            // 
            // NewHtmlBlkSelectAllCB
            // 
            this.NewHtmlBlkSelectAllCB.AutoSize = true;
            this.NewHtmlBlkSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewHtmlBlkSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewHtmlBlkSelectAllCB.Location = new System.Drawing.Point(2, 5);
            this.NewHtmlBlkSelectAllCB.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewHtmlBlkSelectAllCB.Name = "NewHtmlBlkSelectAllCB";
            this.NewHtmlBlkSelectAllCB.Size = new System.Drawing.Size(80, 17);
            this.NewHtmlBlkSelectAllCB.TabIndex = 8;
            this.NewHtmlBlkSelectAllCB.Text = "Select All";
            this.NewHtmlBlkSelectAllCB.UseVisualStyleBackColor = true;
            this.NewHtmlBlkSelectAllCB.CheckedChanged += new System.EventHandler(this.NewHtmlBlkSelectAllCB_CheckedChanged);
            // 
            // UpdateItemsTab
            // 
            this.UpdateItemsTab.Controls.Add(this.TargetHtmlBlk);
            this.UpdateItemsTab.Controls.Add(this.UpdateHtmlBlkSelectAllCB);
            this.UpdateItemsTab.Controls.Add(this.label6);
            this.UpdateItemsTab.Controls.Add(this.SourceHtmlBlk);
            this.UpdateItemsTab.Controls.Add(this.label2);
            this.UpdateItemsTab.Controls.Add(this.label7);
            this.UpdateItemsTab.Controls.Add(this.label1);
            this.UpdateItemsTab.Location = new System.Drawing.Point(4, 22);
            this.UpdateItemsTab.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.UpdateItemsTab.Name = "UpdateItemsTab";
            this.UpdateItemsTab.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.UpdateItemsTab.Size = new System.Drawing.Size(1257, 448);
            this.UpdateItemsTab.TabIndex = 1;
            this.UpdateItemsTab.Text = "Update Html Block";
            this.UpdateItemsTab.UseVisualStyleBackColor = true;
            // 
            // TargetHtmlBlk
            // 
            this.TargetHtmlBlk.AllowUserToAddRows = false;
            this.TargetHtmlBlk.AllowUserToDeleteRows = false;
            this.TargetHtmlBlk.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TargetHtmlBlk.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn19,
            this.dataGridViewTextBoxColumn22,
            this.dataGridViewTextBoxColumn31,
            this.dataGridViewCheckBoxColumn20,
            this.dataGridViewCheckBoxColumn21,
            this.dataGridViewTextBoxColumn33});
            this.TargetHtmlBlk.Location = new System.Drawing.Point(624, 28);
            this.TargetHtmlBlk.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TargetHtmlBlk.Name = "TargetHtmlBlk";
            this.TargetHtmlBlk.RowTemplate.Height = 24;
            this.TargetHtmlBlk.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TargetHtmlBlk.Size = new System.Drawing.Size(629, 414);
            this.TargetHtmlBlk.TabIndex = 16;
            // 
            // UpdateHtmlBlkSelectAllCB
            // 
            this.UpdateHtmlBlkSelectAllCB.AutoSize = true;
            this.UpdateHtmlBlkSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateHtmlBlkSelectAllCB.Location = new System.Drawing.Point(64, 5);
            this.UpdateHtmlBlkSelectAllCB.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.UpdateHtmlBlkSelectAllCB.Name = "UpdateHtmlBlkSelectAllCB";
            this.UpdateHtmlBlkSelectAllCB.Size = new System.Drawing.Size(80, 17);
            this.UpdateHtmlBlkSelectAllCB.TabIndex = 15;
            this.UpdateHtmlBlkSelectAllCB.Text = "Select All";
            this.UpdateHtmlBlkSelectAllCB.UseVisualStyleBackColor = true;
            this.UpdateHtmlBlkSelectAllCB.CheckedChanged += new System.EventHandler(this.UpdateHtmlBlkSelectAllCB_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(621, 5);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Target:";
            // 
            // SourceHtmlBlk
            // 
            this.SourceHtmlBlk.AllowUserToAddRows = false;
            this.SourceHtmlBlk.AllowUserToDeleteRows = false;
            this.SourceHtmlBlk.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SourceHtmlBlk.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn13,
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn20,
            this.dataGridViewCheckBoxColumn14,
            this.dataGridViewCheckBoxColumn15,
            this.dataGridViewTextBoxColumn21});
            this.SourceHtmlBlk.Location = new System.Drawing.Point(4, 28);
            this.SourceHtmlBlk.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.SourceHtmlBlk.Name = "SourceHtmlBlk";
            this.SourceHtmlBlk.RowTemplate.Height = 24;
            this.SourceHtmlBlk.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SourceHtmlBlk.Size = new System.Drawing.Size(639, 414);
            this.SourceHtmlBlk.TabIndex = 12;
            // 
            // dataGridViewCheckBoxColumn13
            // 
            this.dataGridViewCheckBoxColumn13.HeaderText = "";
            this.dataGridViewCheckBoxColumn13.Name = "dataGridViewCheckBoxColumn13";
            this.dataGridViewCheckBoxColumn13.Width = 25;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn19.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.Width = 250;
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn20.HeaderText = "Description";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            this.dataGridViewTextBoxColumn20.Width = 250;
            // 
            // dataGridViewCheckBoxColumn14
            // 
            this.dataGridViewCheckBoxColumn14.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn14.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn14.Name = "dataGridViewCheckBoxColumn14";
            this.dataGridViewCheckBoxColumn14.ReadOnly = true;
            this.dataGridViewCheckBoxColumn14.Width = 50;
            // 
            // dataGridViewCheckBoxColumn15
            // 
            this.dataGridViewCheckBoxColumn15.DataPropertyName = "IsEditable";
            this.dataGridViewCheckBoxColumn15.HeaderText = "Editable";
            this.dataGridViewCheckBoxColumn15.Name = "dataGridViewCheckBoxColumn15";
            this.dataGridViewCheckBoxColumn15.ReadOnly = true;
            this.dataGridViewCheckBoxColumn15.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn15.ThreeState = true;
            this.dataGridViewCheckBoxColumn15.Visible = false;
            this.dataGridViewCheckBoxColumn15.Width = 55;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn21.HeaderText = "Status";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.ReadOnly = true;
            this.dataGridViewTextBoxColumn21.Visible = false;
            this.dataGridViewTextBoxColumn21.Width = 60;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(744, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 5);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "Source:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 9;
            // 
            // diffHtmlBlk
            // 
            this.diffHtmlBlk.Controls.Add(this.HtmlBlk);
            this.diffHtmlBlk.Controls.Add(this.tabPage3);
            this.diffHtmlBlk.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diffHtmlBlk.Location = new System.Drawing.Point(17, 39);
            this.diffHtmlBlk.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.diffHtmlBlk.Name = "diffHtmlBlk";
            this.diffHtmlBlk.SelectedIndex = 0;
            this.diffHtmlBlk.Size = new System.Drawing.Size(1279, 539);
            this.diffHtmlBlk.TabIndex = 33;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnIncludeHtmlLang);
            this.tabPage3.Controls.Add(this.tabControl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1271, 513);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "HtmlBlockLanguage";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnIncludeHtmlLang
            // 
            this.btnIncludeHtmlLang.Location = new System.Drawing.Point(2, 488);
            this.btnIncludeHtmlLang.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnIncludeHtmlLang.Name = "btnIncludeHtmlLang";
            this.btnIncludeHtmlLang.Size = new System.Drawing.Size(72, 25);
            this.btnIncludeHtmlLang.TabIndex = 20;
            this.btnIncludeHtmlLang.Text = "Include";
            this.btnIncludeHtmlLang.UseVisualStyleBackColor = true;
            this.btnIncludeHtmlLang.Click += new System.EventHandler(this.BtnIncludeHtmlLang_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabNewLang);
            this.tabControl1.Controls.Add(this.tabUpdateLang);
            this.tabControl1.Location = new System.Drawing.Point(3, 6);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1263, 480);
            this.tabControl1.TabIndex = 1;
            // 
            // tabNewLang
            // 
            this.tabNewLang.Controls.Add(this.NewLangExistingHtmlBlkCB);
            this.tabNewLang.Controls.Add(this.NewLangNewHtmlBlkCB);
            this.tabNewLang.Controls.Add(this.newHtmlLangView);
            this.tabNewLang.Controls.Add(this.NewHtmlBlkLangSelectAllCB);
            this.tabNewLang.Location = new System.Drawing.Point(4, 22);
            this.tabNewLang.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabNewLang.Name = "tabNewLang";
            this.tabNewLang.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabNewLang.Size = new System.Drawing.Size(1255, 454);
            this.tabNewLang.TabIndex = 0;
            this.tabNewLang.Text = "New Lang";
            this.tabNewLang.UseVisualStyleBackColor = true;
            // 
            // NewLangExistingHtmlBlkCB
            // 
            this.NewLangExistingHtmlBlkCB.AutoSize = true;
            this.NewLangExistingHtmlBlkCB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.NewLangExistingHtmlBlkCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewLangExistingHtmlBlkCB.Location = new System.Drawing.Point(313, 6);
            this.NewLangExistingHtmlBlkCB.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewLangExistingHtmlBlkCB.Name = "NewLangExistingHtmlBlkCB";
            this.NewLangExistingHtmlBlkCB.Size = new System.Drawing.Size(220, 17);
            this.NewLangExistingHtmlBlkCB.TabIndex = 28;
            this.NewLangExistingHtmlBlkCB.Text = "New Languages - Existing HtmlBlk";
            this.NewLangExistingHtmlBlkCB.UseVisualStyleBackColor = false;
            this.NewLangExistingHtmlBlkCB.CheckedChanged += new System.EventHandler(this.NewLangExistingHtmlBlkCB_CheckedChanged);
            // 
            // NewLangNewHtmlBlkCB
            // 
            this.NewLangNewHtmlBlkCB.AutoSize = true;
            this.NewLangNewHtmlBlkCB.BackColor = System.Drawing.Color.LightBlue;
            this.NewLangNewHtmlBlkCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewLangNewHtmlBlkCB.Location = new System.Drawing.Point(95, 4);
            this.NewLangNewHtmlBlkCB.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewLangNewHtmlBlkCB.Name = "NewLangNewHtmlBlkCB";
            this.NewLangNewHtmlBlkCB.Size = new System.Drawing.Size(201, 17);
            this.NewLangNewHtmlBlkCB.TabIndex = 26;
            this.NewLangNewHtmlBlkCB.Text = "New Languages - New HtmlBlk";
            this.NewLangNewHtmlBlkCB.UseVisualStyleBackColor = false;
            this.NewLangNewHtmlBlkCB.CheckedChanged += new System.EventHandler(this.NewLangNewHtmlBlkCB_CheckedChanged);
            // 
            // newHtmlLangView
            // 
            this.newHtmlLangView.AllowUserToAddRows = false;
            this.newHtmlLangView.AllowUserToDeleteRows = false;
            this.newHtmlLangView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.newHtmlLangView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn16,
            this.dataGridViewTextBoxColumn54,
            this.dataGridViewTextBoxColumn56,
            this.dataGridViewTextBoxColumn55,
            this.dataGridViewCheckBoxColumn17,
            this.dataGridViewCheckBoxColumn18,
            this.dataGridViewTextBoxColumn63});
            this.newHtmlLangView.Location = new System.Drawing.Point(4, 28);
            this.newHtmlLangView.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.newHtmlLangView.Name = "newHtmlLangView";
            this.newHtmlLangView.RowTemplate.Height = 24;
            this.newHtmlLangView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.newHtmlLangView.Size = new System.Drawing.Size(1013, 424);
            this.newHtmlLangView.TabIndex = 6;
            this.newHtmlLangView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.NewHtmlLangView_RowsAdded);
            // 
            // dataGridViewCheckBoxColumn16
            // 
            this.dataGridViewCheckBoxColumn16.HeaderText = "";
            this.dataGridViewCheckBoxColumn16.Name = "dataGridViewCheckBoxColumn16";
            this.dataGridViewCheckBoxColumn16.Width = 25;
            // 
            // dataGridViewTextBoxColumn54
            // 
            this.dataGridViewTextBoxColumn54.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn54.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn54.Name = "dataGridViewTextBoxColumn54";
            this.dataGridViewTextBoxColumn54.ReadOnly = true;
            this.dataGridViewTextBoxColumn54.Width = 300;
            // 
            // dataGridViewTextBoxColumn56
            // 
            this.dataGridViewTextBoxColumn56.DataPropertyName = "Html";
            this.dataGridViewTextBoxColumn56.HeaderText = "Html";
            this.dataGridViewTextBoxColumn56.Name = "dataGridViewTextBoxColumn56";
            this.dataGridViewTextBoxColumn56.ReadOnly = true;
            this.dataGridViewTextBoxColumn56.Width = 400;
            // 
            // dataGridViewTextBoxColumn55
            // 
            this.dataGridViewTextBoxColumn55.DataPropertyName = "LocaleID";
            this.dataGridViewTextBoxColumn55.HeaderText = "LocaleID";
            this.dataGridViewTextBoxColumn55.Name = "dataGridViewTextBoxColumn55";
            this.dataGridViewTextBoxColumn55.ReadOnly = true;
            // 
            // dataGridViewCheckBoxColumn17
            // 
            this.dataGridViewCheckBoxColumn17.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn17.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn17.Name = "dataGridViewCheckBoxColumn17";
            this.dataGridViewCheckBoxColumn17.ReadOnly = true;
            this.dataGridViewCheckBoxColumn17.Width = 60;
            // 
            // dataGridViewCheckBoxColumn18
            // 
            this.dataGridViewCheckBoxColumn18.DataPropertyName = "IsEditable";
            this.dataGridViewCheckBoxColumn18.HeaderText = "Editable";
            this.dataGridViewCheckBoxColumn18.Name = "dataGridViewCheckBoxColumn18";
            this.dataGridViewCheckBoxColumn18.ReadOnly = true;
            this.dataGridViewCheckBoxColumn18.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn18.Visible = false;
            this.dataGridViewCheckBoxColumn18.Width = 70;
            // 
            // dataGridViewTextBoxColumn63
            // 
            this.dataGridViewTextBoxColumn63.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn63.HeaderText = "Status";
            this.dataGridViewTextBoxColumn63.Name = "dataGridViewTextBoxColumn63";
            this.dataGridViewTextBoxColumn63.ReadOnly = true;
            this.dataGridViewTextBoxColumn63.Width = 90;
            // 
            // NewHtmlBlkLangSelectAllCB
            // 
            this.NewHtmlBlkLangSelectAllCB.AutoSize = true;
            this.NewHtmlBlkLangSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewHtmlBlkLangSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewHtmlBlkLangSelectAllCB.Location = new System.Drawing.Point(2, 5);
            this.NewHtmlBlkLangSelectAllCB.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewHtmlBlkLangSelectAllCB.Name = "NewHtmlBlkLangSelectAllCB";
            this.NewHtmlBlkLangSelectAllCB.Size = new System.Drawing.Size(80, 17);
            this.NewHtmlBlkLangSelectAllCB.TabIndex = 8;
            this.NewHtmlBlkLangSelectAllCB.Text = "Select All";
            this.NewHtmlBlkLangSelectAllCB.UseVisualStyleBackColor = true;
            this.NewHtmlBlkLangSelectAllCB.CheckedChanged += new System.EventHandler(this.NewHtmlBlkLangSelectAllCB_CheckedChanged);
            // 
            // tabUpdateLang
            // 
            this.tabUpdateLang.Controls.Add(this.targetHtmlBlkLangView);
            this.tabUpdateLang.Controls.Add(this.sourceHtmlBlkLangView);
            this.tabUpdateLang.Controls.Add(this.UpdateHtmlBlkLangSelectAllCB);
            this.tabUpdateLang.Controls.Add(this.label3);
            this.tabUpdateLang.Controls.Add(this.label5);
            this.tabUpdateLang.Location = new System.Drawing.Point(4, 22);
            this.tabUpdateLang.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabUpdateLang.Name = "tabUpdateLang";
            this.tabUpdateLang.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabUpdateLang.Size = new System.Drawing.Size(1255, 454);
            this.tabUpdateLang.TabIndex = 1;
            this.tabUpdateLang.Text = "Update Lang";
            this.tabUpdateLang.UseVisualStyleBackColor = true;
            // 
            // targetHtmlBlkLangView
            // 
            this.targetHtmlBlkLangView.AllowUserToAddRows = false;
            this.targetHtmlBlkLangView.AllowUserToDeleteRows = false;
            this.targetHtmlBlkLangView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.targetHtmlBlkLangView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn22,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16,
            this.dataGridViewCheckBoxColumn23,
            this.dataGridViewCheckBoxColumn24,
            this.dataGridViewTextBoxColumn18});
            this.targetHtmlBlkLangView.Location = new System.Drawing.Point(632, 32);
            this.targetHtmlBlkLangView.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.targetHtmlBlkLangView.Name = "targetHtmlBlkLangView";
            this.targetHtmlBlkLangView.RowTemplate.Height = 24;
            this.targetHtmlBlkLangView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.targetHtmlBlkLangView.Size = new System.Drawing.Size(627, 424);
            this.targetHtmlBlkLangView.TabIndex = 13;
            this.targetHtmlBlkLangView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TargetHtmlBlkLangView_Scroll);
            // 
            // sourceHtmlBlkLangView
            // 
            this.sourceHtmlBlkLangView.AllowUserToAddRows = false;
            this.sourceHtmlBlkLangView.AllowUserToDeleteRows = false;
            this.sourceHtmlBlkLangView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sourceHtmlBlkLangView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn7,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewCheckBoxColumn8,
            this.dataGridViewCheckBoxColumn9,
            this.htmlModified,
            this.dataGridViewTextBoxColumn13,
            this.IsActiveModified});
            this.sourceHtmlBlkLangView.Location = new System.Drawing.Point(4, 32);
            this.sourceHtmlBlkLangView.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.sourceHtmlBlkLangView.Name = "sourceHtmlBlkLangView";
            this.sourceHtmlBlkLangView.RowTemplate.Height = 24;
            this.sourceHtmlBlkLangView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sourceHtmlBlkLangView.Size = new System.Drawing.Size(657, 424);
            this.sourceHtmlBlkLangView.TabIndex = 12;
            this.sourceHtmlBlkLangView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.SourceHtmlBlkLangView_RowAdded);
            this.sourceHtmlBlkLangView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SourceHtmlBlkLangView_Scroll);
            // 
            // dataGridViewCheckBoxColumn7
            // 
            this.dataGridViewCheckBoxColumn7.HeaderText = "";
            this.dataGridViewCheckBoxColumn7.Name = "dataGridViewCheckBoxColumn7";
            this.dataGridViewCheckBoxColumn7.Width = 25;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn9.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 200;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Html";
            this.dataGridViewTextBoxColumn10.HeaderText = "Html";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 250;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "LocaleID";
            this.dataGridViewTextBoxColumn11.HeaderText = "Locale";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 50;
            // 
            // dataGridViewCheckBoxColumn8
            // 
            this.dataGridViewCheckBoxColumn8.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn8.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn8.Name = "dataGridViewCheckBoxColumn8";
            this.dataGridViewCheckBoxColumn8.ReadOnly = true;
            this.dataGridViewCheckBoxColumn8.Width = 60;
            // 
            // dataGridViewCheckBoxColumn9
            // 
            this.dataGridViewCheckBoxColumn9.DataPropertyName = "IsEditable";
            this.dataGridViewCheckBoxColumn9.HeaderText = "Editable";
            this.dataGridViewCheckBoxColumn9.Name = "dataGridViewCheckBoxColumn9";
            this.dataGridViewCheckBoxColumn9.ReadOnly = true;
            this.dataGridViewCheckBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn9.Visible = false;
            this.dataGridViewCheckBoxColumn9.Width = 70;
            // 
            // htmlModified
            // 
            this.htmlModified.DataPropertyName = "htmlModified";
            this.htmlModified.HeaderText = "htmlModified";
            this.htmlModified.Name = "htmlModified";
            this.htmlModified.Visible = false;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn13.HeaderText = "Status";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Visible = false;
            this.dataGridViewTextBoxColumn13.Width = 60;
            // 
            // IsActiveModified
            // 
            this.IsActiveModified.DataPropertyName = "IsActiveModified";
            this.IsActiveModified.HeaderText = "IsActiveModified";
            this.IsActiveModified.Name = "IsActiveModified";
            this.IsActiveModified.Visible = false;
            // 
            // UpdateHtmlBlkLangSelectAllCB
            // 
            this.UpdateHtmlBlkLangSelectAllCB.AutoSize = true;
            this.UpdateHtmlBlkLangSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateHtmlBlkLangSelectAllCB.Location = new System.Drawing.Point(65, 12);
            this.UpdateHtmlBlkLangSelectAllCB.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.UpdateHtmlBlkLangSelectAllCB.Name = "UpdateHtmlBlkLangSelectAllCB";
            this.UpdateHtmlBlkLangSelectAllCB.Size = new System.Drawing.Size(80, 17);
            this.UpdateHtmlBlkLangSelectAllCB.TabIndex = 11;
            this.UpdateHtmlBlkLangSelectAllCB.Text = "Select All";
            this.UpdateHtmlBlkLangSelectAllCB.UseVisualStyleBackColor = true;
            this.UpdateHtmlBlkLangSelectAllCB.CheckedChanged += new System.EventHandler(this.UpdateHtmlBlkLangSelectAllCB_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(629, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Target:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 12);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Source:";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage2.Size = new System.Drawing.Size(1094, 448);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Update";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // UpdateLangTab
            // 
            this.UpdateLangTab.Location = new System.Drawing.Point(4, 22);
            this.UpdateLangTab.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.UpdateLangTab.Name = "UpdateLangTab";
            this.UpdateLangTab.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.UpdateLangTab.Size = new System.Drawing.Size(1094, 448);
            this.UpdateLangTab.TabIndex = 1;
            this.UpdateLangTab.Text = "Update";
            this.UpdateLangTab.UseVisualStyleBackColor = true;
            // 
            // SourceUpdateLangView
            // 
            this.SourceUpdateLangView.AllowUserToAddRows = false;
            this.SourceUpdateLangView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SourceUpdateLangView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.SourceUpdateLangView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SourceUpdateLangView.DefaultCellStyle = dataGridViewCellStyle2;
            this.SourceUpdateLangView.Location = new System.Drawing.Point(5, 23);
            this.SourceUpdateLangView.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.SourceUpdateLangView.Name = "SourceUpdateLangView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SourceUpdateLangView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.SourceUpdateLangView.RowTemplate.Height = 24;
            this.SourceUpdateLangView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SourceUpdateLangView.Size = new System.Drawing.Size(671, 428);
            this.SourceUpdateLangView.TabIndex = 8;
            // 
            // dataGridViewTextBoxColumn30
            // 
            this.dataGridViewTextBoxColumn30.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn30.HeaderText = "Status";
            this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
            this.dataGridViewTextBoxColumn30.ReadOnly = true;
            this.dataGridViewTextBoxColumn30.Visible = false;
            this.dataGridViewTextBoxColumn30.Width = 60;
            // 
            // dataGridViewTextBoxColumn29
            // 
            this.dataGridViewTextBoxColumn29.DataPropertyName = "LongDescription";
            this.dataGridViewTextBoxColumn29.HeaderText = "Long Description";
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            this.dataGridViewTextBoxColumn29.ReadOnly = true;
            this.dataGridViewTextBoxColumn29.Width = 200;
            // 
            // LongDescModified
            // 
            this.LongDescModified.DataPropertyName = "LongDescriptionModified";
            this.LongDescModified.HeaderText = "LongDescModified";
            this.LongDescModified.Name = "LongDescModified";
            this.LongDescModified.Visible = false;
            // 
            // DesciptionModified
            // 
            this.DesciptionModified.DataPropertyName = "DescriptionModified";
            this.DesciptionModified.HeaderText = "Desc Modified";
            this.DesciptionModified.Name = "DesciptionModified";
            this.DesciptionModified.Visible = false;
            // 
            // dataGridViewTextBoxColumn28
            // 
            this.dataGridViewTextBoxColumn28.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn28.HeaderText = "Description";
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            this.dataGridViewTextBoxColumn28.ReadOnly = true;
            this.dataGridViewTextBoxColumn28.Width = 215;
            // 
            // dataGridViewTextBoxColumn27
            // 
            this.dataGridViewTextBoxColumn27.DataPropertyName = "LocaleID";
            this.dataGridViewTextBoxColumn27.HeaderText = "Locale";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.ReadOnly = true;
            this.dataGridViewTextBoxColumn27.Width = 60;
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn26.HeaderText = "Code";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn25.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.ReadOnly = true;
            this.dataGridViewTextBoxColumn25.Width = 250;
            // 
            // dataGridViewCheckBoxColumn11
            // 
            this.dataGridViewCheckBoxColumn11.HeaderText = "";
            this.dataGridViewCheckBoxColumn11.Name = "dataGridViewCheckBoxColumn11";
            this.dataGridViewCheckBoxColumn11.Width = 25;
            // 
            // TargetUpdateLangView
            // 
            this.TargetUpdateLangView.AllowUserToAddRows = false;
            this.TargetUpdateLangView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TargetUpdateLangView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.TargetUpdateLangView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TargetUpdateLangView.DefaultCellStyle = dataGridViewCellStyle5;
            this.TargetUpdateLangView.Location = new System.Drawing.Point(675, 23);
            this.TargetUpdateLangView.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TargetUpdateLangView.Name = "TargetUpdateLangView";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TargetUpdateLangView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.TargetUpdateLangView.RowTemplate.Height = 24;
            this.TargetUpdateLangView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TargetUpdateLangView.Size = new System.Drawing.Size(414, 428);
            this.TargetUpdateLangView.TabIndex = 9;
            // 
            // dataGridViewTextBoxColumn37
            // 
            this.dataGridViewTextBoxColumn37.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn37.HeaderText = "Status";
            this.dataGridViewTextBoxColumn37.Name = "dataGridViewTextBoxColumn37";
            this.dataGridViewTextBoxColumn37.ReadOnly = true;
            this.dataGridViewTextBoxColumn37.Visible = false;
            this.dataGridViewTextBoxColumn37.Width = 60;
            // 
            // dataGridViewTextBoxColumn36
            // 
            this.dataGridViewTextBoxColumn36.DataPropertyName = "LongDescription";
            this.dataGridViewTextBoxColumn36.HeaderText = "Long Description";
            this.dataGridViewTextBoxColumn36.Name = "dataGridViewTextBoxColumn36";
            this.dataGridViewTextBoxColumn36.ReadOnly = true;
            this.dataGridViewTextBoxColumn36.Width = 220;
            // 
            // dataGridViewTextBoxColumn35
            // 
            this.dataGridViewTextBoxColumn35.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn35.HeaderText = "Description";
            this.dataGridViewTextBoxColumn35.Name = "dataGridViewTextBoxColumn35";
            this.dataGridViewTextBoxColumn35.ReadOnly = true;
            this.dataGridViewTextBoxColumn35.Width = 225;
            // 
            // dataGridViewTextBoxColumn34
            // 
            this.dataGridViewTextBoxColumn34.DataPropertyName = "LocaleID";
            this.dataGridViewTextBoxColumn34.HeaderText = "Locale";
            this.dataGridViewTextBoxColumn34.Name = "dataGridViewTextBoxColumn34";
            this.dataGridViewTextBoxColumn34.ReadOnly = true;
            this.dataGridViewTextBoxColumn34.Width = 60;
            // 
            // dataGridViewCheckBoxColumn12
            // 
            this.dataGridViewCheckBoxColumn12.HeaderText = "";
            this.dataGridViewCheckBoxColumn12.Name = "dataGridViewCheckBoxColumn12";
            this.dataGridViewCheckBoxColumn12.Visible = false;
            this.dataGridViewCheckBoxColumn12.Width = 25;
            // 
            // UpdateLangSelectAllCB
            // 
            this.UpdateLangSelectAllCB.AutoSize = true;
            this.UpdateLangSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UpdateLangSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateLangSelectAllCB.Location = new System.Drawing.Point(5, 5);
            this.UpdateLangSelectAllCB.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.UpdateLangSelectAllCB.Name = "UpdateLangSelectAllCB";
            this.UpdateLangSelectAllCB.Size = new System.Drawing.Size(80, 17);
            this.UpdateLangSelectAllCB.TabIndex = 12;
            this.UpdateLangSelectAllCB.Text = "Select All";
            this.UpdateLangSelectAllCB.UseVisualStyleBackColor = true;
            // 
            // NewLangTab
            // 
            this.NewLangTab.Location = new System.Drawing.Point(4, 22);
            this.NewLangTab.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewLangTab.Name = "NewLangTab";
            this.NewLangTab.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewLangTab.Size = new System.Drawing.Size(1094, 448);
            this.NewLangTab.TabIndex = 0;
            this.NewLangTab.Text = "New";
            this.NewLangTab.UseVisualStyleBackColor = true;
            // 
            // NewLangView
            // 
            this.NewLangView.AllowUserToAddRows = false;
            this.NewLangView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NewLangView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.NewLangView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.NewLangView.DefaultCellStyle = dataGridViewCellStyle8;
            this.NewLangView.Location = new System.Drawing.Point(5, 27);
            this.NewLangView.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewLangView.Name = "NewLangView";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NewLangView.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.NewLangView.RowTemplate.Height = 24;
            this.NewLangView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NewLangView.Size = new System.Drawing.Size(1087, 419);
            this.NewLangView.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn32
            // 
            this.dataGridViewTextBoxColumn32.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn32.HeaderText = "Status";
            this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
            this.dataGridViewTextBoxColumn32.ReadOnly = true;
            this.dataGridViewTextBoxColumn32.Visible = false;
            this.dataGridViewTextBoxColumn32.Width = 60;
            // 
            // LongDescription
            // 
            this.LongDescription.DataPropertyName = "LongDescription";
            this.LongDescription.HeaderText = "Long Description";
            this.LongDescription.Name = "LongDescription";
            this.LongDescription.ReadOnly = true;
            this.LongDescription.Width = 370;
            // 
            // Desc
            // 
            this.Desc.DataPropertyName = "Description";
            this.Desc.HeaderText = "Description";
            this.Desc.Name = "Desc";
            this.Desc.ReadOnly = true;
            this.Desc.Width = 250;
            // 
            // Locale
            // 
            this.Locale.DataPropertyName = "LocaleID";
            this.Locale.HeaderText = "Locale";
            this.Locale.Name = "Locale";
            this.Locale.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn24.HeaderText = "Code";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.ReadOnly = true;
            this.dataGridViewTextBoxColumn24.Width = 250;
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn23.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.ReadOnly = true;
            this.dataGridViewTextBoxColumn23.Width = 400;
            // 
            // dataGridViewCheckBoxColumn10
            // 
            this.dataGridViewCheckBoxColumn10.HeaderText = "";
            this.dataGridViewCheckBoxColumn10.Name = "dataGridViewCheckBoxColumn10";
            this.dataGridViewCheckBoxColumn10.Width = 25;
            // 
            // NewLangSelectAllCB
            // 
            this.NewLangSelectAllCB.AutoSize = true;
            this.NewLangSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewLangSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewLangSelectAllCB.Location = new System.Drawing.Point(5, 5);
            this.NewLangSelectAllCB.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewLangSelectAllCB.Name = "NewLangSelectAllCB";
            this.NewLangSelectAllCB.Size = new System.Drawing.Size(80, 17);
            this.NewLangSelectAllCB.TabIndex = 12;
            this.NewLangSelectAllCB.Text = "Select All";
            this.NewLangSelectAllCB.UseVisualStyleBackColor = true;
            // 
            // NewLangNewListCB
            // 
            this.NewLangNewListCB.AutoSize = true;
            this.NewLangNewListCB.BackColor = System.Drawing.Color.LightBlue;
            this.NewLangNewListCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewLangNewListCB.Location = new System.Drawing.Point(124, 5);
            this.NewLangNewListCB.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewLangNewListCB.Name = "NewLangNewListCB";
            this.NewLangNewListCB.Size = new System.Drawing.Size(201, 17);
            this.NewLangNewListCB.TabIndex = 25;
            this.NewLangNewListCB.Text = "New Languages - New Datalist";
            this.NewLangNewListCB.UseVisualStyleBackColor = false;
            // 
            // NewLangNewItemCB
            // 
            this.NewLangNewItemCB.AutoSize = true;
            this.NewLangNewItemCB.BackColor = System.Drawing.Color.LightGray;
            this.NewLangNewItemCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewLangNewItemCB.Location = new System.Drawing.Point(323, 5);
            this.NewLangNewItemCB.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewLangNewItemCB.Name = "NewLangNewItemCB";
            this.NewLangNewItemCB.Size = new System.Drawing.Size(188, 17);
            this.NewLangNewItemCB.TabIndex = 26;
            this.NewLangNewItemCB.Text = "New Languages - New Items";
            this.NewLangNewItemCB.UseVisualStyleBackColor = false;
            // 
            // NewLangExistingItemCB
            // 
            this.NewLangExistingItemCB.AutoSize = true;
            this.NewLangExistingItemCB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.NewLangExistingItemCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewLangExistingItemCB.Location = new System.Drawing.Point(503, 6);
            this.NewLangExistingItemCB.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewLangExistingItemCB.Name = "NewLangExistingItemCB";
            this.NewLangExistingItemCB.Size = new System.Drawing.Size(207, 17);
            this.NewLangExistingItemCB.TabIndex = 27;
            this.NewLangExistingItemCB.Text = "New Languages - Existing Items";
            this.NewLangExistingItemCB.UseVisualStyleBackColor = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn5.HeaderText = "Status";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Visible = false;
            this.dataGridViewTextBoxColumn5.Width = 60;
            // 
            // dataGridViewCheckBoxColumn6
            // 
            this.dataGridViewCheckBoxColumn6.DataPropertyName = "IsEditable";
            this.dataGridViewCheckBoxColumn6.HeaderText = "Editable";
            this.dataGridViewCheckBoxColumn6.Name = "dataGridViewCheckBoxColumn6";
            this.dataGridViewCheckBoxColumn6.ReadOnly = true;
            this.dataGridViewCheckBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn6.Visible = false;
            this.dataGridViewCheckBoxColumn6.Width = 70;
            // 
            // dataGridViewCheckBoxColumn5
            // 
            this.dataGridViewCheckBoxColumn5.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn5.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn5.Name = "dataGridViewCheckBoxColumn5";
            this.dataGridViewCheckBoxColumn5.ReadOnly = true;
            this.dataGridViewCheckBoxColumn5.Width = 60;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn3.HeaderText = "Description";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn2.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewCheckBoxColumn4
            // 
            this.dataGridViewCheckBoxColumn4.HeaderText = "";
            this.dataGridViewCheckBoxColumn4.Name = "dataGridViewCheckBoxColumn4";
            this.dataGridViewCheckBoxColumn4.Width = 25;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn8.HeaderText = "Status";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Visible = false;
            this.dataGridViewTextBoxColumn8.Width = 60;
            // 
            // dataGridViewCheckBoxColumn27
            // 
            this.dataGridViewCheckBoxColumn27.DataPropertyName = "IsEditable";
            this.dataGridViewCheckBoxColumn27.HeaderText = "Editable";
            this.dataGridViewCheckBoxColumn27.Name = "dataGridViewCheckBoxColumn27";
            this.dataGridViewCheckBoxColumn27.ReadOnly = true;
            this.dataGridViewCheckBoxColumn27.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn27.Visible = false;
            this.dataGridViewCheckBoxColumn27.Width = 70;
            // 
            // dataGridViewCheckBoxColumn26
            // 
            this.dataGridViewCheckBoxColumn26.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn26.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn26.Name = "dataGridViewCheckBoxColumn26";
            this.dataGridViewCheckBoxColumn26.ReadOnly = true;
            this.dataGridViewCheckBoxColumn26.Width = 60;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn7.HeaderText = "Description";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 200;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn6.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 200;
            // 
            // dataGridViewCheckBoxColumn25
            // 
            this.dataGridViewCheckBoxColumn25.HeaderText = "";
            this.dataGridViewCheckBoxColumn25.Name = "dataGridViewCheckBoxColumn25";
            this.dataGridViewCheckBoxColumn25.Width = 25;
            // 
            // PreviewUpdate
            // 
            this.PreviewUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreviewUpdate.Location = new System.Drawing.Point(928, 584);
            this.PreviewUpdate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.PreviewUpdate.Name = "PreviewUpdate";
            this.PreviewUpdate.Size = new System.Drawing.Size(132, 26);
            this.PreviewUpdate.TabIndex = 35;
            this.PreviewUpdate.Text = "Preview && Update";
            this.PreviewUpdate.UseVisualStyleBackColor = true;
            this.PreviewUpdate.Click += new System.EventHandler(this.PreviewUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(1071, 584);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 26);
            this.btnClose.TabIndex = 34;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // dataGridViewCheckBoxColumn19
            // 
            this.dataGridViewCheckBoxColumn19.HeaderText = "";
            this.dataGridViewCheckBoxColumn19.Name = "dataGridViewCheckBoxColumn19";
            this.dataGridViewCheckBoxColumn19.Visible = false;
            this.dataGridViewCheckBoxColumn19.Width = 25;
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn22.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.ReadOnly = true;
            this.dataGridViewTextBoxColumn22.Width = 250;
            // 
            // dataGridViewTextBoxColumn31
            // 
            this.dataGridViewTextBoxColumn31.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn31.HeaderText = "Description";
            this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
            this.dataGridViewTextBoxColumn31.ReadOnly = true;
            this.dataGridViewTextBoxColumn31.Width = 250;
            // 
            // dataGridViewCheckBoxColumn20
            // 
            this.dataGridViewCheckBoxColumn20.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn20.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn20.Name = "dataGridViewCheckBoxColumn20";
            this.dataGridViewCheckBoxColumn20.ReadOnly = true;
            this.dataGridViewCheckBoxColumn20.Width = 50;
            // 
            // dataGridViewCheckBoxColumn21
            // 
            this.dataGridViewCheckBoxColumn21.DataPropertyName = "IsEditable";
            this.dataGridViewCheckBoxColumn21.HeaderText = "Editable";
            this.dataGridViewCheckBoxColumn21.Name = "dataGridViewCheckBoxColumn21";
            this.dataGridViewCheckBoxColumn21.ReadOnly = true;
            this.dataGridViewCheckBoxColumn21.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn21.Visible = false;
            this.dataGridViewCheckBoxColumn21.Width = 55;
            // 
            // dataGridViewTextBoxColumn33
            // 
            this.dataGridViewTextBoxColumn33.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn33.HeaderText = "Status";
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            this.dataGridViewTextBoxColumn33.ReadOnly = true;
            this.dataGridViewTextBoxColumn33.Visible = false;
            this.dataGridViewTextBoxColumn33.Width = 50;
            // 
            // dataGridViewCheckBoxColumn22
            // 
            this.dataGridViewCheckBoxColumn22.HeaderText = "";
            this.dataGridViewCheckBoxColumn22.Name = "dataGridViewCheckBoxColumn22";
            this.dataGridViewCheckBoxColumn22.Visible = false;
            this.dataGridViewCheckBoxColumn22.Width = 25;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn14.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Width = 200;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.DataPropertyName = "Html";
            this.dataGridViewTextBoxColumn15.HeaderText = "Html";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.Width = 250;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.DataPropertyName = "LocaleID";
            this.dataGridViewTextBoxColumn16.HeaderText = "Locale";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.Width = 50;
            // 
            // dataGridViewCheckBoxColumn23
            // 
            this.dataGridViewCheckBoxColumn23.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn23.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn23.Name = "dataGridViewCheckBoxColumn23";
            this.dataGridViewCheckBoxColumn23.ReadOnly = true;
            this.dataGridViewCheckBoxColumn23.Width = 60;
            // 
            // dataGridViewCheckBoxColumn24
            // 
            this.dataGridViewCheckBoxColumn24.DataPropertyName = "IsEditable";
            this.dataGridViewCheckBoxColumn24.HeaderText = "Editable";
            this.dataGridViewCheckBoxColumn24.Name = "dataGridViewCheckBoxColumn24";
            this.dataGridViewCheckBoxColumn24.ReadOnly = true;
            this.dataGridViewCheckBoxColumn24.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn24.Visible = false;
            this.dataGridViewCheckBoxColumn24.Width = 70;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn18.HeaderText = "Status";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.ReadOnly = true;
            this.dataGridViewTextBoxColumn18.Visible = false;
            this.dataGridViewTextBoxColumn18.Width = 60;
            // 
            // HtmlBlockDiff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 619);
            this.Controls.Add(this.PreviewUpdate);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.diffHtmlBlk);
            this.Controls.Add(this.moduleList);
            this.Controls.Add(this.label4);
            this.Name = "HtmlBlockDiff";
            this.Text = "HtmlBlockDiff";
            this.HtmlBlk.ResumeLayout(false);
            this.ItemsTab.ResumeLayout(false);
            this.NewItemsPage.ResumeLayout(false);
            this.NewItemsPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newHtmlBlkView)).EndInit();
            this.UpdateItemsTab.ResumeLayout(false);
            this.UpdateItemsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetHtmlBlk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceHtmlBlk)).EndInit();
            this.diffHtmlBlk.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabNewLang.ResumeLayout(false);
            this.tabNewLang.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newHtmlLangView)).EndInit();
            this.tabUpdateLang.ResumeLayout(false);
            this.tabUpdateLang.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetHtmlBlkLangView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourceHtmlBlkLangView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceUpdateLangView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetUpdateLangView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewLangView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage HtmlBlk;
        private System.Windows.Forms.TabControl ItemsTab;
        private System.Windows.Forms.TabPage NewItemsPage;
        private System.Windows.Forms.DataGridView newHtmlBlkView;
        private System.Windows.Forms.CheckBox NewHtmlBlkSelectAllCB;
        private System.Windows.Forms.TabControl diffHtmlBlk;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabNewLang;
        private System.Windows.Forms.DataGridView newHtmlLangView;
        private System.Windows.Forms.CheckBox NewHtmlBlkLangSelectAllCB;
        private System.Windows.Forms.TabPage tabUpdateLang;
        private System.Windows.Forms.CheckBox UpdateHtmlBlkLangSelectAllCB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage2;
        
        private System.Windows.Forms.TabPage UpdateLangTab;
        private System.Windows.Forms.DataGridView SourceUpdateLangView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongDescModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn DesciptionModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn11;
        private System.Windows.Forms.DataGridView TargetUpdateLangView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn12;
        private System.Windows.Forms.CheckBox UpdateLangSelectAllCB;
        private System.Windows.Forms.TabPage NewLangTab;
        private System.Windows.Forms.DataGridView NewLangView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Locale;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn10;
        private System.Windows.Forms.CheckBox NewLangSelectAllCB;
        private System.Windows.Forms.CheckBox NewLangNewListCB;
        private System.Windows.Forms.CheckBox NewLangNewItemCB;
        private System.Windows.Forms.CheckBox NewLangExistingItemCB;
        private System.Windows.Forms.CheckBox NewLangNewHtmlBlkCB;
        private System.Windows.Forms.CheckBox NewLangExistingHtmlBlkCB;
        private System.Windows.Forms.DataGridView targetHtmlBlkLangView;
        private System.Windows.Forms.DataGridView sourceHtmlBlkLangView;
        private System.Windows.Forms.TabPage UpdateItemsTab;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn27;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn25;
        private System.Windows.Forms.CheckBox UpdateHtmlBlkSelectAllCB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView SourceHtmlBlk;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView TargetHtmlBlk;
        private System.Windows.Forms.Button PreviewUpdate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnIncludeHtmlBlk;
        private System.Windows.Forms.Button btnIncludeHtmlLang;
        private new System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn14;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn54;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn56;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn55;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn17;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn63;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn htmlModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsActiveModified;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn20;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn22;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn23;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
    }
}