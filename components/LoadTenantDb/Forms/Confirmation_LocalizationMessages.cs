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
    public partial class Confirmation_LocalizationMessages : Form
    {
        private int loadDatalistsSuccessful = 0;
        private int loadDatalistItemSuccessful = 0;
        private int loadDatalistErrors = 0;
        private int loadDatalistItemErrors = 0;
        private bool updated = true;
        private List<DataListsItems> messageTypeItems = new List<DataListsItems>();

        public Confirmation_LocalizationMessages()
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

                default:
                    return null;
            }
        }

        private void ConfirmationLocalizationMessagesLoad(object sender, EventArgs e)
        {
            this.RefreshGrid();

            if (this.MainForm.LocalizationMessages.Count > 0)
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

            this.messagesGridView.Rows.Clear();
            this.messagesGridView.Refresh();

            for (int i = 0; i < this.MainForm.LocalizationMessages.Count; i++)
            {
                for (int j = 0; j < this.MainForm.LocalizationMessages[i].Messages.Count; j++)
                {
                    row.Clear();
                    row.Add(this.MainForm.LocalizationMessages[i].Messages[j].Action);
                    row.Add(this.MainForm.LocalizationMessages[i].Messages[j].ContentId);
                    row.Add(this.MainForm.LocalizationMessages[i].LocaleId.ToLower());
                    row.Add(this.MainForm.LocalizationMessages[i].Module.Name);
                    row.Add(this.MainForm.LocalizationMessages[i].Messages[j].Type);
                    row.Add(this.MainForm.LocalizationMessages[i].Messages[j].Text);
                    this.messagesGridView.Rows.Add(row.ToArray());
                }
            }
        }

        private Datalist LoadDataList(String name, String description, String contentId)
        {
            // Build the Datalist object.
            Datalist datalist = new Datalist();
            datalist.MainForm = this.MainForm;
            datalist.TenantId = this.MainForm.TenantId;
            string tenantModuleId = this.DetermineTenantModuleId(this.MainForm.LocalizationMessages[0].Module.Name);
            datalist.TenantModuleId = tenantModuleId;
            datalist.Name = name;
            datalist.Description = description;
            datalist.IdentifierId = "USER1";
            datalist.IsActive = true;
            datalist.ContentId = contentId;
            // Read the database to see if the Datalist already exists and set the Id.
            datalist.Id = Datalist.GetDataListId(contentId);

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

        private void CreateDatalistItemLanguages(DatalistItem datalistItem, String locale, String text)
        {
            // If the datalist item doesn't exist on the database yet, add the language to it.
            if (datalistItem.Id == null)
            {
                datalistItem.DataListItemLanguages.Add(new DatalistItemLanguage());
                datalistItem.DataListItemLanguages[0].DataListItemId = null;
                datalistItem.DataListItemLanguages[0].Locale = locale;
                datalistItem.DataListItemLanguages[0].Description = text;
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
                    datalistItem.DataListItemLanguages[0].IsActive = true;
                }
                else
                {
                    datalistItem.DataListItemLanguages.Add(new DatalistItemLanguage());
                    datalistItem.DataListItemLanguages[0].DataListItemId = datalistItem.Id;
                    datalistItem.DataListItemLanguages[0].Locale = locale;
                    datalistItem.DataListItemLanguages[0].Description = text;
                    datalistItem.DataListItemLanguages[0].IsActive = true;
                }
            }
        }

        private DatalistItem AddDataListItem(DatalistItem datalistItem)
        {
            // Add the DatalistItem
            try
            {
                datalistItem = datalistItem.AddDataListItem(datalistItem);
            }
            catch
            {
                datalistItem = null;
            }

            return datalistItem;
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

            // Load the module specific Messages DataList. (i.e. Module.Msg)
            Datalist datalist = LoadDataList(this.MainForm.LocalizationMessages[0].Module.Name + " Messages",
                this.MainForm.LocalizationMessages[0].Module.Name + " Messages",
                this.MainForm.LocalizationMessages[0].Module.Name + ".Msg");

            // Load the Datalist for the Message Type Attributes Datalist (i.e. Core.Datalist.Attributes.MessageType).
            Datalist messageTypeDatalist = LoadDataList("Core DataList Attribute Message Type",
                "Core DataList Message Type",
                "Core.DataList.MessageType");

            messageTypeItems = DatalistItem.GetMessageTypeDatalistItems(messageTypeDatalist.Id);

            // Process each locale (i.e. English, Spanish, etc.)
            for (int i = 0; i < this.MainForm.LocalizationMessages.Count; i++)
            {
                // Process each message for a given locale.
                for (int j = 0; j < this.MainForm.LocalizationMessages[i].Messages.Count; j++)
                {
                    this.messagesGridView.Rows[currentRow].Cells[0].Value = this.MainForm.LocalizationMessages[i].Messages[j].Action;
                    this.messagesGridView.Refresh();

                    if (datalist != null)
                    {
                        // Build the DatalistItem for each Message.

                        DatalistItem datalistItem = CreateDatalistItem(datalist, this.MainForm.LocalizationMessages[i].Messages[j].ContentId);
                        // Process the DatalistItemLanguages.
                        CreateDatalistItemLanguages(datalistItem, this.MainForm.LocalizationMessages[i].LocaleId.ToLower(), this.MainForm.LocalizationMessages[i].Messages[j].Text);
                        // Process the attribute values.
                        CreateAttributeValues(datalist, datalistItem, this.MainForm.LocalizationMessages[i].Messages[j].Type);

                        // Set the datalist id for the datalist item.
                        datalistItem.DataListId = datalist.Id;

                        // Determine if the DataList Item should be added or updated.
                        if (datalistItem.Id == null)
                        {
                            // Add the DatalistItem
                            datalistItem = datalistItem.AddDataListItem(datalistItem);

                            if (datalistItem != null)
                            {
                                this.MainForm.LocalizationMessages[i].Messages[j].Action = "Added";
                                loadDatalistItemSuccessful++;
                            }
                            else
                            {
                                this.MainForm.LocalizationMessages[i].Messages[j].Action = "Add Error";
                                loadDatalistItemErrors++;
                            }
                        }
                        else
                        {
                            // Update the DatalistItem
                            if (UpdateDataListItem(datalistItem))
                            {
                                this.MainForm.LocalizationMessages[i].Messages[j].Action = "Updated";
                                loadDatalistItemSuccessful++;
                            }
                            else
                            {
                                this.MainForm.LocalizationMessages[i].Messages[j].Action = "Update Error";
                                loadDatalistItemErrors++;
                            }
                        }
                    }
                    else
                    {
                        this.MainForm.LocalizationMessages[i].Messages[j].Action = "  ----";
                    }

                    this.messagesGridView.Rows[currentRow].Cells[0].Value = this.MainForm.LocalizationMessages[i].Messages[j].Action;

                    if (string.IsNullOrEmpty((string)this.messagesGridView.Rows[currentRow].Cells[0].Value))
                    {
                        this.messagesGridView.Rows[currentRow].Cells[0].Value = "  ----";
                    }

                    if (this.messagesGridView.Rows.Count > currentRow + 1)
                    {
                        this.messagesGridView.CurrentCell = this.messagesGridView.Rows[currentRow + 1].Cells[0];
                        this.messagesGridView.Rows[currentRow + 1].Selected = true;
                    }

                    this.messagesGridView.Refresh();
                    currentRow++;
                }
            }

            Cursor.Current = Cursors.Default;
            MessageBox.Show("Tenant Configuration Messages load complete. " +
                loadDatalistsSuccessful + " DataLists Loaded " +
                loadDatalistItemSuccessful + " DataList Items loaded, and " +
                loadDatalistErrors + " DataList errors reported " +
                loadDatalistItemErrors + " DataList item errors reported. ",
                "Tenant Load Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}