namespace BASEventsTestingUtil
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
            this.buttonPressureTest = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonNormalTest = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.numericUpDownEventsNumbers = new System.Windows.Forms.NumericUpDown();
            this.labelProgess = new System.Windows.Forms.Label();
            this.tenantIds = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.allTabs.SuspendLayout();
            this.tabPayload.SuspendLayout();
            this.tbErrors.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEventsNumbers)).BeginInit();
            this.SuspendLayout();
            // 
            // tbFileName
            // 
            this.tbFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFileName.Location = new System.Drawing.Point(11, 21);
            this.tbFileName.Margin = new System.Windows.Forms.Padding(2);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.ReadOnly = true;
            this.tbFileName.Size = new System.Drawing.Size(1198, 29);
            this.tbFileName.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(1213, 18);
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
            this.label1.Size = new System.Drawing.Size(166, 24);
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
            this.allTabs.Size = new System.Drawing.Size(1299, 554);
            this.allTabs.TabIndex = 3;
            // 
            // tabPayload
            // 
            this.tabPayload.Controls.Add(this.wbXML);
            this.tabPayload.Location = new System.Drawing.Point(4, 34);
            this.tabPayload.Margin = new System.Windows.Forms.Padding(2);
            this.tabPayload.Name = "tabPayload";
            this.tabPayload.Padding = new System.Windows.Forms.Padding(2);
            this.tabPayload.Size = new System.Drawing.Size(1291, 516);
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
            this.wbXML.Name = "wbXML";
            this.wbXML.Size = new System.Drawing.Size(1289, 514);
            this.wbXML.TabIndex = 0;
            // 
            // tbErrors
            // 
            this.tbErrors.Controls.Add(this.tbError);
            this.tbErrors.Location = new System.Drawing.Point(4, 34);
            this.tbErrors.Name = "tbErrors";
            this.tbErrors.Padding = new System.Windows.Forms.Padding(3);
            this.tbErrors.Size = new System.Drawing.Size(1099, 516);
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
            this.tbError.Size = new System.Drawing.Size(1093, 510);
            this.tbError.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.DarkGray;
            this.tabPage1.Controls.Add(this.tbPayloadContent);
            this.tabPage1.Controls.Add(this.btnCancel);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1099, 516);
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
            this.tbPayloadContent.Size = new System.Drawing.Size(1095, 475);
            this.tbPayloadContent.TabIndex = 12;
            this.tbPayloadContent.Text = "";
            this.tbPayloadContent.WordWrap = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(965, 480);
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
            this.btnSave.Location = new System.Drawing.Point(1044, 480);
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
            this.cbEndpoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEndpoint.BackColor = System.Drawing.SystemColors.Window;
            this.cbEndpoint.DisplayMember = "Name";
            this.cbEndpoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEndpoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEndpoint.FormattingEnabled = true;
            this.cbEndpoint.Location = new System.Drawing.Point(158, 641);
            this.cbEndpoint.Margin = new System.Windows.Forms.Padding(2);
            this.cbEndpoint.Name = "cbEndpoint";
            this.cbEndpoint.Size = new System.Drawing.Size(1158, 32);
            this.cbEndpoint.TabIndex = 4;
            this.cbEndpoint.ValueMember = "Value";
            // 
            // buttonPressureTest
            // 
            this.buttonPressureTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPressureTest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonPressureTest.Enabled = false;
            this.buttonPressureTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPressureTest.Location = new System.Drawing.Point(1091, 675);
            this.buttonPressureTest.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPressureTest.Name = "buttonPressureTest";
            this.buttonPressureTest.Size = new System.Drawing.Size(166, 47);
            this.buttonPressureTest.TabIndex = 5;
            this.buttonPressureTest.Text = "Submit Multiple Events";
            this.buttonPressureTest.UseVisualStyleBackColor = true;
            this.buttonPressureTest.Click += new System.EventHandler(this.btnPresssureTest_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 645);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "Target Service: ";
            // 
            // buttonNormalTest
            // 
            this.buttonNormalTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNormalTest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonNormalTest.Enabled = false;
            this.buttonNormalTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNormalTest.Location = new System.Drawing.Point(251, 675);
            this.buttonNormalTest.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNormalTest.Name = "buttonNormalTest";
            this.buttonNormalTest.Size = new System.Drawing.Size(791, 50);
            this.buttonNormalTest.TabIndex = 5;
            this.buttonNormalTest.Text = "Submit Event";
            this.buttonNormalTest.UseVisualStyleBackColor = true;
            this.buttonNormalTest.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // numericUpDownEventsNumbers
            // 
            this.numericUpDownEventsNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownEventsNumbers.Location = new System.Drawing.Point(1262, 686);
            this.numericUpDownEventsNumbers.Name = "numericUpDownEventsNumbers";
            this.numericUpDownEventsNumbers.Size = new System.Drawing.Size(56, 28);
            this.numericUpDownEventsNumbers.TabIndex = 9;
            this.numericUpDownEventsNumbers.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownEventsNumbers.ValueChanged += new System.EventHandler(this.numericUpDownEventsNumbers_ValueChanged);
            // 
            // labelProgess
            // 
            this.labelProgess.AutoSize = true;
            this.labelProgess.Location = new System.Drawing.Point(603, 801);
            this.labelProgess.Name = "labelProgess";
            this.labelProgess.Size = new System.Drawing.Size(0, 24);
            this.labelProgess.TabIndex = 11;
            // 
            // tenantIds
            // 
            this.tenantIds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tenantIds.BackColor = System.Drawing.SystemColors.Window;
            this.tenantIds.DisplayMember = "Name";
            this.tenantIds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tenantIds.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenantIds.FormattingEnabled = true;
            this.tenantIds.Location = new System.Drawing.Point(158, 608);
            this.tenantIds.Margin = new System.Windows.Forms.Padding(2);
            this.tenantIds.Name = "tenantIds";
            this.tenantIds.Size = new System.Drawing.Size(624, 32);
            this.tenantIds.TabIndex = 12;
            this.tenantIds.ValueMember = "Value";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(48, 612);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 24);
            this.label3.TabIndex = 13;
            this.label3.Text = "Tenant ID:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1329, 736);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tenantIds);
            this.Controls.Add(this.labelProgess);
            this.Controls.Add(this.numericUpDownEventsNumbers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonNormalTest);
            this.Controls.Add(this.buttonPressureTest);
            this.Controls.Add(this.cbEndpoint);
            this.Controls.Add(this.allTabs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbFileName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Event Publishing Test Client";
            this.allTabs.ResumeLayout(false);
            this.tabPayload.ResumeLayout(false);
            this.tbErrors.ResumeLayout(false);
            this.tbErrors.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEventsNumbers)).EndInit();
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
        private System.Windows.Forms.Button buttonPressureTest;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.NumericUpDown numericUpDownEventsNumbers;
        private System.Windows.Forms.Label labelProgess;
        private System.Windows.Forms.ComboBox tenantIds;
        private System.Windows.Forms.Label label3;
    }
}

