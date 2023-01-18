﻿using System.Windows.Forms;

namespace EventRouterByPassTool
{
    partial class MainForm
    {
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.allTabs = new System.Windows.Forms.TabControl();
            this.tabPayload = new System.Windows.Forms.TabPage();
            this.wbXML = new System.Windows.Forms.WebBrowser();
            this.tbErrors = new System.Windows.Forms.TabPage();
            this.tbError = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbPayloadContent = new System.Windows.Forms.RichTextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbEndpoint = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonNormalTest = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.labelProgess = new System.Windows.Forms.Label();
            this.tenantIds = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.moduleIDs = new System.Windows.Forms.ComboBox();
            this.moduleIDTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.btnbrowseFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.checkBoxOverwriteGroupId = new System.Windows.Forms.CheckBox();
            this.txtBoxGroupId = new System.Windows.Forms.TextBox();
            this.checkboxMasive = new System.Windows.Forms.CheckBox();
            this.numericMasive = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonStatusX = new System.Windows.Forms.RadioButton();
            this.radioButtonStatus3 = new System.Windows.Forms.RadioButton();
            this.radioButtonStatus1 = new System.Windows.Forms.RadioButton();
            this.btnCleanTables = new System.Windows.Forms.Button();
            this.chkBoxAggregate = new System.Windows.Forms.CheckBox();
            this.txtBoxAggregate = new System.Windows.Forms.TextBox();
            this.allTabs.SuspendLayout();
            this.tabPayload.SuspendLayout();
            this.tbErrors.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMasive)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbFileName
            // 
            this.tbFileName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFileName.Location = new System.Drawing.Point(11, 21);
            this.tbFileName.Margin = new System.Windows.Forms.Padding(2);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.ReadOnly = true;
            this.tbFileName.Size = new System.Drawing.Size(944, 24);
            this.tbFileName.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(1016, 21);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowse.MaximumSize = new System.Drawing.Size(105, 31);
            this.btnBrowse.MinimumSize = new System.Drawing.Size(105, 31);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(105, 31);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Event Payload File";
            // 
            // allTabs
            // 
            this.allTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.allTabs.Controls.Add(this.tabPayload);
            this.allTabs.Controls.Add(this.tbErrors);
            this.allTabs.Controls.Add(this.tabPage1);
            this.allTabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allTabs.Location = new System.Drawing.Point(17, 53);
            this.allTabs.Margin = new System.Windows.Forms.Padding(2);
            this.allTabs.Name = "allTabs";
            this.allTabs.SelectedIndex = 0;
            this.allTabs.Size = new System.Drawing.Size(1282, 400);
            this.allTabs.TabIndex = 3;
            // 
            // tabPayload
            // 
            this.tabPayload.Controls.Add(this.wbXML);
            this.tabPayload.Location = new System.Drawing.Point(4, 29);
            this.tabPayload.Margin = new System.Windows.Forms.Padding(2);
            this.tabPayload.Name = "tabPayload";
            this.tabPayload.Padding = new System.Windows.Forms.Padding(2);
            this.tabPayload.Size = new System.Drawing.Size(1274, 367);
            this.tabPayload.TabIndex = 0;
            this.tabPayload.Text = "Event Payload";
            this.tabPayload.UseVisualStyleBackColor = true;
            // 
            // wbXML
            // 
            this.wbXML.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wbXML.Location = new System.Drawing.Point(2, 2);
            this.wbXML.MinimumSize = new System.Drawing.Size(1000, 400);
            this.wbXML.Name = "wbXML";
            this.wbXML.Size = new System.Drawing.Size(1276, 418);
            this.wbXML.TabIndex = 0;
            // 
            // tbErrors
            // 
            this.tbErrors.Controls.Add(this.tbError);
            this.tbErrors.Location = new System.Drawing.Point(4, 29);
            this.tbErrors.Name = "tbErrors";
            this.tbErrors.Padding = new System.Windows.Forms.Padding(3);
            this.tbErrors.Size = new System.Drawing.Size(1274, 354);
            this.tbErrors.TabIndex = 1;
            this.tbErrors.Text = "Event Logs";
            this.tbErrors.UseVisualStyleBackColor = true;
            // 
            // tbError
            // 
            this.tbError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbError.Location = new System.Drawing.Point(3, 3);
            this.tbError.Margin = new System.Windows.Forms.Padding(2);
            this.tbError.Multiline = true;
            this.tbError.Name = "tbError";
            this.tbError.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbError.Size = new System.Drawing.Size(1268, 348);
            this.tbError.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.DarkGray;
            this.tabPage1.Controls.Add(this.tbPayloadContent);
            this.tabPage1.Controls.Add(this.btnCancel);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1274, 354);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "XML";
            // 
            // tbPayloadContent
            // 
            this.tbPayloadContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPayloadContent.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPayloadContent.Location = new System.Drawing.Point(1, 0);
            this.tbPayloadContent.Name = "tbPayloadContent";
            this.tbPayloadContent.ShowSelectionMargin = true;
            this.tbPayloadContent.Size = new System.Drawing.Size(1269, 313);
            this.tbPayloadContent.TabIndex = 12;
            this.tbPayloadContent.Text = "";
            this.tbPayloadContent.WordWrap = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(1140, 318);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 31);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(1219, 318);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(50, 31);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbEndpoint
            // 
            this.cbEndpoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbEndpoint.BackColor = System.Drawing.SystemColors.Window;
            this.cbEndpoint.DisplayMember = "Name";
            this.cbEndpoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEndpoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEndpoint.FormattingEnabled = true;
            this.cbEndpoint.Location = new System.Drawing.Point(158, 540);
            this.cbEndpoint.Margin = new System.Windows.Forms.Padding(2);
            this.cbEndpoint.Name = "cbEndpoint";
            this.cbEndpoint.Size = new System.Drawing.Size(486, 26);
            this.cbEndpoint.TabIndex = 4;
            this.cbEndpoint.ValueMember = "Value";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 543);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Connection:";
            // 
            // buttonNormalTest
            // 
            this.buttonNormalTest.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonNormalTest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonNormalTest.Enabled = false;
            this.buttonNormalTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNormalTest.Location = new System.Drawing.Point(23, 590);
            this.buttonNormalTest.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNormalTest.Name = "buttonNormalTest";
            this.buttonNormalTest.Size = new System.Drawing.Size(621, 50);
            this.buttonNormalTest.TabIndex = 5;
            this.buttonNormalTest.Text = "Submit Event";
            this.buttonNormalTest.UseVisualStyleBackColor = true;
            this.buttonNormalTest.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // labelProgess
            // 
            this.labelProgess.AutoSize = true;
            this.labelProgess.Location = new System.Drawing.Point(603, 785);
            this.labelProgess.Name = "labelProgess";
            this.labelProgess.Size = new System.Drawing.Size(0, 18);
            this.labelProgess.TabIndex = 11;
            // 
            // tenantIds
            // 
            this.tenantIds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tenantIds.BackColor = System.Drawing.SystemColors.Window;
            this.tenantIds.DisplayMember = "Name";
            this.tenantIds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tenantIds.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenantIds.FormattingEnabled = true;
            this.tenantIds.Location = new System.Drawing.Point(158, 502);
            this.tenantIds.Margin = new System.Windows.Forms.Padding(2);
            this.tenantIds.Name = "tenantIds";
            this.tenantIds.Size = new System.Drawing.Size(486, 26);
            this.tenantIds.TabIndex = 12;
            this.tenantIds.ValueMember = "Value";
            this.tenantIds.SelectedIndexChanged += new System.EventHandler(this.tenantIds_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 510);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 18);
            this.label3.TabIndex = 13;
            this.label3.Text = "Aggregate Root Id";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(48, 470);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 18);
            this.label4.TabIndex = 15;
            this.label4.Text = "Module ID";
            // 
            // moduleIDs
            // 
            this.moduleIDs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.moduleIDs.BackColor = System.Drawing.SystemColors.Window;
            this.moduleIDs.DisplayMember = "Name";
            this.moduleIDs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moduleIDs.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moduleIDs.FormattingEnabled = true;
            this.moduleIDs.Location = new System.Drawing.Point(158, 467);
            this.moduleIDs.Margin = new System.Windows.Forms.Padding(2);
            this.moduleIDs.Name = "moduleIDs";
            this.moduleIDs.Size = new System.Drawing.Size(486, 26);
            this.moduleIDs.TabIndex = 14;
            this.moduleIDs.ValueMember = "Value";
            // 
            // btnbrowseFolder
            // 
            this.btnbrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnbrowseFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbrowseFolder.Location = new System.Drawing.Point(1147, 21);
            this.btnbrowseFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btnbrowseFolder.MaximumSize = new System.Drawing.Size(160, 60);
            this.btnbrowseFolder.MinimumSize = new System.Drawing.Size(105, 31);
            this.btnbrowseFolder.Name = "btnbrowseFolder";
            this.btnbrowseFolder.Size = new System.Drawing.Size(152, 31);
            this.btnbrowseFolder.TabIndex = 17;
            this.btnbrowseFolder.Text = "Select a folder";
            this.btnbrowseFolder.UseVisualStyleBackColor = true;
            this.btnbrowseFolder.Click += new System.EventHandler(this.btnbrowseFolder_Click);
            // 
            // checkBoxOverwriteGroupId
            // 
            this.checkBoxOverwriteGroupId.AutoSize = true;
            this.checkBoxOverwriteGroupId.Location = new System.Drawing.Point(678, 458);
            this.checkBoxOverwriteGroupId.Name = "checkBoxOverwriteGroupId";
            this.checkBoxOverwriteGroupId.Size = new System.Drawing.Size(151, 22);
            this.checkBoxOverwriteGroupId.TabIndex = 18;
            this.checkBoxOverwriteGroupId.Text = "Overwrite GroupId:";
            this.checkBoxOverwriteGroupId.UseVisualStyleBackColor = true;
            this.checkBoxOverwriteGroupId.CheckedChanged += new System.EventHandler(this.checkBoxOverwriteGroupId_CheckedChanged);
            // 
            // txtBoxGroupId
            // 
            this.txtBoxGroupId.Location = new System.Drawing.Point(836, 457);
            this.txtBoxGroupId.Name = "txtBoxGroupId";
            this.txtBoxGroupId.Size = new System.Drawing.Size(295, 24);
            this.txtBoxGroupId.TabIndex = 19;
            this.txtBoxGroupId.Text = "00000000-0000-0000-0000-000000000000";
            this.txtBoxGroupId.TextChanged += new System.EventHandler(this.txtBoxGroupId_TextChanged);
            // 
            // checkboxMasive
            // 
            this.checkboxMasive.AutoSize = true;
            this.checkboxMasive.Location = new System.Drawing.Point(678, 529);
            this.checkboxMasive.Name = "checkboxMasive";
            this.checkboxMasive.Size = new System.Drawing.Size(111, 22);
            this.checkboxMasive.TabIndex = 20;
            this.checkboxMasive.Text = "Masive Load";
            this.checkboxMasive.UseVisualStyleBackColor = true;
            this.checkboxMasive.CheckedChanged += new System.EventHandler(this.checkboxMasive_CheckedChanged);
            // 
            // numericMasive
            // 
            this.numericMasive.Location = new System.Drawing.Point(795, 527);
            this.numericMasive.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numericMasive.Name = "numericMasive";
            this.numericMasive.Size = new System.Drawing.Size(120, 24);
            this.numericMasive.TabIndex = 21;
            this.numericMasive.ThousandsSeparator = true;
            this.numericMasive.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonStatusX);
            this.groupBox1.Controls.Add(this.radioButtonStatus3);
            this.groupBox1.Controls.Add(this.radioButtonStatus1);
            this.groupBox1.Location = new System.Drawing.Point(795, 566);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 102);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Event Subscriptions Status";
            // 
            // radioButtonStatusX
            // 
            this.radioButtonStatusX.AutoSize = true;
            this.radioButtonStatusX.Location = new System.Drawing.Point(6, 74);
            this.radioButtonStatusX.Name = "radioButtonStatusX";
            this.radioButtonStatusX.Size = new System.Drawing.Size(257, 22);
            this.radioButtonStatusX.TabIndex = 2;
            this.radioButtonStatusX.TabStop = true;
            this.radioButtonStatusX.Text = "Randomize successes and failures";
            this.radioButtonStatusX.UseVisualStyleBackColor = true;
            // 
            // radioButtonStatus3
            // 
            this.radioButtonStatus3.AutoSize = true;
            this.radioButtonStatus3.Location = new System.Drawing.Point(6, 47);
            this.radioButtonStatus3.Name = "radioButtonStatus3";
            this.radioButtonStatus3.Size = new System.Drawing.Size(84, 22);
            this.radioButtonStatus3.TabIndex = 1;
            this.radioButtonStatus3.Text = "All Failed";
            this.radioButtonStatus3.UseVisualStyleBackColor = true;
            // 
            // radioButtonStatus1
            // 
            this.radioButtonStatus1.AutoSize = true;
            this.radioButtonStatus1.Checked = true;
            this.radioButtonStatus1.Location = new System.Drawing.Point(6, 23);
            this.radioButtonStatus1.Name = "radioButtonStatus1";
            this.radioButtonStatus1.Size = new System.Drawing.Size(103, 22);
            this.radioButtonStatus1.TabIndex = 0;
            this.radioButtonStatus1.TabStop = true;
            this.radioButtonStatus1.Text = "All Succeed";
            this.radioButtonStatus1.UseVisualStyleBackColor = true;
            // 
            // btnCleanTables
            // 
            this.btnCleanTables.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCleanTables.Location = new System.Drawing.Point(678, 580);
            this.btnCleanTables.Name = "btnCleanTables";
            this.btnCleanTables.Size = new System.Drawing.Size(104, 44);
            this.btnCleanTables.TabIndex = 23;
            this.btnCleanTables.Text = "Clean tables by GroupId";
            this.btnCleanTables.UseVisualStyleBackColor = true;
            this.btnCleanTables.Click += new System.EventHandler(this.btnCleanTables_Click);
            // 
            // chkBoxAggregate
            // 
            this.chkBoxAggregate.AutoSize = true;
            this.chkBoxAggregate.Location = new System.Drawing.Point(678, 491);
            this.chkBoxAggregate.Name = "chkBoxAggregate";
            this.chkBoxAggregate.Size = new System.Drawing.Size(237, 22);
            this.chkBoxAggregate.TabIndex = 24;
            this.chkBoxAggregate.Text = "Overwrite Aggregate Root Type:";
            this.chkBoxAggregate.UseVisualStyleBackColor = true;
            this.chkBoxAggregate.CheckedChanged += new System.EventHandler(this.chkBoxAggregate_CheckedChanged);
            // 
            // txtBoxAggregate
            // 
            this.txtBoxAggregate.Location = new System.Drawing.Point(921, 489);
            this.txtBoxAggregate.Name = "txtBoxAggregate";
            this.txtBoxAggregate.Size = new System.Drawing.Size(210, 24);
            this.txtBoxAggregate.TabIndex = 25;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1327, 671);
            this.Controls.Add(this.txtBoxAggregate);
            this.Controls.Add(this.chkBoxAggregate);
            this.Controls.Add(this.btnCleanTables);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.numericMasive);
            this.Controls.Add(this.checkboxMasive);
            this.Controls.Add(this.txtBoxGroupId);
            this.Controls.Add(this.checkBoxOverwriteGroupId);
            this.Controls.Add(this.btnbrowseFolder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.moduleIDs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tenantIds);
            this.Controls.Add(this.labelProgess);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonNormalTest);
            this.Controls.Add(this.cbEndpoint);
            this.Controls.Add(this.allTabs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbFileName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Event Publishing Test Client";
            this.allTabs.ResumeLayout(false);
            this.tabPayload.ResumeLayout(false);
            this.tbErrors.ResumeLayout(false);
            this.tbErrors.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMasive)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl allTabs;
        private System.Windows.Forms.TabPage tabPayload;
        private System.Windows.Forms.ComboBox cbEndpoint;
        private System.Windows.Forms.Button buttonNormalTest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tbErrors;
        private System.Windows.Forms.TextBox tbError;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.WebBrowser wbXML;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RichTextBox tbPayloadContent;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label labelProgess;
        private System.Windows.Forms.ComboBox tenantIds;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox moduleIDs;
        private System.Windows.Forms.Button btnbrowseFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private ToolTip moduleIDTooltip;
        private CheckBox checkBoxOverwriteGroupId;
        private TextBox txtBoxGroupId;
        private CheckBox checkboxMasive;
        private NumericUpDown numericMasive;
        private GroupBox groupBox1;
        private RadioButton radioButtonStatus1;
        private RadioButton radioButtonStatusX;
        private RadioButton radioButtonStatus3;
        private Button btnCleanTables;
        private CheckBox chkBoxAggregate;
        private TextBox txtBoxAggregate;
    }
}

