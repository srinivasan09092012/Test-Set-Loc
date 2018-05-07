//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DatalistSyncUtil
{
    [Serializable]
    public class AppSettingsModel
    {
        public Guid TenantModuleAppSettingId { get; set; }

        public Guid ApplicationId { get; set; }

        public Guid TenantModuleID { get; set; }

        public string SettingTypeItemKey { get; set; }

        public string AppSettingKey { get; set; }

        public string Value { get; set; }

        public string TargetValue { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public string OperatorID { get; set; }

        public string Status { get; set; }

        public Guid TenantID { get; set; }

        public string ModuleName { get; set; }
                
        public bool EffectiveStartDateModified { get; set; }

        public bool IsEditable { get; set; }

        public DateTime? LastModifiedTimeStamp { get; set; }
    }
}
