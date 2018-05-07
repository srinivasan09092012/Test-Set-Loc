//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using MainEvent.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace MainEvent.Core.Services
{
    public class OracleDbService : DbService
    {
        public OracleDbService(string connectionString, string providerName)
            : base(connectionString, providerName)
        {
        }

        public override void DeleteTableData(IEnumerable<string> orderedTableNames, IStatusTracker statusTracker)
        {
            using (DbTransaction transaction = this.Connection.BeginTransaction())
            {
                foreach (string table in orderedTableNames)
                {
                    statusTracker.Set("Deleting data from {0}", table);
                    string query = string.Format("DELETE FROM \"{0}\"", table);
                    this.ExecuteNonQuery(query);
                }

                statusTracker.Set("Committing transaction");
                transaction.Commit();
            }
        }

        public override List<string> GetTableNames()
        {
            List<string> result = new List<string>();

            using (DbDataReader reader = this.ExecuteReader("SELECT * FROM USER_TABLES"))
            {
                while (reader.Read())
                {
                    result.Add(reader["TABLE_NAME"].ToString());
                }
            }

            return result;
        }

        public override List<TableSchemaModel> GetTableSchemas()
        {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory("Oracle.DataAccess.Client");

            List<string> allTableNames = this.GetTableNames();

            Dictionary<string, TableSchemaModel> temp = new Dictionary<string, TableSchemaModel>();
            foreach (string tbl in allTableNames)
            {
                temp.Add(tbl, new TableSchemaModel(tbl));
            }

            string schema = this.GetSchema();
            DataTable keys = this.GetForeignKeySchemaTable();

            foreach (DataRow row in keys.Rows)
            {
                if ((string)row["R_OWNER"] != schema)
                {
                    continue;
                }

                string pkTable = (string)row["PRIMARY_KEY_TABLE_NAME"];
                string fkTable = (string)row["FOREIGN_KEY_TABLE_NAME"];

                if (!temp.ContainsKey(fkTable))
                {
                    temp.Add(fkTable, new TableSchemaModel(fkTable));
                }

                temp[fkTable].Dependencies.Add(pkTable);
            }

            return temp.Values.OrderBy(p => p.Name).ToList();
        }

        private string GetSchema()
        {
            object schemaObj = null;
            string schema = string.Empty;

            DbProviderFactory dbFactory = DbProviderFactories.GetFactory("Oracle.DataAccess.Client");
            DbConnectionStringBuilder builder = dbFactory.CreateConnectionStringBuilder();

            builder.ConnectionString = this.ConnectionString;

            if (builder.TryGetValue("USER ID", out schemaObj))
            {
                schema = schemaObj.ToString();
            }

            return schema;
        }

        private DataTable GetForeignKeySchemaTable()
        {
            return this.GetSchemaTable("ForeignKeys");
        }

        public override List<AggregateStorageData> GetAggregatesWithCommitsBetween(DateTime timestampFrom, DateTime timestampThrough)
        {
            List<AggregateStorageData> result = new List<AggregateStorageData>();

            using (var command = this.Connection.CreateCommand())
            {
                command.CommandText = @"SELECT DISTINCT STREAMIDORIGINAL, BUCKETID
FROM COMMITS
WHERE COMMITSTAMP >= :p1 AND COMMITSTAMP <= :p2 AND STREAMREVISION = :p3
GROUP BY BUCKETID, STREAMIDORIGINAL";

                var p1 = command.CreateParameter();
                p1.ParameterName = "p1";
                p1.Value = timestampFrom;
                p1.DbType = DbType.DateTime;

                var p2 = command.CreateParameter();
                p2.ParameterName = "p2";
                p2.Value = timestampThrough;
                p2.DbType = DbType.DateTime;

                var p3 = command.CreateParameter();
                p3.ParameterName = "p3";
                p3.Value = 1;
                p3.DbType = DbType.Int32;

                command.Parameters.Add(p1);
                command.Parameters.Add(p2);
                command.Parameters.Add(p3);

                using (var reader = command.ExecuteReader(CommandBehavior.Default))
                {
                    while (reader.Read())
                    {
                        string bucketId = (string)reader["BUCKETID"];
                        string streamId = (string)reader["STREAMIDORIGINAL"];

                        result.Add(new AggregateStorageData(bucketId, streamId));
                    }
                }
            }

            return result;
        }
    }
}
