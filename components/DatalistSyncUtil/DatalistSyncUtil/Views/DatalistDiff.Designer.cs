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
        private System.Windows.Forms.TabControl ItemsTab;
        private System.Windows.Forms.TabPage NewItemsPage;
        private System.Windows.Forms.DataGridView NewItemsView;
        private System.Windows.Forms.TabPage UpdateItemsTab;
        private System.Windows.Forms.DataGridView UpdateTargetItemView;
        private System.Windows.Forms.DataGridView UpdateSourceItemView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage ItemLanguages;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage NewLangTab;
        private System.Windows.Forms.DataGridView NewLangView;
        private System.Windows.Forms.TabPage UpdateLangTab;
        private System.Windows.Forms.DataGridView TargetUpdateLangView;
        private System.Windows.Forms.DataGridView SourceUpdateLangView;
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
        private System.Windows.Forms.Button btnUpdateAttribute;
        private System.Windows.Forms.DataGridView TargetUpdateAttributeView;
        private System.Windows.Forms.DataGridView NewAttributesView;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle89 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle90 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle91 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle92 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle93 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle94 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle95 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle96 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle97 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle98 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle99 = new System.Windows.Forms.DataGridViewCellStyle();
            this.diffTab = new System.Windows.Forms.TabControl();
            this.DatalistTabPage = new System.Windows.Forms.TabPage();
            this.DatalistSelectAllChkBox = new System.Windows.Forms.CheckBox();
            this.btnListUpdate = new System.Windows.Forms.Button();
            this.DataListView = new System.Windows.Forms.DataGridView();
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
            this.NewItemsSelectAllCB = new System.Windows.Forms.CheckBox();
            this.UpdateItemsTab = new System.Windows.Forms.TabPage();
            this.UpdateItemsSelectAllCB = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UpdateTargetItemView = new System.Windows.Forms.DataGridView();
            this.UpdateSourceItemView = new System.Windows.Forms.DataGridView();
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
            this.UpdateLangTab = new System.Windows.Forms.TabPage();
            this.UpdateLangSelectAllCB = new System.Windows.Forms.CheckBox();
            this.TargetUpdateLangView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn12 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongDescriptionModifiedTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescriptionModifiedTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.lblTypeDataListItem = new System.Windows.Forms.Label();
            this.lblTypeDataList = new System.Windows.Forms.Label();
            this.NewAttributesView = new System.Windows.Forms.DataGridView();
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
            this.tabpage3 = new System.Windows.Forms.TabPage();
            this.btnLinkitem = new System.Windows.Forms.Button();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.LinkgridView = new System.Windows.Forms.DataGridView();
            this.NewLinkView = new System.Windows.Forms.DataGridView();
            this.NewLinkSelectAllCB = new System.Windows.Forms.CheckBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.TargetLinkView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn20 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn91 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn92 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn93 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn94 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn95 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn96 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn97 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn98 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn99 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn100 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn101 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn102 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn103 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn104 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceLinkView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn19 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn59 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn78 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn79 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn80 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn81 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn82 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn83 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn84 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn85 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn86 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn87 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn88 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn89 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn90 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn17 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn66 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn67 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn68 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn69 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn70 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn71 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn18 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn72 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn73 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn74 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn75 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn76 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn77 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.ItemAttribute = new System.Windows.Forms.TabPage();
            this.btnItemAttribute = new System.Windows.Forms.Button();
            this.tabControl4 = new System.Windows.Forms.TabControl();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.NewItemAttrExitingItemsCB = new System.Windows.Forms.CheckBox();
            this.NewItemAtrrNewItemsCB = new System.Windows.Forms.CheckBox();
            this.NewItemAttrNewDataListCB = new System.Windows.Forms.CheckBox();
            this.NewItemAttributeView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn21 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DataListTypeNameItemAtrr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn109 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataListAttributeNameItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataListAtrributeValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataListItemIDItemAttribute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataListTypeIDItemAttr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataListValueIDItemAttr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDItenAtrr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastModifiedDateItemAttr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusItemAttr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataListAttributeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataListTypeNameItemAttr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsEditableItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewItemsAttrSelectAllCB = new System.Windows.Forms.CheckBox();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.checkBox17 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.UpdateTargetItemAttributeView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn24 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn118 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn119 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn120 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn121 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn122 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn123 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn124 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn125 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn126 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn25 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn26 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn127 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateSourceItemAttributeView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn27 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn128 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn129 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn130 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn131 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn132 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn133 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn134 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn135 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn136 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn28 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn29 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn137 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.PreviewUpdate = new System.Windows.Forms.Button();
            this.ModuleList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
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
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ContentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsEditable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ItemsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn10 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Locale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemIDLang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActiveLang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescriptionModifiedLang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongDescrptionModifiedLang = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.TypeNameAttribute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsEditableAttribute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn16 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn54 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn55 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn56 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn57 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn58 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn60 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn61 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn62 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn63 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn64 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn65 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn105 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn106 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn107 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.tabpage3.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LinkgridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewLinkView)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetLinkView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceLinkView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.panel4.SuspendLayout();
            this.ItemAttribute.SuspendLayout();
            this.tabControl4.SuspendLayout();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewItemAttributeView)).BeginInit();
            this.tabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateTargetItemAttributeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateSourceItemAttributeView)).BeginInit();
            this.SuspendLayout();
            // 
            // diffTab
            // 
            this.diffTab.Controls.Add(this.DatalistTabPage);
            this.diffTab.Controls.Add(this.itemTabPage);
            this.diffTab.Controls.Add(this.ItemLanguages);
            this.diffTab.Controls.Add(this.Attributes);
            this.diffTab.Controls.Add(this.tabpage3);
            this.diffTab.Controls.Add(this.ItemAttribute);
            this.diffTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diffTab.Location = new System.Drawing.Point(16, 47);
            this.diffTab.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.diffTab.Name = "diffTab";
            this.diffTab.SelectedIndex = 0;
            this.diffTab.Size = new System.Drawing.Size(1676, 870);
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
            this.DatalistTabPage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.DatalistTabPage.Name = "DatalistTabPage";
            this.DatalistTabPage.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.DatalistTabPage.Size = new System.Drawing.Size(1668, 837);
            this.DatalistTabPage.TabIndex = 0;
            this.DatalistTabPage.Text = "Datalist";
            this.DatalistTabPage.UseVisualStyleBackColor = true;
            // 
            // DatalistSelectAllChkBox
            // 
            this.DatalistSelectAllChkBox.AutoSize = true;
            this.DatalistSelectAllChkBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DatalistSelectAllChkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatalistSelectAllChkBox.Location = new System.Drawing.Point(8, 9);
            this.DatalistSelectAllChkBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.btnListUpdate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnListUpdate.Name = "btnListUpdate";
            this.btnListUpdate.Size = new System.Drawing.Size(94, 40);
            this.btnListUpdate.TabIndex = 2;
            this.btnListUpdate.Text = "Include";
            this.btnListUpdate.UseVisualStyleBackColor = true;
            this.btnListUpdate.Click += new System.EventHandler(this.BtnListUpdate_Click);
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
            this.DataListView.Location = new System.Drawing.Point(0, 45);
            this.DataListView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.DataListView.Name = "DataListView";
            this.DataListView.RowTemplate.Height = 24;
            this.DataListView.Size = new System.Drawing.Size(1425, 735);
            this.DataListView.TabIndex = 3;
            // 
            // itemTabPage
            // 
            this.itemTabPage.Controls.Add(this.panel1);
            this.itemTabPage.Controls.Add(this.btnUpdateItems);
            this.itemTabPage.Controls.Add(this.ItemsTab);
            this.itemTabPage.Location = new System.Drawing.Point(4, 29);
            this.itemTabPage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.itemTabPage.Name = "itemTabPage";
            this.itemTabPage.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.itemTabPage.Size = new System.Drawing.Size(1668, 837);
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
            this.panel1.Location = new System.Drawing.Point(10, 5);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 51);
            this.panel1.TabIndex = 13;
            // 
            // ItemDatalistCB
            // 
            this.ItemDatalistCB.AutoSize = true;
            this.ItemDatalistCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemDatalistCB.Location = new System.Drawing.Point(516, 14);
            this.ItemDatalistCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.ItemMessagesCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.ItemLabelsCB.Location = new System.Drawing.Point(308, 14);
            this.ItemLabelsCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.ItemRightsCB.Location = new System.Drawing.Point(214, 14);
            this.ItemRightsCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.ItemFunctionsCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.ItemRolesCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.btnUpdateItems.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnUpdateItems.Name = "btnUpdateItems";
            this.btnUpdateItems.Size = new System.Drawing.Size(110, 32);
            this.btnUpdateItems.TabIndex = 1;
            this.btnUpdateItems.Text = "Include";
            this.btnUpdateItems.UseVisualStyleBackColor = true;
            this.btnUpdateItems.Click += new System.EventHandler(this.BtnUpdateItems_Click);
            // 
            // ItemsTab
            // 
            this.ItemsTab.Controls.Add(this.NewItemsPage);
            this.ItemsTab.Controls.Add(this.UpdateItemsTab);
            this.ItemsTab.Location = new System.Drawing.Point(6, 62);
            this.ItemsTab.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ItemsTab.Name = "ItemsTab";
            this.ItemsTab.SelectedIndex = 0;
            this.ItemsTab.Size = new System.Drawing.Size(1658, 738);
            this.ItemsTab.TabIndex = 0;
            // 
            // NewItemsPage
            // 
            this.NewItemsPage.Controls.Add(this.ExistingListNewItemCB);
            this.NewItemsPage.Controls.Add(this.NewListNewItemCB);
            this.NewItemsPage.Controls.Add(this.NewItemsView);
            this.NewItemsPage.Controls.Add(this.NewItemsSelectAllCB);
            this.NewItemsPage.Location = new System.Drawing.Point(4, 29);
            this.NewItemsPage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewItemsPage.Name = "NewItemsPage";
            this.NewItemsPage.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewItemsPage.Size = new System.Drawing.Size(1650, 705);
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
            this.ExistingListNewItemCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.NewListNewItemCB.Location = new System.Drawing.Point(170, 8);
            this.NewListNewItemCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.NewItemsView.Location = new System.Drawing.Point(18, 42);
            this.NewItemsView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewItemsView.Name = "NewItemsView";
            this.NewItemsView.RowTemplate.Height = 24;
            this.NewItemsView.Size = new System.Drawing.Size(1286, 652);
            this.NewItemsView.TabIndex = 6;
            this.NewItemsView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.NewItemsView_RowsAdded);
            // 
            // NewItemsSelectAllCB
            // 
            this.NewItemsSelectAllCB.AutoSize = true;
            this.NewItemsSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewItemsSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewItemsSelectAllCB.Location = new System.Drawing.Point(3, 8);
            this.NewItemsSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.UpdateItemsTab.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.UpdateItemsTab.Name = "UpdateItemsTab";
            this.UpdateItemsTab.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.UpdateItemsTab.Size = new System.Drawing.Size(1650, 705);
            this.UpdateItemsTab.TabIndex = 1;
            this.UpdateItemsTab.Text = "Update Items";
            this.UpdateItemsTab.UseVisualStyleBackColor = true;
            // 
            // UpdateItemsSelectAllCB
            // 
            this.UpdateItemsSelectAllCB.AutoSize = true;
            this.UpdateItemsSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateItemsSelectAllCB.Location = new System.Drawing.Point(98, 18);
            this.UpdateItemsSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.label1.Location = new System.Drawing.Point(8, 18);
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
            this.UpdateTargetItemView.Location = new System.Drawing.Point(995, 48);
            this.UpdateTargetItemView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.UpdateTargetItemView.Name = "UpdateTargetItemView";
            this.UpdateTargetItemView.RowTemplate.Height = 24;
            this.UpdateTargetItemView.Size = new System.Drawing.Size(648, 653);
            this.UpdateTargetItemView.TabIndex = 8;
            this.UpdateTargetItemView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.UpdateTargetItemView_Scroll);
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
            this.UpdateSourceItemView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.UpdateSourceItemView.Name = "UpdateSourceItemView";
            this.UpdateSourceItemView.RowTemplate.Height = 24;
            this.UpdateSourceItemView.Size = new System.Drawing.Size(996, 653);
            this.UpdateSourceItemView.TabIndex = 7;
            this.UpdateSourceItemView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.UpdateSourceItemView_RowAdded);
            this.UpdateSourceItemView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.UpdateSourceItemView_Scroll);
            // 
            // ItemLanguages
            // 
            this.ItemLanguages.Controls.Add(this.panel2);
            this.ItemLanguages.Controls.Add(this.btnUpdateLanguages);
            this.ItemLanguages.Controls.Add(this.tabControl1);
            this.ItemLanguages.Location = new System.Drawing.Point(4, 29);
            this.ItemLanguages.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ItemLanguages.Name = "ItemLanguages";
            this.ItemLanguages.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ItemLanguages.Size = new System.Drawing.Size(1668, 837);
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
            this.panel2.Location = new System.Drawing.Point(8, 6);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(622, 51);
            this.panel2.TabIndex = 14;
            // 
            // LangDatalistCB
            // 
            this.LangDatalistCB.AutoSize = true;
            this.LangDatalistCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LangDatalistCB.Location = new System.Drawing.Point(524, 14);
            this.LangDatalistCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.LangMessagesCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.LangLabelsCB.Location = new System.Drawing.Point(308, 14);
            this.LangLabelsCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.LangRightsCB.Location = new System.Drawing.Point(214, 14);
            this.LangRightsCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.LangFunctionsCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.LangRolesCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.btnUpdateLanguages.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnUpdateLanguages.Name = "btnUpdateLanguages";
            this.btnUpdateLanguages.Size = new System.Drawing.Size(102, 35);
            this.btnUpdateLanguages.TabIndex = 1;
            this.btnUpdateLanguages.Text = "Include";
            this.btnUpdateLanguages.UseVisualStyleBackColor = true;
            this.btnUpdateLanguages.Click += new System.EventHandler(this.BtnUpdateLanguages_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.NewLangTab);
            this.tabControl1.Controls.Add(this.UpdateLangTab);
            this.tabControl1.Location = new System.Drawing.Point(8, 60);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1653, 729);
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
            this.NewLangTab.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewLangTab.Name = "NewLangTab";
            this.NewLangTab.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewLangTab.Size = new System.Drawing.Size(1645, 696);
            this.NewLangTab.TabIndex = 0;
            this.NewLangTab.Text = "New";
            this.NewLangTab.UseVisualStyleBackColor = true;
            // 
            // NewLangExistingItemCB
            // 
            this.NewLangExistingItemCB.AutoSize = true;
            this.NewLangExistingItemCB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.NewLangExistingItemCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewLangExistingItemCB.Location = new System.Drawing.Point(754, 9);
            this.NewLangExistingItemCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.NewLangNewItemCB.Location = new System.Drawing.Point(484, 8);
            this.NewLangNewItemCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.NewLangNewListCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.NewLangSelectAllCB.Location = new System.Drawing.Point(8, 8);
            this.NewLangSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.dataGridViewTextBoxColumn32,
            this.ItemIDLang,
            this.IsActiveLang,
            this.DescriptionModifiedLang,
            this.LongDescrptionModifiedLang});
            this.NewLangView.Location = new System.Drawing.Point(8, 42);
            this.NewLangView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewLangView.Name = "NewLangView";
            this.NewLangView.RowTemplate.Height = 24;
            this.NewLangView.Size = new System.Drawing.Size(1630, 644);
            this.NewLangView.TabIndex = 7;
            this.NewLangView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.NewLangView_RowsAdded);
            // 
            // UpdateLangTab
            // 
            this.UpdateLangTab.Controls.Add(this.UpdateLangSelectAllCB);
            this.UpdateLangTab.Controls.Add(this.TargetUpdateLangView);
            this.UpdateLangTab.Controls.Add(this.SourceUpdateLangView);
            this.UpdateLangTab.Location = new System.Drawing.Point(4, 29);
            this.UpdateLangTab.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.UpdateLangTab.Name = "UpdateLangTab";
            this.UpdateLangTab.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.UpdateLangTab.Size = new System.Drawing.Size(1645, 696);
            this.UpdateLangTab.TabIndex = 1;
            this.UpdateLangTab.Text = "Update";
            this.UpdateLangTab.UseVisualStyleBackColor = true;
            // 
            // UpdateLangSelectAllCB
            // 
            this.UpdateLangSelectAllCB.AutoSize = true;
            this.UpdateLangSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UpdateLangSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateLangSelectAllCB.Location = new System.Drawing.Point(8, 8);
            this.UpdateLangSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.dataGridViewTextBoxColumn37,
            this.LongDescriptionModifiedTarget,
            this.DescriptionModifiedTarget});
            this.TargetUpdateLangView.Location = new System.Drawing.Point(916, 33);
            this.TargetUpdateLangView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.TargetUpdateLangView.Name = "TargetUpdateLangView";
            this.TargetUpdateLangView.RowTemplate.Height = 24;
            this.TargetUpdateLangView.Size = new System.Drawing.Size(697, 658);
            this.TargetUpdateLangView.TabIndex = 9;
            this.TargetUpdateLangView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.TargetUpdateLangView_RowsAdded);
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
            this.dataGridViewTextBoxColumn36.Width = 280;
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
            // LongDescriptionModifiedTarget
            // 
            this.LongDescriptionModifiedTarget.DataPropertyName = "LongDescriptionModified";
            this.LongDescriptionModifiedTarget.HeaderText = "LongDescriptionModifiedTarget";
            this.LongDescriptionModifiedTarget.Name = "LongDescriptionModifiedTarget";
            this.LongDescriptionModifiedTarget.Visible = false;
            // 
            // DescriptionModifiedTarget
            // 
            this.DescriptionModifiedTarget.DataPropertyName = "DescriptionModified";
            this.DescriptionModifiedTarget.HeaderText = "DescriptionModifiedTarget";
            this.DescriptionModifiedTarget.Name = "DescriptionModifiedTarget";
            this.DescriptionModifiedTarget.Visible = false;
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
            this.SourceUpdateLangView.Location = new System.Drawing.Point(3, 33);
            this.SourceUpdateLangView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.SourceUpdateLangView.Name = "SourceUpdateLangView";
            this.SourceUpdateLangView.RowTemplate.Height = 24;
            this.SourceUpdateLangView.Size = new System.Drawing.Size(921, 658);
            this.SourceUpdateLangView.TabIndex = 8;
            this.SourceUpdateLangView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.SourceUpdateLangView_RowsAdded);
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
            this.dataGridViewTextBoxColumn25.Width = 220;
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
            this.Attributes.Location = new System.Drawing.Point(4, 29);
            this.Attributes.Name = "Attributes";
            this.Attributes.Padding = new System.Windows.Forms.Padding(3);
            this.Attributes.Size = new System.Drawing.Size(1668, 837);
            this.Attributes.TabIndex = 3;
            this.Attributes.Text = "Attributes";
            this.Attributes.UseVisualStyleBackColor = true;
            // 
            // btnUpdateAttribute
            // 
            this.btnUpdateAttribute.Location = new System.Drawing.Point(8, 789);
            this.btnUpdateAttribute.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnUpdateAttribute.Name = "btnUpdateAttribute";
            this.btnUpdateAttribute.Size = new System.Drawing.Size(94, 35);
            this.btnUpdateAttribute.TabIndex = 17;
            this.btnUpdateAttribute.Text = "Include";
            this.btnUpdateAttribute.UseVisualStyleBackColor = true;
            this.btnUpdateAttribute.Click += new System.EventHandler(this.BtnUpdateAttribute_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Location = new System.Drawing.Point(8, 54);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1653, 729);
            this.tabControl2.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblTypeDataListItem);
            this.tabPage1.Controls.Add(this.lblTypeDataList);
            this.tabPage1.Controls.Add(this.NewAttributesView);
            this.tabPage1.Controls.Add(this.NewItemsAttributeSelectAllCB);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage1.Size = new System.Drawing.Size(1645, 696);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "New";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblTypeDataListItem
            // 
            this.lblTypeDataListItem.AutoSize = true;
            this.lblTypeDataListItem.BackColor = System.Drawing.Color.LightGray;
            this.lblTypeDataListItem.Location = new System.Drawing.Point(314, 12);
            this.lblTypeDataListItem.Name = "lblTypeDataListItem";
            this.lblTypeDataListItem.Size = new System.Drawing.Size(178, 20);
            this.lblTypeDataListItem.TabIndex = 15;
            this.lblTypeDataListItem.Text = "ExistingTypeDataList";
            // 
            // lblTypeDataList
            // 
            this.lblTypeDataList.AutoSize = true;
            this.lblTypeDataList.BackColor = System.Drawing.Color.LightBlue;
            this.lblTypeDataList.Location = new System.Drawing.Point(144, 12);
            this.lblTypeDataList.Name = "lblTypeDataList";
            this.lblTypeDataList.Size = new System.Drawing.Size(149, 20);
            this.lblTypeDataList.TabIndex = 14;
            this.lblTypeDataList.Text = "NewTypeDataList";
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
            this.TypeNameAttribute,
            this.Column5,
            this.IsEditableAttribute});
            this.NewAttributesView.Location = new System.Drawing.Point(8, 40);
            this.NewAttributesView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewAttributesView.Name = "NewAttributesView";
            this.NewAttributesView.RowTemplate.Height = 24;
            this.NewAttributesView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NewAttributesView.Size = new System.Drawing.Size(1630, 645);
            this.NewAttributesView.TabIndex = 13;
            this.NewAttributesView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.NewAttributeView_RowsAdded);
            // 
            // NewItemsAttributeSelectAllCB
            // 
            this.NewItemsAttributeSelectAllCB.AutoSize = true;
            this.NewItemsAttributeSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewItemsAttributeSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewItemsAttributeSelectAllCB.Location = new System.Drawing.Point(8, 8);
            this.NewItemsAttributeSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewItemsAttributeSelectAllCB.Name = "NewItemsAttributeSelectAllCB";
            this.NewItemsAttributeSelectAllCB.Size = new System.Drawing.Size(111, 24);
            this.NewItemsAttributeSelectAllCB.TabIndex = 12;
            this.NewItemsAttributeSelectAllCB.Text = "Select All";
            this.NewItemsAttributeSelectAllCB.UseVisualStyleBackColor = true;
            this.NewItemsAttributeSelectAllCB.CheckedChanged += new System.EventHandler(this.NewItemsAttributeSelectAllCB_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.TargetUpdateAttributeView);
            this.tabPage2.Controls.Add(this.checkBox10);
            this.tabPage2.Controls.Add(this.SourceUpdateAttributeView);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage2.Size = new System.Drawing.Size(1645, 696);
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
            this.TargetUpdateAttributeView.Location = new System.Drawing.Point(788, 35);
            this.TargetUpdateAttributeView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.dataGridViewTextBoxColumn39.Width = 150;
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
            this.dataGridViewTextBoxColumn43.Width = 80;
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
            this.checkBox10.Location = new System.Drawing.Point(8, 8);
            this.checkBox10.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.SourceUpdateAttributeView.Location = new System.Drawing.Point(8, 35);
            this.SourceUpdateAttributeView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.dataGridViewTextBoxColumn51.Width = 150;
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
            this.dataGridViewTextBoxColumn48.Width = 80;
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
            // tabpage3
            // 
            this.tabpage3.Controls.Add(this.btnLinkitem);
            this.tabpage3.Controls.Add(this.tabControl3);
            this.tabpage3.Controls.Add(this.panel4);
            this.tabpage3.Location = new System.Drawing.Point(4, 29);
            this.tabpage3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabpage3.Name = "tabpage3";
            this.tabpage3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabpage3.Size = new System.Drawing.Size(1668, 837);
            this.tabpage3.TabIndex = 4;
            this.tabpage3.Text = "Links";
            this.tabpage3.UseVisualStyleBackColor = true;
            // 
            // btnLinkitem
            // 
            this.btnLinkitem.Location = new System.Drawing.Point(8, 790);
            this.btnLinkitem.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnLinkitem.Name = "btnLinkitem";
            this.btnLinkitem.Size = new System.Drawing.Size(109, 32);
            this.btnLinkitem.TabIndex = 18;
            this.btnLinkitem.Text = "Include";
            this.btnLinkitem.UseVisualStyleBackColor = true;
            this.btnLinkitem.Click += new System.EventHandler(this.BtnLinkitem_Click);
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage4);
            this.tabControl3.Controls.Add(this.tabPage5);
            this.tabControl3.Location = new System.Drawing.Point(4, 51);
            this.tabControl3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(1653, 729);
            this.tabControl3.TabIndex = 17;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.LinkgridView);
            this.tabPage4.Controls.Add(this.NewLinkView);
            this.tabPage4.Controls.Add(this.NewLinkSelectAllCB);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage4.Size = new System.Drawing.Size(1645, 696);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "New";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // LinkgridView
            // 
            this.LinkgridView.AllowUserToAddRows = false;
            this.LinkgridView.AllowUserToDeleteRows = false;
            this.LinkgridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LinkgridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn16,
            this.dataGridViewTextBoxColumn54,
            this.dataGridViewTextBoxColumn55,
            this.dataGridViewTextBoxColumn56,
            this.dataGridViewTextBoxColumn57,
            this.dataGridViewTextBoxColumn58,
            this.dataGridViewTextBoxColumn60,
            this.dataGridViewTextBoxColumn61,
            this.dataGridViewTextBoxColumn62,
            this.dataGridViewTextBoxColumn63,
            this.dataGridViewTextBoxColumn64,
            this.dataGridViewTextBoxColumn65,
            this.dataGridViewTextBoxColumn105,
            this.dataGridViewTextBoxColumn106,
            this.dataGridViewTextBoxColumn107});
            this.LinkgridView.Location = new System.Drawing.Point(0, 54);
            this.LinkgridView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.LinkgridView.Name = "LinkgridView";
            this.LinkgridView.RowTemplate.Height = 24;
            this.LinkgridView.Size = new System.Drawing.Size(1645, 646);
            this.LinkgridView.TabIndex = 15;
            // 
            // NewLinkView
            // 
            this.NewLinkView.AllowUserToAddRows = false;
            this.NewLinkView.AllowUserToDeleteRows = false;
            this.NewLinkView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NewLinkView.Location = new System.Drawing.Point(-10, 54);
            this.NewLinkView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewLinkView.Name = "NewLinkView";
            this.NewLinkView.RowTemplate.Height = 24;
            this.NewLinkView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NewLinkView.Size = new System.Drawing.Size(1652, 642);
            this.NewLinkView.TabIndex = 13;
            this.NewLinkView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            // 
            // NewLinkSelectAllCB
            // 
            this.NewLinkSelectAllCB.AutoSize = true;
            this.NewLinkSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewLinkSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewLinkSelectAllCB.Location = new System.Drawing.Point(8, 8);
            this.NewLinkSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewLinkSelectAllCB.Name = "NewLinkSelectAllCB";
            this.NewLinkSelectAllCB.Size = new System.Drawing.Size(111, 24);
            this.NewLinkSelectAllCB.TabIndex = 12;
            this.NewLinkSelectAllCB.Text = "Select All";
            this.NewLinkSelectAllCB.UseVisualStyleBackColor = true;
            this.NewLinkSelectAllCB.CheckedChanged += new System.EventHandler(this.NewLinkSelectAllCB_CheckedChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.TargetLinkView);
            this.tabPage5.Controls.Add(this.SourceLinkView);
            this.tabPage5.Controls.Add(this.dataGridView2);
            this.tabPage5.Controls.Add(this.checkBox7);
            this.tabPage5.Controls.Add(this.dataGridView3);
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage5.Size = new System.Drawing.Size(1645, 696);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Update";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // TargetLinkView
            // 
            this.TargetLinkView.AllowUserToAddRows = false;
            this.TargetLinkView.AllowUserToDeleteRows = false;
            this.TargetLinkView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TargetLinkView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn20,
            this.dataGridViewTextBoxColumn91,
            this.dataGridViewTextBoxColumn92,
            this.dataGridViewTextBoxColumn93,
            this.dataGridViewTextBoxColumn94,
            this.dataGridViewTextBoxColumn95,
            this.dataGridViewTextBoxColumn96,
            this.dataGridViewTextBoxColumn97,
            this.dataGridViewTextBoxColumn98,
            this.dataGridViewTextBoxColumn99,
            this.dataGridViewTextBoxColumn100,
            this.dataGridViewTextBoxColumn101,
            this.dataGridViewTextBoxColumn102,
            this.dataGridViewTextBoxColumn103,
            this.dataGridViewTextBoxColumn104});
            this.TargetLinkView.Location = new System.Drawing.Point(722, 38);
            this.TargetLinkView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.TargetLinkView.Name = "TargetLinkView";
            this.TargetLinkView.RowTemplate.Height = 24;
            this.TargetLinkView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TargetLinkView.Size = new System.Drawing.Size(968, 642);
            this.TargetLinkView.TabIndex = 15;
            this.TargetLinkView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TargetLinkView_CellContentClick);
            // 
            // dataGridViewCheckBoxColumn20
            // 
            this.dataGridViewCheckBoxColumn20.HeaderText = "";
            this.dataGridViewCheckBoxColumn20.Name = "dataGridViewCheckBoxColumn20";
            this.dataGridViewCheckBoxColumn20.Width = 25;
            // 
            // dataGridViewTextBoxColumn91
            // 
            this.dataGridViewTextBoxColumn91.DataPropertyName = "ParentDataList";
            this.dataGridViewTextBoxColumn91.HeaderText = "ParentDataList";
            this.dataGridViewTextBoxColumn91.Name = "dataGridViewTextBoxColumn91";
            // 
            // dataGridViewTextBoxColumn92
            // 
            this.dataGridViewTextBoxColumn92.DataPropertyName = "ParentCode";
            this.dataGridViewTextBoxColumn92.HeaderText = "ParentCode";
            this.dataGridViewTextBoxColumn92.Name = "dataGridViewTextBoxColumn92";
            // 
            // dataGridViewTextBoxColumn93
            // 
            this.dataGridViewTextBoxColumn93.DataPropertyName = "ChildDataList";
            this.dataGridViewTextBoxColumn93.HeaderText = "ChildDataList";
            this.dataGridViewTextBoxColumn93.Name = "dataGridViewTextBoxColumn93";
            // 
            // dataGridViewTextBoxColumn94
            // 
            this.dataGridViewTextBoxColumn94.DataPropertyName = "A";
            this.dataGridViewTextBoxColumn94.HeaderText = "A";
            this.dataGridViewTextBoxColumn94.Name = "dataGridViewTextBoxColumn94";
            this.dataGridViewTextBoxColumn94.Visible = false;
            // 
            // dataGridViewTextBoxColumn95
            // 
            this.dataGridViewTextBoxColumn95.DataPropertyName = "DefaultTypeID";
            this.dataGridViewTextBoxColumn95.HeaderText = "DefaultTypeID";
            this.dataGridViewTextBoxColumn95.Name = "dataGridViewTextBoxColumn95";
            this.dataGridViewTextBoxColumn95.Visible = false;
            // 
            // dataGridViewTextBoxColumn96
            // 
            this.dataGridViewTextBoxColumn96.DataPropertyName = "ID";
            this.dataGridViewTextBoxColumn96.HeaderText = "ID";
            this.dataGridViewTextBoxColumn96.Name = "dataGridViewTextBoxColumn96";
            this.dataGridViewTextBoxColumn96.Visible = false;
            // 
            // dataGridViewTextBoxColumn97
            // 
            this.dataGridViewTextBoxColumn97.DataPropertyName = "LastModifiedDate";
            this.dataGridViewTextBoxColumn97.HeaderText = "LastModifiedDate";
            this.dataGridViewTextBoxColumn97.Name = "dataGridViewTextBoxColumn97";
            this.dataGridViewTextBoxColumn97.Visible = false;
            // 
            // dataGridViewTextBoxColumn98
            // 
            this.dataGridViewTextBoxColumn98.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn98.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn98.Name = "dataGridViewTextBoxColumn98";
            this.dataGridViewTextBoxColumn98.ReadOnly = true;
            this.dataGridViewTextBoxColumn98.Visible = false;
            this.dataGridViewTextBoxColumn98.Width = 400;
            // 
            // dataGridViewTextBoxColumn99
            // 
            this.dataGridViewTextBoxColumn99.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn99.HeaderText = "Descripation";
            this.dataGridViewTextBoxColumn99.Name = "dataGridViewTextBoxColumn99";
            this.dataGridViewTextBoxColumn99.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn100
            // 
            this.dataGridViewTextBoxColumn100.DataPropertyName = "IsActive";
            this.dataGridViewTextBoxColumn100.HeaderText = "IsActive";
            this.dataGridViewTextBoxColumn100.Name = "dataGridViewTextBoxColumn100";
            this.dataGridViewTextBoxColumn100.ReadOnly = true;
            this.dataGridViewTextBoxColumn100.Visible = false;
            this.dataGridViewTextBoxColumn100.Width = 250;
            // 
            // dataGridViewTextBoxColumn101
            // 
            this.dataGridViewTextBoxColumn101.DataPropertyName = "DataListID";
            this.dataGridViewTextBoxColumn101.HeaderText = "DataListID";
            this.dataGridViewTextBoxColumn101.Name = "dataGridViewTextBoxColumn101";
            this.dataGridViewTextBoxColumn101.ReadOnly = true;
            this.dataGridViewTextBoxColumn101.Visible = false;
            this.dataGridViewTextBoxColumn101.Width = 370;
            // 
            // dataGridViewTextBoxColumn102
            // 
            this.dataGridViewTextBoxColumn102.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn102.HeaderText = "Status";
            this.dataGridViewTextBoxColumn102.Name = "dataGridViewTextBoxColumn102";
            this.dataGridViewTextBoxColumn102.ReadOnly = true;
            this.dataGridViewTextBoxColumn102.Visible = false;
            this.dataGridViewTextBoxColumn102.Width = 60;
            // 
            // dataGridViewTextBoxColumn103
            // 
            this.dataGridViewTextBoxColumn103.DataPropertyName = "DefaultTypeValue";
            this.dataGridViewTextBoxColumn103.HeaderText = "DefaultTypeValue";
            this.dataGridViewTextBoxColumn103.Name = "dataGridViewTextBoxColumn103";
            this.dataGridViewTextBoxColumn103.Visible = false;
            // 
            // dataGridViewTextBoxColumn104
            // 
            this.dataGridViewTextBoxColumn104.DataPropertyName = "TenantID";
            this.dataGridViewTextBoxColumn104.HeaderText = "TenantID";
            this.dataGridViewTextBoxColumn104.Name = "dataGridViewTextBoxColumn104";
            this.dataGridViewTextBoxColumn104.Visible = false;
            // 
            // SourceLinkView
            // 
            this.SourceLinkView.AllowUserToAddRows = false;
            this.SourceLinkView.AllowUserToDeleteRows = false;
            this.SourceLinkView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SourceLinkView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn19,
            this.dataGridViewTextBoxColumn59,
            this.dataGridViewTextBoxColumn78,
            this.dataGridViewTextBoxColumn79,
            this.dataGridViewTextBoxColumn80,
            this.dataGridViewTextBoxColumn81,
            this.dataGridViewTextBoxColumn82,
            this.dataGridViewTextBoxColumn83,
            this.dataGridViewTextBoxColumn84,
            this.dataGridViewTextBoxColumn85,
            this.dataGridViewTextBoxColumn86,
            this.dataGridViewTextBoxColumn87,
            this.dataGridViewTextBoxColumn88,
            this.dataGridViewTextBoxColumn89,
            this.dataGridViewTextBoxColumn90});
            this.SourceLinkView.Location = new System.Drawing.Point(-16, 35);
            this.SourceLinkView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.SourceLinkView.Name = "SourceLinkView";
            this.SourceLinkView.RowTemplate.Height = 24;
            this.SourceLinkView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SourceLinkView.Size = new System.Drawing.Size(1725, 642);
            this.SourceLinkView.TabIndex = 14;
            // 
            // dataGridViewCheckBoxColumn19
            // 
            this.dataGridViewCheckBoxColumn19.HeaderText = "";
            this.dataGridViewCheckBoxColumn19.Name = "dataGridViewCheckBoxColumn19";
            this.dataGridViewCheckBoxColumn19.Width = 25;
            // 
            // dataGridViewTextBoxColumn59
            // 
            this.dataGridViewTextBoxColumn59.DataPropertyName = "ParentDataList";
            this.dataGridViewTextBoxColumn59.HeaderText = "ParentDataList";
            this.dataGridViewTextBoxColumn59.Name = "dataGridViewTextBoxColumn59";
            // 
            // dataGridViewTextBoxColumn78
            // 
            this.dataGridViewTextBoxColumn78.DataPropertyName = "ParentCode";
            this.dataGridViewTextBoxColumn78.HeaderText = "ParentCode";
            this.dataGridViewTextBoxColumn78.Name = "dataGridViewTextBoxColumn78";
            // 
            // dataGridViewTextBoxColumn79
            // 
            this.dataGridViewTextBoxColumn79.DataPropertyName = "ChildDataList";
            this.dataGridViewTextBoxColumn79.HeaderText = "ChildDataList";
            this.dataGridViewTextBoxColumn79.Name = "dataGridViewTextBoxColumn79";
            // 
            // dataGridViewTextBoxColumn80
            // 
            this.dataGridViewTextBoxColumn80.DataPropertyName = "A";
            this.dataGridViewTextBoxColumn80.HeaderText = "A";
            this.dataGridViewTextBoxColumn80.Name = "dataGridViewTextBoxColumn80";
            this.dataGridViewTextBoxColumn80.Visible = false;
            // 
            // dataGridViewTextBoxColumn81
            // 
            this.dataGridViewTextBoxColumn81.DataPropertyName = "DefaultTypeID";
            this.dataGridViewTextBoxColumn81.HeaderText = "DefaultTypeID";
            this.dataGridViewTextBoxColumn81.Name = "dataGridViewTextBoxColumn81";
            this.dataGridViewTextBoxColumn81.Visible = false;
            // 
            // dataGridViewTextBoxColumn82
            // 
            this.dataGridViewTextBoxColumn82.DataPropertyName = "ID";
            this.dataGridViewTextBoxColumn82.HeaderText = "ID";
            this.dataGridViewTextBoxColumn82.Name = "dataGridViewTextBoxColumn82";
            this.dataGridViewTextBoxColumn82.Visible = false;
            // 
            // dataGridViewTextBoxColumn83
            // 
            this.dataGridViewTextBoxColumn83.DataPropertyName = "LastModifiedDate";
            this.dataGridViewTextBoxColumn83.HeaderText = "LastModifiedDate";
            this.dataGridViewTextBoxColumn83.Name = "dataGridViewTextBoxColumn83";
            this.dataGridViewTextBoxColumn83.Visible = false;
            // 
            // dataGridViewTextBoxColumn84
            // 
            this.dataGridViewTextBoxColumn84.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn84.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn84.Name = "dataGridViewTextBoxColumn84";
            this.dataGridViewTextBoxColumn84.ReadOnly = true;
            this.dataGridViewTextBoxColumn84.Visible = false;
            this.dataGridViewTextBoxColumn84.Width = 400;
            // 
            // dataGridViewTextBoxColumn85
            // 
            this.dataGridViewTextBoxColumn85.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn85.HeaderText = "Descripation";
            this.dataGridViewTextBoxColumn85.Name = "dataGridViewTextBoxColumn85";
            this.dataGridViewTextBoxColumn85.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn86
            // 
            this.dataGridViewTextBoxColumn86.DataPropertyName = "IsActive";
            this.dataGridViewTextBoxColumn86.HeaderText = "IsActive";
            this.dataGridViewTextBoxColumn86.Name = "dataGridViewTextBoxColumn86";
            this.dataGridViewTextBoxColumn86.ReadOnly = true;
            this.dataGridViewTextBoxColumn86.Visible = false;
            this.dataGridViewTextBoxColumn86.Width = 250;
            // 
            // dataGridViewTextBoxColumn87
            // 
            this.dataGridViewTextBoxColumn87.DataPropertyName = "DataListID";
            this.dataGridViewTextBoxColumn87.HeaderText = "DataListID";
            this.dataGridViewTextBoxColumn87.Name = "dataGridViewTextBoxColumn87";
            this.dataGridViewTextBoxColumn87.ReadOnly = true;
            this.dataGridViewTextBoxColumn87.Visible = false;
            this.dataGridViewTextBoxColumn87.Width = 370;
            // 
            // dataGridViewTextBoxColumn88
            // 
            this.dataGridViewTextBoxColumn88.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn88.HeaderText = "Status";
            this.dataGridViewTextBoxColumn88.Name = "dataGridViewTextBoxColumn88";
            this.dataGridViewTextBoxColumn88.ReadOnly = true;
            this.dataGridViewTextBoxColumn88.Visible = false;
            this.dataGridViewTextBoxColumn88.Width = 60;
            // 
            // dataGridViewTextBoxColumn89
            // 
            this.dataGridViewTextBoxColumn89.DataPropertyName = "DefaultTypeValue";
            this.dataGridViewTextBoxColumn89.HeaderText = "DefaultTypeValue";
            this.dataGridViewTextBoxColumn89.Name = "dataGridViewTextBoxColumn89";
            this.dataGridViewTextBoxColumn89.Visible = false;
            // 
            // dataGridViewTextBoxColumn90
            // 
            this.dataGridViewTextBoxColumn90.DataPropertyName = "TenantID";
            this.dataGridViewTextBoxColumn90.HeaderText = "TenantID";
            this.dataGridViewTextBoxColumn90.Name = "dataGridViewTextBoxColumn90";
            this.dataGridViewTextBoxColumn90.Visible = false;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn17,
            this.dataGridViewTextBoxColumn66,
            this.dataGridViewTextBoxColumn67,
            this.dataGridViewTextBoxColumn68,
            this.dataGridViewTextBoxColumn69,
            this.dataGridViewTextBoxColumn70,
            this.dataGridViewTextBoxColumn71});
            this.dataGridView2.Location = new System.Drawing.Point(788, 35);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView2.Size = new System.Drawing.Size(836, 658);
            this.dataGridView2.TabIndex = 13;
            // 
            // dataGridViewCheckBoxColumn17
            // 
            this.dataGridViewCheckBoxColumn17.HeaderText = "";
            this.dataGridViewCheckBoxColumn17.Name = "dataGridViewCheckBoxColumn17";
            this.dataGridViewCheckBoxColumn17.Width = 25;
            // 
            // dataGridViewTextBoxColumn66
            // 
            this.dataGridViewTextBoxColumn66.DataPropertyName = "ParentContentID";
            this.dataGridViewTextBoxColumn66.HeaderText = "ParentContentID";
            this.dataGridViewTextBoxColumn66.Name = "dataGridViewTextBoxColumn66";
            // 
            // dataGridViewTextBoxColumn67
            // 
            this.dataGridViewTextBoxColumn67.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn67.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn67.Name = "dataGridViewTextBoxColumn67";
            this.dataGridViewTextBoxColumn67.ReadOnly = true;
            this.dataGridViewTextBoxColumn67.Width = 250;
            // 
            // dataGridViewTextBoxColumn68
            // 
            this.dataGridViewTextBoxColumn68.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn68.HeaderText = "Code";
            this.dataGridViewTextBoxColumn68.Name = "dataGridViewTextBoxColumn68";
            this.dataGridViewTextBoxColumn68.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn69
            // 
            this.dataGridViewTextBoxColumn69.DataPropertyName = "IsActive";
            this.dataGridViewTextBoxColumn69.HeaderText = "IsActive";
            this.dataGridViewTextBoxColumn69.Name = "dataGridViewTextBoxColumn69";
            this.dataGridViewTextBoxColumn69.ReadOnly = true;
            this.dataGridViewTextBoxColumn69.Width = 60;
            // 
            // dataGridViewTextBoxColumn70
            // 
            this.dataGridViewTextBoxColumn70.DataPropertyName = "LastModifiedDate";
            this.dataGridViewTextBoxColumn70.HeaderText = "LastModifiedDate";
            this.dataGridViewTextBoxColumn70.Name = "dataGridViewTextBoxColumn70";
            this.dataGridViewTextBoxColumn70.ReadOnly = true;
            this.dataGridViewTextBoxColumn70.Visible = false;
            this.dataGridViewTextBoxColumn70.Width = 215;
            // 
            // dataGridViewTextBoxColumn71
            // 
            this.dataGridViewTextBoxColumn71.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn71.HeaderText = "Status";
            this.dataGridViewTextBoxColumn71.Name = "dataGridViewTextBoxColumn71";
            this.dataGridViewTextBoxColumn71.ReadOnly = true;
            this.dataGridViewTextBoxColumn71.Visible = false;
            this.dataGridViewTextBoxColumn71.Width = 60;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox7.Location = new System.Drawing.Point(8, 8);
            this.checkBox7.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(111, 24);
            this.checkBox7.TabIndex = 12;
            this.checkBox7.Text = "Select All";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn18,
            this.dataGridViewTextBoxColumn72,
            this.dataGridViewTextBoxColumn73,
            this.dataGridViewTextBoxColumn74,
            this.dataGridViewTextBoxColumn75,
            this.dataGridViewTextBoxColumn76,
            this.dataGridViewTextBoxColumn77});
            this.dataGridView3.Location = new System.Drawing.Point(8, 35);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView3.Size = new System.Drawing.Size(836, 658);
            this.dataGridView3.TabIndex = 8;
            // 
            // dataGridViewCheckBoxColumn18
            // 
            this.dataGridViewCheckBoxColumn18.HeaderText = "";
            this.dataGridViewCheckBoxColumn18.Name = "dataGridViewCheckBoxColumn18";
            this.dataGridViewCheckBoxColumn18.Width = 25;
            // 
            // dataGridViewTextBoxColumn72
            // 
            this.dataGridViewTextBoxColumn72.DataPropertyName = "ParentContentID";
            this.dataGridViewTextBoxColumn72.HeaderText = "ParentContentID";
            this.dataGridViewTextBoxColumn72.Name = "dataGridViewTextBoxColumn72";
            // 
            // dataGridViewTextBoxColumn73
            // 
            this.dataGridViewTextBoxColumn73.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn73.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn73.Name = "dataGridViewTextBoxColumn73";
            this.dataGridViewTextBoxColumn73.ReadOnly = true;
            this.dataGridViewTextBoxColumn73.Width = 250;
            // 
            // dataGridViewTextBoxColumn74
            // 
            this.dataGridViewTextBoxColumn74.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn74.HeaderText = "Code";
            this.dataGridViewTextBoxColumn74.Name = "dataGridViewTextBoxColumn74";
            this.dataGridViewTextBoxColumn74.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn75
            // 
            this.dataGridViewTextBoxColumn75.DataPropertyName = "IsActive";
            this.dataGridViewTextBoxColumn75.HeaderText = "IsActive";
            this.dataGridViewTextBoxColumn75.Name = "dataGridViewTextBoxColumn75";
            this.dataGridViewTextBoxColumn75.ReadOnly = true;
            this.dataGridViewTextBoxColumn75.Width = 60;
            // 
            // dataGridViewTextBoxColumn76
            // 
            this.dataGridViewTextBoxColumn76.DataPropertyName = "LastModifiedDate";
            this.dataGridViewTextBoxColumn76.HeaderText = "LastModifiedDate";
            this.dataGridViewTextBoxColumn76.Name = "dataGridViewTextBoxColumn76";
            this.dataGridViewTextBoxColumn76.ReadOnly = true;
            this.dataGridViewTextBoxColumn76.Visible = false;
            this.dataGridViewTextBoxColumn76.Width = 215;
            // 
            // dataGridViewTextBoxColumn77
            // 
            this.dataGridViewTextBoxColumn77.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn77.HeaderText = "Status";
            this.dataGridViewTextBoxColumn77.Name = "dataGridViewTextBoxColumn77";
            this.dataGridViewTextBoxColumn77.ReadOnly = true;
            this.dataGridViewTextBoxColumn77.Visible = false;
            this.dataGridViewTextBoxColumn77.Width = 60;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Controls.Add(this.checkBox1);
            this.panel4.Controls.Add(this.checkBox2);
            this.panel4.Controls.Add(this.checkBox3);
            this.panel4.Controls.Add(this.checkBox4);
            this.panel4.Controls.Add(this.checkBox5);
            this.panel4.Location = new System.Drawing.Point(3, 5);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(585, 47);
            this.panel4.TabIndex = 16;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(452, 14);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(97, 24);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "Datalist";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2.Location = new System.Drawing.Point(316, 14);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(116, 24);
            this.checkBox2.TabIndex = 13;
            this.checkBox2.Text = "Messages";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox3.Location = new System.Drawing.Point(214, 14);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(87, 24);
            this.checkBox3.TabIndex = 11;
            this.checkBox3.Text = "Rights";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox4.Location = new System.Drawing.Point(93, 14);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(114, 24);
            this.checkBox4.TabIndex = 10;
            this.checkBox4.Text = "Functions";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox5.Location = new System.Drawing.Point(0, 14);
            this.checkBox5.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(81, 24);
            this.checkBox5.TabIndex = 9;
            this.checkBox5.Text = "Roles";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // ItemAttribute
            // 
            this.ItemAttribute.Controls.Add(this.btnItemAttribute);
            this.ItemAttribute.Controls.Add(this.tabControl4);
            this.ItemAttribute.Location = new System.Drawing.Point(4, 29);
            this.ItemAttribute.Name = "ItemAttribute";
            this.ItemAttribute.Padding = new System.Windows.Forms.Padding(3);
            this.ItemAttribute.Size = new System.Drawing.Size(1668, 837);
            this.ItemAttribute.TabIndex = 5;
            this.ItemAttribute.Text = "ItemAttribute";
            this.ItemAttribute.UseVisualStyleBackColor = true;
            // 
            // btnItemAttribute
            // 
            this.btnItemAttribute.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemAttribute.Location = new System.Drawing.Point(6, 793);
            this.btnItemAttribute.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnItemAttribute.Name = "btnItemAttribute";
            this.btnItemAttribute.Size = new System.Drawing.Size(110, 40);
            this.btnItemAttribute.TabIndex = 16;
            this.btnItemAttribute.Text = "Include";
            this.btnItemAttribute.UseVisualStyleBackColor = true;
            this.btnItemAttribute.Click += new System.EventHandler(this.BtnItemAttribute_Click);
            // 
            // tabControl4
            // 
            this.tabControl4.Controls.Add(this.tabPage7);
            this.tabControl4.Controls.Add(this.tabPage8);
            this.tabControl4.Location = new System.Drawing.Point(4, 49);
            this.tabControl4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabControl4.Name = "tabControl4";
            this.tabControl4.SelectedIndex = 0;
            this.tabControl4.Size = new System.Drawing.Size(1658, 738);
            this.tabControl4.TabIndex = 15;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.NewItemAttrExitingItemsCB);
            this.tabPage7.Controls.Add(this.NewItemAtrrNewItemsCB);
            this.tabPage7.Controls.Add(this.NewItemAttrNewDataListCB);
            this.tabPage7.Controls.Add(this.NewItemAttributeView);
            this.tabPage7.Controls.Add(this.NewItemsAttrSelectAllCB);
            this.tabPage7.Location = new System.Drawing.Point(4, 29);
            this.tabPage7.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage7.Size = new System.Drawing.Size(1650, 705);
            this.tabPage7.TabIndex = 0;
            this.tabPage7.Text = "New Items";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // NewItemAttrExitingItemsCB
            // 
            this.NewItemAttrExitingItemsCB.AutoSize = true;
            this.NewItemAttrExitingItemsCB.BackColor = System.Drawing.Color.Silver;
            this.NewItemAttrExitingItemsCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewItemAttrExitingItemsCB.Location = new System.Drawing.Point(844, 9);
            this.NewItemAttrExitingItemsCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewItemAttrExitingItemsCB.Name = "NewItemAttrExitingItemsCB";
            this.NewItemAttrExitingItemsCB.Size = new System.Drawing.Size(318, 24);
            this.NewItemAttrExitingItemsCB.TabIndex = 28;
            this.NewItemAttrExitingItemsCB.Text = "New ItemAttributes - Existing Items";
            this.NewItemAttrExitingItemsCB.UseVisualStyleBackColor = false;
            this.NewItemAttrExitingItemsCB.CheckedChanged += new System.EventHandler(this.NewItemAttrExitingItemsCB_CheckedChanged);
            // 
            // NewItemAtrrNewItemsCB
            // 
            this.NewItemAtrrNewItemsCB.AutoSize = true;
            this.NewItemAtrrNewItemsCB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.NewItemAtrrNewItemsCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewItemAtrrNewItemsCB.Location = new System.Drawing.Point(490, 9);
            this.NewItemAtrrNewItemsCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewItemAtrrNewItemsCB.Name = "NewItemAtrrNewItemsCB";
            this.NewItemAtrrNewItemsCB.Size = new System.Drawing.Size(335, 24);
            this.NewItemAtrrNewItemsCB.TabIndex = 19;
            this.NewItemAtrrNewItemsCB.Text = "New ItemAttributes - Existing Datalist";
            this.NewItemAtrrNewItemsCB.UseVisualStyleBackColor = false;
            this.NewItemAtrrNewItemsCB.CheckedChanged += new System.EventHandler(this.NewItemAtrrNewItemsCB_CheckedChanged);
            // 
            // NewItemAttrNewDataListCB
            // 
            this.NewItemAttrNewDataListCB.AutoSize = true;
            this.NewItemAttrNewDataListCB.BackColor = System.Drawing.Color.LightBlue;
            this.NewItemAttrNewDataListCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewItemAttrNewDataListCB.Location = new System.Drawing.Point(170, 8);
            this.NewItemAttrNewDataListCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewItemAttrNewDataListCB.Name = "NewItemAttrNewDataListCB";
            this.NewItemAttrNewDataListCB.Size = new System.Drawing.Size(315, 24);
            this.NewItemAttrNewDataListCB.TabIndex = 18;
            this.NewItemAttrNewDataListCB.Text = "New ItemAttributess - New Datalist";
            this.NewItemAttrNewDataListCB.UseVisualStyleBackColor = false;
            this.NewItemAttrNewDataListCB.CheckedChanged += new System.EventHandler(this.NewItemAttrNewDataListCB_CheckedChanged);
            // 
            // NewItemAttributeView
            // 
            this.NewItemAttributeView.AllowUserToAddRows = false;
            this.NewItemAttributeView.AllowUserToDeleteRows = false;
            this.NewItemAttributeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NewItemAttributeView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn21,
            this.DataListTypeNameItemAtrr,
            this.dataGridViewTextBoxColumn109,
            this.DataListAttributeNameItem,
            this.DataListAtrributeValue,
            this.DataListItemIDItemAttribute,
            this.DataListTypeIDItemAttr,
            this.DataListValueIDItemAttr,
            this.IDItenAtrr,
            this.LastModifiedDateItemAttr,
            this.StatusItemAttr,
            this.DataListAttributeID,
            this.DataListTypeNameItemAttr,
            this.IsEditableItem});
            this.NewItemAttributeView.Location = new System.Drawing.Point(18, 42);
            this.NewItemAttributeView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewItemAttributeView.Name = "NewItemAttributeView";
            this.NewItemAttributeView.RowTemplate.Height = 24;
            this.NewItemAttributeView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NewItemAttributeView.Size = new System.Drawing.Size(1286, 652);
            this.NewItemAttributeView.TabIndex = 6;
            this.NewItemAttributeView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.NewItemAttributeView_RowsAdded);
            // 
            // dataGridViewCheckBoxColumn21
            // 
            this.dataGridViewCheckBoxColumn21.HeaderText = "";
            this.dataGridViewCheckBoxColumn21.Name = "dataGridViewCheckBoxColumn21";
            this.dataGridViewCheckBoxColumn21.Width = 20;
            // 
            // DataListTypeNameItemAtrr
            // 
            this.DataListTypeNameItemAtrr.DataPropertyName = "ParentContentID";
            this.DataListTypeNameItemAtrr.HeaderText = "DataList";
            this.DataListTypeNameItemAtrr.Name = "DataListTypeNameItemAtrr";
            // 
            // dataGridViewTextBoxColumn109
            // 
            this.dataGridViewTextBoxColumn109.DataPropertyName = "ItemCode";
            this.dataGridViewTextBoxColumn109.HeaderText = "ItemCode";
            this.dataGridViewTextBoxColumn109.Name = "dataGridViewTextBoxColumn109";
            this.dataGridViewTextBoxColumn109.ReadOnly = true;
            // 
            // DataListAttributeNameItem
            // 
            this.DataListAttributeNameItem.DataPropertyName = "DataListAttributeName";
            this.DataListAttributeNameItem.HeaderText = "DataListAttributeName";
            this.DataListAttributeNameItem.Name = "DataListAttributeNameItem";
            // 
            // DataListAtrributeValue
            // 
            this.DataListAtrributeValue.DataPropertyName = "DataListAttributeValue";
            this.DataListAtrributeValue.HeaderText = "DataListAttributeValue";
            this.DataListAtrributeValue.Name = "DataListAtrributeValue";
            // 
            // DataListItemIDItemAttribute
            // 
            this.DataListItemIDItemAttribute.DataPropertyName = "DataListItemID";
            this.DataListItemIDItemAttribute.HeaderText = "DataListItemID ";
            this.DataListItemIDItemAttribute.Name = "DataListItemIDItemAttribute";
            this.DataListItemIDItemAttribute.Visible = false;
            // 
            // DataListTypeIDItemAttr
            // 
            this.DataListTypeIDItemAttr.DataPropertyName = "DataListTypeID";
            this.DataListTypeIDItemAttr.HeaderText = "DataListTypeID ";
            this.DataListTypeIDItemAttr.Name = "DataListTypeIDItemAttr";
            this.DataListTypeIDItemAttr.Visible = false;
            // 
            // DataListValueIDItemAttr
            // 
            this.DataListValueIDItemAttr.DataPropertyName = "DataListValueID";
            this.DataListValueIDItemAttr.HeaderText = "DataListValueID ";
            this.DataListValueIDItemAttr.Name = "DataListValueIDItemAttr";
            this.DataListValueIDItemAttr.Visible = false;
            // 
            // IDItenAtrr
            // 
            this.IDItenAtrr.DataPropertyName = "ID";
            this.IDItenAtrr.HeaderText = "ID";
            this.IDItenAtrr.Name = "IDItenAtrr";
            this.IDItenAtrr.Visible = false;
            // 
            // LastModifiedDateItemAttr
            // 
            this.LastModifiedDateItemAttr.DataPropertyName = "LastModifiedDate";
            this.LastModifiedDateItemAttr.HeaderText = "LastModifiedDate ";
            this.LastModifiedDateItemAttr.Name = "LastModifiedDateItemAttr";
            this.LastModifiedDateItemAttr.Visible = false;
            // 
            // StatusItemAttr
            // 
            this.StatusItemAttr.DataPropertyName = "Status";
            this.StatusItemAttr.HeaderText = "Status";
            this.StatusItemAttr.Name = "StatusItemAttr";
            this.StatusItemAttr.Visible = false;
            // 
            // DataListAttributeID
            // 
            this.DataListAttributeID.DataPropertyName = "DataListAttributeID";
            this.DataListAttributeID.HeaderText = "DataListAttributeID";
            this.DataListAttributeID.Name = "DataListAttributeID";
            this.DataListAttributeID.Visible = false;
            // 
            // DataListTypeNameItemAttr
            // 
            this.DataListTypeNameItemAttr.DataPropertyName = "DataListTypeName";
            this.DataListTypeNameItemAttr.HeaderText = "DataListTypeName";
            this.DataListTypeNameItemAttr.Name = "DataListTypeNameItemAttr";
            this.DataListTypeNameItemAttr.ReadOnly = true;
            this.DataListTypeNameItemAttr.Visible = false;
            this.DataListTypeNameItemAttr.Width = 400;
            // 
            // IsEditableItem
            // 
            this.IsEditableItem.DataPropertyName = "IsEditable";
            this.IsEditableItem.HeaderText = "IsEditable";
            this.IsEditableItem.Name = "IsEditableItem";
            this.IsEditableItem.Visible = false;
            // 
            // NewItemsAttrSelectAllCB
            // 
            this.NewItemsAttrSelectAllCB.AutoSize = true;
            this.NewItemsAttrSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewItemsAttrSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewItemsAttrSelectAllCB.Location = new System.Drawing.Point(3, 8);
            this.NewItemsAttrSelectAllCB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.NewItemsAttrSelectAllCB.Name = "NewItemsAttrSelectAllCB";
            this.NewItemsAttrSelectAllCB.Size = new System.Drawing.Size(111, 24);
            this.NewItemsAttrSelectAllCB.TabIndex = 8;
            this.NewItemsAttrSelectAllCB.Text = "Select All";
            this.NewItemsAttrSelectAllCB.UseVisualStyleBackColor = true;
            this.NewItemsAttrSelectAllCB.CheckedChanged += new System.EventHandler(this.NewItemsAttrSelectAllCB_CheckedChanged);
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.checkBox17);
            this.tabPage8.Controls.Add(this.label3);
            this.tabPage8.Controls.Add(this.label5);
            this.tabPage8.Controls.Add(this.UpdateTargetItemAttributeView);
            this.tabPage8.Controls.Add(this.UpdateSourceItemAttributeView);
            this.tabPage8.Location = new System.Drawing.Point(4, 29);
            this.tabPage8.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage8.Size = new System.Drawing.Size(1650, 705);
            this.tabPage8.TabIndex = 1;
            this.tabPage8.Text = "Update Items";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // checkBox17
            // 
            this.checkBox17.AutoSize = true;
            this.checkBox17.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox17.Location = new System.Drawing.Point(98, 18);
            this.checkBox17.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.checkBox17.Name = "checkBox17";
            this.checkBox17.Size = new System.Drawing.Size(111, 24);
            this.checkBox17.TabIndex = 11;
            this.checkBox17.Text = "Select All";
            this.checkBox17.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1116, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "Target:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 25);
            this.label5.TabIndex = 9;
            this.label5.Text = "Source:";
            // 
            // UpdateTargetItemAttributeView
            // 
            this.UpdateTargetItemAttributeView.AllowUserToAddRows = false;
            this.UpdateTargetItemAttributeView.AllowUserToDeleteRows = false;
            this.UpdateTargetItemAttributeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UpdateTargetItemAttributeView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn24,
            this.dataGridViewTextBoxColumn118,
            this.dataGridViewTextBoxColumn119,
            this.dataGridViewTextBoxColumn120,
            this.dataGridViewTextBoxColumn121,
            this.dataGridViewTextBoxColumn122,
            this.dataGridViewTextBoxColumn123,
            this.dataGridViewTextBoxColumn124,
            this.dataGridViewTextBoxColumn125,
            this.dataGridViewTextBoxColumn126,
            this.dataGridViewCheckBoxColumn25,
            this.dataGridViewCheckBoxColumn26,
            this.dataGridViewTextBoxColumn127});
            this.UpdateTargetItemAttributeView.Location = new System.Drawing.Point(1119, 48);
            this.UpdateTargetItemAttributeView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.UpdateTargetItemAttributeView.Name = "UpdateTargetItemAttributeView";
            this.UpdateTargetItemAttributeView.RowTemplate.Height = 24;
            this.UpdateTargetItemAttributeView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.UpdateTargetItemAttributeView.Size = new System.Drawing.Size(524, 698);
            this.UpdateTargetItemAttributeView.TabIndex = 8;
            // 
            // dataGridViewCheckBoxColumn24
            // 
            this.dataGridViewCheckBoxColumn24.HeaderText = "";
            this.dataGridViewCheckBoxColumn24.Name = "dataGridViewCheckBoxColumn24";
            this.dataGridViewCheckBoxColumn24.Visible = false;
            this.dataGridViewCheckBoxColumn24.Width = 25;
            // 
            // dataGridViewTextBoxColumn118
            // 
            this.dataGridViewTextBoxColumn118.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn118.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn118.Name = "dataGridViewTextBoxColumn118";
            this.dataGridViewTextBoxColumn118.ReadOnly = true;
            this.dataGridViewTextBoxColumn118.Visible = false;
            this.dataGridViewTextBoxColumn118.Width = 200;
            // 
            // dataGridViewTextBoxColumn119
            // 
            this.dataGridViewTextBoxColumn119.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn119.HeaderText = "Code";
            this.dataGridViewTextBoxColumn119.Name = "dataGridViewTextBoxColumn119";
            this.dataGridViewTextBoxColumn119.ReadOnly = true;
            this.dataGridViewTextBoxColumn119.Visible = false;
            // 
            // dataGridViewTextBoxColumn120
            // 
            this.dataGridViewTextBoxColumn120.DataPropertyName = "OrderIndex";
            this.dataGridViewTextBoxColumn120.HeaderText = "Index";
            this.dataGridViewTextBoxColumn120.Name = "dataGridViewTextBoxColumn120";
            this.dataGridViewTextBoxColumn120.ReadOnly = true;
            this.dataGridViewTextBoxColumn120.Width = 50;
            // 
            // dataGridViewTextBoxColumn121
            // 
            this.dataGridViewTextBoxColumn121.DataPropertyName = "EffectiveStartDate";
            dataGridViewCellStyle89.Format = "d";
            dataGridViewCellStyle89.NullValue = null;
            this.dataGridViewTextBoxColumn121.DefaultCellStyle = dataGridViewCellStyle89;
            this.dataGridViewTextBoxColumn121.HeaderText = "Eff Date";
            this.dataGridViewTextBoxColumn121.Name = "dataGridViewTextBoxColumn121";
            this.dataGridViewTextBoxColumn121.ReadOnly = true;
            this.dataGridViewTextBoxColumn121.Width = 120;
            // 
            // dataGridViewTextBoxColumn122
            // 
            this.dataGridViewTextBoxColumn122.DataPropertyName = "EffectiveEndDate";
            dataGridViewCellStyle90.Format = "d";
            dataGridViewCellStyle90.NullValue = null;
            this.dataGridViewTextBoxColumn122.DefaultCellStyle = dataGridViewCellStyle90;
            this.dataGridViewTextBoxColumn122.HeaderText = "End Date";
            this.dataGridViewTextBoxColumn122.Name = "dataGridViewTextBoxColumn122";
            this.dataGridViewTextBoxColumn122.ReadOnly = true;
            this.dataGridViewTextBoxColumn122.Width = 120;
            // 
            // dataGridViewTextBoxColumn123
            // 
            this.dataGridViewTextBoxColumn123.DataPropertyName = "OrderIndexModified";
            this.dataGridViewTextBoxColumn123.HeaderText = "OrderIndexModified";
            this.dataGridViewTextBoxColumn123.Name = "dataGridViewTextBoxColumn123";
            this.dataGridViewTextBoxColumn123.ReadOnly = true;
            this.dataGridViewTextBoxColumn123.Visible = false;
            // 
            // dataGridViewTextBoxColumn124
            // 
            this.dataGridViewTextBoxColumn124.DataPropertyName = "EffectiveStartDateModified";
            this.dataGridViewTextBoxColumn124.HeaderText = "EffDateModified";
            this.dataGridViewTextBoxColumn124.Name = "dataGridViewTextBoxColumn124";
            this.dataGridViewTextBoxColumn124.ReadOnly = true;
            this.dataGridViewTextBoxColumn124.Visible = false;
            // 
            // dataGridViewTextBoxColumn125
            // 
            this.dataGridViewTextBoxColumn125.DataPropertyName = "EffectiveEndDateModified";
            this.dataGridViewTextBoxColumn125.HeaderText = "EndDateModified";
            this.dataGridViewTextBoxColumn125.Name = "dataGridViewTextBoxColumn125";
            this.dataGridViewTextBoxColumn125.ReadOnly = true;
            this.dataGridViewTextBoxColumn125.Visible = false;
            // 
            // dataGridViewTextBoxColumn126
            // 
            this.dataGridViewTextBoxColumn126.DataPropertyName = "IsEditableModified";
            this.dataGridViewTextBoxColumn126.HeaderText = "IsEditableModified";
            this.dataGridViewTextBoxColumn126.Name = "dataGridViewTextBoxColumn126";
            this.dataGridViewTextBoxColumn126.ReadOnly = true;
            this.dataGridViewTextBoxColumn126.Visible = false;
            // 
            // dataGridViewCheckBoxColumn25
            // 
            this.dataGridViewCheckBoxColumn25.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn25.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn25.Name = "dataGridViewCheckBoxColumn25";
            this.dataGridViewCheckBoxColumn25.ReadOnly = true;
            this.dataGridViewCheckBoxColumn25.Width = 60;
            // 
            // dataGridViewCheckBoxColumn26
            // 
            this.dataGridViewCheckBoxColumn26.DataPropertyName = "IsEditable";
            this.dataGridViewCheckBoxColumn26.HeaderText = "Editable";
            this.dataGridViewCheckBoxColumn26.Name = "dataGridViewCheckBoxColumn26";
            this.dataGridViewCheckBoxColumn26.ReadOnly = true;
            this.dataGridViewCheckBoxColumn26.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn26.Width = 70;
            // 
            // dataGridViewTextBoxColumn127
            // 
            this.dataGridViewTextBoxColumn127.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn127.HeaderText = "Status";
            this.dataGridViewTextBoxColumn127.Name = "dataGridViewTextBoxColumn127";
            this.dataGridViewTextBoxColumn127.ReadOnly = true;
            this.dataGridViewTextBoxColumn127.Visible = false;
            this.dataGridViewTextBoxColumn127.Width = 60;
            // 
            // UpdateSourceItemAttributeView
            // 
            this.UpdateSourceItemAttributeView.AllowUserToAddRows = false;
            this.UpdateSourceItemAttributeView.AllowUserToDeleteRows = false;
            this.UpdateSourceItemAttributeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UpdateSourceItemAttributeView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn27,
            this.dataGridViewTextBoxColumn128,
            this.dataGridViewTextBoxColumn129,
            this.dataGridViewTextBoxColumn130,
            this.dataGridViewTextBoxColumn131,
            this.dataGridViewTextBoxColumn132,
            this.dataGridViewTextBoxColumn133,
            this.dataGridViewTextBoxColumn134,
            this.dataGridViewTextBoxColumn135,
            this.dataGridViewTextBoxColumn136,
            this.dataGridViewCheckBoxColumn28,
            this.dataGridViewCheckBoxColumn29,
            this.dataGridViewTextBoxColumn137});
            this.UpdateSourceItemAttributeView.Location = new System.Drawing.Point(4, 48);
            this.UpdateSourceItemAttributeView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.UpdateSourceItemAttributeView.Name = "UpdateSourceItemAttributeView";
            this.UpdateSourceItemAttributeView.RowTemplate.Height = 24;
            this.UpdateSourceItemAttributeView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.UpdateSourceItemAttributeView.Size = new System.Drawing.Size(1114, 698);
            this.UpdateSourceItemAttributeView.TabIndex = 7;
            // 
            // dataGridViewCheckBoxColumn27
            // 
            this.dataGridViewCheckBoxColumn27.HeaderText = "";
            this.dataGridViewCheckBoxColumn27.Name = "dataGridViewCheckBoxColumn27";
            this.dataGridViewCheckBoxColumn27.Width = 25;
            // 
            // dataGridViewTextBoxColumn128
            // 
            this.dataGridViewTextBoxColumn128.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn128.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn128.Name = "dataGridViewTextBoxColumn128";
            this.dataGridViewTextBoxColumn128.ReadOnly = true;
            this.dataGridViewTextBoxColumn128.Width = 300;
            // 
            // dataGridViewTextBoxColumn129
            // 
            this.dataGridViewTextBoxColumn129.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn129.HeaderText = "Code";
            this.dataGridViewTextBoxColumn129.Name = "dataGridViewTextBoxColumn129";
            this.dataGridViewTextBoxColumn129.ReadOnly = true;
            this.dataGridViewTextBoxColumn129.Width = 200;
            // 
            // dataGridViewTextBoxColumn130
            // 
            this.dataGridViewTextBoxColumn130.DataPropertyName = "OrderIndex";
            this.dataGridViewTextBoxColumn130.HeaderText = "Index";
            this.dataGridViewTextBoxColumn130.Name = "dataGridViewTextBoxColumn130";
            this.dataGridViewTextBoxColumn130.ReadOnly = true;
            this.dataGridViewTextBoxColumn130.Width = 50;
            // 
            // dataGridViewTextBoxColumn131
            // 
            this.dataGridViewTextBoxColumn131.DataPropertyName = "EffectiveStartDate";
            dataGridViewCellStyle91.Format = "d";
            dataGridViewCellStyle91.NullValue = null;
            this.dataGridViewTextBoxColumn131.DefaultCellStyle = dataGridViewCellStyle91;
            this.dataGridViewTextBoxColumn131.HeaderText = "Eff Date";
            this.dataGridViewTextBoxColumn131.Name = "dataGridViewTextBoxColumn131";
            this.dataGridViewTextBoxColumn131.ReadOnly = true;
            this.dataGridViewTextBoxColumn131.Width = 120;
            // 
            // dataGridViewTextBoxColumn132
            // 
            this.dataGridViewTextBoxColumn132.DataPropertyName = "EffectiveEndDate";
            dataGridViewCellStyle92.Format = "d";
            dataGridViewCellStyle92.NullValue = null;
            this.dataGridViewTextBoxColumn132.DefaultCellStyle = dataGridViewCellStyle92;
            this.dataGridViewTextBoxColumn132.HeaderText = "End Date";
            this.dataGridViewTextBoxColumn132.Name = "dataGridViewTextBoxColumn132";
            this.dataGridViewTextBoxColumn132.ReadOnly = true;
            this.dataGridViewTextBoxColumn132.Width = 120;
            // 
            // dataGridViewTextBoxColumn133
            // 
            this.dataGridViewTextBoxColumn133.DataPropertyName = "OrderIndexModified";
            this.dataGridViewTextBoxColumn133.HeaderText = "OrderIndexModified";
            this.dataGridViewTextBoxColumn133.Name = "dataGridViewTextBoxColumn133";
            this.dataGridViewTextBoxColumn133.ReadOnly = true;
            this.dataGridViewTextBoxColumn133.Visible = false;
            // 
            // dataGridViewTextBoxColumn134
            // 
            this.dataGridViewTextBoxColumn134.DataPropertyName = "EffectiveStartDateModified";
            this.dataGridViewTextBoxColumn134.HeaderText = "EffDateModified";
            this.dataGridViewTextBoxColumn134.Name = "dataGridViewTextBoxColumn134";
            this.dataGridViewTextBoxColumn134.ReadOnly = true;
            this.dataGridViewTextBoxColumn134.Visible = false;
            // 
            // dataGridViewTextBoxColumn135
            // 
            this.dataGridViewTextBoxColumn135.DataPropertyName = "EffectiveEndDateModified";
            this.dataGridViewTextBoxColumn135.HeaderText = "EndDateModified";
            this.dataGridViewTextBoxColumn135.Name = "dataGridViewTextBoxColumn135";
            this.dataGridViewTextBoxColumn135.ReadOnly = true;
            this.dataGridViewTextBoxColumn135.Visible = false;
            // 
            // dataGridViewTextBoxColumn136
            // 
            this.dataGridViewTextBoxColumn136.DataPropertyName = "IsEditableModified";
            this.dataGridViewTextBoxColumn136.HeaderText = "IsEditableModified";
            this.dataGridViewTextBoxColumn136.Name = "dataGridViewTextBoxColumn136";
            this.dataGridViewTextBoxColumn136.ReadOnly = true;
            this.dataGridViewTextBoxColumn136.Visible = false;
            // 
            // dataGridViewCheckBoxColumn28
            // 
            this.dataGridViewCheckBoxColumn28.DataPropertyName = "IsActive";
            this.dataGridViewCheckBoxColumn28.HeaderText = "Active";
            this.dataGridViewCheckBoxColumn28.Name = "dataGridViewCheckBoxColumn28";
            this.dataGridViewCheckBoxColumn28.ReadOnly = true;
            this.dataGridViewCheckBoxColumn28.Width = 60;
            // 
            // dataGridViewCheckBoxColumn29
            // 
            this.dataGridViewCheckBoxColumn29.DataPropertyName = "IsEditable";
            this.dataGridViewCheckBoxColumn29.HeaderText = "Editable";
            this.dataGridViewCheckBoxColumn29.Name = "dataGridViewCheckBoxColumn29";
            this.dataGridViewCheckBoxColumn29.ReadOnly = true;
            this.dataGridViewCheckBoxColumn29.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn29.Width = 70;
            // 
            // dataGridViewTextBoxColumn137
            // 
            this.dataGridViewTextBoxColumn137.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn137.HeaderText = "Status";
            this.dataGridViewTextBoxColumn137.Name = "dataGridViewTextBoxColumn137";
            this.dataGridViewTextBoxColumn137.ReadOnly = true;
            this.dataGridViewTextBoxColumn137.Visible = false;
            this.dataGridViewTextBoxColumn137.Width = 60;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(1545, 920);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(147, 40);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // PreviewUpdate
            // 
            this.PreviewUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreviewUpdate.Location = new System.Drawing.Point(1341, 920);
            this.PreviewUpdate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.ModuleList.Location = new System.Drawing.Point(87, 9);
            this.ModuleList.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            dataGridViewCellStyle93.Format = "d";
            dataGridViewCellStyle93.NullValue = null;
            this.EffDate.DefaultCellStyle = dataGridViewCellStyle93;
            this.EffDate.HeaderText = "Eff Date";
            this.EffDate.Name = "EffDate";
            this.EffDate.ReadOnly = true;
            this.EffDate.Width = 120;
            // 
            // EndDate
            // 
            this.EndDate.DataPropertyName = "EffectiveEndDate";
            dataGridViewCellStyle94.Format = "d";
            dataGridViewCellStyle94.NullValue = null;
            this.EndDate.DefaultCellStyle = dataGridViewCellStyle94;
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
            this.dataGridViewCheckBoxColumn3.Visible = false;
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
            dataGridViewCellStyle95.Format = "d";
            dataGridViewCellStyle95.NullValue = null;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle95;
            this.dataGridViewTextBoxColumn6.HeaderText = "Eff Date";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 120;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "EffectiveEndDate";
            dataGridViewCellStyle96.Format = "d";
            dataGridViewCellStyle96.NullValue = null;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle96;
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
            this.dataGridViewCheckBoxColumn6.Visible = false;
            this.dataGridViewCheckBoxColumn6.Width = 78;
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
            dataGridViewCellStyle97.Format = "d";
            dataGridViewCellStyle97.NullValue = null;
            this.dataGridViewTextBoxColumn16.DefaultCellStyle = dataGridViewCellStyle97;
            this.dataGridViewTextBoxColumn16.HeaderText = "Eff Date";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.Width = 120;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.DataPropertyName = "EffectiveEndDate";
            dataGridViewCellStyle98.Format = "d";
            dataGridViewCellStyle98.NullValue = null;
            this.dataGridViewTextBoxColumn17.DefaultCellStyle = dataGridViewCellStyle98;
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
            this.dataGridViewCheckBoxColumn9.Visible = false;
            this.dataGridViewCheckBoxColumn9.Width = 78;
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
            this.IsEditable.Visible = false;
            this.IsEditable.Width = 70;
            // 
            // ItemsCount
            // 
            this.ItemsCount.DataPropertyName = "ItemsCount";
            dataGridViewCellStyle99.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ItemsCount.DefaultCellStyle = dataGridViewCellStyle99;
            this.ItemsCount.HeaderText = "Items";
            this.ItemsCount.Name = "ItemsCount";
            this.ItemsCount.ReadOnly = true;
            this.ItemsCount.Visible = false;
            this.ItemsCount.Width = 60;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Visible = false;
            this.Status.Width = 70;
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
            // ItemIDLang
            // 
            this.ItemIDLang.DataPropertyName = "ItemID";
            this.ItemIDLang.HeaderText = "ItemID";
            this.ItemIDLang.Name = "ItemIDLang";
            this.ItemIDLang.Visible = false;
            // 
            // IsActiveLang
            // 
            this.IsActiveLang.DataPropertyName = "IsActive";
            this.IsActiveLang.HeaderText = "IsActive";
            this.IsActiveLang.Name = "IsActiveLang";
            // 
            // DescriptionModifiedLang
            // 
            this.DescriptionModifiedLang.DataPropertyName = "DescriptionModified";
            this.DescriptionModifiedLang.HeaderText = "DescriptionModified";
            this.DescriptionModifiedLang.Name = "DescriptionModifiedLang";
            this.DescriptionModifiedLang.Visible = false;
            // 
            // LongDescrptionModifiedLang
            // 
            this.LongDescrptionModifiedLang.DataPropertyName = "LongDescriptionModified";
            this.LongDescrptionModifiedLang.HeaderText = "LongDescriptionModified";
            this.LongDescrptionModifiedLang.Name = "LongDescrptionModifiedLang";
            this.LongDescrptionModifiedLang.Visible = false;
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
            // TypeNameAttribute
            // 
            this.TypeNameAttribute.DataPropertyName = "TypeName";
            this.TypeNameAttribute.HeaderText = "TypeName";
            this.TypeNameAttribute.Name = "TypeNameAttribute";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "TenantID";
            this.Column5.HeaderText = "TenantID";
            this.Column5.Name = "Column5";
            this.Column5.Visible = false;
            // 
            // IsEditableAttribute
            // 
            this.IsEditableAttribute.DataPropertyName = "IsEditable";
            this.IsEditableAttribute.HeaderText = "IsEditable";
            this.IsEditableAttribute.Name = "IsEditableAttribute";
            this.IsEditableAttribute.Visible = false;
            // 
            // dataGridViewCheckBoxColumn16
            // 
            this.dataGridViewCheckBoxColumn16.HeaderText = "";
            this.dataGridViewCheckBoxColumn16.Name = "dataGridViewCheckBoxColumn16";
            this.dataGridViewCheckBoxColumn16.Width = 25;
            // 
            // dataGridViewTextBoxColumn54
            // 
            this.dataGridViewTextBoxColumn54.DataPropertyName = "ParentDataList";
            this.dataGridViewTextBoxColumn54.HeaderText = "ParentDataList";
            this.dataGridViewTextBoxColumn54.Name = "dataGridViewTextBoxColumn54";
            this.dataGridViewTextBoxColumn54.Width = 150;
            // 
            // dataGridViewTextBoxColumn55
            // 
            this.dataGridViewTextBoxColumn55.DataPropertyName = "ParentCode";
            this.dataGridViewTextBoxColumn55.HeaderText = "ParentCode";
            this.dataGridViewTextBoxColumn55.Name = "dataGridViewTextBoxColumn55";
            this.dataGridViewTextBoxColumn55.Width = 150;
            // 
            // dataGridViewTextBoxColumn56
            // 
            this.dataGridViewTextBoxColumn56.DataPropertyName = "ChildDataList";
            this.dataGridViewTextBoxColumn56.HeaderText = "ChildDataList";
            this.dataGridViewTextBoxColumn56.Name = "dataGridViewTextBoxColumn56";
            this.dataGridViewTextBoxColumn56.Width = 200;
            // 
            // dataGridViewTextBoxColumn57
            // 
            this.dataGridViewTextBoxColumn57.DataPropertyName = "A";
            this.dataGridViewTextBoxColumn57.HeaderText = "A";
            this.dataGridViewTextBoxColumn57.Name = "dataGridViewTextBoxColumn57";
            this.dataGridViewTextBoxColumn57.Visible = false;
            // 
            // dataGridViewTextBoxColumn58
            // 
            this.dataGridViewTextBoxColumn58.DataPropertyName = "DefaultTypeID";
            this.dataGridViewTextBoxColumn58.HeaderText = "DefaultTypeID";
            this.dataGridViewTextBoxColumn58.Name = "dataGridViewTextBoxColumn58";
            this.dataGridViewTextBoxColumn58.Visible = false;
            // 
            // dataGridViewTextBoxColumn60
            // 
            this.dataGridViewTextBoxColumn60.DataPropertyName = "ID";
            this.dataGridViewTextBoxColumn60.HeaderText = "ID";
            this.dataGridViewTextBoxColumn60.Name = "dataGridViewTextBoxColumn60";
            this.dataGridViewTextBoxColumn60.Visible = false;
            // 
            // dataGridViewTextBoxColumn61
            // 
            this.dataGridViewTextBoxColumn61.DataPropertyName = "LastModifiedDate";
            this.dataGridViewTextBoxColumn61.HeaderText = "LastModifiedDate";
            this.dataGridViewTextBoxColumn61.Name = "dataGridViewTextBoxColumn61";
            this.dataGridViewTextBoxColumn61.Visible = false;
            // 
            // dataGridViewTextBoxColumn62
            // 
            this.dataGridViewTextBoxColumn62.DataPropertyName = "ContentID";
            this.dataGridViewTextBoxColumn62.HeaderText = "Content ID";
            this.dataGridViewTextBoxColumn62.Name = "dataGridViewTextBoxColumn62";
            this.dataGridViewTextBoxColumn62.ReadOnly = true;
            this.dataGridViewTextBoxColumn62.Visible = false;
            this.dataGridViewTextBoxColumn62.Width = 400;
            // 
            // dataGridViewTextBoxColumn63
            // 
            this.dataGridViewTextBoxColumn63.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn63.HeaderText = "Description";
            this.dataGridViewTextBoxColumn63.Name = "dataGridViewTextBoxColumn63";
            this.dataGridViewTextBoxColumn63.ReadOnly = true;
            this.dataGridViewTextBoxColumn63.Width = 150;
            // 
            // dataGridViewTextBoxColumn64
            // 
            this.dataGridViewTextBoxColumn64.DataPropertyName = "IsActive";
            this.dataGridViewTextBoxColumn64.HeaderText = "IsActive";
            this.dataGridViewTextBoxColumn64.Name = "dataGridViewTextBoxColumn64";
            this.dataGridViewTextBoxColumn64.ReadOnly = true;
            this.dataGridViewTextBoxColumn64.Visible = false;
            this.dataGridViewTextBoxColumn64.Width = 250;
            // 
            // dataGridViewTextBoxColumn65
            // 
            this.dataGridViewTextBoxColumn65.DataPropertyName = "DataListID";
            this.dataGridViewTextBoxColumn65.HeaderText = "DataListID";
            this.dataGridViewTextBoxColumn65.Name = "dataGridViewTextBoxColumn65";
            this.dataGridViewTextBoxColumn65.ReadOnly = true;
            this.dataGridViewTextBoxColumn65.Visible = false;
            this.dataGridViewTextBoxColumn65.Width = 370;
            // 
            // dataGridViewTextBoxColumn105
            // 
            this.dataGridViewTextBoxColumn105.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn105.HeaderText = "Status";
            this.dataGridViewTextBoxColumn105.Name = "dataGridViewTextBoxColumn105";
            this.dataGridViewTextBoxColumn105.ReadOnly = true;
            this.dataGridViewTextBoxColumn105.Visible = false;
            this.dataGridViewTextBoxColumn105.Width = 60;
            // 
            // dataGridViewTextBoxColumn106
            // 
            this.dataGridViewTextBoxColumn106.DataPropertyName = "DefaultTypeValue";
            this.dataGridViewTextBoxColumn106.HeaderText = "DefaultTypeValue";
            this.dataGridViewTextBoxColumn106.Name = "dataGridViewTextBoxColumn106";
            this.dataGridViewTextBoxColumn106.Visible = false;
            // 
            // dataGridViewTextBoxColumn107
            // 
            this.dataGridViewTextBoxColumn107.DataPropertyName = "TenantID";
            this.dataGridViewTextBoxColumn107.HeaderText = "TenantID";
            this.dataGridViewTextBoxColumn107.Name = "dataGridViewTextBoxColumn107";
            this.dataGridViewTextBoxColumn107.Visible = false;
            // 
            // DatalistDiff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1902, 1028);
            this.Controls.Add(this.ModuleList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PreviewUpdate);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.diffTab);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.Name = "DatalistDiff";
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
            this.tabpage3.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LinkgridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewLinkView)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetLinkView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceLinkView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ItemAttribute.ResumeLayout(false);
            this.tabControl4.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewItemAttributeView)).EndInit();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateTargetItemAttributeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateSourceItemAttributeView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.TabPage tabpage3;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView NewLinkView;
        private System.Windows.Forms.CheckBox NewLinkSelectAllCB;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn66;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn67;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn68;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn69;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn70;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn71;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn72;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn73;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn74;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn75;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn76;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn77;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.DataGridView TargetLinkView;
        private System.Windows.Forms.DataGridView SourceLinkView;
        private System.Windows.Forms.DataGridView LinkgridView;
        private System.Windows.Forms.Button btnLinkitem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn20;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn91;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn92;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn93;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn94;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn95;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn96;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn97;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn98;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn99;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn100;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn101;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn102;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn103;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn104;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn59;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn78;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn79;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn80;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn81;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn82;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn83;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn84;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn85;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn86;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn87;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn88;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn89;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn90;
        private System.Windows.Forms.TabPage ItemAttribute;
        private System.Windows.Forms.TabControl tabControl4;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.CheckBox NewItemAtrrNewItemsCB;
        private System.Windows.Forms.CheckBox NewItemAttrNewDataListCB;
        private System.Windows.Forms.DataGridView NewItemAttributeView;
        private System.Windows.Forms.CheckBox NewItemsAttrSelectAllCB;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.CheckBox checkBox17;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView UpdateTargetItemAttributeView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn118;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn119;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn120;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn121;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn122;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn123;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn124;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn125;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn126;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn25;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn127;
        private System.Windows.Forms.DataGridView UpdateSourceItemAttributeView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn27;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn128;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn129;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn130;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn131;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn132;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn133;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn134;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn135;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn136;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn28;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn29;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn137;
        private System.Windows.Forms.CheckBox NewItemAttrExitingItemsCB;
        private System.Windows.Forms.Button btnItemAttribute;
        private System.Windows.Forms.Label lblTypeDataListItem;
        private System.Windows.Forms.Label lblTypeDataList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataListTypeNameItemAtrr;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn109;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataListAttributeNameItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataListAtrributeValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataListItemIDItemAttribute;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataListTypeIDItemAttr;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataListValueIDItemAttr;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDItenAtrr;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastModifiedDateItemAttr;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusItemAttr;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataListAttributeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataListTypeNameItemAttr;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsEditableItem;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn LongDescriptionModifiedTarget;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescriptionModifiedTarget;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn42;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn43;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn44;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn45;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn51;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn46;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn47;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn48;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn49;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn53;
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
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsEditable;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemsCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn Locale;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemIDLang;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsActiveLang;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescriptionModifiedLang;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongDescrptionModifiedLang;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeNameAttribute;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsEditableAttribute;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn54;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn55;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn56;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn57;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn58;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn60;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn61;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn62;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn63;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn64;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn65;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn105;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn106;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn107;
    }
}