using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;

namespace BatchDirectoryCheck
{
    public partial class BatchCheckForm : Form
    {
        public BatchCheckForm()
        {
            InitializeComponent();
        }

        private bool connected = false;
        private bool dbmsOracle = false;
        private OracleConnection oracleConnection = null;
        private SqlConnection sqlServerConnection = null;

        List<object> row = new List<object>();

        private void Form1_Load(object sender, EventArgs e)
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            bool isAdministrator = principal.IsInRole(WindowsBuiltInRole.Administrator);

            if (!isAdministrator)
            {
                MessageBox.Show("This application requires administrator privileges. Right-click the application icon or executable and select the 'Run as administrator' option.", "Administrator Privileges Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            comboBoxDbms.Items.Add("Oracle");
            comboBoxDbms.Items.Add("SqlServer");
            comboBoxDbms.SelectedIndex = 0;
            btnCreateFolders.Enabled = false;

            this.Show();
            this.Refresh();

            this.LoadEnvironment();
            setFieldProtection();
        }

        private bool loadDataSources()
        {
            Cursor.Current = Cursors.WaitCursor;
            comboBoxDataSource.Items.Clear();

            dataGridAppSettings.Rows.Clear();
            comboBoxModules.Items.Clear();
            comboBoxModules.ResetText();
            comboBoxModules.SelectedIndex = -1;

            this.Refresh();

            try
            {
                CustomConfigurationSection section = ConfigurationManager.GetSection("custom") as CustomConfigurationSection;

                if (section != null)
                {
                    CustomConfigurationElementDbServerCollection dbServers = section.CustomConfigurationsDbServers;

                    foreach (CustomConfigurationElementDbServers dbServer in dbServers)
                    {
                        if ((dbServer.Type == "SqlServer" && !dbmsOracle ||
                            dbServer.Type == "Oracle" && dbmsOracle) &&
                            !string.IsNullOrEmpty(dbServer.DatabaseHost))
                        {
                            comboBoxDataSource.Items.Add(dbServer.DatabaseService);
                        }
                    }
                }

                comboBoxDataSource.Sorted = true;
                comboBoxDataSource.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                buttonConnect.Enabled = false;
                comboBoxModules.Enabled = false;
                comboBoxDataSource.Enabled = false;
                buttonSearch.Enabled = false;
                MessageBox.Show("Exception loading data sources.\n\n" + ex.Message, "Data Source Load Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            Cursor.Current = Cursors.Default;
            return true;
        }

        private void LoadEnvironment()
        {
            Cursor.Current = Cursors.WaitCursor;
            cmbEnvironMent.Items.Clear();

            CustomConfigurationSection section = ConfigurationManager.GetSection("custom") as CustomConfigurationSection;

            if (section != null)
            {
                CustomConfigurationElementDbServerCollection dbServers = section.CustomConfigurationsDbServers;

                foreach (CustomConfigurationElementDbServers dbServer in dbServers)
                {
                    cmbEnvironMent.Items.Add(dbServer.Environment);
                }
            }

            cmbEnvironMent.Sorted = true;
            cmbEnvironMent.SelectedIndex = 0;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string connectionString = null;

            resetConnection();

            if (dbmsOracle)
            {
                connectionString = buildOracleConnectionString(comboBoxDataSource.Text, textBoxUserId.Text, textBoxPassword.Text);

                try
                {
                    oracleConnection = new OracleConnection();
                    oracleConnection.ConnectionString = connectionString;
                    oracleConnection.Open();

                    if (oracleConnection.State == ConnectionState.Open)
                    {
                        connected = true;
                    }
                    else
                    {
                        connected = false;
                        MessageBox.Show("A general error has occured connecting to the Oracle database.", "Oracle Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (OracleException ex)
                {
                    connected = false;
                    MessageBox.Show(ex.Message, "Oracle Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                connectionString = buildSqlServerConnectionString(comboBoxDataSource.Text, textBoxUserId.Text, textBoxPassword.Text);

                try
                {
                    sqlServerConnection = new SqlConnection();
                    sqlServerConnection.ConnectionString = connectionString;
                    sqlServerConnection.Open();

                    if (sqlServerConnection.State == ConnectionState.Open)
                    {
                        connected = true;
                    }
                    else
                    {
                        connected = false;
                        MessageBox.Show("A general error has occured connecting to the SqlServer database.", "SqlServer Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (SqlException ex)
                {
                    connected = false;
                    MessageBox.Show(ex.Message, "SqlServer Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            this.loadTenants();
        }

        private bool loadTenants()
        {
            comboBoxTenants.SelectedIndexChanged += comboBoxTenants_SelectedIndexChanged;

            List<string[]> tenants = new List<string[]>();

            if (connected)
            {
                tenants = readTenants(oracleConnection, sqlServerConnection);

                if (tenants.Count <= 0)
                {
                    return false;
                }

                comboBoxTenants.Items.Clear();
                int idx = 0;

                foreach (string[] tenant in tenants)
                {
                    comboBoxTenants.Items.Add(tenant[0]);

                    if (tenant[0] == "Default")
                    {
                        comboBoxTenants.SelectedIndex = idx;
                    }

                    idx++;
                }

                if (string.IsNullOrEmpty(comboBoxTenants.Text) &&
                    comboBoxTenants.Items.Count > 0)
                {
                    comboBoxTenants.SelectedIndex = 0;
                }

                if (dbmsOracle)
                {
                    comboBoxTenants.SelectedItem = Properties.Settings.Default.DefaultOracleTenant;
                }
                else
                {
                    comboBoxTenants.SelectedItem = Properties.Settings.Default.DefaultSqlServerTenant;
                }
            }


            return true;
        }

        private void resetConnection()
        {
            comboBoxDataSource.DroppedDown = false;

            if (connected)
            {
                if (dbmsOracle)
                {
                    if (oracleConnection != null)
                    {
                        OracleConnection.ClearPool(oracleConnection);
                        oracleConnection.Close();
                    }
                }
                else
                {
                    if (sqlServerConnection != null)
                    {
                        SqlConnection.ClearPool(sqlServerConnection);
                        sqlServerConnection.Close();
                    }
                }
            }

            connected = false;
            setFieldProtection();
            this.Refresh();
        }

        private List<string[]> readTenants(OracleConnection oracleConnection, SqlConnection sqlConnection, string tenantName = null)
        {
            Cursor.Current = Cursors.WaitCursor;
            string sql = null;
            object tenantIdGuid = null;
            IDataReader reader = null;
            List<string> row = new List<string>();
            List<string[]> tenants = new List<string[]>();

            sql = "SELECT DISTINCT TENANT_ID, TENANT_NAME " +
                  "  FROM TENANT";

            if (!string.IsNullOrEmpty(tenantName))
            {
                sql = sql + " AND TENANT_NAME = '" + tenantName + "'";
            }

            try
            {
                if (dbmsOracle)
                {
                    reader = executeOracleSql(oracleConnection, sql, false, true);
                }
                else
                {
                    reader = executeSqlServerSql(sqlConnection, sql, false, true);
                }

                while (reader.Read())
                {
                    string name = reader["TENANT_NAME"].ToString();
                    tenantIdGuid = reader["TENANT_ID"];

                    row.Clear();
                    row.Add(name);
                    tenants.Add(row.ToArray());
                }

                reader.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading the Tenant table, the table may not exist in the schema.\n\n" + ex.Message, "Tenant Table Read Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Cursor.Current = Cursors.Default;
            return tenants;
        }

        private static OracleDataReader executeOracleSql(OracleConnection connection, string sql, bool showException = false, bool throwException = false)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                OracleCommand command = connection.CreateCommand();
                command.CommandText = sql;
                command.InitialLOBFetchSize = -1;
                command.InitialLONGFetchSize = -1;

                OracleDataReader reader = command.ExecuteReader();
                command.Dispose();

                return reader;
            }
            catch (OracleException ex)
            {
                if (showException)
                {
                    MessageBox.Show(ex.Message, "Oracle Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (throwException)
                {
                    throw;
                }

                return null;
            }
        }

        private static SqlDataReader executeSqlServerSql(SqlConnection connection, string sql, bool showException = false, bool throwException = false)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                SqlCommand command = connection.CreateCommand();
                command.CommandText = sql;

                SqlDataReader reader = command.ExecuteReader();
                command.Dispose();

                return reader;
            }
            catch (Exception ex)
            {
                if (showException)
                {
                    MessageBox.Show(ex.Message, "SqlServer Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (throwException)
                {
                    throw;
                }

                return null;
            }
        }

        private static string buildSqlServerConnectionString(string dataSource, string userid, string password)
        {
            string initialCatalog = null;
            CustomConfigurationSection section = ConfigurationManager.GetSection("custom") as CustomConfigurationSection;

            if (section != null)
            {
                CustomConfigurationElementDbServerCollection dbServers = section.CustomConfigurationsDbServers;
                foreach (CustomConfigurationElementDbServers dbServer in dbServers)
                {
                    if (dbServer.DatabaseService == dataSource)
                    {
                        if (!string.IsNullOrEmpty(dbServer.DatabaseInitialCatalog))
                        {
                            initialCatalog = dbServer.DatabaseInitialCatalog;
                        }
                        else
                        {
                            initialCatalog = "UA3_TENANT";
                        }

                        return
                            "Data Source=" + dbServer.DatabaseHost + ";" +
                            "User id=" + userid + "; " +
                            "Password=" + password + ";" +
                            "Initial Catalog=" + initialCatalog + ";" +
                            "Persist Security Info=False;" +
                            "MultipleActiveResultSets=True;" +
                            "Encrypt=True;" +
                            "TrustServerCertificate=True;Pooling=True;Min Pool Size=3;Max Pool Size=100;Connection Timeout=0";
                    }
                }
            }

            return null;
        }

        private static string buildOracleConnectionString(string dataSource, string userid, string password)
        {
            CustomConfigurationSection section = ConfigurationManager.GetSection("custom") as CustomConfigurationSection;
            if (section != null)
            {
                CustomConfigurationElementDbServerCollection dbServers = section.CustomConfigurationsDbServers;
                foreach (CustomConfigurationElementDbServers dbServer in dbServers)
                {
                    if (dbServer.DatabaseService == dataSource)
                    {
                        return
                            "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(" +
                            "HOST=" + dbServer.DatabaseHost + ")(" +
                            "PORT=" + dbServer.DatabasePort + "))(CONNECT_DATA=(SERVER=DEDICATED)(" +
                            "SERVICE_NAME=" + dbServer.DatabaseService + ")));" +
                            "USER ID=" + userid + ";" +
                            "PASSWORD=" + password;
                    }
                }
            }

            return null;
        }

        private void setFieldProtection()
        {
            comboBoxDataSource.Enabled = false;
            comboBoxDbms.Enabled = false;
            buttonSearch.Enabled = false;

            if (string.IsNullOrEmpty((string)comboBoxDataSource.SelectedItem) ||
                string.IsNullOrEmpty(textBoxPassword.Text) ||
                string.IsNullOrEmpty(textBoxUserId.Text))
            {
                buttonConnect.Enabled = false;
            }
            else
            {
                buttonConnect.Enabled = true;
            }

            if (connected)
            {
                comboBoxTenants.Enabled = true;
                buttonConnect.Text = "Connected";
                buttonConnect.Enabled = false;
                textBoxUserId.Enabled = false;
                textBoxPassword.Enabled = false;
                comboBoxModules.Enabled = true;
            }
            else
            {
                comboBoxTenants.Enabled = false;
                buttonConnect.Text = "Connect";
            }
        }

        private void ConnectButtonReset()
        {
            resetConnection();
            setFieldProtection();
            this.AcceptButton = buttonConnect;
        }

        private void loadModules()
        {
            IDataReader reader = null;

            Cursor.Current = Cursors.WaitCursor;
            string sql = null;
            comboBoxModules.Items.Clear();

            //TODO : Enable If all Module  is needed.
            //comboBoxModules.Items.Add("<All>");

            try
            {
                sql = "SELECT DISTINCT MODULE_NAME  " +
                      "  FROM MODULE A, " +
                      "       TENANT_MODULE B, " +
                      "       TENANT C " +
                      " WHERE A.MODULE_ID = B.MODULE_ID  " +
                      "   AND B.TENANT_ID = C.TENANT_ID " +
                      "   AND C.TENANT_NAME = '" + comboBoxTenants.Text + "' " +
                      "ORDER BY MODULE_NAME";

                if (dbmsOracle)
                {
                    reader = executeOracleSql(oracleConnection, sql, false, true);
                }
                else
                {
                    reader = executeSqlServerSql(sqlServerConnection, sql, false, true);
                }

                while (reader.Read())
                {
                    comboBoxModules.Items.Add(reader["MODULE_NAME"].ToString());
                }

                if(comboBoxModules.Items.Count > 0)
                {
                    buttonSearch.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Setting Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (comboBoxModules.Items.Count > 0)
            {
                comboBoxModules.SelectedIndex = 0;
            }

            Cursor.Current = Cursors.Default;
        }

        private void loadAppSettings(string searchString, string appKey, string batchRootDir)
        {

            Cursor.Current = Cursors.WaitCursor;
            dataGridAppSettings.Rows.Clear();
            this.Refresh();

            IDataReader reader = null;
            string searchClause = null;
            string appKeyClause = null;
            string moduleClause = null;
            string appClause = null;
            string sql = null;
            int rowCount = 0;
            List<object> row = new List<object>();

            try
            {
                if ((string)comboBoxModules.SelectedItem != "<All>")
                {
                    moduleClause = " AND C.MODULE_NAME = '" + comboBoxModules.SelectedItem + "'";
                }

                if (!string.IsNullOrEmpty(searchString) && dbmsOracle)
                {
                    searchClause = " AND (regexp_like(VALUE, '" + searchString + "', 'i'))";
                }

                sql = "SELECT count(*) " +
                      "  FROM TM_APP_SETTING A, " +
                      "       TENANT_MODULE B, " +
                      "       MODULE C, " +
                      "       APPLICATION D, " +
                      "       TENANT E " +
                      " WHERE A.TM_ID = B.TM_ID  " +
                      "   AND B.TENANT_ID = E.TENANT_ID " +
                      "   AND E.TENANT_NAME = '" + comboBoxTenants.Text + "' " +
                      "   AND B.MODULE_ID = C.MODULE_ID " +
                      "   AND A.APP_ID = D.APP_ID" +
                      moduleClause +
                      searchClause +
                      appKeyClause +
                      appClause;

                if (dbmsOracle)
                {
                    reader = executeOracleSql(oracleConnection, sql, false, true);
                }
                else
                {
                    reader = executeSqlServerSql(sqlServerConnection, sql, false, true);
                }

                while (reader.Read())
                {
                    rowCount = Convert.ToInt32(reader[0]);
                }

                reader.Dispose();

                sql = "SELECT *  " +
                      "  FROM TM_APP_SETTING A, " +
                      "       TENANT_MODULE B, " +
                      "       MODULE C, " +
                      "       APPLICATION D, " +
                      "       TENANT E " +
                      " WHERE A.TM_ID = B.TM_ID  " +
                      "   AND B.TENANT_ID = E.TENANT_ID " +
                      "   AND E.TENANT_NAME = '" + comboBoxTenants.Text + "' " +
                      "   AND B.MODULE_ID = C.MODULE_ID " +
                      "   AND A.APP_ID = D.APP_ID" +
                      moduleClause +
                      searchClause +
                      appKeyClause +
                      appClause;

                if (dbmsOracle)
                {
                    reader = executeOracleSql(oracleConnection, sql, false, true);
                }
                else
                {
                    reader = executeSqlServerSql(sqlServerConnection, sql, false, true);
                }

                int rowCnt = 0;

                while (reader.Read())
                {
                    row.Clear();

                    string val = reader["VALUE"].ToString();

                    if (val.Contains("$(#RootDir)"))
                    {
                        val = val.Replace("$(#RootDir)", batchRootDir);
                    }

                    if (val.Contains("$(\"#RootDir\")"))
                    {
                        val = val.Replace("$(\"#RootDir\")", batchRootDir);
                    }

                    //Exclude files type path
                    if (!val.Contains("."))
                    {
                        row.Add(reader["MODULE_NAME"].ToString());
                        row.Add(reader["APP_NAME"].ToString());
                        row.Add(reader["APP_SETTING_KEY"].ToString());
                        row.Add(val);
                        row.Add(CheckFolderAvailable(val).ToString());
                        dataGridAppSettings.Rows.Add(row.ToArray());
                    }

                }

                rowCnt++;
                reader.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Setting Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dataGridAppSettings.Sort(dataGridAppSettings.Columns[1], System.ComponentModel.ListSortDirection.Ascending);

            if (dataGridAppSettings.Rows.Count > 0)
            {
                int noFolderCount = 0;

                for (int i = 0; i < dataGridAppSettings.Rows.Count; i++)
                {
                    dataGridAppSettings.Rows[i].Cells["FolderExists"].Style.ForeColor = Color.White;
                    dataGridAppSettings.Rows[i].Cells["FolderExists"].Style.Font = new Font(dataGridAppSettings.Font, FontStyle.Bold);

                    if (dataGridAppSettings.Rows[i].Cells["FolderExists"].Value.ToString() == "True")
                    {
                        dataGridAppSettings.Rows[i].Cells["FolderExists"].Style.BackColor = Color.Green;
                    }
                    else
                    {
                        noFolderCount += 1;
                        dataGridAppSettings.Rows[i].Cells["FolderExists"].Style.BackColor = Color.Red;
                    }

                }

                if(noFolderCount > 0)
                {
                    btnCreateFolders.Enabled = true;
                }
                else
                {
                    btnCreateFolders.Enabled = false;
                }
            }
            else
            {
                dataGridAppSettings.Rows.Add("No keys found");
            }

            setFieldProtection();

            Cursor.Current = Cursors.Default;
        }

        private bool CheckFolderAvailable(string folderPath)
        {
            try
            {
                if (Directory.Exists(folderPath))
                {
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }

            return false;
        }

        private static string lookupValueParm(bool dbmsOracle, OracleConnection oracleConnection, SqlConnection sqlServerConnection, string parm, string tenant)
        {
            string resolvedParm = parm;
            IDataReader reader = null;
            parm = parm.Replace("$", "").Replace("(", "").Replace(")", "");

            string sql =
                  "SELECT VALUE " +
                  "  FROM TM_APP_SETTING A, " +
                  "       TENANT_MODULE B, " +
                  "       MODULE C, " +
                  "       APPLICATION D, " +
                  "       TENANT E " +
                  " WHERE A.TM_ID = B.TM_ID " +
                  "   AND B.TENANT_ID = E.TENANT_ID " +
                  "   AND E.TENANT_NAME = '" + tenant + "' " +
                  "   AND B.MODULE_ID = C.MODULE_ID " +
                  "   AND A.APP_ID = D.APP_ID " +
                  "   AND APP_SETTING_KEY = '" + parm + "' " +
                  "   AND C.MODULE_NAME = 'Core' " +
                  "   AND D.APP_NAME='Tenant Level Common'";

            if (dbmsOracle)
            {
                reader = executeOracleSql(oracleConnection, sql, false, true);
            }
            else
            {
                reader = executeSqlServerSql(sqlServerConnection, sql, false, true);
            }

            while (reader.Read())
            {
                try
                {
                    resolvedParm = reader[0].ToString();
                }
                catch
                { }
            }

            return resolvedParm;
        }


        #region Button Click Events

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string batchIORootDir = lookupValueParm(dbmsOracle, oracleConnection, sqlServerConnection, "$(#RootDir)", comboBoxTenants.SelectedItem.ToString());

            this.loadAppSettings("#RootDir", "<All>", batchIORootDir);
            buttonSearch.Enabled = true;
            dataGridAppSettings.Focus();
        }

        private void btnCreateFolders_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridAppSettings.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridAppSettings.Rows.Count; i++)
                    {
                        if (dataGridAppSettings.Rows[i].Cells["FolderExists"].Value.ToString() == "False")
                        {
                            string batchFolderPath = dataGridAppSettings.Rows[i].Cells["AppSettingValue"].Value.ToString();

                            Directory.CreateDirectory(batchFolderPath);
                        }
                    }
                }

                string batchIORootDir = lookupValueParm(dbmsOracle, oracleConnection, sqlServerConnection, "$(#RootDir)", comboBoxTenants.SelectedItem.ToString());
                this.loadAppSettings("#RootDir", "<All>", batchIORootDir);
                MessageBox.Show("Batch Directories Successfully Created");
                buttonSearch.Enabled = true;
            }
            catch
            {
                MessageBox.Show(cmbEnvironMent.SelectedItem.ToString() + " Batch Root Directory Path Access Denied");
            }
        }


        #endregion

        #region TextBox Changed Events

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            this.ConnectButtonReset();
        }

        private void textBoxUserId_TextChanged(object sender, EventArgs e)
        {
            this.ConnectButtonReset();
        }

        #endregion

        #region DropDown IndexChanged Events

        private void comboBoxTenants_SelectedIndexChanged(object sender, EventArgs e)
        {
            setFieldProtection();
            loadModules();
            buttonSearch.Enabled = true;
            btnCreateFolders.Enabled = false;
        }

        private void cmbEnvironMent_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxUserId.Text = string.Empty;
            textBoxPassword.Text = string.Empty;
            textBoxUserId.Enabled = true;
            textBoxPassword.Enabled = true;
            comboBoxTenants.Items.Clear();
            comboBoxModules.Items.Clear();
            comboBoxModules.Enabled = false;
            dataGridAppSettings.Rows.Clear();
            btnCreateFolders.Enabled = false;

            Cursor.Current = Cursors.WaitCursor;

            CustomConfigurationSection section = ConfigurationManager.GetSection("custom") as CustomConfigurationSection;

            if (section != null)
            {
                CustomConfigurationElementDbServerCollection dbServers = section.CustomConfigurationsDbServers;

                foreach (CustomConfigurationElementDbServers dbServer in dbServers)
                {
                    if (dbServer.Environment == cmbEnvironMent.SelectedItem.ToString())
                    {
                        comboBoxDataSource.SelectedItem = dbServer.DatabaseService;
                        comboBoxDbms.SelectedItem = dbServer.Type;
                        textBoxUserId.Text = dbServer.DatabaseInitialCatalog;
                        if (string.IsNullOrEmpty(textBoxUserId.Text))
                        {
                            textBoxUserId.Focus();
                        }
                        else
                        {
                            textBoxPassword.Focus();
                        }

                        break;
                    }
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void comboBoxModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonSearch.Enabled = true;
            dataGridAppSettings.Rows.Clear();
            btnCreateFolders.Enabled = false;
        }


        private void comboBoxDbms_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string dbmsType = comboBoxDbms.Text;

            resetConnection();
            textBoxPassword.Text = null;

            if (dbmsType == "Oracle")
            {
                dbmsOracle = true;

                if (!string.IsNullOrEmpty((string)comboBoxDataSource.SelectedItem))
                {
                    Properties.Settings.Default.DefaultSqlServerDatasource = (string)comboBoxDataSource.SelectedItem;
                }

                if (!string.IsNullOrEmpty((string)comboBoxTenants.SelectedItem))
                {
                    Properties.Settings.Default.DefaultSqlServerTenant = (string)comboBoxTenants.SelectedItem;
                }

                comboBoxDataSource.SelectedItem = Properties.Settings.Default.DefaultOracleDatasource;
                comboBoxTenants.SelectedItem = Properties.Settings.Default.DefaultOracleTenant;
            }
            else if (dbmsType == "SqlServer")
            {
                dbmsOracle = false;

                if (!string.IsNullOrEmpty((string)comboBoxDataSource.SelectedItem))
                {
                    Properties.Settings.Default.DefaultOracleDatasource = (string)comboBoxDataSource.SelectedItem;
                }

                if (!string.IsNullOrEmpty((string)comboBoxTenants.SelectedItem))
                {
                    Properties.Settings.Default.DefaultOracleTenant = (string)comboBoxTenants.SelectedItem;
                }

                comboBoxDataSource.SelectedItem = Properties.Settings.Default.DefaultSqlServerDatasource;
                comboBoxTenants.SelectedItem = Properties.Settings.Default.DefaultSqlServerTenant;
            }
            else
            {
                MessageBox.Show("Invalid DbmsType configured in app.config. Valid values are 'Oracle' and 'SqlServer'.", "Invalid Dbms Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Properties.Settings.Default.Save();

            this.loadDataSources();

            if (dbmsType == "Oracle")
            {
                comboBoxDataSource.SelectedItem = Properties.Settings.Default.DefaultOracleDatasource;
                comboBoxTenants.SelectedItem = Properties.Settings.Default.DefaultOracleTenant;
            }
            else if (dbmsType == "SqlServer")
            {
                comboBoxDataSource.SelectedItem = Properties.Settings.Default.DefaultSqlServerDatasource;
                comboBoxTenants.SelectedItem = Properties.Settings.Default.DefaultSqlServerTenant;
            }

            setFieldProtection();

            Cursor.Current = Cursors.Default;
        }

        private void comboBoxDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetConnection();
            setFieldProtection();
        }


        #endregion

    }
}