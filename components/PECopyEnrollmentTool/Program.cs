using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PECopyEnrollment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;

            string trackingId = string.Empty;
            int tenantCode = 0;
            EnrollmentCopier enrollmentCopier = new EnrollmentCopier();

            while (!endApp)
            {
                Console.WriteLine($"Please enter your ATN:");
                trackingId = Console.ReadLine();
                trackingId = trackingId.Trim();

                Console.WriteLine($"Please select tenant: Press 1 for Medicaid and Press 2 for Commercial:");
                tenantCode = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"**********************************************");
                Console.WriteLine($"PE Application Migration Data Transfer started....");
                Console.WriteLine("\n");

                try
                {
                    enrollmentCopier.Copy(trackingId, tenantCode);
                    Console.WriteLine($"PE Application Migration Data Transfer completed ||");
                    Console.WriteLine($"**********************************************");

                    Console.Write("Press 'n' and Enter to close the app, or press Enter to continue: ");
                    if (Console.ReadLine() == "n") { endApp = true; }

                    Console.WriteLine("\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
        }
    }
}
