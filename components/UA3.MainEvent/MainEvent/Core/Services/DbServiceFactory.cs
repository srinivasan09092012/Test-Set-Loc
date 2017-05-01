//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Configuration;

namespace MainEvent.Core.Services
{
    public class DbServiceFactory : IDbServiceFactory
    {
        public IDbService CreateDbService(ConnectionStringSettings connectionString)
        {
            if (connectionString.ProviderName.ToLower().Contains("oracle"))
            {
                return new OracleDbService(connectionString.ConnectionString, connectionString.ProviderName);
            }
            else
            {
                throw new NotSupportedException(string.Format("Provider {0} is not yet supported", connectionString.ProviderName));
            }
        }
    }
}