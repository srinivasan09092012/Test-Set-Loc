﻿using HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces;
using HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UserAccountManager.Domain;
using UserAccountManager.Providers;
using UserAccountManager.Utilities;
using static UserAccountManager.Domain.Enumerations;
using api = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain;

namespace UserAccountManager.Forms
{
    public partial class UserAccountForm : Form
    {
        public Domain.Environment EnvConfig { get; set; }

        public UserAccount UserAccount { get; set; }

        public UserProfile UserProfile { get; set; }

        public List<Group> UserGroups { get; set; }

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
                this.VOSTagsBindingSource.DataSource = Serializer.Clone<List<UserVOSTag>>(this.UserProfile.VOSTags);
                this.GeneralIdLabel.Enabled = this.EnvConfig.ServiceVersion > 1;
                this.GeneralIdTtextBox.Enabled = this.EnvConfig.ServiceVersion > 1;
                this.VOSTagsLabel.Enabled = this.EnvConfig.ServiceVersion > 1;
                this.VOSTagsDataGridView.Enabled = this.EnvConfig.ServiceVersion > 1;
                this.RegQualifiersBindingSource.DataSource = this.UserProfile.RegQualifiers;
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

        private void LoadAvailableGroups()
        {
            GroupsListBox.Items.Clear();
            if (this.UserGroups != null && this.UserGroups.Count > 0)
            {
                foreach (Group role in this.UserGroups)
                {
                    GroupsListBox.Items.Add(role.Name);
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
            if (this.UserAccount.AccountExpirationDate.HasValue)
            {
                AccountExpirationDateTextBox.Text = this.UserAccount.AccountExpirationDate.Value.ToLocalTime().ToString("MM/dd/yyyy");
            }

            UserNameTextBox.Text = this.UserAccount.Identity.UserName;
            ExpiredCheckbox.Checked = this.UserAccount.IsExpired;
            LockedCheckbox.Checked = this.UserAccount.IsLockedOut;

            PasswordTextBox.Text = string.Empty;
            ConfirmPasswordTextBox.Text = string.Empty;
            foreach (string role in this.UserAccount.Groups)
            {
                for (int i = 0; i < GroupsListBox.Items.Count; i++)
                {
                    string item = GroupsListBox.Items[i].ToString();
                    if (item.ToString().Equals(role))
                    {
                        GroupsListBox.SetSelected(i, true);
                    }
                }
            }

            LocaleTextBox.Text = this.UserProfile.LocaleId;
            GeneralIdTtextBox.Text = this.UserProfile.GeneralId;
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
            this.LoadAvailableGroups();
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

            if (!string.IsNullOrEmpty(PhoneTextBox.Text) && PhoneTextBox.Text.Length != 10)
            {
                FormHelper.DisplayMessage("Phone number must be 10 digits.", MessageBoxIcon.Error);
                UserNameTextBox.Focus();
                return false;
            }

            if (AccountExpirationDateTextBox.Text != "  /  /")
            {
                DateTime accountExpirationDate;
                bool isDateValid = DateTime.TryParse(AccountExpirationDateTextBox.Text, out accountExpirationDate);

                if (!isDateValid)
                {
                    FormHelper.DisplayMessage("Account expiration date must be a valid date.", MessageBoxIcon.Error);
                    UserNameTextBox.Focus();
                    return false;
                }
            }

            if (this.EditMode == EditModeType.Add)
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

            if (GroupsListBox.SelectedIndices.Count == 0)
            {
                FormHelper.DisplayMessage("A minimum of one Group must be selected.", MessageBoxIcon.Error);
                GroupsListBox.Focus();
                return false;
            }

            int idx = 1;
            List<Domain.RegistrationQualifier> qualifiers = (List<Domain.RegistrationQualifier>)RegQualifiersBindingSource.DataSource;
            foreach (Domain.RegistrationQualifier qualifier in qualifiers)
            {
                if (string.IsNullOrEmpty(qualifier.Key))
                {
                    FormHelper.DisplayMessage(string.Format("Reg Qualifier Key {0} is required.", idx.ToString()), MessageBoxIcon.Error);
                    RegQualifiersDataGridView.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(qualifier.Value))
                {
                    FormHelper.DisplayMessage(string.Format("Reg Qualifier Value {0} is required.", idx.ToString()), MessageBoxIcon.Error);
                    RegQualifiersDataGridView.Focus();
                    return false;
                }

                idx++;
            }

            if (this.CreateUserProfileCheckBox.Checked)
            {
                if (string.IsNullOrEmpty(LocaleTextBox.Text))
                {
                    FormHelper.DisplayMessage("Locale is required.", MessageBoxIcon.Error);
                    LocaleTextBox.Focus();
                    return false;
                }

                if (VOSTagsDataGridView.Enabled)
                {
                    idx = 1;
                    List<UserVOSTag> gridTags = (List<UserVOSTag>)VOSTagsBindingSource.DataSource;
                    foreach (UserVOSTag tag in gridTags)
                    {
                        if (string.IsNullOrEmpty(tag.Code))
                        {
                            FormHelper.DisplayMessage(string.Format("VOS Tag Code {0} is required.", idx.ToString()), MessageBoxIcon.Error);
                            VOSTagsDataGridView.Focus();
                            return false;
                        }

                        if (string.IsNullOrEmpty(tag.TypeCode))
                        {
                            FormHelper.DisplayMessage(string.Format("VOS Tag Type Code {0} is required.", idx.ToString()), MessageBoxIcon.Error);
                            VOSTagsDataGridView.Focus();
                            return false;
                        }

                        if (tag.EffectiveDate == DateTime.MinValue)
                        {
                            FormHelper.DisplayMessage(string.Format("VOS Tag Effective Date {0} is required.", idx.ToString()), MessageBoxIcon.Error);
                            VOSTagsDataGridView.Focus();
                            return false;
                        }

                        if (tag.EndDate == DateTime.MinValue)
                        {
                            FormHelper.DisplayMessage(string.Format("VOS Tag End Date {0} is required.", idx.ToString()), MessageBoxIcon.Error);
                            VOSTagsDataGridView.Focus();
                            return false;
                        }

                        if (tag.EndDate.CompareTo(tag.EffectiveDate) < 0)
                        {
                            FormHelper.DisplayMessage(string.Format("VOS Tag End Date {0} must come after Effective Date {0}.", idx.ToString()), MessageBoxIcon.Error);
                            VOSTagsDataGridView.Focus();
                            return false;
                        }

                        idx++;
                    }
                }
            }

            return isValid;
        }

        private void SaveUserAccount()
        {
            List<api.RegistrationQualifier> accountQualifiers = new List<api.RegistrationQualifier>();
            List<Domain.RegistrationQualifier> profileQualifiers = ((List<Domain.RegistrationQualifier>)RegQualifiersBindingSource.DataSource);
            foreach (Domain.RegistrationQualifier qualifier in profileQualifiers)
            {
                accountQualifiers.Add(new api.RegistrationQualifier() { Key = qualifier.Key, Value = qualifier.Value });
            }

            DateTime? accountExpirationDate = null;
            if (AccountExpirationDateTextBox.Text != "  /  /")
            {
                DateTime date = Convert.ToDateTime(AccountExpirationDateTextBox.Text);
                accountExpirationDate = DateTime.SpecifyKind(date, DateTimeKind.Local);
            }

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
                    UserName = UserNameTextBox.Text.Trim()
                },
                AccountExpirationDate = accountExpirationDate,
                IsEnabled = true,
                PasswordNeverExpires = true,
                Groups = new List<string>(),
                RegistrationQualifiers = accountQualifiers
            };

            foreach (var item in GroupsListBox.SelectedItems)
            {
                newUserAccount.Groups.Add(item.ToString());
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
                FirstName = FirstNameTextBox.Text.Trim(),
                GeneralId = GeneralIdTtextBox.Text.Trim(),
                IsAccountVerified = true,
                IsActive = true,
                LastName = LastNameTextBox.Text.Trim(),
                LocaleId = LocaleTextBox.Text.Trim(),
                MiddleName = MiddleNameTextBox.Text.Trim(),
                PhoneNumber = PhoneTextBox.Text.Trim(),
                TenantId = Guid.Parse(EnvConfig.TenantId),
                UserName = UserNameTextBox.Text.Trim(),
                RegQualifiers = profileQualifiers,
                VOSTags = (List<UserVOSTag>)VOSTagsBindingSource.DataSource                
            };

            IUserQueryServiceProvider qryProvider = null;
            IUserServiceProvider cmdProvider = null;

            if (this.EnvConfig.ServiceVersion == 1)
            {
                qryProvider = new UserQueryServiceProvider1(this.EnvConfig);
                cmdProvider = new UserServiceProvider1(this.EnvConfig);
            }
            else
            {
                qryProvider = new UserQueryServiceProvider2(this.EnvConfig);
                cmdProvider = new UserServiceProvider2(this.EnvConfig);
            }

            if (this.EditMode == EditModeType.Add)
            {
                adManagementProvider.AddUser(newUserAccount);
                if (CreateUserProfileCheckBox.Checked)
                {
                    UserProfile existingUserProfile = qryProvider.LoadUserProfile(newUserProfile.UserName);
                    if (existingUserProfile != null)
                    {
                        newUserProfile.ProfileId = existingUserProfile.ProfileId;
                        cmdProvider.UpdateProfile(newUserProfile, this.UserProfile);
                    }
                    else
                    {
                        cmdProvider.AddProfile(newUserProfile);
                    }
                }
            }
            else
            {
                adManagementProvider.UpdateUser(newUserAccount.Identity, newUserAccount.RegistrationQualifiers, accountExpirationDate);

                if (!string.IsNullOrEmpty(newUserAccount.Password))
                {
                    adManagementProvider.ResetUserPassword(newUserAccount.Identity.UserName, newUserAccount.Password);
                    adManagementProvider.UnlockUser(newUserAccount.Identity.UserName);
                }

                foreach (string curGroup in this.UserAccount.Groups)
                {
                    if (!newUserAccount.Groups.Contains(curGroup))
                    {
                        adManagementProvider.RemoveUserFromGroup(newUserAccount.Identity.UserName, curGroup);
                    }
                }

                foreach (string newGroup in newUserAccount.Groups)
                {
                    if (!this.UserAccount.Groups.Contains(newGroup))
                    {
                        adManagementProvider.AddUserToGroup(newUserAccount.Identity.UserName, newGroup);
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
                        cmdProvider.UpdateProfile(newUserProfile, this.UserProfile);
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
