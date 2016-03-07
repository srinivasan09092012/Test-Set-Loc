//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved. 
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------

using HP.HSP.UA3.Core.UX.Common.Utilities;
using HP.HSP.UA3.Utilities.ProjectSetupWizard.Common;
using HP.HSP.UA3.Utilities.ProjectSetupWizard.Data;
using System;
using System.IO;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.ProjectSetupWizard.Forms
{
    public partial class UserConfigForm : Form
    {
        public UserConfigForm()
        {
            InitializeComponent();
        }

        private void UserConfigForm_Load(object sender, EventArgs e)
        {
        }

        private void UserConfigForm_Shown(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                InitializeForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (IsFormValid())
                {
                    SaveUserConfig();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SourceDirectoryBrowseButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string path = MainForm.UserConfig.SourcePath;
                if (string.IsNullOrEmpty(path))
                {
                    path = "C:\\";
                }
                FolderBrowserDialog.SelectedPath = path;
                DialogResult result = FolderBrowserDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    SourceDirectoryTextbox.Text = FolderBrowserDialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void InitializeForm()
        {
            SourceDirectoryTextbox.Text = MainForm.UserConfig.SourcePath;
        }

        private bool IsFormValid()
        {
            if (!Directory.Exists(SourceDirectoryTextbox.Text))
            {
                MessageBox.Show("Select a valid directory where the TFS project source is located (i.e. 'C:\\USHC_AMERICAS_US_ADU_HSP_UA3\\Source')."
                    , this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                SourceDirectoryBrowseButton.Focus();
                return false;
            }
            return true;
        }

        private void SaveUserConfig()
        {
            MainForm.UserConfig = new UserConfigModel()
            {
                SourcePath = SourceDirectoryTextbox.Text
            };
            File.WriteAllText(Constants.Config.User.FileName, Serializer.XmlSerialize<UserConfigModel>(MainForm.UserConfig));
        }
    }
}