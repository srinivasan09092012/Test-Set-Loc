using SSRSImportExportWizard.ReportServer2010;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SSRSImportExportWizard
{
    public partial class ExportReportView : Form
    {
        public ExportReportView()
        {
            InitializeComponent();
        }

        public ExportReportView(ReportingService2010 reportServer, string downloadPath)
        {
            InitializeComponent();

            this.ReportServer = reportServer;
            this.Reports = new List<TreeNode>();
            this.DownloadPath = downloadPath;
            this.LoadExportReportFolder();
            ExportTreeView.Nodes.Add(new TreeNode(this.ReportServer.Url, this.Reports.ToArray()));
            ExportTreeView.ExpandAll();
        }

        public ReportingService2010 ReportServer { get; set; }

        public List<TreeNode> Reports { get; set; }

        public string DownloadPath { get; set; }

        private void LoadExportReportFolder()
        {
            List<TreeNode> nodes = new List<TreeNode>();
            CatalogItem[] items = this.ReportServer.ListChildren(@"/", true);

            foreach (CatalogItem item in items)
            {
                if (item.TypeName == "Folder")
                {
                    CatalogItem[] childItems = this.ReportServer.ListChildren((string.IsNullOrEmpty(item.Path) ? string.Format(@"/{0}", item.Path) : string.Format(@"{0}", item.Path)), false);

                    foreach (CatalogItem childItem in childItems)
                    {
                        if (childItem.TypeName == "Report")
                        {
                            nodes.Add(new TreeNode(childItem.Name));
                        }
                        else if (childItem.TypeName == "DataSource" || childItem.TypeName == "DataSet")
                        {
                            nodes.Add(new TreeNode(childItem.Name));
                        }
                    }

                    if(nodes.Count > 0)
                    {
                        this.Reports.Add(new TreeNode(item.Path, nodes.ToArray()));
                        nodes.Clear();
                    }
                }
            }
        }

        private void btnExportReports_Click(object sender, EventArgs e)
        {
            btnExportReports.Enabled = false;
            CatalogItem[] items = this.ReportServer.ListChildren(@"/", true);
            
            foreach (CatalogItem item in items)
            {
                if (item.TypeName == "Folder")
                {
                    CatalogItem[] childItems = this.ReportServer.ListChildren((string.IsNullOrEmpty(item.Path) ? string.Format(@"/{0}", item.Path) : string.Format(@"{0}", item.Path)), false);

                    foreach (CatalogItem childItem in childItems)
                    {
                        if (childItem.TypeName == "Report")
                        {
                            this.DownloadReports(childItem);
                        }
                        else if (childItem.TypeName == "DataSource")
                        {
                            this.DownloadDataSource(childItem);
                        }
                        else if (childItem.TypeName == "DataSet")
                        {
                            this.DownloadDataSource(childItem);
                        }
                    }
                }
            }

            MessageBox.Show("Reports downloaded successfully");
            btnExportReports.Enabled = true;
        }

        private void DownloadDataSource(CatalogItem item)
        {
            byte[] rpt_def = null;
            XmlDocument doc = new XmlDocument();
            string sOutFile = "";

            rpt_def = this.ReportServer.GetItemDefinition(item.Path);
            MemoryStream stream = new MemoryStream(rpt_def);

            sOutFile = string.Format(@"{0}{1}.rds", this.DownloadPath + item.Path.Replace(item.Name, string.Empty), item.Name);

            if (!Directory.Exists(this.DownloadPath + item.Path.Replace(item.Name, string.Empty)))
                Directory.CreateDirectory(this.DownloadPath + item.Path.Replace(item.Name, string.Empty));

            if (File.Exists(sOutFile))
                File.Delete(sOutFile);

            doc.Load(stream);
            doc.Save(sOutFile);
        }

        private void DownloadReports(CatalogItem item)
        {
            byte[] rpt_def = null;
            XmlDocument doc = new XmlDocument();
            string sOutFile = "";

            rpt_def = this.ReportServer.GetItemDefinition(item.Path);
            MemoryStream stream = new MemoryStream(rpt_def);

            sOutFile = string.Format(@"{0}{1}.rdl", this.DownloadPath + item.Path.Replace(item.Name, string.Empty), item.Name);

            if (!Directory.Exists(this.DownloadPath + item.Path.Replace(item.Name, string.Empty)))
                Directory.CreateDirectory(this.DownloadPath + item.Path.Replace(item.Name, string.Empty));

            if (File.Exists(sOutFile))
                File.Delete(sOutFile);

            doc.Load(stream);
            doc.Save(sOutFile);
        }
    }
}
