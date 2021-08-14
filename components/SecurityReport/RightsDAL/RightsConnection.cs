//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2020. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using static HostsManager.MainForm;

namespace RightsDAL
{
    public class RightsConnection
    {
        private SqlConnection sqlCn = null;
        private OracleConnection oracleCn = null;

        public RightsConnection(bool IsOracle, string ServerName, string UserName, string Password, string Schema = "")
        {
            this.IsOracle = IsOracle;

            if (this.IsOracle)
            {
                this.CreateOracleConnection(ServerName, Schema, UserName, Password);
            }
            else
            {
                this.CreateSqlConnection(ServerName, Schema, UserName, Password);
            }
        }

        public string OracleSchema { get; set; }

        public bool IsConnected()
        {
            if (IsOracle)
            {
                return (oracleCn.State == ConnectionState.Open);
            }
            else
            {
                return (sqlCn.State == ConnectionState.Open);
            }
        }

        public void CloseConnection()
        {
            if (this.IsOracle)
            {
                this.CloseConnection(oracleCn);
                oracleCn = null;
            }
            else
            {
                this.CloseConnection(sqlCn);
                sqlCn = null;
            }
        }

        public void CloseConnection(IDbConnection conn)
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
             
                conn.Dispose();
            }
        }

        public bool IsOracle { get; private set; }

        public bool CreateOracleConnection(string ServerName, string Schema, string UserName, string Password)
        {

            if (this.oracleCn != null && this.oracleCn.State != ConnectionState.Open)
            {
                this.CloseConnection(oracleCn);
            }

            if (this.oracleCn == null)
            {
                Host host = getHostDetails("Oracle", ServerName);

                if (host != null && host.dbService == ServerName)
                {
                    oracleCn = new OracleConnection();
                    oracleCn.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(" +
                        $"HOST={host.dbHost})(" +
                        $"PORT={host.dbPort}))(CONNECT_DATA=(SERVER=DEDICATED)(" +
                        $"SERVICE_NAME={host.dbService})));" +
                        $"USER ID={UserName};" +
                        $"PASSWORD={Password}";

                    this.oracleCn.Open();

                    if (!string.IsNullOrEmpty(Schema))
                    {
                        this.OracleSchema = Schema;

                        using (var command = new OracleCommand("alter session set current_schema=" + Schema))
                        {
                            command.Connection = oracleCn;
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }

            return (this.oracleCn.State == ConnectionState.Open);
        }

        public bool CreateSqlConnection(string ServerName, string Catalog, string UserName, string Password)
        {
            if (this.sqlCn != null && this.sqlCn.State != ConnectionState.Open)
            {
                this.CloseConnection(sqlCn);
            }

            if (this.sqlCn == null)
            {
                Host host = getHostDetails("SqlServer", ServerName);

                sqlCn = new SqlConnection();

                if (host != null && host.dbService == ServerName)
                {
                    sqlCn.ConnectionString = $"Data Source={host.dbHost};" +
                        $"User id={UserName}; " +
                        $"Password={Password};" +
                        $"Initial Catalog={host.dbInitialCatalog};" +
                        "Persist Security Info=False;" +
                        "MultipleActiveResultSets=True;" +
                        "Encrypt=True;" +
                        "TrustServerCertificate=True;Pooling=True;Min Pool Size=3;Max Pool Size=100;Connection Timeout=25";
                }
                else
                {
                    sqlCn.ConnectionString = $"Data Source={ServerName};" +
                        $"User id={UserName}; " +
                        $"Password={Password};" +
                        $"Initial Catalog={Catalog};" +
                        "Persist Security Info=False;" +
                        "MultipleActiveResultSets=True;" +
                        "Encrypt=True;" +
                        "TrustServerCertificate=True;Pooling=True;Min Pool Size=3;Max Pool Size=100;Connection Timeout=25";
                }
                this.sqlCn.Open();
            }

            return (this.sqlCn.State == ConnectionState.Open);
        }

        public IDataReader ExecuteQuery(string CommandText)
        {
            IDataReader reader = null;
            IDbCommand cmd;


            if (this.IsOracle)
            {
                cmd = oracleCn.CreateCommand();
            }
            else
            {
                cmd = sqlCn.CreateCommand();
            }

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = CommandText;

            reader = cmd.ExecuteReader();

            cmd.Dispose();

            return reader;
        }

    }
}
