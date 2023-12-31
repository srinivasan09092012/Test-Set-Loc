//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
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
            Guid appSettingId = Guid.NewGuid();

            if (cmd.TenantModuleAppSettingId != Guid.Empty)
            {
                appSettingId = cmd.TenantModuleAppSettingId;
            }

            this.Context.AppSettings.Add(new AppSetting()
            {
                TenantModuleAppSettingId = appSettingId,
                ApplicationId = cmd.ApplicationId,
                AppSettingKey = cmd.AppSettingKey,
                SettingTypeItemKey = cmd.SettingTypeItemKey,    
                Description = cmd.Description,
                LastModifiedTimeStamp = DateTime.UtcNow,
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
