using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess;
using OracleDBUtil.Contracts;

namespace OracleDBUtil.DataAccess.DAOHelpers
{
    public class GetSchemaProcedures : OracleDAOBase<DatabaseObject>
    {
        public GetSchemaProcedures(string ConnectionName)
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
            result.ObjectName = reader.GetNullableString("object_name");
            result.ObjectCreateScript = reader.GetNullableString("metadata");
            return result;
        }

        private string Query
        {
            get
            {
                return @"select
                           DBMS_METADATA.GET_DDL('PROCEDURE',u.object_name) as metadata, u.object_name
                         from
                           user_objects u
                         where
                           object_type = 'PROCEDURE'";
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
