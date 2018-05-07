//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Config;
using System.Configuration;

namespace MainEvent.Core
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
