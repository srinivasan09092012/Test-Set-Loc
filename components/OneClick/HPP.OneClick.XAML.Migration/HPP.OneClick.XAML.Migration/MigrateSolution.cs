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
  public class MigrateSolution
  {
    private string slnFilepath = ConfigurationManager.AppSettings["SLNPath"].ToString();
    private IOneClickLog log;

    public MigrateSolution()
    {
      this.log = new OneClickLog(MethodBase.GetCurrentMethod().DeclaringType);
      getsolutiondetails();
    }

    public IOneClickLog Log
    {
      get
      {
        return this.log;
      }
    }

    private void getsolutiondetails()
    {
      string[] lines = System.IO.File.ReadAllLines(slnFilepath);
      List<string> slnList = lines.ToList();

      DBAccess dbAccess = new DBAccess();
      dbAccess.InsertSolutions(slnList);
      // Display the file contents by using a foreach loop.
      System.Console.WriteLine("Contents of WriteLines2.txt = ");
      foreach (string line in lines)
      {
        // Use a tab to indent each line of the file.
        Console.WriteLine("\t" + line);
      }

    }
  }
}
