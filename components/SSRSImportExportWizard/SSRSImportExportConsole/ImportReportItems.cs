using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSRSImportExportConsole.ReportServer2010;
using System.IO;
using System.Xml;

namespace SSRSImportExportConsole
{
    public class ImportReportItems
    {
        private ReportingService2010 ReportServer;
        private string UploadPath;

        public string ReportServerPath { get; set; }

        public ImportReportItems(ReportingService2010 reportServer, string uploadPath)
        {
            this.ReportServer = reportServer;
            this.UploadPath = uploadPath;

            this.CreateFolders();
            this.CreateDataSource();
            this.CreateReports();
        }

        private void CreateReports()
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
                        string parent = string.Format(@"/{0}", di.FullName.Replace(this.UploadPath + "\\", string.Empty)).Replace("\\", "/");

                        try
                        {
                            FileStream stream = File.OpenRead(fi.FullName);
                            definition = new byte[stream.Length];
                            stream.Read(definition, 0, (int)stream.Length);
                            stream.Close();
                            this.ReportServer.CreateCatalogItem("Report", fi.Name.Replace(".rdl", string.Empty), parent, true, definition, null, out warnings);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Create Report error: " + ex.ToString());
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
                                    Console.WriteLine("Update Item DataSources error: " + ex.ToString());
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
                                    Console.WriteLine("Update Item References error: " + ex.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CreateDataSource()
        {
            string rootFolder = "\\" + this.ReportServerPath;
            DirectoryInfo reportDir = new DirectoryInfo(this.UploadPath + rootFolder);
            byte[] definition = null;
            Warning[] warnings = null;
            string dataSourceURL = "/Data Sources/";
            XmlDocument xmlDoc = new XmlDocument();
            List<ItemReference> dataSourceReference = null;

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
                        try
                        {
                            this.ReportServer.CreateCatalogItem("DataSource", fi.Name.Replace(".rds", string.Empty), "/Data Sources", true, definition, null, out warnings);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Create DataSource error: " + ex.ToString());
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
                            Console.WriteLine("Create DataSet error: " + ex.ToString());
                        }
                    }
                }
            }
        }

        private void CreateFolders()
        {
            string rootFolder = "\\";
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

                try
                {
                    this.ReportServer.CreateFolder(di.Name, parent.Replace("\\", "/"), null);
                }
                catch
                {
                    Console.WriteLine("Folder already exists");
                }
            }
        }
    }
}
