namespace UXWarmUpParamBuilder
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewModuleParam = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.textBoxActionPath = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnActionBrw = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.ParamType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RouteUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JsonParameters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.PerformTest = new System.Windows.Forms.DataGridViewImageColumn();
            this.Result = new System.Windows.Forms.DataGridViewImageColumn();
            this.Status = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModuleParam)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewModuleParam
            // 
            this.dataGridViewModuleParam.AllowUserToAddRows = false;
            this.dataGridViewModuleParam.AllowUserToDeleteRows = false;
            this.dataGridViewModuleParam.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewModuleParam.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewModuleParam.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewModuleParam.ColumnHeadersHeight = 50;
            this.dataGridViewModuleParam.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParamType,
            this.RouteUrl,
            this.JsonParameters,
            this.Edit,
            this.PerformTest,
            this.Result,
            this.Status});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewModuleParam.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewModuleParam.EnableHeadersVisualStyles = false;
            this.dataGridViewModuleParam.Location = new System.Drawing.Point(12, 68);
            this.dataGridViewModuleParam.Name = "dataGridViewModuleParam";
            this.dataGridViewModuleParam.RowHeadersVisible = false;
            this.dataGridViewModuleParam.RowHeadersWidth = 62;
            this.dataGridViewModuleParam.RowTemplate.Height = 28;
            this.dataGridViewModuleParam.Size = new System.Drawing.Size(1621, 497);
            this.dataGridViewModuleParam.TabIndex = 0;
            this.dataGridViewModuleParam.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewModuleParam_buttonCol);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(353, 34);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(388, 33);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(102, 41);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(31, 22);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1672, 671);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(210)))), ((int)(((byte)(245)))));
            this.tabPage1.Controls.Add(this.btnExport);
            this.tabPage1.Controls.Add(this.btnAddNew);
            this.tabPage1.Controls.Add(this.textBoxActionPath);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.btnActionBrw);
            this.tabPage1.Controls.Add(this.dataGridViewModuleParam);
            this.tabPage1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 37);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1664, 630);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Action Parameter";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.AutoSize = true;
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.btnExport.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnExport.Location = new System.Drawing.Point(1462, 573);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(171, 50);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export to CSV";
            this.btnExport.UseVisualStyleBackColor = false;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddNew.AutoSize = true;
            this.btnAddNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.btnAddNew.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAddNew.Location = new System.Drawing.Point(14, 573);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(171, 50);
            this.btnAddNew.TabIndex = 4;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = false;
            // 
            // textBoxActionPath
            // 
            this.textBoxActionPath.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxActionPath.Location = new System.Drawing.Point(12, 16);
            this.textBoxActionPath.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxActionPath.Multiline = true;
            this.textBoxActionPath.Name = "textBoxActionPath";
            this.textBoxActionPath.Size = new System.Drawing.Size(436, 45);
            this.textBoxActionPath.TabIndex = 3;
            this.textBoxActionPath.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.AutoSize = true;
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.Location = new System.Drawing.Point(1461, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(171, 50);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // btnActionBrw
            // 
            this.btnActionBrw.AutoSize = true;
            this.btnActionBrw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.btnActionBrw.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnActionBrw.Location = new System.Drawing.Point(461, 12);
            this.btnActionBrw.Name = "btnActionBrw";
            this.btnActionBrw.Size = new System.Drawing.Size(171, 50);
            this.btnActionBrw.TabIndex = 1;
            this.btnActionBrw.Text = "Browse";
            this.btnActionBrw.UseVisualStyleBackColor = false;
            this.btnActionBrw.Click += new System.EventHandler(this.btnActionBrw_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.btnBrowse);
            this.tabPage2.Location = new System.Drawing.Point(4, 37);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1217, 630);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Replacement Values";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.tabControl1);
            this.panel4.Location = new System.Drawing.Point(-3, 1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1706, 725);
            this.panel4.TabIndex = 7;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewImageColumn1.HeaderText = "Edit";
            this.dataGridViewImageColumn1.Image = global::UXWarmUpParamBuilder.Properties.Resources.Edit;
            this.dataGridViewImageColumn1.MinimumWidth = 8;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Width = 82;
            // 
            // ParamType
            // 
            this.ParamType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ParamType.HeaderText = "Parameter Type";
            this.ParamType.MinimumWidth = 8;
            this.ParamType.Name = "ParamType";
            this.ParamType.Width = 189;
            // 
            // RouteUrl
            // 
            this.RouteUrl.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.RouteUrl.HeaderText = "Route Url";
            this.RouteUrl.MinimumWidth = 8;
            this.RouteUrl.Name = "RouteUrl";
            this.RouteUrl.Width = 210;
            // 
            // JsonParameters
            // 
            this.JsonParameters.HeaderText = "Json Parameters";
            this.JsonParameters.MinimumWidth = 8;
            this.JsonParameters.Name = "JsonParameters";
            this.JsonParameters.Width = 700;
            // 
            // Edit
            // 
            this.Edit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = global::UXWarmUpParamBuilder.Properties.Resources.Edit;
            this.Edit.MinimumWidth = 8;
            this.Edit.Name = "Edit";
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Edit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Edit.Width = 82;
            // 
            // PerformTest
            // 
            this.PerformTest.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PerformTest.HeaderText = "Test";
            this.PerformTest.MinimumWidth = 8;
            this.PerformTest.Name = "PerformTest";
            this.PerformTest.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PerformTest.Width = 54;
            // 
            // Result
            // 
            this.Result.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Result.HeaderText = "Result";
            this.Result.MinimumWidth = 8;
            this.Result.Name = "Result";
            this.Result.Width = 73;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Status.HeaderText = "";
            this.Status.MinimumWidth = 8;
            this.Status.Name = "Status";
            this.Status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Status.Width = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(210)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1694, 749);
            this.Controls.Add(this.panel4);
            this.Name = "Form1";
            this.Text = "UX WarmUp";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModuleParam)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewModuleParam;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnActionBrw;
        private System.Windows.Forms.TextBox textBoxActionPath;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamType;
        private System.Windows.Forms.DataGridViewTextBoxColumn RouteUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn JsonParameters;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn PerformTest;
        private System.Windows.Forms.DataGridViewImageColumn Result;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Status;
    }
}

