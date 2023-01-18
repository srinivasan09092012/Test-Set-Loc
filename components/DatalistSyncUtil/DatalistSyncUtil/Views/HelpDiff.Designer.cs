//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace DatalistSyncUtil
{
    public partial class HelpDiff
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
            this.Help = new System.Windows.Forms.TabPage();
            this.btnIncludeHelp = new System.Windows.Forms.Button();
            this.ItemsTab = new System.Windows.Forms.TabControl();
            this.NewHelpPage = new System.Windows.Forms.TabPage();
            this.newHelpView = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NewHelpSelectAllCB = new System.Windows.Forms.CheckBox();
            this.UpdateItemsTab = new System.Windows.Forms.TabPage();
            this.TargetHelp = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn19 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn20 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn21 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateHelpSelectAllCB = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SourceHelp = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn13 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.diffHelpMain = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnIncludeHelpLang = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.NewLangExistingHelpCB = new System.Windows.Forms.CheckBox();
            this.NewLangNewHelpCB = new System.Windows.Forms.CheckBox();
            this.newHelpLangView = new System.Windows.Forms.DataGridView();
            this.NewHelpLangSelectAllCB = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.sourceHelpLangView = new System.Windows.Forms.DataGridView();
            this.targetHelpLangView = new System.Windows.Forms.DataGridView();
            this.UpdateHelpLangSelectAllCB = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
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
            this.tabUpdateLang = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.UpdateHtmlBlkLangSelectAllCB = new System.Windows.Forms.CheckBox();
            this.sourceHtmlBlkLangView = new System.Windows.Forms.DataGridView();
            this.IsActiveModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.htmlModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn9 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn8 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.targetHtmlBlkLangView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn24 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn23 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn22 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabNewLang = new System.Windows.Forms.TabPage();
            this.NewHtmlBlkLangSelectAllCB = new System.Windows.Forms.CheckBox();
            this.newHtmlLangView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn63 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn18 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn17 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn55 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn56 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn54 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn16 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NewLangNewHtmlBlkCB = new System.Windows.Forms.CheckBox();
            this.NewLangExistingHtmlBlkCB = new System.Windows.Forms.CheckBox();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn14 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn30 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn43 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HelpModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TitleModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn44 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn15 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn41 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn28 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn29 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn48 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Help.SuspendLayout();
            this.ItemsTab.SuspendLayout();
            this.NewHelpPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newHelpView)).BeginInit();
            this.UpdateItemsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceHelp)).BeginInit();
            this.diffHelpMain.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newHelpLangView)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourceHelpLangView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetHelpLangView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceUpdateLangView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetUpdateLangView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewLangView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourceHtmlBlkLangView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetHtmlBlkLangView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newHtmlLangView)).BeginInit();
            this.SuspendLayout();
            // 
            // moduleList
            // 
            this.moduleList.DisplayMember = "ModuleName";
            this.moduleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moduleList.FormattingEnabled = true;
            this.moduleList.Location = new System.Drawing.Point(103, 15);
            this.moduleList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.moduleList.Name = "moduleList";
            this.moduleList.Size = new System.Drawing.Size(277, 24);
            this.moduleList.Sorted = true;
            this.moduleList.TabIndex = 30;
            this.moduleList.ValueMember = "TenantModuleID";
            this.moduleList.SelectedIndexChanged += new System.EventHandler(this.ModuleList_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 18);
            this.label4.TabIndex = 29;
            this.label4.Text = "Module:";
            // 
            // Help
            // 
            this.Help.Controls.Add(this.btnIncludeHelp);
            this.Help.Controls.Add(this.ItemsTab);
            this.Help.Location = new System.Drawing.Point(4, 25);
            this.Help.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Help.Name = "Help";
            this.Help.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Help.Size = new System.Drawing.Size(1697, 634);
            this.Help.TabIndex = 1;
            this.Help.Text = "Help";
            this.Help.UseVisualStyleBackColor = true;
            // 
            // btnIncludeHelp
            // 
            this.btnIncludeHelp.Location = new System.Drawing.Point(5, 593);
            this.btnIncludeHelp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnIncludeHelp.Name = "btnIncludeHelp";
            this.btnIncludeHelp.Size = new System.Drawing.Size(96, 31);
            this.btnIncludeHelp.TabIndex = 19;
            this.btnIncludeHelp.Text = "Include";
            this.btnIncludeHelp.UseVisualStyleBackColor = true;
            this.btnIncludeHelp.Click += new System.EventHandler(this.BtnIncludeHelp_Click);
            // 
            // ItemsTab
            // 
            this.ItemsTab.Controls.Add(this.NewHelpPage);
            this.ItemsTab.Controls.Add(this.UpdateItemsTab);
            this.ItemsTab.Location = new System.Drawing.Point(5, 7);
            this.ItemsTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ItemsTab.Name = "ItemsTab";
            this.ItemsTab.SelectedIndex = 0;
            this.ItemsTab.Size = new System.Drawing.Size(1620, 582);
            this.ItemsTab.TabIndex = 0;
            // 
            // NewHelpPage
            // 
            this.NewHelpPage.Controls.Add(this.newHelpView);
            this.NewHelpPage.Controls.Add(this.NewHelpSelectAllCB);
            this.NewHelpPage.Location = new System.Drawing.Point(4, 25);
            this.NewHelpPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewHelpPage.Name = "NewHelpPage";
            this.NewHelpPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewHelpPage.Size = new System.Drawing.Size(1612, 553);
            this.NewHelpPage.TabIndex = 0;
            this.NewHelpPage.Text = "New Help";
            this.NewHelpPage.UseVisualStyleBackColor = true;
            // 
            // newHelpView
            // 
            this.newHelpView.AllowUserToAddRows = false;
            this.newHelpView.AllowUserToDeleteRows = false;
            this.newHelpView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.newHelpView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewCheckBoxColumn2});
            this.newHelpView.Location = new System.Drawing.Point(16, 33);
            this.newHelpView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.newHelpView.Name = "newHelpView";
            this.newHelpView.RowTemplate.Height = 24;
            this.newHelpView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.newHelpView.Size = new System.Drawing.Size(1329, 510);
            this.newHelpView.TabIndex = 6;
            // 
            // Select
            // 
            this.Select.HeaderText = "";
            this.Select.Name = "Select";
            this.Select.Width = 25;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "HelpNodeNM";
            this.dataGridViewTextBoxColumn1.HeaderText = "HelpNodeNM";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 400;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn2.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.ReadOnly = true;
            this.dataGridViewCheckBoxColumn2.Width = 60;
            // 
            // NewHelpSelectAllCB
            // 
            this.NewHelpSelectAllCB.AutoSize = true;
            this.NewHelpSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewHelpSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewHelpSelectAllCB.Location = new System.Drawing.Point(3, 6);
            this.NewHelpSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewHelpSelectAllCB.Name = "NewHelpSelectAllCB";
            this.NewHelpSelectAllCB.Size = new System.Drawing.Size(98, 21);
            this.NewHelpSelectAllCB.TabIndex = 8;
            this.NewHelpSelectAllCB.Text = "Select All";
            this.NewHelpSelectAllCB.UseVisualStyleBackColor = true;
            this.NewHelpSelectAllCB.CheckedChanged += new System.EventHandler(this.NewHelpSelectAllCB_CheckedChanged);
            // 
            // UpdateItemsTab
            // 
            this.UpdateItemsTab.Controls.Add(this.TargetHelp);
            this.UpdateItemsTab.Controls.Add(this.UpdateHelpSelectAllCB);
            this.UpdateItemsTab.Controls.Add(this.label6);
            this.UpdateItemsTab.Controls.Add(this.SourceHelp);
            this.UpdateItemsTab.Controls.Add(this.label2);
            this.UpdateItemsTab.Controls.Add(this.label7);
            this.UpdateItemsTab.Controls.Add(this.label1);
            this.UpdateItemsTab.Location = new System.Drawing.Point(4, 25);
            this.UpdateItemsTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UpdateItemsTab.Name = "UpdateItemsTab";
            this.UpdateItemsTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UpdateItemsTab.Size = new System.Drawing.Size(1612, 553);
            this.UpdateItemsTab.TabIndex = 1;
            this.UpdateItemsTab.Text = "Update Help";
            this.UpdateItemsTab.UseVisualStyleBackColor = true;
            // 
            // TargetHelp
            // 
            this.TargetHelp.AllowUserToAddRows = false;
            this.TargetHelp.AllowUserToDeleteRows = false;
            this.TargetHelp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TargetHelp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn19,
            this.dataGridViewTextBoxColumn22,
            this.dataGridViewCheckBoxColumn20,
            this.dataGridViewCheckBoxColumn21,
            this.dataGridViewTextBoxColumn33});
            this.TargetHelp.Location = new System.Drawing.Point(569, 34);
            this.TargetHelp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TargetHelp.Name = "TargetHelp";
            this.TargetHelp.RowTemplate.Height = 24;
            this.TargetHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TargetHelp.Size = new System.Drawing.Size(632, 510);
            this.TargetHelp.TabIndex = 16;
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
            this.dataGridViewTextBoxColumn22.DataPropertyName = "HelpNodeNM";
            this.dataGridViewTextBoxColumn22.HeaderText = "HelpNodeNM";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.ReadOnly = true;
            this.dataGridViewTextBoxColumn22.Width = 250;
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
            // UpdateHelpSelectAllCB
            // 
            this.UpdateHelpSelectAllCB.AutoSize = true;
            this.UpdateHelpSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateHelpSelectAllCB.Location = new System.Drawing.Point(85, 6);
            this.UpdateHelpSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UpdateHelpSelectAllCB.Name = "UpdateHelpSelectAllCB";
            this.UpdateHelpSelectAllCB.Size = new System.Drawing.Size(98, 21);
            this.UpdateHelpSelectAllCB.TabIndex = 15;
            this.UpdateHelpSelectAllCB.Text = "Select All";
            this.UpdateHelpSelectAllCB.UseVisualStyleBackColor = true;
            this.UpdateHelpSelectAllCB.CheckedChanged += new System.EventHandler(this.UpdateHelpSelectAllCB_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(828, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "Target:";
            // 
            // SourceHelp
            // 
            this.SourceHelp.AllowUserToAddRows = false;
            this.SourceHelp.AllowUserToDeleteRows = false;
            this.SourceHelp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SourceHelp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn13,
            this.dataGridViewTextBoxColumn19,
            this.Active});
            this.SourceHelp.Location = new System.Drawing.Point(5, 34);
            this.SourceHelp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SourceHelp.Name = "SourceHelp";
            this.SourceHelp.RowTemplate.Height = 24;
            this.SourceHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SourceHelp.Size = new System.Drawing.Size(560, 510);
            this.SourceHelp.TabIndex = 12;
            // 
            // dataGridViewCheckBoxColumn13
            // 
            this.dataGridViewCheckBoxColumn13.HeaderText = "";
            this.dataGridViewCheckBoxColumn13.Name = "dataGridViewCheckBoxColumn13";
            this.dataGridViewCheckBoxColumn13.Width = 25;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.DataPropertyName = "HelpNodeNM";
            this.dataGridViewTextBoxColumn19.HeaderText = "HelpNodeNM";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.Width = 250;
            // 
            // Active
            // 
            this.Active.DataPropertyName = "IsActive";
            this.Active.HeaderText = "Active";
            this.Active.Name = "Active";
            this.Active.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(992, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 20);
            this.label2.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(5, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "Source:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 9;
            // 
            // diffHelpMain
            // 
            this.diffHelpMain.Controls.Add(this.Help);
            this.diffHelpMain.Controls.Add(this.tabPage3);
            this.diffHelpMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diffHelpMain.Location = new System.Drawing.Point(23, 48);
            this.diffHelpMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.diffHelpMain.Name = "diffHelpMain";
            this.diffHelpMain.SelectedIndex = 0;
            this.diffHelpMain.Size = new System.Drawing.Size(1705, 663);
            this.diffHelpMain.TabIndex = 33;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnIncludeHelpLang);
            this.tabPage3.Controls.Add(this.tabControl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(1697, 634);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "HelpLanguage";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnIncludeHelpLang
            // 
            this.btnIncludeHelpLang.Location = new System.Drawing.Point(3, 601);
            this.btnIncludeHelpLang.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnIncludeHelpLang.Name = "btnIncludeHelpLang";
            this.btnIncludeHelpLang.Size = new System.Drawing.Size(96, 31);
            this.btnIncludeHelpLang.TabIndex = 20;
            this.btnIncludeHelpLang.Text = "Include";
            this.btnIncludeHelpLang.UseVisualStyleBackColor = true;
            this.btnIncludeHelpLang.Click += new System.EventHandler(this.BtnIncludeHelpLang_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(4, 23);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1684, 574);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.NewLangExistingHelpCB);
            this.tabPage1.Controls.Add(this.NewLangNewHelpCB);
            this.tabPage1.Controls.Add(this.newHelpLangView);
            this.tabPage1.Controls.Add(this.NewHelpLangSelectAllCB);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(1676, 545);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "New Lang";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // NewLangExistingHelpCB
            // 
            this.NewLangExistingHelpCB.AutoSize = true;
            this.NewLangExistingHelpCB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.NewLangExistingHelpCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewLangExistingHelpCB.Location = new System.Drawing.Point(417, 7);
            this.NewLangExistingHelpCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewLangExistingHelpCB.Name = "NewLangExistingHelpCB";
            this.NewLangExistingHelpCB.Size = new System.Drawing.Size(293, 21);
            this.NewLangExistingHelpCB.TabIndex = 28;
            this.NewLangExistingHelpCB.Text = "New Languages - Existing HelpNode";
            this.NewLangExistingHelpCB.UseVisualStyleBackColor = false;
            this.NewLangExistingHelpCB.CheckedChanged += new System.EventHandler(this.NewLangExistingHelpCB_CheckedChanged);
            // 
            // NewLangNewHelpCB
            // 
            this.NewLangNewHelpCB.AutoSize = true;
            this.NewLangNewHelpCB.BackColor = System.Drawing.Color.LightBlue;
            this.NewLangNewHelpCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewLangNewHelpCB.Location = new System.Drawing.Point(127, 5);
            this.NewLangNewHelpCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewLangNewHelpCB.Name = "NewLangNewHelpCB";
            this.NewLangNewHelpCB.Size = new System.Drawing.Size(267, 21);
            this.NewLangNewHelpCB.TabIndex = 26;
            this.NewLangNewHelpCB.Text = "New Languages - New HelpNode";
            this.NewLangNewHelpCB.UseVisualStyleBackColor = false;
            this.NewLangNewHelpCB.CheckedChanged += new System.EventHandler(this.NewLangNewHelpCB_CheckedChanged);
            // 
            // newHelpLangView
            // 
            this.newHelpLangView.AllowUserToAddRows = false;
            this.newHelpLangView.AllowUserToDeleteRows = false;
            this.newHelpLangView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.newHelpLangView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn12,
            this.Title1,
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn20,
            this.dataGridViewCheckBoxColumn3,
            this.dataGridViewCheckBoxColumn14,
            this.dataGridViewTextBoxColumn21});
            this.newHelpLangView.Location = new System.Drawing.Point(5, 34);
            this.newHelpLangView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.newHelpLangView.Name = "newHelpLangView";
            this.newHelpLangView.RowTemplate.Height = 24;
            this.newHelpLangView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.newHelpLangView.Size = new System.Drawing.Size(1351, 522);
            this.newHelpLangView.TabIndex = 6;
            this.newHelpLangView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.NewHelpLangView_RowsAdded);
            // 
            // NewHelpLangSelectAllCB
            // 
            this.NewHelpLangSelectAllCB.AutoSize = true;
            this.NewHelpLangSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewHelpLangSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewHelpLangSelectAllCB.Location = new System.Drawing.Point(3, 6);
            this.NewHelpLangSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewHelpLangSelectAllCB.Name = "NewHelpLangSelectAllCB";
            this.NewHelpLangSelectAllCB.Size = new System.Drawing.Size(98, 21);
            this.NewHelpLangSelectAllCB.TabIndex = 8;
            this.NewHelpLangSelectAllCB.Text = "Select All";
            this.NewHelpLangSelectAllCB.UseVisualStyleBackColor = true;
            this.NewHelpLangSelectAllCB.CheckedChanged += new System.EventHandler(this.NewHelpLangSelectAllCB_CheckedChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.sourceHelpLangView);
            this.tabPage4.Controls.Add(this.targetHelpLangView);
            this.tabPage4.Controls.Add(this.UpdateHelpLangSelectAllCB);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage4.Size = new System.Drawing.Size(1676, 545);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Update Lang";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // sourceHelpLangView
            // 
            this.sourceHelpLangView.AllowUserToAddRows = false;
            this.sourceHelpLangView.AllowUserToDeleteRows = false;
            this.sourceHelpLangView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sourceHelpLangView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn30,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn31,
            this.dataGridViewTextBoxColumn42,
            this.dataGridViewTextBoxColumn43,
            this.HelpModified,
            this.TitleModified,
            this.dataGridViewTextBoxColumn44,
            this.IsActive});
            this.sourceHelpLangView.Location = new System.Drawing.Point(3, 39);
            this.sourceHelpLangView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sourceHelpLangView.Name = "sourceHelpLangView";
            this.sourceHelpLangView.RowTemplate.Height = 24;
            this.sourceHelpLangView.Size = new System.Drawing.Size(819, 510);
            this.sourceHelpLangView.TabIndex = 14;
            this.sourceHelpLangView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.SourceHelpLangView_RowAdded);
            this.sourceHelpLangView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SourceHelpLangView_Scroll);
            // 
            // targetHelpLangView
            // 
            this.targetHelpLangView.AllowUserToAddRows = false;
            this.targetHelpLangView.AllowUserToDeleteRows = false;
            this.targetHelpLangView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.targetHelpLangView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn15,
            this.dataGridViewTextBoxColumn38,
            this.dataGridViewTextBoxColumn39,
            this.dataGridViewTextBoxColumn40,
            this.dataGridViewTextBoxColumn41,
            this.dataGridViewCheckBoxColumn28,
            this.dataGridViewCheckBoxColumn29,
            this.dataGridViewTextBoxColumn48});
            this.targetHelpLangView.Location = new System.Drawing.Point(818, 39);
            this.targetHelpLangView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.targetHelpLangView.Name = "targetHelpLangView";
            this.targetHelpLangView.RowTemplate.Height = 24;
            this.targetHelpLangView.Size = new System.Drawing.Size(560, 510);
            this.targetHelpLangView.TabIndex = 13;
            this.targetHelpLangView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TargetHelpLangView_Scroll);
            // 
            // UpdateHelpLangSelectAllCB
            // 
            this.UpdateHelpLangSelectAllCB.AutoSize = true;
            this.UpdateHelpLangSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateHelpLangSelectAllCB.Location = new System.Drawing.Point(87, 15);
            this.UpdateHelpLangSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UpdateHelpLangSelectAllCB.Name = "UpdateHelpLangSelectAllCB";
            this.UpdateHelpLangSelectAllCB.Size = new System.Drawing.Size(98, 21);
            this.UpdateHelpLangSelectAllCB.TabIndex = 11;
            this.UpdateHelpLangSelectAllCB.Text = "Select All";
            this.UpdateHelpLangSelectAllCB.UseVisualStyleBackColor = true;
            this.UpdateHelpLangSelectAllCB.CheckedChanged += new System.EventHandler(this.UpdateHelpLangSelectAllCB_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(839, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "Target:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 20);
            this.label9.TabIndex = 9;
            this.label9.Text = "Source:";
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
            this.PreviewUpdate.Location = new System.Drawing.Point(1237, 719);
            this.PreviewUpdate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PreviewUpdate.Name = "PreviewUpdate";
            this.PreviewUpdate.Size = new System.Drawing.Size(176, 32);
            this.PreviewUpdate.TabIndex = 35;
            this.PreviewUpdate.Text = "Preview && Update";
            this.PreviewUpdate.UseVisualStyleBackColor = true;
            this.PreviewUpdate.Click += new System.EventHandler(this.PreviewUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(1428, 719);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 32);
            this.btnClose.TabIndex = 34;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // tabUpdateLang
            // 
            this.tabUpdateLang.Location = new System.Drawing.Point(4, 29);
            this.tabUpdateLang.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabUpdateLang.Name = "tabUpdateLang";
            this.tabUpdateLang.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabUpdateLang.Size = new System.Drawing.Size(1886, 705);
            this.tabUpdateLang.TabIndex = 1;
            this.tabUpdateLang.Text = "Update Lang";
            this.tabUpdateLang.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 25);
            this.label5.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(944, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 25);
            this.label3.TabIndex = 10;
            // 
            // UpdateHtmlBlkLangSelectAllCB
            // 
            this.UpdateHtmlBlkLangSelectAllCB.AutoSize = true;
            this.UpdateHtmlBlkLangSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateHtmlBlkLangSelectAllCB.Location = new System.Drawing.Point(98, 18);
            this.UpdateHtmlBlkLangSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.UpdateHtmlBlkLangSelectAllCB.Name = "UpdateHtmlBlkLangSelectAllCB";
            this.UpdateHtmlBlkLangSelectAllCB.Size = new System.Drawing.Size(111, 24);
            this.UpdateHtmlBlkLangSelectAllCB.TabIndex = 11;
            this.UpdateHtmlBlkLangSelectAllCB.Text = "Select All";
            this.UpdateHtmlBlkLangSelectAllCB.UseVisualStyleBackColor = true;
            this.UpdateHtmlBlkLangSelectAllCB.CheckedChanged += new System.EventHandler(this.UpdateHtmlBlkLangSelectAllCB_CheckedChanged);
            // 
            // sourceHtmlBlkLangView
            // 
            this.sourceHtmlBlkLangView.AllowUserToAddRows = false;
            this.sourceHtmlBlkLangView.AllowUserToDeleteRows = false;
            this.sourceHtmlBlkLangView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sourceHtmlBlkLangView.Location = new System.Drawing.Point(6, 49);
            this.sourceHtmlBlkLangView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.sourceHtmlBlkLangView.Name = "sourceHtmlBlkLangView";
            this.sourceHtmlBlkLangView.RowTemplate.Height = 24;
            this.sourceHtmlBlkLangView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sourceHtmlBlkLangView.Size = new System.Drawing.Size(986, 652);
            this.sourceHtmlBlkLangView.TabIndex = 12;
            this.sourceHtmlBlkLangView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.SourceHelpLangView_RowAdded);
            this.sourceHtmlBlkLangView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SourceHelpLangView_Scroll);
            // 
            // IsActiveModified
            // 
            this.IsActiveModified.DataPropertyName = "IsActiveModified";
            this.IsActiveModified.HeaderText = "IsActiveModified";
            this.IsActiveModified.Name = "IsActiveModified";
            this.IsActiveModified.Visible = false;
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
            // htmlModified
            // 
            this.htmlModified.DataPropertyName = "htmlModified";
            this.htmlModified.HeaderText = "htmlModified";
            this.htmlModified.Name = "htmlModified";
            this.htmlModified.Visible = false;
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
            // dataGridViewCheckBoxColumn8
            // 
            this.dataGridViewCheckBoxColumn8.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn8.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn8.Name = "dataGridViewCheckBoxColumn8";
            this.dataGridViewCheckBoxColumn8.ReadOnly = true;
            this.dataGridViewCheckBoxColumn8.Width = 60;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "LocaleID";
            this.dataGridViewTextBoxColumn11.HeaderText = "Locale";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 50;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Html";
            this.dataGridViewTextBoxColumn10.HeaderText = "Html";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 250;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn9.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 200;
            // 
            // dataGridViewCheckBoxColumn7
            // 
            this.dataGridViewCheckBoxColumn7.HeaderText = "";
            this.dataGridViewCheckBoxColumn7.Name = "dataGridViewCheckBoxColumn7";
            this.dataGridViewCheckBoxColumn7.Width = 25;
            // 
            // targetHtmlBlkLangView
            // 
            this.targetHtmlBlkLangView.AllowUserToAddRows = false;
            this.targetHtmlBlkLangView.AllowUserToDeleteRows = false;
            this.targetHtmlBlkLangView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.targetHtmlBlkLangView.Location = new System.Drawing.Point(948, 49);
            this.targetHtmlBlkLangView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.targetHtmlBlkLangView.Name = "targetHtmlBlkLangView";
            this.targetHtmlBlkLangView.RowTemplate.Height = 24;
            this.targetHtmlBlkLangView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.targetHtmlBlkLangView.Size = new System.Drawing.Size(940, 652);
            this.targetHtmlBlkLangView.TabIndex = 13;
            this.targetHtmlBlkLangView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TargetHelpLangView_Scroll);
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
            // dataGridViewCheckBoxColumn23
            // 
            this.dataGridViewCheckBoxColumn23.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn23.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn23.Name = "dataGridViewCheckBoxColumn23";
            this.dataGridViewCheckBoxColumn23.ReadOnly = true;
            this.dataGridViewCheckBoxColumn23.Width = 60;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.DataPropertyName = "LocaleID";
            this.dataGridViewTextBoxColumn16.HeaderText = "Locale";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.Width = 50;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.DataPropertyName = "Html";
            this.dataGridViewTextBoxColumn15.HeaderText = "Html";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.Width = 250;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn14.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Width = 200;
            // 
            // dataGridViewCheckBoxColumn22
            // 
            this.dataGridViewCheckBoxColumn22.HeaderText = "";
            this.dataGridViewCheckBoxColumn22.Name = "dataGridViewCheckBoxColumn22";
            this.dataGridViewCheckBoxColumn22.Visible = false;
            this.dataGridViewCheckBoxColumn22.Width = 25;
            // 
            // tabNewLang
            // 
            this.tabNewLang.Location = new System.Drawing.Point(4, 29);
            this.tabNewLang.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabNewLang.Name = "tabNewLang";
            this.tabNewLang.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabNewLang.Size = new System.Drawing.Size(1886, 705);
            this.tabNewLang.TabIndex = 0;
            this.tabNewLang.Text = "New Lang";
            this.tabNewLang.UseVisualStyleBackColor = true;
            // 
            // NewHtmlBlkLangSelectAllCB
            // 
            this.NewHtmlBlkLangSelectAllCB.AutoSize = true;
            this.NewHtmlBlkLangSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewHtmlBlkLangSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewHtmlBlkLangSelectAllCB.Location = new System.Drawing.Point(3, 8);
            this.NewHtmlBlkLangSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewHtmlBlkLangSelectAllCB.Name = "NewHtmlBlkLangSelectAllCB";
            this.NewHtmlBlkLangSelectAllCB.Size = new System.Drawing.Size(111, 24);
            this.NewHtmlBlkLangSelectAllCB.TabIndex = 8;
            this.NewHtmlBlkLangSelectAllCB.Text = "Select All";
            this.NewHtmlBlkLangSelectAllCB.UseVisualStyleBackColor = true;
            this.NewHtmlBlkLangSelectAllCB.CheckedChanged += new System.EventHandler(this.NewHtmlBlkLangSelectAllCB_CheckedChanged);
            // 
            // newHtmlLangView
            // 
            this.newHtmlLangView.AllowUserToAddRows = false;
            this.newHtmlLangView.AllowUserToDeleteRows = false;
            this.newHtmlLangView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.newHtmlLangView.Location = new System.Drawing.Point(6, 43);
            this.newHtmlLangView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.newHtmlLangView.Name = "newHtmlLangView";
            this.newHtmlLangView.RowTemplate.Height = 24;
            this.newHtmlLangView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.newHtmlLangView.Size = new System.Drawing.Size(1520, 652);
            this.newHtmlLangView.TabIndex = 6;
            this.newHtmlLangView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.NewHelpLangView_RowsAdded);
            // 
            // dataGridViewTextBoxColumn63
            // 
            this.dataGridViewTextBoxColumn63.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn63.HeaderText = "Status";
            this.dataGridViewTextBoxColumn63.Name = "dataGridViewTextBoxColumn63";
            this.dataGridViewTextBoxColumn63.ReadOnly = true;
            this.dataGridViewTextBoxColumn63.Width = 90;
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
            // dataGridViewCheckBoxColumn17
            // 
            this.dataGridViewCheckBoxColumn17.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn17.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn17.Name = "dataGridViewCheckBoxColumn17";
            this.dataGridViewCheckBoxColumn17.ReadOnly = true;
            this.dataGridViewCheckBoxColumn17.Width = 60;
            // 
            // dataGridViewTextBoxColumn55
            // 
            this.dataGridViewTextBoxColumn55.DataPropertyName = "LocaleID";
            this.dataGridViewTextBoxColumn55.HeaderText = "LocaleID";
            this.dataGridViewTextBoxColumn55.Name = "dataGridViewTextBoxColumn55";
            this.dataGridViewTextBoxColumn55.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn56
            // 
            this.dataGridViewTextBoxColumn56.DataPropertyName = "Html";
            this.dataGridViewTextBoxColumn56.HeaderText = "Html";
            this.dataGridViewTextBoxColumn56.Name = "dataGridViewTextBoxColumn56";
            this.dataGridViewTextBoxColumn56.ReadOnly = true;
            this.dataGridViewTextBoxColumn56.Width = 400;
            // 
            // dataGridViewTextBoxColumn54
            // 
            this.dataGridViewTextBoxColumn54.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn54.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn54.Name = "dataGridViewTextBoxColumn54";
            this.dataGridViewTextBoxColumn54.ReadOnly = true;
            this.dataGridViewTextBoxColumn54.Width = 300;
            // 
            // dataGridViewCheckBoxColumn16
            // 
            this.dataGridViewCheckBoxColumn16.HeaderText = "";
            this.dataGridViewCheckBoxColumn16.Name = "dataGridViewCheckBoxColumn16";
            this.dataGridViewCheckBoxColumn16.Width = 25;
            // 
            // NewLangNewHtmlBlkCB
            // 
            this.NewLangNewHtmlBlkCB.AutoSize = true;
            this.NewLangNewHtmlBlkCB.BackColor = System.Drawing.Color.LightBlue;
            this.NewLangNewHtmlBlkCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewLangNewHtmlBlkCB.Location = new System.Drawing.Point(142, 6);
            this.NewLangNewHtmlBlkCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewLangNewHtmlBlkCB.Name = "NewLangNewHtmlBlkCB";
            this.NewLangNewHtmlBlkCB.Size = new System.Drawing.Size(280, 24);
            this.NewLangNewHtmlBlkCB.TabIndex = 26;
            this.NewLangNewHtmlBlkCB.Text = "New Languages - New HtmlBlk";
            this.NewLangNewHtmlBlkCB.UseVisualStyleBackColor = false;
            this.NewLangNewHtmlBlkCB.CheckedChanged += new System.EventHandler(this.NewLangNewHtmlBlkCB_CheckedChanged);
            // 
            // NewLangExistingHtmlBlkCB
            // 
            this.NewLangExistingHtmlBlkCB.AutoSize = true;
            this.NewLangExistingHtmlBlkCB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.NewLangExistingHtmlBlkCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewLangExistingHtmlBlkCB.Location = new System.Drawing.Point(470, 9);
            this.NewLangExistingHtmlBlkCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewLangExistingHtmlBlkCB.Name = "NewLangExistingHtmlBlkCB";
            this.NewLangExistingHtmlBlkCB.Size = new System.Drawing.Size(309, 24);
            this.NewLangExistingHtmlBlkCB.TabIndex = 28;
            this.NewLangExistingHtmlBlkCB.Text = "New Languages - Existing HtmlBlk";
            this.NewLangExistingHtmlBlkCB.UseVisualStyleBackColor = false;
            this.NewLangExistingHtmlBlkCB.CheckedChanged += new System.EventHandler(this.NewLangExistingHtmlBlkCB_CheckedChanged);
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 25;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "HelpNodeNM";
            this.dataGridViewTextBoxColumn12.HeaderText = "HelpNodeNM";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 300;
            // 
            // Title1
            // 
            this.Title1.DataPropertyName = "Title";
            this.Title1.HeaderText = "Title";
            this.Title1.Name = "Title1";
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.DataPropertyName = "HtmlContent";
            this.dataGridViewTextBoxColumn17.HeaderText = "HtmlContent";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.ReadOnly = true;
            this.dataGridViewTextBoxColumn17.Width = 400;
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.DataPropertyName = "Language";
            this.dataGridViewTextBoxColumn20.HeaderText = "Language";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            // 
            // dataGridViewCheckBoxColumn3
            // 
            this.dataGridViewCheckBoxColumn3.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn3.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
            this.dataGridViewCheckBoxColumn3.ReadOnly = true;
            this.dataGridViewCheckBoxColumn3.Visible = false;
            this.dataGridViewCheckBoxColumn3.Width = 60;
            // 
            // dataGridViewCheckBoxColumn14
            // 
            this.dataGridViewCheckBoxColumn14.DataPropertyName = "IsEditable";
            this.dataGridViewCheckBoxColumn14.HeaderText = "Editable";
            this.dataGridViewCheckBoxColumn14.Name = "dataGridViewCheckBoxColumn14";
            this.dataGridViewCheckBoxColumn14.ReadOnly = true;
            this.dataGridViewCheckBoxColumn14.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn14.Visible = false;
            this.dataGridViewCheckBoxColumn14.Width = 70;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn21.HeaderText = "Status";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.ReadOnly = true;
            this.dataGridViewTextBoxColumn21.Visible = false;
            this.dataGridViewTextBoxColumn21.Width = 90;
            // 
            // dataGridViewCheckBoxColumn30
            // 
            this.dataGridViewCheckBoxColumn30.HeaderText = "";
            this.dataGridViewCheckBoxColumn30.Name = "dataGridViewCheckBoxColumn30";
            this.dataGridViewCheckBoxColumn30.Width = 25;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "HelpNodeNM";
            this.dataGridViewTextBoxColumn4.HeaderText = "HelpNodeNM";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // dataGridViewTextBoxColumn31
            // 
            this.dataGridViewTextBoxColumn31.DataPropertyName = "Title";
            this.dataGridViewTextBoxColumn31.HeaderText = "Title";
            this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
            // 
            // dataGridViewTextBoxColumn42
            // 
            this.dataGridViewTextBoxColumn42.DataPropertyName = "HtmlContent";
            this.dataGridViewTextBoxColumn42.HeaderText = "HtmlContent";
            this.dataGridViewTextBoxColumn42.Name = "dataGridViewTextBoxColumn42";
            this.dataGridViewTextBoxColumn42.ReadOnly = true;
            this.dataGridViewTextBoxColumn42.Width = 250;
            // 
            // dataGridViewTextBoxColumn43
            // 
            this.dataGridViewTextBoxColumn43.DataPropertyName = "Language";
            this.dataGridViewTextBoxColumn43.HeaderText = "Language";
            this.dataGridViewTextBoxColumn43.Name = "dataGridViewTextBoxColumn43";
            this.dataGridViewTextBoxColumn43.ReadOnly = true;
            this.dataGridViewTextBoxColumn43.Width = 50;
            // 
            // HelpModified
            // 
            this.HelpModified.DataPropertyName = "HelpModified";
            this.HelpModified.HeaderText = "HelpModified";
            this.HelpModified.Name = "HelpModified";
            this.HelpModified.Visible = false;
            // 
            // TitleModified
            // 
            this.TitleModified.DataPropertyName = "TitleModified";
            this.TitleModified.HeaderText = "TitleModified";
            this.TitleModified.Name = "TitleModified";
            this.TitleModified.Visible = false;
            // 
            // dataGridViewTextBoxColumn44
            // 
            this.dataGridViewTextBoxColumn44.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn44.HeaderText = "Status";
            this.dataGridViewTextBoxColumn44.Name = "dataGridViewTextBoxColumn44";
            this.dataGridViewTextBoxColumn44.ReadOnly = true;
            this.dataGridViewTextBoxColumn44.Visible = false;
            this.dataGridViewTextBoxColumn44.Width = 60;
            // 
            // IsActive
            // 
            this.IsActive.DataPropertyName = "IsActive";
            this.IsActive.HeaderText = "Active";
            this.IsActive.Name = "IsActive";
            this.IsActive.ReadOnly = true;
            this.IsActive.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsActive.Visible = false;
            this.IsActive.Width = 60;
            // 
            // dataGridViewCheckBoxColumn15
            // 
            this.dataGridViewCheckBoxColumn15.HeaderText = "";
            this.dataGridViewCheckBoxColumn15.Name = "dataGridViewCheckBoxColumn15";
            this.dataGridViewCheckBoxColumn15.Visible = false;
            this.dataGridViewCheckBoxColumn15.Width = 25;
            // 
            // dataGridViewTextBoxColumn38
            // 
            this.dataGridViewTextBoxColumn38.DataPropertyName = "HelpNodeNM";
            this.dataGridViewTextBoxColumn38.HeaderText = "HelpNodeNM";
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            this.dataGridViewTextBoxColumn38.ReadOnly = true;
            this.dataGridViewTextBoxColumn38.Width = 200;
            // 
            // dataGridViewTextBoxColumn39
            // 
            this.dataGridViewTextBoxColumn39.DataPropertyName = "Title";
            this.dataGridViewTextBoxColumn39.HeaderText = "Title";
            this.dataGridViewTextBoxColumn39.Name = "dataGridViewTextBoxColumn39";
            // 
            // dataGridViewTextBoxColumn40
            // 
            this.dataGridViewTextBoxColumn40.DataPropertyName = "HtmlContent";
            this.dataGridViewTextBoxColumn40.HeaderText = "HtmlContent";
            this.dataGridViewTextBoxColumn40.Name = "dataGridViewTextBoxColumn40";
            this.dataGridViewTextBoxColumn40.ReadOnly = true;
            this.dataGridViewTextBoxColumn40.Width = 250;
            // 
            // dataGridViewTextBoxColumn41
            // 
            this.dataGridViewTextBoxColumn41.DataPropertyName = "Language";
            this.dataGridViewTextBoxColumn41.HeaderText = "Language";
            this.dataGridViewTextBoxColumn41.Name = "dataGridViewTextBoxColumn41";
            this.dataGridViewTextBoxColumn41.ReadOnly = true;
            this.dataGridViewTextBoxColumn41.Width = 50;
            // 
            // dataGridViewCheckBoxColumn28
            // 
            this.dataGridViewCheckBoxColumn28.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn28.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn28.Name = "dataGridViewCheckBoxColumn28";
            this.dataGridViewCheckBoxColumn28.ReadOnly = true;
            this.dataGridViewCheckBoxColumn28.Visible = false;
            this.dataGridViewCheckBoxColumn28.Width = 60;
            // 
            // dataGridViewCheckBoxColumn29
            // 
            this.dataGridViewCheckBoxColumn29.DataPropertyName = "IsEditable";
            this.dataGridViewCheckBoxColumn29.HeaderText = "Editable";
            this.dataGridViewCheckBoxColumn29.Name = "dataGridViewCheckBoxColumn29";
            this.dataGridViewCheckBoxColumn29.ReadOnly = true;
            this.dataGridViewCheckBoxColumn29.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn29.Visible = false;
            this.dataGridViewCheckBoxColumn29.Width = 70;
            // 
            // dataGridViewTextBoxColumn48
            // 
            this.dataGridViewTextBoxColumn48.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn48.HeaderText = "Status";
            this.dataGridViewTextBoxColumn48.Name = "dataGridViewTextBoxColumn48";
            this.dataGridViewTextBoxColumn48.ReadOnly = true;
            this.dataGridViewTextBoxColumn48.Visible = false;
            this.dataGridViewTextBoxColumn48.Width = 60;
            // 
            // HelpDiff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1425, 705);
            this.Controls.Add(this.PreviewUpdate);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.diffHelpMain);
            this.Controls.Add(this.moduleList);
            this.Controls.Add(this.label4);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HelpDiff";
            this.Text = "HelpDiff";
            this.Help.ResumeLayout(false);
            this.ItemsTab.ResumeLayout(false);
            this.NewHelpPage.ResumeLayout(false);
            this.NewHelpPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newHelpView)).EndInit();
            this.UpdateItemsTab.ResumeLayout(false);
            this.UpdateItemsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetHelp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceHelp)).EndInit();
            this.diffHelpMain.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newHelpLangView)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourceHelpLangView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetHelpLangView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceUpdateLangView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetUpdateLangView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewLangView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourceHtmlBlkLangView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetHtmlBlkLangView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newHtmlLangView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage Help;
        private System.Windows.Forms.TabControl ItemsTab;
        private System.Windows.Forms.TabPage NewHelpPage;
        private System.Windows.Forms.DataGridView newHelpView;
        private System.Windows.Forms.CheckBox NewHelpSelectAllCB;
        private System.Windows.Forms.TabControl diffHelpMain;
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
        private System.Windows.Forms.CheckBox UpdateHelpSelectAllCB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView SourceHelp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView TargetHelp;
        private System.Windows.Forms.Button PreviewUpdate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnIncludeHelp;
        private System.Windows.Forms.TabPage tabUpdateLang;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox UpdateHtmlBlkLangSelectAllCB;
        private System.Windows.Forms.DataGridView sourceHtmlBlkLangView;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsActiveModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn htmlModified;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn9;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn7;
        private System.Windows.Forms.DataGridView targetHtmlBlkLangView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn24;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn23;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn22;
        private System.Windows.Forms.TabPage tabNewLang;
        private System.Windows.Forms.CheckBox NewHtmlBlkLangSelectAllCB;
        private System.Windows.Forms.DataGridView newHtmlLangView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn63;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn18;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn55;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn56;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn54;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn16;
        private System.Windows.Forms.CheckBox NewLangNewHtmlBlkCB;
        private System.Windows.Forms.CheckBox NewLangExistingHtmlBlkCB;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnIncludeHelpLang;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox NewLangExistingHelpCB;
        private System.Windows.Forms.CheckBox NewLangNewHelpCB;
        private System.Windows.Forms.DataGridView newHelpLangView;
        private System.Windows.Forms.CheckBox NewHelpLangSelectAllCB;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView targetHelpLangView;
        private System.Windows.Forms.CheckBox UpdateHelpLangSelectAllCB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn20;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private System.Windows.Forms.DataGridView sourceHelpLangView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Active;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn30;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn42;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn43;
        private System.Windows.Forms.DataGridViewTextBoxColumn HelpModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn TitleModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn44;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn41;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn28;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn29;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn48;
    }
}
