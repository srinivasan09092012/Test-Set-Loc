using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess;
using Oracle.DataAccess.Client;

namespace OracleDBUtil.DataAccess.DAOHelpers
{
    public class CreateSchema : OracleDAOBase<int>
    {
        public int ExecuteProcedure(string user, string password)
        {
            base.CreateOracleCommand(CreateSchemaCommand(user, password), System.Data.CommandType.Text);
            return base.ExecuteNonQuery();
        }

        protected override int ConvertDataReaderToObject(OracleCommand cmd)
        {
            return 1;
        }

        protected override string ConnectionName
        {
            get
            {
                return "InstallConnection";
            }
        }

        private string CreateSchemaCommand(string user, string password)
        {
            string cmd = @"DECLARE
                         v_count NUMBER(1);
                        BEGIN
                        SELECT COUNT (1) 
                        into v_count 
                        FROM dba_users 
                        WHERE username = UPPER('USERNAME');

                        IF (v_count = 0) THEN
                          EXECUTE IMMEDIATE ('CREATE USER USERNAME IDENTIFIED BY PASSWORD');
                          EXECUTE IMMEDIATE ('GRANT CREATE TABLE, CREATE PROCEDURE TO USERNAME');
                          EXECUTE IMMEDIATE ('ALTER USER UA3_PROVIDER QUOTA UNLIMITED ON USERS');
                            
                        END IF;

                        END;";

            cmd = cmd.Replace("USERNAME", user);
            cmd = cmd.Replace("PASSWORD", password);
            return cmd;
        }
    }
}
