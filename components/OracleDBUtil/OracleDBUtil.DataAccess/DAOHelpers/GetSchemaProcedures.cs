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
        public GetSchemaProcedures(string ConnectionName, DatabaseObjectType TypeToGet)
        {
            _connectionname = ConnectionName;
            SetQueryTypes(TypeToGet);
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
                string query= @"select
                           DBMS_METADATA.GET_DDL('DdlTypeToGet',u.object_name) as metadata, u.object_name
                         from
                           user_objects u
                         where
                           object_type = 'TypeToGet'";

                query = query.Replace("DdlTypeToGet", _ddltype);
                query = query.Replace("TypeToGet", _type);

                return query;
            }
        }

        private void SetQueryTypes(DatabaseObjectType TypeToGet)
        {

            switch (TypeToGet)
            {
                case DatabaseObjectType.Procedure:
                    _type = "PROCEDURE";
                    _ddltype = "PROCEDURE";
                    break;
                case DatabaseObjectType.PackageSpec:
                    _type = "PACKAGE";
                    _ddltype = "PACKAGE_SPEC";
                    break;
                case DatabaseObjectType.PackageBody:
                    _type = "PACKAGE";
                    _ddltype = "PACKAGE_BODY";
                    break;
                case DatabaseObjectType.Sequence:
                    _type = "SEQUENCE";
                    _ddltype = "SEQUENCE";
                    break;
                default:
                    break;
            }
        }

        private string _ddltype;
        private string _type;
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
