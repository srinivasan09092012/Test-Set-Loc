using HPE.HSP.UA3.Core.API.Logger;
using Newtonsoft.Json;
using SSRSImportExportWizard.ReportServer2010;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace SSRSImportExportWizard
{
    public partial class ImportReportView : Form
    {
        public ImportReportView()
        {
            InitializeComponent();
        }

        public ImportReportView(ReportingService2010 reportServer, string importPath, string reportServerPath)
        {
            InitializeComponent();

            this.ReportServer = reportServer;
            this.Reports = new List<TreeNode>();
            this.UploadPath = importPath;
            this.ReportServerPath = reportServerPath;
            this.LoadImportReportFolder();
            TreeNode rootNode = new TreeNode(this.UploadPath, this.Reports.OrderByDescending(o => o.Name).ToArray());
            rootNode.Checked = true;
            rootNode.Expand();
            ImportTreeView.Nodes.Add(rootNode);
            this.CheckTreeViewNode(rootNode, true);
        }

        public ReportingService2010 ReportServer { get; set; }

        public List<TreeNode> Reports { get; set; }

        public string UploadPath { get; set; }

        public string ReportServerPath { get; set; }

        private void btnImportReports_Click(object sender, EventArgs e)
        {
            List<TreeNode> checkedList = new List<TreeNode>();
            this.LookupChecks(ImportTreeView.Nodes, checkedList);
            Cursor.Current = Cursors.WaitCursor;
            this.CreateFolders(checkedList);
            this.CreateDataSource(checkedList);
            this.CreateReports(checkedList);
            Cursor.Current = Cursors.Default;
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

        private void CreateFolders(List<TreeNode> checkedList)
        {
            string rootFolder = "\\" + this.ReportServerPath;
            DirectoryInfo reportDir = new DirectoryInfo(this.UploadPath + rootFolder);

            foreach (var di in reportDir.EnumerateDirectories("*", SearchOption.AllDirectories))
            {
                string parent = string.Format(@"/{0}", di.Parent.FullName.Replace(this.UploadPath + "\\", string.Empty));
                if (this.IsItemChecked(checkedList, parent.Replace("\\", "/"), di.Name))
                {
                    try
                    {
                        this.ReportServer.CreateFolder(di.Name, parent.Replace("\\", "/"), null);
                    }
                    catch(Exception ex)
                    {
                        LoggerManager.Logger.LogWarning("Folder already exist", ex);
                    }
                }
            }

            MessageBox.Show("Folders created successfully");
        }

        private void CreateDataSource(List<TreeNode> checkedList)
        {
            string rootFolder = "\\" + this.ReportServerPath;
            DirectoryInfo reportDir = new DirectoryInfo(this.UploadPath + rootFolder);
            byte[] definition = null;
            Warning[] warnings = null;

            foreach (var di in reportDir.EnumerateDirectories("*", SearchOption.AllDirectories))
            {
                if (di.Name == "Data Sources")
                {
                    foreach (var fi in di.EnumerateFiles("*.rds", SearchOption.TopDirectoryOnly))
                    {
                        FileStream stream = File.OpenRead(fi.FullName);
                        definition = new byte[stream.Length];
                        stream.Read(definition, 0, (int)stream.Length);
                        stream.Close();
                        this.ReportServer.CreateCatalogItem("DataSource", fi.Name, "/Data Sources", true, definition, null, out warnings);
                    }
                }
                else if (di.Name == "Datasets")
                {
                    foreach (var fi in di.EnumerateFiles("*.rds", SearchOption.TopDirectoryOnly))
                    {
                        FileStream stream = File.OpenRead(fi.FullName);
                        definition = new byte[stream.Length];
                        stream.Read(definition, 0, (int)stream.Length);
                        stream.Close();
                        this.ReportServer.CreateCatalogItem("DataSet", fi.Name, "/Datasets", true, definition, null, out warnings);
                    }
                }
            }
        }

        private void CreateReports(List<TreeNode> checkedList)
        {
            string rootFolder = "\\" + this.ReportServerPath;
            DirectoryInfo reportDir = new DirectoryInfo(this.UploadPath + rootFolder);
            byte[] definition = null;
            Warning[] warnings = null;
            List<DataSource> ds = null;
            List<ItemReference> references = null;

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

                        if (File.Exists(fi.FullName.Replace(fi.Extension, ".rds")))
                        {
                            ds = new List<DataSource>();
                            Dictionary<string, DataSourceReference> dataSource = JsonConvert.DeserializeObject<Dictionary<string, DataSourceReference>>(File.ReadAllText(fi.FullName.Replace(fi.Extension, ".ds")), new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.All });

                            foreach (KeyValuePair<string, DataSourceReference> def in dataSource)
                            {
                                if (def.Value is DataSourceReference)
                                {
                                    ds.Add(new DataSource()
                                    {
                                        Item = def.Value,
                                        Name = def.Key
                                    });
                                }
                            }

                            this.ReportServer.SetItemDataSources(parent + "/" + fi.Name, ds.ToArray());
                        }

                        if (File.Exists(fi.FullName.Replace(fi.Extension, ".rsd")))
                        {
                            references = new List<ItemReference>();
                            Dictionary<string, ItemReferenceData> dataSource = JsonConvert.DeserializeObject<Dictionary<string, ItemReferenceData>>(File.ReadAllText(fi.FullName.Replace(fi.Extension, ".dataset")), new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.All });

                            foreach (KeyValuePair<string, ItemReferenceData> def in dataSource)
                            {
                                if (!string.IsNullOrEmpty(def.Value.Reference))
                                {
                                    references.Add(new ItemReference()
                                    {
                                        Name = def.Key,
                                        Reference = def.Value.Reference
                                    });
                                }
                            }

                            this.ReportServer.SetItemReferences(parent + "/" + fi.Name, references.ToArray());
                        }
                    }
                }
            }

            MessageBox.Show("Reports created successfully");
        }

        private void LookupChecks(TreeNodeCollection nodes, List<TreeNode> checkedList)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    if (node.Parent != null && node.Parent.Level > 0 && node.Parent.Checked == false)
                    {
                        //node.Parent.Checked = true;
                        checkedList.Add(node.Parent);
                    }

                    checkedList.Add(node);
                }

                LookupChecks(node.Nodes, checkedList);
            }
        }

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

        private bool IsItemChecked(List<TreeNode> checkedList, string path, string name)
        {
            return checkedList.Exists(f => f.Text.Contains(name) || f.Text.Contains(path));
        }

        private void ImportTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            this.CheckTreeViewNode(e.Node, e.Node.Checked);
        }

        private void CheckTreeViewNode(TreeNode node, Boolean isChecked)
        {
            foreach (TreeNode item in node.Nodes)
            {
                item.Checked = isChecked;

                if (item.Nodes.Count > 0)
                {
                    this.CheckTreeViewNode(item, isChecked);
                }
            }
        }

        private void ImportClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}