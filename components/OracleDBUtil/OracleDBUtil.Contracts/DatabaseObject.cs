using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleDBUtil.Contracts
{
    public class DatabaseObject
    {
        public string ObjectName { get; set; }

        public string ObjectCreateScript { get; set; }
    }
}
