//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace DatalistSyncUtil
{
    partial class DataListSync
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
            this.DataListView = new System.Windows.Forms.DataGridView();
            this.DataListLoad = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.SelectAllChkBox = new System.Windows.Forms.CheckBox();
            this.ModuleList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Close = new System.Windows.Forms.Button();
            this.Preview = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Age = new System.Windows.Forms.NumericUpDown();
            this.BtnClearCache = new System.Windows.Forms.Button();
            this.InactiveCB = new System.Windows.Forms.CheckBox();
            this.BtnDownloadToFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TenantList = new System.Windows.Forms.ComboBox();
            this.btnCompare = new System.Windows.Forms.Button();
            this.control_label = new System.Windows.Forms.Label();
            this.ControlName = new System.Windows.Forms.ComboBox();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ContentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Age)).BeginInit();
            this.SuspendLayout();
            // 
            // DataListView
            // 
            this.DataListView.AllowUserToAddRows = false;
            this.DataListView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataListView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataListView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.ContentID,
            this.Column1,
            this.IsActive});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataListView.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataListView.Location = new System.Drawing.Point(11, 110);
            this.DataListView.Margin = new System.Windows.Forms.Padding(2);
            this.DataListView.Name = "DataListView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataListView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DataListView.RowTemplate.Height = 24;
            this.DataListView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DataListView.Size = new System.Drawing.Size(736, 439);
            this.DataListView.TabIndex = 2;
            this.DataListView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataListView_CellDoubleClick);
            this.DataListView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataListView_CellValueChanged);
            // 
            // DataListLoad
            // 
            this.DataListLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataListLoad.Location = new System.Drawing.Point(691, 79);
            this.DataListLoad.Margin = new System.Windows.Forms.Padding(2);
            this.DataListLoad.Name = "DataListLoad";
            this.DataListLoad.Size = new System.Drawing.Size(56, 28);
            this.DataListLoad.TabIndex = 4;
            this.DataListLoad.Text = "Load";
            this.DataListLoad.UseVisualStyleBackColor = true;
            this.DataListLoad.Click += new System.EventHandler(this.DataListLoad_Click);
            // 
            // Start
            // 
            this.Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start.Location = new System.Drawing.Point(11, 557);
            this.Start.Margin = new System.Windows.Forms.Padding(2);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(130, 28);
            this.Start.TabIndex = 5;
            this.Start.Text = "Generate Script";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // SelectAllChkBox
            // 
            this.SelectAllChkBox.AutoSize = true;
            this.SelectAllChkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectAllChkBox.Location = new System.Drawing.Point(46, 88);
            this.SelectAllChkBox.Margin = new System.Windows.Forms.Padding(2);
            this.SelectAllChkBox.Name = "SelectAllChkBox";
            this.SelectAllChkBox.Size = new System.Drawing.Size(80, 17);
            this.SelectAllChkBox.TabIndex = 6;
            this.SelectAllChkBox.Text = "Select All";
            this.SelectAllChkBox.UseVisualStyleBackColor = true;
            this.SelectAllChkBox.CheckedChanged += new System.EventHandler(this.SelectAllChkBox_CheckedChanged);
            // 
            // ModuleList
            // 
            this.ModuleList.DisplayMember = "ModuleName";
            this.ModuleList.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModuleList.FormattingEnabled = true;
            this.ModuleList.Location = new System.Drawing.Point(274, 10);
            this.ModuleList.Margin = new System.Windows.Forms.Padding(2);
            this.ModuleList.Name = "ModuleList";
            this.ModuleList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ModuleList.Size = new System.Drawing.Size(240, 69);
            this.ModuleList.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(226, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Module:";
            // 
            // Close
            // 
            this.Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close.Location = new System.Drawing.Point(680, 557);
            this.Close.Margin = new System.Windows.Forms.Padding(2);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(68, 28);
            this.Close.TabIndex = 14;
            this.Close.Text = "Close";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // Preview
            // 
            this.Preview.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Preview.Location = new System.Drawing.Point(146, 557);
            this.Preview.Margin = new System.Windows.Forms.Padding(2);
            this.Preview.Name = "Preview";
            this.Preview.Size = new System.Drawing.Size(68, 28);
            this.Preview.TabIndex = 15;
            this.Preview.Text = "Preview";
            this.Preview.UseVisualStyleBackColor = true;
            // 
            // Clear
            // 
            this.Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clear.Location = new System.Drawing.Point(218, 557);
            this.Clear.Margin = new System.Windows.Forms.Padding(2);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(68, 28);
            this.Clear.TabIndex = 16;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(518, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Age (in days):";
            // 
            // Age
            // 
            this.Age.Location = new System.Drawing.Point(596, 11);
            this.Age.Margin = new System.Windows.Forms.Padding(2);
            this.Age.Name = "Age";
            this.Age.Size = new System.Drawing.Size(48, 20);
            this.Age.TabIndex = 18;
            // 
            // BtnClearCache
            // 
            this.BtnClearCache.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearCache.Location = new System.Drawing.Point(664, 1);
            this.BtnClearCache.Margin = new System.Windows.Forms.Padding(2);
            this.BtnClearCache.Name = "BtnClearCache";
            this.BtnClearCache.Size = new System.Drawing.Size(82, 28);
            this.BtnClearCache.TabIndex = 19;
            this.BtnClearCache.Text = "Clear Cache";
            this.BtnClearCache.UseVisualStyleBackColor = true;
            this.BtnClearCache.Click += new System.EventHandler(this.BtnClearCache_Click);
            // 
            // InactiveCB
            // 
            this.InactiveCB.AutoSize = true;
            this.InactiveCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.InactiveCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InactiveCB.Location = new System.Drawing.Point(518, 44);
            this.InactiveCB.Margin = new System.Windows.Forms.Padding(2);
            this.InactiveCB.Name = "InactiveCB";
            this.InactiveCB.Size = new System.Drawing.Size(72, 17);
            this.InactiveCB.TabIndex = 13;
            this.InactiveCB.Text = "Inactive";
            this.InactiveCB.UseVisualStyleBackColor = true;
            // 
            // BtnDownloadToFile
            // 
            this.BtnDownloadToFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDownloadToFile.Location = new System.Drawing.Point(546, 557);
            this.BtnDownloadToFile.Margin = new System.Windows.Forms.Padding(2);
            this.BtnDownloadToFile.Name = "BtnDownloadToFile";
            this.BtnDownloadToFile.Size = new System.Drawing.Size(130, 28);
            this.BtnDownloadToFile.TabIndex = 20;
            this.BtnDownloadToFile.Text = "Download to File";
            this.BtnDownloadToFile.UseVisualStyleBackColor = true;
            this.BtnDownloadToFile.Click += new System.EventHandler(this.BtnDownloadToFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Tenant:";
            // 
            // TenantList
            // 
            this.TenantList.DisplayMember = "TenantName";
            this.TenantList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TenantList.FormattingEnabled = true;
            this.TenantList.Location = new System.Drawing.Point(62, 19);
            this.TenantList.Margin = new System.Windows.Forms.Padding(2);
            this.TenantList.Name = "TenantList";
            this.TenantList.Size = new System.Drawing.Size(152, 21);
            this.TenantList.Sorted = true;
            this.TenantList.TabIndex = 22;
            this.TenantList.ValueMember = "TenantID";
            this.TenantList.SelectedIndexChanged += new System.EventHandler(this.TenantList_SelectedIndexChanged);
            // 
            // btnCompare
            // 
            this.btnCompare.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompare.Location = new System.Drawing.Point(474, 557);
            this.btnCompare.Margin = new System.Windows.Forms.Padding(2);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(68, 28);
            this.btnCompare.TabIndex = 23;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.BtnCompare_Click);
            // 
            // control_label
            // 
            this.control_label.AutoSize = true;
            this.control_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.control_label.Location = new System.Drawing.Point(9, 61);
            this.control_label.Name = "control_label";
            this.control_label.Size = new System.Drawing.Size(51, 13);
            this.control_label.TabIndex = 24;
            this.control_label.Text = "Control:";
            // 
            // ControlName
            // 
            this.ControlName.DisplayMember = "ControlName";
            this.ControlName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ControlName.FormattingEnabled = true;
            this.ControlName.Location = new System.Drawing.Point(62, 58);
            this.ControlName.Margin = new System.Windows.Forms.Padding(2);
            this.ControlName.Name = "ControlName";
            this.ControlName.Size = new System.Drawing.Size(152, 21);
            this.ControlName.Sorted = true;
            this.ControlName.TabIndex = 25;
            this.ControlName.ValueMember = "TenantID";
            this.ControlName.SelectedIndexChanged += new System.EventHandler(this.ControlName_SelectedIndexChanged);
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
            this.ContentID.Visible = false;
            this.ContentID.Width = 600;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Name";
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.Width = 550;
            // 
            // IsActive
            // 
            this.IsActive.DataPropertyName = "IsActive";
            this.IsActive.HeaderText = "Active";
            this.IsActive.Name = "IsActive";
            this.IsActive.ReadOnly = true;
            this.IsActive.Width = 60;
            // 
            // DataListSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 592);
            this.Controls.Add(this.control_label);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.TenantList);
            this.Controls.Add(this.ControlName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnDownloadToFile);
            this.Controls.Add(this.BtnClearCache);
            this.Controls.Add(this.Age);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.Preview);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.InactiveCB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ModuleList);
            this.Controls.Add(this.SelectAllChkBox);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.DataListLoad);
            this.Controls.Add(this.DataListView);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "DataListSync";
            this.Text = "Datalist Utility";
            ((System.ComponentModel.ISupportInitialize)(this.DataListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Age)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataListView;
        private System.Windows.Forms.Button DataListLoad;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.CheckBox SelectAllChkBox;
        private System.Windows.Forms.ListBox ModuleList;
        private System.Windows.Forms.Label label1;
        private new System.Windows.Forms.Button Close;
        private System.Windows.Forms.Button Preview;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown Age;
        private System.Windows.Forms.Button BtnClearCache;
        private System.Windows.Forms.CheckBox InactiveCB;
        private System.Windows.Forms.Button BtnDownloadToFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox TenantList;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Label control_label;
        private System.Windows.Forms.ComboBox ControlName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
    }
}