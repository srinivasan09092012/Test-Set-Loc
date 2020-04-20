//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using ViewProjectionLoader.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ViewProjectionLoader.Core.Services
{
    public abstract class DbService : IDbService
    {
        private Lazy<DbProviderFactory> factoryLoader;
        private Lazy<DbConnection> connectionLoader;

        public DbService(string connectionString, string providerName)
        {
            this.ConnectionString = connectionString;
            this.ProviderName = providerName;

            this.factoryLoader = new Lazy<DbProviderFactory>(() =>
            {
                return DbProviderFactories.GetFactory(this.ProviderName);
            }, true);

            this.connectionLoader = new Lazy<DbConnection>(() =>
            {
                DbConnection conn = this.factoryLoader.Value.CreateConnection();
                conn.ConnectionString = this.ConnectionString;
                conn.Open();
                return conn;
            }, true);
        }

        protected DbProviderFactory ProviderFactory
        {
            get { return this.factoryLoader.Value; }
        }

        protected DbConnection Connection
        {
            get { return this.connectionLoader.Value; }
        }

        public string ConnectionString { get; private set; }

        public string ProviderName { get; private set; }

        public abstract void DeleteTableData(IEnumerable<string> orderedTableNames, IStatusTracker statusTracker);

        public abstract List<string> GetTableNames();

        public abstract List<TableSchemaModel> GetTableSchemas();

        public abstract List<AggregateStorageData> GetAggregatesWithCommitsBetween(DateTime timestampFrom, DateTime timestampThrough);

        public bool TryConnect(string connectionString, out Exception error)
        {
            error = null;
            bool result = false;
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(this.ProviderName);

            try
            {
                using (DbConnection dbConnection = dbFactory.CreateConnection())
                {
                    dbConnection.ConnectionString = connectionString;
                    dbConnection.Open();
                    dbConnection.Close();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                error = ex;
            }

            return result;
        }

        public void Dispose()
        {
            this.Disposing(true);
        }

        protected object ExecuteScalar(string commandText)
        {
            object result = null;

            using (DbCommand cmd = this.Connection.CreateCommand())
            {
                cmd.CommandText = commandText;
                result = cmd.ExecuteScalar();
            }

            return result;
        }

        protected int ExecuteNonQuery(string commandText)
        {
            int result = 0;

            using (DbCommand cmd = this.Connection.CreateCommand())
            {
                cmd.CommandText = commandText;
                result = cmd.ExecuteNonQuery();
            }

            return result;
        }

        protected DbDataReader ExecuteReader(string queryText)
        {
            DbDataReader result = null;

            using (DbCommand cmd = this.Connection.CreateCommand())
            {
                cmd.CommandText = queryText;
                result = cmd.ExecuteReader();
            }

            return result;
        }

        protected virtual void Disposing(bool disposingAll)
        {
            if (this.Connection.State == ConnectionState.Open)
            {
                try
                {
                    this.Connection.Close();
                }
                catch
                {
                }
            }
        }

        protected DataTable GetSchemaTable(string schemaTableName)
        {
            return this.Connection.GetSchema(schemaTableName);
        }
    }
}
