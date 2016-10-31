//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Administration.UX.Common;
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
    public partial class Confirmation_AppSettings : Form
    {
        public Confirmation_AppSettings()
        {
            InitializeComponent();
        }
        public MainForm MainForm { get; set; }

        private void Confirmation_AppSettings_Load(object sender, EventArgs e)
        {
          this.RefreshGrid();

            if (MainForm.AppSettings.Count > 0)
            {
                this.loadPushButton.Enabled = true;
            }
            else
            {
                this.loadPushButton.Enabled = false;
            }
        }

        private void RefreshGrid()
        {
            List<string> row = new List<string>();

            for (int i = 0; i < MainForm.AppSettings.Count; i++)
            {
                ////if (string.IsNullOrEmpty(MainForm.AppSettings[i].Id))
                ////{
                ////    MainForm.AppSettings[i].Id = "<< NULL OR MISSING VALUE >>";
                ////}

                row.Clear();
                row.Add(MainForm.AppSettings[i].Action);
                row.Add(MainForm.AppSettings[i].Module.Name);
                ////row.Add(MainForm.AppSettings[i].Id);
                row.Add(MainForm.AppSettings[i].Key);
                row.Add(MainForm.AppSettings[i].Value);

                this.appSettingsGridView.Rows.Add(row.ToArray());
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            int loadAppSettingSuccessful = 0;
            int loadErrors = 0;

            Cursor.Current = Cursors.WaitCursor;

            this.LoadAppSettings(ref loadAppSettingSuccessful, ref loadErrors);
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Tenant Configuration load complete. " + loadAppSettingSuccessful + " App Settings loaded and " + loadErrors + " errors reported.", "Tenant Load Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void LoadAppSettings(ref int loadAppSettingSuccessful, ref int loadErrors)
        {
            string appId = this.DetermineAppId();
            string tenantModuleId = string.Empty;
            int currentRow = 0;

            for (int i = 0; i < MainForm.AppSettings.Count; i++)
            {
                HP.HSP.UA3.Utilities.LoadTenantDb.Data.AppSetting item = null;
                //if (i == 0)
                //{
                //    tenantModuleId = this.DetermineTenantModuleId(this.MainForm.AppSettings[i].Module.Name);
                //    if (string.IsNullOrEmpty(tenantModuleId))
                //    {
                //        break;
                //    }
                //}

                item = new Data.AppSetting();
                item.MainForm = this.MainForm;
                item.Id = this.MainForm.AppSettings[i].Id;
                item.ApplicationId = appId;
                item.AppSettingKey = this.MainForm.AppSettings[i].Key;
                item.Description = string.Empty;
                item.IsActive = true;
                item.SettingTypeItemKey = "UX";
                item.Value = this.MainForm.AppSettings[i].Value;
                item.TenantModuleId = this.DetermineTenantModuleId(this.MainForm.AppSettings[i].Module.Name); ;

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
                        this.MainForm.AppSettings[i].Action = "Updated";
                        loadAppSettingSuccessful++;
                    }
                    else
                    {
                        this.MainForm.AppSettings[i].Action = "Update Error";
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
                        this.MainForm.AppSettings[i].Action = "Added";
                        loadAppSettingSuccessful++;
                    }
                    else
                    {
                        this.MainForm.AppSettings[i].Action = "Add Error";
                        loadErrors++;
                    }
                }

                this.appSettingsGridView.Rows[currentRow].Cells[0].Value = this.MainForm.AppSettings[i].Action;

                if (this.appSettingsGridView.Rows.Count > currentRow + 1)
                {
                    this.appSettingsGridView.CurrentCell = this.appSettingsGridView.Rows[currentRow + 1].Cells[0];
                    this.appSettingsGridView.Rows[currentRow + 1].Selected = true;
                }

                this.appSettingsGridView.Refresh();
                currentRow++;
            }

            if (loadAppSettingSuccessful > 0)
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

                default:
                    return null;
            }
        }

        private bool SettingExists(Data.AppSetting item)
        {
            bool exists = false;
            int retryCount = 0;
            string objDataQuery = string.Format("AppSetting?$filter=ApplicationId%20eq%20{0}%20and%20TenantModuleID%20eq%20{1}%20and%20AppSettingKey%20eq%20%27{2}%27%20", item.ApplicationId, item.TenantModuleId, item.AppSettingKey);
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
