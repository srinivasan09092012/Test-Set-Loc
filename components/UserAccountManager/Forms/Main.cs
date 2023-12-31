﻿using HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces;
using HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain;
using HPE.HSP.UA3.Core.API.IdentityManagement.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UserAccountManager.Domain;
using UserAccountManager.Providers;
using UserAccountManager.Utilities;
using api = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain;

namespace UserAccountManager.Forms
{
    public partial class MainForm : Form
    {
        private static Domain.EnvironmentsConfig envConfigs = null;
        private static Domain.Environment currentEnvConfig = null;
        private static IUserQueryProvider adQueryProvider = null;
        private static IUserManagementProvider adManagementProvider = null;
        private static List<Group> envGroups = null;
        private static string defaultStatus = "Select an environment, enter search fields, and click Search. To delete an account, select the row and click Delete.";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.StatusStripLabel.Text = "Initializing...";
                Application.DoEvents();
                LoadConfigration();
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserNameTextBox_TextChanged(object sender, EventArgs e)
        {
            ToggleUserSearchButton();
        }

        private void UserGroupsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleUserSearchButton();
        }

        private void FirstNameTextBox_TextChanged(object sender, EventArgs e)
        {
            ToggleUserSearchButton();
        }

        private void LastNameTextBox_TextChanged(object sender, EventArgs e)
        {
            ToggleUserSearchButton();
        }

