using HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces;
using HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using UserAccountManager.Domain;
using UserAccountManager.Providers;
using static UserAccountManager.Domain.Enumerations;

namespace UserAccountManager.Forms
{
    public partial class UserAccountForm : Form
    {
        public Domain.Environment EnvConfig { get; set; }

        public UserAccount UserAccount { get; set; }

        public UserProfile UserProfile { get; set; }

        public List<Role> UserRoles { get; set; }

        public EditModeType EditMode { get; set; }

        public IUserQueryProvider adQueryProvider { get; set; }

        public IUserManagementProvider adManagementProvider { get; set; }

        public bool IsCanceled { get; set; }

        private static string defaultStatus = "Enter the user account details and click Save.";

        public UserAccountForm()
        {
            InitializeComponent();
        }

        private void UserAccountForm_Load(object sender, EventArgs e)
        {
        }

        private void UserAccountForm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.StatusStripLabel.Text = "Initializing user account...";
                this.InitializeForm();
                if (this.EditMode == EditModeType.Edit)
                {
                    this.LoadUserAccount();
                }
                if (this.EditMode == EditModeType.Add)
                {
                    CreateUserProfileCheckBox.Enabled = true;
                }
                else if (this.EditMode == EditModeType.Edit && this.UserProfile.ProfileId.ToString().CompareTo(Constants.InitializedGuid) == 0)
                {
                    CreateUserProfileCheckBox.Enabled = true;
                    FormHelper.DisplayMessage("This user does not have a profile. Check the 'Create user profile' checkbox to create one on Save.", MessageBoxIcon.Warning);
                }
                else
                {
                    CreateUserProfileCheckBox.Checked = true;
                }
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.IsCanceled = true;
            this.Hide();
        }

        private void LoadAvailableRoles()
        {
            RolesListBox.Items.Clear();
            if (this.UserRoles != null && this.UserRoles.Count > 0)
            {
                foreach (Role role in this.UserRoles)
                {
                    RolesListBox.Items.Add(role.Name);
                }
            }
        }

        private void LoadUserAccount()
        {
            FirstNameTextBox.Text = this.UserAccount.Identity.FirstName;
            MiddleNameTextBox.Text = this.UserAccount.Identity.MiddleName;
            LastNameTextBox.Text = this.UserAccount.Identity.LastName;
            if (string.IsNullOrEmpty(LastNameTextBox.Text))
            {
                LastNameTextBox.Text = "Account";
            }
            DisplayNameTextBox.Text = this.UserAccount.Identity.DisplayName;
            EmailTextBox.Text = this.UserAccount.Identity.EmailAddress;
            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                EmailTextBox.Text = string.Format("maps_{0}@dxc.com", this.UserAccount.Identity.UserName);
            }
            PhoneTextBox.Text = this.UserAccount.Identity.PhoneNumber;
            UserNameTextBox.Text = this.UserAccount.Identity.UserName;
            PasswordTextBox.Text = string.Empty;
            ConfirmPasswordTextBox.Text = string.Empty;
            foreach (string role in this.UserAccount.Roles)
            {
                for (int i = 0; i < RolesListBox.Items.Count; i++)
                {
                    string item = RolesListBox.Items[i].ToString();
                    if (item.ToString().Equals(role))
                    {
                        RolesListBox.SetSelected(i, true);
                    }
                }
            }

