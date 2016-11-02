
//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using HP.HSP.UA3.Administration.UX.Common;
using HP.HSP.UA3.Utilities.LoadTenantDb.Forms;
using System;
using WebRefAppSettingsMaint = HP.HSP.UA3.Utilities.LoadTenantDb.AppSettingService;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Data
{
    public class AppSetting
    {
        private WebRefAppSettingsMaint.AppSettingServiceClient clientLicense = new WebRefAppSettingsMaint.AppSettingServiceClient();

        public AppSetting()
        {
        }

        public MainForm MainForm { get; set; }

        public string Id { get; set; }

        public string ApplicationId { get; set; }

        public string AppSettingKey { get; set; }

        public string Description { get; set; }

        public string SettingTypeItemKey { get; set; }
 
        public string Value { get; set; }

        public bool IsActive { get; set; }

        public string TenantModuleId { get; set; }

        public AppSetting AddAppSetting(AppSetting appSetting)
        {
            AppSettingService.AppSettingAdded response = null;

            AppSettingService.AddAppSettingCommand command = new AppSettingService.AddAppSettingCommand()
            {
                AppSetting = new AppSettingService.AppSetting()
                {
                    ApplicationId = appSetting.ApplicationId,
                    AppSettingKey = appSetting.AppSettingKey,
                    Description = appSetting.Description,
                    IsActive = true,
                    SettingTypeItemKey = AdministrationConstants.ApplicationSettingTypeKeys.App,
                    TenantModuleId = appSetting.TenantModuleId,
                    Value = appSetting.Value
                },
                Requestor = new Core.BAS.CQRS.UserMeta.RequestorModel()
                {
                    IdentifierId = "User1",
                    IdentifierIdType = Core.BAS.CQRS.UserMeta.CoreEnumerations.Messaging.IdentifierIdType.User,
                    TenantId = this.MainForm.TenantId,
                    RequestDate = DateTime.UtcNow
                }
            };

            try
            {
                response = this.clientLicense.AddAppSetting(command);
            }
            catch(Exception ex)
            {
                return null;
            }

            if (response.TenantModuleAppSettingId != null)
            {
                appSetting.Id = response.TenantModuleAppSettingId;
                return appSetting;
            }
            else
            {
                return null;
            }
        }

        public AppSetting UpdateAppSetting(AppSetting appSetting)
        {
            AppSettingService.AppSettingUpdated response = null;

            AppSettingService.UpdateAppSettingCommand command = new AppSettingService.UpdateAppSettingCommand()
            {
                AppSetting = new AppSettingService.AppSetting()
                {
                    TenantModuleAppSettingId = appSetting.Id,
                    ApplicationId = appSetting.ApplicationId,
                    AppSettingKey = appSetting.AppSettingKey,
                    Description = appSetting.Description,
                    IsActive = true,
                    SettingTypeItemKey = AdministrationConstants.ApplicationSettingTypeKeys.App,
                    TenantModuleId = appSetting.TenantModuleId,
                    Value = appSetting.Value
                },
                Requestor = new Core.BAS.CQRS.UserMeta.RequestorModel()
                {
                    IdentifierId = "User1",
                    IdentifierIdType = Core.BAS.CQRS.UserMeta.CoreEnumerations.Messaging.IdentifierIdType.User,
                    TenantId = this.MainForm.TenantId,
                    RequestDate = DateTime.UtcNow
                }
            };

            try
            {
                response = this.clientLicense.UpdateAppSetting(command);
            }
            catch (Exception ex)
            {
                return null;
            }

            if (response.TenantModuleAppSettingId != null)
            {
                appSetting.Id = response.TenantModuleAppSettingId;
                return appSetting;
            }
            else
            {
                return null;
            }
        }
    }
}