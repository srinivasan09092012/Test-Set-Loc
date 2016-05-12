//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Utilities.LoadTenantDb.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    public partial class ConfirmationLocalizationDatalistDialog : Form
    {
        public ConfirmationLocalizationDatalistDialog()
        {
            this.InitializeComponent();
        }

        public MainForm MainForm { get; set; }

        private void CancelPushButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfirmationLocalizationDatalistsLoad(object sender, EventArgs e)
        {
            this.RefreshGrid();

            if (this.MainForm.LocalizationDatalists.Count > 0)
            {
                this.loadPushButtonAll.Enabled = true;
            }
            else
            {
                this.loadPushButtonAll.Enabled = false;
            }
        }

        private void RefreshGrid()
        {
            List<string> row = new List<string>();

            this.datalistsGridView.Rows.Clear();
            this.datalistsGridView.Refresh();

            for (int i = 0; i < this.MainForm.LocalizationDatalists.Count; i++)
            {
                for (int j = 0; j < this.MainForm.LocalizationDatalists[i].Datalists.Count; j++)
                {
                    for (int k = 0; k < this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems.Count; k++)
                    {
                        row.Clear();
                        row.Add(this.MainForm.LocalizationDatalists[i].Datalists[j].Action);
                        row.Add(this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems[k].Action);
                        row.Add(this.MainForm.LocalizationDatalists[i].Name);
                        row.Add(this.MainForm.LocalizationDatalists[i].Module.Name);
                        row.Add(this.MainForm.LocalizationDatalists[i].Datalists[j].Name);
                        row.Add(this.MainForm.LocalizationDatalists[i].Datalists[j].ContentId);
                        row.Add(this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems[k].Text);
                        row.Add(this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems[k].ContentId);
                        row.Add(this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems[k].Order);
                        this.datalistsGridView.Rows.Add(row.ToArray());
                    }
                }
            }
        }

        private void LoadAllLocalizationDatalists(object sender, EventArgs e)
        {
            this.LoadGrid();
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

        private void LoadGrid()
        {
            Cursor.Current = Cursors.WaitCursor;

            int currentRow = 0;
            int loadDatalistSuccessful = 0;
            int loadDatalistItemSuccessful = 0;
            int loadErrors = 0;

            for (int i = 0; i < this.MainForm.LocalizationDatalists.Count; i++)
            {
                for (int j = 0; j < this.MainForm.LocalizationDatalists[i].Datalists.Count; j++)
                {
                    string tenantModuleId = this.DetermineTenantModuleId(this.MainForm.LocalizationDatalists[i].Module.Name);

                    if (tenantModuleId != null)
                    {
                        Datalist datalist = new Datalist();
                        datalist.MainForm = this.MainForm;

                        datalist.ContentId = this.MainForm.LocalizationDatalists[i].Datalists[j].ContentId;
                        datalist.Id = datalist.GetDataList(datalist);
                        datalist.TenantId = this.MainForm.TenantId;
                        datalist.Name = this.MainForm.LocalizationDatalists[i].Datalists[j].Name;
                        datalist.TenantModuleId = tenantModuleId;
                        datalist.Description = this.MainForm.LocalizationDatalists[i].Datalists[j].Name;
                        datalist.IdentifierId = "USER1";
                        datalist.IsActive = true;

                        if (datalist.Id == null)
                        {
                            try
                            {
                                datalist.Id = this.MainForm.LocalizationDatalists[i].Datalists[j].Id;
                                datalist = datalist.AddDataList(datalist);
                            }
                            catch
                            {
                                datalist = null;
                            }

                            if (datalist != null)
                            {
                                this.MainForm.LocalizationDatalists[i].Datalists[j].Action = "Added";
                                loadDatalistSuccessful++;
                            }
                            else
                            {
                                this.MainForm.LocalizationDatalists[i].Datalists[j].Action = "Add Error";
                                loadErrors++;
                            }
                        }
                        else
                        {
                            bool rowUpdated;

                            try
                            {
                                rowUpdated = datalist.UpdateDataList(datalist);
                            }
                            catch
                            {
                                rowUpdated = false;
                            }

                            if (rowUpdated)
                            {
                                this.MainForm.LocalizationDatalists[i].Datalists[j].Action = "Updated";
                                loadDatalistSuccessful++;
                            }
                            else
                            {
                                this.MainForm.LocalizationDatalists[i].Datalists[j].Action = "Update Error";
                                loadErrors++;
                            }
                        }

                        this.datalistsGridView.Rows[currentRow].Cells[0].Value = this.MainForm.LocalizationDatalists[i].Datalists[j].Action;
                        this.datalistsGridView.Refresh();

                        for (int k = 0; k < this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems.Count; k++)
                        {
                            if (datalist != null)
                            {
                                DatalistItem datalistItem = new DatalistItem();
                                datalistItem.MainForm = this.MainForm;
                                datalistItem.DataListItemLanguages.Add(new DatalistItemLanguage());

                                datalistItem.ContentId = this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems[k].ContentId;
                                datalistItem.Key = this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems[k].Value;
                                datalistItem.Id = datalistItem.GetDataListItem(datalist, datalistItem);
                                datalistItem.DataListId = datalist.Id;
                                datalistItem.TenantId = this.MainForm.TenantId;
                                datalistItem.IdentifierId = "USER1";
                                datalistItem.IsActive = true;
                                datalistItem.OrderIndex = int.Parse(this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems[k].Order);

                                if (datalistItem.Id != null)
                                {
                                    bool newLanguage = true;

                                    for (int l = 0; l < datalistItem.DataListItemLanguages.Count; l++)
                                    {
                                        if (datalistItem.DataListItemLanguages[l].Locale == this.MainForm.LocalizationDatalists[i].LocaleId.ToLower())
                                        {
                                            newLanguage = false;
                                            datalistItem.DataListItemLanguages[l].DataListItemId = datalistItem.Id;
                                            datalistItem.DataListItemLanguages[l].Locale = this.MainForm.LocalizationDatalists[i].LocaleId.ToLower();
                                            datalistItem.DataListItemLanguages[l].Description = this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems[k].Text;
                                            datalistItem.DataListItemLanguages[l].IsActive = true;
                                        }
                                    }

                                    if (newLanguage)
                                    {
                                        int numLangs = datalistItem.DataListItemLanguages.Count;
                                        datalistItem.DataListItemLanguages.Add(new DatalistItemLanguage());
                                        datalistItem.DataListItemLanguages[numLangs].DataListItemId = null;
                                        datalistItem.DataListItemLanguages[numLangs].Locale = this.MainForm.LocalizationDatalists[i].LocaleId.ToLower();
                                        datalistItem.DataListItemLanguages[numLangs].Description = this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems[k].Text;
                                        datalistItem.DataListItemLanguages[numLangs].IsActive = true;
                                    }
                                }
                                else
                                {
                                    datalistItem.DataListItemLanguages[0].DataListItemId = null;
                                    datalistItem.DataListItemLanguages[0].Locale = this.MainForm.LocalizationDatalists[i].LocaleId.ToLower();
                                    datalistItem.DataListItemLanguages[0].Description = this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems[k].Text;
                                    datalistItem.DataListItemLanguages[0].IsActive = true;
                                }

                                if (datalistItem.Id == null)
                                {
                                    try
                                    {
                                        datalistItem = datalistItem.AddDataListItem(datalistItem);
                                    }
                                    catch
                                    {
                                        datalistItem = null;
                                    }

                                    if (datalistItem != null)
                                    {
                                        this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems[k].Action = "Added";
                                        loadDatalistItemSuccessful++;
                                    }
                                    else
                                    {
                                        this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems[k].Action = "Add Error";
                                        loadErrors++;
                                    }
                                }
                                else
                                {
                                    bool rowUpdated;

                                    try
                                    {
                                        rowUpdated = datalistItem.UpdateDataListItem(datalistItem);
                                    }
                                    catch
                                    {
                                        rowUpdated = false;
                                    }

                                    if (rowUpdated)
                                    {
                                        this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems[k].Action = "Updated";
                                        loadDatalistItemSuccessful++;
                                    }
                                    else
                                    {
                                        this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems[k].Action = "Update Error";
                                        loadErrors++;
                                    }
                                }
                            }
                            else 
                            {
                                this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems[k].Action = "  ----";
                            }

                            this.datalistsGridView.Rows[currentRow].Cells[1].Value = this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems[k].Action;

                            if (string.IsNullOrEmpty((string)this.datalistsGridView.Rows[currentRow].Cells[0].Value))
                            {
                                this.datalistsGridView.Rows[currentRow].Cells[0].Value = "  ----";
                            }

                            if (this.datalistsGridView.Rows.Count > currentRow + 1)
                            {
                                this.datalistsGridView.CurrentCell = this.datalistsGridView.Rows[currentRow + 1].Cells[0];
                                this.datalistsGridView.Rows[currentRow + 1].Selected = true;
                            }

                            this.datalistsGridView.Refresh();
                            currentRow++;
                        }
                    }
                    else
                    {
                        this.datalistsGridView.Rows[currentRow].Cells[0].Value = "Missing TM ID";
                        this.datalistsGridView.Refresh();
                        loadErrors++;

                        for (int k = 0; k < this.MainForm.LocalizationDatalists[i].Datalists[j].DatalistItems.Count; k++)
                        {
                            this.datalistsGridView.Rows[currentRow].Cells[1].Value = "  ----";

                            if (this.datalistsGridView.Rows.Count > currentRow + 1)
                            {
                                this.datalistsGridView.CurrentCell = this.datalistsGridView.Rows[currentRow + 1].Cells[0];
                                this.datalistsGridView.Rows[currentRow + 1].Selected = true;
                            }

                            this.datalistsGridView.Refresh();
                            currentRow++;
                        }
                    }
                }
            }

            Cursor.Current = Cursors.Default;
            MessageBox.Show("Tenant Configuration load complete. " + loadDatalistSuccessful + " DataLists loaded, " + loadDatalistItemSuccessful + " DataList Items loaded, and " + loadErrors + " errors reported.", "Tenant Load Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
