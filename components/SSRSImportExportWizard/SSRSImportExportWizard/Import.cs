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
                if (this.ConnectReportServer())
                {
                    lblTestSuccess.Text = "Test succeeded!!!";
                }
            }
        }

        private void btnViewReportObjects_Click(object sender, EventArgs e)
        {
            this.ReportURL = TargetURL.Text.Trim();
            this.UserName = txtUserName.Text.Trim();
            this.Password = txtPassword.Text;
            this.UploadPath = txtDownloadPath.Text;

            if (string.IsNullOrEmpty(this.ReportURL) || string.IsNullOrEmpty(this.UserName) || string.IsNullOrEmpty(this.Password) || string.IsNullOrEmpty(this.UploadPath))
            {
                lblError.Text = "Please enter required (*) field";
            }
            else
            {
                if (this.ConnectReportServer())
                {
                    new ImportReportView(this.ReportServer, this.UploadPath, this.txtReportServerPath.Text).ShowDialog();
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
                return true;
            }
            catch (SoapException)
            {
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

            return returnFlag;
        }
    }
}
