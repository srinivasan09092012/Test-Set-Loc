using System;
using System.DirectoryServices.AccountManagement;

namespace WindowsIdentityHelper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                UserPrincipal userPrinciple = UserPrincipal.Current;
                Console.WriteLine("User ID: " + userPrinciple.SamAccountName);
                Console.WriteLine("Distinguished Name: " + userPrinciple.DistinguishedName);
                Console.WriteLine("First Name: " + userPrinciple.GivenName);
                Console.WriteLine("Last Name: " + userPrinciple.Surname);
                Console.WriteLine("Display Name: " + userPrinciple.DisplayName);
                Console.WriteLine("Employee ID: " + userPrinciple.EmployeeId);
                Console.WriteLine("Telephone Number: " + userPrinciple.VoiceTelephoneNumber);
                Console.WriteLine("Email Address: " + userPrinciple.EmailAddress);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            finally
            {
                Console.Write("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
