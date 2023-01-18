namespace QueryLoggingReconciliationUtility.GUI.Forms
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
            this.groupBoxAssembly = new System.Windows.Forms.GroupBox();
            this.buttonLoadServiceContracts = new System.Windows.Forms.Button();
            this.labelChooseAssembly = new System.Windows.Forms.Label();
            this.buttonBrowseForAssembly = new System.Windows.Forms.Button();
            this.textBoxAssemblyPath = new System.Windows.Forms.TextBox();
            this.openFileDialogAssembly = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxServiceContract = new System.Windows.Forms.GroupBox();
            this.labelSelectedServiceContractValue = new System.Windows.Forms.Label();
            this.labelSelectedServiceContractLabel = new System.Windows.Forms.Label();
            this.comboBoxServiceContracts = new System.Windows.Forms.ComboBox();
            this.dgvServiceOperations = new System.Windows.Forms.DataGridView();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ServiceOperationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCreateQueryLoggingDataListEntry = new System.Windows.Forms.Button();
            this.labelTotalItemsLabel = new System.Windows.Forms.Label();
            this.labelTotalItemsValue = new System.Windows.Forms.Label();
            this.labelItemsHavingEntryValue = new System.Windows.Forms.Label();
            this.labelItemsHavingEntryLabel = new System.Windows.Forms.Label();
            this.labelItemsWithoutEntryValue = new System.Windows.Forms.Label();
            this.labelItemsWithoutEntryLabel = new System.Windows.Forms.Label();
            this.labelLoggedInUserLabel = new System.Windows.Forms.Label();
            this.labelLoggedInUserValue = new System.Windows.Forms.Label();
            this.labelTotalOperationsValue = new System.Windows.Forms.Label();
            this.labelTotalOperationsLabel = new System.Windows.Forms.Label();
            this.checkboxEndsWithQueryService = new System.Windows.Forms.CheckBox();
            this.groupBoxAssembly.SuspendLayout();
            this.groupBoxServiceContract.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiceOperations)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxAssembly
            // 
            this.groupBoxAssembly.Controls.Add(this.checkboxEndsWithQueryService);
            this.groupBoxAssembly.Controls.Add(this.buttonLoadServiceContracts);
            this.groupBoxAssembly.Controls.Add(this.labelChooseAssembly);
            this.groupBoxAssembly.Controls.Add(this.buttonBrowseForAssembly);
            this.groupBoxAssembly.Controls.Add(this.textBoxAssemblyPath);
            this.groupBoxAssembly.Location = new System.Drawing.Point(13, 13);
            this.groupBoxAssembly.Name = "groupBoxAssembly";
            this.groupBoxAssembly.Size = new System.Drawing.Size(1245, 172);
            this.groupBoxAssembly.TabIndex = 0;
            this.groupBoxAssembly.TabStop = false;
            this.groupBoxAssembly.Text = "Assembly";
            // 
            // buttonLoadServiceContracts
            // 
            this.buttonLoadServiceContracts.Location = new System.Drawing.Point(6, 123);
            this.buttonLoadServiceContracts.Name = "buttonLoadServiceContracts";
            this.buttonLoadServiceContracts.Size = new System.Drawing.Size(208, 29);
            this.buttonLoadServiceContracts.TabIndex = 0;
            this.buttonLoadServiceContracts.Text = "Load Service Contracts";
            this.buttonLoadServiceContracts.UseVisualStyleBackColor = true;
            this.buttonLoadServiceContracts.Click += new System.EventHandler(this.buttonLoadServiceContracts_Click);
            // 
            // labelChooseAssembly
            // 
            this.labelChooseAssembly.AutoSize = true;
            this.labelChooseAssembly.Location = new System.Drawing.Point(7, 22);
            this.labelChooseAssembly.Name = "labelChooseAssembly";
            this.labelChooseAssembly.Size = new System.Drawing.Size(267, 17);
            this.labelChooseAssembly.TabIndex = 2;
            this.labelChooseAssembly.Text = "Browse and pick the BAS Assembly Path:";
            // 
            // buttonBrowseForAssembly
            // 
            this.buttonBrowseForAssembly.Location = new System.Drawing.Point(1089, 46);
            this.buttonBrowseForAssembly.Name = "buttonBrowseForAssembly";
            this.buttonBrowseForAssembly.Size = new System.Drawing.Size(150, 29);
            this.buttonBrowseForAssembly.TabIndex = 1;
            this.buttonBrowseForAssembly.Text = "Browse";
            this.buttonBrowseForAssembly.UseVisualStyleBackColor = true;
            this.buttonBrowseForAssembly.Click += new System.EventHandler(this.buttonBrowseForAssembly_Click);
            // 
            // textBoxAssemblyPath
            // 
            this.textBoxAssemblyPath.Location = new System.Drawing.Point(7, 49);
            this.textBoxAssemblyPath.Name = "textBoxAssemblyPath";
            this.textBoxAssemblyPath.ReadOnly = true;
            this.textBoxAssemblyPath.Size = new System.Drawing.Size(1076, 22);
            this.textBoxAssemblyPath.TabIndex = 0;
            // 
            // openFileDialogAssembly
            // 
            this.openFileDialogAssembly.FileName = "openFileDialogAssembly";
            // 
            // groupBoxServiceContract
            // 
            this.groupBoxServiceContract.Controls.Add(this.labelSelectedServiceContractValue);
            this.groupBoxServiceContract.Controls.Add(this.labelSelectedServiceContractLabel);
            this.groupBoxServiceContract.Controls.Add(this.comboBoxServiceContracts);
            this.groupBoxServiceContract.Location = new System.Drawing.Point(13, 191);
            this.groupBoxServiceContract.Name = "groupBoxServiceContract";
            this.groupBoxServiceContract.Size = new System.Drawing.Size(1245, 100);
            this.groupBoxServiceContract.TabIndex = 1;
            this.groupBoxServiceContract.TabStop = false;
            this.groupBoxServiceContract.Text = "Service Contract";
            // 
            // labelSelectedServiceContractValue
            // 
            this.labelSelectedServiceContractValue.AutoSize = true;
            this.labelSelectedServiceContractValue.Location = new System.Drawing.Point(190, 61);
            this.labelSelectedServiceContractValue.Name = "labelSelectedServiceContractValue";
            this.labelSelectedServiceContractValue.Size = new System.Drawing.Size(0, 17);
            this.labelSelectedServiceContractValue.TabIndex = 2;
            // 
            // labelSelectedServiceContractLabel
            // 
            this.labelSelectedServiceContractLabel.AutoSize = true;
            this.labelSelectedServiceContractLabel.Location = new System.Drawing.Point(5, 61);
            this.labelSelectedServiceContractLabel.Name = "labelSelectedServiceContractLabel";
            this.labelSelectedServiceContractLabel.Size = new System.Drawing.Size(171, 17);
            this.labelSelectedServiceContractLabel.TabIndex = 1;
            this.labelSelectedServiceContractLabel.Text = "Selected Service Contract";
            // 
            // comboBoxServiceContracts
            // 
            this.comboBoxServiceContracts.FormattingEnabled = true;
            this.comboBoxServiceContracts.Location = new System.Drawing.Point(7, 22);
            this.comboBoxServiceContracts.Name = "comboBoxServiceContracts";
            this.comboBoxServiceContracts.Size = new System.Drawing.Size(308, 24);
            this.comboBoxServiceContracts.TabIndex = 0;
            this.comboBoxServiceContracts.SelectedIndexChanged += new System.EventHandler(this.comboBoxServiceContracts_SelectedIndexChanged);
            // 
            // dgvServiceOperations
            // 
            this.dgvServiceOperations.AllowUserToAddRows = false;
            this.dgvServiceOperations.AllowUserToDeleteRows = false;
            this.dgvServiceOperations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServiceOperations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.ServiceOperationName,
            this.Status});
            this.dgvServiceOperations.Location = new System.Drawing.Point(13, 297);
            this.dgvServiceOperations.Name = "dgvServiceOperations";
            this.dgvServiceOperations.RowHeadersWidth = 51;
            this.dgvServiceOperations.RowTemplate.Height = 24;
            this.dgvServiceOperations.Size = new System.Drawing.Size(1246, 322);
            this.dgvServiceOperations.TabIndex = 2;
            // 
            // Selected
            // 
            this.Selected.Frozen = true;
            this.Selected.HeaderText = "Selected";
            this.Selected.MinimumWidth = 6;
            this.Selected.Name = "Selected";
            this.Selected.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Selected.Width = 125;
            // 
            // ServiceOperationName
            // 
            this.ServiceOperationName.Frozen = true;
            this.ServiceOperationName.HeaderText = "Service Operation Name";
            this.ServiceOperationName.MinimumWidth = 6;
            this.ServiceOperationName.Name = "ServiceOperationName";
            this.ServiceOperationName.Width = 600;
            // 
            // Status
            // 
            this.Status.Frozen = true;
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.Width = 300;
            // 
            // btnCreateQueryLoggingDataListEntry
            // 
            this.btnCreateQueryLoggingDataListEntry.Location = new System.Drawing.Point(999, 649);
            this.btnCreateQueryLoggingDataListEntry.Name = "btnCreateQueryLoggingDataListEntry";
            this.btnCreateQueryLoggingDataListEntry.Size = new System.Drawing.Size(253, 41);
            this.btnCreateQueryLoggingDataListEntry.TabIndex = 3;
            this.btnCreateQueryLoggingDataListEntry.Text = "Create Query Logging DL Entry";
            this.btnCreateQueryLoggingDataListEntry.UseVisualStyleBackColor = true;
            this.btnCreateQueryLoggingDataListEntry.Click += new System.EventHandler(this.btnCreateQueryLoggingDataListEntry_Click);
            // 
            // labelTotalItemsLabel
            // 
            this.labelTotalItemsLabel.AutoSize = true;
            this.labelTotalItemsLabel.Location = new System.Drawing.Point(10, 654);
            this.labelTotalItemsLabel.Name = "labelTotalItemsLabel";
            this.labelTotalItemsLabel.Size = new System.Drawing.Size(81, 17);
            this.labelTotalItemsLabel.TabIndex = 4;
            this.labelTotalItemsLabel.Text = "Total Items:";
            // 
            // labelTotalItemsValue
            // 
            this.labelTotalItemsValue.AutoSize = true;
            this.labelTotalItemsValue.Location = new System.Drawing.Point(97, 661);
            this.labelTotalItemsValue.Name = "labelTotalItemsValue";
            this.labelTotalItemsValue.Size = new System.Drawing.Size(0, 17);
            this.labelTotalItemsValue.TabIndex = 5;
            // 
            // labelItemsHavingEntryValue
            // 
            this.labelItemsHavingEntryValue.AutoSize = true;
            this.labelItemsHavingEntryValue.Location = new System.Drawing.Point(147, 682);
            this.labelItemsHavingEntryValue.Name = "labelItemsHavingEntryValue";
            this.labelItemsHavingEntryValue.Size = new System.Drawing.Size(0, 17);
            this.labelItemsHavingEntryValue.TabIndex = 7;
            // 
            // labelItemsHavingEntryLabel
            // 
            this.labelItemsHavingEntryLabel.AutoSize = true;
            this.labelItemsHavingEntryLabel.Location = new System.Drawing.Point(10, 682);
            this.labelItemsHavingEntryLabel.Name = "labelItemsHavingEntryLabel";
            this.labelItemsHavingEntryLabel.Size = new System.Drawing.Size(130, 17);
            this.labelItemsHavingEntryLabel.TabIndex = 6;
            this.labelItemsHavingEntryLabel.Text = "Items Having Entry:";
            // 
            // labelItemsWithoutEntryValue
            // 
            this.labelItemsWithoutEntryValue.AutoSize = true;
            this.labelItemsWithoutEntryValue.Location = new System.Drawing.Point(153, 709);
            this.labelItemsWithoutEntryValue.Name = "labelItemsWithoutEntryValue";
            this.labelItemsWithoutEntryValue.Size = new System.Drawing.Size(0, 17);
            this.labelItemsWithoutEntryValue.TabIndex = 9;
            // 
            // labelItemsWithoutEntryLabel
            // 
            this.labelItemsWithoutEntryLabel.AutoSize = true;
            this.labelItemsWithoutEntryLabel.Location = new System.Drawing.Point(10, 709);
            this.labelItemsWithoutEntryLabel.Name = "labelItemsWithoutEntryLabel";
            this.labelItemsWithoutEntryLabel.Size = new System.Drawing.Size(134, 17);
            this.labelItemsWithoutEntryLabel.TabIndex = 8;
            this.labelItemsWithoutEntryLabel.Text = "Items Without Entry:";
            // 
            // labelLoggedInUserLabel
            // 
            this.labelLoggedInUserLabel.AutoSize = true;
            this.labelLoggedInUserLabel.Location = new System.Drawing.Point(1003, 622);
            this.labelLoggedInUserLabel.Name = "labelLoggedInUserLabel";
            this.labelLoggedInUserLabel.Size = new System.Drawing.Size(109, 17);
            this.labelLoggedInUserLabel.TabIndex = 10;
            this.labelLoggedInUserLabel.Text = "Logged in User:";
            // 
            // labelLoggedInUserValue
            // 
            this.labelLoggedInUserValue.AutoSize = true;
            this.labelLoggedInUserValue.Location = new System.Drawing.Point(1118, 622);
            this.labelLoggedInUserValue.Name = "labelLoggedInUserValue";
            this.labelLoggedInUserValue.Size = new System.Drawing.Size(46, 17);
            this.labelLoggedInUserValue.TabIndex = 11;
            this.labelLoggedInUserValue.Text = "label1";
            // 
            // labelTotalOperationsValue
            // 
            this.labelTotalOperationsValue.AutoSize = true;
            this.labelTotalOperationsValue.Location = new System.Drawing.Point(134, 632);
            this.labelTotalOperationsValue.Name = "labelTotalOperationsValue";
            this.labelTotalOperationsValue.Size = new System.Drawing.Size(0, 17);
            this.labelTotalOperationsValue.TabIndex = 13;
            // 
            // labelTotalOperationsLabel
            // 
            this.labelTotalOperationsLabel.AutoSize = true;
            this.labelTotalOperationsLabel.Location = new System.Drawing.Point(10, 632);
            this.labelTotalOperationsLabel.Name = "labelTotalOperationsLabel";
            this.labelTotalOperationsLabel.Size = new System.Drawing.Size(118, 17);
            this.labelTotalOperationsLabel.TabIndex = 12;
            this.labelTotalOperationsLabel.Text = "Total Operations:";
            // 
            // checkboxEndsWithQueryService
            // 
            this.checkboxEndsWithQueryService.AutoSize = true;
            this.checkboxEndsWithQueryService.Location = new System.Drawing.Point(8, 86);
            this.checkboxEndsWithQueryService.Name = "checkboxEndsWithQueryService";
            this.checkboxEndsWithQueryService.Size = new System.Drawing.Size(330, 21);
            this.checkboxEndsWithQueryService.TabIndex = 3;
            this.checkboxEndsWithQueryService.Text = "Load only Types which end with \"QueryService\"";
            this.checkboxEndsWithQueryService.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 738);
            this.Controls.Add(this.labelTotalOperationsValue);
            this.Controls.Add(this.labelTotalOperationsLabel);
            this.Controls.Add(this.labelLoggedInUserValue);
            this.Controls.Add(this.labelLoggedInUserLabel);
            this.Controls.Add(this.labelItemsWithoutEntryValue);
            this.Controls.Add(this.labelItemsWithoutEntryLabel);
            this.Controls.Add(this.labelItemsHavingEntryValue);
            this.Controls.Add(this.labelItemsHavingEntryLabel);
            this.Controls.Add(this.labelTotalItemsValue);
            this.Controls.Add(this.labelTotalItemsLabel);
            this.Controls.Add(this.btnCreateQueryLoggingDataListEntry);
            this.Controls.Add(this.dgvServiceOperations);
            this.Controls.Add(this.groupBoxServiceContract);
            this.Controls.Add(this.groupBoxAssembly);
            this.Name = "MainForm";
            this.Text = "Query Logging Reconciliation Utility";
            this.groupBoxAssembly.ResumeLayout(false);
            this.groupBoxAssembly.PerformLayout();
            this.groupBoxServiceContract.ResumeLayout(false);
            this.groupBoxServiceContract.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiceOperations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAssembly;
        private System.Windows.Forms.Label labelChooseAssembly;
        private System.Windows.Forms.Button buttonBrowseForAssembly;
        private System.Windows.Forms.TextBox textBoxAssemblyPath;
        private System.Windows.Forms.OpenFileDialog openFileDialogAssembly;
        private System.Windows.Forms.GroupBox groupBoxServiceContract;
        private System.Windows.Forms.Button buttonLoadServiceContracts;
        private System.Windows.Forms.ComboBox comboBoxServiceContracts;
        private System.Windows.Forms.Label labelSelectedServiceContractValue;
        private System.Windows.Forms.Label labelSelectedServiceContractLabel;
        private System.Windows.Forms.DataGridView dgvServiceOperations;
        private System.Windows.Forms.Button btnCreateQueryLoggingDataListEntry;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceOperationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Label labelTotalItemsLabel;
        private System.Windows.Forms.Label labelTotalItemsValue;
        private System.Windows.Forms.Label labelItemsHavingEntryValue;
        private System.Windows.Forms.Label labelItemsHavingEntryLabel;
        private System.Windows.Forms.Label labelItemsWithoutEntryValue;
        private System.Windows.Forms.Label labelItemsWithoutEntryLabel;
        private System.Windows.Forms.Label labelLoggedInUserLabel;
        private System.Windows.Forms.Label labelLoggedInUserValue;
        private System.Windows.Forms.Label labelTotalOperationsValue;
        private System.Windows.Forms.Label labelTotalOperationsLabel;
        private System.Windows.Forms.CheckBox checkboxEndsWithQueryService;
    }
}