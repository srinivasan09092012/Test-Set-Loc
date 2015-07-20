using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OracleDBUtil.Contracts;

namespace OracleDBUtil.DataAccess
{
    public class DatabaseMetaRepository
    {
        public List<DatabaseObject> GetTableMetadata(string ConnectionName)
        {
            return new DAOHelpers.GetSchemaTables(ConnectionName).ExecuteProcedure();
        }

        public List<DatabaseObject> GetProcedureMetadata(string ConnectionName)
        {
            return new DAOHelpers.GetSchemaProcedures(ConnectionName).ExecuteProcedure();
        }

        public int CreateDBObject(string command)
        {
            return new DAOHelpers.CreateDBObject().ExecuteProcedure(command);
        }

        public int CreateSchema(string user, string password)
        {
            return new DAOHelpers.CreateSchema().ExecuteProcedure(user, password);
        }        
    }
}
