namespace HPPLocalNugetMgrVSIXProject.HPP
{
    /// <summary>
    ///     The primary custom form/window displayed by this VS addin.
    /// </summary>
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.ProjectsListView = new System.Windows.Forms.ListView();
            this.ProjectNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ButtonOpenDllFolder = new System.Windows.Forms.Button();
            this.ProjectDllFolder = new System.Windows.Forms.Label();
            this.Status = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.LocalPackageFolderGroupBox = new System.Windows.Forms.GroupBox();
            this.DeleteLocalPackageFolderHelptext2 = new System.Windows.Forms.Label();
            this.OverlayDllsHelptext2 = new System.Windows.Forms.Label();
            this.OpenLocalPackageFolderHelptext = new System.Windows.Forms.Label();
            this.OverlayDllsHelptext = new System.Windows.Forms.Label();
            this.DeleteLocalPackageFolderHelptext = new System.Windows.Forms.Label();
            this.LocalPackageFolderLabel = new System.Windows.Forms.Label();
            this.LocalPackageFolder = new System.Windows.Forms.Label();
            this.ButtonOpenLocalPackageFolder = new System.Windows.Forms.Button();
            this.ButtonDeleteUnpackedPackage = new System.Windows.Forms.Button();
            this.ButtonOverlayDlls = new System.Windows.Forms.Button();
            this.ProjectGroupBox = new System.Windows.Forms.GroupBox();
            this.OpenProjectDllFolderHelptext = new System.Windows.Forms.Label();
            this.ProjectDllFolderLabel = new System.Windows.Forms.Label();
            this.ProjectDllFolderTextBox = new System.Windows.Forms.TextBox();
            this.LocalPackageFolderTextBox = new System.Windows.Forms.TextBox();
            this.Status.SuspendLayout();
            this.LocalPackageFolderGroupBox.SuspendLayout();
            this.ProjectGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProjectsListView
            // 
            this.ProjectsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProjectNameHeader});
            this.ProjectsListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectsListView.FullRowSelect = true;
            this.ProjectsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ProjectsListView.HideSelection = false;
            this.ProjectsListView.Location = new System.Drawing.Point(22, 12);
            this.ProjectsListView.MultiSelect = false;
            this.ProjectsListView.Name = "ProjectsListView";
            this.ProjectsListView.Size = new System.Drawing.Size(339, 357);
            this.ProjectsListView.TabIndex = 0;
            this.ProjectsListView.UseCompatibleStateImageBehavior = false;
            this.ProjectsListView.View = System.Windows.Forms.View.Details;
            this.ProjectsListView.SelectedIndexChanged += new System.EventHandler(this.ProjectsListView_SelectedIndexChanged);
            // 
            // ProjectNameHeader
            // 
            this.ProjectNameHeader.Text = "\"Packable\" Project Name";
            this.ProjectNameHeader.Width = 330;
            // 
            // ButtonOpenDllFolder
            // 
            this.ButtonOpenDllFolder.AutoSize = true;
            this.ButtonOpenDllFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOpenDllFolder.Location = new System.Drawing.Point(21, 78);
            this.ButtonOpenDllFolder.Name = "ButtonOpenDllFolder";
            this.ButtonOpenDllFolder.Size = new System.Drawing.Size(123, 27);
            this.ButtonOpenDllFolder.TabIndex = 3;
            this.ButtonOpenDllFolder.Text = "Open Folder";
            this.ButtonOpenDllFolder.UseVisualStyleBackColor = true;
            this.ButtonOpenDllFolder.Click += new System.EventHandler(this.ButtonOpenDllFolder_Click);
            // 
            // ProjectDllFolder
            // 
            this.ProjectDllFolder.AutoSize = true;
            this.ProjectDllFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProjectDllFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectDllFolder.Location = new System.Drawing.Point(21, 53);
            this.ProjectDllFolder.Name = "ProjectDllFolder";
            this.ProjectDllFolder.Size = new System.Drawing.Size(123, 22);
            this.ProjectDllFolder.TabIndex = 5;
            this.ProjectDllFolder.Text = "ProjectDllFolder";
            // 
            // Status
            // 
            this.Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.Status.Location = new System.Drawing.Point(0, 420);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(1111, 25);
            this.Status.TabIndex = 8;
            this.Status.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(85, 20);
            this.StatusLabel.Text = "StatusLabel";
            // 
            // LocalPackageFolderGroupBox
            // 
            this.LocalPackageFolderGroupBox.AutoSize = true;
            this.LocalPackageFolderGroupBox.Controls.Add(this.LocalPackageFolderTextBox);
            this.LocalPackageFolderGroupBox.Controls.Add(this.DeleteLocalPackageFolderHelptext2);
            this.LocalPackageFolderGroupBox.Controls.Add(this.OverlayDllsHelptext2);
            this.LocalPackageFolderGroupBox.Controls.Add(this.OpenLocalPackageFolderHelptext);
            this.LocalPackageFolderGroupBox.Controls.Add(this.OverlayDllsHelptext);
            this.LocalPackageFolderGroupBox.Controls.Add(this.DeleteLocalPackageFolderHelptext);
            this.LocalPackageFolderGroupBox.Controls.Add(this.LocalPackageFolderLabel);
            this.LocalPackageFolderGroupBox.Controls.Add(this.LocalPackageFolder);
            this.LocalPackageFolderGroupBox.Controls.Add(this.ButtonOpenLocalPackageFolder);
            this.LocalPackageFolderGroupBox.Controls.Add(this.ButtonDeleteUnpackedPackage);
            this.LocalPackageFolderGroupBox.Controls.Add(this.ButtonOverlayDlls);
            this.LocalPackageFolderGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocalPackageFolderGroupBox.Location = new System.Drawing.Point(371, 154);
            this.LocalPackageFolderGroupBox.Name = "LocalPackageFolderGroupBox";
            this.LocalPackageFolderGroupBox.Size = new System.Drawing.Size(673, 215);
            this.LocalPackageFolderGroupBox.TabIndex = 9;
            this.LocalPackageFolderGroupBox.TabStop = false;
            this.LocalPackageFolderGroupBox.Text = "Local / Expanded Nuget Package";
            // 
            // DeleteLocalPackageFolderHelptext2
            // 
            this.DeleteLocalPackageFolderHelptext2.AutoSize = true;
            this.DeleteLocalPackageFolderHelptext2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteLocalPackageFolderHelptext2.Location = new System.Drawing.Point(160, 135);
            this.DeleteLocalPackageFolderHelptext2.Name = "DeleteLocalPackageFolderHelptext2";
            this.DeleteLocalPackageFolderHelptext2.Size = new System.Drawing.Size(410, 15);
            this.DeleteLocalPackageFolderHelptext2.TabIndex = 16;
            this.DeleteLocalPackageFolderHelptext2.Text = "2. You must  compile a project that references the package for it to unpack.";
            // 
            // OverlayDllsHelptext2
            // 
            this.OverlayDllsHelptext2.AutoSize = true;
            this.OverlayDllsHelptext2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OverlayDllsHelptext2.Location = new System.Drawing.Point(160, 175);
            this.OverlayDllsHelptext2.Name = "OverlayDllsHelptext2";
            this.OverlayDllsHelptext2.Size = new System.Drawing.Size(462, 15);
            this.OverlayDllsHelptext2.TabIndex = 15;
            this.OverlayDllsHelptext2.Text = "2. Note this does not build the project.  You must build before running this exte" +
    "nsion.";
            // 
            // OpenLocalPackageFolderHelptext
            // 
            this.OpenLocalPackageFolderHelptext.AutoSize = true;
            this.OpenLocalPackageFolderHelptext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenLocalPackageFolderHelptext.Location = new System.Drawing.Point(160, 89);
            this.OpenLocalPackageFolderHelptext.Name = "OpenLocalPackageFolderHelptext";
            this.OpenLocalPackageFolderHelptext.Size = new System.Drawing.Size(131, 15);
            this.OpenLocalPackageFolderHelptext.TabIndex = 14;
            this.OpenLocalPackageFolderHelptext.Text = "Opens in File Explorer.";
            // 
            // OverlayDllsHelptext
            // 
            this.OverlayDllsHelptext.AutoSize = true;
            this.OverlayDllsHelptext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OverlayDllsHelptext.Location = new System.Drawing.Point(160, 160);
            this.OverlayDllsHelptext.Name = "OverlayDllsHelptext";
            this.OverlayDllsHelptext.Size = new System.Drawing.Size(507, 15);
            this.OverlayDllsHelptext.TabIndex = 13;
            this.OverlayDllsHelptext.Text = "1. Copies executables from the VS Project to the local package folder for debuggi" +
    "ng / testing.";
            // 
            // DeleteLocalPackageFolderHelptext
            // 
            this.DeleteLocalPackageFolderHelptext.AutoSize = true;
            this.DeleteLocalPackageFolderHelptext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteLocalPackageFolderHelptext.Location = new System.Drawing.Point(160, 120);
            this.DeleteLocalPackageFolderHelptext.Name = "DeleteLocalPackageFolderHelptext";
            this.DeleteLocalPackageFolderHelptext.Size = new System.Drawing.Size(430, 15);
            this.DeleteLocalPackageFolderHelptext.TabIndex = 12;
            this.DeleteLocalPackageFolderHelptext.Text = "1. Deletes the folder so that the source package can re-download and unpack.";
            // 
            // LocalPackageFolderLabel
            // 
            this.LocalPackageFolderLabel.AutoSize = true;
            this.LocalPackageFolderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocalPackageFolderLabel.Location = new System.Drawing.Point(17, 33);
            this.LocalPackageFolderLabel.Name = "LocalPackageFolderLabel";
            this.LocalPackageFolderLabel.Size = new System.Drawing.Size(193, 20);
            this.LocalPackageFolderLabel.TabIndex = 11;
            this.LocalPackageFolderLabel.Text = "LocalPackageFolderLabel";
            // 
            // LocalPackageFolder
            // 
            this.LocalPackageFolder.AutoSize = true;
            this.LocalPackageFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LocalPackageFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocalPackageFolder.Location = new System.Drawing.Point(21, 53);
            this.LocalPackageFolder.Name = "LocalPackageFolder";
            this.LocalPackageFolder.Size = new System.Drawing.Size(156, 22);
            this.LocalPackageFolder.TabIndex = 10;
            this.LocalPackageFolder.Text = "LocalPackageFolder";
            // 
            // ButtonOpenLocalPackageFolder
            // 
            this.ButtonOpenLocalPackageFolder.AutoSize = true;
            this.ButtonOpenLocalPackageFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOpenLocalPackageFolder.Location = new System.Drawing.Point(21, 85);
            this.ButtonOpenLocalPackageFolder.Name = "ButtonOpenLocalPackageFolder";
            this.ButtonOpenLocalPackageFolder.Size = new System.Drawing.Size(123, 27);
            this.ButtonOpenLocalPackageFolder.TabIndex = 9;
            this.ButtonOpenLocalPackageFolder.Text = "Open Folder";
            this.ButtonOpenLocalPackageFolder.UseVisualStyleBackColor = true;
            this.ButtonOpenLocalPackageFolder.Click += new System.EventHandler(this.ButtonOpenLocalPackageFolder_Click);
            // 
            // ButtonDeleteUnpackedPackage
            // 
            this.ButtonDeleteUnpackedPackage.AutoSize = true;
            this.ButtonDeleteUnpackedPackage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonDeleteUnpackedPackage.Location = new System.Drawing.Point(21, 120);
            this.ButtonDeleteUnpackedPackage.Name = "ButtonDeleteUnpackedPackage";
            this.ButtonDeleteUnpackedPackage.Size = new System.Drawing.Size(123, 30);
            this.ButtonDeleteUnpackedPackage.TabIndex = 8;
            this.ButtonDeleteUnpackedPackage.Text = "Delete Folder";
            this.ButtonDeleteUnpackedPackage.UseVisualStyleBackColor = true;
            this.ButtonDeleteUnpackedPackage.Click += new System.EventHandler(this.ButtonDeleteUnpackedPackage_Click);
            // 
            // ButtonOverlayDlls
            // 
            this.ButtonOverlayDlls.AutoSize = true;
            this.ButtonOverlayDlls.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOverlayDlls.Location = new System.Drawing.Point(21, 162);
            this.ButtonOverlayDlls.Name = "ButtonOverlayDlls";
            this.ButtonOverlayDlls.Size = new System.Drawing.Size(123, 28);
            this.ButtonOverlayDlls.TabIndex = 7;
            this.ButtonOverlayDlls.Text = "Overlay DLLs";
            this.ButtonOverlayDlls.UseVisualStyleBackColor = true;
            this.ButtonOverlayDlls.Click += new System.EventHandler(this.ButtonOverlayDlls_Click);
            // 
            // ProjectGroupBox
            // 
            this.ProjectGroupBox.AutoSize = true;
            this.ProjectGroupBox.Controls.Add(this.ProjectDllFolderTextBox);
            this.ProjectGroupBox.Controls.Add(this.OpenProjectDllFolderHelptext);
            this.ProjectGroupBox.Controls.Add(this.ProjectDllFolderLabel);
            this.ProjectGroupBox.Controls.Add(this.ProjectDllFolder);
            this.ProjectGroupBox.Controls.Add(this.ButtonOpenDllFolder);
            this.ProjectGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectGroupBox.Location = new System.Drawing.Point(371, 12);
            this.ProjectGroupBox.Name = "ProjectGroupBox";
            this.ProjectGroupBox.Size = new System.Drawing.Size(673, 130);
            this.ProjectGroupBox.TabIndex = 10;
            this.ProjectGroupBox.TabStop = false;
            this.ProjectGroupBox.Text = "VS Project";
            // 
            // OpenProjectDllFolderHelptext
            // 
            this.OpenProjectDllFolderHelptext.AutoSize = true;
            this.OpenProjectDllFolderHelptext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenProjectDllFolderHelptext.Location = new System.Drawing.Point(150, 78);
            this.OpenProjectDllFolderHelptext.Name = "OpenProjectDllFolderHelptext";
            this.OpenProjectDllFolderHelptext.Size = new System.Drawing.Size(345, 15);
            this.OpenProjectDllFolderHelptext.TabIndex = 15;
            this.OpenProjectDllFolderHelptext.Text = "Opens in File Explorer for the selected build: Release / Debug.";
            // 
            // ProjectDllFolderLabel
            // 
            this.ProjectDllFolderLabel.AutoSize = true;
            this.ProjectDllFolderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectDllFolderLabel.Location = new System.Drawing.Point(17, 33);
            this.ProjectDllFolderLabel.Name = "ProjectDllFolderLabel";
            this.ProjectDllFolderLabel.Size = new System.Drawing.Size(160, 20);
            this.ProjectDllFolderLabel.TabIndex = 6;
            this.ProjectDllFolderLabel.Text = "ProjectDllFolderLabel";
            // 
            // ProjectDllFolderTextBox
            // 
            this.ProjectDllFolderTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProjectDllFolderTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectDllFolderTextBox.Location = new System.Drawing.Point(153, 53);
            this.ProjectDllFolderTextBox.Name = "ProjectDllFolderTextBox";
            this.ProjectDllFolderTextBox.ReadOnly = true;
            this.ProjectDllFolderTextBox.Size = new System.Drawing.Size(128, 26);
            this.ProjectDllFolderTextBox.TabIndex = 16;
            // 
            // LocalPackageFolderTextBox
            // 
            this.LocalPackageFolderTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LocalPackageFolderTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocalPackageFolderTextBox.Location = new System.Drawing.Point(193, 53);
            this.LocalPackageFolderTextBox.Name = "LocalPackageFolderTextBox";
            this.LocalPackageFolderTextBox.ReadOnly = true;
            this.LocalPackageFolderTextBox.Size = new System.Drawing.Size(128, 26);
            this.LocalPackageFolderTextBox.TabIndex = 17;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1111, 445);
            this.Controls.Add(this.ProjectGroupBox);
            this.Controls.Add(this.LocalPackageFolderGroupBox);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.ProjectsListView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Text = "Manage Local Nuget Packages";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.HelpButton_Clicked);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Status.ResumeLayout(false);
            this.Status.PerformLayout();
            this.LocalPackageFolderGroupBox.ResumeLayout(false);
            this.LocalPackageFolderGroupBox.PerformLayout();
            this.ProjectGroupBox.ResumeLayout(false);
            this.ProjectGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView ProjectsListView;
        private System.Windows.Forms.ColumnHeader ProjectNameHeader;
        private System.Windows.Forms.Button ButtonOpenDllFolder;
        private System.Windows.Forms.Label ProjectDllFolder;
        private System.Windows.Forms.StatusStrip Status;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.GroupBox LocalPackageFolderGroupBox;
        private System.Windows.Forms.Label LocalPackageFolder;
        private System.Windows.Forms.Button ButtonOpenLocalPackageFolder;
        private System.Windows.Forms.Button ButtonDeleteUnpackedPackage;
        private System.Windows.Forms.Button ButtonOverlayDlls;
        private System.Windows.Forms.Label LocalPackageFolderLabel;
        private System.Windows.Forms.GroupBox ProjectGroupBox;
        private System.Windows.Forms.Label ProjectDllFolderLabel;
        private System.Windows.Forms.Label DeleteLocalPackageFolderHelptext;
        private System.Windows.Forms.Label OpenLocalPackageFolderHelptext;
        private System.Windows.Forms.Label OverlayDllsHelptext;
        private System.Windows.Forms.Label OpenProjectDllFolderHelptext;
        private System.Windows.Forms.Label OverlayDllsHelptext2;
        private System.Windows.Forms.Label DeleteLocalPackageFolderHelptext2;
        private System.Windows.Forms.TextBox LocalPackageFolderTextBox;
        private System.Windows.Forms.TextBox ProjectDllFolderTextBox;
    }
}