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
                string select = "0";
                Console.WriteLine("UA3 Database Utility");

                while (select != "3")
                {
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Select Option:");
                    Console.WriteLine("1. Extract Schema Metadata");
                    Console.WriteLine("2. Install Database");
                    Console.WriteLine("3. Exit");
                    select = Console.ReadLine();

                    switch (select)
                    {
                        case "1":
                            new Business.ExtractDatabaseServices().ExtractDatabaseObjects();
                            break;
                        case "2":
                            new Business.DatabaseInstallServices().ExecuteInstall();
                            break;
                        case "3":
                            break;
                        default:
                            Console.WriteLine("Invalid Option");
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
