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
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.allTabs = new System.Windows.Forms.TabControl();
            this.tabPayload = new System.Windows.Forms.TabPage();
            this.tbPayloadContent = new System.Windows.Forms.TextBox();
            this.cbEndpoint = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.allTabs.SuspendLayout();
            this.tabPayload.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(12, 44);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.ReadOnly = true;
            this.tbFileName.Size = new System.Drawing.Size(614, 22);
            this.tbFileName.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(632, 28);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(86, 38);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse..";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enrollment Event Payload File";
            // 
            // allTabs
            // 
            this.allTabs.Controls.Add(this.tabPayload);
            this.allTabs.Location = new System.Drawing.Point(15, 83);
            this.allTabs.Name = "allTabs";
            this.allTabs.SelectedIndex = 0;
            this.allTabs.Size = new System.Drawing.Size(692, 379);
            this.allTabs.TabIndex = 3;
            // 
            // tabPayload
            // 
            this.tabPayload.Controls.Add(this.tbPayloadContent);
            this.tabPayload.Location = new System.Drawing.Point(4, 25);
            this.tabPayload.Name = "tabPayload";
            this.tabPayload.Padding = new System.Windows.Forms.Padding(3);
            this.tabPayload.Size = new System.Drawing.Size(684, 350);
            this.tabPayload.TabIndex = 0;
            this.tabPayload.Text = "Payload";
            this.tabPayload.UseVisualStyleBackColor = true;
            // 
            // tbPayloadContent
            // 
            this.tbPayloadContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPayloadContent.Location = new System.Drawing.Point(3, 3);
            this.tbPayloadContent.Multiline = true;
            this.tbPayloadContent.Name = "tbPayloadContent";
            this.tbPayloadContent.ReadOnly = true;
            this.tbPayloadContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbPayloadContent.Size = new System.Drawing.Size(678, 344);
            this.tbPayloadContent.TabIndex = 0;
            // 
            // cbEndpoint
            // 
            this.cbEndpoint.DisplayMember = "Name";
            this.cbEndpoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEndpoint.FormattingEnabled = true;
            this.cbEndpoint.Location = new System.Drawing.Point(22, 485);
            this.cbEndpoint.Name = "cbEndpoint";
            this.cbEndpoint.Size = new System.Drawing.Size(600, 24);
            this.cbEndpoint.TabIndex = 4;
            this.cbEndpoint.ValueMember = "Value";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Enabled = false;
            this.btnSubmit.Location = new System.Drawing.Point(628, 471);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 38);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 520);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cbEndpoint);
            this.Controls.Add(this.allTabs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbFileName);
            this.Name = "MainForm";
            this.Text = "Provider Management - Enrollment Test Client";
            this.allTabs.ResumeLayout(false);
            this.tabPayload.ResumeLayout(false);
            this.tabPayload.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl allTabs;
        private System.Windows.Forms.TabPage tabPayload;
        private System.Windows.Forms.TextBox tbPayloadContent;
        private System.Windows.Forms.ComboBox cbEndpoint;
        private System.Windows.Forms.Button btnSubmit;
    }
}

