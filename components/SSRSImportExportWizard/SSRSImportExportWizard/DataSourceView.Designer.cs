﻿namespace SSRSImportExportWizard
{
    partial class DataSourceView
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DataSourceGridView = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DataSourceConnectionView = new System.Windows.Forms.DataGridView();
            this.btnReplaceDataSource = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ReplaceStringDGV = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reportFullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataSourceNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connectionStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extensionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataSourceItemModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblUpdateProgress = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceConnectionView)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReplaceStringDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSourceItemModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DataSourceGridView);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1066, 239);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Sources";
            // 
            // DataSourceGridView
            // 
            this.DataSourceGridView.AllowUserToAddRows = false;
            this.DataSourceGridView.AllowUserToDeleteRows = false;
            this.DataSourceGridView.AutoGenerateColumns = false;
            this.DataSourceGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataSourceGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.reportFullNameDataGridViewTextBoxColumn,
            this.dataSourceNameDataGridViewTextBoxColumn,
            this.connectionStringDataGridViewTextBoxColumn,
            this.extensionDataGridViewTextBoxColumn});
            this.DataSourceGridView.DataSource = this.dataSourceItemModelBindingSource;
            this.DataSourceGridView.Location = new System.Drawing.Point(6, 25);
            this.DataSourceGridView.Name = "DataSourceGridView";
            this.DataSourceGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.DataSourceGridView.RowTemplate.Height = 24;
            this.DataSourceGridView.Size = new System.Drawing.Size(1039, 204);
            this.DataSourceGridView.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DataSourceConnectionView);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 257);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1066, 189);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connections";
            // 
            // DataSourceConnectionView
            // 
            this.DataSourceConnectionView.AllowUserToDeleteRows = false;
            this.DataSourceConnectionView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataSourceConnectionView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CurrentValue});
            this.DataSourceConnectionView.Location = new System.Drawing.Point(6, 25);
            this.DataSourceConnectionView.Name = "DataSourceConnectionView";
            this.DataSourceConnectionView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.DataSourceConnectionView.RowTemplate.Height = 24;
            this.DataSourceConnectionView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DataSourceConnectionView.Size = new System.Drawing.Size(1039, 159);
            this.DataSourceConnectionView.TabIndex = 0;
            // 
            // btnReplaceDataSource
            // 
            this.btnReplaceDataSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReplaceDataSource.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReplaceDataSource.Location = new System.Drawing.Point(942, 622);
            this.btnReplaceDataSource.Name = "btnReplaceDataSource";
            this.btnReplaceDataSource.Size = new System.Drawing.Size(136, 38);
            this.btnReplaceDataSource.TabIndex = 13;
            this.btnReplaceDataSource.Text = "Update";
            this.btnReplaceDataSource.UseVisualStyleBackColor = true;
            this.btnReplaceDataSource.Click += new System.EventHandler(this.btnReplaceDataSource_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ReplaceStringDGV);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 452);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1066, 145);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Replacement Strings";
            // 
            // ReplaceStringDGV
            // 
            this.ReplaceStringDGV.AllowUserToDeleteRows = false;
            this.ReplaceStringDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ReplaceStringDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.ReplaceStringDGV.Location = new System.Drawing.Point(6, 25);
            this.ReplaceStringDGV.Name = "ReplaceStringDGV";
            this.ReplaceStringDGV.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.ReplaceStringDGV.RowTemplate.Height = 24;
            this.ReplaceStringDGV.Size = new System.Drawing.Size(1039, 114);
            this.ReplaceStringDGV.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(12, 626);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(136, 38);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "CurrentValue";
            this.dataGridViewTextBoxColumn1.HeaderText = "Current Value";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 350;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "NewValue";
            this.dataGridViewTextBoxColumn2.HeaderText = "New Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 350;
            // 
            // CurrentValue
            // 
            this.CurrentValue.DataPropertyName = "CurrentValue";
            this.CurrentValue.HeaderText = "Connection";
            this.CurrentValue.Name = "CurrentValue";
            this.CurrentValue.Width = 800;
            // 
            // reportFullNameDataGridViewTextBoxColumn
            // 
            this.reportFullNameDataGridViewTextBoxColumn.DataPropertyName = "ReportFullName";
            this.reportFullNameDataGridViewTextBoxColumn.HeaderText = "Report Full Name";
            this.reportFullNameDataGridViewTextBoxColumn.Name = "reportFullNameDataGridViewTextBoxColumn";
            this.reportFullNameDataGridViewTextBoxColumn.Width = 500;
            // 
            // dataSourceNameDataGridViewTextBoxColumn
            // 
            this.dataSourceNameDataGridViewTextBoxColumn.DataPropertyName = "DataSourceName";
            this.dataSourceNameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.dataSourceNameDataGridViewTextBoxColumn.Name = "dataSourceNameDataGridViewTextBoxColumn";
            this.dataSourceNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.dataSourceNameDataGridViewTextBoxColumn.Visible = false;
            // 
            // connectionStringDataGridViewTextBoxColumn
            // 
            this.connectionStringDataGridViewTextBoxColumn.DataPropertyName = "ConnectionString";
            this.connectionStringDataGridViewTextBoxColumn.HeaderText = "Connection String";
            this.connectionStringDataGridViewTextBoxColumn.Name = "connectionStringDataGridViewTextBoxColumn";
            this.connectionStringDataGridViewTextBoxColumn.Width = 300;
            // 
            // extensionDataGridViewTextBoxColumn
            // 
            this.extensionDataGridViewTextBoxColumn.DataPropertyName = "Extension";
            this.extensionDataGridViewTextBoxColumn.HeaderText = "Extension";
            this.extensionDataGridViewTextBoxColumn.Name = "extensionDataGridViewTextBoxColumn";
            this.extensionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataSourceItemModelBindingSource
            // 
            this.dataSourceItemModelBindingSource.DataSource = typeof(SSRSImportExportWizard.Models.DataSourceItemModel);
            // 
            // lblUpdateProgress
            // 
            this.lblUpdateProgress.AutoSize = true;
            this.lblUpdateProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateProgress.ForeColor = System.Drawing.Color.Green;
            this.lblUpdateProgress.Location = new System.Drawing.Point(15, 603);
            this.lblUpdateProgress.Name = "lblUpdateProgress";
            this.lblUpdateProgress.Size = new System.Drawing.Size(0, 17);
            this.lblUpdateProgress.TabIndex = 18;
            // 
            // DataSourceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 672);
            this.Controls.Add(this.lblUpdateProgress);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnReplaceDataSource);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "DataSourceView";
            this.Text = "View Data Source";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataSourceConnectionView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ReplaceStringDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSourceItemModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.BindingSource dataSourceItemModelBindingSource;
        private System.Windows.Forms.DataGridView DataSourceGridView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView DataSourceConnectionView;
        private System.Windows.Forms.Button btnReplaceDataSource;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView ReplaceStringDGV;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn reportFullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataSourceNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn connectionStringDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn extensionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentValue;
        private System.Windows.Forms.Label lblUpdateProgress;
    }
}