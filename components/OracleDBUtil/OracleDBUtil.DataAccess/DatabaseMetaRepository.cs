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
    }
}
