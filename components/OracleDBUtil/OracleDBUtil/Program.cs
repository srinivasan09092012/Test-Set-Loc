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
            new Business.ExtractDatabaseServices().ExtractDatabaseObjects();

            Console.WriteLine("Operation Complete");
            Console.ReadLine();
        }
    }
}