        private void PhoneNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            ToggleUserSearchButton();
        }

        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {
            ToggleUserSearchButton();
        }

        private void UserNameTextBox_Leave(object sender, EventArgs e)
        {
            UserNameTextBox.Text = UserNameTextBox.Text.Trim();
        }

        private void FirstNameTextBox_Leave(object sender, EventArgs e)
        {
            FirstNameTextBox.Text = FirstNameTextBox.Text.Trim();
        }

        private void LastNameTextBox_Leave(object sender, EventArgs e)
        {
            LastNameTextBox.Text = LastNameTextBox.Text.Trim();
        }

        private void PhoneNumberTextBox_Leave(object sender, EventArgs e)
        {
            PhoneNumberTextBox.Text = PhoneNumberTextBox.Text.Trim();
        }

        private void EmailTextBox_Leave(object sender, EventArgs e)
        {
            EmailTextBox.Text = EmailTextBox.Text.Trim();
        }

        private void AccountExpiredCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            ToggleUserSearchButton();
        }

        private void DisabledCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            ToggleUserSearchButton();
        }

        private void LockedOutCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            ToggleUserSearchButton();
        }

        private void UserSearchButton_Click(object sender, EventArgs e)
        {
            this.SearchUserAccounts();
        }

        private void UserAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.StatusStripLabel.Text = "Adding new user account...";
                Application.DoEvents();
                this.AddUserAccount();
                if (UserSearchButton.Enabled)
                {
                    this.SearchUserAccounts();
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

        private void EnvComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (EnvComboBox.SelectedIndex >= 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.StatusStripLabel.Text = "Initializing environment...";
                    Application.DoEvents();
                    this.ClearUserSearchResults();
                    this.InitializeEnvironment(EnvComboBox.Text);
                    this.TabControl.Enabled = true;
                    switch (this.TabControl.SelectedIndex)
                    {
                        case 0:
                            this.UserNameTextBox.Focus();
                            this.UserNameTextBox.SelectAll();
                            break;

                        case 1:
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    this.ClearUserSearchResults();
                    this.TabControl.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                EnvComboBox.SelectedIndex = -1;
                FormHelper.DisplayMessage(ex);
            }
            finally
            {
                this.StatusStripLabel.Text = defaultStatus;
                this.Cursor = Cursors.Default;
            }
        }

        private void UserResultsDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.StatusStripLabel.Text = "Deleting user account...";
                Application.DoEvents();
                if (currentEnvConfig.IsDeleteAllowed)
                {
                    string userName = e.Row.Cells[0].Value.ToString();
                    DialogResult result = FormHelper.DisplayMessage(string.Format("Are you sure you want to delete user account '{0}'?\r\n\r\nThe action is permanent and connot be undone.", userName), MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        this.DeleteUserAccount(userName);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    FormHelper.DisplayMessage("Deletes are not allowed in this environment.", MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                FormHelper.DisplayMessage(ex);
            }
            finally
            {
                this.StatusStripLabel.Text = defaultStatus;
                this.Cursor = Cursors.Default;
            }
        }

        private void UserResultsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (UserResultsDataGridView.Rows.Count > 0)
            {
                //if (e.RowIndex == -1 && e.ColumnIndex > 0)
                //{
                //    UserResultsDataGridView.Sort(UserResultsDataGridView.Columns[e.ColumnIndex], System.ComponentModel.ListSortDirection.Ascending);
                //}
            }
        }

        private void UserResultsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (UserResultsDataGridView.Rows.Count > 0)
                {
                    if (e.RowIndex == -1)
                    {
                        if (e.ColumnIndex == 0)
                        {
                            UserResultsDataGridView.ClearSelection();
                            UserResultsDataGridView.CurrentCell = null;
                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)UserResultsDataGridView.Rows[0].Cells[0];
                            bool isSelected = (chk.Value == chk.TrueValue);
                            for (int i = 0; i < UserResultsDataGridView.Rows.Count; i++)
                            {
                                chk = (DataGridViewCheckBoxCell)UserResultsDataGridView.Rows[i].Cells[0];
                                chk.Value = isSelected ? chk.FalseValue : chk.TrueValue;
                            }
                        }
                    }
                    else
                    {
                        if (e.RowIndex >= 0)
                        {
                            this.Cursor = Cursors.WaitCursor;
                            this.StatusStripLabel.Text = "Loading user account...";
                            Application.DoEvents();
                            this.LoadUserAccount(UserResultsDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString());
                        }
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
                this.Cursor = Cursors.Default;
            }
        }

        private void AddUserAccount()
        {
            using (UserAccountForm form = new UserAccountForm())
            {
                form.EnvConfig = currentEnvConfig;
                form.adManagementProvider = adManagementProvider;
                form.adQueryProvider = adQueryProvider;
                form.UserGroups = envGroups;
                form.UserAccount = new UserAccount();
                form.UserProfile = new UserProfile();
                form.EditMode = Domain.Enumerations.EditModeType.Add;
                form.Text = "User Account - new user";
                this.StatusStripLabel.Text = defaultStatus;
                form.ShowDialog();
            }
        }

        private void ClearUserSearchResults()
        {
            this.UserAccountsBindingSource.Clear();
            this.UserResultsDataGridView.Refresh();
            this.UserResultsGroupBox.Enabled = false;
        }

        private void DeleteUserAccount(string userName)
        {
            adManagementProvider.DeleteUser(userName);

            IUserQueryServiceProvider qryProvider = null;
            IUserServiceProvider cmdProvider = null;

            if (currentEnvConfig.ServiceVersion == 1)
            {
                qryProvider = new UserQueryServiceProvider1(currentEnvConfig);
                cmdProvider = new UserServiceProvider1(currentEnvConfig);
            }
            else
            {
                qryProvider = new UserQueryServiceProvider2(currentEnvConfig);
                cmdProvider = new UserServiceProvider2(currentEnvConfig);
            }

            if (qryProvider.LoadUserProfile(userName) != null)
            {
                cmdProvider.InactiveProfile(userName);
            }
        }

        private void DisplayUserSearchResults(List<UserIdentity> users)
        {
            if (users != null && users.Count > 0)
            {
                this.UserAccountsBindingSource.DataSource = users;
                this.UserResultsGroupBox.Enabled = true;
                UserResultsDataGridView.Focus();
            }
            else
            {
                FormHelper.DisplayMessage("No accounts found", MessageBoxIcon.Information);
                UserNameTextBox.Focus();
            }
        }

        private void InitializeEnvironment(string envName)
        {
            envGroups = new List<Group>();
            currentEnvConfig = envConfigs.Environments.Find(o => o.Name == envName);
            adQueryProvider = new ActiveDirectoryQueryProvider(currentEnvConfig.ADServer, currentEnvConfig.ADUser, currentEnvConfig.ADPassword, currentEnvConfig.ADContainer);
            adManagementProvider = new ActiveDirectoryProvider(currentEnvConfig.ADServer, currentEnvConfig.ADUser, currentEnvConfig.ADPassword, currentEnvConfig.ADContainer);

            SearchGroupsRequest request = new SearchGroupsRequest()
            {
                PagingCriteria = new PagingCriteria()
                {
                    Page = 1,
                    RowsPerPage = 200
                }
            };

            if (!string.IsNullOrEmpty(currentEnvConfig.AdGroupFilterPrefix))
            {
                request.SearchFields = new List<GroupSearchField>()
                {
                    new GroupSearchField()
                    {
                        FieldName = api.Enumerations.GroupSearchFieldType.GroupName,
                        FieldValue = currentEnvConfig.AdGroupFilterPrefix,
                        SearchMode = api.Enumerations.SearchModeType.StartsWith
                    }
                };
            }

            SearchGroupsResponse response = adQueryProvider.SearchGroups(request);
            if (response != null && response.RowCount > 0)
            {
                envGroups = response.Groups;
            }

            this.UserGroupsComboBox.Items.Clear();
            this.UserGroupsComboBox.Items.Add(string.Empty);
            if (envGroups != null && envGroups.Count > 0)
            {
                envGroups.Sort(
                    delegate (Group r1, Group r2)
                    {
                        return r1.Name.CompareTo(r2.Name);
                    }
                );

                foreach (Group role in envGroups)
                {
                    this.UserGroupsComboBox.Items.Add(role.Name);
                }
            }
        }

        private void LoadConfigration()
        {
            this.StatusStripLabel.Text = "Loading configuration...";
            Application.DoEvents();
            string filePath = AppDomain.CurrentDomain.BaseDirectory + Constants.ConfigFile;
            if (File.Exists(filePath))
            {
                string fileContents = File.ReadAllText(filePath);
                envConfigs = Serializer.XmlDeserialize<EnvironmentsConfig>(fileContents, string.Empty);
                envConfigs.Validate();
                LoadEnvironments();
            }
            else
            {
                throw new ApplicationException(string.Format("Environments configuration file '{0}' does not exist.", filePath));
            }
        }

        private void LoadEnvironments()
        {
            EnvComboBox.Items.Clear();
            envConfigs.Environments.Sort(
                delegate (Domain.Environment e1, Domain.Environment e2)
                {
                    return e1.Name.CompareTo(e2.Name);
                }
            );

            foreach (Domain.Environment currentEnvConfig in envConfigs.Environments)
            {
                EnvComboBox.Items.Add(currentEnvConfig.Name);
            }

            EnvComboBox.SelectedIndex = -1;
        }

        private void LoadUserAccount(string userName)
        {
            IUserQueryServiceProvider qrySvcProvider = null;

            if (currentEnvConfig.ServiceVersion == 1)
            {
                qrySvcProvider = new UserQueryServiceProvider1(currentEnvConfig);
            }
            else
            {
                qrySvcProvider = new UserQueryServiceProvider2(currentEnvConfig);
            }

            UserAccount userAccount = adQueryProvider.GetUser(userName);
            UserProfile userProfile = qrySvcProvider.LoadUserProfile(userName);
            if (userProfile == null)
            {
                userProfile = new UserProfile();
            }

            if (userAccount.RegistrationQualifiers != null && userAccount.RegistrationQualifiers.Count > 0)
            {
                foreach (api.RegistrationQualifier qualifier in userAccount.RegistrationQualifiers)
                {
                    userProfile.RegQualifiers.Add(new Domain.RegistrationQualifier(qualifier.Key, qualifier.Value));
                }
            }

            using (UserAccountForm form = new UserAccountForm())
            {
                form.EnvConfig = currentEnvConfig;
                form.adManagementProvider = adManagementProvider;
                form.adQueryProvider = adQueryProvider;
                form.UserGroups = envGroups;
                form.UserAccount = userAccount;
                form.UserProfile = userProfile;
                form.EditMode = Domain.Enumerations.EditModeType.Edit;
                form.Text = "User Account - " + userName;
                this.StatusStripLabel.Text = defaultStatus;
                form.ShowDialog();
                if (!form.IsCanceled)
                {
                    if (UserSearchButton.Enabled)
                    {
                        this.SearchUserAccounts();
                    }
                }
            }
        }

        private void SearchUserAccounts()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.StatusStripLabel.Text = "Searching user accounts...";
                Application.DoEvents();
                this.ClearUserSearchResults();

                SearchUsersRequest request = new SearchUsersRequest()
                {
                    PagingCriteria = new PagingCriteria()
                    {
                        Page = 1,
                        RowsPerPage = 100
                    },

                    SortCriteria = new List<UserSortCriteria>()
                    {
                        new UserSortCriteria()
                        {
                            SortDirection = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.SortDirectionType.Ascending,
                            SortField = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.UserSortFieldType.UserName,
                            SortPriority = 1
                        }
                    }
                };

                if (!string.IsNullOrWhiteSpace(UserNameTextBox.Text))
                {
                    request.SearchFields.Add(
                        new UserSearchField()
                        {
                            FieldName = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.UserSearchFieldType.UserName,
                            FieldValue = UserNameTextBox.Text,
                            SearchMode = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.SearchModeType.Contains
                        });
                }

                if (UserGroupsComboBox.SelectedIndex > 0)
                {
                    request.SearchFields.Add(
                        new UserSearchField()
                        {
                            FieldName = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.UserSearchFieldType.GroupName,
                            FieldValue = UserGroupsComboBox.SelectedItem.ToString(),
                            SearchMode = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.SearchModeType.EqualTo
                        });
                }

                if (!string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
                {
                    request.SearchFields.Add(
                        new UserSearchField()
                        {
                            FieldName = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.UserSearchFieldType.FirstName,
                            FieldValue = FirstNameTextBox.Text,
                            SearchMode = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.SearchModeType.Contains
                        });
                }

                if (!string.IsNullOrWhiteSpace(LastNameTextBox.Text))
                {
                    request.SearchFields.Add(
                        new UserSearchField()
                        {
                            FieldName = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.UserSearchFieldType.LastName,
                            FieldValue = LastNameTextBox.Text,
                            SearchMode = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.SearchModeType.Contains
                        });
                }

                if (!string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text))
                {
                    request.SearchFields.Add(
                        new UserSearchField()
                        {
                            FieldName = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.UserSearchFieldType.PhoneNumber,
                            FieldValue = PhoneNumberTextBox.Text,
                            SearchMode = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.SearchModeType.StartsWith
                        });
                }

                if (!string.IsNullOrWhiteSpace(EmailTextBox.Text))
                {
                    request.SearchFields.Add(
                        new UserSearchField()
                        {
                            FieldName = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.UserSearchFieldType.EmailAddress,
                            FieldValue = EmailTextBox.Text,
                            SearchMode = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.SearchModeType.Contains
                        });
                }

                if (AccountExpiredCheckbox.Checked)
                {
                    request.SearchFields.Add(
                        new UserSearchField()
                        {
                            FieldName = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.UserSearchFieldType.IsAccountExpired,
                            FieldValue = "true",
                            SearchMode = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.SearchModeType.EqualTo
                        });
                }

                if (LockedOutCheckbox.Checked)
                {
                    request.SearchFields.Add(
                        new UserSearchField()
                        {
                            FieldName = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.UserSearchFieldType.IsAccountLocked,
                            FieldValue = "true",
                            SearchMode = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.SearchModeType.EqualTo
                        });
                }

                SearchUsersResponse response = adQueryProvider.SearchUsers(request);
                List<UserIdentity> identities = new List<UserIdentity>();
                foreach(UserAccount account in response.Users)
                {
                    identities.Add(account.Identity);
                }
                this.DisplayUserSearchResults(identities);
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
            this.AcceptButton = UserSearchButton;
            this.EnvComboBox.Focus();
        }

        private void ToggleUserSearchButton()
        {
            UserSearchButton.Enabled = (EnvComboBox.Text.Length > 0) &&
                (UserNameTextBox.Text.Trim().Length > 0
                || UserGroupsComboBox.SelectedIndex > 0
                || FirstNameTextBox.Text.Trim().Length > 0
                || LastNameTextBox.Text.Trim().Length > 0
                || PhoneNumberTextBox.Text.Trim().Length > 0
                || EmailTextBox.Text.Trim().Length > 0
                || LockedOutCheckbox.Checked
                || AccountExpiredCheckbox.Checked);
        }

        private List<DataGridViewRow> ReturnSelectedUsers()
        {
            List<DataGridViewRow> selectedRows = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in UserResultsDataGridView.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (chk.Value == chk.TrueValue)
                {
                    selectedRows.Add(row);
                }
            }
            return selectedRows;
        }

        private void UserResetPasswordButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.StatusStripLabel.Text = "Resetting user account password(s)...";
                Application.DoEvents();
                List<DataGridViewRow> selectedRows = this.ReturnSelectedUsers();
                if (selectedRows == null || selectedRows.Count == 0)
                {
                    FormHelper.DisplayMessage("Select 1 or more user accounts and try again.", MessageBoxIcon.Error);
                }
                else
                {
                    List<string> selectedUserNames = new List<string>();
                    foreach (DataGridViewRow row in selectedRows)
                    {
                        selectedUserNames.Add(row.Cells[1].Value.ToString());
                    }
                    ResetPasswordForm form = new ResetPasswordForm();
                    form.SelectedUserNames = selectedUserNames;
                    form.EnvConfig = currentEnvConfig;
                    form.ShowDialog();
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

        private void UserDeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.StatusStripLabel.Text = "Deleting user account(s)...";
                Application.DoEvents();
                List<DataGridViewRow> selectedRows = this.ReturnSelectedUsers();
                if (selectedRows == null || selectedRows.Count == 0)
                {
                    FormHelper.DisplayMessage("Select 1 or more user accounts and try again.", MessageBoxIcon.Error);
                }
                else if (!currentEnvConfig.IsDeleteAllowed)
                {
                    FormHelper.DisplayMessage("Deletes are not allowed in this environment.", MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult result = FormHelper.DisplayMessage(string.Format("Are you sure you want to delete the {0} selected user account(s)?\r\n\r\nThe action is permanent and connot be undone.", selectedRows.Count.ToString("#,##0")), MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in selectedRows)
                        {
                            this.DeleteUserAccount(row.Cells[1].Value.ToString());
                        }
                        if (UserSearchButton.Enabled)
                        {
                            this.SearchUserAccounts();
                        }
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
                this.Cursor = Cursors.Default;
            }
        }

        private void UserCloneButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.StatusStripLabel.Text = "Cloning user account(s)...";
                Application.DoEvents();
                List<DataGridViewRow> selectedRows = this.ReturnSelectedUsers();
                if (selectedRows == null || selectedRows.Count == 0)
                {
                    FormHelper.DisplayMessage("Select 1 or more user accounts and try again.", MessageBoxIcon.Error);
                }
                else
                {
                    List<string> selectedUserNames = new List<string>();
                    foreach (DataGridViewRow row in selectedRows)
                    {
                        selectedUserNames.Add(row.Cells[1].Value.ToString());
                    }
                    CloneToEnvironmentForm form = new CloneToEnvironmentForm();
                    form.SelectedUserNames = selectedUserNames;
                    form.EnvConfigs = envConfigs;
                    form.SourceEnvConfig = currentEnvConfig;
                    form.SourceEnvGroups = envGroups;
                    form.ShowDialog();
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
    }
}
