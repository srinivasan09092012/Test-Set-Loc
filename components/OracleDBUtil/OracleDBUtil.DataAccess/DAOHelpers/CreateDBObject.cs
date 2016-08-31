using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HP.HSP.UA3.Core.BAS.CQRS.DataAccess;
using Oracle.DataAccess.Client;

namespace OracleDBUtil.DataAccess.DAOHelpers
{
    public class CreateDBObject : OracleDAOBase<int>
    {
        public int ExecuteProcedure(string command)
        {
            base.CreateOracleCommand(command, System.Data.CommandType.Text);
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
    }
}
