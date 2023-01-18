//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Config;
using System.Configuration;

namespace ViewProjectionLoader.Core
{
    public static class TenantBasConfigurationExtensions
    {
        public static ConnectionStringSettings GetPrimaryConnectionString(this TenantBasConfiguration source)
        {
            return new ConnectionStringSettings(
                "DefaultConnection",
                source.ConnectionString,
                source.ConnectionStringProvider);
        }
    }
}
