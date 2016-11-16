//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Administration.UX.Common;
using HP.HSP.UA3.Utilities.LoadTenantDb.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    public partial class Confirmation_IocSettings : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Confirmation_IocSettings()
        {
            InitializeComponent();
        }

        public MainForm MainForm { get; set; }

        private void Confirmation_IocSettings_Load(object sender, EventArgs e)
        {
            this.RefreshGrid();

            if (MainForm.IocSettings.Count > 0)
            {
                this.loadButton.Enabled = true;
            }
            else
            {
                this.loadButton.Enabled = false;
            }
        }

        private void RefreshGrid()
        {
            List<string> row = new List<string>();

            for (int i = 0; i < MainForm.IocSettings.Count; i++)
            {
                row.Clear();
                row.Add(MainForm.IocSettings[i].Action);
                row.Add(MainForm.IocSettings[i].Module.Name);
                row.Add(MainForm.IocSettings[i].Value);

                this.iocSettingdataGridView.Rows.Add(row.ToArray());
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            int loadIocSettingSuccessful = 0;
            int loadErrors = 0;

            Cursor.Current = Cursors.WaitCursor;

            this.LoadIocSettings(ref loadIocSettingSuccessful, ref loadErrors);
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Tenant Configuration load complete. " + loadIocSettingSuccessful + " IOC Configuration loaded and " + loadErrors + " errors reported.", "Tenant Load Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadIocSettings(ref int loadIocSettingSuccessful, ref int loadErrors)
        {
            string appId = this.DetermineAppId();
            string tenantModuleId = string.Empty;
            int currentRow = 0;
            IocConfiguration item = null;

            for (int i = 0; i < MainForm.IocSettings.Count; i++)
            {
                item = new Data.IocConfiguration();
                item.MainForm = this.MainForm;
                item.Id = this.MainForm.IocSettings[i].Id;
                item.ApplicationId = appId;
                item.Description = string.Empty;
                item.IsActive = true;
                item.Value = this.MainForm.IocSettings[i].Value;
                item.TenantModuleId = ConfigurationManager.AppSettings[this.MainForm.IocSettings[i].Module.Name + "TenantModuleId"];

                if (SettingExists(item))
                {
                    try
                    {
                        item = item.UpdateAppSetting(item);
                    }
                    catch
                    {
                        item = null;
                    }

                    if (item != null)
                    {
                        this.MainForm.IocSettings[i].Action = "Updated";
                        loadIocSettingSuccessful++;
                    }
                    else
                    {
                        this.MainForm.IocSettings[i].Action = "Update Error";
                        log.Error("Error Confirmation_IocSettings.LoadIocSettings Update Error " +
                            "Id=" + item.Id);
                        loadErrors++;
                    }
                }
                else
                {

                    try
                    {
                        item = item.AddAppSetting(item);
                    }
                    catch
                    {
                        item = null;
                    }

                    if (item != null)
                    {
                        this.MainForm.IocSettings[i].Action = "Added";
                        loadIocSettingSuccessful++;
                    }
                    else
                    {
                        this.MainForm.IocSettings[i].Action = "Add Error";
                        log.Error("Error Confirmation_IocSettings.LoadIocSettings Add Error " +
                            "Id=" + item.Id);
                        loadErrors++;
                    }
                }

                this.iocSettingdataGridView.Rows[currentRow].Cells[0].Value = this.MainForm.IocSettings[i].Action;

                if (this.iocSettingdataGridView.Rows.Count > currentRow + 1)
                {
                    this.iocSettingdataGridView.CurrentCell = this.iocSettingdataGridView.Rows[currentRow + 1].Cells[0];
                    this.iocSettingdataGridView.Rows[currentRow + 1].Selected = true;
                }

                this.iocSettingdataGridView.Refresh();
                currentRow++;
            }

            if (loadIocSettingSuccessful > 0)
            {
                this.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheAppSettingTableKey);
            }
        }

        private string DetermineAppId()
        {
            string appId = string.Empty;
            appId = ConfigurationManager.AppSettings["UXApplicationId"];

            return appId;
        }

        private string DetermineTenantModuleId(string module)
        {
            switch (module)
            {
                case "Core":
                    return ConfigurationManager.AppSettings["CoreTenantModuleId"];

                case "Administration":
                    return ConfigurationManager.AppSettings["AdministrationTenantModuleId"];

                case "EmployeeMgmt":
                    return ConfigurationManager.AppSettings["EmployeeManagementTenantModuleId"];

                case "ProviderCredentialing":
                    return ConfigurationManager.AppSettings["ProviderCredentialingTenantModuleId"];

                case "ProviderEnrollment":
                    return ConfigurationManager.AppSettings["ProviderEnrollmentTenantModuleId"];

                case "PlanManagement":
                    return ConfigurationManager.AppSettings["PlanManagementTenantModuleId"];

                case "CorrespondenceManagement":
                    return ConfigurationManager.AppSettings["CorrespondenceManagementTenantModuleId"];

                case "ProviderManagement":
                    return ConfigurationManager.AppSettings["ProviderManagementTenantModuleId"];

                case "ProviderPortal":
                    return ConfigurationManager.AppSettings["ProviderPortalTenantModuleId"];

                case "MemberPortal":
                    return ConfigurationManager.AppSettings["MemberPortalTenantModuleId"];

                case "ProgramIntegrity":
                    return ConfigurationManager.AppSettings["ProgramIntegrityTenantModuleId"];

                case "TPLPolicy":
                    return ConfigurationManager.AppSettings["TPLPolicyTenantModuleId"];

                case "MemberManagement":
                    return ConfigurationManager.AppSettings["MemberManagementTenantModuleId"];

                default:
                    return null;
            }
        }

        private bool SettingExists(IocConfiguration item)
        {
            bool exists = false;
            int retryCount = 0;
            string objDataQuery = string.Format("AppSetting?$filter=ApplicationId%20eq%20{0}%20and%20TenantModuleID%20eq%20{1}%20and%20AppSettingKey%20eq%20%27{2}%27%20", item.ApplicationId, item.TenantModuleId, AdministrationConstants.IocConfig);
            string baseUrl = MainForm.ODataEndpointAddress;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(objDataQuery).Result;
            while (!response.IsSuccessStatusCode && retryCount < 9999)
            {
                response = client.GetAsync(objDataQuery).Result;
                retryCount++;
            }

            if (response.IsSuccessStatusCode)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Dictionary<string, object> parameters = serializer.Deserialize<Dictionary<string, object>>(response.Content.ReadAsStringAsync().Result);

                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    if (parameter.Value is IList)
                    {
                        IList lists = (IList)parameter.Value;

                        foreach (Dictionary<string, object> list in lists)
                        {
                            item.Id = (string)list["TenantModuleAppSettingId"];
                            break;
                        }
                    }
                }
            }

            exists = !string.IsNullOrEmpty(item.Id);

            return exists;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RefreshCache(string cacheKey, bool reloadCache = true, string cacheType = "AppSetting")
        {
            string objDataQuery =
                string.Format("CacheRefreshTable(CacheKey='{0}',ReloadCache={1},CacheType='{2}')", cacheKey, reloadCache.ToString().ToLower(), cacheType);
            string baseUrl = MainForm.ODataEndpointAddress;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(objDataQuery).Result;
        }
    }
}
