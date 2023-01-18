//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------

using HP.HSP.UA3.Utilities.ProjectSetupWizard.Common;
using System;
using System.IO;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.ProjectSetupWizard.Forms
{
    public partial class CreateNewApiForm : Form
    {
        public string ModuleName = string.Empty;
        public string ModulePath = string.Empty;
        public string TemplatePath = string.Empty;
        public bool WasCreated = false;

        public CreateNewApiForm()
        {
            InitializeComponent();
        }

        private void CreateNewApiForm_Shown(object sender, EventArgs e)
        {
            ApiNameTextbox.Focus();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (IsApiCreated())
                {
                    this.WasCreated = true;
                    this.Hide();
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

        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                this.Hide();
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

        private bool IsFormValid()
        {
            string apiName = ApiNameTextbox.Text.Trim();
            if (string.IsNullOrEmpty(apiName))
            {
                MessageBox.Show("API Name is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ApiNameTextbox.Focus();
                return false;
            }

            return true;
        }

        private bool IsApiCreated()
        {
            if (IsFormValid())
            {
                if (IsApiFolderRootAvailable())
                {
                    if (IsProjectDirectoryStructureCreated())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsApiFolderRootAvailable()
        {
            string apiName = ApiNameTextbox.Text.Trim();
            string apiRootPath = this.ModulePath + "\\" + apiName;
            if (Directory.Exists(apiRootPath))
            {
                MessageBox.Show(string.Format("API path '{0}' already exists. Select a new API Name or delete the existing one first.", apiRootPath.ToLower()), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ApiNameTextbox.Focus();
                return false;
            }
            return true;
        }

        private bool IsProjectDirectoryStructureCreated()
        {
            string apiName = ApiNameTextbox.Text.Trim();
            string apiRootPath = this.ModulePath + "\\" + apiName;
            string templateRootPath = this.TemplatePath + "\\[ApiName]";

            FileIOUtility.CreateServiceDirectory(templateRootPath, apiRootPath, this.ModuleName, apiName);

            return true;
        }
    }
}
