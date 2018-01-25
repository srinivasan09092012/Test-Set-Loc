using HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces;
using HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain;
using HPE.HSP.UA3.Core.API.IdentityManagement.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UserAccountManager.Domain;
using UserAccountManager.Providers;
using UserAccountManager.Utilities;

namespace UserAccountManager.Forms
{
    public partial class MainForm : Form
    {
        private static Domain.EnvironmentsConfig envConfigs = null;
        private static Domain.Environment currentEnvConfig = null;
        private static IUserQueryProvider adQueryProvider = null;
        private static IUserManagementProvider adManagementProvider = null;
        private static List<Role> envRoles = null;
        private static string defaultStatus = "Select an environment, enter search fields, and click Search. To delete an account, select the row and click Delete.";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.StatusStripLabel.Text = "Initializing application...";
                Application.DoEvents();
                LoadConfigration();
                this.InitializeForm();
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

        private void UserNameTextBox_TextChanged(object sender, EventArgs e)
        {
            ToggleSeachButton();
        }

        private void FirstNameTextBox_TextChanged(object sender, EventArgs e)
        {
            ToggleSeachButton();
        }

        private void LastNameTextBox_TextChanged(object sender, EventArgs e)
        {
            ToggleSeachButton();
        }

        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {
            ToggleSeachButton();
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

        private void EmailTextBox_Leave(object sender, EventArgs e)
        {
            EmailTextBox.Text = EmailTextBox.Text.Trim();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.StatusStripLabel.Text = "Searching user accounts...";
                Application.DoEvents();
                this.SearchUserAccounts();
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

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.StatusStripLabel.Text = "Adding new user account...";
                Application.DoEvents();
                this.AddUserAccount();
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

        private void EnvComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (EnvComboBox.SelectedIndex >= 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.StatusStripLabel.Text = "Loading environment variables...";
                    Application.DoEvents();
                    this.ClearSearchResults();
                    this.InitializeEnvironment(EnvComboBox.Text);
                    this.SearchGroupBox.Enabled = true;
                    this.OtherActionsGroupBox.Enabled = true;
                    this.ResultsGroupBox.Enabled = true;
                    this.UserNameTextBox.Focus();
                    this.UserNameTextBox.SelectAll();
                }
                else
                {
                    this.ClearSearchResults();
                }
            }
            catch (Exception ex)
            {
                EnvComboBox.SelectedIndex = -1;
                this.DisplayMessage(ex);
            }
            finally
            {
                this.StatusStripLabel.Text = defaultStatus;
                this.Cursor = Cursors.Default;
            }
        }

        private void ResultsDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.StatusStripLabel.Text = "Deleting user account...";
                Application.DoEvents();
                if (currentEnvConfig.IsDeleteAllowed)
                {
                    string userName = e.Row.Cells[0].Value.ToString();
                    DialogResult result = this.DisplayMessage(string.Format("Are you sure you want to delete user account '{0}'?\r\n\r\nThe action is permanent and connot be undone.", userName), MessageBoxIcon.Question);
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
                    this.DisplayMessage("Deletes are not allowed in this environment.", MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                this.DisplayMessage(ex);
            }
            finally
            {
                this.StatusStripLabel.Text = defaultStatus;
                this.Cursor = Cursors.Default;
            }
        }

        private void ResultsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.StatusStripLabel.Text = "Loading user account...";
                Application.DoEvents();
                this.LoadUserAccount(ResultsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
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

        private void AddUserAccount()
        {
            using (UserAccountForm form = new UserAccountForm())
            {
                form.EnvConfig = currentEnvConfig;
                form.adManagementProvider = adManagementProvider;
                form.adQueryProvider = adQueryProvider;
                form.UserRoles = envRoles;
                form.UserAccount = new UserAccount();
                form.UserProfile = new UserProfile();
                form.EditMode = Domain.Enumerations.EditModeType.Add;
                form.Text = "User Account - new user";
                this.StatusStripLabel.Text = defaultStatus;
                form.ShowDialog();
            }
        }

        private void ClearSearchResults()
        {
            this.UserAccountsBindingSource.Clear();
            this.ResultsDataGridView.Refresh();
        }

        private void DeleteUserAccount(string userName)
        {
            adManagementProvider.DeleteUser(userName);
        }

        private void DisplaySearchResults(List<UserIdentity> users)
        {
            if (users != null && users.Count > 0)
            {
                this.UserAccountsBindingSource.DataSource = users;
                ResultsDataGridView.Focus();
            }
            else
            {
                this.DisplayMessage("No accounts found", MessageBoxIcon.Information);
                UserNameTextBox.Focus();
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

        private void InitializeEnvironment(string envName)
        {
            envRoles = new List<Role>();
            currentEnvConfig = envConfigs.Environments.Find(o => o.Name == envName);
            adQueryProvider = new ActiveDirectoryQueryProvider(currentEnvConfig.ADServer, currentEnvConfig.ADUser, currentEnvConfig.ADPassword, currentEnvConfig.ADContainer);
            adManagementProvider = new ActiveDirectoryProvider(currentEnvConfig.ADServer, currentEnvConfig.ADUser, currentEnvConfig.ADPassword, currentEnvConfig.ADContainer);

            SearchRolesRequest request = new SearchRolesRequest()
            {
                PagingCriteria = new PagingCriteria()
                {
                    Page = 1,
                    RowsPerPage = 200
                },
                SearchFields = new List<RoleSearchField>()
                {
                    new RoleSearchField()
                    {
                        FieldName = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.RoleSearchFieldType.RoleName,
                        FieldValue = Constants.RolePrefix,
                        SearchMode = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain.Enumerations.SearchModeType.StartsWith
                    }
                }
            };

            SearchRolesResponse response = adQueryProvider.SearchRoles(request);
            if (response != null && response.RowCount > 0)
            {
                envRoles = response.Roles;
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
            UserQueryServiceProvider svcProvider = new UserQueryServiceProvider(currentEnvConfig);
            UserAccount userAccount = adQueryProvider.GetUser(userName);
            UserProfile userProfile = svcProvider.LoadUserProfile(userName);
            if (userProfile == null)
            {
                userProfile = new UserProfile();
            }

            using (UserAccountForm form = new UserAccountForm())
            {
                form.EnvConfig = currentEnvConfig;
                form.adManagementProvider = adManagementProvider;
                form.adQueryProvider = adQueryProvider;
                form.UserRoles = envRoles;
                form.UserAccount = userAccount;
                form.UserProfile = userProfile;
                form.EditMode = Domain.Enumerations.EditModeType.Edit;
                form.Text = "User Account - " + userName;
                this.StatusStripLabel.Text = defaultStatus;
                form.ShowDialog();
            }
        }

        private void SearchUserAccounts()
        {
            this.ClearSearchResults();

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

            SearchUsersResponse response = adQueryProvider.SearchUsers(request);
            this.DisplaySearchResults(response.Users);
        }

        private void InitializeForm()
        {
            this.AcceptButton = SearchButton;
            this.EnvComboBox.Focus();
        }

        private void ToggleSeachButton()
        {
            SearchButton.Enabled = (EnvComboBox.Text.Length > 0) &&
                (UserNameTextBox.Text.Trim().Length > 0
                || FirstNameTextBox.Text.Trim().Length > 0
                || LastNameTextBox.Text.Trim().Length > 0
                || EmailTextBox.Text.Trim().Length > 0);
        }
    }
}
