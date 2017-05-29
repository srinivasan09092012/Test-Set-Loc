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

namespace DatalistSyncUtil
{
    public partial class AppSettingDiff : Form
    {
        public AppSettingDiff()
        {
            InitializeComponent();
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
            //this.LoadApplicationNames();            
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
            List<TenantModuleModel> modules = this.LoadHelper.LoadModules();
            modules.Insert(
                0,
                new TenantModuleModel()
                {
                    ModuleName = "---All Modules---",
                    TenantModuleId = Guid.Empty,
                    TenantId = TenantID
                });
            this.ModuleList.DataSource = modules.Where(w => w.TenantId == TenantID).GroupBy(i => i.ModuleName)
                  .Select(group =>
                        new
                        {
                            Key = group.Key,
                            Items = group.OrderByDescending(x => x.ModuleName)
                        })
                  .Select(g => g.Items.First()).OrderBy(o => o.ModuleName).ToList();
            this.ModuleList.DisplayMember = "ModuleName";
            this.ModuleList.SelectAll();
        }
        //private void LoadDelta()
        //{
        //    switch (this.DeltaType.ToUpper())
        //    {
        //        case "APPSETTINGS":
        //            this.LoadAppSettingDelta();                 
        //            break;

        //        default:
        //            break;
        //    }
        //}
        //private void LoadAppSettingDelta()
        //{
        //    List<AppSettingsModel> newAppsettings = null;          
        //    this.NewItemsView.AutoGenerateColumns = false;
        //    newAppsettings = this.GetAppsettings();            
        //    Guid tenantModuleId = (this.ModuleList.SelectedItem as TenantModuleModel).TenantModuleId;
        //    if (tenantModuleId != Guid.Empty)
        //    {
        //        newAppsettings = newAppsettings.Where(w => w.TenantModuleID == tenantModuleId).ToList();
        //    }
        //    this.NewItemsView.DataSource = new BindingList<AppSettingsModel>(newAppsettings);
        //    this.NewItemsView.EditMode = DataGridViewEditMode.EditOnEnter;

        //}
        List<TenantModuleModel> selectedModules = new List<TenantModuleModel>();

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
        private void btnListUpdate_Click(object sender, EventArgs e)
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
            PreviewPage AppSettingPreviewPage = new PreviewPage(this.NewAppsetting);
            AppSettingPreviewPage.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
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

        private void drpApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(drpApplication.SelectedIndex != 0)
            {
                Guid applicationId = (this.drpApplication.SelectedItem as ApplicationModel).ApplicationId;
                Guid tenantModuleId = (this.ModuleList.SelectedItem as TenantModuleModel).TenantModuleId;
                this.NewItemsView.AutoGenerateColumns = false;                
                List<AppSettingsModel> newhtmllists = null;
                List<AppSettingsModel> finalSource = null;
                List<AppSettingsModel> finalTarget = null;
                var modulesQuery = from source in this.SourceAppsetting                                                                    
                                   where source.TenantModuleID == tenantModuleId
                                   where source.ApplicationId == applicationId                                   
                                   where source.IsActive= true 
                                   select source;
                finalSource = modulesQuery.ToList();

                var modulesQuery1 = from target in this.TargetAppsetting
                                   where target.TenantModuleID == tenantModuleId
                                   where target.ApplicationId == applicationId
                                   where target.IsActive = true
                                   select target;
                finalTarget = modulesQuery1.ToList();

                List<string> appSettingLists = finalSource.Select(c => c.AppSettingKey.ToString()).Except(finalTarget.Select(c => c.AppSettingKey.ToString())).ToList();
                newhtmllists = finalSource.Where(c => appSettingLists.Contains(c.AppSettingKey.ToString())).ToList();
               
                newhtmllists.OrderBy(o => o.AppSettingKey).ToList();
                this.NewItemsView.DataSource = new BindingList<AppSettingsModel>(newhtmllists);
                this.NewItemsView.EditMode = DataGridViewEditMode.EditOnEnter;
            }           
            
        }
    }    
}
