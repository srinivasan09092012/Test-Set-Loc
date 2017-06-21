namespace DatalistSyncUtil
{
    partial class AppSettingDiff
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl diffTab;
        private System.Windows.Forms.TabPage AppSettingTabPage;
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
            this.ModuleList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PreviewUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.AppSetting = new System.Windows.Forms.TabPage();
            this.ItemsTab = new System.Windows.Forms.TabControl();
            this.NewItems = new System.Windows.Forms.TabPage();
            this.btnListUpdate = new System.Windows.Forms.Button();
            this.NewItemsView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TargetValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewItemsSelectAllCB = new System.Windows.Forms.CheckBox();
            this.AppSettingTab = new System.Windows.Forms.TabControl();
            this.drpApplication = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AppSetting.SuspendLayout();
            this.ItemsTab.SuspendLayout();
            this.NewItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewItemsView)).BeginInit();
            this.AppSettingTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // ModuleList
            // 
            this.ModuleList.DisplayMember = "ModuleName";
            this.ModuleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModuleList.FormattingEnabled = true;
            this.ModuleList.Location = new System.Drawing.Point(116, 12);
            this.ModuleList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ModuleList.Name = "ModuleList";
            this.ModuleList.Size = new System.Drawing.Size(209, 21);
            this.ModuleList.Sorted = true;
            this.ModuleList.TabIndex = 30;
            this.ModuleList.ValueMember = "TenantModuleID";
            this.ModuleList.SelectedIndexChanged += new System.EventHandler(this.ModuleList_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(60, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Module:";
            // 
            // PreviewUpdate
            // 
            this.PreviewUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreviewUpdate.Location = new System.Drawing.Point(918, 687);
            this.PreviewUpdate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.PreviewUpdate.Name = "PreviewUpdate";
            this.PreviewUpdate.Size = new System.Drawing.Size(132, 26);
            this.PreviewUpdate.TabIndex = 36;
            this.PreviewUpdate.Text = "Preview && Update";
            this.PreviewUpdate.UseVisualStyleBackColor = true;
            this.PreviewUpdate.Click += new System.EventHandler(this.PreviewUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(1073, 687);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 26);
            this.btnClose.TabIndex = 35;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // AppSetting
            // 
            this.AppSetting.Controls.Add(this.ItemsTab);
            this.AppSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppSetting.Location = new System.Drawing.Point(4, 22);
            this.AppSetting.Name = "AppSetting";
            this.AppSetting.Padding = new System.Windows.Forms.Padding(3);
            this.AppSetting.Size = new System.Drawing.Size(1151, 570);
            this.AppSetting.TabIndex = 1;
            this.AppSetting.Text = "AppSetting";
            this.AppSetting.UseVisualStyleBackColor = true;
            // 
            // ItemsTab
            // 
            this.ItemsTab.Controls.Add(this.NewItems);
            this.ItemsTab.Location = new System.Drawing.Point(6, 15);
            this.ItemsTab.Name = "ItemsTab";
            this.ItemsTab.SelectedIndex = 0;
            this.ItemsTab.Size = new System.Drawing.Size(1139, 552);
            this.ItemsTab.TabIndex = 0;
            // 
            // NewItems
            // 
            this.NewItems.Controls.Add(this.btnListUpdate);
            this.NewItems.Controls.Add(this.NewItemsView);
            this.NewItems.Controls.Add(this.NewItemsSelectAllCB);
            this.NewItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewItems.Location = new System.Drawing.Point(4, 22);
            this.NewItems.Name = "NewItems";
            this.NewItems.Padding = new System.Windows.Forms.Padding(3);
            this.NewItems.Size = new System.Drawing.Size(1131, 526);
            this.NewItems.TabIndex = 0;
            this.NewItems.Text = "NewItems";
            this.NewItems.UseVisualStyleBackColor = true;
            // 
            // btnListUpdate
            // 
            this.btnListUpdate.Location = new System.Drawing.Point(5, 497);
            this.btnListUpdate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnListUpdate.Name = "btnListUpdate";
            this.btnListUpdate.Size = new System.Drawing.Size(67, 26);
            this.btnListUpdate.TabIndex = 34;
            this.btnListUpdate.Text = "Include";
            this.btnListUpdate.UseVisualStyleBackColor = true;
            this.btnListUpdate.Click += new System.EventHandler(this.BtnListUpdate_Click);
            // 
            // NewItemsView
            // 
            this.NewItemsView.AllowUserToAddRows = false;
            this.NewItemsView.AllowUserToDeleteRows = false;
            this.NewItemsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NewItemsView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.Column1,
            this.TargetValue,
            this.dataGridViewCheckBoxColumn3,
            this.Column4});
            this.NewItemsView.Location = new System.Drawing.Point(2, 29);
            this.NewItemsView.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewItemsView.Name = "NewItemsView";
            this.NewItemsView.RowTemplate.Height = 24;
            this.NewItemsView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NewItemsView.Size = new System.Drawing.Size(1124, 457);
            this.NewItemsView.TabIndex = 33;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 25;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "AppSettingKey";
            this.dataGridViewTextBoxColumn1.HeaderText = "AppSetting Key";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Value";
            this.Column1.HeaderText = "Value";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 500;
            // 
            // TargetValue
            // 
            this.TargetValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.TargetValue.DataPropertyName = "TargetValue";
            this.TargetValue.HeaderText = "TargetValue";
            this.TargetValue.Name = "TargetValue";
            this.TargetValue.Width = 101;
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
            // Column4
            // 
            this.Column4.HeaderText = "Status";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Visible = false;
            this.Column4.Width = 60;
            // 
            // NewItemsSelectAllCB
            // 
            this.NewItemsSelectAllCB.AutoSize = true;
            this.NewItemsSelectAllCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewItemsSelectAllCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewItemsSelectAllCB.Location = new System.Drawing.Point(4, 6);
            this.NewItemsSelectAllCB.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewItemsSelectAllCB.Name = "NewItemsSelectAllCB";
            this.NewItemsSelectAllCB.Size = new System.Drawing.Size(80, 17);
            this.NewItemsSelectAllCB.TabIndex = 20;
            this.NewItemsSelectAllCB.Text = "Select All";
            this.NewItemsSelectAllCB.UseVisualStyleBackColor = true;
            this.NewItemsSelectAllCB.CheckedChanged += new System.EventHandler(this.NewItemsSelectAllCB_CheckedChanged);
            // 
            // AppSettingTab
            // 
            this.AppSettingTab.Controls.Add(this.AppSetting);
            this.AppSettingTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppSettingTab.Location = new System.Drawing.Point(5, 85);
            this.AppSettingTab.Name = "AppSettingTab";
            this.AppSettingTab.SelectedIndex = 0;
            this.AppSettingTab.Size = new System.Drawing.Size(1159, 596);
            this.AppSettingTab.TabIndex = 37;
            // 
            // drpApplication
            // 
            this.drpApplication.DisplayMember = "ModuleName";
            this.drpApplication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpApplication.FormattingEnabled = true;
            this.drpApplication.Location = new System.Drawing.Point(116, 39);
            this.drpApplication.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.drpApplication.Name = "drpApplication";
            this.drpApplication.Size = new System.Drawing.Size(209, 21);
            this.drpApplication.Sorted = true;
            this.drpApplication.TabIndex = 38;
            this.drpApplication.ValueMember = "TenantModuleID";
            this.drpApplication.SelectedIndexChanged += new System.EventHandler(this.DrpApplication_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Application Name:";
            // 
            // AppSettingDiff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 725);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.drpApplication);
            this.Controls.Add(this.AppSettingTab);
            this.Controls.Add(this.PreviewUpdate);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ModuleList);
            this.Controls.Add(this.label4);
            this.Name = "AppSettingDiff";
            this.Text = "AppSettingDiff";
            this.AppSetting.ResumeLayout(false);
            this.ItemsTab.ResumeLayout(false);
            this.NewItems.ResumeLayout(false);
            this.NewItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewItemsView)).EndInit();
            this.AppSettingTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ModuleList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button PreviewUpdate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabPage AppSetting;
        private System.Windows.Forms.TabControl AppSettingTab;
        private System.Windows.Forms.TabControl ItemsTab;
        private System.Windows.Forms.TabPage NewItems;
        private System.Windows.Forms.CheckBox NewItemsSelectAllCB;
        private System.Windows.Forms.DataGridView NewItemsView;
        private System.Windows.Forms.Button btnListUpdate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TargetValue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.ComboBox drpApplication;
        private System.Windows.Forms.Label label1;
    }
}