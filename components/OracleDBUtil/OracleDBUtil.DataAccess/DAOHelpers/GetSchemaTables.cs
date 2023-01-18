using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess;
using Oracle.DataAccess.Client;
using OracleDBUtil.Contracts;

namespace OracleDBUtil.DataAccess.DAOHelpers
{
    public class GetSchemaTables : OracleDAOBase<DatabaseObject>
    {
        public GetSchemaTables(string ConnectionName)
        {
            _connectionname = ConnectionName;
        }

        public List<DatabaseObject> ExecuteProcedure()
        {
            base.CreateOracleCommand(Query, System.Data.CommandType.Text);
            return base.ExecuteNullableQuery();
        }

        protected override DatabaseObject ConvertDataReaderToObject(NullableDataReader reader)
        {
            DatabaseObject result = new DatabaseObject();
            result.ObjectName = reader.GetNullableString("table_name");
            result.ObjectCreateScript = reader.GetNullableString("metadata");
            return result;
        }
      
        private string Query
        {
            get
            {
                return @"SELECT DBMS_METADATA.GET_DDL('TABLE', u.table_name) as metadata, u.table_name FROM USER_ALL_TABLES u";
            }
        }

        private string _connectionname;
        protected override string ConnectionName
        {
            get
            {
                return _connectionname;
            }
        }
    }
}
