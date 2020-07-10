//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using static HostsManager.MainForm;
using RightsDAL;
using RightsExcel;
using RightsModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RightsUX
{
    public partial class frmRightsMain : Form
    {
        const string FunctionsContentId = "Core.SecurityFunctions";
        const string RightsContentId = "Core.SecurityRights";
        const string RolesContentId = "Core.SecurityRoles";

        private RightsDataAccess data;
        private RightsConnection cn;
        private ReportGenerator reportGen;
        private SettingsModel settings = new SettingsModel();

        public frmRightsMain()
        {
            InitializeComponent();

            this.LoadSettings();
        }

        // Event Handlers
        private void AnyControl_TextChanged(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = SystemColors.Window;
            UpdateStatus("");
        }

        private void BtnCreateReport_Click(object sender, EventArgs e)
        {
            if (this.cn == null || !this.cn.IsConnected())
            {
                ShowFieldError(cboDatabaseServer, "Not Connected to Database", "Please connect to the database before trying to create the report");
                return;
            }

            if (cboTenant.Items.Count == 0 || cboTenant.SelectedIndex < 0)
            {
                ShowFieldError(cboTenant, "No Tenant", "No Tenant was selected, unable to create the report");
                return;
            }

            if (!ValidateOutputSection())
            {
                return;
            }

            if (File.Exists(Path.Combine(this.txtOutputPath.Text.Trim(), this.txtFileName.Text.Trim() + ".xlsx")))
            {
                DialogResult result = MessageBox.Show("A file already exists with the same name in the output path\n\nDo you want to replace the existing file with new data?", "File Already Exists", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    this.txtFileName.BackColor = Color.MistyRose;
                    this.txtFileName.Focus();
                    return;
                }
            }

            SaveSettings();

            Cursor.Current = Cursors.WaitCursor;
            btnCreateReport.Enabled = false;
            btnConnect.Enabled = false;
            btnSelectFolder.Enabled = false;

            try
            {
                this.CreateReport(Path.Combine(this.txtOutputPath.Text.Trim(), this.txtFileName.Text.Trim() + ".xlsx"));
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error Creating Report", ex.Message);
            }

            btnCreateReport.Enabled = true;
            btnConnect.Enabled = true;
            btnSelectFolder.Enabled = true;
            Cursor.Current = Cursors.Default;

        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (!ValidateDatabaseSection())
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            btnCreateReport.Enabled = false;
            btnConnect.Enabled = false;

            try
            {
                if (cboDatabaseServer.SelectedIndex != -1)
                {
                    this.cn = new RightsConnection(rdoOracle.Checked, this.cboDatabaseServer.Text.Trim(), this.txtUserName.Text.Trim(), txtPassword.Text.Trim(), txtSchema.Text.Trim());
                }
                else if (rdoSQL.Checked && cboDatabaseServer.SelectedIndex == -1 && !string.IsNullOrWhiteSpace(cboDatabaseServer.Text))
                {
                    this.cn = new RightsConnection(false, this.cboDatabaseServer.Text.Trim(), this.txtUserName.Text.Trim(), txtPassword.Text.Trim(), txtSchema.Text.Trim());
                }
                else
                {
                    cn.CloseConnection();
                }

                if (this.cn.IsConnected())
                {
                    data = new RightsDataAccess();
                    this.LoadTenantList(data.GetTenantList(this.cn));
                    if (this.cboTenant.Items.Count > 0 && this.cboTenant.SelectedIndex >= 0)
                    {
                        btnCreateReport.Enabled = true;
                    }
                }
                else
                {
                    ShowErrorMessage("Connection Error", $"Unable to connect to the database.\n\n{data.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Connection Error", $"Unable to connect to the database.\n\n{ex.Message}");
            }
            finally
            {
                btnConnect.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void BtnSelectFolder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtOutputPath.Text) || !Directory.Exists(this.txtOutputPath.Text))
            {
                this.dlgFolderSelector.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            this.dlgFolderSelector.ShowNewFolderButton = true;

            DialogResult result = this.dlgFolderSelector.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.txtOutputPath.Text = this.dlgFolderSelector.SelectedPath;
            }
        }

        private void All_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStatus("");
            ((Control)sender).BackColor = SystemColors.Window;

            switch(((ComboBox)sender).Name)
            {
                case "cboDatabaseServer":
                    btnCreateReport.Enabled = false;
                    cboTenant.DataSource = null;
                    txtFileName.Text = "Security Report";
                    break;
                case "cboTenant":
                    if (!string.IsNullOrWhiteSpace(cboTenant.Text))
                    {
                        txtFileName.Text = $"Security Report-{DateTime.Now.ToString("yyyyMMdd")}-{cboDatabaseServer.Text}-{cboTenant.Text}";
                        txtFileName.BackColor = SystemColors.Window;
                    }
                    break;
            }
        }

        private void FrmRightsMain_Load(object sender, EventArgs e)
        {
            this.Location = settings.Location;
            this.lblVersion.Font = new Font(this.lblVersion.Name, 6);
            this.lblVersion.Text = $"Version: {Application.ProductVersion.ToString()}";

            if (Type.GetTypeFromProgID("Excel.Application") == null)
            {
                ShowErrorMessage("Excel is Required", 
                    "Microsoft Excel does not appear to\nbe installed on this computer.\n\nPlease either install Excel or run\nthis on a computer that has Excel\ninstalled.");
                this.Close();
            }
        }

        private void FrmRightsMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.settings.Location = this.Location;
            this.settings.Save();

            if (this.data != null)
            {
                this.cn.CloseConnection();
            }
        }

        private void Datasource_CheckedChanged(object sender, EventArgs e)
        {
            rdoOracle.BackColor = SystemColors.Control;
            rdoSQL.BackColor = SystemColors.Control;

            if (rdoOracle.Checked)
            {
                SetupDatabaseInputs(true);
            }
            else if (rdoSQL.Checked)
            {
                SetupDatabaseInputs(false);
            }

            UpdateStatus("");
        }

        // Private Methods
        private void CreateReport(string FileNameAndPath)
        {
            string tenantId = cboTenant.SelectedValue.ToString();

            this.reportGen = new ReportGenerator();
            this.reportGen.OnStatusChanged += this.UpdateStatus;

            if (LoadReportData(tenantId))
            {
                reportGen.CreateReport(FileNameAndPath, cboDatabaseServer.Text.Trim(), cboTenant.Text.Trim());

                UpdateStatus("Report Complete");
                lblStatus.BackColor = Color.LightGreen;

                reportGen = null;
            }
        }

        private bool LoadReportData(string TenantId)
        {
            UpdateStatus("Loading Data");
            string coreTMId = data.GetTenantModuleForModuleName(cn, TenantId, "Core").TenantModuleId;

            DataListModel roleList = data.GetDataListForContentId(cn, coreTMId, RolesContentId);
            DataListModel functionList = data.GetDataListForContentId(cn, coreTMId, FunctionsContentId);
            DataListModel rightList = data.GetDataListForContentId(cn, coreTMId, RightsContentId);

            List<DataListItemModel> roleItems = data.GetDataListItems(cn, roleList.DataListId);
            List<DataListItemModel> functionItems = data.GetDataListItems(cn, functionList.DataListId);
            List<DataListItemModel> rightItems = data.GetDataListItems(cn, rightList.DataListId);

            foreach(DataListItemModel item in roleItems) { reportGen.Roles.Add(new ReportRole(item)); }
            foreach (DataListItemModel item in functionItems) { reportGen.Functions.Add(new ReportFunction(item)); }
            foreach (DataListItemModel item in rightItems) { reportGen.Rights.Add(new ReportRight(item)); }

            List<DataListLinkModel> roleToFunctionLinks = data.GetDataListItemLinks(cn, roleList.DataListId, functionList.DataListId);
            List<DataListLinkModel> functionToRightLinks = data.GetDataListItemLinks(cn, functionList.DataListId, rightList.DataListId);

            UpdateStatus("Integrating Roles");
            foreach(ReportRole role in reportGen.Roles)
            {
                role.FunctionIds.AddRange(roleToFunctionLinks.FindAll(x => x.ParentListItemId == role.Id).Select(x => x.ChildListItemId).Distinct());
                role.RightIds.AddRange(functionToRightLinks.FindAll(x => role.FunctionIds.Contains(x.ParentListItemId)).Select(x => x.ChildListItemId).Distinct());
            }

            UpdateStatus("Integrating Functions");
            foreach (ReportFunction function in reportGen.Functions)
            {
                function.RoleIds.AddRange(roleToFunctionLinks.FindAll(x => x.ChildListItemId == function.Id).Select(x => x.ParentListItemId).Distinct());
                function.RightIds.AddRange(functionToRightLinks.FindAll(x => x.ParentListItemId == function.Id).Select(x => x.ChildListItemId).Distinct());
            }

            UpdateStatus("Integrating Rights");
            foreach (ReportRight right in reportGen.Rights)
            {
                right.FunctionIds.AddRange(functionToRightLinks.FindAll(x => x.ChildListItemId == right.Id).Select(x => x.ParentListItemId).Distinct());
                right.RoleIds.AddRange(roleToFunctionLinks.FindAll(x => right.FunctionIds.Contains(x.ChildListItemId)).Select(x => x.ParentListItemId).Distinct());
            }

            return true;
        }

        private void UpdateStatus(string status)
        {
            this.lblStatus.Text = status;
            lblStatus.BackColor = SystemColors.Control;

            Application.DoEvents();
        }

        private void LoadSettings()
        {
            rdoOracle.Checked = this.settings.OracleSourceSelected;
            rdoSQL.Checked = this.settings.SQLSourceSelected;

            this.txtFileName.Text = "SecurityReport";

            if (!string.IsNullOrWhiteSpace(this.settings.OutputFolder))
            {
                this.txtOutputPath.Text = this.settings.OutputFolder;
            }
            else
            {
                this.txtOutputPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            if (rdoOracle.Checked || rdoSQL.Checked)
            {
                SetupDatabaseInputs(rdoOracle.Checked);
            }
        }

        private void LoadTenantList(List<TenantModel> tenants)
        {
            this.cboTenant.DataSource = null;
            this.cboTenant.Items.Clear();
            this.cboTenant.DataSource = tenants;
            this.cboTenant.DisplayMember = "Name";
            this.cboTenant.ValueMember = "Id";

            if (!string.IsNullOrWhiteSpace(settings.TenantId))
            {
                for (int i = 0; i < this.cboTenant.Items.Count; i++)
                {
                    if (((TenantModel)(this.cboTenant.Items[i])).Id == this.settings.TenantId)
                    {
                        this.cboTenant.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void ShowErrorMessage(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SaveSettings()
        {
            this.settings.OracleSourceSelected = this.rdoOracle.Checked;
            this.settings.SQLSourceSelected = this.rdoSQL.Checked;
            if (rdoOracle.Checked)
            {
                this.settings.OracleServerName = this.cboDatabaseServer.Text.Trim();
                this.settings.OracleSchemaName = this.txtSchema.Text.Trim();
                this.settings.OracleUserName = this.txtUserName.Text.Trim();
            } else if (rdoSQL.Checked)
            {
                this.settings.SQLServerName = this.cboDatabaseServer.Text.Trim();
                this.settings.SQLCatalog = this.txtSchema.Text.Trim();
                this.settings.SQLUserName = this.txtUserName.Text.Trim();
            }
            this.settings.TenantId = ((TenantModel)this.cboTenant.SelectedItem).Id;
            this.settings.OutputFolder = this.txtOutputPath.Text.Trim();

            this.settings.Save();
            this.settings.Reload();
        }

        private bool ValidateDatabaseSection()
        {
            if (!(rdoOracle.Checked || rdoSQL.Checked))
            {
                this.ShowFieldError(rdoOracle, "No Database Type Selected", "Please select either Oracle or SQL as the database type");
                this.rdoSQL.BackColor = Color.MistyRose;
                return false;
            }

            if (rdoOracle.Checked)
            {
                if (cboDatabaseServer.SelectedIndex == -1)
                {
                    this.ShowFieldError(cboDatabaseServer, "No Server Selected", "Please select a database server from the list");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtSchema.Text))
                {
                    this.ShowFieldError(txtSchema, "No Schema Provided", "Please enter a Schema name for the Oracle connection");
                    return false;
                }
            }
            else if (rdoSQL.Checked)
            {
                if (string.IsNullOrWhiteSpace(this.cboDatabaseServer.Text) && this.cboDatabaseServer.SelectedIndex == -1)
                {
                    this.ShowFieldError(cboDatabaseServer, "Missing Database Information", "Please enter or select a valid SQL Server");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtSchema.Text))
                {
                    this.ShowFieldError(txtSchema, "No Catalog Provided", "Please enter an Initial Catalog for the SQL connection");
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(this.txtUserName.Text))
            {
                this.ShowFieldError(txtUserName, "Missing User Name", "Please enter a valid user name for the database connection");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                this.ShowFieldError(txtPassword, "Missing Password", "Please enter the password for the database connection");
                return false;
            }

            return true;
        }

        private bool ValidateOutputSection()
        {
            if (string.IsNullOrWhiteSpace(this.txtFileName.Text))
            {
                this.ShowFieldError(txtFileName, "Empty File Name", "Please enter a name for the report file");
                return false;
            }

            if (!Directory.Exists(this.txtOutputPath.Text))
            {
                this.ShowFieldError(txtOutputPath, "Invalid Output Path", "Please provide a valid path to save the file to");
                return false;
            }

            return true;
        }

        public void loadDataSources(bool isOracle)
        {
            List<string> hosts;

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (isOracle)
                {
                    hosts = listServices("Oracle");
                }
                else
                {
                    hosts = listServices("SqlServer");
                }

                hosts.Sort();

                cboDatabaseServer.Items.Clear();
                cboDatabaseServer.Items.AddRange(hosts.ToArray());
                cboDatabaseServer.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                cboDatabaseServer.Items.Clear();
                ShowErrorMessage("Data Source Load Exception", "Exception loading data sources.\n\n" + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void SetupDatabaseInputs(bool isOracle)
        {
            btnCreateReport.Enabled = false;
            cboTenant.DataSource = null;

            loadDataSources(isOracle);
            cboDatabaseServer.DropDownStyle = isOracle ? ComboBoxStyle.DropDownList : ComboBoxStyle.DropDown;

            lblSchema.Text = isOracle ? "Schema" : "Initial Catalog";

            if (isOracle)
            {
                if (!string.IsNullOrWhiteSpace(this.settings.OracleServerName))
                {
                    cboDatabaseServer.SelectedIndex = cboDatabaseServer.FindStringExact(this.settings.OracleServerName);
                }

                if (!string.IsNullOrWhiteSpace(this.settings.OracleSchemaName))
                {
                    this.txtSchema.Text = this.settings.OracleSchemaName;
                }

                if (!string.IsNullOrWhiteSpace(this.settings.OracleUserName))
                {
                    this.txtUserName.Text = this.settings.OracleUserName;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(this.settings.SQLServerName))
                {
                    cboDatabaseServer.SelectedIndex = cboDatabaseServer.FindStringExact(this.settings.SQLServerName);
                    if (cboDatabaseServer.SelectedIndex == -1)
                    {
                        cboDatabaseServer.Text = settings.SQLServerName;
                    }
                }

                if (!string.IsNullOrWhiteSpace(this.settings.SQLCatalog))
                {
                    this.txtSchema.Text = this.settings.SQLCatalog;
                }

                if (!string.IsNullOrWhiteSpace(this.settings.SQLUserName))
                {
                    this.txtUserName.Text = this.settings.SQLUserName;
                }
            }
        }

        private void All_TextChanged(object sender, EventArgs e)
        {
            UpdateStatus("");
            ((Control)sender).BackColor = SystemColors.Window;
        }

        private void ShowFieldError(Control control, string title, string message)
        {
            this.ShowErrorMessage(title, message);
            control.BackColor = Color.MistyRose;
            control.Focus();
        }
    }
}
