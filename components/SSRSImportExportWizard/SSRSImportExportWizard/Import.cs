using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using SSRSImportExportWizard.ReportServer2010;
using System.Web.Services.Protocols;

namespace SSRSImportExportWizard
{
    public partial class ImportControl : UserControl
    {
        public ImportControl()
        {
            InitializeComponent();

            this.ReportServer = new ReportingService2010();
        }

        public string ReportURL { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string UploadPath { get; set; }

        public bool DoCompare { get; set; }

        public ReportingService2010 ReportServer { get; set; }

        private void btnImportTestConnection_Click(object sender, EventArgs e)
        {
            this.ReportURL = TargetURL.Text.Trim();
            this.UserName = txtUserName.Text.Trim();
            this.Password = txtPassword.Text;

            if (string.IsNullOrEmpty(this.ReportURL) || string.IsNullOrEmpty(this.UserName) || string.IsNullOrEmpty(this.Password))
            {
                lblError.Text = "Please enter required (*) field";
            }
            else
            {
                btnImportTestConnection.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;

                if (this.ConnectReportServer())
                {
                    lblTestSuccess.Text = "Test succeeded!!!";
                }

                Cursor.Current = Cursors.Default;
                btnImportTestConnection.Enabled = true;
            }
        }

        private void btnViewReportObjects_Click(object sender, EventArgs e)
        {
            this.ReportURL = TargetURL.Text.Trim();
            this.UserName = txtUserName.Text.Trim();
            this.Password = txtPassword.Text;
            this.UploadPath = txtDownloadPath.Text;
            this.DoCompare = chkCompare.Checked;

            if (string.IsNullOrEmpty(this.ReportURL) || string.IsNullOrEmpty(this.UserName) || string.IsNullOrEmpty(this.Password) || string.IsNullOrEmpty(this.UploadPath))
            {
                lblError.Text = "Please enter required (*) field";
            }
            else
            {
                if (this.ConnectReportServer())
                {
                    new ImportReportView(this.ReportServer, this.UploadPath, this.txtReportServerPath.Text, this.DoCompare).ShowDialog();
                }
            }
        }

        private bool ConnectReportServer()
        {
            bool returnFlag = false;
            this.ReportServer.Url = this.ReportURL;
            NetworkCredential cred = new NetworkCredential(this.UserName, this.Password);
            this.ReportServer.Credentials = cred;
            try
            {
                CatalogItem[] items = this.ReportServer.ListChildren(@"/", false);
                lblError.Text = string.Empty;
                return true;
            }
            catch (SoapException ex)
            {
                lblTestSuccess.Text = string.Empty;
                lblError.Text = ex.Message;
            }
            catch (Exception ex)
            {
                lblTestSuccess.Text = string.Empty;
                lblError.Text = ex.Message;
            }

            return returnFlag;
        }

        private void btnImportFolderBrowser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtDownloadPath.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }
    }
}
