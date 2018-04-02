namespace ProviderManagement.EnrollmentTestClient
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
            this.label3 = new System.Windows.Forms.Label();
            this.cbEventName = new System.Windows.Forms.ComboBox();
            this.buttonNormalTest = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.numericUpDownEventsNumbers = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.labelProgess = new System.Windows.Forms.Label();
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
            this.tbFileName.Size = new System.Drawing.Size(938, 33);
            this.tbFileName.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(953, 18);
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
            this.label1.Size = new System.Drawing.Size(213, 29);
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
            this.allTabs.Location = new System.Drawing.Point(11, 53);
            this.allTabs.Margin = new System.Windows.Forms.Padding(2);
            this.allTabs.Name = "allTabs";
            this.allTabs.SelectedIndex = 0;
            this.allTabs.Size = new System.Drawing.Size(1047, 608);
            this.allTabs.TabIndex = 3;
            // 
            // tabPayload
            // 
            this.tabPayload.Controls.Add(this.wbXML);
            this.tabPayload.Location = new System.Drawing.Point(4, 38);
            this.tabPayload.Margin = new System.Windows.Forms.Padding(2);
            this.tabPayload.Name = "tabPayload";
            this.tabPayload.Padding = new System.Windows.Forms.Padding(2);
            this.tabPayload.Size = new System.Drawing.Size(1039, 566);
            this.tabPayload.TabIndex = 0;
            this.tabPayload.Text = "Payload";
            this.tabPayload.UseVisualStyleBackColor = true;
            // 
            // wbXML
            // 
            this.wbXML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbXML.Location = new System.Drawing.Point(2, 2);
            this.wbXML.Name = "wbXML";
            this.wbXML.Size = new System.Drawing.Size(1035, 562);
            this.wbXML.TabIndex = 0;
            // 
            // tbErrors
            // 
            this.tbErrors.Controls.Add(this.tbError);
            this.tbErrors.Location = new System.Drawing.Point(4, 38);
            this.tbErrors.Name = "tbErrors";
            this.tbErrors.Padding = new System.Windows.Forms.Padding(3);
            this.tbErrors.Size = new System.Drawing.Size(1039, 566);
            this.tbErrors.TabIndex = 1;
            this.tbErrors.Text = "Logs";
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
            this.tbError.Size = new System.Drawing.Size(1033, 560);
            this.tbError.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.DarkGray;
            this.tabPage1.Controls.Add(this.tbPayloadContent);
            this.tabPage1.Controls.Add(this.btnCancel);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1039, 566);
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
            this.tbPayloadContent.Size = new System.Drawing.Size(1035, 525);
            this.tbPayloadContent.TabIndex = 12;
            this.tbPayloadContent.Text = "";
            this.tbPayloadContent.WordWrap = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(905, 530);
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
            this.btnSave.Location = new System.Drawing.Point(984, 530);
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
            this.cbEndpoint.Location = new System.Drawing.Point(164, 666);
            this.cbEndpoint.Margin = new System.Windows.Forms.Padding(2);
            this.cbEndpoint.Name = "cbEndpoint";
            this.cbEndpoint.Size = new System.Drawing.Size(894, 37);
            this.cbEndpoint.TabIndex = 4;
            this.cbEndpoint.ValueMember = "Value";
            // 
            // buttonPressureTest
            // 
            this.buttonPressureTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPressureTest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonPressureTest.Enabled = false;
            this.buttonPressureTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPressureTest.Location = new System.Drawing.Point(464, 758);
            this.buttonPressureTest.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPressureTest.Name = "buttonPressureTest";
            this.buttonPressureTest.Size = new System.Drawing.Size(322, 47);
            this.buttonPressureTest.TabIndex = 5;
            this.buttonPressureTest.Text = "Pressure Test";
            this.buttonPressureTest.UseVisualStyleBackColor = true;
            this.buttonPressureTest.Click += new System.EventHandler(this.btnPresssureTest_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 669);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 29);
            this.label2.TabIndex = 6;
            this.label2.Text = "Service URL";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 707);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 29);
            this.label3.TabIndex = 7;
            this.label3.Text = "Event Name";
            // 
            // cbEventName
            // 
            this.cbEventName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEventName.DisplayMember = "Name";
            this.cbEventName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEventName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEventName.FormattingEnabled = true;
            this.cbEventName.Location = new System.Drawing.Point(163, 705);
            this.cbEventName.Margin = new System.Windows.Forms.Padding(2);
            this.cbEventName.MaxDropDownItems = 25;
            this.cbEventName.Name = "cbEventName";
            this.cbEventName.Size = new System.Drawing.Size(409, 37);
            this.cbEventName.TabIndex = 8;
            this.cbEventName.ValueMember = "Value";
            // 
            // buttonNormalTest
            // 
            this.buttonNormalTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNormalTest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonNormalTest.Enabled = false;
            this.buttonNormalTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNormalTest.Location = new System.Drawing.Point(18, 758);
            this.buttonNormalTest.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNormalTest.Name = "buttonNormalTest";
            this.buttonNormalTest.Size = new System.Drawing.Size(359, 47);
            this.buttonNormalTest.TabIndex = 5;
            this.buttonNormalTest.Text = "Normal Test (Single Request)";
            this.buttonNormalTest.UseVisualStyleBackColor = true;
            this.buttonNormalTest.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // numericUpDownEventsNumbers
            // 
            this.numericUpDownEventsNumbers.Location = new System.Drawing.Point(1002, 766);
            this.numericUpDownEventsNumbers.Name = "numericUpDownEventsNumbers";
            this.numericUpDownEventsNumbers.Size = new System.Drawing.Size(56, 32);
            this.numericUpDownEventsNumbers.TabIndex = 9;
            this.numericUpDownEventsNumbers.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownEventsNumbers.ValueChanged += new System.EventHandler(this.numericUpDownEventsNumbers_ValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(816, 769);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 26);
            this.label4.TabIndex = 10;
            this.label4.Text = "Events Numbers:";
            // 
            // labelProgess
            // 
            this.labelProgess.AutoSize = true;
            this.labelProgess.Location = new System.Drawing.Point(605, 709);
            this.labelProgess.Name = "labelProgess";
            this.labelProgess.Size = new System.Drawing.Size(0, 26);
            this.labelProgess.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1069, 816);
            this.Controls.Add(this.labelProgess);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownEventsNumbers);
            this.Controls.Add(this.cbEventName);
            this.Controls.Add(this.label3);
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
            this.Text = "Provider Management - Enrollment Test Client";
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbEventName;
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelProgess;
    }
}

