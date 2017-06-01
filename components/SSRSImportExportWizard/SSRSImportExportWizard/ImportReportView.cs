using SSRSImportExportWizard.ReportServer2010;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace SSRSImportExportWizard
{
    public partial class ImportReportView : Form
    {
        public ImportReportView()
        {
            InitializeComponent();
        }

        public ImportReportView(ReportingService2010 reportServer, string importPath)
        {
            InitializeComponent();

            this.ReportServer = reportServer;
            this.Reports = new List<TreeNode>();
            this.UploadPath = importPath;
            this.LoadImportReportFolder();
            ImportTreeView.Nodes.Add(new TreeNode(this.UploadPath, this.Reports.OrderByDescending(o => o.Name).ToArray()));
        }

        public ReportingService2010 ReportServer { get; set; }

        public List<TreeNode> Reports { get; set; }

        public string UploadPath { get; set; }

        private void btnImportReports_Click(object sender, EventArgs e)
        {
            //this.CreateFolders();
            //this.CreateReports();
            //this.CreateSingleReports();
        }

        private void LoadImportReportFolder()
        {
            DirectoryInfo reportDir = new DirectoryInfo(this.UploadPath);
            List<TreeNode> nodes = new List<TreeNode>();
            TreeNode node = null;

            foreach (var di in reportDir.EnumerateDirectories("*", SearchOption.AllDirectories))
            {
                foreach (var fi in di.EnumerateFiles("*.rd*", SearchOption.TopDirectoryOnly))
                {
                    nodes.Add(new TreeNode(fi.Name));
                }

                if (nodes.Count > 0)
                {
                    node = new TreeNode(di.FullName.Replace(this.UploadPath, string.Empty), nodes.ToArray());
                    nodes.Clear();
                    this.Reports.Add(node);
                }
            }
        }

        private void CreateFolders()
        {
            string rootFolder = "\\Tenant 3 - Customer A";
            DirectoryInfo reportDir = new DirectoryInfo(this.UploadPath + rootFolder);

            foreach (var di in reportDir.EnumerateDirectories("*", SearchOption.AllDirectories))
            {
                string parent = string.Format(@"/{0}", di.Parent.FullName.Replace(this.UploadPath + "\\", string.Empty));
                this.ReportServer.CreateFolder(di.Name, parent.Replace("\\", "/"), null);
            }

            MessageBox.Show("Folders created successfully");
        }

        private void CreateReports()
        {
            string rootFolder = "\\Tenant 3 - Customer A";
            DirectoryInfo reportDir = new DirectoryInfo(this.UploadPath + rootFolder);
            byte[] definition = null;
            Warning[] warnings = null;

            foreach (var di in reportDir.EnumerateDirectories("*", SearchOption.AllDirectories))
            {
                if (di.Name != "Data Sources" && di.Name != "Datasets")
                {
                    foreach (var fi in di.EnumerateFiles("*.rdl", SearchOption.TopDirectoryOnly))
                    {
                        FileStream stream = File.OpenRead(fi.FullName);
                        definition = new byte[stream.Length];
                        stream.Read(definition, 0, (int)stream.Length);
                        stream.Close();
                        string parent = string.Format(@"/{0}", di.FullName.Replace(this.UploadPath + "\\", string.Empty)).Replace("\\", "/");
                        this.ReportServer.CreateCatalogItem("Report", fi.Name, parent, true, definition, null, out warnings);
                    }
                }
            }

            MessageBox.Show("Reports created successfully");
        }

        //private void CreateSingleReports()
        //{
        //    string rootFolder = "\\Tenant 3 - Customer A";
        //    DirectoryInfo reportDir = new DirectoryInfo(this.UploadPath + rootFolder);
        //    byte[] definition = null;
        //    Warning[] warnings = null;

        //    foreach (var di in reportDir.EnumerateDirectories("*", SearchOption.AllDirectories))
        //    {
        //        if (di.Name != "Data Sources" && di.Name != "Datasets" && di.Name == "Test")
        //        {
        //            foreach (var fi in di.EnumerateFiles("*.rdl", SearchOption.TopDirectoryOnly))
        //            {
        //                FileStream stream = File.OpenRead(fi.FullName);
        //                definition = new byte[stream.Length];
        //                stream.Read(definition, 0, (int)stream.Length);
        //                stream.Close();

        //                string parent = string.Format(@"/{0}", di.FullName.Replace(this.UploadPath + "\\", string.Empty)).Replace("\\", "/");
        //                this.ReportServer.CreateCatalogItem("Report", fi.Name, parent, true, definition, null, out warnings);

        //                DataSource[] dataSources = this.ReportServer.GetItemDataSources(item.Path);
                        

        //                foreach (DataSource data in dataSources)
        //                {
        //                    if (data.Item.ToString().Contains("InvalidDataSourceReference"))
        //                    {
        //                        DataSourceDefinition[] dataSources = this.ImportReportSharedDataSource(fi.FullName);
        //                    }
        //                    else
        //                    {
        //                        DataSourceDefinition def = data.Item as DataSourceDefinition;
        //                        if (def != null)
        //                        {
        //                            foreach (KeyValuePair<string, string> repString in replaceStrings)
        //                            {
        //                                if (def.ConnectString != null && def.ConnectString.Contains(repString.Key))
        //                                {
        //                                    validDataSource = true;
        //                                    def.UseOriginalConnectString = false;
        //                                    def.ConnectString = def.ConnectString.Replace(repString.Key, repString.Value);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }

        //                if (validDataSource)
        //                {
        //                    try
        //                    {
        //                        this.ReportServer.SetItemDataSources(item.Path, dataSources);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        errors.Add("FATAL ERROR" + item.Path + Environment.NewLine + ex.Message + Environment.NewLine);
        //                    }
        //                }

                        

        //            }
        //        }
        //    }

        //    MessageBox.Show("Reports created successfully");
        //}

        private DataSourceDefinition[] ImportReportSharedDataSource(string fullName)
        {
            DataSourceDefinition[] dataSources = null;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fullName);
            XmlNode root = xmlDoc.DocumentElement;

            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.Name == "DataSources")
                {
                    foreach (XmlNode data in node.ChildNodes)
                    {
                        if (node.Name == "DataSource")
                        {
                            foreach (XmlNode data1 in node.ChildNodes)
                            {
                                if (node.Name == "DataSourceReference")
                                {
                                    dataSources[dataSources.Length + 1] = new DataSourceDefinition();
                                    dataSources[dataSources.Length + 1] = this.ReportServer.GetDataSourceContents(node.Value);
                                }
                            }
                        }
                    }
                }
            }

            return dataSources;
        }


    }
}
