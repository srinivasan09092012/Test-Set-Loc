//--------------------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
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
    class IocConfiguration
    {
        private WebRefAppSettingsMaint.AppSettingServiceClient clientLicense = new WebRefAppSettingsMaint.AppSettingServiceClient();

        public IocConfiguration()
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

        public IocConfiguration AddAppSetting(IocConfiguration iocConfiguration)
        {
            AppSettingService.AppSettingAdded response = null;

            AppSettingService.AddAppSettingCommand command = new AppSettingService.AddAppSettingCommand()
            {
                AppSetting = new AppSettingService.AppSetting()
                {
                    ApplicationId = iocConfiguration.ApplicationId,
                    AppSettingKey = AdministrationConstants.IocConfig,
                    Description = iocConfiguration.Description,
                    IsActive = true,
                    SettingTypeItemKey = AdministrationConstants.ApplicationSettingTypeKeys.Ioc,
                    TenantModuleId = iocConfiguration.TenantModuleId,
                    Value = iocConfiguration.Value
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
            catch (Exception ex)
            {
                return null;
            }

            if (response.TenantModuleAppSettingId != null)
            {
                iocConfiguration.Id = response.TenantModuleAppSettingId;
                return iocConfiguration;
            }
            else
            {
                return null;
            }
        }

        public IocConfiguration UpdateAppSetting(IocConfiguration iocConfiguration)
        {
            AppSettingService.AppSettingUpdated response = null;

            AppSettingService.UpdateAppSettingCommand command = new AppSettingService.UpdateAppSettingCommand()
            {
                AppSetting = new AppSettingService.AppSetting()
                {
                    TenantModuleAppSettingId = iocConfiguration.Id,
                    ApplicationId = iocConfiguration.ApplicationId,
                    AppSettingKey = AdministrationConstants.IocConfig,
                    Description = iocConfiguration.Description,
                    IsActive = true,
                    SettingTypeItemKey = AdministrationConstants.ApplicationSettingTypeKeys.Ioc,
                    TenantModuleId = iocConfiguration.TenantModuleId,
                    Value = iocConfiguration.Value
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
                iocConfiguration.Id = response.TenantModuleAppSettingId;
                return iocConfiguration;
            }
            else
            {
                return null;
            }
        }
    }
}
