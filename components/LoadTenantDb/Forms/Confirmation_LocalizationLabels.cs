using HP.HSP.UA3.Utilities.LoadTenantDb.Data;
using HP.HSP.UA3.Administration.BAS.DataLists.DataAccess.Entities;
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
    public partial class Confirmation_LocalizationLabels : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private int loadDatalistsSuccessful = 0;
        private int loadDatalistItemSuccessful = 0;
        private int loadDatalistErrors = 0;
        private int loadDatalistItemErrors = 0;
        private bool updated = true;
        private List<DataListsItems> messageTypeItems = new List<DataListsItems>();

        public Confirmation_LocalizationLabels()
        {
            InitializeComponent();
        }

        public MainForm MainForm { get; set; }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadAllLocalizationMessages(object sender, EventArgs e)
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

                case "MemberManagement":
                    return ConfigurationManager.AppSettings["MemberManagementTenantModuleId"];

                case "NetworkManagement":
                    return ConfigurationManager.AppSettings["NetworkManagementTenantModuleId"];

                case "TPLBillings":
                    return ConfigurationManager.AppSettings["TPLBillingsTenantModuleId"];

                case "TPLCaseTracking":
                    return ConfigurationManager.AppSettings["TPLCaseTrackingTenantModuleId"];

                case "TPLHIPP":
                    return ConfigurationManager.AppSettings["TPLHIPPTenantModuleId"];

                case "TPLPolicy":
                    return ConfigurationManager.AppSettings["TPLPolicyTenantModuleId"];

                default:
                    return null;
            }
        }

        private void ConfirmationLocalizationLabelsLoad(object sender, EventArgs e)
        {
            this.RefreshGrid();

            if (this.MainForm.LocalizationLabels.Count > 0)
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

            this.labelsGridView.Rows.Clear();
            this.labelsGridView.Refresh();

            for (int i = 0; i < this.MainForm.LocalizationLabels.Count; i++)
            {
                for (int j = 0; j < this.MainForm.LocalizationLabels[i].Labels.Count; j++)
                {
                    row.Clear();
                    row.Add(this.MainForm.LocalizationLabels[i].Labels[j].Action);
                    row.Add(this.MainForm.LocalizationLabels[i].Labels[j].ContentId);
                    row.Add(this.MainForm.LocalizationLabels[i].LocaleId.ToLower());
                    row.Add(this.MainForm.LocalizationLabels[i].Module.Name);
                    row.Add(this.MainForm.LocalizationLabels[i].Labels[j].Tooltip);
                    row.Add(this.MainForm.LocalizationLabels[i].Labels[j].Text);
                    this.labelsGridView.Rows.Add(row.ToArray());
                }
            }
            totalLabel.Text = "TOTAL: " + this.labelsGridView.RowCount.ToString();
        }

        private Datalist LoadDataList(String name, String description, String contentId)
        {
            // Build the Datalist object.
            Datalist datalist = new Datalist();
            datalist.MainForm = this.MainForm;
            datalist.TenantId = this.MainForm.TenantId;
            string tenantModuleId = this.DetermineTenantModuleId(this.MainForm.LocalizationLabels[0].Module.Name);
            datalist.TenantModuleId = tenantModuleId;
            datalist.Name = name;
            datalist.Description = description;
            datalist.IdentifierId = "USER1";
            datalist.IsActive = true;
            datalist.ContentId = contentId;
            // Read the database to see if the Datalist already exists and set the Id.
            datalist.Id = datalist.GetDataListId(datalist);

            if (datalist.Id == null)
            {
                datalist = datalist.AddDataList(datalist);
            }
            else
            {
                updated = datalist.UpdateDataList(datalist);
            }

            return datalist;
        }

        private DatalistItem CreateDatalistItem(Datalist datalist, String contentId)
        {
            // Build the DatalistItem object.
            DatalistItem datalistItem = new DatalistItem();
            datalistItem.MainForm = this.MainForm;
            datalistItem.Key = contentId;
            datalistItem.DataListId = datalist.Id;
            datalistItem.TenantId = this.MainForm.TenantId;
            datalistItem.IdentifierId = "USER1";
            datalistItem.IsActive = true;
            // Query the database to see if the item already exists. If it exists, the Id is set to the GUID. Otherwise, it will be null.
            datalistItem.Id = datalistItem.GetDataListItemId(datalist, datalistItem);
            return datalistItem;
        }

        private string GetMessageTypeItemId(String type)
        {
            string itemId = null;
            foreach (var DataListsItems in messageTypeItems) 
            {
                if (DataListsItems.DataListsItemKey.Contains(type))
                {
                    itemId = DataListsItems.DataListsItemId.ToString();
                    break;
                }
            }

            return itemId;
        }

        private void CreateAttributeValues(Datalist datalist, DatalistItem datalistItem, String typeAttribute)
        {
            // If the datalist item doesn't exist on the database yet, add the attribute value to it.
            if (datalistItem.Id == null)
            {
                datalistItem.DataListItemAttributeValues.Add(new DatalistItemAttributeValue());
                datalistItem.DataListItemAttributeValues[0].DataListsAttributeValueId = null;
                datalistItem.DataListItemAttributeValues[0].DataListAttributeId = Datalist.GetAttributeId(datalist);  // GUID OF ATTRIBUTE ID column on ATTRIBUTES table.
                datalistItem.DataListItemAttributeValues[0].DataListsItemId = null;
                datalistItem.DataListItemAttributeValues[0].DataListsItemValueId = GetMessageTypeItemId(typeAttribute);
            }
        }

        private void CreateDatalistItemLanguages(DatalistItem datalistItem, String locale, String text, String tooltip)
        {
            // If the datalist item doesn't exist on the database yet, add the language to it.
            if (datalistItem.Id == null)
            {
                datalistItem.DataListItemLanguages.Add(new DatalistItemLanguage());
                datalistItem.DataListItemLanguages[0].DataListItemId = null;
                datalistItem.DataListItemLanguages[0].Locale = locale;
                datalistItem.DataListItemLanguages[0].Description = text;
                datalistItem.DataListItemLanguages[0].LongDescription = tooltip;
                datalistItem.DataListItemLanguages[0].IsActive = true;
            }
            else
            {
                // The datalist item exists, so obtain any datalist item languages that already exist for this datalist item on the database.
                List<Object> existingLanguages = datalistItem.GetDataListItemLanguages(datalistItem.Id);
                // Assume the language being passed in is not already associated with the item.
                bool newLanguage = true;

                // Process the languages already associated with this datalist item on the database.
                for (int l = 0; l < existingLanguages.Count; l++)
                {
                    // Compare the locale being processed to the one that exists on the database.
                    if (existingLanguages[l].ToString().Contains(locale))
                    {
                        newLanguage = false;
                        break;
                    }
                }

                // Language is not associated with the item, so add it to the item.
                if (newLanguage)
                {
                    datalistItem.DataListItemLanguages.Add(new DatalistItemLanguage());
                    datalistItem.DataListItemLanguages[0].DataListItemId = null;
                    datalistItem.DataListItemLanguages[0].Locale = locale;
                    datalistItem.DataListItemLanguages[0].Description = text;
                    datalistItem.DataListItemLanguages[0].LongDescription = tooltip;
                    datalistItem.DataListItemLanguages[0].IsActive = true;
                }
                else
                {
                    datalistItem.DataListItemLanguages.Add(new DatalistItemLanguage());
                    datalistItem.DataListItemLanguages[0].DataListItemId = datalistItem.Id;
                    datalistItem.DataListItemLanguages[0].Locale = locale;
                    datalistItem.DataListItemLanguages[0].Description = text;
                    datalistItem.DataListItemLanguages[0].LongDescription = tooltip;
                    datalistItem.DataListItemLanguages[0].IsActive = true;
                }
            }
        }

        private bool UpdateDataListItem(DatalistItem datalistItem)
        {
            // Update the DatalistItem
            bool rowUpdated;

            try
            {
                rowUpdated = datalistItem.UpdateDataListItem(datalistItem);
            }
            catch
            {
                rowUpdated = false;
            }

            return rowUpdated;
        }

        private void LoadGrid()
        {
            Cursor.Current = Cursors.WaitCursor;

            int currentRow = 0;
            bool added = false;

            // Load the module specific Messages DataList. (i.e. Module.Msg)
            Datalist datalist = LoadDataList(this.MainForm.LocalizationLabels[0].Module.Name + " Labels",
                this.MainForm.LocalizationLabels[0].Module.Name + " Labels",
                this.MainForm.LocalizationLabels[0].Module.Name + ".Label");

            // Process each locale (i.e. English, Spanish, etc.)
            for (int i = 0; i < this.MainForm.LocalizationLabels.Count; i++)
            {
                // Process each message for a given locale.
                for (int j = 0; j < this.MainForm.LocalizationLabels[i].Labels.Count; j++)
                {
                    this.labelsGridView.Rows[currentRow].Cells[0].Value = this.MainForm.LocalizationLabels[i].Labels[j].Action;
                    this.labelsGridView.Refresh();

                    if (datalist != null)
                    {
                        // Build the DatalistItem for each Message.

                        DatalistItem datalistItem = CreateDatalistItem(datalist, this.MainForm.LocalizationLabels[i].Labels[j].ContentId);
                        // Process the DatalistItemLanguages.
                        CreateDatalistItemLanguages(datalistItem, this.MainForm.LocalizationLabels[i].LocaleId.ToLower(), this.MainForm.LocalizationLabels[i].Labels[j].Text, this.MainForm.LocalizationLabels[i].Labels[j].Tooltip);

                        // Set the datalist id for the datalist item.
                        datalistItem.DataListId = datalist.Id;

                        // Determine if the DataList Item should be added or updated.
                        if (datalistItem.Id == null)
                        {
                            // Add the DatalistItem
                            added = datalistItem.AddDataListItem(datalistItem);

                            if (added)
                            {
                                this.MainForm.LocalizationLabels[i].Labels[j].Action = "Added";
                                loadDatalistItemSuccessful++;
                            }
                            else
                            {
                                this.MainForm.LocalizationLabels[i].Labels[j].Action = "Add Error ";
                                log.Error("Error Confirmation_LocalizationLabels.LoadGrid Add Error" + 
                                    "ContentId=" + datalistItem.ContentId);
                                loadDatalistItemErrors++;
                            }
                        }
                        else
                        {
                            // Update the DatalistItem
                            if (UpdateDataListItem(datalistItem))
                            {
                                this.MainForm.LocalizationLabels[i].Labels[j].Action = "Updated";
                                loadDatalistItemSuccessful++;
                            }
                            else
                            {
                                this.MainForm.LocalizationLabels[i].Labels[j].Action = "Update Error";
                                log.Error("Error Confirmation_LocalizationLabels.LoadGrid Update Error " +
                                    "ContentId=" + datalistItem.ContentId);
                                loadDatalistItemErrors++;
                            }

                        }
                    }
                    else
                    {
                        this.MainForm.LocalizationLabels[i].Labels[j].Action = "  ----";
                    }

                    this.labelsGridView.Rows[currentRow].Cells[0].Value = this.MainForm.LocalizationLabels[i].Labels[j].Action;

                    if (string.IsNullOrEmpty((string)this.labelsGridView.Rows[currentRow].Cells[0].Value))
                    {
                        this.labelsGridView.Rows[currentRow].Cells[0].Value = "  ----";
                    }

                    if (this.labelsGridView.Rows.Count > currentRow + 1)
                    {
                        this.labelsGridView.CurrentCell = this.labelsGridView.Rows[currentRow + 1].Cells[0];
                        this.labelsGridView.Rows[currentRow + 1].Selected = true;
                    }

                    this.labelsGridView.Refresh();
                    currentRow++;
                }
            }

            Cursor.Current = Cursors.Default;
            log.Info("Tenant Configuration Labels load complete. " + "\n" +
                loadDatalistsSuccessful + " DataLists Loaded, " + "\n" +
                loadDatalistItemSuccessful + " DataList Items loaded, " + "\n" +
                loadDatalistErrors + " DataList errors reported, and " + "\n" +
                loadDatalistItemErrors + " DataList item errors reported. ");

            MessageBox.Show("Tenant Configuration Labels load complete. " + "\n" +
                loadDatalistsSuccessful + " DataLists Loaded, " + "\n" +
                loadDatalistItemSuccessful + " DataList Items loaded, " + "\n" +
                loadDatalistErrors + " DataList errors reported, and " + "\n" +
                loadDatalistItemErrors + " DataList item errors reported. ",
                "Tenant Load Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}