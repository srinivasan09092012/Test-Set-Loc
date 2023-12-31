using HP.HSP.UA3.Administration.UX.Common;

//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Utilities.LoadTenantDb.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    public partial class ConfirmationSecurityDialog : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ConfirmationSecurityDialog()
        {
            this.InitializeComponent();
        }

        public MainForm MainForm { get; set; }

        private void CancelPushButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfirmationSecurityLoad(object sender, EventArgs e)
        {
            this.RefreshGrid();

            this.loadRolesCheckbox.Checked = true;
            this.loadFunctionsCheckbox.Checked = true;
            this.loadRightsCheckbox.Checked = true;

            if (MainForm.SecurityFunctions.Count > 0 &&
                MainForm.SecurityRoles.Count > 0)
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
            int lastCol;

            if (MainForm.SecurityRoles.Count > 0)
            {
                int rolesAdded = 0;
                for (int i = 0; i < MainForm.SecurityRoles[0].SecurityNodeAttribute.Count; i++)
                {
                    string roleName = MainForm.SecurityRoles[0].SecurityNodeAttribute[i].Name;
                    if (roleName == "English Description" ||
                        roleName == "Spanish Description" ||
                        roleName == "roleType" ||
                        roleName == "isInternal")
                    {
                        this.roleAttributesListBox.Items.Add(roleName);
                        this.roleAttributesListBox.SetItemChecked(rolesAdded, true);

                        lastCol = this.rolesGridView.ColumnCount;
                        this.rolesGridView.ColumnCount++;
                        this.rolesGridView.Columns[lastCol].Name = roleName;
                        rolesAdded++;
                    }
                }
            }

            if (MainForm.SecurityFunctions.Count > 0)
            {
                int functionsAdded = 0;
                for (int i = 0; i < MainForm.SecurityFunctions[0].SecurityNodeAttribute.Count; i++)
                {
                    string functionName = MainForm.SecurityFunctions[0].SecurityNodeAttribute[i].Name;
                    if (functionName == "English Description" ||
                        functionName == "Spanish Description")
                    {
                        this.functionAttributesListBox.Items.Add(functionName);
                        this.functionAttributesListBox.SetItemChecked(functionsAdded, true);

                        lastCol = this.functionsGridView.ColumnCount;
                        this.functionsGridView.ColumnCount++;
                        this.functionsGridView.Columns[lastCol].Name = functionName;
                        functionsAdded++;
                    }
                }
            }

            if (MainForm.SecurityRights.Count > 0)
            {
                int rightsAdded = 0;
                for (int i = 0; i < MainForm.SecurityRights[0].SecurityNodeAttribute.Count; i++)
                {
                    string rightName = MainForm.SecurityRights[0].SecurityNodeAttribute[i].Name;
                    if (rightName == "English Description" ||
                        rightName == "Spanish Description" ||
                        rightName == "type")
                    {
                        this.rightAttributesListBox.Items.Add(rightName);
                        this.rightAttributesListBox.SetItemChecked(rightsAdded, true);

                        lastCol = this.rightsGridView.ColumnCount;
                        this.rightsGridView.ColumnCount++;
                        this.rightsGridView.Columns[lastCol].Name = rightName;
                        rightsAdded++;
                    }
                }
            }

            for (int i = 0; i < MainForm.SecurityRoles.Count; i++)
            {
                if (string.IsNullOrEmpty(MainForm.SecurityRoles[i].ContentId))
                {
                    MainForm.SecurityRoles[i].ContentId = "<< NULL OR MISSING VALUE >>";
                }

                row.Clear();
                row.Add(MainForm.SecurityRoles[i].Action);
                row.Add(MainForm.SecurityRoles[i].Module.Name);
                row.Add(MainForm.SecurityRoles[i].Name);
                row.Add(MainForm.SecurityRoles[i].ContentId);

                for (int j = 0; j < MainForm.SecurityRoles[i].SecurityNodeAttribute.Count; j++)
                {
                    row.Add(MainForm.SecurityRoles[i].SecurityNodeAttribute[j].Value);
                }

                this.rolesGridView.Rows.Add(row.ToArray());
            }

            for (int i = 0; i < MainForm.SecurityFunctions.Count; i++)
            {
                if (string.IsNullOrEmpty(MainForm.SecurityFunctions[i].ContentId))
                {
                    MainForm.SecurityFunctions[i].ContentId = "<< NULL OR MISSING VALUE >>";
                }

                row.Clear();
                row.Add(MainForm.SecurityFunctions[i].Action);
                row.Add(MainForm.SecurityFunctions[i].Module.Name);
                row.Add(MainForm.SecurityFunctions[i].Name);
                row.Add(MainForm.SecurityFunctions[i].ContentId);
                row.Add(MainForm.SecurityFunctions[i].ParentLink);

                for (int j = 0; j < MainForm.SecurityFunctions[i].SecurityNodeAttribute.Count; j++)
                {
                    row.Add(MainForm.SecurityFunctions[i].SecurityNodeAttribute[j].Value);
                }

                this.functionsGridView.Rows.Add(row.ToArray());
            }

            for (int i = 0; i < MainForm.SecurityRights.Count; i++)
            {
                if (string.IsNullOrEmpty(MainForm.SecurityRights[i].ContentId))
                {
                    MainForm.SecurityRights[i].ContentId = "<< NULL OR MISSING VALUE >>";
                }

                row.Clear();
                row.Add(MainForm.SecurityRights[i].Action);
                row.Add(MainForm.SecurityRights[i].Module.Name);
                row.Add(MainForm.SecurityRights[i].Name);
                row.Add(MainForm.SecurityRights[i].ContentId);
                row.Add(MainForm.SecurityRights[i].ParentLink);

                for (int j = 0; j < MainForm.SecurityRights[i].SecurityNodeAttribute.Count; j++)
                {
                    row.Add(MainForm.SecurityRights[i].SecurityNodeAttribute[j].Value);
                }

                this.rightsGridView.Rows.Add(row.ToArray());
            }
        }

        private void ProcessLoad(object sender, EventArgs e)
        {
            string coreTenantModuleId;
            int loadRoleDatalistItemSuccessful = 0;
            int loadFunctionDatalistItemSuccessful = 0;
            int loadRightDatalistItemSuccessful = 0;
            int loadErrors = 0;

            Cursor.Current = Cursors.WaitCursor;
            coreTenantModuleId = ConfigurationManager.AppSettings["CoreTenantModuleId"];

            if (coreTenantModuleId != null)
            {
                if (this.loadRolesCheckbox.Checked == true)
                {
                    this.LoadSecurityRoles(coreTenantModuleId, ref loadRoleDatalistItemSuccessful, ref loadErrors);
                }

                if (this.loadFunctionsCheckbox.Checked == true)
                {
                    this.LoadSecurityFunctions(coreTenantModuleId, ref loadFunctionDatalistItemSuccessful, ref loadErrors);
                }

                if (this.loadRightsCheckbox.Checked == true && MainForm.SecurityRights.Count > 0)
                {
                    this.LoadSecurityRights(coreTenantModuleId, ref loadRightDatalistItemSuccessful, ref loadErrors);
                }

                Cursor.Current = Cursors.Default;
                log.Info("Tenant Configuration load complete. Roles =" + loadRoleDatalistItemSuccessful + 
                    " Functions =" + loadFunctionDatalistItemSuccessful +
                    " Rights = " + loadRightDatalistItemSuccessful +
                    " DataList Items loaded and " + loadErrors + " errors reported.");
                MessageBox.Show("Tenant Configuration load complete. Roles = " + loadRoleDatalistItemSuccessful +
                    " Functions =" + loadFunctionDatalistItemSuccessful +
                    " Rights = " + loadRightDatalistItemSuccessful +
                    " DataList Items loaded and " + loadErrors + " errors reported.", "Tenant Load Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void LoadSecurityRoles(string tenantModuleId, ref int loadDatalistItemSuccessful, ref int loadErrors)
        {
            int currentRow = 0;
            bool updated = true;
            bool added = false;
            DatalistItem parentDatalistItem = new DatalistItem();
            Datalist parentDatalist = new Datalist();

            Datalist datalist = new Datalist();
            datalist.MainForm = this.MainForm;
            datalist.TenantId = MainForm.TenantId;
            datalist.TenantModuleId = tenantModuleId;
            datalist.IdentifierId = "USER1";
            datalist.IsActive = true;
            datalist.ContentId = "Core.SecurityRoles";
            datalist.Id = datalist.GetDataListId(datalist);
            if (datalist.Id == "00000000-0000-0000-0000-000000000000")
            {
                datalist.Id = null;
            }

            datalist.Name = "Security Roles";
            datalist.Description = "Security Roles";

            if (datalist.Id == null)
            {
                datalist = datalist.AddDataList(datalist);
            }
            else
            {
                updated = datalist.UpdateDataList(datalist);
            }

            parentDatalist = datalist;

            //Assumption here is that if Core.DataList.Attributes.roleType exists then no need to 
            //create attributes
            Datalist checkAttributeDatalist = new Datalist();
            string contentIdAttribute = "Core.DataList.Attributes.roleType";
            bool createAttribute = false;
            if (!checkAttributeDatalist.DoesDataListExistsDirect(contentIdAttribute))
            {
                createAttribute = true;
            }


            for (int i = 0; i < MainForm.SecurityRoles.Count; i++)
            {
                if (MainForm.SecurityRoles[i].ContentId != "<< NULL OR MISSING VALUE >>")
                {
                    DatalistItem datalistItem = new DatalistItem();
                    datalistItem.MainForm = this.MainForm;
                    datalistItem.ContentId = this.MainForm.SecurityRoles[i].ContentId;
                    datalistItem.Key = this.MainForm.SecurityRoles[i].Name;
                    datalistItem.Id = datalistItem.GetDataListItemId(datalist, datalistItem);
                    datalistItem.DataListId = datalist.Id;
                    datalistItem.TenantId = this.MainForm.TenantId;
                    datalistItem.IsActive = true;
                    datalistItem.OrderIndex = 0;
                    datalistItem.IdentifierId = "USER1";

                    if (datalistItem.DataListItemLanguages.Count == 0)
                    {
                        datalistItem.DataListItemLanguages.Add(new DatalistItemLanguage());
                        datalistItem.DataListItemLanguages.Add(new DatalistItemLanguage());
                    }

                    datalistItem.DataListItemLanguages[0].Locale = "en-us";
                    datalistItem.DataListItemLanguages[0].Description = this.MainForm.SecurityRoles[i].SecurityNodeAttribute[0].Value;
                    datalistItem.DataListItemLanguages[0].IsActive = true;
                    datalistItem.DataListItemLanguages[0].DataListItemId = datalistItem.Id;

                    datalistItem.DataListItemLanguages[1].Locale = "es-mx";
                    datalistItem.DataListItemLanguages[1].Description = this.MainForm.SecurityRoles[i].SecurityNodeAttribute[1].Value;
                    datalistItem.DataListItemLanguages[1].IsActive = true;
                    datalistItem.DataListItemLanguages[1].DataListItemId = datalistItem.Id;

                    if (datalistItem.Id == null)
                    {
                        //Check to see if we have a valid GUID if not error off and skip process 
                        Guid testNewGuid;
                        if (!Guid.TryParse(this.MainForm.SecurityRoles[i].Id, out testNewGuid))
                        {
                            log.Error("Error Confirmation_Security.LoadSecurityRoles Add Error " +
                              "INVALID GUID=" + this.MainForm.SecurityRoles[i].Id);
                        }

                        datalistItem.Id = this.MainForm.SecurityRoles[i].Id;
                        added = datalistItem.AddDataListItem(datalistItem);

                        if (added)
                        {
                            parentDatalistItem = datalistItem;
                            this.MainForm.SecurityRoles[i].Action = "Added";
                            loadDatalistItemSuccessful++;
                        }
                        else
                        {
                            this.MainForm.SecurityRoles[i].Action = "Add Error";
                            log.Error("Error Confirmation_Security.LoadSecurityRoles Add Error " +
                                "ContentId=" + datalistItem.ContentId);
                            loadErrors++;
                            continue;
                        }
                    }
                    else
                    {
                        try
                        {
                            updated = datalistItem.UpdateDataListItem(datalistItem);
                        }
                        catch
                        {
                            updated = false;
                        }

                        if (updated)
                        {
                            parentDatalistItem = datalistItem;
                            this.MainForm.SecurityRoles[i].Action = "Updated";
                            loadDatalistItemSuccessful++;
                        }
                        else
                        {
                            this.MainForm.SecurityRoles[i].Action = "Update Error";
                            log.Error("Error Confirmation_Security.LoadSecurityRoles Update Error " +
                                "ContentId=" + datalistItem.ContentId);
                            loadErrors++;
                        }
                    }

                    if (createAttribute)
                    {
                        //Add RoleType Attribute
                        List<string> attributeValuesRoleType = new List<string>();
                        attributeValuesRoleType.Add("U");
                        attributeValuesRoleType.Add("A");
                        this.CreateAttributeDataListsWithValue(datalist.TenantModuleId, parentDatalist, "roleType", attributeValuesRoleType);

                         //Add IsInternal Attribute
                        List<string> attributeValuesIsInternal = new List<string>();
                        attributeValuesIsInternal.Add("true");
                        attributeValuesIsInternal.Add("false");
                        this.CreateAttributeDataListsWithValue(datalist.TenantModuleId, parentDatalist, "isInternal", attributeValuesIsInternal);

                        datalistItem.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheDataListItemAttrKey, "false", "false", "false");
                        datalistItem.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheItemLinkerKey, "false", "false", "false");
                        datalistItem.RefreshCache("FullCodeTableKey", "true", "false", "true");

                        createAttribute = false;
                    }

                    string attributeValue = null;
                    for (int j = 0; j < MainForm.SecurityRoles[i].SecurityNodeAttribute.Count; j++)
                    {
                        string roleName = MainForm.SecurityRoles[i].SecurityNodeAttribute[j].Name;

                        if (roleName == "roleType")
                        {
                            attributeValue = MainForm.SecurityRoles[i].SecurityNodeAttribute[j].Value;
                            updateDataListItemAttributeWithValue(
                               "roleType",
                                attributeValue,
                               datalist,
                               datalistItem);                         
                        } 
                        else if(roleName == "isInternal")
                        {
                            updateDataListItemAttributeWithValue(
                                  "isInternal",
                                   attributeValue,
                                  datalist,
                                  datalistItem);
                        }
                    }

                    this.rolesGridView.Rows[currentRow].Cells[0].Value = this.MainForm.SecurityRoles[i].Action;

                    if (this.rolesGridView.Rows.Count > currentRow + 1)
                    {
                        this.rolesGridView.CurrentCell = this.rolesGridView.Rows[currentRow + 1].Cells[0];
                        this.rolesGridView.Rows[currentRow + 1].Selected = true;
                    }

                    this.rolesGridView.Refresh();
                    currentRow++;
                }
                else
                {
                    this.rolesGridView.Rows[currentRow].Cells[0].Value = "Invalid ContentId";

                    if (this.rolesGridView.Rows.Count > currentRow + 1)
                    {
                        this.rolesGridView.CurrentCell = this.rolesGridView.Rows[currentRow + 1].Cells[0];
                        this.rolesGridView.Rows[currentRow + 1].Selected = true;
                    }

                    this.rolesGridView.Refresh();
                    currentRow++;
                }
             }
        }

        private void LoadSecurityFunctions(string tenantModuleId, ref int loadDatalistItemSuccessful, ref int loadErrors)
        {            
            int currentRow = 0;
            bool updated = true;
            bool added = false;
            DatalistItem parentDatalistItem = new DatalistItem();
            Datalist parentDatalist = new Datalist();

            Datalist datalist = new Datalist();
            datalist.MainForm = this.MainForm;
            datalist.TenantId = MainForm.TenantId;
            datalist.TenantModuleId = tenantModuleId;
            datalist.IdentifierId = "USER1";
            datalist.IsActive = true;
            datalist.ContentId = "Core.SecurityFunctions";
            datalist.Id = datalist.GetDataListId(datalist);
            if (datalist.Id == "00000000-0000-0000-0000-000000000000")
            {
                datalist.Id = null;
            }

            datalist.Name = "Security Functions";
            datalist.Description = "Security Functions";

            if (datalist.Id == null)
            {
                datalist = datalist.AddDataList(datalist);
            }
            else
            {
                updated = datalist.UpdateDataList(datalist);
            }

            parentDatalist = datalist;

            for (int i = 0; i < MainForm.SecurityFunctions.Count; i++)
            {
                if (MainForm.SecurityFunctions[i].ContentId != "<< NULL OR MISSING VALUE >>")
                {
                    DatalistItem datalistItem = new DatalistItem();
                    datalistItem.MainForm = this.MainForm;
                    datalistItem.ContentId = this.MainForm.SecurityFunctions[i].ContentId;
                    datalistItem.Key = this.MainForm.SecurityFunctions[i].Name;
                    datalistItem.Id = datalistItem.GetDataListItemId(datalist, datalistItem);
                    datalistItem.DataListId = datalist.Id;
                    datalistItem.TenantId = this.MainForm.TenantId;
                    datalistItem.IsActive = true;
                    datalistItem.OrderIndex = 0;
                    datalistItem.IdentifierId = "USER1";

                    if (datalistItem.DataListItemLanguages.Count == 0)
                    {
                        datalistItem.DataListItemLanguages.Add(new DatalistItemLanguage());
                        datalistItem.DataListItemLanguages.Add(new DatalistItemLanguage());
                    }

                    datalistItem.DataListItemLanguages[0].Locale = "en-us";
                    datalistItem.DataListItemLanguages[0].Description = this.MainForm.SecurityFunctions[i].SecurityNodeAttribute[0].Value;
                    datalistItem.DataListItemLanguages[0].IsActive = true;
                    datalistItem.DataListItemLanguages[0].DataListItemId = datalistItem.Id;

                    datalistItem.DataListItemLanguages[1].Locale = "es-mx";
                    datalistItem.DataListItemLanguages[1].Description = this.MainForm.SecurityFunctions[i].SecurityNodeAttribute[1].Value;
                    datalistItem.DataListItemLanguages[1].IsActive = true;
                    datalistItem.DataListItemLanguages[1].DataListItemId = datalistItem.Id;

                    if (datalistItem.Id == null)
                    {
                        //Check to see if we have a valid GUID if not error off and skip process 
                        Guid testNewGuid;
                        if (!Guid.TryParse(this.MainForm.SecurityFunctions[i].Id, out testNewGuid))
                        {
                            log.Error("Error Confirmation_Security.LoadSecurityFunctions Add Error " +
                              "INVALID GUID=" + this.MainForm.SecurityFunctions[i].Id);
                        }

                        datalistItem.Id = this.MainForm.SecurityFunctions[i].Id;
                        added = datalistItem.AddDataListItem(datalistItem);

                        if (added)
                        {
                            parentDatalistItem = datalistItem;
                            this.MainForm.SecurityFunctions[i].Action = "Added";
                            loadDatalistItemSuccessful++;
                        }
                        else
                        {
                            this.MainForm.SecurityFunctions[i].Action = "Add Error";
                            log.Error("Error Confirmation_Security.LoadSecurityFunctions Add Error " +
                                "ContentId=" + datalistItem.ContentId);
                            loadErrors++;
                            continue;
                        }
                    }
                    else
                    {
                        try
                        {
                            updated = datalistItem.UpdateDataListItem(datalistItem);
                        }
                        catch
                        {
                            updated = false;
                        }

                        if (updated)
                        {
                            parentDatalistItem = datalistItem;
                            this.MainForm.SecurityFunctions[i].Action = "Updated";
                            loadDatalistItemSuccessful++;
                        }
                        else
                        {
                            this.MainForm.SecurityFunctions[i].Action = "Update Error";
                            log.Error("Error Confirmation_Security.LoadSecurityFunctions Update Error " +
                                "ContentId=" + datalistItem.ContentId);
                            loadErrors++;
                        }                        
                    }

                    if (!string.IsNullOrEmpty(MainForm.SecurityFunctions[i].ParentLink) &&
                        datalistItem != null)
                    {
                        bool newLink = true;

                        Datalist roleDatalist = new Datalist();
                        roleDatalist.MainForm = this.MainForm;

                        DatalistItem roleDatalistItem = new DatalistItem();
                        roleDatalistItem.MainForm = this.MainForm;

                        roleDatalist.ContentId = "Core.SecurityRoles";
                        roleDatalistItem.Key = this.MainForm.SecurityFunctions[i].ParentKey;
                        roleDatalist.Id = roleDatalist.GetDataListId(roleDatalist);
                        roleDatalistItem.DataListId = roleDatalist.Id;

                        newLink = roleDatalistItem.DoesLinkExists(roleDatalist.Id, datalistItem.Key, roleDatalistItem.Key);

                        if (newLink)
                        {
                            roleDatalistItem.DataListItemLinks.Add(new DatalistItemLink());
                            roleDatalist.Id = roleDatalist.GetDataListId(roleDatalist);
                            roleDatalistItem.Id = datalistItem.GetDataListItemId(roleDatalist, roleDatalistItem);
                            roleDatalistItem.DataListItemLinks[0].ChildId = datalistItem.Id;
                            roleDatalistItem.DataListItemLinks[0].Active = true;
                            updated = roleDatalistItem.UpdateDataListItem(roleDatalistItem);

                            if (!updated)
                            {
                                this.MainForm.SecurityFunctions[i].Action = "Link Error";
                                log.Error("Error Confirmation_Security.LoadSecurityFunctions Link Error " +
                                    "ContentId=" + datalistItem.ContentId);
                                loadDatalistItemSuccessful--;
                                loadErrors++;
                            }
                        }
                    }
 
                    this.functionsGridView.Rows[currentRow].Cells[0].Value = this.MainForm.SecurityFunctions[i].Action;

                    if (this.functionsGridView.Rows.Count > currentRow + 1)
                    {
                        this.functionsGridView.CurrentCell = this.functionsGridView.Rows[currentRow + 1].Cells[0];
                        this.functionsGridView.Rows[currentRow + 1].Selected = true;
                    }

                    this.functionsGridView.Refresh();
                    currentRow++;
                }
                else
                {
                    this.functionsGridView.Rows[currentRow].Cells[0].Value = "Invalid ContentId";

                    if (this.functionsGridView.Rows.Count > currentRow + 1)
                    {
                        this.functionsGridView.CurrentCell = this.functionsGridView.Rows[currentRow + 1].Cells[0];
                        this.functionsGridView.Rows[currentRow + 1].Selected = true;
                    }

                    this.functionsGridView.Refresh();
                    currentRow++;
                }
            }
        }

        private void LoadSecurityRights(string tenantModuleId, ref int loadDatalistItemSuccessful, ref int loadErrors)
        {
            int currentRow = 0;
            bool updated = true;
            bool added = false;           
            DatalistItem parentDatalistItem = new DatalistItem();
            parentDatalistItem.MainForm = this.MainForm;
            Datalist parentDatalist = new Datalist();
            parentDatalist.MainForm = this.MainForm;

            Datalist datalist = new Datalist();
            datalist.MainForm = this.MainForm;
            datalist.TenantId = MainForm.TenantId;
            datalist.TenantModuleId = tenantModuleId;
            datalist.IdentifierId = "USER1";
            datalist.IsActive = true;
            datalist.ContentId = "Core.SecurityRights";
            datalist.Id = datalist.GetDataListId(datalist);
            if (datalist.Id == "00000000-0000-0000-0000-000000000000")
            {
                datalist.Id = null;
            }

            datalist.Name = "Security Rights";
            datalist.Description = "Security Rights";

            if (datalist.Id == null)
            {
                datalist = datalist.AddDataList(datalist);
            }
            else
            {
                updated = datalist.UpdateDataList(datalist);
            }

            parentDatalist = datalist;

            //Assumption here is that if Core.DataList.Attributes.RightTypes exists then no need to 
            //create attributes
            Datalist checkAttributeDatalist = new Datalist();
            string contentIdAttribute = "Core.DataList.Attributes.RightTypes";
            bool createAttribute = false;
            if (!checkAttributeDatalist.DoesDataListExistsDirect(contentIdAttribute))
            {
                createAttribute = true;
            }

            for (int i = 0; i < MainForm.SecurityRights.Count; i++)
            {
                if (MainForm.SecurityRights[i].ContentId != "<< NULL OR MISSING VALUE >>")
                {
                    DatalistItem datalistItem = new DatalistItem();
                    datalistItem.MainForm = this.MainForm;
                    datalistItem.ContentId = this.MainForm.SecurityRights[i].ContentId;
                    datalistItem.Key = this.MainForm.SecurityRights[i].Name;
                    datalistItem.Id = datalistItem.GetDataListItemId(datalist, datalistItem);
                    datalistItem.DataListId = datalist.Id;
                    datalistItem.TenantId = this.MainForm.TenantId;
                    datalistItem.IsActive = true;
                    datalistItem.OrderIndex = 0;
                    datalistItem.IdentifierId = "USER1";

                    if (datalistItem.DataListItemLanguages.Count == 0)
                    {
                        datalistItem.DataListItemLanguages.Add(new DatalistItemLanguage());
                        datalistItem.DataListItemLanguages.Add(new DatalistItemLanguage());
                    }

                    datalistItem.DataListItemLanguages[0].Locale = "en-us";
                    datalistItem.DataListItemLanguages[0].Description = this.MainForm.SecurityRights[i].SecurityNodeAttribute[0].Value;
                    datalistItem.DataListItemLanguages[0].IsActive = true;
                    datalistItem.DataListItemLanguages[0].DataListItemId = datalistItem.Id;

                    datalistItem.DataListItemLanguages[1].Locale = "es-mx";
                    datalistItem.DataListItemLanguages[1].Description = this.MainForm.SecurityRights[i].SecurityNodeAttribute[1].Value;
                    datalistItem.DataListItemLanguages[1].IsActive = true;
                    datalistItem.DataListItemLanguages[1].DataListItemId = datalistItem.Id;

                    if (datalistItem.Id == null)
                    {
                        //Check to see if we have a valid GUID if not error off and skip process 
                        Guid testNewGuid;
                        if (!Guid.TryParse(this.MainForm.SecurityRights[i].Id, out testNewGuid))
                        {
                            log.Error("Error Confirmation_Security.LoadSecurityRights Add Error " +
                              "INVALID GUID=" + this.MainForm.SecurityRights[i].Id);
                        }

                        datalistItem.Id = this.MainForm.SecurityRights[i].Id;                        
                        added = datalistItem.AddDataListItem(datalistItem);
 
                        if (added)
                        {
                            parentDatalistItem = datalistItem;
                            this.MainForm.SecurityRights[i].Action = "Added";
                            loadDatalistItemSuccessful++;
                        }
                        else
                        {
                            this.MainForm.SecurityRights[i].Action = "Add Error";
                            log.Error("Error Confirmation_Security.LoadSecurityRights Add Error " +
                                "ContentId=" + datalistItem.ContentId);
                            loadErrors++;
                            continue;
                        }
                    }
                    else
                    {
                        try
                        {
                            updated = datalistItem.UpdateDataListItem(datalistItem);
                        }
                        catch
                        {
                            updated = false;
                        }

                        if (updated)
                        {
                            parentDatalistItem = datalistItem;
                            this.MainForm.SecurityRights[i].Action = "Updated";
                            loadDatalistItemSuccessful++;
                        }
                        else
                        {
                            this.MainForm.SecurityRights[i].Action = "Update Error";
                            log.Error("Error Confirmation_Security.LoadSecurityRights Update Error " +
                                "ContentId=" + datalistItem.ContentId);
                            loadErrors++;
                        }
                    }

                    if (!string.IsNullOrEmpty(MainForm.SecurityRights[i].ParentLink) &&
                        datalistItem != null)
                    {
                        bool newLink = true;

                        Datalist functionDatalist = new Datalist();
                        functionDatalist.MainForm = this.MainForm;

                        DatalistItem functionDatalistItem = new DatalistItem();
                        functionDatalistItem.MainForm = this.MainForm;

                        functionDatalist.ContentId = "Core.SecurityFunctions";
                        functionDatalistItem.Key = this.MainForm.SecurityRights[i].ParentKey;
                        functionDatalist.Id = functionDatalist.GetDataListId(functionDatalist);
                        functionDatalistItem.DataListId = functionDatalist.Id;

                        newLink = functionDatalistItem.DoesLinkExists(functionDatalist.Id, datalistItem.Key, functionDatalistItem.Key);

                        if (newLink)
                        {
                            functionDatalistItem.DataListItemLinks.Add(new DatalistItemLink());
                            functionDatalist.Id = functionDatalist.GetDataListId(functionDatalist);
                            functionDatalistItem.Id = datalistItem.GetDataListItemId(functionDatalist, functionDatalistItem);
                            functionDatalistItem.DataListItemLinks[0].ChildId = datalistItem.Id;
                            functionDatalistItem.DataListItemLinks[0].Active = true;
                            updated = functionDatalistItem.UpdateDataListItem(functionDatalistItem);

                            if (!updated)
                            {
                                this.MainForm.SecurityRights[i].Action = "Link Error";
                                log.Error("Error Confirmation_Security.LoadSecurityRights Link Error " +
                                    "ContentId=" + datalistItem.ContentId);
                                loadDatalistItemSuccessful--;
                                loadErrors++;
                            }
                        }
                    }

                    if (createAttribute)
                    {
                        //Add Type Attribute
                        List<string> attributeValuesType = new List<string>();
                        attributeValuesType.Add("Application");
                        attributeValuesType.Add("Page");
                        attributeValuesType.Add("Service");
                        attributeValuesType.Add("Component");
                        attributeValuesType.Add("Operation");
                        attributeValuesType.Add("Control");
                        attributeValuesType.Add("Property");
                        attributeValuesType.Add("Other");
                        this.CreateAttributeDataListsWithValue(datalist.TenantModuleId, parentDatalist, "type", attributeValuesType);

                        datalistItem.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheDataListItemAttrKey, "false", "false", "false");
                        datalistItem.RefreshCache(AdministrationConstants.ApplicationSettings.ODataCacheItemLinkerKey, "false", "false", "false");
                        datalistItem.RefreshCache("FullCodeTableKey", "true", "false", "true");

                        createAttribute = false;
                    }



                    string attributeValue = null;
                    for (int j = 0; j < MainForm.SecurityRights[i].SecurityNodeAttribute.Count; j++)
                    {
                        string roleName = MainForm.SecurityRights[i].SecurityNodeAttribute[j].Name;

                        if (roleName == "type")
                        {
                            attributeValue = MainForm.SecurityRights[i].SecurityNodeAttribute[j].Value;
                            updateDataListItemAttributeWithValue(
                               "type",
                                attributeValue,
                               datalist,
                               datalistItem);
                        }
                    }

                    this.rightsGridView.Rows[currentRow].Cells[0].Value = this.MainForm.SecurityRights[i].Action;

                    if (this.rightsGridView.Rows.Count > currentRow + 1)
                    {
                        this.rightsGridView.CurrentCell = this.rightsGridView.Rows[currentRow + 1].Cells[0];
                        this.rightsGridView.Rows[currentRow + 1].Selected = true;
                    }

                    this.rightsGridView.Refresh();
                    currentRow++;
                }
                else
                {
                    this.rightsGridView.Rows[currentRow].Cells[0].Value = "Invalid ContentId";

                    if (this.rightsGridView.Rows.Count > currentRow + 1)
                    {
                        this.rightsGridView.CurrentCell = this.rightsGridView.Rows[currentRow + 1].Cells[0];
                        this.rightsGridView.Rows[currentRow + 1].Selected = true;
                    }

                    this.rightsGridView.Refresh();
                    currentRow++;
                }
            }
        }

        private void CreateAttributeDataLists(string tenantModuleId, Datalist parentDatalist, DatalistItem parentDatalistItem, string attribute, string attributeValue)
        {
            bool updated = false;
            bool added = false;
            int numAttributeIdx = 0;
            int numAttributeValueIdx = 0;

            // Create Attribute DataList
            Datalist attributeDatalist = new Datalist();
            attributeDatalist.MainForm = this.MainForm;
            attributeDatalist.ContentId = "Core.DataList.Attributes." + attribute;
            attributeDatalist.Id = attributeDatalist.GetDataListId(attributeDatalist);

            if (attributeDatalist.Id == null)
            {
                attributeDatalist.TenantId = MainForm.TenantId;
                attributeDatalist.TenantModuleId = tenantModuleId;
                attributeDatalist.IdentifierId = "USER1";
                attributeDatalist.IsActive = true;
                attributeDatalist.Name = attribute + " Attribute";
                attributeDatalist.Description = attribute + " Attribute";
                attributeDatalist = attributeDatalist.AddDataList(attributeDatalist);
            }

            // Create Attribute DataList Item
            DatalistItem attributeDatalistItem = new DatalistItem();
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
                added = attributeDatalistItem.AddDataListItem(attributeDatalistItem);
            }

            if (added && attributeDatalistItem.Id != null)
            {
                bool newAttribute = true;
                bool newAttributeValue = true;

                // Update Attribute DataList with new Attributes
                parentDatalist.GetDataList(parentDatalist);
                attributeDatalist.GetDataList(attributeDatalist);

                for (int i = 0; i < parentDatalist.DataListItemAttributes.Count; i++)
                {
                    if (parentDatalist.DataListItemAttributes[i].TypeName == attribute)
                    {
                        newAttribute = false;
                        break;
                    }
                }

                if (newAttribute)
                {
                    numAttributeIdx = parentDatalist.DataListItemAttributes.Count;
                    parentDatalist.DataListItemAttributes.Add(new DatalistItemAttribute());
                    parentDatalist.DataListItemAttributes[numAttributeIdx].IsActive = true;
                    parentDatalist.DataListItemAttributes[numAttributeIdx].TypeName = attribute;
                    parentDatalist.DataListItemAttributes[numAttributeIdx].DataListId = parentDatalist.Id;
                    parentDatalist.DataListItemAttributes[numAttributeIdx].TypeDataListId = attributeDatalist.Id;
                    parentDatalist.DataListItemAttributes[numAttributeIdx].TypeDefaultItemId = attributeDatalistItem.Id;
                    updated = parentDatalist.UpdateDataList(parentDatalist);
                }

                // Update Attribute DataList Item with new Attribute Values
                attributeDatalist.GetDataList(attributeDatalist);
                attributeDatalistItem.GetDataListItem(attributeDatalist, attributeDatalistItem);
                parentDatalist.GetDataList(parentDatalist);
                parentDatalistItem.MainForm = this.MainForm;
                parentDatalistItem.GetDataListItem(parentDatalist, parentDatalistItem);

                for (int i = 0; i < parentDatalistItem.DataListItemAttributeValues.Count; i++)
                {
                    if (parentDatalistItem.DataListItemAttributeValues[i].DataListAttributeText == attribute)
                    {
                        newAttributeValue = false;
                        break;
                    }
                }

                if (newAttributeValue)
                {
                    for (int i = 0; i < parentDatalist.DataListItemAttributes.Count; i++)
                    {
                        if (parentDatalist.DataListItemAttributes[i].TypeName == attribute)
                        {
                            numAttributeIdx = i;
                            break;
                        }
                    }

                    numAttributeValueIdx = parentDatalistItem.DataListItemAttributeValues.Count;
                    parentDatalistItem.DataListItemAttributeValues.Add(new DatalistItemAttributeValue());
                    parentDatalistItem.DataListItemAttributeValues[numAttributeValueIdx].DataListsItemId = parentDatalistItem.Id;
                    parentDatalistItem.DataListItemAttributeValues[numAttributeValueIdx].DataListAttributeId = parentDatalist.DataListItemAttributes[numAttributeIdx].DataListsAttributeId;
                    parentDatalistItem.DataListItemAttributeValues[numAttributeValueIdx].DataListsItemValueId = attributeDatalistItem.Id;
                    updated = parentDatalistItem.UpdateDataListItem(parentDatalistItem);
                }
            }
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
            string parentAttributeDataListId = attributeDatalist.GetDataListDirect(attributeDatalist);
            if (parentAttributeDataListId == "00000000-0000-0000-0000-000000000000")
            {
                parentAttributeDataListId = null;
            }

            //Add the Attribute to the data list if needed
            if (addAttributeToDataList && parentAttributeDataListId == null)
            {
                //Get default id for Type attribute
                if (nameOfAttribute == "roleType")
                {
                    if (attributeDatalistItems.Count == 0)
                    {
                        attributeDatalistItem = new DatalistItem();
                        attributeDatalistItem.Key = "U";
                        defaultAttributeDatalistItemId = attributeDatalistItem.GetDataListItemId(attributeDatalist, attributeDatalistItem);

                    }
                    else
                    {
                        defaultAttributeDatalistItemId = attributeDatalistItems[0].Id;
                    }
                }
                else if (nameOfAttribute == "isInternal")
                {
                    if (attributeDatalistItems.Count == 0)
                    {
                        attributeDatalistItem = new DatalistItem();
                        attributeDatalistItem.Key = "true";
                        defaultAttributeDatalistItemId = attributeDatalistItem.GetDataListItemId(attributeDatalist, attributeDatalistItem);

                    }
                    else
                    {
                        defaultAttributeDatalistItemId = attributeDatalistItems[0].Id;
                    }
                }
                else if (nameOfAttribute == "type")
                {
                    if (attributeDatalistItems.Count == 0)
                    {
                        attributeDatalistItem = new DatalistItem();
                        attributeDatalistItem.Key = "Page";
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
