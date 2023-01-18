//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using DatalistSyncUtil.Views;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DatalistSyncUtil.Views
{
    public partial class ServicesDiff : Form
    {
        public ServicesDiff()
        {
            this.InitializeComponent();
        }

        public ServicesDiff(Guid tenantID, string type, List<ServicesMainModel> sourceList, List<ServicesMainModel> targetList)
        {
            this.InitializeComponent();
            this.TargetConnectionString = ConfigurationManager.ConnectionStrings["TargetDataList"];
            this.LoadHelper = new TenantHelper(this.TargetConnectionString);
            this.TenantID = tenantID;
            this.DeltaType = type;
            this.SourceLoadHelper = new SourceTenantHelper();
            this.SourceServicesList = sourceList;
            this.TargetServicesList = targetList;
            this.TargetSecItems = this.LoadHelper.GetSecRightsAndLabels(this.TenantID);
            this.SourceSecItems = this.SourceLoadHelper.GetSecRightsAndLabels(this.TenantID);
            this.LoadModules();
            this.LoadDelta();
        }

        public TenantHelper LoadHelper { get; set; }

        public ConnectionStringSettings TargetConnectionString { get; set; }

        public Guid TenantID { get; set; }

        public string DeltaType { get; set; }

        public List<CodeListModel> TargetSecItems { get; set; }

        public List<CodeListModel> SourceSecItems { get; set; }

        public List<ServicesMainModel> SourceServicesList { get; set; }

        public SourceTenantHelper SourceLoadHelper { get; set; }

        public List<ServicesMainModel> TargetServicesList { get; set; }

        public List<ServicesMainModel> UpdateNewService { get; set; }

        public List<ServicesMainModel> UpdatedTargetService { get; set; }

        public List<ServicesMainModel> UpdateServices { get; set; }

        private void LoadModules()
        {
            List<TenantModuleModel> modules = this.LoadHelper.LoadModules(this.TenantID);
            modules.Insert(
                   0,
                   new TenantModuleModel()
                   {
                       ModuleName = "---All Modules---",
                       TenantModuleId = Guid.Empty,
                       TenantId = this.TenantID
                   });
            this.moduleList.DataSource = modules.GroupBy(i => i.ModuleName)
                  .Select(group =>
                        new
                        {
                            Key = group.Key,
                            Items = group.OrderByDescending(x => x.ModuleName)
                        })
                  .Select(g => g.Items.First()).OrderBy(o => o.ModuleName).ToList();
            this.moduleList.DisplayMember = "ModuleName";
            this.moduleList.SelectAll();
        }

        private void LoadDelta()
        {
            switch (this.DeltaType.ToUpper())
            {
                case "SERVICES":
                    this.LoadServicesDelta();
                    this.diffServices.SelectedTab = this.newSvc;
                    break;

                default:
                    break;
            }
        }

        private void LoadServicesDelta()
        {
            List<ServicesMainModel> newServicelists = null;
            this.newServicesView.AutoGenerateColumns = false;
            newServicelists = this.GetNewServiceslist();
            Guid tenantModuleId = (this.moduleList.SelectedItem as TenantModuleModel).TenantModuleId;

            if (tenantModuleId != Guid.Empty)
            {
                newServicelists = newServicelists.Where(w => w.TenantModuleID == tenantModuleId).ToList();
            }

            this.newServicesView.DataSource = new BindingList<ServicesMainModel>(newServicelists);
            this.LoadUpdatedServices();
        }

        private List<ServicesMainModel> GetNewServiceslist()
        {
            List<string> servicesLists = this.SourceServicesList.Select(c => c.Name).Except(this.TargetServicesList.Select(c => c.Name)).ToList();
            List<ServicesMainModel> newServiceslists = this.SourceServicesList.Where(c => servicesLists.Contains(c.Name)).ToList();
            newServiceslists.ForEach(i =>
            {
                i.Status = "NEW";
            });
            return newServiceslists.OrderBy(o => o.Name).ToList();
        }

        private void LoadUpdatedServices()
        {
            List<ServicesMainModel> updateSourceServices = new List<ServicesMainModel>();
            List<ServicesMainModel> updateTargetServices = new List<ServicesMainModel>();
            List<ServicesMainModel> updatedServicesists = new List<ServicesMainModel>();
            ServicesMainModel sourceServicelist = null;
            ServicesMainModel targetServicelist = null;
            CodeListModel sourceSecurity = null;
            CodeListModel targetSecurity = null;
            string sourceSecurityRight = null;
            string targetSecurityRight = null;

            bool isChanged = false;
            List<string> services = this.SourceServicesList.Select(c => c.Name).Intersect(this.TargetServicesList.Select(c => c.Name)).ToList();
            services.ForEach(f =>
            {
                isChanged = false;
                sourceServicelist = this.SourceServicesList.Find(e => e.Name == f);
                targetServicelist = this.TargetServicesList.Find(e => e.Name == f);

                if (sourceServicelist.BaseURL != targetServicelist.BaseURL)
                {
                    isChanged = true;
                    sourceServicelist.BaseURLModified = true;
                }

                if (sourceServicelist.SecurityRightItemContentID != targetServicelist.SecurityRightItemContentID)
                {
                    isChanged = true;
                    sourceServicelist.SecRightItemModified = true;
                }

                if (sourceServicelist.LabelItemKey != targetServicelist.LabelItemKey)
                {
                    isChanged = true;
                    sourceServicelist.LabelItemModified = true;
                }

                if (sourceServicelist.IOCContainer != targetServicelist.IOCContainer)
                {
                    isChanged = true;
                    sourceServicelist.IOCContainerModified = true;
                }

                if (sourceServicelist.DefaultText != targetServicelist.DefaultText)
                {
                    isChanged = true;
                    sourceServicelist.DefaultTextModified = true;
                }

                if (sourceServicelist.IsActive != targetServicelist.IsActive)
                {
                    isChanged = true;
                    sourceServicelist.IsActiveModified = true;
                }

                if (isChanged)
                {
                    sourceServicelist.Status = "UPDATE";
                    sourceServicelist.ServiceID = targetServicelist.ServiceID;
                    updateSourceServices.Add(sourceServicelist);
                    updateTargetServices.Add(targetServicelist);
                }
            });

            this.SourceServices.AutoGenerateColumns = this.TargetServices.AutoGenerateColumns = false;
            Guid tenantModuleId = (this.moduleList.SelectedItem as TenantModuleModel).TenantModuleId;

            if (tenantModuleId != Guid.Empty)
            {
                updateSourceServices = updateSourceServices.Where(w => w.TenantModuleID == tenantModuleId).ToList();
                updateTargetServices = updateTargetServices.Where(w => w.TenantModuleID == tenantModuleId).ToList();
            }

            this.UpdateNewService = updateSourceServices;
            this.UpdatedTargetService = updateTargetServices;
            this.SourceServices.DataSource = new BindingList<ServicesMainModel>(updateSourceServices.OrderBy(o => o.Name).ToList());
            this.TargetServices.DataSource = new BindingList<ServicesMainModel>(updateTargetServices.OrderBy(o => o.Name).ToList());
        }

        private void ModuleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadServicesDelta();
        }

        private bool CheckIfLabelPresentAtTarget(ServicesMainModel service, bool selectAll)
        {
            bool present = true;
            string lbl = null;
            List<string> list = new List<string>();

            if (service.LabelItemKey != null)
            {
                present = this.TargetSecItems.Any(a => a.Code == service.LabelItemKey);
                if (present == false && selectAll == false)
                {
                    lbl = lbl + service.LabelItemKey;
                    MessageBox.Show("The Label: " + lbl + " is not present in the Target Environment", "Label", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    present = false;
                }

                if (present == false && selectAll == true)
                {
                    lbl = lbl + service.LabelItemKey;
                    present = false;
                }
            }

            return present;
        }

        private bool CheckIfSecRightPresentAtTarget(ServicesMainModel service, bool selectAll)
        {
            bool present = true;
            string right = null;
            List<string> list = new List<string>();

            if (service.LabelItemKey != null)
            {
                present = this.TargetSecItems.Any(a => a.Code == service.LabelItemKey);
                if (present == false && selectAll == false)
                {
                    right = right + service.SecurityRightItemContentID;
                    MessageBox.Show("The Security Right: " + right + " is not present in the Target Environment", "Security Right", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    present = false;
                }

                if (present == false && selectAll == true)
                {
                    right = right + service.SecurityRightItemContentID;
                    present = false;
                }
            }

            return present;
        }

        private void SelectColumn_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            bool selectAll = false;
            int rowIndex = e.RowIndex;
            DataGridViewRow row = newServicesView.Rows[rowIndex];
            bool secRightPresent = this.CheckIfSecRightPresentAtTarget(row.DataBoundItem as ServicesMainModel, selectAll);
            bool labelPresent = this.CheckIfLabelPresentAtTarget(row.DataBoundItem as ServicesMainModel, selectAll);
            if (secRightPresent == false || labelPresent == false)
            {
                newServicesView.Rows[rowIndex].ReadOnly = true;
                newServicesView.Rows[rowIndex].Cells[0].Selected = false;
                newServicesView.Rows[rowIndex].Cells[0].Value = false;
            }
        }

        private void SelectUpdateServices_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            bool selectAll = false;
            int rowIndex = e.RowIndex;
            DataGridViewRow row = SourceServices.Rows[rowIndex];
            bool secRightPresent = this.CheckIfSecRightPresentAtTarget(row.DataBoundItem as ServicesMainModel, selectAll);
            bool labelPresent = this.CheckIfLabelPresentAtTarget(row.DataBoundItem as ServicesMainModel, selectAll);
            if (secRightPresent == false || labelPresent == false)
            {
                SourceServices.Rows[rowIndex].ReadOnly = true;
                SourceServices.Rows[rowIndex].Cells[0].Selected = false;
                SourceServices.Rows[rowIndex].Cells[0].Value = false;
            }
        }

        private void NewServicesSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            if (this.NewServicesSelectAllCB.Checked)
            {
                bool selectAll = true;
                bool showMessage = false;
                foreach (DataGridViewRow row in this.newServicesView.Rows)
                {
                    bool secRightPresent = this.CheckIfSecRightPresentAtTarget(row.DataBoundItem as ServicesMainModel, selectAll);
                    bool labelPresent = this.CheckIfLabelPresentAtTarget(row.DataBoundItem as ServicesMainModel, selectAll);
                    if (secRightPresent == false || labelPresent == false)
                    {
                        row.ReadOnly = true;
                        row.Cells[0].Selected = false;
                        row.Cells[0].Value = false;
                        showMessage = true;
                    }
                    else
                    {
                        row.Cells[0].Value = true;
                    }
                }

                if (showMessage)
                {
                    MessageBox.Show("Only the records having the SecurityRight and LabelItem in the Target Environment are selected", "Security Right", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                foreach (DataGridViewRow row in this.newServicesView.Rows)
                {
                    row.Cells[0].Value = false;
                }
            }
        }

        private void UpdateServicesSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            if (this.UpdateServicesSelectAllCB.Checked)
            {
                bool selectAll = true;
                bool showMessage = false;
                foreach (DataGridViewRow row in this.SourceServices.Rows)
                {
                    bool secRightPresent = this.CheckIfSecRightPresentAtTarget(row.DataBoundItem as ServicesMainModel, selectAll);
                    bool labelPresent = this.CheckIfLabelPresentAtTarget(row.DataBoundItem as ServicesMainModel, selectAll);
                    if (secRightPresent == false || labelPresent == false)
                    {
                        row.ReadOnly = true;
                        row.Cells[0].Selected = false;
                        row.Cells[0].Value = false;
                        showMessage = true;
                    }
                    else
                    {
                        row.Cells[0].Value = true;
                    }
                }

                if (showMessage)
                {
                    MessageBox.Show("Only the records having the SecurityRight and LabelItem in the Target Environment are selected", "Security Right", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                foreach (DataGridViewRow row in this.SourceServices.Rows)
                {
                    row.Cells[0].Value = false;
                }
            }
        }

        private void BtnIncludeServices_Click(object sender, EventArgs e)
        {
            bool selected = false;
            this.UpdateServices = new List<ServicesMainModel>();
            this.CheckForCheckedValue();
            foreach (DataGridViewRow row in this.newServicesView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells["Select"].Value);

                if (selected)
                {
                    this.UpdateServices.Add(row.DataBoundItem as ServicesMainModel);
                }
            }

            foreach (DataGridViewRow row in this.SourceServices.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.UpdateServices.Add(row.DataBoundItem as ServicesMainModel);
                }
            }
        }

        private void CheckForCheckedValue()
        {
            bool noNewRowsSelected = true;
            bool noSvcRowsSelected = true;

            if (this.newServicesView.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in this.newServicesView.Rows)
                {
                    bool temp = Convert.ToBoolean(row.Cells[0].Value);
                    if (temp != false)
                    {
                        noNewRowsSelected = false;
                        break;
                    }
                }
            }

            if (this.SourceServices.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in this.SourceServices.Rows)
                {
                    bool temp = Convert.ToBoolean(row.Cells[0].Value);
                    if (temp != false)
                    {
                        noSvcRowsSelected = false;
                        break;
                    }
                }
            }

            if (noNewRowsSelected == true && noSvcRowsSelected == true)
            {
                MessageBox.Show("You must select one or more rows");
            }
        }

        private void SourceServices_Scroll(object sender, ScrollEventArgs e)
        {
            this.TargetServices.FirstDisplayedScrollingRowIndex = this.SourceServices.FirstDisplayedScrollingRowIndex;
        }

        private void TargetServices_Scroll(object sender, ScrollEventArgs e)
        {
            this.SourceServices.FirstDisplayedScrollingRowIndex = this.TargetServices.FirstDisplayedScrollingRowIndex;
        }

        private void SourceServices_RowAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in this.SourceServices.Rows)
            {
                bool secRightModified = row.Cells[7].Value != null ? Convert.ToBoolean(row.Cells[7].Value) : false;

                if (secRightModified == true)
                {
                    row.Cells[2].Style.BackColor = Color.LightBlue;
                    row.Cells[2].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[2].Style.BackColor = Color.White;
                    row.Cells[2].Style.ForeColor = Color.Black;
                }

                bool labelItemModified = row.Cells[8].Value != null ? Convert.ToBoolean(row.Cells[8].Value) : false;

                if (labelItemModified == true)
                {
                    row.Cells[3].Style.BackColor = Color.LightBlue;
                    row.Cells[3].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[3].Style.BackColor = Color.White;
                    row.Cells[3].Style.ForeColor = Color.Black;
                }

                bool defaultTextModified = row.Cells[9].Value != null ? Convert.ToBoolean(row.Cells[9].Value) : false;

                if (defaultTextModified == true)
                {
                    row.Cells[4].Style.BackColor = Color.LightBlue;
                    row.Cells[4].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[4].Style.BackColor = Color.White;
                    row.Cells[4].Style.ForeColor = Color.Black;
                }

                bool baseURLModified = row.Cells[10].Value != null ? Convert.ToBoolean(row.Cells[10].Value) : false;

                if (baseURLModified == true)
                {
                    row.Cells[5].Style.BackColor = Color.LightBlue;
                    row.Cells[5].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[5].Style.BackColor = Color.White;
                    row.Cells[5].Style.ForeColor = Color.Black;
                }

                bool iocContainerModified = row.Cells[11].Value != null ? Convert.ToBoolean(row.Cells[11].Value) : false;

                if (iocContainerModified == true)
                {
                    row.Cells[6].Style.BackColor = Color.LightBlue;
                    row.Cells[6].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[6].Style.BackColor = Color.White;
                    row.Cells[6].Style.ForeColor = Color.Black;
                }

                bool isActiveModified = row.Cells[12].Value != null ? Convert.ToBoolean(row.Cells[12].Value) : false;

                if (isActiveModified == true)
                {
                    row.Cells[13].Style.BackColor = Color.LightBlue;
                    row.Cells[13].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[13].Style.BackColor = Color.White;
                    row.Cells[13].Style.ForeColor = Color.Black;
                }
            }
        }

        private void PreviewUpdate_Click(object sender, EventArgs e)
        {
            if (this.UpdateServices == null || this.UpdateServices.Count() == 0)
            {
                MessageBox.Show("Error:Please include some rows before moving to preview screen");
                return;
            }

            PreviewPage previewPage = new PreviewPage(this.UpdateServices);
            previewPage.ShowDialog();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
