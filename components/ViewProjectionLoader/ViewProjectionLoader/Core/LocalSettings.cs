//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Domain;
using System;
using System.Configuration;

namespace ViewProjectionLoader.Core
{
    public class LocalSettings
    {
        private static Lazy<string> tenantIdLoader;

        static LocalSettings()
        {
            tenantIdLoader = new Lazy<string>(GetTenantId);
        }

        private static string GetTenantId()
        {
            return ConfigurationManager.AppSettings["TenantId"];
        }

        public static ConnectionStringSettings TenantConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["TenantConfig"];
            }
        }

        public static string TenantSchemaName
        {
            get
            {
                return DbContextBase.GetConfiguredSchemaName(typeof(DataListsDbContext));
            }
        }

        public static string TenantId
        {
            get
            {
                return tenantIdLoader.Value;
            }
        }
    }
}
