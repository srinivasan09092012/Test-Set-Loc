using SSRSImportExportWizard.Models;
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

namespace SSRSImportExportWizard
{
    public partial class DataSourceView : Form
    {
        public DataSourceView()
        {
            InitializeComponent();
        }

        public DataSourceView(ReportingService2010 reportServer, string rootPath)
        {
            InitializeComponent();

            this.ReportServer = reportServer;
            this.Reports = new List<TreeNode>();
            this.RootPath = rootPath;

            this.LoadReportDataSource();
        }

        public ReportingService2010 ReportServer { get; set; }

        public List<TreeNode> Reports { get; set; }

        public string RootPath { get; set; }

        private void LoadReportDataSource()
        {
            List<DataSourceItemModel> dataItems = new List<DataSourceItemModel>();
            List<ConnectionModel> connectionStrings = new List<ConnectionModel>();
            List<ConnectionModel> replacementStrings = null;
            CatalogItem[] items = this.ReportServer.ListChildren(string.Format(@"/{0}", this.RootPath), true);
            //int loopIndex = 0;

            foreach (CatalogItem item in items)
            {
                if (item.TypeName == "Report")
                {
                    DataSource[] dataSources = this.ReportServer.GetItemDataSources(item.Path);

                    //if (loopIndex >= 10)
                    //{
                    //    break;
                    //}

                    //loopIndex++;
                    foreach (DataSource data in dataSources)
                    {
                        DataSourceDefinition def = data.Item as DataSourceDefinition;
                        if (def != null)
                        {
                            dataItems.Add(
                            new DataSourceItemModel()
                            {
                                ConnectionString = def.ConnectString,
                                DataSourceName = data.Name,
                                ReportFullName = item.Path,
                                Extension = def.Extension
                            });
                        }
                    }
                }
            }

            DataSourceGridView.DataSource = dataItems.OrderBy(o => o.ReportFullName).ToList();

            List<DataSourceItemModel> connections = dataItems.GroupBy(test => test.ConnectionString)
                   .Select(grp => grp.First())
                   .ToList();

            foreach (DataSourceItemModel ds in connections)
            {
                if(!string.IsNullOrEmpty(ds.ConnectionString) && !string.IsNullOrEmpty(ds.ConnectionString.Trim()))
                {
                    connectionStrings.Add(new ConnectionModel()
                    {
                        CurrentValue = ds.ConnectionString,
                        NewValue = string.Empty
                    });
                }
            }

            DataSourceConnectionView.DataSource = connectionStrings.OrderBy(o => o.CurrentValue).ToList();

            replacementStrings = new List<ConnectionModel>()
            {
                new ConnectionModel() { CurrentValue = string.Empty, NewValue = string.Empty },
                new ConnectionModel() { CurrentValue = string.Empty, NewValue = string.Empty },
                new ConnectionModel() { CurrentValue = string.Empty, NewValue = string.Empty },
                new ConnectionModel() { CurrentValue = string.Empty, NewValue = string.Empty },
                new ConnectionModel() { CurrentValue = string.Empty, NewValue = string.Empty },
                new ConnectionModel() { CurrentValue = string.Empty, NewValue = string.Empty },
                new ConnectionModel() { CurrentValue = string.Empty, NewValue = string.Empty }
            };

            ReplaceStringDGV.DataSource = replacementStrings;
        }

        private void btnReplaceDataSource_Click(object sender, EventArgs e)
        {
            List<string> errors = new List<string>();
            bool validDataSource = false;
            Dictionary<string, string> replaceStrings = new Dictionary<string, string>();
            foreach (DataGridViewRow row in ReplaceStringDGV.Rows)
            {
                if(!string.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                {
                    replaceStrings.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
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
                            errors.Add(item.Path);
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
                            errors.Add("FATAL ERROR" + item.Path + Environment.NewLine + ex.Message + Environment.NewLine);
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
                                errors.Add("FATAL ERROR" + item.Path + Environment.NewLine + ex.Message + Environment.NewLine);
                            }
                        }
                    }
                }
            }

            if(errors.Count > 0)
            {
                File.WriteAllLines(@"C:\UA3\Reports\ErroredReports.txt", errors.ToArray());
                MessageBox.Show(@"DataSource updated with error. Please see error log in C:\UA3\Reports folder");
            }
            else
            {
                MessageBox.Show(@"DataSource updated successfully");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
