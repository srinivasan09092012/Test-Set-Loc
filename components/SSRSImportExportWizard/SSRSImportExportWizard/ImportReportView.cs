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
using System.Xml.Linq;

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
            btnImportReports.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            this.CreateFolders(checkedList);
            this.CreateDataSource(checkedList);
            this.CreateReports(checkedList);
            this.CreateComponents(checkedList);
            btnImportReports.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void LoadImportReportFolder()
        {
            DirectoryInfo reportDir = new DirectoryInfo(this.UploadPath);
            List<TreeNode> nodes = new List<TreeNode>();
            TreeNode node = null;

            foreach (var di in reportDir.EnumerateDirectories("*", SearchOption.AllDirectories))
            {
                foreach (var fi in di.EnumerateFiles("*", SearchOption.TopDirectoryOnly))
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
            string parent = string.Empty;

            if (this.ReportServerPath.Trim().Length > 0)
            {
                this.UploadPath = this.UploadPath + "\\";
            }

            foreach (var di in reportDir.EnumerateDirectories("*", SearchOption.AllDirectories))
            {
                if (this.ReportServerPath.Trim().Length > 0)
                {
                    parent = string.Format(@"/{0}", di.Parent.FullName.Replace(this.UploadPath, string.Empty));
                }
                else
                {
                    parent = string.Format(@"{0}", di.Parent.FullName.Replace(this.UploadPath, string.Empty));
                }

                if(parent.Length == 0)
                {
                    parent = "/";
                }

                if (this.IsItemChecked(checkedList, parent.Replace("\\", "/"), di.Name))
                {
                    try
                    {
                        this.ReportServer.CreateFolder(di.Name, parent.Replace("\\", "/"), null);
                    }
                    catch (Exception ex)
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
                        if (this.IsCatalogItemChecked(checkedList, fi.Name.Replace("\\", "/"), fi.Name))
                        {
                            FileStream stream = File.OpenRead(fi.FullName);
                            definition = new byte[stream.Length];
                            stream.Read(definition, 0, (int)stream.Length);
                            stream.Close();
                            try
                            {
                                this.ReportServer.CreateCatalogItem("DataSource", fi.Name.Replace(".rds", string.Empty), "/Data Sources", true, definition, null, out warnings);
                            }
                            catch (Exception ex)
                            {
                                LoggerManager.Logger.LogWarning("Create DataSource error", ex);
                            }
                        }
                    }
                }
                else if (di.Name == "Datasets")
                {
                    foreach (var fi in di.EnumerateFiles("*.rsd", SearchOption.TopDirectoryOnly))
                    {
                        if (this.IsCatalogItemChecked(checkedList, fi.Name.Replace("\\", "/"), fi.Name))
                        {
                            FileStream stream = File.OpenRead(fi.FullName);
                            definition = new byte[stream.Length];
                            stream.Read(definition, 0, (int)stream.Length);
                            stream.Close();
                            try
                            {
                                this.ReportServer.CreateCatalogItem("DataSet", fi.Name.Replace(".rsd", string.Empty), "/Datasets", true, definition, null, out warnings);
                            }
                            catch (Exception ex)
                            {
                                LoggerManager.Logger.LogWarning("Create DataSet error", ex);
                            }
                        }
                    }
                }
            }

            MessageBox.Show("Data Sources created successfully");
        }

        private void CreateComponents(List<TreeNode> checkedList)
        {
            string rootFolder = "\\" + this.ReportServerPath;
            DirectoryInfo reportDir = new DirectoryInfo(this.UploadPath + rootFolder);
            byte[] definition = null;
            Warning[] warnings = null;

            foreach (var di in reportDir.EnumerateDirectories("*", SearchOption.AllDirectories))
            {
                if (this.IsItemChecked(checkedList, di.Name.Replace("\\", "/"), di.Name))
                {
                    foreach (var fi in di.EnumerateFiles("*.", SearchOption.TopDirectoryOnly))
                    {
                        if (this.IsCatalogItemChecked(checkedList, fi.Name.Replace("\\", "/"), fi.Name))
                        {
                            FileStream stream = File.OpenRead(fi.FullName);
                            definition = new byte[stream.Length];
                            stream.Read(definition, 0, (int)stream.Length);
                            stream.Close();
                            string parent = string.Format(@"/{0}", di.FullName.Replace(this.UploadPath + "\\", string.Empty)).Replace("\\", "/");

                            try
                            {
                                this.ReportServer.CreateCatalogItem("Component", fi.Name, parent, true, definition, null, out warnings);
                            }
                            catch (Exception ex)
                            {
                                LoggerManager.Logger.LogWarning("Create Component error", ex);
                            }
                        }
                    }
                }
            }

            MessageBox.Show("Components created successfully");
        }

        private void CreateReports(List<TreeNode> checkedList)
        {
            string rootFolder = "\\" + this.ReportServerPath;
            DirectoryInfo reportDir = new DirectoryInfo(this.UploadPath + rootFolder);
            byte[] definition = null;
            Warning[] warnings = null;
            List<DataSource> ds = null;
            List<ItemReference> references = null;
            XmlDocument xmlDoc = new XmlDocument();
            DataSourceReference reference = null;
            string dataSourceFolder = @"/Data Sources/";
            string dataSetFolder = @"/Datasets/";

            foreach (var di in reportDir.EnumerateDirectories("*", SearchOption.AllDirectories))
            {
                if (di.Name != "Data Sources" && di.Name != "Datasets")
                {
                    foreach (var fi in di.EnumerateFiles("*.rdl", SearchOption.TopDirectoryOnly))
                    {
                        if (this.IsCatalogItemChecked(checkedList, fi.Name.Replace("\\", "/"), fi.Name))
                        {
                            FileStream stream = File.OpenRead(fi.FullName);
                            definition = new byte[stream.Length];
                            stream.Read(definition, 0, (int)stream.Length);
                            stream.Close();
                            string parent = string.Format(@"/{0}", di.FullName.Replace(this.UploadPath + "\\", string.Empty)).Replace("\\", "/");

                            try
                            {
                                this.ReportServer.CreateCatalogItem("Report", fi.Name.Replace(".rdl", string.Empty), parent, true, definition, null, out warnings);
                            }
                            catch (Exception ex)
                            {
                                LoggerManager.Logger.LogWarning("Create Report error", ex);
                            }

                            xmlDoc.Load(fi.FullName);
                            XmlNodeList dsReferenceList = xmlDoc.GetElementsByTagName("DataSource");
                            XmlNodeList sharedDSReferenceList = xmlDoc.GetElementsByTagName("DataSet");

                            if (dsReferenceList != null && dsReferenceList.Count > 0)
                            {
                                ds = new List<DataSource>();
                                string dataSourceName = string.Empty;

                                foreach (XmlNode node in dsReferenceList)
                                {
                                    if (node.Attributes != null && node.Attributes.Count > 0)
                                    {
                                        dataSourceName = node.Attributes["Name"].Value;
                                    }

                                    if (node["DataSourceReference"] != null)
                                    {
                                        reference = new DataSourceReference();
                                        reference.Reference = dataSourceFolder + node["DataSourceReference"].InnerText;
                                        ds.Add(new DataSource()
                                        {
                                            Item = reference,
                                            Name = dataSourceName
                                        });
                                    }
                                }

                                if (ds.Count > 0)
                                {
                                    try
                                    {
                                        this.ReportServer.SetItemDataSources(parent + "/" + fi.Name.Replace(".rdl", string.Empty), ds.ToArray());
                                    }
                                    catch (Exception ex)
                                    {
                                        LoggerManager.Logger.LogWarning("Update Item DataSources error", ex);
                                    }
                                }
                            }

                            if (sharedDSReferenceList != null && sharedDSReferenceList.Count > 0)
                            {
                                references = new List<ItemReference>();
                                string dataSetName = string.Empty;

                                foreach (XmlNode node in sharedDSReferenceList)
                                {
                                    if (node.Attributes != null && node.Attributes.Count > 0)
                                    {
                                        dataSetName = node.Attributes["Name"].Value;
                                    }

                                    foreach (XmlNode childNode in node.ChildNodes)
                                    {
                                        if (childNode.Name == "SharedDataSet")
                                        {
                                            if (childNode.FirstChild != null && !string.IsNullOrEmpty(childNode.FirstChild.InnerText))
                                            {
                                                references.Add(new ItemReference()
                                                {
                                                    Name = dataSetName,
                                                    Reference = dataSetFolder + childNode.FirstChild.InnerText
                                                });
                                            }
                                        }
                                    }
                                }

                                if (references.Count > 0)
                                {
                                    try
                                    {
                                        this.ReportServer.SetItemReferences(parent + "/" + fi.Name.Replace(".rdl", string.Empty), references.ToArray());
                                    }
                                    catch (Exception ex)
                                    {
                                        LoggerManager.Logger.LogWarning("Update Item References error", ex);
                                    }
                                }
                            }

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

                                try
                                {
                                    this.ReportServer.SetItemDataSources(parent + "/" + fi.Name, ds.ToArray());
                                }
                                catch (Exception ex)
                                {
                                    LoggerManager.Logger.LogWarning("Update Item DataSources error", ex);
                                }
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

                                try
                                {
                                    this.ReportServer.SetItemReferences(parent + "/" + fi.Name, references.ToArray());
                                }
                                catch (Exception ex)
                                {
                                    LoggerManager.Logger.LogWarning("Update Item References error", ex);
                                }
                            }
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

        private bool IsFolderExistChecked(List<TreeNode> checkedList, string folderName)
        {
            return checkedList.Exists(f => f.Text.Contains(folderName));
        }

        private bool IsItemChecked(List<TreeNode> checkedList, string path, string name)
        {
            return checkedList.Exists(f => f.Text.Contains(name) || f.Text.Contains(path));
        }

        private bool IsCatalogItemChecked(List<TreeNode> checkedList, string path, string name)
        {
            return checkedList.Exists(f => f.Text.Equals(name) || f.Text.Equals(path));
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