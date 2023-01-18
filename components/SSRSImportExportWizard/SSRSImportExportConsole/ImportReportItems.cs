using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSRSImportExportConsole.ReportServer2010;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using HPE.HSP.UA3.Core.API.Logger;
using System.Drawing;

namespace SSRSImportExportConsole
{
    public class ImportReportItems
    {
        private ReportingService2010 ReportServer;
        private string UploadPath;
        private string BackupPath;

        public string ReportServerPath { get; set; }

        public ImportReportItems(ReportingService2010 reportServer, string uploadPath, string backupPath)
        {
            this.ReportServer = reportServer;
            this.UploadPath = uploadPath;
            this.BackupPath = backupPath;
            this.ReportServerPath = string.Empty;

            this.ExportReports();
            this.CreateFolders();
            this.CreateDataSource();
            this.CreateReports();
        }

        static void DownloadReports(ReportingService2010 ReportServer, CatalogItem item, string DownloadPath)
        {
            byte[] rpt_def;
            XmlDocument doc = new XmlDocument();
            string sOutFile;

            try
            {
                rpt_def = ReportServer.GetItemDefinition(item.Path);
                MemoryStream stream = new MemoryStream(rpt_def);
                sOutFile = string.Format(@"{0}{1}.rdl", DownloadPath + item.Path.Replace(item.Name, string.Empty), item.Name);

                if (!Directory.Exists(DownloadPath + item.Path.Replace(item.Name, string.Empty)))
                    Directory.CreateDirectory(DownloadPath + item.Path.Replace(item.Name, string.Empty));

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

        static void DownloadDataSource(ReportingService2010 ReportServer, CatalogItem item, string DownloadPath)
        {
            byte[] rpt_def;
            XmlDocument doc = new XmlDocument();
            string sOutFile;

            try
            {
                rpt_def = ReportServer.GetItemDefinition(item.Path);
                MemoryStream stream = new MemoryStream(rpt_def);

                sOutFile = string.Format(@"{0}{1}.rds", DownloadPath + item.Path.Replace(item.Name, string.Empty), item.Name);

                if (!Directory.Exists(DownloadPath + item.Path.Replace(item.Name, string.Empty)))
                    Directory.CreateDirectory(DownloadPath + item.Path.Replace(item.Name, string.Empty));

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

        static void DownloadDataSets(ReportingService2010 ReportServer, CatalogItem item, string DownloadPath)
        {
            byte[] rpt_def;
            XmlDocument doc = new XmlDocument();
            string sOutFile;

            try
            {
                rpt_def = ReportServer.GetItemDefinition(item.Path);
                MemoryStream stream = new MemoryStream(rpt_def);

                sOutFile = string.Format(@"{0}{1}.rsd", DownloadPath + item.Path.Replace(item.Name, string.Empty), item.Name);

                if (!Directory.Exists(DownloadPath + item.Path.Replace(item.Name, string.Empty)))
                    Directory.CreateDirectory(DownloadPath + item.Path.Replace(item.Name, string.Empty));

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

        static void DownloadComponents(ReportingService2010 ReportServer, CatalogItem item, string DownloadPath)
        {
            byte[] rpt_def;
            XmlDocument doc = new XmlDocument();
            string sOutFile;

            try
            {
                rpt_def = ReportServer.GetItemDefinition(item.Path);
                MemoryStream stream = new MemoryStream(rpt_def);

                sOutFile = string.Format(@"{0}{1}", DownloadPath + item.Path.Replace(item.Name, string.Empty), item.Name);

                if (!Directory.Exists(DownloadPath + item.Path.Replace(item.Name, string.Empty)))
                    Directory.CreateDirectory(DownloadPath + item.Path.Replace(item.Name, string.Empty));

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

        static void DownloadResources(ReportingService2010 ReportServer, CatalogItem item, string DownloadPath)
        {
            byte[] rpt_def;
            string sOutFile;

            try
            {
                rpt_def = ReportServer.GetItemDefinition(item.Path);
                MemoryStream stream = new MemoryStream();

                sOutFile = string.Format(@"{0}{1}", DownloadPath + item.Path.Replace(item.Name, string.Empty), item.Name);
                sOutFile = sOutFile.Replace("\\", "/");

                if (!Directory.Exists(DownloadPath + item.Path.Replace(item.Name, string.Empty)))
                    Directory.CreateDirectory(DownloadPath + item.Path.Replace(item.Name, string.Empty));

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

        private void ExportReports()
        {
            if (!string.IsNullOrEmpty(BackupPath))
            {
                LoggerManager.Logger.LogInformational("======================================================================================================");
                LoggerManager.Logger.LogInformational("Export process started");

                BackupPath = BackupPath + @"\" + string.Format("{0:yyyyMMdd-HHmm}", DateTime.Now);

                if (!Directory.Exists(BackupPath))
                {
                    Directory.CreateDirectory(BackupPath);
                    LoggerManager.Logger.LogInformational("Export path " + BackupPath + " created");
                }
                else
                {
                    LoggerManager.Logger.LogInformational("Export path " + BackupPath + " exists");
                }

                CatalogItem[] items = ReportServer.ListChildren(@"/", true);
                foreach (CatalogItem item in items.OrderBy(a => a.Path))
                {
                    if (item.TypeName == "Folder")
                    {
                        CatalogItem[] childItems = ReportServer.ListChildren((string.IsNullOrEmpty(item.Path) ? string.Format(@"/{0}", item.Path) : string.Format(@"{0}", item.Path)), false);

                        foreach (CatalogItem childItem in childItems)
                        {
                            LoggerManager.Logger.LogInformational("Exporting " + childItem.Path + @"/" + childItem.Name);

                            if (childItem.TypeName == "Report")
                            {
                                DownloadReports(ReportServer, childItem, BackupPath);
                            }
                            else if (childItem.TypeName == "DataSource")
                            {
                                DownloadDataSource(ReportServer, childItem, BackupPath);
                            }
                            else if (childItem.TypeName == "DataSet")
                            {
                                DownloadDataSets(ReportServer, childItem, BackupPath);
                            }
                            else if (childItem.TypeName == "Resource")
                            {
                                if (!childItem.Name.Contains("sln"))
                                {
                                    DownloadResources(ReportServer, childItem, BackupPath);
                                }
                            }
                            else if (childItem.TypeName == "Component")
                            {
                                DownloadComponents(ReportServer, childItem, BackupPath);
                            }
                        }
                    }
                }

                LoggerManager.Logger.LogInformational("Export process ended");
            }
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
            StringBuilder sb = new StringBuilder();

            LoggerManager.Logger.LogInformational("======================================================================================================");
            LoggerManager.Logger.LogInformational("Report Import process started");

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

                            try
                            {
                                this.ReportServer.DeleteItem(parent + "/" + fi.Name.Replace(".rdl", string.Empty));
                            }
                            catch { }

                            this.ReportServer.CreateCatalogItem("Report", fi.Name.Replace(".rdl", string.Empty), parent, true, definition, null, out warnings);
                        }
                        catch (Exception ex)
                        {
                            LoggerManager.Logger.LogFatal("Create Report error: " + ex.ToString());
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
                                    LoggerManager.Logger.LogFatal("Update Item DataSources error: " + ex.ToString());
                                }
                            }
                            else
                            {
                                try
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
                                catch (Exception ex)
                                {
                                    LoggerManager.Logger.LogWarning("Get Item DataSources error", ex);
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
                                    LoggerManager.Logger.LogFatal("Update Item References error: " + ex.ToString());
                                }
                            }
                        }

                        LoggerManager.Logger.LogInformational("Report " + fi.FullName.Replace(".rdl", string.Empty) + " created successfully");
                    }
                }
            }

            LoggerManager.Logger.LogInformational("Report Import process ended");
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
            DataSourceDefinition dsDef = null;

            LoggerManager.Logger.LogInformational("======================================================================================================");
            LoggerManager.Logger.LogInformational("DataSource Import process started");

            foreach (var di in reportDir.EnumerateDirectories("*", SearchOption.AllDirectories))
            {
                if (di.Name == "Data Sources")
                {
                    foreach (var fi in di.EnumerateFiles("*.rds", SearchOption.TopDirectoryOnly))
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
                            LoggerManager.Logger.LogInformational("Data Source " + fi.Name.Replace(".rds", string.Empty) + " created successfully");
                        }
                        catch (Exception ex)
                        {
                            LoggerManager.Logger.LogFatal("Create DataSource error: " + ex.ToString());
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

                            LoggerManager.Logger.LogInformational("Dataset " + fi.Name.Replace(".rsd", string.Empty) + " created successfully");
                        }
                        catch (Exception ex)
                        {
                            LoggerManager.Logger.LogFatal("Create DataSet error: " + ex.ToString());
                        }
                    }
                }
            }

            try
            {
                File.Delete(@"datasource.xml");
            }
            catch { }

            LoggerManager.Logger.LogInformational("DataSource Import process ended");
        }

        private void CreateFolders()
        {
            string rootFolder = "\\";
            DirectoryInfo reportDir = new DirectoryInfo(this.UploadPath + rootFolder);
            string parent = string.Empty;

            LoggerManager.Logger.LogInformational("======================================================================================================");
            LoggerManager.Logger.LogInformational("Folder Import process started");

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
                    LoggerManager.Logger.LogInformational("Folder " + di.Name + " created successfully");
                }
                catch
                {
                    LoggerManager.Logger.LogWarning(di.Name + " folder already exist");
                }
            }

            LoggerManager.Logger.LogInformational("Folder Import process ended");
        }
    }
}
