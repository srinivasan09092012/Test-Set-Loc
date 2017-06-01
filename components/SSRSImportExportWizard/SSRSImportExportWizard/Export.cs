using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SSRSImportExportWizard.ReportServer2010;
using System.Net;
using System.Web.Services.Protocols;

namespace SSRSImportExportWizard
{
    public partial class ExportControl : UserControl
    {
        public ExportControl()
        {
            InitializeComponent();

            this.ReportServer = new ReportingService2010();
        }

        public string ReportURL { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string DownloadPath { get; set; }

        public ReportingService2010 ReportServer { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ReportURL = sourceURL.Text.Trim();
            this.UserName = txtUserName.Text.Trim();
            this.Password = txtPassword.Text;
            this.DownloadPath = txtDownloadPath.Text;

            if (string.IsNullOrEmpty(this.ReportURL) || string.IsNullOrEmpty(this.UserName) || string.IsNullOrEmpty(this.Password) || string.IsNullOrEmpty(this.DownloadPath))
            {
                lblError.Text = "Please enter required (*) field";
            }
            else
            {
                if (this.ConnectReportServer())
                {
                    new ExportReportView(this.ReportServer, this.DownloadPath).ShowDialog();
                }
            }

            btnExportReports.Enabled = true;
        }

        private bool ConnectReportServer()
        {
            bool returnFlag = false;
            this.ReportServer.Url = this.ReportURL;
            NetworkCredential cred = new NetworkCredential(this.UserName, this.Password);
            this.ReportServer.Credentials = cred;
            try
            {
                CatalogItem[] items = this.ReportServer.ListChildren(@"/", true);

                if (items.Length > 0)
                {
                    return true;
                }
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

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExportTestConnection_Click(object sender, EventArgs e)
        {
            this.ReportURL = sourceURL.Text.Trim();
            this.UserName = txtUserName.Text.Trim();
            this.Password = txtPassword.Text;

            if (string.IsNullOrEmpty(this.ReportURL) || string.IsNullOrEmpty(this.UserName) || string.IsNullOrEmpty(this.Password))
            {
                lblError.Text = "Please enter required (*) field";
            }
            else
            {
                if(this.ConnectReportServer())
                {
                    lblTestSuccess.Text = "Test succeeded!!!";
                }
            }
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            txtUserName.Text = string.Empty;
        }

        private void btnExportReports_Click(object sender, EventArgs e)
        {

        }
    }
}
