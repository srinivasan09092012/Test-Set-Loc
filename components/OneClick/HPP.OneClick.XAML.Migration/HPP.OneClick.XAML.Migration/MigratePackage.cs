
using HPP.OneClick.XAML.Migration.Log4Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HPP.OneClick.XAML.Migration
{
  public class MigratePackage
  {
    private string pkgFilepath = ConfigurationManager.AppSettings["PKGPath"].ToString();
    private IOneClickLog log;

    public MigratePackage()
    {
      this.log = new OneClickLog(MethodBase.GetCurrentMethod().DeclaringType);
      ExtractPackageModel();
    }

    public IOneClickLog Log
    {
      get
      {
        return this.log;
      }
    }

    private void ExtractPackageModel()
    {
      string[] lines = System.IO.File.ReadAllLines(pkgFilepath);
      List<string> pkgList = lines.ToList();

      DBAccess dbAccess = new DBAccess();
      dbAccess.InsertPackages(pkgList);
    }

  }
}
