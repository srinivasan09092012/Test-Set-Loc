﻿// Any unauthorized use in whole or in part without written consent is strictly prohibited.
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

namespace DatalistSyncUtil
{
    public partial class AppSettingDiff : Form
    {
        public AppSettingDiff()
        {
            this.InitializeComponent();
        }

        public AppSettingDiff(Guid tenantID, string type, List<AppSettingsModel> sourceAppsetting, List<AppSettingsModel> targetAppsetting)
        {
            this.InitializeComponent();
            this.TargetConnectionString = ConfigurationManager.ConnectionStrings["TargetDataList"];
            this.SourceConnectionString = ConfigurationManager.ConnectionStrings["SourceDataList"];
            this.LoadHelper = new SourceTenantHelper(this.SourceConnectionString);
            this.TenantID = tenantID;
            this.DeltaType = type;
            this.SourceAppsetting = sourceAppsetting;
            this.TargetAppsetting = targetAppsetting;
            this.LoadModules();
            drpApplication.Enabled = false;
        }

        public ConnectionStringSettings TargetConnectionString { get; set; }

        public Guid TenantID { get; set; }

        public string DeltaType { get; set; }

        public ConnectionStringSettings SourceConnectionString { get; set; }

        public SourceTenantHelper LoadHelper { get; set; }

        public List<AppSettingsModel> NewAppsetting { get; set; }

        public List<AppSettingsModel> UpdateAppsetting { get; set; }

        public List<AppSettingsModel> SourceAppsetting { get; set; }

        public List<AppSettingsModel> TargetAppsetting { get; set; }

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
            this.ModuleList.DataSource = modules.GroupBy(i => i.ModuleName)
                  .Select(group =>
                        new
                        {
                            Key = group.Key,
                            Items = group.OrderByDescending(x => x.ModuleName)
                        })
                  .Select(g => g.Items.First()).OrderBy(o => o.ModuleName).ToList();
            this.ModuleList.DisplayMember = "ModuleName";
        }

        private bool CheckUpdateItemChanged(ref AppSettingsModel t, ref AppSettingsModel targetSetting)
        {
            bool itemChanged = false;
            if (t.ModuleName == "File Transfer")
            {
            }

            if (t.LastModifiedTimeStamp.Value != targetSetting.LastModifiedTimeStamp.Value)
            {
                if (t.SettingTypeItemKey != "IOC" && t.AppSettingKey != "DefaultConnectionString")
                {
                    if (t.Value != targetSetting.Value && t.AppSettingKey == targetSetting.AppSettingKey)
                    {
                        itemChanged = true;
                        t.EffectiveStartDateModified = targetSetting.EffectiveStartDateModified = true;
                    }
                }
            }

            return itemChanged;
        }

        private void BtnListUpdate_Click(object sender, EventArgs e)
        {
            this.UpdateAppsetting = null;
            bool selected = false;
            this.NewAppsetting = new List<AppSettingsModel>();
            foreach (DataGridViewRow row in this.NewItemsView.Rows)
            {
                selected = Convert.ToBoolean(row.Cells[0].Value);

                if (selected)
                {
                    this.NewAppsetting.Add(row.DataBoundItem as AppSettingsModel);
                }
            }
        }

        private void ModuleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid moduleId = (this.ModuleList.SelectedItem as TenantModuleModel).ModuleId;

            List<ApplicationModel> finalapplication = null;
            List<ApplicationModel> application = this.LoadHelper.LoadApplicationName();

            var modulesQuery = from lists in application
                               where (lists.ModuleId == moduleId || lists.ModuleId == null)
                               select lists;
            finalapplication = modulesQuery.ToList();

            finalapplication.Insert(
                   0,
                   new ApplicationModel()
                   {
                       ApplicationName = "---All Applications---"
                   });
            this.drpApplication.DataSource = finalapplication.GroupBy(i => i.ApplicationName)
                  .Select(group =>
                        new
                        {
                            Key = group.Key,
                            Items = group.OrderByDescending(x => x.ApplicationName)
                        })
                  .Select(g => g.Items.First()).OrderBy(o => o.ApplicationName).ToList();

