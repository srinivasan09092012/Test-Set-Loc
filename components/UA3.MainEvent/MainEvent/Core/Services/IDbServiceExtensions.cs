//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace MainEvent.Core.Services
{
    public static class IDbServiceExtensions
    {
        public static List<string> GetTableNames(this IDbService source, string connectionString, Predicate<string> requiredCondition)
        {
            List<string> result = source.GetTableNames();

            result.RemoveAll(p => requiredCondition(p) == false);

            return result;
        }

        public static List<string> GetViewProjectionTableNames(this IDbService source, string connectionString)
        {
            string[] exclude = new string[] { "COMMITS", "SNAPSHOTS", "EVENT_DISTRIBUTION", "EVENT_AUDIT", "QUERY_LOG" };

            return source.GetTableNames(connectionString, tbl => exclude.Contains(tbl) == false && tbl.Contains("_TEXT") == false);
        }
    }
}