            LocaleTextBox.Text = this.UserProfile.LocaleId;
            ExternalIdTtextBox.Text = this.UserProfile.ExternalId;
            if (this.UserProfile.VosTags.Count > 0)
            {
                StringBuilder vosTags = new StringBuilder();
                foreach (string tag in this.UserProfile.VosTags)
                {
                    if (vosTags.Length > 0)
                    {
                        vosTags.Append(", ");
                    }
                    vosTags.Append(tag);
                }

                VosTagsTextBox.Text = vosTags.ToString();
            }
        }

        private void UserNameTextBox_TextChanged(object sender, EventArgs e)
        {
            CheckAvailableButton.Enabled = UserNameTextBox.Text.Trim().Length > 0;
        }

        private void UserNameTextBox_Leave(object sender, EventArgs e)
        {
            UserNameTextBox.Text = UserNameTextBox.Text.Trim();
        }

        private void CheckAvailableButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.StatusStripLabel.Text = "Checking user id availability in AD...";
                Application.DoEvents();
                UserAccount userAccount = adQueryProvider.GetUser(UserNameTextBox.Text);
                if (userAccount != null)
                {
                    FormHelper.DisplayMessage("UserName already taken.", MessageBoxIcon.Warning);
                }
                else
                {
                    FormHelper.DisplayMessage("UserName is available.", MessageBoxIcon.Information);
                }
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

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if(this.EditMode == EditModeType.Add)
                {
                    this.StatusStripLabel.Text = "Adding the user account...";
                }
                else
                {
                    this.StatusStripLabel.Text = "Updating the user account...";
                }
                Application.DoEvents();
                if (this.IsUserAccountValid())
                {
                    this.SaveUserAccount();
                    this.IsCanceled = false;
                    this.Close();
                }
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

        private void InitializeForm()
        {
            if (this.EditMode == EditModeType.Add)
            {
                this.UserNameTextBox.Enabled = true;
                this.PasswordLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
                this.ConfirmPasswordLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
                this.CheckAvailableButton.Visible = true;
            }
            else
            {
                this.UserNameTextBox.Enabled = false;
                this.PasswordLabel.ForeColor = System.Drawing.SystemColors.ControlText;
                this.ConfirmPasswordLabel.ForeColor = System.Drawing.SystemColors.ControlText;
                this.CheckAvailableButton.Visible = false;
            }

            this.IsCanceled = true;
            this.LoadAvailableRoles();
            this.AcceptButton = SaveButton;
            this.FirstNameTextBox.Focus();
            this.FirstNameTextBox.SelectAll();
        }

        private bool IsUserAccountValid()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(FirstNameTextBox.Text))
            {
                FormHelper.DisplayMessage("First Name is required.", MessageBoxIcon.Error);
                FirstNameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(LastNameTextBox.Text))
            {
                FormHelper.DisplayMessage("Last Name is required.", MessageBoxIcon.Error);
                LastNameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(DisplayNameTextBox.Text))
            {
                FormHelper.DisplayMessage("Display Name is required.", MessageBoxIcon.Error);
                DisplayNameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                FormHelper.DisplayMessage("Email is required.", MessageBoxIcon.Error);
                EmailTextBox.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(UserNameTextBox.Text))
            {
                FormHelper.DisplayMessage("User Name is required.", MessageBoxIcon.Error);
                UserNameTextBox.Focus();
                return false;
            }

            if(this.EditMode == EditModeType.Add)
            {
                UserAccount userAccount = adQueryProvider.GetUser(UserNameTextBox.Text);
                if (userAccount != null)
                {
                    FormHelper.DisplayMessage("UserName already taken.", MessageBoxIcon.Error);
                    UserNameTextBox.Focus();
                    UserNameTextBox.SelectAll();
                    return false;
                }

                if (string.IsNullOrEmpty(PasswordTextBox.Text))
                {
                    FormHelper.DisplayMessage("Password is required.", MessageBoxIcon.Error);
                    PasswordTextBox.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(ConfirmPasswordTextBox.Text))
                {
                    FormHelper.DisplayMessage("Confirm Password is required.", MessageBoxIcon.Error);
                    ConfirmPasswordTextBox.Focus();
                    return false;
                }

                if (PasswordTextBox.Text.CompareTo(ConfirmPasswordTextBox.Text) != 0)
                {
                    FormHelper.DisplayMessage("Password and Confirm Password must match.", MessageBoxIcon.Error);
                    PasswordTextBox.Focus();
                    PasswordTextBox.SelectAll();
                    return false;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(PasswordTextBox.Text) || !string.IsNullOrEmpty(ConfirmPasswordTextBox.Text))
                {
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
                }
            }

            if (RolesListBox.SelectedIndices.Count == 0)
            {
                FormHelper.DisplayMessage("A minimum of one Role must be selected.", MessageBoxIcon.Error);
                RolesListBox.Focus();
                return false;
            }

            if (this.CreateUserProfileCheckBox.Checked)
            {
                if (string.IsNullOrEmpty(LocaleTextBox.Text))
                {
                    FormHelper.DisplayMessage("Locale is required.", MessageBoxIcon.Error);
                    LocaleTextBox.Focus();
                    return false;
                }
            }

            return isValid;
        }

        private void SaveUserAccount()
        {
            UserAccount newUserAccount = new UserAccount()
            {
                Identity = new UserIdentity()
                {
                    DisplayName = DisplayNameTextBox.Text.Trim(),
                    EmailAddress = string.IsNullOrEmpty(EmailTextBox.Text) ? null : EmailTextBox.Text.Trim(),
                    FirstName = FirstNameTextBox.Text.Trim(),
                    LastName = LastNameTextBox.Text.Trim(),
                    MiddleName = string.IsNullOrEmpty(MiddleNameTextBox.Text) ? null : MiddleNameTextBox.Text.Trim(),
                    PhoneNumber = string.IsNullOrEmpty(PhoneTextBox.Text) ? null : PhoneTextBox.Text.Trim(),
                    UserName = UserNameTextBox.Text.Trim(),
                },
                IsEnabled = true,
                PasswordNeverExpires = true,
                Roles = new List<string>()
            };

            foreach (var item in RolesListBox.SelectedItems)
            {
                newUserAccount.Roles.Add(item.ToString());
            }

            if (!string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                newUserAccount.Password = PasswordTextBox.Text;
            }

            UserProfile newUserProfile = new UserProfile()
            {
                ProfileId = this.UserProfile.ProfileId,
                DisplayName = DisplayNameTextBox.Text.Trim(),
                EmailAddress = EmailTextBox.Text.Trim(),
                ExternalId = ExternalIdTtextBox.Text.Trim(),
                FirstName = FirstNameTextBox.Text.Trim(),
                LastName = LastNameTextBox.Text.Trim(),
                LocaleId = LocaleTextBox.Text.Trim(),
                MiddleName = MiddleNameTextBox.Text.Trim(),
                PhoneNumber = PhoneTextBox.Text.Trim(),
                TenantId = Guid.Parse(EnvConfig.TenantId),
                UserName = UserNameTextBox.Text.Trim(),
                VosTags = new List<string>(VosTagsTextBox.Text.Trim().Split(','))
            };

            UserQueryServiceProvider qryProvider = new UserQueryServiceProvider(this.EnvConfig);
            UserServiceProvider cmdProvider = new UserServiceProvider(this.EnvConfig);
            if (this.EditMode == EditModeType.Add)
            {
                adManagementProvider.AddUser(newUserAccount);
                if (CreateUserProfileCheckBox.Checked)
                {
                    UserProfile existingUserProfile = qryProvider.LoadUserProfile(newUserProfile.UserName);
                    if (existingUserProfile != null)
                    {
                        newUserProfile.ProfileId = existingUserProfile.ProfileId;
                        cmdProvider.UpdateProfile(newUserProfile);
                    }
                    else
                    {
                        cmdProvider.AddProfile(newUserProfile);
                    }
                }
            }
            else
            {
                adManagementProvider.UpdateUser(newUserAccount.Identity);

                if (!string.IsNullOrEmpty(newUserAccount.Password))
                {
                    adManagementProvider.ResetUserPassword(newUserAccount.Identity.UserName, newUserAccount.Password);
                    adManagementProvider.UnlockUser(newUserAccount.Identity.UserName);
                }

                foreach (string curRole in this.UserAccount.Roles)
                {
                    if (curRole.StartsWith(Constants.RolePrefix))
                    {
                        if (!newUserAccount.Roles.Contains(curRole))
                        {
                            adManagementProvider.RemoveUserFromRole(newUserAccount.Identity.UserName, curRole);
                        }
                    }
                }

                foreach (string newRole in newUserAccount.Roles)
                {
                    if (!this.UserAccount.Roles.Contains(newRole))
                    {
                        adManagementProvider.AddUserToRole(newUserAccount.Identity.UserName, newRole);
                    }
                }

                if (CreateUserProfileCheckBox.Checked)
                {
                    if (this.UserProfile.ProfileId.ToString().CompareTo(Constants.InitializedGuid) == 0)
                    {
                        cmdProvider.AddProfile(newUserProfile);
                    }
                    else
                    {
                        cmdProvider.UpdateProfile(newUserProfile);
                    }
                }
            }
        }

        private void CreateUserProfileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ProfileGroupBox.Enabled = CreateUserProfileCheckBox.Checked;
        }
    }
}
