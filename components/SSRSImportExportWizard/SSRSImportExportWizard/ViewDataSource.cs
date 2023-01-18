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
    public partial class ViewDataSource : UserControl
    {
        public ViewDataSource()
        {
            InitializeComponent();

            this.ReportServer = new ReportingService2010();
        }

        public string ReportURL { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public ReportingService2010 ReportServer { get; set; }

        public string RootPath { get; set; }

        private void btnViewDataSourceObjects_Click(object sender, EventArgs e)
        {
            this.ReportURL = TargetURL.Text.Trim();
            this.UserName = txtUserName.Text.Trim();
            this.Password = txtPassword.Text;
            this.RootPath = txtRootPath.Text;

            if (string.IsNullOrEmpty(this.ReportURL) || string.IsNullOrEmpty(this.UserName) || string.IsNullOrEmpty(this.Password) || string.IsNullOrEmpty(this.RootPath))
            {
                lblError.Text = "Please enter required (*) field";
            }
            else
            {
                if (this.ConnectReportServer())
                {
                    new DataSourceView(this.ReportServer, this.RootPath).ShowDialog();
                }
            }
        }

        private void btnDataSourceTestConnection_Click(object sender, EventArgs e)
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
                Cursor.Current = Cursors.WaitCursor;
                btnDataSourceTestConnection.Enabled = false;
                if (this.ConnectReportServer())
                {
                    lblTestSuccess.Text = "Test succeeded!!!";
                }
                btnDataSourceTestConnection.Enabled = true;
                Cursor.Current = Cursors.Default;
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
                CatalogItem[] items = this.ReportServer.ListChildren(@"/", true);
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
    }
}
