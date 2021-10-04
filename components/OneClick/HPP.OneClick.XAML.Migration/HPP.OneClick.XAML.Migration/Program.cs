using HPP.OneClick.XAML.Migration.Log4Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPP.OneClick.XAML.Migration
{
    class Program
    {
        static void Main(string[] args)
        {
            //XAMLBuild xamlbuild = new XAMLBuild();
            Console.WriteLine("Choice options from below");

            Console.WriteLine("1: XAML MIGRATION Job configuration");
            Console.WriteLine("2: Solution table update");
            Console.WriteLine("3: Package table update");
            Console.WriteLine("4: Dependency table update");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    XAMLBuild xamlbuild = new XAMLBuild();
                    break;

                case 2:
                    MigrateSolution migsolution = new MigrateSolution();
                    break;

                case 3:
                    MigratePackage migpackage = new MigratePackage();
                    break;

                case 4:
                    MigrateDependency migdep = new MigrateDependency();
                    break;

                case 5:
                    AuditBuild auditbuild = new AuditBuild();
                    break;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

        }
    }
}
