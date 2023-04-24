using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HPPLocalNugetMgrVSIXProject.HPP
{
    public partial class FormMain : Form
    {
        private readonly Color errorTextColor = Color.DarkRed;

        private enum VerticalPosition
        {
            Top = 0,
            Bottom = 1,
            Center = 2
        }

        internal ProjectDictionary ProjectDictionary { get; private set; }

        public FormMain()
        {
            InitializeComponent();
        }

        internal void Populate(ProjectDictionary projectDictionary)
        {
            this.ProjectDictionary = projectDictionary;
            foreach (var projectEntry in this.ProjectDictionary)
            {
                var item = new System.Windows.Forms.ListViewItem()
                {
                    Text = projectEntry.Value.PackageId
                };

                this.ProjectsListView.Items.Add(item);
            }

            if (this.ProjectsListView.Items.Count == 1)
            {
                this.ProjectsListView.Items[0].Selected = true;
            }

            if (this.ProjectsListView.Items.Count == 0)
            {
                this.SetControlStates(string.Empty);
            }


            System.Windows.Forms.Application.DoEvents();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            string myVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text = myVersion + " " + this.Text;
            this.SetToolTips();
        }

        private void SetToolTips()
        {
            ToolTip tooltip = new ToolTip();

            tooltip.SetToolTip(this.ProjectDllFolder, "Folder where executables will be located after a Build.");
            tooltip.SetToolTip(this.LocalPackageFolder, "Folder where executables will be located when the package is unpacked.");
        }

        private void ButtonOpenDllFolder_Click(object sender, EventArgs e)
        {
            try
            {
                var project = this.GetSelectedProject();
                if (project != null)
                {
                    FileUtility.OpenFolderWithFileExplorer(project.OutputPathFull);
                }
            }
            catch (Exception ex)
            {
                this.DisplayException(ex);
            }
        }

        private void ButtonOpenLocalPackageFolder_Click(object sender, EventArgs e)
        {
            try
            {
                var project = this.GetSelectedProject();
                if (project != null)
                {
                    FileUtility.OpenLocalPackageFolder(project);
                }
            }
            catch (Exception ex)
            {
                this.DisplayException(ex);
            }
        }

        private void ButtonOverlayDlls_Click(object sender, EventArgs e)
        {
            try
            {
                var project = this.GetSelectedProject();
                if (project != null)
                {
                    FileUtility.CopyFiles(project.OutputPathFull, project.LocalPackageLibFolder);
                    this.SetStatus("Files copied.  Click 'Open Local Package Folder' to verify.");
                }
            }
            catch (Exception ex)
            {
                this.SetStatus("Error occurred copying files.");
                this.DisplayException(ex);
            }
        }

        private void ButtonDeleteUnpackedPackage_Click(object sender, EventArgs e)
        {
            string statusText = string.Empty;
            var project = this.GetSelectedProject();
            if (project != null)
            { 
                if (FileUtility.DeleteFolder(project.LocalPackageFolder))
                {
                    statusText = "Folder deleted.";
                }
                else
                {
                    statusText = "Folder deletion not successful.";
                }

                this.SetControlStates(statusText);
            }
        }

        private void ProjectsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetControlStates(string.Empty);
        }

        private void SetControlStates(string statusText)
        {
            this.ProjectDllFolderLabel.Text = "Build Output Folder";
            this.ProjectDllFolder.ForeColor = Control.DefaultForeColor;
            this.ProjectDllFolderTextBox.ForeColor = Control.DefaultForeColor;
            this.LocalPackageFolderLabel.Text = "Unpacked folder path";
            this.LocalPackageFolder.ForeColor = Control.DefaultForeColor;
            this.LocalPackageFolderTextBox.ForeColor = Control.DefaultForeColor;


            if (this.ProjectsListView.SelectedItems.Count == 1)
            {
                this.LocalPackageFolderGroupBox.Enabled = true;
                this.ProjectGroupBox.Enabled = true;

                ListViewItem selectedProject = this.ProjectsListView.SelectedItems[0];
                string projectName = selectedProject.Text;

                if (this.ProjectDictionary.TryGetValue(projectName, out var project))
                {
                    this.ProjectDllFolder.Text = project.OutputPathFull;
                    this.ProjectDllFolderTextBox.Text = project.OutputPathFull;

                    if (Directory.Exists(project.OutputPathFull))
                    {
                        this.ButtonOpenDllFolder.Enabled = true;
                    }
                    else
                    {
                        this.ProjectDllFolderLabel.Text += " (not present - try compiling the project)";
                        this.ProjectDllFolder.ForeColor = this.errorTextColor;
                        this.ProjectDllFolderTextBox.ForeColor = this.errorTextColor;
                        this.ButtonOpenDllFolder.Enabled = false;
                    }

                    this.LocalPackageFolder.Text = project.LocalPackageLibFolder;
                    this.LocalPackageFolderTextBox.Text = project.LocalPackageLibFolder;
                    if (Directory.Exists(project.LocalPackageFolder))
                    {
                        this.ButtonOpenLocalPackageFolder.Enabled = true;
                        this.ButtonDeleteUnpackedPackage.Enabled = true;
                        this.DeleteLocalPackageFolderHelptext2.ForeColor = Control.DefaultForeColor;
                    }
                    else
                    {
                        this.LocalPackageFolderLabel.Text += " (not present - package not yet unpacked)";
                        this.LocalPackageFolder.ForeColor = this.errorTextColor;
                        this.LocalPackageFolderTextBox.ForeColor = this.errorTextColor;
                        this.ButtonOpenLocalPackageFolder.Enabled = false;
                        this.ButtonDeleteUnpackedPackage.Enabled = false;
                        this.DeleteLocalPackageFolderHelptext2.ForeColor = this.errorTextColor;
                    }
                }

                if (this.ButtonOpenDllFolder.Enabled && this.ButtonOpenLocalPackageFolder.Enabled)
                {
                    this.ButtonOverlayDlls.Enabled = true;
                }
                else
                {
                    this.ButtonOverlayDlls.Enabled = false;
                }

                this.SetStatus(statusText);
            }
            else
            {
                this.LocalPackageFolderGroupBox.Enabled = false;
                this.ProjectGroupBox.Enabled = false;

                this.ProjectDllFolder.Text = string.Empty;
                this.LocalPackageFolder.Text = string.Empty;

                this.ButtonOpenDllFolder.Enabled = false;
                this.ButtonOpenLocalPackageFolder.Enabled = false;
                this.ButtonDeleteUnpackedPackage.Enabled = false;
                this.ButtonOverlayDlls.Enabled = false;

                if (this.ProjectsListView.Items.Count == 0)
                {
                    this.SetStatus("Either no solution opened or solution does not contain any packable projects.", true);
                }
                else
                {
                    this.SetStatus("Select a project.", true);
                }
            }

            this.PositionControls();
        }

        private void PositionControls()
        {
            int labelSpacing = 1;
            int buttonSpacing = 12;
            int heightAdjust;

            //------------------------------------------------------------------------------------
            // VS Project GroupBox
            //------------------------------------------------------------------------------------

            // Open Project Folder button and help text
            this.PositionUnder(this.ProjectDllFolderLabel, this.ProjectDllFolder, labelSpacing);
            this.ProjectDllFolderTextBox.Left = this.ProjectDllFolder.Left;
            this.ProjectDllFolderTextBox.Top = this.ProjectDllFolder.Top;
            this.ProjectDllFolderTextBox.Width = this.ProjectDllFolder.Width + this.ProjectDllFolderTextBox.Margin.Left + this.ProjectDllFolderTextBox.Margin.Right; ;
            this.ProjectDllFolderTextBox.Height = this.ProjectDllFolder.Height + this.ProjectDllFolderTextBox.Margin.Top + this.ProjectDllFolderTextBox.Margin.Bottom;
            this.PositionUnder(this.ProjectDllFolderTextBox, this.ButtonOpenDllFolder, buttonSpacing);
            this.PositionToRight(this.ButtonOpenDllFolder, this.OpenProjectDllFolderHelptext, buttonSpacing, VerticalPosition.Center);

            //------------------------------------------------------------------------------------
            // Nuget Package GroupBox
            //------------------------------------------------------------------------------------

            if (this.LocalPackageFolder.Width > this.LocalPackageFolderGroupBox.Width)
            {
                this.LocalPackageFolderGroupBox.Width = this.LocalPackageFolder.Width;
            }
            else
            {
                this.LocalPackageFolder.Width = this.LocalPackageFolderGroupBox.Width;
            }

            this.LocalPackageFolderGroupBox.Top = this.ProjectGroupBox.Bottom + buttonSpacing;

            // Open Package Folder button and help text
            this.PositionUnder(this.LocalPackageFolderLabel, this.LocalPackageFolder, labelSpacing);
            this.LocalPackageFolderTextBox.Left = this.LocalPackageFolder.Left;
            this.LocalPackageFolderTextBox.Top = this.LocalPackageFolder.Top;
            this.LocalPackageFolderTextBox.Width = this.LocalPackageFolder.Width + this.LocalPackageFolderTextBox.Margin.Left + this.LocalPackageFolderTextBox.Margin.Right;
            this.LocalPackageFolderTextBox.Height = this.LocalPackageFolder.Height + this.LocalPackageFolderTextBox.Margin.Top + this.LocalPackageFolderTextBox.Margin.Bottom;

            this.PositionUnder(this.LocalPackageFolderTextBox, this.ButtonOpenLocalPackageFolder, buttonSpacing);
            this.PositionToRight(this.ButtonOpenLocalPackageFolder, this.OpenLocalPackageFolderHelptext, buttonSpacing, VerticalPosition.Center);

            // Delete Package Folder button and help text
            this.PositionUnder(this.ButtonOpenLocalPackageFolder, this.ButtonDeleteUnpackedPackage, buttonSpacing);
            this.PositionToRight(this.ButtonDeleteUnpackedPackage, this.DeleteLocalPackageFolderHelptext, buttonSpacing, VerticalPosition.Top);
            this.DeleteLocalPackageFolderHelptext2.Left = this.DeleteLocalPackageFolderHelptext.Left;
            this.PositionUnder(this.DeleteLocalPackageFolderHelptext, this.DeleteLocalPackageFolderHelptext2, labelSpacing);

            // Overlay Package DLLs button and help text
            this.PositionUnder(this.ButtonDeleteUnpackedPackage, this.ButtonOverlayDlls, buttonSpacing);
            this.PositionToRight(this.ButtonOverlayDlls, this.OverlayDllsHelptext, buttonSpacing, VerticalPosition.Top);
            this.OverlayDllsHelptext2.Left = this.OverlayDllsHelptext.Left;
            this.PositionUnder(this.OverlayDllsHelptext, this.OverlayDllsHelptext2, labelSpacing);

            //------------------------------------------------------------------------------------
            // Project ListBox
            //------------------------------------------------------------------------------------

            // Size the project list so bottom aligns with bottom of groupbox
            if (this.LocalPackageFolderGroupBox.Bottom > this.ProjectsListView.Bottom)
            {
                heightAdjust = this.LocalPackageFolderGroupBox.Bottom - this.ProjectsListView.Bottom;
            }
            else
            {
                heightAdjust = (this.ProjectsListView.Bottom - this.LocalPackageFolderGroupBox.Bottom) * -1;
            }

            this.ProjectsListView.Height += heightAdjust;

            //------------------------------------------------------------------------------------
            // Form 
            //------------------------------------------------------------------------------------

            // Height
            int statusOffset = this.Status.Top - ProjectsListView.Bottom;
            if (statusOffset > 0)
            {
                if (statusOffset < buttonSpacing)
                {
                    this.Height += buttonSpacing;
                }
                else if (statusOffset > buttonSpacing)
                {
                    int difference = statusOffset - buttonSpacing;
                    this.Height -= difference;

                }
            }
            else
            {
                this.Height += (statusOffset * -1) + buttonSpacing;
            }

            // Width
            Control widestGroupBox;

            if (this.LocalPackageFolderGroupBox.Width > this.ProjectGroupBox.Width)
            {
                widestGroupBox = this.LocalPackageFolderGroupBox;
            }
            else
            {
                widestGroupBox = this.ProjectGroupBox;
            }

            this.Width = this.ProjectsListView.Left + widestGroupBox.Right + (this.ProjectsListView.Left);
        }

        /// <summary>
        ///     Positions a control under another control.
        /// </summary>
        /// <param name="overControl">
        ///     Control on top.  It's position should have already been established.
        /// </param>
        /// <param name="underControl">
        ///     Control to be placed under the overControl.
        /// </param>
        /// <param name="spacing">
        ///     Desired amount of spacing between the controls.
        /// </param>
        private void PositionUnder(Control overControl, Control underControl, int spacing)
        {
            underControl.Top = overControl.Top + overControl.Height + spacing;
        }

        /// <summary>
        ///     Positions a control to the right of another control.
        /// </summary>
        /// <param name="leftControl">
        ///     Control to the left.  It's position should have already been established.
        /// </param>
        /// <param name="rightControl">
        ///     Control to be placed to the right of the leftControl.
        /// </param>
        /// <param name="spacing">
        ///     Desired amount of spacing between the controls.
        /// </param>
        /// <param name="position">
        ///     Indicates whether to top-align or center the rightControl relative to the
        ///     leftControl.
        /// </param>
        private void PositionToRight(Control leftControl, Control rightControl, int spacing, VerticalPosition position)
        {
            rightControl.Left = leftControl.Left + leftControl.Width + spacing;

            if (position == VerticalPosition.Top)
            {
                rightControl.Top = leftControl.Top;
            }
            else if (position == VerticalPosition.Center)
            {
                // This assumes the right control is shorter or the same height
                rightControl.Top = leftControl.Top + ((leftControl.Height - rightControl.Height) / 2);
            }
        }

        /// <summary>
        ///     Called when a run-time exception occurs.
        ///   <para>
        ///     Displays a pop-up with the exception details.
        ///   </para>
        /// </summary>
        /// <param name="ex"></param>
        private void DisplayException(Exception ex)
        {
            var formOutput = new FormOutput();
            formOutput.Text = "Unexpected Exception";
            formOutput.SetOutputText(ex.ToString());
            formOutput.ShowDialog(this);
            System.Windows.Forms.Application.DoEvents();
        }

        /// <summary>
        ///     Gets the project object for the project selected by the user.
        /// </summary>
        private Project GetSelectedProject()
        {
            if (this.ProjectsListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedProject = this.ProjectsListView.SelectedItems[0];
                string projectName = selectedProject.Text;

                this.ProjectDictionary.TryGetValue(projectName, out var project);

                return project;
            }

            return null;
        }

        /// <summary>
        ///     Sets the status message at the bottom of the form.
        /// </summary>
        /// <param name="text">
        ///     The status message to display.
        /// </param>
        /// <param name="isError">
        ///     Indicates whether the message indicates an error.
        /// </param>
        private void SetStatus(string text, bool isError = false)
        {
            this.StatusLabel.Text = text;

            if (isError)
            {
                this.StatusLabel.ForeColor = this.errorTextColor;
            }
            else
            {
                this.StatusLabel.ForeColor = Control.DefaultForeColor;
            }
        }

        private void HelpButton_Clicked(object sender, CancelEventArgs e)
        {
            this.OpenDocumentation();
        }

        /// <summary>
        ///     Opens related best practice word document.
        /// </summary>
        private void OpenDocumentation()
        {
            string url = "https://mygainwell.sharepoint.com.mcas.ms/:w:/r/teams/hchppinnovations/_layouts/15/Doc.aspx?sourcedoc=%7BEAD58EC6-9825-4753-B8E7-AD2991BFE62E%7D&file=Manage%20Local%20Nuget%20Packages%20VS%20Extension.docx&action=default&mobileredirect=true";
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    System.Diagnostics.Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    System.Diagnostics.Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    System.Diagnostics.Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
