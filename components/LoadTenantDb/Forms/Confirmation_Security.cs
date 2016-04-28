//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Utilities.LoadTenantDb.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

            if (MainForm.SecurityFunctions.Count > 0 &&
                MainForm.SecurityRoles.Count > 0 &&
                MainForm.SecurityRights.Count > 0)
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
                for (int i = 0; i < MainForm.SecurityRoles[0].SecurityNodeAttribute.Count; i++)
                {
                    this.roleAttributesListBox.Items.Add(MainForm.SecurityRoles[0].SecurityNodeAttribute[i].Name);
                    this.roleAttributesListBox.SetItemChecked(i, true);

                    lastCol = this.rolesGridView.ColumnCount;
                    this.rolesGridView.ColumnCount++;
                    this.rolesGridView.Columns[lastCol].Name = MainForm.SecurityRoles[0].SecurityNodeAttribute[i].Name;
                }
            }

            if (MainForm.SecurityFunctions.Count > 0)
            {
                for (int i = 0; i < MainForm.SecurityFunctions[0].SecurityNodeAttribute.Count; i++)
                {
                    this.functionAttributesListBox.Items.Add(MainForm.SecurityFunctions[0].SecurityNodeAttribute[i].Name);
                    this.functionAttributesListBox.SetItemChecked(i, true);

                    lastCol = this.functionsGridView.ColumnCount;
                    this.functionsGridView.ColumnCount++;
                    this.functionsGridView.Columns[lastCol].Name = MainForm.SecurityFunctions[0].SecurityNodeAttribute[i].Name;
                }
            }

            if (MainForm.SecurityRights.Count > 0)
            {
                for (int i = 0; i < MainForm.SecurityRights[0].SecurityNodeAttribute.Count; i++)
                {
                    this.rightAttributesListBox.Items.Add(MainForm.SecurityRights[0].SecurityNodeAttribute[i].Name);
                    this.rightAttributesListBox.SetItemChecked(i, true);

                    lastCol = this.rightsGridView.ColumnCount;
                    this.rightsGridView.ColumnCount++;
                    this.rightsGridView.Columns[lastCol].Name = MainForm.SecurityRights[0].SecurityNodeAttribute[i].Name;
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
                row.Add(MainForm.SecurityFunctions[i].Link);

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
                row.Add(MainForm.SecurityRights[i].Link);

                for (int j = 0; j < MainForm.SecurityRights[i].SecurityNodeAttribute.Count; j++)
                {
                    row.Add(MainForm.SecurityRights[i].SecurityNodeAttribute[j].Value);
                }

                this.rightsGridView.Rows.Add(row.ToArray());
            }
        }

        private void LoadSecurityRoles(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            int loadDatalistItemSuccessful = 0;
            int loadErrors = 0;
            bool updated = false;
            string parentDatalistItemId = null;
            string parentRoleDatalistId = null;

            Datalist datalist = new Datalist();
            datalist.MainForm = this.MainForm;

            datalist.TenantId = MainForm.TenantId;
            datalist.TenantModuleId = "1F50602EACA1B650E053C9157ACEFB50";
            datalist.IdentifierId = "USER1";
            datalist.IsActive = true;

            datalist.ContentId = "Core.DataList.SecurityRoles";
            datalist.Id = datalist.GetDataList(datalist);
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

            parentRoleDatalistId = datalist.Id;

            datalist.ContentId = "Core.DataList.SecurityFunctions";
            datalist.Id = datalist.GetDataList(datalist);
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

            datalist.ContentId = "Core.DataList.SecurityRights";
            datalist.Id = datalist.GetDataList(datalist);
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

            for (int i = 0; i < MainForm.SecurityRoles.Count; i++)
            {
                if (MainForm.SecurityRoles[i].ContentId != "<< NULL OR MISSING VALUE >>")
                {
                    DatalistItem datalistItem = new DatalistItem();
                    datalistItem.MainForm = this.MainForm;
                    datalistItem.DataListItemLanguages.Add(new DatalistItemLanguage());

                    datalistItem.ContentId = this.MainForm.SecurityRoles[i].ContentId;
                    datalistItem.Key = this.MainForm.SecurityRoles[i].Name;
                    datalistItem.Id = datalistItem.GetDataListItem(datalist, datalistItem);
                    datalistItem.DataListId = datalist.Id;
                    datalistItem.TenantId = this.MainForm.TenantId;
                    datalistItem.IdentifierId = "USER1";
                    datalistItem.IsActive = true;
                    datalistItem.OrderIndex = 0;
                    datalistItem.DataListItemLanguages[0].Locale = "en-us";
                    datalistItem.DataListItemLanguages[0].Description = this.MainForm.SecurityRoles[i].Name;
                    datalistItem.DataListItemLanguages[0].IsActive = true;
                    datalistItem.DataListItemLanguages[0].DataListItemId = datalistItem.Id;

                    if (datalistItem.Id == null)
                    {
                        try
                        {
                            datalistItem = datalistItem.AddDataListItem(datalistItem);
                            loadDatalistItemSuccessful++;
                        }
                        catch
                        {
                            datalistItem = null;
                            loadErrors++;
                        }

                        if (datalistItem != null)
                        {
                            this.MainForm.SecurityRoles[i].Action = "Added";
                            loadDatalistItemSuccessful++;
                        }
                        else
                        {
                            this.MainForm.SecurityRoles[i].Action = "Add Error";
                            loadErrors++;
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
                            this.MainForm.SecurityRoles[i].Action = "Updated";
                            loadDatalistItemSuccessful++;
                        }
                        else
                        {
                            this.MainForm.SecurityRoles[i].Action = "Update Error";
                            loadErrors++;
                        }
                    }

                    parentDatalistItemId = datalistItem.Id;

                    for (int j = 0; j < this.roleAttributesListBox.Items.Count; j++)
                    {
                        if (this.roleAttributesListBox.GetItemChecked(j))
                        {
                            datalist.ContentId = "Core.DataList.Attributes." + this.roleAttributesListBox.Items[i].ToString();
                            datalist.Id = datalist.GetDataList(datalist);
                            datalist.Name = this.roleAttributesListBox.Items[i].ToString() + " Attribute";
                            datalist.Description = this.roleAttributesListBox.Items[i].ToString() + " Attribute";

                            if (datalist.Id == null)
                            {
                                try
                                {
                                    datalist = datalist.AddDataList(datalist);
                                }
                                catch
                                {
                                    datalist = null;
                                }
                            }

                            datalistItem.ContentId = datalist.ContentId;
                            datalistItem.Key = this.MainForm.SecurityRoles[j].Name;
                            datalistItem.Id = datalistItem.GetDataListItem(datalist, datalistItem);
                            datalistItem.DataListId = datalist.Id;
                            datalistItem.TenantId = this.MainForm.TenantId;
                            datalistItem.IdentifierId = "USER1";
                            datalistItem.IsActive = true;
                            datalistItem.OrderIndex = 0;
                            datalistItem.DataListItemLanguages[0].Locale = "en-us";
                            datalistItem.DataListItemLanguages[0].Description = this.MainForm.SecurityRoles[j].Name;
                            datalistItem.DataListItemLanguages[0].IsActive = true;
                            datalistItem.DataListItemLanguages[0].DataListItemId = datalistItem.Id;

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
                            }

                            datalistItem.DataListItemAttributeValues.Add(new DatalistItemAttributeValue());
                            datalistItem.DataListItemAttributeValues[0].DataListAttributeId = datalist.Id;
                            datalistItem.DataListItemAttributeValues[0].DataListsAttributeValueId = parentRoleDatalistId;
                            datalistItem.DataListItemAttributeValues[0].DataListAttributeText = "";
                            datalistItem.DataListItemAttributeValues[0].DataListsItemId = datalistItem.Id;
                            datalistItem.DataListItemAttributeValues[0].DataListsItemValueId = parentDatalistItemId;
                            datalistItem.DataListItemAttributeValues[0].DataListsItemValueText = "";

                            try
                            {
                                updated = datalistItem.UpdateDataListItem(datalistItem);
                            }
                            catch
                            {
                                updated = false;
                            }
                        }
                    }

                }
            }
        }
 
   }
}