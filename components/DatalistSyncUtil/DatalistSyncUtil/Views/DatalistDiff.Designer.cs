//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace DatalistSyncUtil
{
    public partial class DatalistDiff
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl diffTab;
        private System.Windows.Forms.TabPage DatalistTabPage;
        private System.Windows.Forms.TabPage itemTabPage;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView DataListView;
        private System.Windows.Forms.Button btnListUpdate;
        private new System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsEditable;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemsCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.TabControl ItemsTab;
        private System.Windows.Forms.TabPage NewItemsPage;
        private System.Windows.Forms.DataGridView NewItemsView;
        private System.Windows.Forms.TabPage UpdateItemsTab;
        private System.Windows.Forms.DataGridView UpdateTargetItemView;
        private System.Windows.Forms.DataGridView UpdateSourceItemView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn EffDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderIndexModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn EffDateModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndDateModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsEditableModified;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.TabPage ItemLanguages;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage NewLangTab;
        private System.Windows.Forms.DataGridView NewLangView;
        private System.Windows.Forms.TabPage UpdateLangTab;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn Locale;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private System.Windows.Forms.DataGridView TargetUpdateLangView;
        private System.Windows.Forms.DataGridView SourceUpdateLangView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private System.Windows.Forms.DataGridViewTextBoxColumn DesciptionModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongDescModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        private System.Windows.Forms.CheckBox DatalistSelectAllChkBox;
        private System.Windows.Forms.CheckBox NewItemsSelectAllCB;
        private System.Windows.Forms.CheckBox UpdateItemsSelectAllCB;
        private System.Windows.Forms.CheckBox NewLangSelectAllCB;
        private System.Windows.Forms.CheckBox UpdateLangSelectAllCB;
        private System.Windows.Forms.Button btnUpdateItems;
        private System.Windows.Forms.Button btnUpdateLanguages;
        private System.Windows.Forms.Button PreviewUpdate;
        private System.Windows.Forms.ComboBox ModuleList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox ItemMessagesCB;
        private System.Windows.Forms.CheckBox ItemLabelsCB;
        private System.Windows.Forms.CheckBox ItemRightsCB;
        private System.Windows.Forms.CheckBox ItemFunctionsCB;
        private System.Windows.Forms.CheckBox ItemRolesCB;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox LangMessagesCB;
        private System.Windows.Forms.CheckBox LangLabelsCB;
        private System.Windows.Forms.CheckBox LangRightsCB;
        private System.Windows.Forms.CheckBox LangFunctionsCB;
        private System.Windows.Forms.CheckBox LangRolesCB;
        private System.Windows.Forms.CheckBox ItemDatalistCB;
        private System.Windows.Forms.CheckBox LangDatalistCB;
        private System.Windows.Forms.CheckBox ExistingListNewItemCB;
        private System.Windows.Forms.CheckBox NewListNewItemCB;
        private System.Windows.Forms.CheckBox NewLangExistingItemCB;
        private System.Windows.Forms.CheckBox NewLangNewItemCB;
        private System.Windows.Forms.CheckBox NewLangNewListCB;
        private System.Windows.Forms.TabPage Attributes;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox NewItemsAttributeSelectAllCB;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox checkBox10;
        private System.Windows.Forms.DataGridView SourceUpdateAttributeView;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox AttributesDataListCB;
        private System.Windows.Forms.CheckBox AttributesMsgCB;
        private System.Windows.Forms.CheckBox AttributesRightsCB;
        private System.Windows.Forms.CheckBox AttributesFunctionCB;
        private System.Windows.Forms.CheckBox AttributesRoleCB;
        private System.Windows.Forms.Button btnUpdateAttribute;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn51;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn46;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn47;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn48;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn49;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn53;
        private System.Windows.Forms.DataGridView TargetUpdateAttributeView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn42;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn43;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn44;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn45;
        private System.Windows.Forms.DataGridView NewAttributesView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn41;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn50;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn52;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.diffTab = new System.Windows.Forms.TabControl();
            this.DatalistTabPage = new System.Windows.Forms.TabPage();
            this.DatalistSelectAllChkBox = new System.Windows.Forms.CheckBox();
            this.btnListUpdate = new System.Windows.Forms.Button();
            this.DataListView = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ContentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsEditable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ItemsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemTabPage = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ItemDatalistCB = new System.Windows.Forms.CheckBox();
            this.ItemMessagesCB = new System.Windows.Forms.CheckBox();
            this.ItemLabelsCB = new System.Windows.Forms.CheckBox();
            this.ItemRightsCB = new System.Windows.Forms.CheckBox();
            this.ItemFunctionsCB = new System.Windows.Forms.CheckBox();
            this.ItemRolesCB = new System.Windows.Forms.CheckBox();
            this.btnUpdateItems = new System.Windows.Forms.Button();
            this.ItemsTab = new System.Windows.Forms.TabControl();
            this.NewItemsPage = new System.Windows.Forms.TabPage();
            this.ExistingListNewItemCB = new System.Windows.Forms.CheckBox();
            this.NewListNewItemCB = new System.Windows.Forms.CheckBox();
            this.NewItemsView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EffDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderIndexModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EffDateModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndDateModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsEditableModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewItemsSelectAllCB = new System.Windows.Forms.CheckBox();
            this.UpdateItemsTab = new System.Windows.Forms.TabPage();
            this.UpdateItemsSelectAllCB = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UpdateTargetItemView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn8 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn9 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateSourceItemView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemLanguages = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LangDatalistCB = new System.Windows.Forms.CheckBox();
            this.LangMessagesCB = new System.Windows.Forms.CheckBox();
            this.LangLabelsCB = new System.Windows.Forms.CheckBox();
            this.LangRightsCB = new System.Windows.Forms.CheckBox();
            this.LangFunctionsCB = new System.Windows.Forms.CheckBox();
            this.LangRolesCB = new System.Windows.Forms.CheckBox();
            this.btnUpdateLanguages = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.NewLangTab = new System.Windows.Forms.TabPage();
            this.NewLangExistingItemCB = new System.Windows.Forms.CheckBox();
            this.NewLangNewItemCB = new System.Windows.Forms.CheckBox();
            this.NewLangNewListCB = new System.Windows.Forms.CheckBox();
            this.NewLangSelectAllCB = new System.Windows.Forms.CheckBox();
            this.NewLangView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn10 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Locale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateLangTab = new System.Windows.Forms.TabPage();
            this.UpdateLangSelectAllCB = new System.Windows.Forms.CheckBox();
            this.TargetUpdateLangView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn12 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceUpdateLangView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn11 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DesciptionModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongDescModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Attributes = new System.Windows.Forms.TabPage();
            this.btnUpdateAttribute = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.NewAttributesView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn13 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn41 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn50 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn52 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewItemsAttributeSelectAllCB = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.TargetUpdateAttributeView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn14 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn43 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn44 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn45 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.SourceUpdateAttributeView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn15 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn51 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn46 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn47 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn48 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn49 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn53 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.AttributesDataListCB = new System.Windows.Forms.CheckBox();
            this.AttributesMsgCB = new System.Windows.Forms.CheckBox();
            this.AttributesRightsCB = new System.Windows.Forms.CheckBox();
            this.AttributesFunctionCB = new System.Windows.Forms.CheckBox();
            this.AttributesRoleCB = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.PreviewUpdate = new System.Windows.Forms.Button();
            this.ModuleList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.diffTab.SuspendLayout();
            this.DatalistTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataListView)).BeginInit();
            this.itemTabPage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.ItemsTab.SuspendLayout();
            this.NewItemsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewItemsView)).BeginInit();
            this.UpdateItemsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateTargetItemView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateSourceItemView)).BeginInit();
            this.ItemLanguages.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.NewLangTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewLangView)).BeginInit();
            this.UpdateLangTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetUpdateLangView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceUpdateLangView)).BeginInit();
            this.Attributes.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewAttributesView)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetUpdateAttributeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceUpdateAttributeView)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // diffTab
            // 
            this.diffTab.Controls.Add(this.DatalistTabPage);
            this.diffTab.Controls.Add(this.itemTabPage);
            this.diffTab.Controls.Add(this.ItemLanguages);
            this.diffTab.Controls.Add(this.Attributes);
            this.diffTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diffTab.Location = new System.Drawing.Point(16, 46);
            this.diffTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.diffTab.Name = "diffTab";
            this.diffTab.SelectedIndex = 0;
            this.diffTab.Size = new System.Drawing.Size(1675, 871);
            this.diffTab.TabIndex = 0;
            // 
            // DatalistTabPage
            // 
            this.DatalistTabPage.AutoScroll = true;
            this.DatalistTabPage.Controls.Add(this.DatalistSelectAllChkBox);
            this.DatalistTabPage.Controls.Add(this.btnListUpdate);
            this.DatalistTabPage.Controls.Add(this.DataListView);
            this.DatalistTabPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatalistTabPage.Location = new System.Drawing.Point(4, 29);
            this.DatalistTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DatalistTabPage.Name = "DatalistTabPage";
            this.DatalistTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DatalistTabPage.Size = new System.Drawing.Size(1667, 838);
            this.DatalistTabPage.TabIndex = 0;
            this.DatalistTabPage.Text = "Datalist";
            this.DatalistTabPage.UseVisualStyleBackColor = true;
            // 
            // DatalistSelectAllChkBox
            // 
            this.DatalistSelectAllChkBox.AutoSize = true;
            this.DatalistSelectAllChkBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DatalistSelectAllChkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatalistSelectAllChkBox.Location = new System.Drawing.Point(7, 10);
            this.DatalistSelectAllChkBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DatalistSelectAllChkBox.Name = "DatalistSelectAllChkBox";
            this.DatalistSelectAllChkBox.Size = new System.Drawing.Size(111, 24);
            this.DatalistSelectAllChkBox.TabIndex = 7;
            this.DatalistSelectAllChkBox.Text = "Select All";
            this.DatalistSelectAllChkBox.UseVisualStyleBackColor = true;
            this.DatalistSelectAllChkBox.CheckedChanged += new System.EventHandler(this.DatalistSelectAllChkBox_CheckedChanged);
            // 
            // btnListUpdate
            // 
            this.btnListUpdate.Location = new System.Drawing.Point(0, 788);
            this.btnListUpdate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnListUpdate.Name = "btnListUpdate";
            this.btnListUpdate.Size = new System.Drawing.Size(84, 40);
            this.btnListUpdate.TabIndex = 2;
            this.btnListUpdate.Text = "Include";
            this.btnListUpdate.UseVisualStyleBackColor = true;
            this.btnListUpdate.Click += new System.EventHandler(this.btnListUpdate_Click);
            // 
            // DataListView
            // 
            this.DataListView.AllowUserToAddRows = false;
            this.DataListView.AllowUserToDeleteRows = false;
            this.DataListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataListView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.ContentID,
            this.Description,
            this.IsActive,
            this.IsEditable,
            this.ItemsCount,
            this.Status});
            this.DataListView.Location = new System.Drawing.Point(0, 44);
            this.DataListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DataListView.Name = "DataListView";
            this.DataListView.RowTemplate.Height = 24;
            this.DataListView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DataListView.Size = new System.Drawing.Size(1425, 736);
            this.DataListView.TabIndex = 3;
            // 
            // Select
            // 
            this.Select.HeaderText = "";
            this.Select.Name = "Select";
            this.Select.Width = 25;
            // 
            // ContentID
            // 
            this.ContentID.DataPropertyName = "ContentID";
            this.ContentID.HeaderText = "Content ID";
            this.ContentID.Name = "ContentID";
            this.ContentID.ReadOnly = true;
            this.ContentID.Width = 460;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 460;
            // 
            // IsActive
            // 
            this.IsActive.DataPropertyName = "IsActive";
            this.IsActive.HeaderText = "Active";
            this.IsActive.Name = "IsActive";
            this.IsActive.ReadOnly = true;
            this.IsActive.Width = 60;
            // 
            // IsEditable
            // 
            this.IsEditable.DataPropertyName = "IsEditable";
            this.IsEditable.HeaderText = "Editable";
            this.IsEditable.Name = "IsEditable";
            this.IsEditable.ReadOnly = true;
            this.IsEditable.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsEditable.Width = 70;
            // 
            // ItemsCount
            // 
            this.ItemsCount.DataPropertyName = "ItemsCount";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ItemsCount.DefaultCellStyle = dataGridViewCellStyle8;
            this.ItemsCount.HeaderText = "Items";
            this.ItemsCount.Name = "ItemsCount";
            this.ItemsCount.ReadOnly = true;
            this.ItemsCount.Width = 60;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 70;
            // 
            // itemTabPage
            // 
            this.itemTabPage.Controls.Add(this.panel1);
            this.itemTabPage.Controls.Add(this.btnUpdateItems);
            this.itemTabPage.Controls.Add(this.ItemsTab);
            this.itemTabPage.Location = new System.Drawing.Point(4, 29);
            this.itemTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.itemTabPage.Name = "itemTabPage";
            this.itemTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.itemTabPage.Size = new System.Drawing.Size(1667, 838);
            this.itemTabPage.TabIndex = 1;
            this.itemTabPage.Text = "Items";
            this.itemTabPage.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.ItemDatalistCB);
            this.panel1.Controls.Add(this.ItemMessagesCB);
            this.panel1.Controls.Add(this.ItemLabelsCB);
            this.panel1.Controls.Add(this.ItemRightsCB);
            this.panel1.Controls.Add(this.ItemFunctionsCB);
            this.panel1.Controls.Add(this.ItemRolesCB);
            this.panel1.Location = new System.Drawing.Point(10, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 51);
            this.panel1.TabIndex = 13;
            // 
            // ItemDatalistCB
            // 
            this.ItemDatalistCB.AutoSize = true;
            this.ItemDatalistCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemDatalistCB.Location = new System.Drawing.Point(516, 14);
            this.ItemDatalistCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ItemDatalistCB.Name = "ItemDatalistCB";
            this.ItemDatalistCB.Size = new System.Drawing.Size(97, 24);
            this.ItemDatalistCB.TabIndex = 14;
            this.ItemDatalistCB.Text = "Datalist";
            this.ItemDatalistCB.UseVisualStyleBackColor = true;
            this.ItemDatalistCB.CheckedChanged += new System.EventHandler(this.ItemCB_CheckedChanged);
            // 
            // ItemMessagesCB
            // 
            this.ItemMessagesCB.AutoSize = true;
            this.ItemMessagesCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemMessagesCB.Location = new System.Drawing.Point(402, 14);
            this.ItemMessagesCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ItemMessagesCB.Name = "ItemMessagesCB";
            this.ItemMessagesCB.Size = new System.Drawing.Size(116, 24);
            this.ItemMessagesCB.TabIndex = 13;
            this.ItemMessagesCB.Text = "Messages";
            this.ItemMessagesCB.UseVisualStyleBackColor = true;
            this.ItemMessagesCB.CheckedChanged += new System.EventHandler(this.ItemCB_CheckedChanged);
            // 
            // ItemLabelsCB
            // 
            this.ItemLabelsCB.AutoSize = true;
            this.ItemLabelsCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemLabelsCB.Location = new System.Drawing.Point(307, 14);
            this.ItemLabelsCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ItemLabelsCB.Name = "ItemLabelsCB";
            this.ItemLabelsCB.Size = new System.Drawing.Size(88, 24);
            this.ItemLabelsCB.TabIndex = 12;
            this.ItemLabelsCB.Text = "Labels";
            this.ItemLabelsCB.UseVisualStyleBackColor = true;
            this.ItemLabelsCB.CheckedChanged += new System.EventHandler(this.ItemCB_CheckedChanged);
            // 
            // ItemRightsCB
            // 
            this.ItemRightsCB.AutoSize = true;
            this.ItemRightsCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemRightsCB.Location = new System.Drawing.Point(215, 14);
            this.ItemRightsCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ItemRightsCB.Name = "ItemRightsCB";
            this.ItemRightsCB.Size = new System.Drawing.Size(87, 24);
            this.ItemRightsCB.TabIndex = 11;
            this.ItemRightsCB.Text = "Rights";
            this.ItemRightsCB.UseVisualStyleBackColor = true;
            this.ItemRightsCB.CheckedChanged += new System.EventHandler(this.ItemCB_CheckedChanged);
            // 
            // ItemFunctionsCB
            // 
            this.ItemFunctionsCB.AutoSize = true;
            this.ItemFunctionsCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemFunctionsCB.Location = new System.Drawing.Point(96, 14);
            this.ItemFunctionsCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ItemFunctionsCB.Name = "ItemFunctionsCB";
            this.ItemFunctionsCB.Size = new System.Drawing.Size(114, 24);
            this.ItemFunctionsCB.TabIndex = 10;
            this.ItemFunctionsCB.Text = "Functions";
            this.ItemFunctionsCB.UseVisualStyleBackColor = true;
            this.ItemFunctionsCB.CheckedChanged += new System.EventHandler(this.ItemCB_CheckedChanged);
            // 
            // ItemRolesCB
            // 
            this.ItemRolesCB.AutoSize = true;
            this.ItemRolesCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemRolesCB.Location = new System.Drawing.Point(9, 14);
            this.ItemRolesCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ItemRolesCB.Name = "ItemRolesCB";
            this.ItemRolesCB.Size = new System.Drawing.Size(81, 24);
            this.ItemRolesCB.TabIndex = 9;
            this.ItemRolesCB.Text = "Roles";
            this.ItemRolesCB.UseVisualStyleBackColor = true;
            this.ItemRolesCB.CheckedChanged += new System.EventHandler(this.ItemCB_CheckedChanged);
            // 
            // btnUpdateItems
            // 
            this.btnUpdateItems.Location = new System.Drawing.Point(2, 802);
            this.btnUpdateItems.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUpdateItems.Name = "btnUpdateItems";
            this.btnUpdateItems.Size = new System.Drawing.Size(84, 32);
            this.btnUpdateItems.TabIndex = 1;
            this.btnUpdateItems.Text = "Include";
            this.btnUpdateItems.UseVisualStyleBackColor = true;
            this.btnUpdateItems.Click += new System.EventHandler(this.btnUpdateItems_Click);
            // 
            // ItemsTab
            // 
            this.ItemsTab.Controls.Add(this.NewItemsPage);
            this.ItemsTab.Controls.Add(this.UpdateItemsTab);
            this.ItemsTab.Location = new System.Drawing.Point(6, 62);
            this.ItemsTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ItemsTab.Name = "ItemsTab";
            this.ItemsTab.SelectedIndex = 0;
            this.ItemsTab.Size = new System.Drawing.Size(1657, 738);
            this.ItemsTab.TabIndex = 0;
            // 
            // NewItemsPage
            // 
            this.NewItemsPage.Controls.Add(this.ExistingListNewItemCB);
            this.NewItemsPage.Controls.Add(this.NewListNewItemCB);
            this.NewItemsPage.Controls.Add(this.NewItemsView);
            this.NewItemsPage.Controls.Add(this.NewItemsSelectAllCB);
            this.NewItemsPage.Location = new System.Drawing.Point(4, 29);
            this.NewItemsPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewItemsPage.Name = "NewItemsPage";
            this.NewItemsPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewItemsPage.Size = new System.Drawing.Size(1649, 705);
            this.NewItemsPage.TabIndex = 0;
            this.NewItemsPage.Text = "New Items";
            this.NewItemsPage.UseVisualStyleBackColor = true;
            // 
            // ExistingListNewItemCB
            // 
            this.ExistingListNewItemCB.AutoSize = true;
            this.ExistingListNewItemCB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ExistingListNewItemCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExistingListNewItemCB.Location = new System.Drawing.Point(402, 8);
            this.ExistingListNewItemCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ExistingListNewItemCB.Name = "ExistingListNewItemCB";
            this.ExistingListNewItemCB.Size = new System.Drawing.Size(256, 24);
            this.ExistingListNewItemCB.TabIndex = 19;
            this.ExistingListNewItemCB.Text = "New Item - Existing Datalist";
            this.ExistingListNewItemCB.UseVisualStyleBackColor = false;
            this.ExistingListNewItemCB.CheckedChanged += new System.EventHandler(this.ExistingListNewItemCB_CheckedChanged);
            // 
            // NewListNewItemCB
            // 
            this.NewListNewItemCB.AutoSize = true;
            this.NewListNewItemCB.BackColor = System.Drawing.Color.LightBlue;
            this.NewListNewItemCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewListNewItemCB.Location = new System.Drawing.Point(169, 8);
            this.NewListNewItemCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewListNewItemCB.Name = "NewListNewItemCB";
            this.NewListNewItemCB.Size = new System.Drawing.Size(227, 24);
            this.NewListNewItemCB.TabIndex = 18;
            this.NewListNewItemCB.Text = "New Item - New Datalist";
            this.NewListNewItemCB.UseVisualStyleBackColor = false;
            this.NewListNewItemCB.CheckedChanged += new System.EventHandler(this.NewListNewItemCB_CheckedChanged);
            // 
            // NewItemsView
            // 
            this.NewItemsView.AllowUserToAddRows = false;
            this.NewItemsView.AllowUserToDeleteRows = false;
            this.NewItemsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NewItemsView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.Code,
            this.OrderIndex,
            this.EffDate,
            this.EndDate,
            this.OrderIndexModified,
            this.EffDateModified,
            this.EndDateModified,
            this.IsEditableModified,
            this.dataGridViewCheckBoxColumn2,
            this.dataGridViewCheckBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.NewItemsView.Location = new System.Drawing.Point(18, 41);
            this.NewItemsView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewItemsView.Name = "NewItemsView";
            this.NewItemsView.RowTemplate.Height = 24;
            this.NewItemsView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NewItemsView.Size = new System.Drawing.Size(1286, 652);
            this.NewItemsView.TabIndex = 6;
            this.NewItemsView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.NewItemsView_RowsAdded);
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 25;
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
            this.Code.DataPropertyName = "Code";
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Width = 250;
            // 
            // OrderIndex
            // 
            this.OrderIndex.DataPropertyName = "OrderIndex";
            this.OrderIndex.HeaderText = "Index";
            this.OrderIndex.Name = "OrderIndex";
            this.OrderIndex.ReadOnly = true;
            this.OrderIndex.Width = 50;
            // 
            // EffDate
            // 
            this.EffDate.DataPropertyName = "EffectiveStartDate";
            dataGridViewCellStyle9.Format = "d";
            dataGridViewCellStyle9.NullValue = null;
            this.EffDate.DefaultCellStyle = dataGridViewCellStyle9;
            this.EffDate.HeaderText = "Eff Date";
            this.EffDate.Name = "EffDate";
            this.EffDate.ReadOnly = true;
            this.EffDate.Width = 120;
            // 
            // EndDate
            // 
            this.EndDate.DataPropertyName = "EffectiveEndDate";
            dataGridViewCellStyle10.Format = "d";
            dataGridViewCellStyle10.NullValue = null;
            this.EndDate.DefaultCellStyle = dataGridViewCellStyle10;
            this.EndDate.HeaderText = "End Date";
            this.EndDate.Name = "EndDate";
            this.EndDate.ReadOnly = true;
            this.EndDate.Width = 120;
            // 
            // OrderIndexModified
            // 
            this.OrderIndexModified.DataPropertyName = "OrderIndexModified";
            this.OrderIndexModified.HeaderText = "OrderIndexModified";
            this.OrderIndexModified.Name = "OrderIndexModified";
            this.OrderIndexModified.ReadOnly = true;
            this.OrderIndexModified.Visible = false;
            // 
            // EffDateModified
            // 
            this.EffDateModified.DataPropertyName = "EffectiveStartDateModified ";
            this.EffDateModified.HeaderText = "EffDateModified";
            this.EffDateModified.Name = "EffDateModified";
            this.EffDateModified.ReadOnly = true;
            this.EffDateModified.Visible = false;
            // 
            // EndDateModified
            // 
            this.EndDateModified.DataPropertyName = "EffectiveEndDateModified ";
            this.EndDateModified.HeaderText = "EndDateModified";
            this.EndDateModified.Name = "EndDateModified";
            this.EndDateModified.ReadOnly = true;
            this.EndDateModified.Visible = false;
            // 
            // IsEditableModified
            // 
            this.IsEditableModified.DataPropertyName = "IsEditableModified ";
            this.IsEditableModified.HeaderText = "IsEditableModified";
            this.IsEditableModified.Name = "IsEditableModified";
            this.IsEditableModified.ReadOnly = true;
            this.IsEditableModified.Visible = false;
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
            this.dataGridViewTextBoxColumn4.Visible = false;
            this.dataGridViewTextBoxColumn4.Width = 60;
            // 
            // NewItemsSelectAllCB
            // 
            this.NewItemsSelectAllCB.AutoSize = true;
            this.NewItemsSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewItemsSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewItemsSelectAllCB.Location = new System.Drawing.Point(3, 8);
            this.NewItemsSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewItemsSelectAllCB.Name = "NewItemsSelectAllCB";
            this.NewItemsSelectAllCB.Size = new System.Drawing.Size(111, 24);
            this.NewItemsSelectAllCB.TabIndex = 8;
            this.NewItemsSelectAllCB.Text = "Select All";
            this.NewItemsSelectAllCB.UseVisualStyleBackColor = true;
            this.NewItemsSelectAllCB.CheckedChanged += new System.EventHandler(this.NewItemsSelectAllCB_CheckedChanged);
            // 
            // UpdateItemsTab
            // 
            this.UpdateItemsTab.Controls.Add(this.UpdateItemsSelectAllCB);
            this.UpdateItemsTab.Controls.Add(this.label2);
            this.UpdateItemsTab.Controls.Add(this.label1);
            this.UpdateItemsTab.Controls.Add(this.UpdateTargetItemView);
            this.UpdateItemsTab.Controls.Add(this.UpdateSourceItemView);
            this.UpdateItemsTab.Location = new System.Drawing.Point(4, 29);
            this.UpdateItemsTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UpdateItemsTab.Name = "UpdateItemsTab";
            this.UpdateItemsTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UpdateItemsTab.Size = new System.Drawing.Size(1649, 705);
            this.UpdateItemsTab.TabIndex = 1;
            this.UpdateItemsTab.Text = "Update Items";
            this.UpdateItemsTab.UseVisualStyleBackColor = true;
            // 
            // UpdateItemsSelectAllCB
            // 
            this.UpdateItemsSelectAllCB.AutoSize = true;
            this.UpdateItemsSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateItemsSelectAllCB.Location = new System.Drawing.Point(97, 19);
            this.UpdateItemsSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UpdateItemsSelectAllCB.Name = "UpdateItemsSelectAllCB";
            this.UpdateItemsSelectAllCB.Size = new System.Drawing.Size(111, 24);
            this.UpdateItemsSelectAllCB.TabIndex = 11;
            this.UpdateItemsSelectAllCB.Text = "Select All";
            this.UpdateItemsSelectAllCB.UseVisualStyleBackColor = true;
            this.UpdateItemsSelectAllCB.CheckedChanged += new System.EventHandler(this.UpdateItemsSelectAllCB_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1116, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 25);
            this.label2.TabIndex = 10;
            this.label2.Text = "Target:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Source:";
            // 
            // UpdateTargetItemView
            // 
            this.UpdateTargetItemView.AllowUserToAddRows = false;
            this.UpdateTargetItemView.AllowUserToDeleteRows = false;
            this.UpdateTargetItemView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UpdateTargetItemView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn7,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16,
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn20,
            this.dataGridViewTextBoxColumn21,
            this.dataGridViewCheckBoxColumn8,
            this.dataGridViewCheckBoxColumn9,
            this.dataGridViewTextBoxColumn22});
            this.UpdateTargetItemView.Location = new System.Drawing.Point(1119, 48);
            this.UpdateTargetItemView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UpdateTargetItemView.Name = "UpdateTargetItemView";
            this.UpdateTargetItemView.RowTemplate.Height = 24;
            this.UpdateTargetItemView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.UpdateTargetItemView.Size = new System.Drawing.Size(524, 698);
            this.UpdateTargetItemView.TabIndex = 8;
            this.UpdateTargetItemView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.UpdateTargetItemView_Scroll);
            // 
            // dataGridViewCheckBoxColumn7
            // 
            this.dataGridViewCheckBoxColumn7.HeaderText = "";
            this.dataGridViewCheckBoxColumn7.Name = "dataGridViewCheckBoxColumn7";
            this.dataGridViewCheckBoxColumn7.Visible = false;
            this.dataGridViewCheckBoxColumn7.Width = 25;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn13.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Visible = false;
            this.dataGridViewTextBoxColumn13.Width = 200;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn14.HeaderText = "Code";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Visible = false;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.DataPropertyName = "OrderIndex";
            this.dataGridViewTextBoxColumn15.HeaderText = "Index";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.Width = 50;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.DataPropertyName = "EffectiveStartDate";
            dataGridViewCellStyle11.Format = "d";
            dataGridViewCellStyle11.NullValue = null;
            this.dataGridViewTextBoxColumn16.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn16.HeaderText = "Eff Date";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.Width = 120;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.DataPropertyName = "EffectiveEndDate";
            dataGridViewCellStyle12.Format = "d";
            dataGridViewCellStyle12.NullValue = null;
            this.dataGridViewTextBoxColumn17.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn17.HeaderText = "End Date";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.ReadOnly = true;
            this.dataGridViewTextBoxColumn17.Width = 120;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.DataPropertyName = "OrderIndexModified";
            this.dataGridViewTextBoxColumn18.HeaderText = "OrderIndexModified";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.ReadOnly = true;
            this.dataGridViewTextBoxColumn18.Visible = false;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.DataPropertyName = "EffectiveStartDateModified";
            this.dataGridViewTextBoxColumn19.HeaderText = "EffDateModified";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.Visible = false;
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.DataPropertyName = "EffectiveEndDateModified";
            this.dataGridViewTextBoxColumn20.HeaderText = "EndDateModified";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            this.dataGridViewTextBoxColumn20.Visible = false;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.DataPropertyName = "IsEditableModified";
            this.dataGridViewTextBoxColumn21.HeaderText = "IsEditableModified";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.ReadOnly = true;
            this.dataGridViewTextBoxColumn21.Visible = false;
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
            this.dataGridViewCheckBoxColumn9.Width = 70;
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn22.HeaderText = "Status";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.ReadOnly = true;
            this.dataGridViewTextBoxColumn22.Visible = false;
            this.dataGridViewTextBoxColumn22.Width = 60;
            // 
            // UpdateSourceItemView
            // 
            this.UpdateSourceItemView.AllowUserToAddRows = false;
            this.UpdateSourceItemView.AllowUserToDeleteRows = false;
            this.UpdateSourceItemView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UpdateSourceItemView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn4,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewCheckBoxColumn5,
            this.dataGridViewCheckBoxColumn6,
            this.dataGridViewTextBoxColumn12});
            this.UpdateSourceItemView.Location = new System.Drawing.Point(4, 48);
            this.UpdateSourceItemView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UpdateSourceItemView.Name = "UpdateSourceItemView";
            this.UpdateSourceItemView.RowTemplate.Height = 24;
            this.UpdateSourceItemView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.UpdateSourceItemView.Size = new System.Drawing.Size(1115, 698);
            this.UpdateSourceItemView.TabIndex = 7;
            this.UpdateSourceItemView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.UpdateSourceItemView_RowAdded);
            this.UpdateSourceItemView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.UpdateSourceItemView_Scroll);
            // 
            // dataGridViewCheckBoxColumn4
            // 
            this.dataGridViewCheckBoxColumn4.HeaderText = "";
            this.dataGridViewCheckBoxColumn4.Name = "dataGridViewCheckBoxColumn4";
            this.dataGridViewCheckBoxColumn4.Width = 25;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn2.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 300;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn3.HeaderText = "Code";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "OrderIndex";
            this.dataGridViewTextBoxColumn5.HeaderText = "Index";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 50;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "EffectiveStartDate";
            dataGridViewCellStyle13.Format = "d";
            dataGridViewCellStyle13.NullValue = null;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewTextBoxColumn6.HeaderText = "Eff Date";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 120;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "EffectiveEndDate";
            dataGridViewCellStyle14.Format = "d";
            dataGridViewCellStyle14.NullValue = null;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewTextBoxColumn7.HeaderText = "End Date";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 120;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "OrderIndexModified";
            this.dataGridViewTextBoxColumn8.HeaderText = "OrderIndexModified";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Visible = false;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "EffectiveStartDateModified";
            this.dataGridViewTextBoxColumn9.HeaderText = "EffDateModified";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Visible = false;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "EffectiveEndDateModified";
            this.dataGridViewTextBoxColumn10.HeaderText = "EndDateModified";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Visible = false;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "IsEditableModified";
            this.dataGridViewTextBoxColumn11.HeaderText = "IsEditableModified";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Visible = false;
            // 
            // dataGridViewCheckBoxColumn5
            // 
            this.dataGridViewCheckBoxColumn5.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn5.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn5.Name = "dataGridViewCheckBoxColumn5";
            this.dataGridViewCheckBoxColumn5.ReadOnly = true;
            this.dataGridViewCheckBoxColumn5.Width = 60;
            // 
            // dataGridViewCheckBoxColumn6
            // 
            this.dataGridViewCheckBoxColumn6.DataPropertyName = "IsEditable";
            this.dataGridViewCheckBoxColumn6.HeaderText = "Editable";
            this.dataGridViewCheckBoxColumn6.Name = "dataGridViewCheckBoxColumn6";
            this.dataGridViewCheckBoxColumn6.ReadOnly = true;
            this.dataGridViewCheckBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn6.Width = 70;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn12.HeaderText = "Status";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Visible = false;
            this.dataGridViewTextBoxColumn12.Width = 60;
            // 
            // ItemLanguages
            // 
            this.ItemLanguages.Controls.Add(this.panel2);
            this.ItemLanguages.Controls.Add(this.btnUpdateLanguages);
            this.ItemLanguages.Controls.Add(this.tabControl1);
            this.ItemLanguages.Location = new System.Drawing.Point(4, 29);
            this.ItemLanguages.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ItemLanguages.Name = "ItemLanguages";
            this.ItemLanguages.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ItemLanguages.Size = new System.Drawing.Size(1667, 838);
            this.ItemLanguages.TabIndex = 2;
            this.ItemLanguages.Text = "Languages";
            this.ItemLanguages.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.LangDatalistCB);
            this.panel2.Controls.Add(this.LangMessagesCB);
            this.panel2.Controls.Add(this.LangLabelsCB);
            this.panel2.Controls.Add(this.LangRightsCB);
            this.panel2.Controls.Add(this.LangFunctionsCB);
            this.panel2.Controls.Add(this.LangRolesCB);
            this.panel2.Location = new System.Drawing.Point(7, 6);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(623, 51);
            this.panel2.TabIndex = 14;
            // 
            // LangDatalistCB
            // 
            this.LangDatalistCB.AutoSize = true;
            this.LangDatalistCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LangDatalistCB.Location = new System.Drawing.Point(523, 14);
            this.LangDatalistCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LangDatalistCB.Name = "LangDatalistCB";
            this.LangDatalistCB.Size = new System.Drawing.Size(97, 24);
            this.LangDatalistCB.TabIndex = 15;
            this.LangDatalistCB.Text = "Datalist";
            this.LangDatalistCB.UseVisualStyleBackColor = true;
            this.LangDatalistCB.CheckedChanged += new System.EventHandler(this.LangCB_CheckedChanged);
            // 
            // LangMessagesCB
            // 
            this.LangMessagesCB.AutoSize = true;
            this.LangMessagesCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LangMessagesCB.Location = new System.Drawing.Point(402, 14);
            this.LangMessagesCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LangMessagesCB.Name = "LangMessagesCB";
            this.LangMessagesCB.Size = new System.Drawing.Size(116, 24);
            this.LangMessagesCB.TabIndex = 13;
            this.LangMessagesCB.Text = "Messages";
            this.LangMessagesCB.UseVisualStyleBackColor = true;
            this.LangMessagesCB.CheckedChanged += new System.EventHandler(this.LangCB_CheckedChanged);
            // 
            // LangLabelsCB
            // 
            this.LangLabelsCB.AutoSize = true;
            this.LangLabelsCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LangLabelsCB.Location = new System.Drawing.Point(307, 14);
            this.LangLabelsCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LangLabelsCB.Name = "LangLabelsCB";
            this.LangLabelsCB.Size = new System.Drawing.Size(88, 24);
            this.LangLabelsCB.TabIndex = 12;
            this.LangLabelsCB.Text = "Labels";
            this.LangLabelsCB.UseVisualStyleBackColor = true;
            this.LangLabelsCB.CheckedChanged += new System.EventHandler(this.LangCB_CheckedChanged);
            // 
            // LangRightsCB
            // 
            this.LangRightsCB.AutoSize = true;
            this.LangRightsCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LangRightsCB.Location = new System.Drawing.Point(215, 14);
            this.LangRightsCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LangRightsCB.Name = "LangRightsCB";
            this.LangRightsCB.Size = new System.Drawing.Size(87, 24);
            this.LangRightsCB.TabIndex = 11;
            this.LangRightsCB.Text = "Rights";
            this.LangRightsCB.UseVisualStyleBackColor = true;
            this.LangRightsCB.CheckedChanged += new System.EventHandler(this.LangCB_CheckedChanged);
            // 
            // LangFunctionsCB
            // 
            this.LangFunctionsCB.AutoSize = true;
            this.LangFunctionsCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LangFunctionsCB.Location = new System.Drawing.Point(96, 14);
            this.LangFunctionsCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LangFunctionsCB.Name = "LangFunctionsCB";
            this.LangFunctionsCB.Size = new System.Drawing.Size(114, 24);
            this.LangFunctionsCB.TabIndex = 10;
            this.LangFunctionsCB.Text = "Functions";
            this.LangFunctionsCB.UseVisualStyleBackColor = true;
            this.LangFunctionsCB.CheckedChanged += new System.EventHandler(this.LangCB_CheckedChanged);
            // 
            // LangRolesCB
            // 
            this.LangRolesCB.AutoSize = true;
            this.LangRolesCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LangRolesCB.Location = new System.Drawing.Point(9, 14);
            this.LangRolesCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LangRolesCB.Name = "LangRolesCB";
            this.LangRolesCB.Size = new System.Drawing.Size(81, 24);
            this.LangRolesCB.TabIndex = 9;
            this.LangRolesCB.Text = "Roles";
            this.LangRolesCB.UseVisualStyleBackColor = true;
            this.LangRolesCB.CheckedChanged += new System.EventHandler(this.LangCB_CheckedChanged);
            // 
            // btnUpdateLanguages
            // 
            this.btnUpdateLanguages.Location = new System.Drawing.Point(3, 798);
            this.btnUpdateLanguages.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUpdateLanguages.Name = "btnUpdateLanguages";
            this.btnUpdateLanguages.Size = new System.Drawing.Size(84, 35);
            this.btnUpdateLanguages.TabIndex = 1;
            this.btnUpdateLanguages.Text = "Include";
            this.btnUpdateLanguages.UseVisualStyleBackColor = true;
            this.btnUpdateLanguages.Click += new System.EventHandler(this.btnUpdateLanguages_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.NewLangTab);
            this.tabControl1.Controls.Add(this.UpdateLangTab);
            this.tabControl1.Location = new System.Drawing.Point(7, 60);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1653, 730);
            this.tabControl1.TabIndex = 0;
            // 
            // NewLangTab
            // 
            this.NewLangTab.Controls.Add(this.NewLangExistingItemCB);
            this.NewLangTab.Controls.Add(this.NewLangNewItemCB);
            this.NewLangTab.Controls.Add(this.NewLangNewListCB);
            this.NewLangTab.Controls.Add(this.NewLangSelectAllCB);
            this.NewLangTab.Controls.Add(this.NewLangView);
            this.NewLangTab.Location = new System.Drawing.Point(4, 29);
            this.NewLangTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewLangTab.Name = "NewLangTab";
            this.NewLangTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewLangTab.Size = new System.Drawing.Size(1645, 697);
            this.NewLangTab.TabIndex = 0;
            this.NewLangTab.Text = "New";
            this.NewLangTab.UseVisualStyleBackColor = true;
            // 
            // NewLangExistingItemCB
            // 
            this.NewLangExistingItemCB.AutoSize = true;
            this.NewLangExistingItemCB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.NewLangExistingItemCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewLangExistingItemCB.Location = new System.Drawing.Point(755, 9);
            this.NewLangExistingItemCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewLangExistingItemCB.Name = "NewLangExistingItemCB";
            this.NewLangExistingItemCB.Size = new System.Drawing.Size(292, 24);
            this.NewLangExistingItemCB.TabIndex = 27;
            this.NewLangExistingItemCB.Text = "New Languages - Existing Items";
            this.NewLangExistingItemCB.UseVisualStyleBackColor = false;
            this.NewLangExistingItemCB.CheckedChanged += new System.EventHandler(this.NewLangExistingItemCB_CheckedChanged);
            // 
            // NewLangNewItemCB
            // 
            this.NewLangNewItemCB.AutoSize = true;
            this.NewLangNewItemCB.BackColor = System.Drawing.Color.LightGray;
            this.NewLangNewItemCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewLangNewItemCB.Location = new System.Drawing.Point(485, 8);
            this.NewLangNewItemCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewLangNewItemCB.Name = "NewLangNewItemCB";
            this.NewLangNewItemCB.Size = new System.Drawing.Size(263, 24);
            this.NewLangNewItemCB.TabIndex = 26;
            this.NewLangNewItemCB.Text = "New Languages - New Items";
            this.NewLangNewItemCB.UseVisualStyleBackColor = false;
            this.NewLangNewItemCB.CheckedChanged += new System.EventHandler(this.NewLangNewItemCB_CheckedChanged);
            // 
            // NewLangNewListCB
            // 
            this.NewLangNewListCB.AutoSize = true;
            this.NewLangNewListCB.BackColor = System.Drawing.Color.LightBlue;
            this.NewLangNewListCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewLangNewListCB.Location = new System.Drawing.Point(186, 8);
            this.NewLangNewListCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewLangNewListCB.Name = "NewLangNewListCB";
            this.NewLangNewListCB.Size = new System.Drawing.Size(280, 24);
            this.NewLangNewListCB.TabIndex = 25;
            this.NewLangNewListCB.Text = "New Languages - New Datalist";
            this.NewLangNewListCB.UseVisualStyleBackColor = false;
            this.NewLangNewListCB.CheckedChanged += new System.EventHandler(this.NewLangNewListCB_CheckedChanged);
            // 
            // NewLangSelectAllCB
            // 
            this.NewLangSelectAllCB.AutoSize = true;
            this.NewLangSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewLangSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewLangSelectAllCB.Location = new System.Drawing.Point(7, 8);
            this.NewLangSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewLangSelectAllCB.Name = "NewLangSelectAllCB";
            this.NewLangSelectAllCB.Size = new System.Drawing.Size(111, 24);
            this.NewLangSelectAllCB.TabIndex = 12;
            this.NewLangSelectAllCB.Text = "Select All";
            this.NewLangSelectAllCB.UseVisualStyleBackColor = true;
            this.NewLangSelectAllCB.CheckedChanged += new System.EventHandler(this.NewLangSelectAllCB_CheckedChanged);
            // 
            // NewLangView
            // 
            this.NewLangView.AllowUserToAddRows = false;
            this.NewLangView.AllowUserToDeleteRows = false;
            this.NewLangView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NewLangView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn10,
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn24,
            this.Locale,
            this.Desc,
            this.LongDescription,
            this.dataGridViewTextBoxColumn32});
            this.NewLangView.Location = new System.Drawing.Point(7, 41);
            this.NewLangView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewLangView.Name = "NewLangView";
            this.NewLangView.RowTemplate.Height = 24;
            this.NewLangView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NewLangView.Size = new System.Drawing.Size(1630, 645);
            this.NewLangView.TabIndex = 7;
            this.NewLangView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.NewLangView_RowsAdded);
            // 
            // dataGridViewCheckBoxColumn10
            // 
            this.dataGridViewCheckBoxColumn10.HeaderText = "";
            this.dataGridViewCheckBoxColumn10.Name = "dataGridViewCheckBoxColumn10";
            this.dataGridViewCheckBoxColumn10.Width = 25;
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn23.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.ReadOnly = true;
            this.dataGridViewTextBoxColumn23.Width = 400;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn24.HeaderText = "Code";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.ReadOnly = true;
            this.dataGridViewTextBoxColumn24.Width = 250;
            // 
            // Locale
            // 
            this.Locale.DataPropertyName = "LocaleID";
            this.Locale.HeaderText = "Locale";
            this.Locale.Name = "Locale";
            this.Locale.ReadOnly = true;
            // 
            // Desc
            // 
            this.Desc.DataPropertyName = "Description";
            this.Desc.HeaderText = "Description";
            this.Desc.Name = "Desc";
            this.Desc.ReadOnly = true;
            this.Desc.Width = 250;
            // 
            // LongDescription
            // 
            this.LongDescription.DataPropertyName = "LongDescription";
            this.LongDescription.HeaderText = "Long Description";
            this.LongDescription.Name = "LongDescription";
            this.LongDescription.ReadOnly = true;
            this.LongDescription.Width = 370;
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
            // UpdateLangTab
            // 
            this.UpdateLangTab.Controls.Add(this.UpdateLangSelectAllCB);
            this.UpdateLangTab.Controls.Add(this.TargetUpdateLangView);
            this.UpdateLangTab.Controls.Add(this.SourceUpdateLangView);
            this.UpdateLangTab.Location = new System.Drawing.Point(4, 29);
            this.UpdateLangTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UpdateLangTab.Name = "UpdateLangTab";
            this.UpdateLangTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UpdateLangTab.Size = new System.Drawing.Size(1645, 697);
            this.UpdateLangTab.TabIndex = 1;
            this.UpdateLangTab.Text = "Update";
            this.UpdateLangTab.UseVisualStyleBackColor = true;
            // 
            // UpdateLangSelectAllCB
            // 
            this.UpdateLangSelectAllCB.AutoSize = true;
            this.UpdateLangSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UpdateLangSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateLangSelectAllCB.Location = new System.Drawing.Point(7, 8);
            this.UpdateLangSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UpdateLangSelectAllCB.Name = "UpdateLangSelectAllCB";
            this.UpdateLangSelectAllCB.Size = new System.Drawing.Size(111, 24);
            this.UpdateLangSelectAllCB.TabIndex = 12;
            this.UpdateLangSelectAllCB.Text = "Select All";
            this.UpdateLangSelectAllCB.UseVisualStyleBackColor = true;
            this.UpdateLangSelectAllCB.CheckedChanged += new System.EventHandler(this.UpdateLangSelectAllCB_CheckedChanged);
            // 
            // TargetUpdateLangView
            // 
            this.TargetUpdateLangView.AllowUserToAddRows = false;
            this.TargetUpdateLangView.AllowUserToDeleteRows = false;
            this.TargetUpdateLangView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TargetUpdateLangView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn12,
            this.dataGridViewTextBoxColumn34,
            this.dataGridViewTextBoxColumn35,
            this.dataGridViewTextBoxColumn36,
            this.dataGridViewTextBoxColumn37});
            this.TargetUpdateLangView.Location = new System.Drawing.Point(1012, 36);
            this.TargetUpdateLangView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TargetUpdateLangView.Name = "TargetUpdateLangView";
            this.TargetUpdateLangView.RowTemplate.Height = 24;
            this.TargetUpdateLangView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TargetUpdateLangView.Size = new System.Drawing.Size(621, 658);
            this.TargetUpdateLangView.TabIndex = 9;
            this.TargetUpdateLangView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TargetUpdateLangView_Scroll);
            // 
            // dataGridViewCheckBoxColumn12
            // 
            this.dataGridViewCheckBoxColumn12.HeaderText = "";
            this.dataGridViewCheckBoxColumn12.Name = "dataGridViewCheckBoxColumn12";
            this.dataGridViewCheckBoxColumn12.Visible = false;
            this.dataGridViewCheckBoxColumn12.Width = 25;
            // 
            // dataGridViewTextBoxColumn34
            // 
            this.dataGridViewTextBoxColumn34.DataPropertyName = "LocaleID";
            this.dataGridViewTextBoxColumn34.HeaderText = "Locale";
            this.dataGridViewTextBoxColumn34.Name = "dataGridViewTextBoxColumn34";
            this.dataGridViewTextBoxColumn34.ReadOnly = true;
            this.dataGridViewTextBoxColumn34.Width = 60;
            // 
            // dataGridViewTextBoxColumn35
            // 
            this.dataGridViewTextBoxColumn35.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn35.HeaderText = "Description";
            this.dataGridViewTextBoxColumn35.Name = "dataGridViewTextBoxColumn35";
            this.dataGridViewTextBoxColumn35.ReadOnly = true;
            this.dataGridViewTextBoxColumn35.Width = 225;
            // 
            // dataGridViewTextBoxColumn36
            // 
            this.dataGridViewTextBoxColumn36.DataPropertyName = "LongDescription";
            this.dataGridViewTextBoxColumn36.HeaderText = "Long Description";
            this.dataGridViewTextBoxColumn36.Name = "dataGridViewTextBoxColumn36";
            this.dataGridViewTextBoxColumn36.ReadOnly = true;
            this.dataGridViewTextBoxColumn36.Width = 220;
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
            // SourceUpdateLangView
            // 
            this.SourceUpdateLangView.AllowUserToAddRows = false;
            this.SourceUpdateLangView.AllowUserToDeleteRows = false;
            this.SourceUpdateLangView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SourceUpdateLangView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn11,
            this.dataGridViewTextBoxColumn25,
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn27,
            this.dataGridViewTextBoxColumn28,
            this.DesciptionModified,
            this.LongDescModified,
            this.dataGridViewTextBoxColumn29,
            this.dataGridViewTextBoxColumn30});
            this.SourceUpdateLangView.Location = new System.Drawing.Point(7, 36);
            this.SourceUpdateLangView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SourceUpdateLangView.Name = "SourceUpdateLangView";
            this.SourceUpdateLangView.RowTemplate.Height = 24;
            this.SourceUpdateLangView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SourceUpdateLangView.Size = new System.Drawing.Size(1006, 658);
            this.SourceUpdateLangView.TabIndex = 8;
            this.SourceUpdateLangView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SourceUpdateLangView_Scroll);
            // 
            // dataGridViewCheckBoxColumn11
            // 
            this.dataGridViewCheckBoxColumn11.HeaderText = "";
            this.dataGridViewCheckBoxColumn11.Name = "dataGridViewCheckBoxColumn11";
            this.dataGridViewCheckBoxColumn11.Width = 25;
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn25.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.ReadOnly = true;
            this.dataGridViewTextBoxColumn25.Width = 250;
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn26.HeaderText = "Code";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn27
            // 
            this.dataGridViewTextBoxColumn27.DataPropertyName = "LocaleID";
            this.dataGridViewTextBoxColumn27.HeaderText = "Locale";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.ReadOnly = true;
            this.dataGridViewTextBoxColumn27.Width = 60;
            // 
            // dataGridViewTextBoxColumn28
            // 
            this.dataGridViewTextBoxColumn28.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn28.HeaderText = "Description";
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            this.dataGridViewTextBoxColumn28.ReadOnly = true;
            this.dataGridViewTextBoxColumn28.Width = 215;
            // 
            // DesciptionModified
            // 
            this.DesciptionModified.DataPropertyName = "DescriptionModified";
            this.DesciptionModified.HeaderText = "Desc Modified";
            this.DesciptionModified.Name = "DesciptionModified";
            this.DesciptionModified.Visible = false;
            // 
            // LongDescModified
            // 
            this.LongDescModified.DataPropertyName = "LongDescriptionModified";
            this.LongDescModified.HeaderText = "LongDescModified";
            this.LongDescModified.Name = "LongDescModified";
            this.LongDescModified.Visible = false;
            // 
            // dataGridViewTextBoxColumn29
            // 
            this.dataGridViewTextBoxColumn29.DataPropertyName = "LongDescription";
            this.dataGridViewTextBoxColumn29.HeaderText = "Long Description";
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            this.dataGridViewTextBoxColumn29.ReadOnly = true;
            this.dataGridViewTextBoxColumn29.Width = 200;
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
            // Attributes
            // 
            this.Attributes.Controls.Add(this.btnUpdateAttribute);
            this.Attributes.Controls.Add(this.tabControl2);
            this.Attributes.Controls.Add(this.panel3);
            this.Attributes.Location = new System.Drawing.Point(4, 29);
            this.Attributes.Name = "Attributes";
            this.Attributes.Padding = new System.Windows.Forms.Padding(3);
            this.Attributes.Size = new System.Drawing.Size(1667, 838);
            this.Attributes.TabIndex = 3;
            this.Attributes.Text = "Attributes";
            this.Attributes.UseVisualStyleBackColor = true;
            // 
            // btnUpdateAttribute
            // 
            this.btnUpdateAttribute.Location = new System.Drawing.Point(18, 788);
            this.btnUpdateAttribute.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUpdateAttribute.Name = "btnUpdateAttribute";
            this.btnUpdateAttribute.Size = new System.Drawing.Size(84, 35);
            this.btnUpdateAttribute.TabIndex = 17;
            this.btnUpdateAttribute.Text = "Include";
            this.btnUpdateAttribute.UseVisualStyleBackColor = true;
            this.btnUpdateAttribute.Click += new System.EventHandler(this.btnUpdateAttribute_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Location = new System.Drawing.Point(7, 54);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1653, 730);
            this.tabControl2.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.NewAttributesView);
            this.tabPage1.Controls.Add(this.NewItemsAttributeSelectAllCB);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(1645, 697);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "New";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // NewAttributesView
            // 
            this.NewAttributesView.AllowUserToAddRows = false;
            this.NewAttributesView.AllowUserToDeleteRows = false;
            this.NewAttributesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NewAttributesView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn13,
            this.Column2,
            this.Column3,
            this.ID,
            this.Column1,
            this.dataGridViewTextBoxColumn31,
            this.dataGridViewTextBoxColumn33,
            this.dataGridViewTextBoxColumn38,
            this.dataGridViewTextBoxColumn41,
            this.dataGridViewTextBoxColumn50,
            this.dataGridViewTextBoxColumn52,
            this.Column4,
            this.Column5});
            this.NewAttributesView.Location = new System.Drawing.Point(7, 40);
            this.NewAttributesView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewAttributesView.Name = "NewAttributesView";
            this.NewAttributesView.RowTemplate.Height = 24;
            this.NewAttributesView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NewAttributesView.Size = new System.Drawing.Size(1630, 645);
            this.NewAttributesView.TabIndex = 13;
            // 
            // dataGridViewCheckBoxColumn13
            // 
            this.dataGridViewCheckBoxColumn13.HeaderText = "";
            this.dataGridViewCheckBoxColumn13.Name = "dataGridViewCheckBoxColumn13";
            this.dataGridViewCheckBoxColumn13.Width = 25;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "DataListTypeID";
            this.Column2.HeaderText = "DataListTypeID";
            this.Column2.Name = "Column2";
            this.Column2.Visible = false;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "DefaultTypeID";
            this.Column3.HeaderText = "DefaultTypeID";
            this.Column3.Name = "Column3";
            this.Column3.Visible = false;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "LastModifiedDate";
            this.Column1.HeaderText = "LastModifiedDate";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // dataGridViewTextBoxColumn31
            // 
            this.dataGridViewTextBoxColumn31.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn31.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
            this.dataGridViewTextBoxColumn31.ReadOnly = true;
            this.dataGridViewTextBoxColumn31.Width = 400;
            // 
            // dataGridViewTextBoxColumn33
            // 
            this.dataGridViewTextBoxColumn33.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn33.HeaderText = "Code";
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            this.dataGridViewTextBoxColumn33.ReadOnly = true;
            this.dataGridViewTextBoxColumn33.Width = 250;
            // 
            // dataGridViewTextBoxColumn38
            // 
            this.dataGridViewTextBoxColumn38.DataPropertyName = "ParentContentID";
            this.dataGridViewTextBoxColumn38.HeaderText = "ParentContenID";
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            this.dataGridViewTextBoxColumn38.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn41
            // 
            this.dataGridViewTextBoxColumn41.DataPropertyName = "IsActive";
            this.dataGridViewTextBoxColumn41.HeaderText = "IsActive";
            this.dataGridViewTextBoxColumn41.Name = "dataGridViewTextBoxColumn41";
            this.dataGridViewTextBoxColumn41.ReadOnly = true;
            this.dataGridViewTextBoxColumn41.Visible = false;
            this.dataGridViewTextBoxColumn41.Width = 250;
            // 
            // dataGridViewTextBoxColumn50
            // 
            this.dataGridViewTextBoxColumn50.DataPropertyName = "DataListID";
            this.dataGridViewTextBoxColumn50.HeaderText = "DataListID";
            this.dataGridViewTextBoxColumn50.Name = "dataGridViewTextBoxColumn50";
            this.dataGridViewTextBoxColumn50.ReadOnly = true;
            this.dataGridViewTextBoxColumn50.Visible = false;
            this.dataGridViewTextBoxColumn50.Width = 370;
            // 
            // dataGridViewTextBoxColumn52
            // 
            this.dataGridViewTextBoxColumn52.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn52.HeaderText = "Status";
            this.dataGridViewTextBoxColumn52.Name = "dataGridViewTextBoxColumn52";
            this.dataGridViewTextBoxColumn52.ReadOnly = true;
            this.dataGridViewTextBoxColumn52.Visible = false;
            this.dataGridViewTextBoxColumn52.Width = 60;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "DefaultTypeValue";
            this.Column4.HeaderText = "DefaultTypeValue";
            this.Column4.Name = "Column4";
            this.Column4.Visible = false;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "TenantID";
            this.Column5.HeaderText = "TenantID";
            this.Column5.Name = "Column5";
            this.Column5.Visible = false;
            // 
            // NewItemsAttributeSelectAllCB
            // 
            this.NewItemsAttributeSelectAllCB.AutoSize = true;
            this.NewItemsAttributeSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewItemsAttributeSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewItemsAttributeSelectAllCB.Location = new System.Drawing.Point(7, 8);
            this.NewItemsAttributeSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewItemsAttributeSelectAllCB.Name = "NewItemsAttributeSelectAllCB";
            this.NewItemsAttributeSelectAllCB.Size = new System.Drawing.Size(111, 24);
            this.NewItemsAttributeSelectAllCB.TabIndex = 12;
            this.NewItemsAttributeSelectAllCB.Text = "Select All";
            this.NewItemsAttributeSelectAllCB.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.TargetUpdateAttributeView);
            this.tabPage2.Controls.Add(this.checkBox10);
            this.tabPage2.Controls.Add(this.SourceUpdateAttributeView);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(1645, 697);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Update";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TargetUpdateAttributeView
            // 
            this.TargetUpdateAttributeView.AllowUserToAddRows = false;
            this.TargetUpdateAttributeView.AllowUserToDeleteRows = false;
            this.TargetUpdateAttributeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TargetUpdateAttributeView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn14,
            this.dataGridViewTextBoxColumn39,
            this.dataGridViewTextBoxColumn40,
            this.dataGridViewTextBoxColumn42,
            this.dataGridViewTextBoxColumn43,
            this.dataGridViewTextBoxColumn44,
            this.dataGridViewTextBoxColumn45});
            this.TargetUpdateAttributeView.Location = new System.Drawing.Point(788, 36);
            this.TargetUpdateAttributeView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TargetUpdateAttributeView.Name = "TargetUpdateAttributeView";
            this.TargetUpdateAttributeView.RowTemplate.Height = 24;
            this.TargetUpdateAttributeView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TargetUpdateAttributeView.Size = new System.Drawing.Size(836, 658);
            this.TargetUpdateAttributeView.TabIndex = 13;
            // 
            // dataGridViewCheckBoxColumn14
            // 
            this.dataGridViewCheckBoxColumn14.HeaderText = "";
            this.dataGridViewCheckBoxColumn14.Name = "dataGridViewCheckBoxColumn14";
            this.dataGridViewCheckBoxColumn14.Width = 25;
            // 
            // dataGridViewTextBoxColumn39
            // 
            this.dataGridViewTextBoxColumn39.DataPropertyName = "ParentContentID";
            this.dataGridViewTextBoxColumn39.HeaderText = "ParentContentID";
            this.dataGridViewTextBoxColumn39.Name = "dataGridViewTextBoxColumn39";
            // 
            // dataGridViewTextBoxColumn40
            // 
            this.dataGridViewTextBoxColumn40.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn40.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn40.Name = "dataGridViewTextBoxColumn40";
            this.dataGridViewTextBoxColumn40.ReadOnly = true;
            this.dataGridViewTextBoxColumn40.Width = 250;
            // 
            // dataGridViewTextBoxColumn42
            // 
            this.dataGridViewTextBoxColumn42.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn42.HeaderText = "Code";
            this.dataGridViewTextBoxColumn42.Name = "dataGridViewTextBoxColumn42";
            this.dataGridViewTextBoxColumn42.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn43
            // 
            this.dataGridViewTextBoxColumn43.DataPropertyName = "IsActive";
            this.dataGridViewTextBoxColumn43.HeaderText = "IsActive";
            this.dataGridViewTextBoxColumn43.Name = "dataGridViewTextBoxColumn43";
            this.dataGridViewTextBoxColumn43.ReadOnly = true;
            this.dataGridViewTextBoxColumn43.Width = 60;
            // 
            // dataGridViewTextBoxColumn44
            // 
            this.dataGridViewTextBoxColumn44.DataPropertyName = "LastModifiedDate";
            this.dataGridViewTextBoxColumn44.HeaderText = "LastModifiedDate";
            this.dataGridViewTextBoxColumn44.Name = "dataGridViewTextBoxColumn44";
            this.dataGridViewTextBoxColumn44.ReadOnly = true;
            this.dataGridViewTextBoxColumn44.Visible = false;
            this.dataGridViewTextBoxColumn44.Width = 215;
            // 
            // dataGridViewTextBoxColumn45
            // 
            this.dataGridViewTextBoxColumn45.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn45.HeaderText = "Status";
            this.dataGridViewTextBoxColumn45.Name = "dataGridViewTextBoxColumn45";
            this.dataGridViewTextBoxColumn45.ReadOnly = true;
            this.dataGridViewTextBoxColumn45.Visible = false;
            this.dataGridViewTextBoxColumn45.Width = 60;
            // 
            // checkBox10
            // 
            this.checkBox10.AutoSize = true;
            this.checkBox10.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox10.Location = new System.Drawing.Point(7, 8);
            this.checkBox10.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(111, 24);
            this.checkBox10.TabIndex = 12;
            this.checkBox10.Text = "Select All";
            this.checkBox10.UseVisualStyleBackColor = true;
            // 
            // SourceUpdateAttributeView
            // 
            this.SourceUpdateAttributeView.AllowUserToAddRows = false;
            this.SourceUpdateAttributeView.AllowUserToDeleteRows = false;
            this.SourceUpdateAttributeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SourceUpdateAttributeView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn15,
            this.dataGridViewTextBoxColumn51,
            this.dataGridViewTextBoxColumn46,
            this.dataGridViewTextBoxColumn47,
            this.dataGridViewTextBoxColumn48,
            this.dataGridViewTextBoxColumn49,
            this.dataGridViewTextBoxColumn53});
            this.SourceUpdateAttributeView.Location = new System.Drawing.Point(7, 36);
            this.SourceUpdateAttributeView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SourceUpdateAttributeView.Name = "SourceUpdateAttributeView";
            this.SourceUpdateAttributeView.RowTemplate.Height = 24;
            this.SourceUpdateAttributeView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SourceUpdateAttributeView.Size = new System.Drawing.Size(836, 658);
            this.SourceUpdateAttributeView.TabIndex = 8;
            // 
            // dataGridViewCheckBoxColumn15
            // 
            this.dataGridViewCheckBoxColumn15.HeaderText = "";
            this.dataGridViewCheckBoxColumn15.Name = "dataGridViewCheckBoxColumn15";
            this.dataGridViewCheckBoxColumn15.Width = 25;
            // 
            // dataGridViewTextBoxColumn51
            // 
            this.dataGridViewTextBoxColumn51.DataPropertyName = "ParentContentID";
            this.dataGridViewTextBoxColumn51.HeaderText = "ParentContentID";
            this.dataGridViewTextBoxColumn51.Name = "dataGridViewTextBoxColumn51";
            // 
            // dataGridViewTextBoxColumn46
            // 
            this.dataGridViewTextBoxColumn46.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn46.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn46.Name = "dataGridViewTextBoxColumn46";
            this.dataGridViewTextBoxColumn46.ReadOnly = true;
            this.dataGridViewTextBoxColumn46.Width = 250;
            // 
            // dataGridViewTextBoxColumn47
            // 
            this.dataGridViewTextBoxColumn47.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn47.HeaderText = "Code";
            this.dataGridViewTextBoxColumn47.Name = "dataGridViewTextBoxColumn47";
            this.dataGridViewTextBoxColumn47.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn48
            // 
            this.dataGridViewTextBoxColumn48.DataPropertyName = "IsActive";
            this.dataGridViewTextBoxColumn48.HeaderText = "IsActive";
            this.dataGridViewTextBoxColumn48.Name = "dataGridViewTextBoxColumn48";
            this.dataGridViewTextBoxColumn48.ReadOnly = true;
            this.dataGridViewTextBoxColumn48.Width = 60;
            // 
            // dataGridViewTextBoxColumn49
            // 
            this.dataGridViewTextBoxColumn49.DataPropertyName = "LastModifiedDate";
            this.dataGridViewTextBoxColumn49.HeaderText = "LastModifiedDate";
            this.dataGridViewTextBoxColumn49.Name = "dataGridViewTextBoxColumn49";
            this.dataGridViewTextBoxColumn49.ReadOnly = true;
            this.dataGridViewTextBoxColumn49.Visible = false;
            this.dataGridViewTextBoxColumn49.Width = 215;
            // 
            // dataGridViewTextBoxColumn53
            // 
            this.dataGridViewTextBoxColumn53.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn53.HeaderText = "Status";
            this.dataGridViewTextBoxColumn53.Name = "dataGridViewTextBoxColumn53";
            this.dataGridViewTextBoxColumn53.ReadOnly = true;
            this.dataGridViewTextBoxColumn53.Visible = false;
            this.dataGridViewTextBoxColumn53.Width = 60;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.Controls.Add(this.AttributesDataListCB);
            this.panel3.Controls.Add(this.AttributesMsgCB);
            this.panel3.Controls.Add(this.AttributesRightsCB);
            this.panel3.Controls.Add(this.AttributesFunctionCB);
            this.panel3.Controls.Add(this.AttributesRoleCB);
            this.panel3.Location = new System.Drawing.Point(3, 7);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(580, 51);
            this.panel3.TabIndex = 15;
            // 
            // AttributesDataListCB
            // 
            this.AttributesDataListCB.AutoSize = true;
            this.AttributesDataListCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttributesDataListCB.Location = new System.Drawing.Point(452, 14);
            this.AttributesDataListCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AttributesDataListCB.Name = "AttributesDataListCB";
            this.AttributesDataListCB.Size = new System.Drawing.Size(97, 24);
            this.AttributesDataListCB.TabIndex = 15;
            this.AttributesDataListCB.Text = "Datalist";
            this.AttributesDataListCB.UseVisualStyleBackColor = true;
            // 
            // AttributesMsgCB
            // 
            this.AttributesMsgCB.AutoSize = true;
            this.AttributesMsgCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttributesMsgCB.Location = new System.Drawing.Point(317, 14);
            this.AttributesMsgCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AttributesMsgCB.Name = "AttributesMsgCB";
            this.AttributesMsgCB.Size = new System.Drawing.Size(116, 24);
            this.AttributesMsgCB.TabIndex = 13;
            this.AttributesMsgCB.Text = "Messages";
            this.AttributesMsgCB.UseVisualStyleBackColor = true;
            // 
            // AttributesRightsCB
            // 
            this.AttributesRightsCB.AutoSize = true;
            this.AttributesRightsCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttributesRightsCB.Location = new System.Drawing.Point(215, 14);
            this.AttributesRightsCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AttributesRightsCB.Name = "AttributesRightsCB";
            this.AttributesRightsCB.Size = new System.Drawing.Size(87, 24);
            this.AttributesRightsCB.TabIndex = 11;
            this.AttributesRightsCB.Text = "Rights";
            this.AttributesRightsCB.UseVisualStyleBackColor = true;
            // 
            // AttributesFunctionCB
            // 
            this.AttributesFunctionCB.AutoSize = true;
            this.AttributesFunctionCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttributesFunctionCB.Location = new System.Drawing.Point(96, 14);
            this.AttributesFunctionCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AttributesFunctionCB.Name = "AttributesFunctionCB";
            this.AttributesFunctionCB.Size = new System.Drawing.Size(114, 24);
            this.AttributesFunctionCB.TabIndex = 10;
            this.AttributesFunctionCB.Text = "Functions";
            this.AttributesFunctionCB.UseVisualStyleBackColor = true;
            // 
            // AttributesRoleCB
            // 
            this.AttributesRoleCB.AutoSize = true;
            this.AttributesRoleCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttributesRoleCB.Location = new System.Drawing.Point(9, 14);
            this.AttributesRoleCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AttributesRoleCB.Name = "AttributesRoleCB";
            this.AttributesRoleCB.Size = new System.Drawing.Size(81, 24);
            this.AttributesRoleCB.TabIndex = 9;
            this.AttributesRoleCB.Text = "Roles";
            this.AttributesRoleCB.UseVisualStyleBackColor = true;
            this.AttributesRoleCB.CheckedChanged += new System.EventHandler(this.AttributesRoleCB_CheckedChanged_1);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(1595, 920);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 40);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // PreviewUpdate
            // 
            this.PreviewUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreviewUpdate.Location = new System.Drawing.Point(1380, 920);
            this.PreviewUpdate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PreviewUpdate.Name = "PreviewUpdate";
            this.PreviewUpdate.Size = new System.Drawing.Size(198, 40);
            this.PreviewUpdate.TabIndex = 2;
            this.PreviewUpdate.Text = "Preview && Update";
            this.PreviewUpdate.UseVisualStyleBackColor = true;
            this.PreviewUpdate.Click += new System.EventHandler(this.PreviewUpdate_Click);
            // 
            // ModuleList
            // 
            this.ModuleList.DisplayMember = "ModuleName";
            this.ModuleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModuleList.FormattingEnabled = true;
            this.ModuleList.Location = new System.Drawing.Point(87, 10);
            this.ModuleList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ModuleList.Name = "ModuleList";
            this.ModuleList.Size = new System.Drawing.Size(312, 28);
            this.ModuleList.Sorted = true;
            this.ModuleList.TabIndex = 28;
            this.ModuleList.ValueMember = "TenantModuleID";
            this.ModuleList.SelectedIndexChanged += new System.EventHandler(this.ModuleList_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 20);
            this.label4.TabIndex = 27;
            this.label4.Text = "Module:";
            // 
            // datalistDiff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1692, 961);
            this.Controls.Add(this.ModuleList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PreviewUpdate);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.diffTab);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "datalistDiff";
            this.Text = "Delta";
            this.diffTab.ResumeLayout(false);
            this.DatalistTabPage.ResumeLayout(false);
            this.DatalistTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataListView)).EndInit();
            this.itemTabPage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ItemsTab.ResumeLayout(false);
            this.NewItemsPage.ResumeLayout(false);
            this.NewItemsPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewItemsView)).EndInit();
            this.UpdateItemsTab.ResumeLayout(false);
            this.UpdateItemsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateTargetItemView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateSourceItemView)).EndInit();
            this.ItemLanguages.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.NewLangTab.ResumeLayout(false);
            this.NewLangTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewLangView)).EndInit();
            this.UpdateLangTab.ResumeLayout(false);
            this.UpdateLangTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetUpdateLangView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceUpdateLangView)).EndInit();
            this.Attributes.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewAttributesView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetUpdateAttributeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceUpdateAttributeView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       
    }
}