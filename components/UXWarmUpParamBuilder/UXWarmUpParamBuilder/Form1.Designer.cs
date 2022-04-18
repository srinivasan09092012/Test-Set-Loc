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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewModuleParam = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.comboBoxEnv = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.textBoxActionPath = new System.Windows.Forms.TextBox();
            this.btnActionBrw = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonAddToken = new System.Windows.Forms.Button();
            this.dataGridViewToken = new System.Windows.Forms.DataGridView();
            this.TokenKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TokenValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EditToken = new System.Windows.Forms.DataGridViewImageColumn();
            this.Copy = new System.Windows.Forms.DataGridViewImageColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxRepBrw = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewToken)).BeginInit();
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
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewModuleParam.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
            this.dataGridViewModuleParam.ColumnHeadersHeight = 50;
            this.dataGridViewModuleParam.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.ModuleName,
            this.ParamType,
            this.RouteUrl,
            this.JsonParameters,
            this.Edit,
            this.PerformTest,
            this.Result,
            this.Status});
            this.dataGridViewModuleParam.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewModuleParam.DefaultCellStyle = dataGridViewCellStyle26;
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
            this.tabPage1.Controls.Add(this.comboBoxEnv);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnExport);
            this.tabPage1.Controls.Add(this.btnAddNew);
            this.tabPage1.Controls.Add(this.textBoxActionPath);
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
            // comboBoxEnv
            // 
            this.comboBoxEnv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEnv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxEnv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxEnv.FormattingEnabled = true;
            this.comboBoxEnv.Location = new System.Drawing.Point(1390, 20);
            this.comboBoxEnv.Name = "comboBoxEnv";
            this.comboBoxEnv.Size = new System.Drawing.Size(243, 36);
            this.comboBoxEnv.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1258, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 28);
            this.label1.TabIndex = 6;
            this.label1.Text = "Environment";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.AutoSize = true;
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnExport.Location = new System.Drawing.Point(1462, 573);
            this.btnExport.Margin = new System.Windows.Forms.Padding(0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(171, 50);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export to CSV";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddNew.AutoSize = true;
            this.btnAddNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.btnAddNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNew.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAddNew.Location = new System.Drawing.Point(14, 573);
            this.btnAddNew.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(171, 50);
            this.btnAddNew.TabIndex = 4;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = false;
            this.btnAddNew.Click += new System.EventHandler(this.buttonAddNew_Click);
            // 
            // textBoxActionPath
            // 
            this.textBoxActionPath.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxActionPath.Location = new System.Drawing.Point(12, 8);
            this.textBoxActionPath.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxActionPath.Multiline = true;
            this.textBoxActionPath.Name = "textBoxActionPath";
            this.textBoxActionPath.Size = new System.Drawing.Size(460, 52);
            this.textBoxActionPath.TabIndex = 3;
            this.textBoxActionPath.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // btnActionBrw
            // 
            this.btnActionBrw.AutoSize = true;
            this.btnActionBrw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.btnActionBrw.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActionBrw.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnActionBrw.Location = new System.Drawing.Point(480, 11);
            this.btnActionBrw.Name = "btnActionBrw";
            this.btnActionBrw.Size = new System.Drawing.Size(171, 50);
            this.btnActionBrw.TabIndex = 1;
            this.btnActionBrw.Text = "Browse";
            this.btnActionBrw.UseVisualStyleBackColor = false;
            this.btnActionBrw.Click += new System.EventHandler(this.btnActionBrw_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(210)))), ((int)(((byte)(245)))));
            this.tabPage2.Controls.Add(this.buttonAddToken);
            this.tabPage2.Controls.Add(this.dataGridViewToken);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.textBoxRepBrw);
            this.tabPage2.Location = new System.Drawing.Point(4, 37);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1664, 630);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Replacement Values";
            // 
            // buttonAddToken
            // 
            this.buttonAddToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddToken.AutoSize = true;
            this.buttonAddToken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.buttonAddToken.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAddToken.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonAddToken.Location = new System.Drawing.Point(14, 573);
            this.buttonAddToken.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAddToken.Name = "buttonAddToken";
            this.buttonAddToken.Size = new System.Drawing.Size(171, 50);
            this.buttonAddToken.TabIndex = 8;
            this.buttonAddToken.Text = "Add New";
            this.buttonAddToken.UseVisualStyleBackColor = false;
            this.buttonAddToken.Click += new System.EventHandler(this.buttonAddToken_Click);
            // 
            // dataGridViewToken
            // 
            this.dataGridViewToken.AllowUserToAddRows = false;
            this.dataGridViewToken.AllowUserToDeleteRows = false;
            this.dataGridViewToken.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewToken.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewToken.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewToken.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewToken.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle27;
            this.dataGridViewToken.ColumnHeadersHeight = 50;
            this.dataGridViewToken.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TokenKey,
            this.TokenValue,
            this.EditToken,
            this.Copy});
            this.dataGridViewToken.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewToken.DefaultCellStyle = dataGridViewCellStyle28;
            this.dataGridViewToken.EnableHeadersVisualStyles = false;
            this.dataGridViewToken.Location = new System.Drawing.Point(12, 68);
            this.dataGridViewToken.Name = "dataGridViewToken";
            this.dataGridViewToken.ReadOnly = true;
            this.dataGridViewToken.RowHeadersVisible = false;
            this.dataGridViewToken.RowHeadersWidth = 62;
            this.dataGridViewToken.RowTemplate.Height = 28;
            this.dataGridViewToken.Size = new System.Drawing.Size(1649, 497);
            this.dataGridViewToken.TabIndex = 8;
            this.dataGridViewToken.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewToken_buttonCol);
            // 
            // TokenKey
            // 
            this.TokenKey.HeaderText = "Token Key";
            this.TokenKey.MinimumWidth = 8;
            this.TokenKey.Name = "TokenKey";
            this.TokenKey.ReadOnly = true;
            // 
            // TokenValue
            // 
            this.TokenValue.HeaderText = "Token Value";
            this.TokenValue.MinimumWidth = 8;
            this.TokenValue.Name = "TokenValue";
            this.TokenValue.ReadOnly = true;
            // 
            // EditToken
            // 
            this.EditToken.HeaderText = "Edit";
            this.EditToken.MinimumWidth = 8;
            this.EditToken.Name = "EditToken";
            this.EditToken.ReadOnly = true;
            // 
            // Copy
            // 
            this.Copy.HeaderText = "Copy Key";
            this.Copy.MinimumWidth = 8;
            this.Copy.Name = "Copy";
            this.Copy.ReadOnly = true;
            this.Copy.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(480, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 50);
            this.button1.TabIndex = 8;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // textBoxRepBrw
            // 
            this.textBoxRepBrw.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRepBrw.Location = new System.Drawing.Point(12, 8);
            this.textBoxRepBrw.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxRepBrw.Multiline = true;
            this.textBoxRepBrw.Name = "textBoxRepBrw";
            this.textBoxRepBrw.Size = new System.Drawing.Size(460, 52);
            this.textBoxRepBrw.TabIndex = 8;
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
            this.dataGridViewImageColumn1.Width = 150;
            // 
            // Id
            // 
            this.Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 8;
            this.Id.Name = "Id";
            this.Id.Visible = false;
            this.Id.Width = 150;
            // 
            // ModuleName
            // 
            this.ModuleName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ModuleName.HeaderText = "ModuleName";
            this.ModuleName.MinimumWidth = 8;
            this.ModuleName.Name = "ModuleName";
            this.ModuleName.Visible = false;
            this.ModuleName.Width = 150;
            // 
            // ParamType
            // 
            this.ParamType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ParamType.HeaderText = "Parameter Type";
            this.ParamType.MinimumWidth = 8;
            this.ParamType.Name = "ParamType";
            this.ParamType.Width = 110;
            // 
            // RouteUrl
            // 
            this.RouteUrl.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.RouteUrl.HeaderText = "Route Url";
            this.RouteUrl.MinimumWidth = 8;
            this.RouteUrl.Name = "RouteUrl";
            this.RouteUrl.Width = 290;
            // 
            // JsonParameters
            // 
            this.JsonParameters.HeaderText = "Json Parameters";
            this.JsonParameters.MinimumWidth = 8;
            this.JsonParameters.Name = "JsonParameters";
            this.JsonParameters.Width = 650;
            // 
            // Edit
            // 
            this.Edit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Edit.HeaderText = "Edit";
            this.Edit.MinimumWidth = 8;
            this.Edit.Name = "Edit";
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Edit.Width = 52;
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewToken)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewModuleParam;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnActionBrw;
        private System.Windows.Forms.TextBox textBoxActionPath;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxEnv;
        private System.Windows.Forms.TextBox textBoxRepBrw;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridViewToken;
        private System.Windows.Forms.Button buttonAddToken;
        private System.Windows.Forms.DataGridViewTextBoxColumn TokenKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn TokenValue;
        private System.Windows.Forms.DataGridViewImageColumn EditToken;
        private System.Windows.Forms.DataGridViewImageColumn Copy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModuleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamType;
        private System.Windows.Forms.DataGridViewTextBoxColumn RouteUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn JsonParameters;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn PerformTest;
        private System.Windows.Forms.DataGridViewImageColumn Result;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Status;
    }
}

