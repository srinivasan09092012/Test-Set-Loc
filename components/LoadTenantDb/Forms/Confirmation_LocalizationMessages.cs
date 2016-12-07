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
using HP.HSP.UA3.Administration.UX.Common;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    public partial class Confirmation_LocalizationMessages : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            if(datalist.Id == "00000000-0000-0000-0000-000000000000")
            {
                datalist.Id = null;
            }

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
            bool added = false;            

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
            bool createAttribute = true;
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

                        List<string> attributeValues = new List<string>();
                        if (createAttribute)
                        {
                            // Process the attribute values.                            
                            attributeValues.Add("Info");
                            attributeValues.Add("Success");
                            attributeValues.Add("Question");
                            attributeValues.Add("Warning");
                            attributeValues.Add("Error");

                            this.CreateAttributeDataListsWithValue(datalist.TenantModuleId, datalist, "MessageType", attributeValues);
                            createAttribute = false;
                        }


                        // Set the datalist id for the datalist item.
                        datalistItem.DataListId = datalist.Id;

                        //Check to see if the descirption is too long 
                        if (this.MainForm.LocalizationMessages[i].Messages[j].Text.Length > 1000)
                        {
                            log.Error("Error Confirmation_LocalizationMessages.LoadGrid Error " +
                              "DESCRIPTION TOO LONG - Max 1000 for Key=" + this.MainForm.LocalizationMessages[i].Messages[j].ContentId);
                        }

                        // Determine if the DataList Item should be added or updated.
                        if (datalistItem.Id == null)
                        {
                            // Add the DatalistItem
                            added = datalistItem.AddDataListItem(datalistItem);

                            if (added)
                            {
                                this.MainForm.LocalizationMessages[i].Messages[j].Action = "Added";
                                loadDatalistItemSuccessful++;
                            }
                            else
                            {
                                this.MainForm.LocalizationMessages[i].Messages[j].Action = "Add Error";
                                log.Error("Error Confirmation_LocalizationMessages.LoadGrid Add Error " +
                                    "ContentId=" + datalistItem.ContentId);
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
                                log.Error("Error Confirmation_LocalizationMessages.LoadGrid Update Error " + 
                                    "ContentId=" + datalistItem.ContentId);
                                loadDatalistItemErrors++;
                            }
                        }


                        datalistItem.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheDataListItemAttrKey, "false", "false", "false");
                        datalistItem.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheItemLinkerKey, "false", "false", "false");
                        datalistItem.RefreshCache("FullCodeTableKey", "true", "false", "true");

                        updateDataListItemAttributeWithValue(
                            "MessageType",
                            MainForm.LocalizationMessages[i].Messages[j].Type,
                            datalist,
                            datalistItem );
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
            log.Info("Tenant Configuration Messages load complete. " + "\n" +
                loadDatalistsSuccessful + " DataLists Loaded, " + "\n" +
                loadDatalistItemSuccessful + " DataList Items loaded, " + "\n" +
                loadDatalistErrors + " DataList errors reported, and " + "\n" +
                loadDatalistItemErrors + " DataList item errors reported. ");

            MessageBox.Show("Tenant Configuration Messages load complete. " +
                loadDatalistsSuccessful + " DataLists Loaded " +
                loadDatalistItemSuccessful + " DataList Items loaded, and " +
                loadDatalistErrors + " DataList errors reported " +
                loadDatalistItemErrors + " DataList item errors reported. ",
                "Tenant Load Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CreateAttributeDataListsWithValue(
            string tenantModuleId,
            Datalist parentDatalist,
            string nameOfAttribute,
            List<string> attributeValues)
        {
            bool updated = false;
            List<bool> addedSuccessfullyList = new List<bool>();
            List<DatalistItem> attributeDatalistItems = new List<DatalistItem>();
            bool addedSuccessfully = false;
            int numAttributeIdx = 0;
            bool addAttributeToDataList = true;
            string defaultAttributeDatalistItemId = null;
            DatalistItem attributeDatalistItem = null;

            // Create Attribute DataList using the nameOfAttribute
            Datalist attributeDatalist = new Datalist();
            attributeDatalist.MainForm = this.MainForm;
            attributeDatalist.ContentId = "Core.DataList.Attributes." + nameOfAttribute;
            attributeDatalist.Id = attributeDatalist.GetDataListId(attributeDatalist);

            if (attributeDatalist.Id == null)
            {
                attributeDatalist.TenantId = MainForm.TenantId;
                attributeDatalist.TenantModuleId = tenantModuleId;
                attributeDatalist.IdentifierId = "USER1";
                attributeDatalist.IsActive = true;
                attributeDatalist.Name = nameOfAttribute + " Attribute";
                attributeDatalist.Description = nameOfAttribute + " Attribute";
                attributeDatalist = attributeDatalist.AddDataList(attributeDatalist);

                // Create Attribute DataList Item with all values passed in 
                foreach (string attributeValue in attributeValues)
                {
                    attributeDatalistItem = new DatalistItem();
                    attributeDatalistItem.MainForm = this.MainForm;
                    attributeDatalistItem.ContentId = attributeDatalist.ContentId;
                    attributeDatalistItem.Key = attributeValue;
                    attributeDatalistItem.Id = attributeDatalistItem.GetDataListItemId(attributeDatalist, attributeDatalistItem);

                    if (attributeDatalistItem.Id == null)
                    {
                        if (attributeDatalistItem.DataListItemLanguages.Count == 0)
                        {
                            attributeDatalistItem.DataListItemLanguages.Add(new DatalistItemLanguage());
                        }

                        attributeDatalistItem.DataListId = attributeDatalist.Id;
                        attributeDatalistItem.TenantId = this.MainForm.TenantId;
                        attributeDatalistItem.IdentifierId = "USER1";
                        attributeDatalistItem.IsActive = true;
                        attributeDatalistItem.OrderIndex = 0;
                        attributeDatalistItem.DataListItemLanguages[0].Locale = "en-us";
                        attributeDatalistItem.DataListItemLanguages[0].Description = attributeValue;
                        attributeDatalistItem.DataListItemLanguages[0].IsActive = true;
                        attributeDatalistItem.DataListItemLanguages[0].DataListItemId = null;
                        addedSuccessfully = attributeDatalistItem.AddDataListItem(attributeDatalistItem);
                        if (addedSuccessfully)
                        {
                            attributeDatalistItems.Add(attributeDatalistItem);
                        }
                        addedSuccessfullyList.Add(addedSuccessfully);
                    }
                }

                //If all attribute values where added succesfully then continue              
                foreach (bool added in addedSuccessfullyList)
                {
                    if (!added)
                    {
                        addAttributeToDataList = false;
                        break;
                    }
                }
            }

            //Check to see if Attribute has already been Added to the DataList, assumes exists data list with
            //the attribute already
            parentDatalist.GetDataListDirect(parentDatalist);
            string parentAttributeDataListId = attributeDatalist.GetDataList(attributeDatalist);
            if (parentAttributeDataListId == "00000000-0000-0000-0000-000000000000")
            {
                parentAttributeDataListId = null;
            }

            //Add the Attribute to the data list if needed
            if ((addAttributeToDataList && parentAttributeDataListId == null) || 
                (addAttributeToDataList && parentDatalist.DataListItemAttributes.Count == 0))
            {
                //Get default id for Type attribute
                if (nameOfAttribute == "MessageType")
                {
                    if (attributeDatalistItems.Count == 0)
                    {
                        attributeDatalistItem = new DatalistItem();
                        attributeDatalistItem.Key = "Info";
                        defaultAttributeDatalistItemId = attributeDatalistItem.GetDataListItemId(attributeDatalist, attributeDatalistItem);

                    }
                    else
                    {
                        defaultAttributeDatalistItemId = attributeDatalistItems[0].Id;
                    }
                }
                   
                //Update the Datalist with the Attribute 
                numAttributeIdx = parentDatalist.DataListItemAttributes.Count;
                parentDatalist.DataListItemAttributes.Add(new DatalistItemAttribute());
                parentDatalist.DataListItemAttributes[numAttributeIdx].IsActive = true;
                parentDatalist.DataListItemAttributes[numAttributeIdx].TypeName = nameOfAttribute;
                parentDatalist.DataListItemAttributes[numAttributeIdx].DataListId = parentDatalist.Id;
                parentDatalist.DataListItemAttributes[numAttributeIdx].TypeDataListId = attributeDatalist.Id;
                //Pick the first datalist attribute item as the default value
                parentDatalist.DataListItemAttributes[numAttributeIdx].TypeDefaultItemId = defaultAttributeDatalistItemId;
                updated = parentDatalist.UpdateDataList(parentDatalist);
            }            
        }

        private bool updateDataListItemAttributeWithValue(
            string nameOfAttribute,
            string attributeValue,
            Datalist parentDatalist,
            DatalistItem parentDatalistItem)
        {
            int numAttributeValueIdx = 0;
            int numAttributeIdx = 0;
            bool updateSuccess = true;
            List<DatalistItem> attributeDataListItems = new List<DatalistItem>();
            Datalist attributeDatalist = new Datalist();
            DatalistItem attributeDatalistItem = new DatalistItem();
            string attributeDataListItemIdToUse = null;
            bool updated = false;

            //For this attribute(nameOfAttribute) get a list of data list items
            try
            {
                attributeDatalist.ContentId = "Core.DataList.Attributes." + nameOfAttribute;
                attributeDatalist.Id = attributeDatalist.GetDataListId(attributeDatalist);
                attributeDatalistItem.Key = attributeValue;
                attributeDataListItemIdToUse = attributeDatalistItem.GetDataListItemId(attributeDatalist, attributeDatalistItem);
            }
            catch
            {
                updateSuccess = false;
            }

            if (updateSuccess && attributeDataListItemIdToUse != null)
            {

                // Update the DataList Item with new Attribute Values
                parentDatalist.GetDataListDirect(parentDatalist);
                string parentDataListemItemId = parentDatalistItem.GetDataListItemId(parentDatalist, parentDatalistItem);

                //Set DataListItem ID on language so it will not try to perform and add
                foreach (DatalistItemLanguage datalistItemLanguage in parentDatalistItem.DataListItemLanguages)
                {
                    datalistItemLanguage.DataListItemId = parentDataListemItemId;
                }

                for (int i = 0; i < parentDatalist.DataListItemAttributes.Count; i++)
                {
                    if (parentDatalist.DataListItemAttributes[i].TypeName == nameOfAttribute)
                    {
                        numAttributeIdx = i;
                        break;
                    }
                }

                //Update the datalist item with the attribute value
                numAttributeValueIdx = parentDatalistItem.DataListItemAttributeValues.Count;
                parentDatalistItem.DataListItemAttributeValues.Add(new DatalistItemAttributeValue());
                parentDatalistItem.DataListItemAttributeValues[numAttributeValueIdx].DataListsItemId = parentDatalistItem.Id;
                parentDatalistItem.DataListItemAttributeValues[numAttributeValueIdx].DataListAttributeId = parentDatalist.DataListItemAttributes[numAttributeIdx].DataListsAttributeId;
                parentDatalistItem.DataListItemAttributeValues[numAttributeValueIdx].DataListsItemValueId = attributeDataListItemIdToUse;
                try
                {
                    updated = parentDatalistItem.UpdateDataListItem(parentDatalistItem);
                }
                catch
                {
                    updateSuccess = false;
                }
            }

            return updateSuccess;
        }
    }
}