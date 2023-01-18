using HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces;
using HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain;
using HPE.HSP.UA3.Core.API.IdentityManagement.Providers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UserAccountManager.Domain;
using UserAccountManager.Providers;

namespace UserAccountManager.Forms
{
    public partial class CloneToEnvironmentForm : Form
    {
        public List<string> SelectedUserNames { get; set; }

        public Domain.EnvironmentsConfig EnvConfigs { get; set; }

        public Domain.Environment SourceEnvConfig { get; set; }

        public List<Group> SourceEnvGroups { get; set; }

        private static IUserQueryProvider adQueryProvider = null;
        private static IUserManagementProvider adManagementProvider = null;
        private static List<Group> targetEnvGroups = null;
        private static Domain.Environment targetEnvConfig = null;
        private string defaultStatus = "Select environment and password and click Clone.";

        public CloneToEnvironmentForm()
        {
            InitializeComponent();
        }

        private void CloneToEnvironmentForm_Shown(object sender, EventArgs e)
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

        private void EnvComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.StatusStripLabel.Text = "Initializing environment...";
                Application.DoEvents();
                if (EnvComboBox.SelectedIndex > 0)
                {
                    targetEnvConfig = this.EnvConfigs.Environments.Find(ec => ec.Name == EnvComboBox.Text);
                    targetEnvGroups = this.LoadEnvGroups(targetEnvConfig);
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

        private List<Group> LoadEnvGroups(Domain.Environment envConfig)
        {
            adQueryProvider = new ActiveDirectoryQueryProvider(envConfig.ADServer, envConfig.ADUser, envConfig.ADPassword, envConfig.ADContainer);
            SearchGroupsRequest rolesRequest = new SearchGroupsRequest()
            {
                PagingCriteria = new PagingCriteria()
                {
                    Page = 1,
                    RowsPerPage = 200
                }
            };
            SearchGroupsResponse rolesResponse = adQueryProvider.SearchGroups(rolesRequest);
            return rolesResponse.Groups;
        }

        private void CloneButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.IsFormValid())
                {
                    this.StatusStripLabel.Text = "Cloning selected user account(s)...";
                    int count = this.SelectedUserNames.Count;
                    this.ProgressBar.Value = 0;
                    this.ProgressBar.Maximum = count;
                    this.ProgressBar.Visible = true;
                    Application.DoEvents();

                    int idx = 1;
                    foreach (string userName in this.SelectedUserNames)
                    {
                        this.StatusStripLabel.Text = string.Format("Cloning user account {0}: ({1} of {2})...", userName, idx.ToString("#,##0"), count.ToString("#,##0"));
                        this.ProgressBar.Value = idx;
                        Application.DoEvents();

                        this.CloneUserAccount(userName);
                        this.CloneUserProfile(userName);
                        idx++;
                    }

                    FormHelper.DisplayMessage(string.Format("User accounts successfully cloned to environment {0}.", EnvComboBox.Text), MessageBoxIcon.Information);
                    this.ProgressBar.Visible = false;
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

        private void CloneUserAccount(string userName)
        {
            // Get source user account
            adQueryProvider = new ActiveDirectoryQueryProvider(this.SourceEnvConfig.ADServer, this.SourceEnvConfig.ADUser, this.SourceEnvConfig.ADPassword, this.SourceEnvConfig.ADContainer);
            UserAccount sourceUserAccount = adQueryProvider.GetUser(userName);
            if (sourceUserAccount != null)
            {
                // Check for target user account
                adQueryProvider = new ActiveDirectoryQueryProvider(targetEnvConfig.ADServer, targetEnvConfig.ADUser, targetEnvConfig.ADPassword, targetEnvConfig.ADContainer);
                adManagementProvider = new ActiveDirectoryProvider(targetEnvConfig.ADServer, targetEnvConfig.ADUser, targetEnvConfig.ADPassword, targetEnvConfig.ADContainer);
                UserAccount targetUserAccount = adQueryProvider.GetUser(userName);

                if (targetUserAccount == null)
                {
                    // Add target user account
                    targetUserAccount = sourceUserAccount;
                    if (string.IsNullOrEmpty(targetUserAccount.Identity.DisplayName))
                    {
                        targetUserAccount.Identity.DisplayName = targetUserAccount.Identity.LastName + ", " + targetUserAccount.Identity.FirstName;
                    }
                    if (string.IsNullOrEmpty(targetUserAccount.Identity.EmailAddress))
                    {
                        targetUserAccount.Identity.EmailAddress = "maps_" + targetUserAccount.Identity.UserName + "@dxc.com";
                    }
                    targetUserAccount.Password = PasswordTextBox.Text;
                    foreach (string groupName in targetUserAccount.Groups)
                    {
                        if (targetEnvGroups.Find(r => r.Name.CompareTo(groupName) == 0) == null)
                        {
                            adManagementProvider.AddGroup(groupName);
                        }
                    }

                    adManagementProvider.AddUser(targetUserAccount);
                }
            }
            else
            {
                string msg = string.Format("User account {0} not found in environment {1}. Process stopping.", userName, this.SourceEnvConfig.Name);
                throw new ApplicationException(msg);
            }
        }

        private void CloneUserProfile(string userName)
        {
            IUserQueryServiceProvider srcQryProvider = null;
            if (this.SourceEnvConfig.ServiceVersion == 1)
            {
                srcQryProvider = new UserQueryServiceProvider1(this.SourceEnvConfig);
            }
            else
            {
                srcQryProvider = new UserQueryServiceProvider2(this.SourceEnvConfig);
            }

            IUserQueryServiceProvider trgQryProvider = null;
            IUserServiceProvider trgCmdProvider = null;
            if (targetEnvConfig.ServiceVersion == 1)
            {
                trgQryProvider = new UserQueryServiceProvider1(targetEnvConfig);
                trgCmdProvider = new UserServiceProvider1(targetEnvConfig);
            }
            else
            {
                trgQryProvider = new UserQueryServiceProvider2(targetEnvConfig);
                trgCmdProvider = new UserServiceProvider2(targetEnvConfig);
            }

            // Get source user profile
            UserProfile sourceUserProfile = srcQryProvider.LoadUserProfile(userName);
            if (sourceUserProfile != null)
            {
                // Check for target user profile
                UserProfile targetUserProfile = trgQryProvider.LoadUserProfile(userName);
                if (targetUserProfile == null)
                {
                    //Copy source profile and wipe out profile ID
                    targetUserProfile = sourceUserProfile;
                    targetUserProfile.ProfileId = Guid.Empty;

                    // Add target user profile
                    trgCmdProvider.AddProfile(targetUserProfile);
                }
                else
                {
                    //Copy source profile and save off original profile ID
                    targetUserProfile.DisplayName = sourceUserProfile.DisplayName;
                    targetUserProfile.EmailAddress = sourceUserProfile.EmailAddress;
                    targetUserProfile.FirstName = sourceUserProfile.FirstName;
                    targetUserProfile.GeneralId = sourceUserProfile.GeneralId;
                    targetUserProfile.IsAccountVerified = sourceUserProfile.IsAccountVerified;
                    targetUserProfile.IsActive = sourceUserProfile.IsActive;
                    targetUserProfile.LastName = sourceUserProfile.LastName;
                    targetUserProfile.MiddleName = sourceUserProfile.MiddleName;
                    targetUserProfile.PhoneNumber = sourceUserProfile.PhoneNumber;
                    targetUserProfile.TenantId = sourceUserProfile.TenantId;
                    targetUserProfile.UserName = sourceUserProfile.UserName;

                    foreach (UserVOSTag tag in sourceUserProfile.VOSTags)
                    {
                        if (targetUserProfile.VOSTags.Find(t => t.Code.ToLower() == tag.Code.ToLower() && t.TypeCode.ToLower() == tag.TypeCode.ToLower() && t.EffectiveDate.CompareTo(tag.EffectiveDate) == 0 && t.EndDate.CompareTo(tag.EndDate) == 0) == null)
                        {
                            targetUserProfile.VOSTags.Add(new UserVOSTag(Guid.Empty, tag.Code, tag.TypeCode, tag.EffectiveDate, tag.EndDate));
                        }
                    }

                    // Update target user profile
                    trgCmdProvider.UpdateProfile(targetUserProfile, sourceUserProfile);
                }
            }
        }

        private void InitializeForm()
        {
            EnvComboBox.Items.Clear();
            EnvComboBox.Items.Add(string.Empty);
            foreach (Domain.Environment env in this.EnvConfigs.Environments)
            {
                if (env.Name.CompareTo(this.SourceEnvConfig.Name) != 0)
                {
                    EnvComboBox.Items.Add(env.Name);
                }
            }
            ProgressBar.Visible = false;
            ProgressBar.Maximum = this.SelectedUserNames.Count;

            EnvComboBox.Focus();
        }

        private bool IsFormValid()
        {
            bool isValid = true;

            if (EnvComboBox.SelectedIndex <=0 )
            {
                FormHelper.DisplayMessage("Environment is required.", MessageBoxIcon.Error);
                EnvComboBox.Focus();
                return false;
            }

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
