using HPE.HSP.UA3.Core.API.Logger;
using SSRSImportExportWizard.Models;
using SSRSImportExportWizard.ReportServer2010;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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
            Cursor.Current = Cursors.WaitCursor;
            this.LoadReportDataSource();
            Cursor.Current = Cursors.Default;
        }

        public ReportingService2010 ReportServer { get; set; }

        public List<TreeNode> Reports { get; set; }

        public string RootPath { get; set; }

        private void LoadReportDataSource()
        {
            List<DataSourceItemModel> dataItems = new List<DataSourceItemModel>();
            List<ConnectionModel> connectionStrings = new List<ConnectionModel>();
            List<ConnectionModel> replacementStrings = null;
            string path = string.Empty;

            try
            {
                path = (string.Format(@"/{0}", this.RootPath)).Replace("//", "/");
                CatalogItem[] items = this.ReportServer.ListChildren(path, true);
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
                            if (def != null && !string.IsNullOrEmpty(def.ConnectString) && !string.IsNullOrEmpty(def.ConnectString.Trim()))
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
                    else if (item.TypeName == "DataSource")
                    {
                        DataSourceDefinition def = this.ReportServer.GetDataSourceContents(item.Path);
                            if (def != null && !string.IsNullOrEmpty(def.ConnectString) && !string.IsNullOrEmpty(def.ConnectString.Trim()))
                            {
                                dataItems.Add(
                                new DataSourceItemModel()
                                {
                                    ConnectionString = def.ConnectString,
                                    ReportFullName = item.Path,
                                    Extension = def.Extension
                                });
                            }
                    }
                }

                 DataSourceGridView.DataSource = dataItems.OrderBy(o => o.ReportFullName).ToList();

                List<DataSourceItemModel> connections = dataItems.GroupBy(test => test.ConnectionString)
                       .Select(grp => grp.First())
                       .ToList();

                foreach (DataSourceItemModel ds in connections)
                {
                    if (!string.IsNullOrEmpty(ds.ConnectionString) && !string.IsNullOrEmpty(ds.ConnectionString.Trim()))
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

                connectionStrings = new List<ConnectionModel>();
                foreach (DataSourceItemModel ds in connections)
                {
                    if (!string.IsNullOrEmpty(ds.ConnectionString) && !string.IsNullOrEmpty(ds.ConnectionString.Trim()) &&  ds.Extension != null && ds.Extension.ToLower().Equals("xml"))
                    {
                        try
                        {
                            Uri myUri = new Uri(ds.ConnectionString);
                            string host = myUri.Host;
                            if (connectionStrings.FindIndex(x => x.CurrentValue == host) == -1)
                            {
                                connectionStrings.Add(new ConnectionModel()
                                {
                                    CurrentValue = host,
                                    NewValue = string.Empty
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            LoggerManager.Logger.LogFatal("Invalid URI in the connectionstring", ex);
                        }
                    }
                }

                ReplaceStringDGV.DataSource = connectionStrings.OrderBy(o => o.CurrentValue).ToList();
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogFatal("Error while loading Report DataSources", ex);
                MessageBox.Show("Error while loading Report DataSources");
            }
        }

        private void btnReplaceDataSource_Click(object sender, EventArgs e)
        {
            List<string> errors = new List<string>();
            bool validDataSource = false;
            Cursor.Current = Cursors.WaitCursor;
            btnReplaceDataSource.Enabled = false;
            int updatedDSCount = 0;
            string path = string.Empty;

            Dictionary<string, string> replaceStrings = new Dictionary<string, string>();
            List<string> replaceCurrentValues = new List<string>();
            try
            {
                foreach (DataGridViewRow row in ReplaceStringDGV.Rows)
                {
                    if (!string.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                    {
                        replaceStrings.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                        replaceCurrentValues.Add(row.Cells[0].Value.ToString());
                    }
                }

                if (replaceStrings.Count > 0)
                {
                    path = (string.Format(@"/{0}", this.RootPath)).Replace("//", "/");
                    CatalogItem[] items = this.ReportServer.ListChildren(path, true);

                    foreach (CatalogItem item in items)
                    {
                        if (item.TypeName == "Report")
                        {
                            validDataSource = false;
                            DataSource[] dataSources = this.ReportServer.GetItemDataSources(item.Path);
                            List<string> currentConnectionStrings = new List<string>();

                        foreach (DataSource data in dataSources)
                        {
                            if (data.Item.ToString().Contains("InvalidDataSourceReference"))
                            {
                                validDataSource = false;
                                errors.Add(item.Path + " is referring to the data source, " + dataSources[0].Name.ToString() + " that is either invalid or does not exist.");
                                LoggerManager.Logger.LogWarning(item.Path + " is referring to the data source, " + dataSources[0].Name.ToString() + " that is either invalid or does not exist.");
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
                                        
                                            foreach (var connectionstring in currentConnectionStrings)
                                            {
                                                bool hasUpdated = false;
                                                if (!string.IsNullOrEmpty(connectionstring))
                                                {
                                                    replaceCurrentValues.ForEach(value =>
                                                    {
                                                        if (connectionstring.Contains(value))
                                                        {
                                                            hasUpdated = true;
                                                        }
                                                    });
                                                }
                                                if (hasUpdated)
                                                {
                                                    updatedDSCount++;
                                                    lblUpdateProgress.Text = "DataSource " + item.Path + " updated successfully";
                                                    lblUpdateProgress.Refresh();
                                                }
                                            }
                                        }
                                    catch (Exception ex)
                                    {
                                        LoggerManager.Logger.LogWarning("FATAL ERROR: " + item.Path, ex);
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
                                        bool hasUpdated = false;
                                        this.ReportServer.SetDataSourceContents(item.Path, def);
                                        if (!string.IsNullOrEmpty(def.ConnectString))
                                        {
                                            replaceCurrentValues.ForEach(value =>
                                            {
                                                if (def.ConnectString.Contains(value))
                                                {
                                                    hasUpdated = true;
                                                }
                                            });
                                        }
                                        if (hasUpdated)
                                        {
                                            updatedDSCount++;
                                            lblUpdateProgress.Text = "Shared DataSource " + item.Path + " updated successfully";
                                            lblUpdateProgress.Refresh();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LoggerManager.Logger.LogWarning("FATAL ERROR: " + item.Path, ex);
                                        errors.Add("FATAL ERROR" + item.Path + Environment.NewLine + ex.Message + Environment.NewLine);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogFatal("Error while updating Report DataSources", ex);
                MessageBox.Show("Error while updating Report DataSources");
            }

            Cursor.Current = Cursors.Default;
            btnReplaceDataSource.Enabled = true;

            if (errors.Count > 0)
            {
                File.WriteAllLines(@"C:\UA3\Reports\ErroredReports.txt", errors.ToArray());
                lblUpdateProgress.Text = @"DataSource updated with error. Please see error log in C:\UA3\Reports folder";
                lblUpdateProgress.Refresh();
            }
            else if (replaceStrings.Count > 0)
            {
                LoadReportDataSource();
                lblUpdateProgress.Text = updatedDSCount + @" DataSources updated successfully";
                lblUpdateProgress.Refresh();
            }
            else if (replaceStrings.Count == 0)
            {
                lblUpdateProgress.Text = updatedDSCount + @" DataSources updated successfully";
                lblUpdateProgress.Refresh();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}