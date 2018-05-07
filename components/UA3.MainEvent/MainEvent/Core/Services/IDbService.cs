//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using MainEvent.Core.Model;
using System;
using System.Collections.Generic;

namespace MainEvent.Core.Services
{
    public interface IDbService : IDisposable
    {
        string ConnectionString { get; }

        bool TryConnect(string connectionString, out Exception error);

        List<string> GetTableNames();

        List<TableSchemaModel> GetTableSchemas();

        void DeleteTableData(IEnumerable<string> orderedTableNames, IStatusTracker statusTracker);

        List<AggregateStorageData> GetAggregatesWithCommitsBetween(DateTime timestampFrom, DateTime timestampThrough);
    }
}
