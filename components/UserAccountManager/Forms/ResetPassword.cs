using HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces;
using HPE.HSP.UA3.Core.API.IdentityManagement.Providers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UserAccountManager.Forms
{
    public partial class ResetPasswordForm : Form
    {
        public List<string> SelectedUserNames { get; set; }

        public Domain.Environment EnvConfig { get; set; }

        private static IUserManagementProvider adManagementProvider = null;
        private string defaultStatus = "Enter password and click Reset.";

        public ResetPasswordForm()
        {
            InitializeComponent();
        }

        private void ResetPassword_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.StatusStripLabel.Text = "Initializing...";
                Application.DoEvents();
                this.InitializeForm();
            }
            catch (Exception ex)
            {
                FormHelper.DisplayMessage(ex);
            }
            finally
            {
                this.StatusStripLabel.Text = defaultStatus;
                this.Cursor = Cursors.Default;
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.IsFormValid())
                {
                    this.StatusStripLabel.Text = "Reseting user account password(s)...";
                    int count = this.SelectedUserNames.Count;
                    this.ProgressBar.Value = 0;
                    this.ProgressBar.Maximum = count;
                    this.ProgressBar.Visible = true;
                    Application.DoEvents();

                    DialogResult result = FormHelper.DisplayMessage(string.Format("Are you sure you want to reset passwords for the {0} selected user account(s)?\r\n\r\nThe action is permanent and connot be undone.", this.SelectedUserNames.Count.ToString("#,##0")), MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        int idx = 1;
                        foreach (string userName in this.SelectedUserNames)
                        {
                            this.StatusStripLabel.Text = string.Format("Resetting password for user {0}: ({1} of {2})...", userName, idx.ToString("#,##0"), count.ToString("#,##0"));
                            this.ProgressBar.Value = idx;
                            Application.DoEvents();

                            adManagementProvider.ResetUserPassword(userName, PasswordTextBox.Text);
                            idx++;
                        }

                        FormHelper.DisplayMessage("User account passwords successfully reset.", MessageBoxIcon.Information);
                        this.ProgressBar.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                FormHelper.DisplayMessage(ex);
            }
            finally
            {
                this.StatusStripLabel.Text = defaultStatus;
                this.ProgressBar.Visible = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeForm()
        {
            adManagementProvider = new ActiveDirectoryProvider(this.EnvConfig.ADServer, this.EnvConfig.ADUser, this.EnvConfig.ADPassword, this.EnvConfig.ADContainer);
            PasswordTextBox.Focus();
        }

        private bool IsFormValid()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                FormHelper.DisplayMessage("Password is required.", MessageBoxIcon.Error);
                PasswordTextBox.Focus();
                PasswordTextBox.SelectAll();
                return false;
            }

            if (string.IsNullOrEmpty(ConfirmPasswordTextBox.Text))
            {
                FormHelper.DisplayMessage("Confirm Password is required.", MessageBoxIcon.Error);
                ConfirmPasswordTextBox.Focus();
                ConfirmPasswordTextBox.SelectAll();
                return false;
            }

            if (PasswordTextBox.Text.CompareTo(ConfirmPasswordTextBox.Text) != 0)
            {
                FormHelper.DisplayMessage("Password and Confirm Password must match.", MessageBoxIcon.Error);
                PasswordTextBox.Focus();
                PasswordTextBox.SelectAll();
                return false;
            }

            return isValid;
        }
    }
}
