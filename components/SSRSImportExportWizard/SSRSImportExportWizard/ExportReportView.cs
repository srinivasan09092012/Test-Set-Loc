using HPE.HSP.UA3.Core.API.Logger;
using SSRSImportExportWizard.ReportServer2010;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            ExportTreeView.CheckBoxes = true;
            TreeNode rootNode = new TreeNode(this.ReportServer.Url, this.Reports.ToArray());
            rootNode.Checked = true;
            rootNode.Expand();
            ExportTreeView.Nodes.Add(rootNode);
            this.CheckTreeViewNode(rootNode, true);
        }

        public ReportingService2010 ReportServer { get; set; }

        public List<TreeNode> Reports { get; set; }

        public string DownloadPath { get; set; }

        private void LoadExportReportFolder()
        {
            List<TreeNode> nodes = new List<TreeNode>();
            CatalogItem[] items = null;
            CatalogItem[] childItems = null;

            try
            {
                items = this.ReportServer.ListChildren(@"/", true);
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogFatal("Error while displaying Reports", ex);
                MessageBox.Show("Error while displaying Reports");
                return;
            }

            foreach (CatalogItem item in items)
            {
                if (item.TypeName == "Folder")
                {
                    try
                    {
                        childItems = this.ReportServer.ListChildren((string.IsNullOrEmpty(item.Path) ? string.Format(@"/{0}", item.Path) : string.Format(@"{0}", item.Path)), false);
                    }
                    catch (Exception ex)
                    {
                        LoggerManager.Logger.LogWarning("Error while displaying reports under folder", ex);
                    }

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
                        else if (childItem.TypeName == "Resource")
                        {
                            nodes.Add(new TreeNode(childItem.Name));
                        }
                        else if (childItem.TypeName == "Component")
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
            List<TreeNode> checkedList = new List<TreeNode>();
            this.LookupChecks(ExportTreeView.Nodes, checkedList);
            Cursor.Current = Cursors.WaitCursor;
            btnExportReports.Enabled = false;
            CatalogItem[] items = null;
            CatalogItem[] childItems = null;

            try
            {
                items = this.ReportServer.ListChildren(@"/", true);
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogFatal("Error while exporting Reports", ex);
                MessageBox.Show("Error while exporting Report items");
                return;
            }

            if (checkedList.Count > 0)
            {
                foreach (CatalogItem item in items)
                {
                    if (this.IsItemChecked(checkedList, item.Path, item.Name))
                    {
                        if (item.TypeName == "Folder")
                        {
                            try
                            {
                                childItems = this.ReportServer.ListChildren((string.IsNullOrEmpty(item.Path) ? string.Format(@"/{0}", item.Path) : string.Format(@"{0}", item.Path)), false);
                            }
                            catch (Exception ex)
                            {
                                LoggerManager.Logger.LogWarning("Error while exporting reports under folder", ex);
                            }

                            foreach (CatalogItem childItem in childItems)
                            {
                                if (this.IsItemChecked(checkedList, childItem.Path, childItem.Name))
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
                                        this.DownloadDataSets(childItem);
                                    }
                                    else if (childItem.TypeName == "Resource")
                                    {
                                        if (!childItem.Name.Contains("sln"))
                                        {
                                            this.DownloadResources(childItem);
                                        }
                                    }
                                    else if (childItem.TypeName == "Component")
                                    {
                                        this.DownloadComponents(childItem);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Items not selected");
            }

            Cursor.Current = Cursors.Default;
            MessageBox.Show("Reports downloaded successfully");
            btnExportReports.Enabled = true;
        }

        private bool IsItemChecked(List<TreeNode> checkedList, string path, string name)
        {
            return checkedList.Exists(f => f.Text.Equals(name) || f.Text.Equals(path));
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

        private void DownloadDataSource(CatalogItem item)
        {
            byte[] rpt_def = null;
            XmlDocument doc = new XmlDocument();
            string sOutFile = "";

            try
            {
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
            catch (Exception ex)
            {
                LoggerManager.Logger.LogWarning("Error while exporting datasource", ex);
            }
        }

        private void DownloadDataSets(CatalogItem item)
        {
            byte[] rpt_def = null;
            XmlDocument doc = new XmlDocument();
            string sOutFile = "";

            try
            {
                rpt_def = this.ReportServer.GetItemDefinition(item.Path);
                MemoryStream stream = new MemoryStream(rpt_def);

                sOutFile = string.Format(@"{0}{1}.rsd", this.DownloadPath + item.Path.Replace(item.Name, string.Empty), item.Name);

                if (!Directory.Exists(this.DownloadPath + item.Path.Replace(item.Name, string.Empty)))
                    Directory.CreateDirectory(this.DownloadPath + item.Path.Replace(item.Name, string.Empty));

                if (File.Exists(sOutFile))
                    File.Delete(sOutFile);

                doc.Load(stream);
                doc.Save(sOutFile);
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogWarning("Error while exporting datasets", ex);
            }
        }

        private void DownloadComponents(CatalogItem item)
        {
            byte[] rpt_def = null;
            XmlDocument doc = new XmlDocument();
            string sOutFile = "";

            try
            {
                rpt_def = this.ReportServer.GetItemDefinition(item.Path);
                MemoryStream stream = new MemoryStream(rpt_def);

                sOutFile = string.Format(@"{0}{1}", this.DownloadPath + item.Path.Replace(item.Name, string.Empty), item.Name);

                if (!Directory.Exists(this.DownloadPath + item.Path.Replace(item.Name, string.Empty)))
                    Directory.CreateDirectory(this.DownloadPath + item.Path.Replace(item.Name, string.Empty));

                if (File.Exists(sOutFile))
                    File.Delete(sOutFile);

                doc.Load(stream);
                doc.Save(sOutFile);
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogWarning("Error while exporting component", ex);
            }
        }

        private void DownloadResources(CatalogItem item)
        {
            byte[] rpt_def = null;
            XmlDocument doc = new XmlDocument();
            
            string sOutFile = "";

            try
            {
                rpt_def = this.ReportServer.GetItemDefinition(item.Path);
                MemoryStream stream = new MemoryStream();

                sOutFile = string.Format(@"{0}{1}", this.DownloadPath + item.Path.Replace(item.Name, string.Empty), item.Name);
                sOutFile = sOutFile.Replace("\\", "/");

                if (!Directory.Exists(this.DownloadPath + item.Path.Replace(item.Name, string.Empty)))
                    Directory.CreateDirectory(this.DownloadPath + item.Path.Replace(item.Name, string.Empty));

                if (File.Exists(sOutFile))
                    File.Delete(sOutFile);

                using (stream = new MemoryStream(rpt_def))
                {
                    Image img = Image.FromStream(stream);
                    img.Save(sOutFile);
                    img.Dispose();
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogWarning("Error while exporting component", ex);
            }
        }

        private void DownloadReports(CatalogItem item)
        {
            byte[] rpt_def = null;
            XmlDocument doc = new XmlDocument();
            string sOutFile = "";

            try
            {
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
            catch (Exception ex)
            {
                LoggerManager.Logger.LogWarning("Error while exporting report", ex);
            }
        }

        private void ExportTreeView_AfterCheck(object sender, TreeViewEventArgs e)
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

        private void ExportClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
