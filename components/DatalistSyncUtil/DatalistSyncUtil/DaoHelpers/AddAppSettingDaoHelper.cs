//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess.Entities;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;

namespace DatalistSyncUtil.DaoHelpers
{
    public class AddAppSettingDaoHelper
    {
        public AddAppSettingDaoHelper(DataListsDbContext context)
        {
            this.Context = context;
        }

        public DataListsDbContext Context
        {
            get; set;
        }

        public bool ExecuteProcedure(AppSettingsModel cmd)
        {           
            this.Context.AppSettings.Add(new AppSetting()
            {
                ApplicationId = cmd.ApplicationId,
                AppSettingKey = cmd.AppSettingKey,
                SettingTypeItemKey = cmd.SettingTypeItemKey,   
                Description = cmd.Description,             
                TenantModuleAppSettingId = cmd.TenantModuleAppSettingId,
                Value = cmd.TargetValue,                
                IsActive = cmd.IsActive,
                OperatorID = cmd.OperatorID,
                TenantModuleID = cmd.TenantModuleID
            });

            this.Context.SaveChanges();

            return true;
        }
    }
}
