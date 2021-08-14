//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2020. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;

namespace RightsModels
{
    public class SettingsModel : ApplicationSettingsBase
    {
        [UserScopedSetting]
        [DefaultSettingValue("false")]
        public bool OracleSourceSelected
        {
            get
            {
                if (this["OracleSourceSelected"] != null)
                {
                    return (bool)this["OracleSourceSelected"];
                }
                else
                { 
                    return false;
                }
            }
            set
            {
                this["OracleSourceSelected"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("false")]
        public bool SQLSourceSelected
        {
            get
            {
                if (this["SQLSourceSelected"] != null)
                {
                    return (bool)this["SQLSourceSelected"];
                }
                else
                {
                    return false;
                }
            }
            set
            {
                this["SQLSourceSelected"] = value;
            }
        }

        [UserScopedSetting]
        public string OracleServerName 
        {
            get
            {
                return (string)this["OracleServerName"];
            }
            set
            {
                this["OracleServerName"] = value;
            }
        }

        [UserScopedSetting]
        public string OracleSchemaName
        {
            get
            {
                return (string)this["OracleSchemaName"];
            }
            set
            {
                this["OracleSchemaName"] = value;
            }
        }

        [UserScopedSetting]
        public string OracleUserName
        {
            get
            {
                return (string)this["OracleUserName"];
            }
            set
            {
                this["OracleUserName"] = value;
            }
        }

        [UserScopedSetting]
        public string SQLServerName
        {
            get
            {
                return (string)this["SQLServerName"];
            }
            set
            {
                this["SQLServerName"] = value;
            }
        }

        [UserScopedSetting]
        public string SQLCatalog
        {
            get
            {
                return (string)this["SQLCatalog"];
            }
            set
            {
                this["SQLCatalog"] = value;
            }
        }

        [UserScopedSetting]
        public string SQLUserName 
        {
            get
            {
                return (string)this["SQLUserName"];
            }
            set
            {
                this["SQLUserName"] = value;
            }
        }

        [UserScopedSetting]
        public string TenantId 
        {
            get
            {
                return (string)this["TenantId"];
            }
            set
            {
                this["TenantId"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("")]
        public string ModuleIds
        {
            get 
            {
                return (string)this["ModuleIds"];
            }
            set 
            {
                this["ModuleIds"] = value;
            }
        }

        public List<string> ModuleIdList
        {
            get
            {
                List<string> ids = new List<string>();
                string rawIds = "" + (string)this["ModuleIds"];
                if (!string.IsNullOrEmpty(rawIds))
                {
                    ids.AddRange(rawIds.Split(','));
                }
                return ids;
            }
            set
            {
                this["ModuleIds"] = string.Join(",", value.ToArray());
            }
        }
        [UserScopedSetting]
        public string OutputFileName 
        {
            get
            {
                return (string)this["OutputFileName"];
            }
            set
            {
                this["OutputFileName"] = value;
            }
        }

        [UserScopedSetting]
        public string OutputFolder 
        {
            get
            {
                return (string)this["OutputFolder"];
            }
            set
            {
                this["OutputFolder"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("0,0")]
        public Point Location
        {
            get
            {
                return (Point)this["Location"];
            }
            set
            {
                this["Location"] = value;
            }
        }
    }
}
