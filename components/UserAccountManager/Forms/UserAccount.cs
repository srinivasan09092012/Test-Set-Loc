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

                if (this.EditMode == EditModeType.Edit && this.UserProfile.ProfileId.ToString().CompareTo(Constants.InitializedGuid) == 0)
                {
                    this.DisplayMessage("This user does not have a profile. One will be created when you Save.", MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex);
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
            this.Close();
        }

        private void LoadAvailableRoles()
        {
            RolesListBox.Items.Clear();
            this.UserRoles.Sort(
                delegate (Role r1, Role r2)
                {
                    return r1.Name.CompareTo(r2.Name);
                }
            );
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
            LocaleTextBox.Text = this.UserProfile.LocaleId;
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
                    this.DisplayMessage("UserName already taken.", MessageBoxIcon.Warning);
                }
                else
                {
                    this.DisplayMessage("UserName is available.", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex);
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
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex);
            }
            finally
            {
                this.StatusStripLabel.Text = defaultStatus;
                this.Cursor = Cursors.Default;
            }
        }

        private DialogResult DisplayMessage(string msg, MessageBoxIcon type)
        {
            DialogResult result = new DialogResult();

            switch (type)
            {
                case MessageBoxIcon.Error:
                    MessageBox.Show(msg, "Error", MessageBoxButtons.OK, type);
                    break;

                case MessageBoxIcon.Warning:
                    MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, type);
                    break;

                case MessageBoxIcon.Question:
                    result = MessageBox.Show(msg, "Question", MessageBoxButtons.YesNo, type);
                    break;

                default:
                    MessageBox.Show(msg, "Information", MessageBoxButtons.OK, type);
                    break;
            }

            return result;
        }

        private void DisplayMessage(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                this.DisplayMessage("First Name is required.", MessageBoxIcon.Error);
                FirstNameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(LastNameTextBox.Text))
            {
                this.DisplayMessage("Last Name is required.", MessageBoxIcon.Error);
                LastNameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(DisplayNameTextBox.Text))
            {
                this.DisplayMessage("Display Name is required.", MessageBoxIcon.Error);
                DisplayNameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(LocaleTextBox.Text))
            {
                this.DisplayMessage("Locale is required.", MessageBoxIcon.Error);
                LocaleTextBox.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                this.DisplayMessage("Email is required.", MessageBoxIcon.Error);
                EmailTextBox.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(UserNameTextBox.Text))
            {
                this.DisplayMessage("User Name is required.", MessageBoxIcon.Error);
                UserNameTextBox.Focus();
                return false;
            }

            if(this.EditMode == EditModeType.Add)
            {
                UserAccount userAccount = adQueryProvider.GetUser(UserNameTextBox.Text);
                if (userAccount != null)
                {
                    this.DisplayMessage("UserName already taken.", MessageBoxIcon.Error);
                    UserNameTextBox.Focus();
                    UserNameTextBox.SelectAll();
                    return false;
                }

                if (string.IsNullOrEmpty(PasswordTextBox.Text))
                {
                    this.DisplayMessage("Password is required.", MessageBoxIcon.Error);
                    PasswordTextBox.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(ConfirmPasswordTextBox.Text))
                {
                    this.DisplayMessage("Confirm Password is required.", MessageBoxIcon.Error);
                    ConfirmPasswordTextBox.Focus();
                    return false;
                }

                if (PasswordTextBox.Text.CompareTo(ConfirmPasswordTextBox.Text) != 0)
                {
                    this.DisplayMessage("Password and Confirm Password must match.", MessageBoxIcon.Error);
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
                        this.DisplayMessage("Password is required.", MessageBoxIcon.Error);
                        PasswordTextBox.Focus();
                        PasswordTextBox.SelectAll();
                        return false;
                    }

                    if (string.IsNullOrEmpty(ConfirmPasswordTextBox.Text))
                    {
                        this.DisplayMessage("Confirm Password is required.", MessageBoxIcon.Error);
                        ConfirmPasswordTextBox.Focus();
                        ConfirmPasswordTextBox.SelectAll();
                        return false;
                    }

                    if (PasswordTextBox.Text.CompareTo(ConfirmPasswordTextBox.Text) != 0)
                    {
                        this.DisplayMessage("Password and Confirm Password must match.", MessageBoxIcon.Error);
                        PasswordTextBox.Focus();
                        PasswordTextBox.SelectAll();
                        return false;
                    }
                }
            }

            if (RolesListBox.SelectedIndices.Count == 0)
            {
                this.DisplayMessage("A minimum of one Role must be selected.", MessageBoxIcon.Error);
                RolesListBox.Focus();
                return false;
            }

            return isValid;
        }

        private void SaveUserAccount()
        {
            UserProfile newUserProfile = new UserProfile()
            {
                ProfileId = this.UserProfile.ProfileId,
                DisplayName = DisplayNameTextBox.Text.Trim(),
                EmailAddress = EmailTextBox.Text.Trim(),
                FirstName = FirstNameTextBox.Text.Trim(),
                LastName = LastNameTextBox.Text.Trim(),
                LocaleId = LocaleTextBox.Text.Trim(),
                MiddleName = MiddleNameTextBox.Text.Trim(),
                PhoneNumber = PhoneTextBox.Text.Trim(),
                TenantId = Guid.Parse(EnvConfig.TenantId),
                UserName = UserNameTextBox.Text.Trim(),
                VosTags = new List<string>(VosTagsTextBox.Text.Trim().Split(','))
            };

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

            UserQueryServiceProvider qryProvider = new UserQueryServiceProvider(this.EnvConfig);
            UserServiceProvider cmdProvider = new UserServiceProvider(this.EnvConfig);
            if (this.EditMode == EditModeType.Add)
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
                adManagementProvider.AddUser(newUserAccount);
            }
            else
            {
                if (this.UserProfile.ProfileId.ToString().CompareTo(Constants.InitializedGuid) == 0)
                {
                    cmdProvider.AddProfile(newUserProfile);
                }
                else
                {
                    cmdProvider.UpdateProfile(newUserProfile);
                }

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
            }
        }
    }
}
