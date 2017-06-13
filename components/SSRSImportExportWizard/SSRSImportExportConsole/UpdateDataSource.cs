using SSRSImportExportConsole.ReportServer2010;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SSRSImportExportConsole
{
    public class UpdateDataSource
    {
        public ReportingService2010 ReportServer { get; set; }

        public string RootPath { get; set; }

        public UpdateDataSource(ReportingService2010 reportServer, string rootPath)
        {
            this.ReportServer = reportServer;
            this.RootPath = rootPath;
            this.SetReportDataSources();
        }

        private void SetReportDataSources()
        {
            bool validDataSource = false;
            Dictionary<string, string> replaceStrings = new Dictionary<string, string>();

            try
            {
                XElement xelement = GetFromResources(@"Reports.xml");
                var strings = from nm in xelement.Elements("ReplaceStrings")
                             select nm;

                foreach (XElement xEle in strings.Elements("ReplaceString"))
                {
                    if (!string.IsNullOrEmpty(xEle.Attribute("OldValue").Value))
                    {
                        replaceStrings.Add(xEle.Attribute("OldValue").Value, xEle.Attribute("NewValue").Value);
                    }
                }

                CatalogItem[] items = this.ReportServer.ListChildren(string.Format(@"/{0}", this.RootPath), true);

                foreach (CatalogItem item in items)
                {
                    if (item.TypeName == "Report")
                    {
                        validDataSource = false;
                        DataSource[] dataSources = this.ReportServer.GetItemDataSources(item.Path);

                        foreach (DataSource data in dataSources)
                        {
                            if (data.Item.ToString().Contains("InvalidDataSourceReference"))
                            {
                                data.Item = this.ReportServer.GetDataSourceContents(data.Name);
                                validDataSource = false;
                                break;
                            }
                            else
                            {
                                DataSourceDefinition def = data.Item as DataSourceDefinition;
                                if (def != null)
                                {
                                    foreach (KeyValuePair<string, string> repString in replaceStrings)
                                    {
                                        if (def.ConnectString != null && def.ConnectString.Contains(repString.Key))
                                        {
                                            validDataSource = true;
                                            def.UseOriginalConnectString = false;
                                            def.ConnectString = def.ConnectString.Replace(repString.Key, repString.Value);
                                        }
                                    }

                                    if (def.Extension != null && def.Extension.ToLower().Equals("xml"))
                                    {
                                        validDataSource = true;
                                        def.CredentialRetrieval = CredentialRetrievalEnum.None;
                                    }
                                }
                            }
                        }

                        if (validDataSource)
                        {
                            try
                            {
                                this.ReportServer.SetItemDataSources(item.Path, dataSources);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("FATAL ERROR: " + item.Path + " : " + ex.ToString());
                            }
                        }
                    }
                    else if (item.TypeName == "DataSource")
                    {
                        DataSourceDefinition def = this.ReportServer.GetDataSourceContents(item.Path);
                        if (def != null)
                        {
                            foreach (KeyValuePair<string, string> repString in replaceStrings)
                            {
                                if (def.ConnectString != null && def.ConnectString.Contains(repString.Key))
                                {
                                    validDataSource = true;
                                    def.UseOriginalConnectString = false;
                                    def.ConnectString = def.ConnectString.Replace(repString.Key, repString.Value);
                                }
                            }

                            if (def.Extension != null && def.Extension.ToLower().Equals("xml") && def.CredentialRetrieval != CredentialRetrievalEnum.None)
                            {
                                validDataSource = true;
                                def.CredentialRetrieval = CredentialRetrievalEnum.None;
                            }

                            if (validDataSource)
                            {
                                try
                                {
                                    this.ReportServer.SetDataSourceContents(item.Path, def);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("FATAL ERROR: " + item.Path + " : " + ex.ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while updating Report DataSources: " + ex.ToString());
            }
        }

        private static XElement GetFromResources(string resourceName)
        {
            Assembly assem = Assembly.GetExecutingAssembly();
            using (Stream stream = assem.GetManifestResourceStream(assem.GetName().Name + '.' + resourceName))
            {
                return XElement.Load(stream);
            }
        }
    }
}
