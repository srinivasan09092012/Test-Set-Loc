using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleDBUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                new Business.ExtractDatabaseServices().ExtractDatabaseObjects();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            
            Console.ReadLine();
        }
    }
}
