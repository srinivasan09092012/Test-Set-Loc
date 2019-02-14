﻿using HPE.HSP.UA3.Core.API.Logger;
using Microsoft.XmlDiffPatch;
using Newtonsoft.Json;
using SSRSImportExportWizard.ReportServer2010;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SSRSImportExportWizard
{
    public enum ReportUpdateType
    {
        New = 0,
        Modified = 1,
        None = 2
    }

    public partial class ImportReportView : Form
    {
        public ImportReportView()
        {
            InitializeComponent();
        }

        public ImportReportView(ReportingService2010 reportServer, string importPath, string reportServerPath, bool doCompare)
        {
            InitializeComponent();

            this.ReportServer = reportServer;
            this.Reports = new List<TreeNode>();
            this.UploadPath = importPath;
            this.ReportServerPath = reportServerPath;
            this.DoCompare = doCompare;
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

        public bool DoCompare { get; set; }

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
            lblImportProgress.Text = "Reports imported successfully";
            lblImportProgress.Refresh();
        }

        private void LoadImportReportFolder()
        {
            DirectoryInfo reportDir = new DirectoryInfo(this.UploadPath);
            List<TreeNode> nodes = new List<TreeNode>();
            TreeNode node = null;
            ReportUpdateType reportType = ReportUpdateType.None;

            foreach (var di in reportDir.EnumerateDirectories("*", SearchOption.AllDirectories))
            {
                foreach (var fi in di.EnumerateFiles("*", SearchOption.TopDirectoryOnly))
                {
                    TreeNode reportNode = new TreeNode(fi.Name);
                    string parent = string.Format(@"/{0}", di.FullName.Replace(this.UploadPath + "\\", string.Empty)).Replace("\\", "/");

                    if (this.DoCompare)
                    {
                        reportType = this.CompareReport(fi.FullName, fi.Name.Replace(".rdl", string.Empty), parent);
                    }

                    if (reportType == ReportUpdateType.New)
                    {
                        reportNode.BackColor = System.Drawing.Color.LightPink;
                        nodes.Add(reportNode);
                    }
                    else if (reportType == ReportUpdateType.Modified)
                    {
                        reportNode.BackColor = System.Drawing.Color.LightSeaGreen;
                        nodes.Add(reportNode);
                    }
                    else
                    {
                        nodes.Add(reportNode);
                    }
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

                if (parent.Length == 0)
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
            
            lblImportProgress.Text = "Folders created successfully";
            lblImportProgress.Refresh();
            Thread.Sleep(3000);
        }

        private void CreateDataSource(List<TreeNode> checkedList)
        {
            string rootFolder = "\\" + this.ReportServerPath;
            DirectoryInfo reportDir = new DirectoryInfo(this.UploadPath + rootFolder);
            byte[] definition = null;
            Warning[] warnings = null;
            string dataSourceURL = "/Data Sources/";
            XmlDocument xmlDoc = new XmlDocument();
            List<ItemReference> dataSourceReference = null;
            DataSourceDefinition dsDef = null;

            foreach (var di in reportDir.EnumerateDirectories("*", SearchOption.AllDirectories))
            {
                if (di.Name == "Data Sources")
                {
                    foreach (var fi in di.EnumerateFiles("*.rds", SearchOption.TopDirectoryOnly))
                    {
                        if (this.IsCatalogItemChecked(checkedList, fi.Name.Replace("\\", "/"), fi.Name))
                        {
                            xmlDoc.Load(fi.FullName);
                            dsDef = new DataSourceDefinition()
                            {
                                ConnectString = xmlDoc.GetElementsByTagName("ConnectString")[0].InnerText,
                                CredentialRetrieval = CredentialRetrievalEnum.None,
                                Enabled = true,
                                Extension = xmlDoc.GetElementsByTagName("Extension")[0].InnerText
                            };

                            XmlSerializer xs = new XmlSerializer(typeof(DataSourceDefinition), string.Empty);
                            TextWriter tw = new StreamWriter(@"datasource.xml", false);
                            xs.Serialize(tw, dsDef);
                            tw.Close();

                            FileStream stream = File.OpenRead(@"datasource.xml");
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

                            lblImportProgress.Text = "Data Source " + fi.Name.Replace(".rds", string.Empty) + " created";
                            lblImportProgress.Refresh();
                        }
                    }
                }
            }

            foreach (var di in reportDir.EnumerateDirectories("*", SearchOption.AllDirectories))
            {
                if (di.Name == "Datasets")
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
                                ItemReferenceData[] references = this.ReportServer.GetItemReferences("/Datasets/" + fi.Name.Replace(".rsd", string.Empty), "DataSet");
                                //Sets shared data source reference
                                xmlDoc.Load(fi.FullName);
                                XmlNodeList dsReferenceList = xmlDoc.GetElementsByTagName("DataSourceReference");
                                if (dsReferenceList.Count > 0 && references.Length > 0)
                                {
                                    dataSourceReference = new List<ItemReference>();
                                    dataSourceReference.Add(new ItemReference()
                                    {
                                        Name = references[0].Name,
                                        Reference = dsReferenceList[0].InnerText.Contains(dataSourceURL) ? dsReferenceList[0].InnerText : dataSourceURL + dsReferenceList[0].InnerText
                                    });

                                    this.ReportServer.SetItemReferences("/Datasets/" + fi.Name.Replace(".rsd", string.Empty), dataSourceReference.ToArray());
                                }
                            }
                            catch (Exception ex)
                            {
                                LoggerManager.Logger.LogWarning("Create DataSet error", ex);
                            }

                            lblImportProgress.Text = "Dataset " + fi.Name.Replace(".rsd", string.Empty) + " created";
                            lblImportProgress.Refresh();
                        }
                    }
                }
            }

            lblImportProgress.Text = "Data Sources created successfully";
            lblImportProgress.Refresh();
            Thread.Sleep(3000);
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

            lblImportProgress.Text = "Components created successfully";
            lblImportProgress.Refresh();
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
                            string parent = string.Format(@"/{0}", di.FullName.Replace(this.UploadPath + "\\", string.Empty)).Replace("\\", "/");

                            try
                            {
                                FileStream stream = File.OpenRead(fi.FullName);
                                definition = new byte[stream.Length];
                                stream.Read(definition, 0, (int)stream.Length);
                                stream.Close();

                                try
                                {
                                    this.ReportServer.DeleteItem(parent + "/" + fi.Name.Replace(".rdl", string.Empty));
                                }
                                catch
                                {
                                }

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
                                else
                                {
                                    DataSource[] dataSources = this.ReportServer.GetItemDataSources(parent + "/" + fi.Name.Replace(".rdl", string.Empty));
                                    foreach (DataSource data in dataSources)
                                    {
                                        DataSourceDefinition def = data.Item as DataSourceDefinition;

                                        if (def != null)
                                        {
                                            if (def.Extension != null && def.Extension.ToLower().Equals("xml"))
                                            {
                                                def.CredentialRetrieval = CredentialRetrievalEnum.None;
                                            }
                                        }
                                    }

                                    if (dataSources != null && dataSources.ToList().Count() > 0)
                                    {
                                        try
                                        {
                                            this.ReportServer.SetItemDataSources(parent + "/" + fi.Name.Replace(".rdl", string.Empty), dataSources);
                                        }
                                        catch (Exception ex)
                                        {
                                            LoggerManager.Logger.LogWarning("Update Item DataSources error", ex);
                                        }
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

                            lblImportProgress.Text = "Report " + fi.Name.Replace(".rdl", string.Empty) + " created";
                            lblImportProgress.Refresh();
                        }
                    }
                }
            }

            lblImportProgress.Text = "Reports created successfully";
            lblImportProgress.Refresh();
            Thread.Sleep(3000);
        }

        private ReportUpdateType CompareReport(string reportFullPath, string reportName, string parent, bool showCompare = false)
        {
            XmlDiff diff = new XmlDiff();
            XmlDiffOptions diffOptions = new XmlDiffOptions();
            XmlDocument doc = new XmlDocument();
            string reportLayout = "<Report>{0}</Report>";
            XmlDocument xmlDoc = new XmlDocument();
            Random r = new Random();
            int randonNumber = r.Next();
            string tempFile1 = string.Empty, tempFile2 = string.Empty, file2 = string.Empty;
            string startupPath = Application.StartupPath;
            //output diff file.
            string diffFile = startupPath + Path.DirectorySeparatorChar + "vxd.out";
            XmlTextWriter tw = new XmlTextWriter(new StreamWriter(diffFile));
            tw.Formatting = System.Xml.Formatting.Indented;

            //This method sets the diff.Options property.
            diff.Algorithm = XmlDiffAlgorithm.Precise;
            diffOptions = diffOptions | XmlDiffOptions.IgnoreChildOrder;
            diffOptions = diffOptions | XmlDiffOptions.IgnoreComments;
            diffOptions = diffOptions | XmlDiffOptions.IgnoreDtd;
            diffOptions = diffOptions | XmlDiffOptions.IgnoreWhitespace;
            diffOptions = diffOptions | XmlDiffOptions.IgnoreXmlDecl;
            diffOptions = diffOptions | XmlDiffOptions.IgnorePI;
            diffOptions = diffOptions | XmlDiffOptions.IgnorePrefixes;
            diffOptions = diffOptions | XmlDiffOptions.IgnoreNamespaces;
            diff.Options = diffOptions;
            bool isEqual = false;
            file2 = startupPath + Path.DirectorySeparatorChar + "file2.rdl";
            //Now compare the two files.
            try
            {
                if (!this.GetReport(reportName, parent))
                {
                    return ReportUpdateType.New;
                }

                xmlDoc.Load(reportFullPath);
                XmlNodeList srcReportSections = xmlDoc.GetElementsByTagName("ReportSections");
                if (srcReportSections != null && srcReportSections.Count > 0)
                {
                    tempFile1 = startupPath + Path.DirectorySeparatorChar + "file1.xml";
                    File.WriteAllText(tempFile1, string.Format(reportLayout, srcReportSections[0].InnerXml));
                }

                xmlDoc.Load(file2);
                XmlNodeList targetReportSections = xmlDoc.GetElementsByTagName("ReportSections");
                if (targetReportSections != null && targetReportSections.Count > 0)
                {
                    tempFile2 = startupPath + Path.DirectorySeparatorChar + "file2.xml";
                    File.WriteAllText(tempFile2, string.Format(reportLayout, targetReportSections[0].InnerXml));
                }

                isEqual = diff.Compare(tempFile1, tempFile2, false, tw);
            }
            catch (XmlException xe)
            {
                LoggerManager.Logger.LogWarning("An XmlException occured while comparing", xe);
                return ReportUpdateType.None;
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogWarning("An exception occured while comparing", ex);
                return ReportUpdateType.None;
            }
            finally
            {
                tw.Close();
            }

            if (isEqual)
            {
                //This means the files were identical for given options.
                //MessageBox.Show("Files Identical for the given options");
                return ReportUpdateType.None; //dont need to show the differences.
            }

            if (showCompare)
            {
                //Files were not equal, so construct XmlDiffView.
                XmlDiffView dv = new XmlDiffView();

                //Load the original file again and the diff file.
                XmlTextReader orig = new XmlTextReader(tempFile1);
                XmlTextReader diffGram = new XmlTextReader(diffFile);
                dv.Load(orig, diffGram);

                //Wrap the HTML file with necessary html and 
                //body tags and prepare it before passing it to the GetHtml method.

                string tempFile = startupPath + Path.DirectorySeparatorChar + "diff" + randonNumber + ".htm";
                StreamWriter sw1 = new StreamWriter(tempFile);

                sw1.Write("<html><body><table width='100%'>");
                //Write Legend.
                sw1.Write("<tr><td colspan='2' align='center'><b>Legend:</b> <font style='background-color: yellow'" +
                    " color='black'>added</font>&nbsp;&nbsp;<font style='background-color: red'" +
                    " color='black'>removed</font>&nbsp;&nbsp;<font style='background-color: " +
                    "lightgreen' color='black'>changed</font>&nbsp;&nbsp;" +
                    "<font style='background-color: red' color='blue'>moved from</font>" +
                    "&nbsp;&nbsp;<font style='background-color: yellow' color='blue'>moved to" +
                    "</font>&nbsp;&nbsp;<font style='background-color: white' color='#AAAAAA'>" +
                    "ignored</font></td></tr>");

                //This gets the differences but just has the 
                //rows and columns of an HTML table
                dv.GetHtml(sw1);

                //Finish wrapping up the generated HTML and complete the file.
                sw1.Write("</table></body></html>");

                //HouseKeeping...close everything we dont want to lock.
                sw1.Close();
                dv = null;
                orig.Close();
                diffGram.Close();
                File.Delete(diffFile);


                ReportCompare browse = new ReportCompare(tempFile);
                browse.Show(); //Display it!
            }

            return ReportUpdateType.Modified;
        }

        private bool GetReport(string reportName, string parent)
        {
            byte[] rpt_def = null;
            XmlDocument doc = new XmlDocument();
            string sOutFile = "";
            string startupPath = Application.StartupPath;
            //output diff file.
            sOutFile = startupPath + Path.DirectorySeparatorChar + "file2.rdl";

            try
            {
                rpt_def = this.ReportServer.GetItemDefinition(parent + "/" + reportName);
                MemoryStream stream = new MemoryStream(rpt_def);
                doc.Load(stream);
                doc.Save(sOutFile);
                return true;
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogWarning("Error while importing report", ex);
                return false;
            }
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

        private void ImportTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeView node = sender as TreeView;
            if (node.SelectedNode.BackColor == System.Drawing.Color.LightSeaGreen)
            {
                string fullName = node.SelectedNode.FullPath;
                string reportName = node.SelectedNode.Text.Replace(".rdl", string.Empty);
                string parent = node.SelectedNode.Parent.Text.Replace("\\", "/");
                this.CompareReport(fullName, reportName, parent, true);
            }
        }

        private void btnUpdateDS_Click(object sender, EventArgs e)
        {
            this.Close();
            Cursor.Current = Cursors.WaitCursor;
            new DataSourceView(this.ReportServer, this.ReportServerPath).ShowDialog();
            Cursor.Current = Cursors.Default;
        }
    }
}