            this.drpApplication.DisplayMember = "ApplicationName";
            this.drpApplication.SelectAll();
        }

        private void PreviewUpdate_Click(object sender, EventArgs e)
        {
            if (this.NewAppsetting == null || this.NewAppsetting.Count == 0)
            {
                MessageBox.Show("Error:Please include some rows before preview screen");
                return;
            }

            PreviewPage appSettingPreviewPage = new PreviewPage(this.NewAppsetting);
            appSettingPreviewPage.ShowDialog();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewItemsSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.NewItemsView.Rows)
            {
                row.Cells[0].Value = this.NewItemsSelectAllCB.Checked;
            }
        }

        private void UpdateSelectAllCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.NewItemsView.Rows)
            {
                row.Cells[0].Value = this.NewItemsSelectAllCB.Checked;
            }
        }

        private void DrpApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
                if ((this.ModuleList.SelectedItem as TenantModuleModel).ModuleName != "---All Modules---")
                {
                    drpApplication.Enabled = true;
                }
                else
                {
                    drpApplication.Enabled = false;
                }
                    
                Guid applicationId = (this.drpApplication.SelectedItem as ApplicationModel).ApplicationId;
                Guid tenantModuleId = (this.ModuleList.SelectedItem as TenantModuleModel).TenantModuleId;
                this.NewItemsView.AutoGenerateColumns = false;                
                List<AppSettingsModel> newhtmllists = null;
                List<AppSettingsModel> finalSource = null;
                List<AppSettingsModel> finalTarget = null;
                Guid tenantModuleIDTarget = this.GetTenantModuleTarget((this.ModuleList.SelectedItem as TenantModuleModel).ModuleName);
                Guid tenantAppIDTarget = this.GetAppIDTarget((this.drpApplication.SelectedItem as ApplicationModel).ApplicationName);
                var modulesQuery = from source in this.SourceAppsetting                                                                    
                                   where source.TenantModuleID == tenantModuleId
                                   where source.ApplicationId == applicationId                                   
                                   where source.IsActive = true 
                                   select source;
                finalSource = modulesQuery.ToList();

                if (this.TargetAppsetting != null)
                 {
                    var modulesQuery1 = from target in this.TargetAppsetting
                                        where target.TenantModuleID == tenantModuleIDTarget
                                        where target.ApplicationId == applicationId    
                                        where target.IsActive = true
                                        select target;
                    finalTarget = modulesQuery1.ToList();
                }              

                List<string> appSettingLists = finalSource.Select(c => c.AppSettingKey.ToString()).Except(finalTarget.Select(c => c.AppSettingKey.ToString())).ToList();
                newhtmllists = finalSource.Where(c => appSettingLists.Contains(c.AppSettingKey.ToString())).ToList();
               
                newhtmllists.OrderBy(o => o.AppSettingKey).ToList();
                this.NewItemsView.DataSource = new BindingList<AppSettingsModel>(newhtmllists);
                this.NewItemsView.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private Guid GetAppIDTarget(string applicationName)
        {
            TenantHelper targetHelper = new TenantHelper(this.TargetConnectionString);
            List<ApplicationModel> application = targetHelper.LoadApplicationName();
            List<TenantModuleModel> modules = targetHelper.LoadModules(this.TenantID);
            Guid moduleID = new Guid();
            if ((this.ModuleList.SelectedItem as TenantModuleModel).ModuleName != "---All Modules---")
            {
                moduleID = modules.Find(x => x.ModuleName == (this.ModuleList.SelectedItem as TenantModuleModel).ModuleName).ModuleId;
            }

            Guid applicationID = new Guid();
            if (applicationName != "---All Applications---")
            {
                ApplicationModel filterapp = new ApplicationModel();
                filterapp = application.Find(w => w.ApplicationName == applicationName && w.ModuleId == moduleID);

                if (filterapp == null)
                {
                    filterapp = application.Find(w => w.ApplicationName == applicationName);
                }

                applicationID = filterapp.ApplicationId;
            }

            return applicationID;
        }

        private Guid GetTenantModuleTarget(string moduleName)
        {
            TenantHelper targetHelper = new TenantHelper(this.TargetConnectionString);
            List<TenantModuleModel> modules = targetHelper.LoadModules(this.TenantID);
            Guid tenantModuleID = new Guid();
            if (moduleName != "---All Modules---")
            {
                 tenantModuleID = modules.Find(w => w.TenantId == this.TenantID && w.ModuleName == moduleName).TenantModuleId;
            }

            return tenantModuleID;
        }
    }    
}
