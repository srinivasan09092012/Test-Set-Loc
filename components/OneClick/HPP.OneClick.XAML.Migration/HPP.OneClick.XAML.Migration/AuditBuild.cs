using HPP.OneClick.XAML.Migration.Log4Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HPP.OneClick.XAML.Migration
{
    public class AuditBuild
    {
        private IOneClickLog log;
        public AuditBuild()
        {
            this.log = new OneClickLog(MethodBase.GetCurrentMethod().DeclaringType);
          
        }
        public IOneClickLog Log
        {
            get
            {
                return this.log;
            }
        }

        private void GetAuditBuilds()
        {
            DBAccess dbAccess = new DBAccess();
        }
    }
}
