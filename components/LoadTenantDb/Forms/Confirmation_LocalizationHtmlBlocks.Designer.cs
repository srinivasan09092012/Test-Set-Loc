namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    partial class Confirmation_LocalizationHtmlBlocks
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
            this.htmlBlocksGroupBox = new System.Windows.Forms.GroupBox();
            this.htmlBlocksGridView = new System.Windows.Forms.DataGridView();
            this.htmlBlockAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.htmlBlockModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.html = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loadButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.htmlBlocksGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.htmlBlocksGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // htmlBlocksGroupBox
            // 
            this.htmlBlocksGroupBox.Controls.Add(this.htmlBlocksGridView);
            this.htmlBlocksGroupBox.Location = new System.Drawing.Point(12, 12);
            this.htmlBlocksGroupBox.Name = "htmlBlocksGroupBox";
            this.htmlBlocksGroupBox.Size = new System.Drawing.Size(1057, 349);
            this.htmlBlocksGroupBox.TabIndex = 11;
            this.htmlBlocksGroupBox.TabStop = false;
            this.htmlBlocksGroupBox.Text = "HTML Blocks";
            // 
            // htmlBlocksGridView
            // 
            this.htmlBlocksGridView.AllowUserToAddRows = false;
            this.htmlBlocksGridView.AllowUserToDeleteRows = false;
            this.htmlBlocksGridView.AllowUserToResizeRows = false;
            this.htmlBlocksGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.htmlBlocksGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.htmlBlockAction,
            this.htmlBlockModule,
            this.contentid,
            this.locale,
            this.html});
            this.htmlBlocksGridView.Location = new System.Drawing.Point(6, 19);
            this.htmlBlocksGridView.MultiSelect = false;
            this.htmlBlocksGridView.Name = "htmlBlocksGridView";
            this.htmlBlocksGridView.ReadOnly = true;
            this.htmlBlocksGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.htmlBlocksGridView.Size = new System.Drawing.Size(1045, 308);
            this.htmlBlocksGridView.StandardTab = true;
            this.htmlBlocksGridView.TabIndex = 1;
            this.htmlBlocksGridView.TabStop = false;
            // 
            // htmlBlockAction
            // 
            this.htmlBlockAction.HeaderText = "Action";
            this.htmlBlockAction.Name = "htmlBlockAction";
            this.htmlBlockAction.ReadOnly = true;
            this.htmlBlockAction.Width = 80;
            // 
            // htmlBlockModule
            // 
            this.htmlBlockModule.HeaderText = "Module";
            this.htmlBlockModule.Name = "htmlBlockModule";
            this.htmlBlockModule.ReadOnly = true;
            this.htmlBlockModule.Width = 120;
            // 
            // contentid
            // 
            this.contentid.HeaderText = "ContentId";
            this.contentid.Name = "contentid";
            this.contentid.ReadOnly = true;
            this.contentid.Width = 325;
            // 
            // locale
            // 
            this.locale.HeaderText = "Locale";
            this.locale.Name = "locale";
            this.locale.ReadOnly = true;
            this.locale.Width = 60;
            // 
            // html
            // 
            this.html.HeaderText = "Html";
            this.html.Name = "html";
            this.html.ReadOnly = true;
            this.html.Width = 400;
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(971, 384);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(56, 29);
            this.loadButton.TabIndex = 15;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(878, 384);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(72, 29);
            this.cancelButton.TabIndex = 16;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // Confirmation_LocalizationHtmlBlocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 426);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.htmlBlocksGroupBox);
            this.Name = "Confirmation_LocalizationHtmlBlocks";
            this.Text = "Model Configuration";
            this.Load += new System.EventHandler(this.Confirmation_LocalizationHtmlBlocks_Load);
            this.htmlBlocksGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.htmlBlocksGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox htmlBlocksGroupBox;
        private System.Windows.Forms.DataGridView htmlBlocksGridView;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn htmlBlockAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn htmlBlockModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentid;
        private System.Windows.Forms.DataGridViewTextBoxColumn locale;
        private System.Windows.Forms.DataGridViewTextBoxColumn html;
    }
}