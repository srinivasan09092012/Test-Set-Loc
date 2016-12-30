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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DataListView = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ContentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.DataListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Age)).BeginInit();
            this.SuspendLayout();
            // 
            // DataListView
            // 
            this.DataListView.AllowUserToAddRows = false;
            this.DataListView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataListView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.DataListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataListView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.ContentID,
            this.IsActive});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataListView.DefaultCellStyle = dataGridViewCellStyle14;
            this.DataListView.Location = new System.Drawing.Point(15, 135);
            this.DataListView.Name = "DataListView";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataListView.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.DataListView.RowTemplate.Height = 24;
            this.DataListView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DataListView.Size = new System.Drawing.Size(981, 540);
            this.DataListView.TabIndex = 2;
            this.DataListView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataListView_CellDoubleClick);
            this.DataListView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataListView_CellValueChanged);
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
            this.ContentID.Width = 850;
            // 
            // IsActive
            // 
            this.IsActive.DataPropertyName = "IsActive";
            this.IsActive.HeaderText = "Active";
            this.IsActive.Name = "IsActive";
            this.IsActive.ReadOnly = true;
            this.IsActive.Width = 60;
            // 
            // DataListLoad
            // 
            this.DataListLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataListLoad.Location = new System.Drawing.Point(921, 97);
            this.DataListLoad.Name = "DataListLoad";
            this.DataListLoad.Size = new System.Drawing.Size(75, 34);
            this.DataListLoad.TabIndex = 4;
            this.DataListLoad.Text = "Load";
            this.DataListLoad.UseVisualStyleBackColor = true;
            this.DataListLoad.Click += new System.EventHandler(this.DataListLoad_Click);
            // 
            // Start
            // 
            this.Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start.Location = new System.Drawing.Point(15, 685);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(173, 34);
            this.Start.TabIndex = 5;
            this.Start.Text = "Generate Script";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // SelectAllChkBox
            // 
            this.SelectAllChkBox.AutoSize = true;
            this.SelectAllChkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectAllChkBox.Location = new System.Drawing.Point(61, 108);
            this.SelectAllChkBox.Name = "SelectAllChkBox";
            this.SelectAllChkBox.Size = new System.Drawing.Size(98, 21);
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
            this.ModuleList.ItemHeight = 16;
            this.ModuleList.Location = new System.Drawing.Point(77, 12);
            this.ModuleList.Name = "ModuleList";
            this.ModuleList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ModuleList.Size = new System.Drawing.Size(318, 84);
            this.ModuleList.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Module:";
            // 
            // Close
            // 
            this.Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close.Location = new System.Drawing.Point(907, 685);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(90, 34);
            this.Close.TabIndex = 14;
            this.Close.Text = "Close";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // Preview
            // 
            this.Preview.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Preview.Location = new System.Drawing.Point(194, 685);
            this.Preview.Name = "Preview";
            this.Preview.Size = new System.Drawing.Size(90, 34);
            this.Preview.TabIndex = 15;
            this.Preview.Text = "Preview";
            this.Preview.UseVisualStyleBackColor = true;
            this.Preview.Click += new System.EventHandler(this.Preview_Click);
            // 
            // Clear
            // 
            this.Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clear.Location = new System.Drawing.Point(290, 685);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(90, 34);
            this.Clear.TabIndex = 16;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(401, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Age (in days):";
            // 
            // Age
            // 
            this.Age.Location = new System.Drawing.Point(505, 13);
            this.Age.Name = "Age";
            this.Age.Size = new System.Drawing.Size(64, 22);
            this.Age.TabIndex = 18;
            // 
            // BtnClearCache
            // 
            this.BtnClearCache.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearCache.Location = new System.Drawing.Point(886, 1);
            this.BtnClearCache.Name = "BtnClearCache";
            this.BtnClearCache.Size = new System.Drawing.Size(110, 34);
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
            this.InactiveCB.Location = new System.Drawing.Point(401, 54);
            this.InactiveCB.Name = "InactiveCB";
            this.InactiveCB.Size = new System.Drawing.Size(86, 21);
            this.InactiveCB.TabIndex = 13;
            this.InactiveCB.Text = "Inactive";
            this.InactiveCB.UseVisualStyleBackColor = true;
            // 
            // DataListSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 728);
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
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContentID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.CheckBox SelectAllChkBox;
        private System.Windows.Forms.ListBox ModuleList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.Button Preview;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown Age;
        private System.Windows.Forms.Button BtnClearCache;
        private System.Windows.Forms.CheckBox InactiveCB;
    }
}