//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------

using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatalistSyncUtil.DaoHelpers
{
    public class SearchAppSettingsDaoHelper
    {
        public SearchAppSettingsDaoHelper(DataListsDbContext context)
        {
            this.DataListItemContext = context;
        }       

        public DataListsDbContext DataListItemContext { get; set; }

        public List<AppSettingsModel> ExecuteProcedure(Guid tenantID)
        {            
            var results = from tenanantModule in this.DataListItemContext.TenantModules
                          join appSetting_List in this.DataListItemContext.AppSettings on tenanantModule.TenantModuleId equals appSetting_List.TenantModuleID
                          join module in this.DataListItemContext.Modules on tenanantModule.ModuleId equals module.ModuleId
                          join application in this.DataListItemContext.Applications on appSetting_List.ApplicationId equals application.ApplicationId
                          join tenant in this.DataListItemContext.Tenants on tenanantModule.TenantId equals tenant.TenantID
                          where appSetting_List.IsActive == true
                            && tenant.TenantID.Equals(tenantID)
                          select new AppSettingsModel()
                          {
                              TenantModuleAppSettingId = appSetting_List.TenantModuleAppSettingId,
                              ApplicationId = appSetting_List.ApplicationId,
                              TenantModuleID = appSetting_List.TenantModuleID,
                              SettingTypeItemKey = appSetting_List.SettingTypeItemKey,                             
                              AppSettingKey = appSetting_List.AppSettingKey,
                              TenantID = tenant.TenantID,
                              ModuleName = module.ModuleName,
                              Value = appSetting_List.Value,
                              Description = appSetting_List.Description,
                              IsActive = appSetting_List.IsActive,
                              OperatorID = appSetting_List.OperatorID,
                              LastModifiedTimeStamp = appSetting_List.LastModifiedTimeStamp
                          };

            return results.ToList();
        }
    }
}
