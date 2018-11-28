//-----------------------------------------------------------------------------------------
// This code is the property of DXC.Technology, Copyright (c) 2018. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace ADIdentityManagementTester
{
    /// <summary>
    /// If you receive RPC errors running this test harness, see the following article.
    /// 
    /// https://social.msdn.microsoft.com/Forums/vstudio/en-US/03376415-fa4a-48aa-bb81-39661ea31c3e/what-are-the-required-security-permissions-to-call-sam-accountmanagement-over-the-wire-using?forum=netfxbcl
    /// 
    /// </summary>
    public class Program
    {
        private static string adContainer = null;
        private static string adServer = null;
        private static string adUser = null;
        private static string adPassword = null;
        private static string testUserName = null;
        private static string groupName = "MapsSystemAdministrators";

        private static PrincipalContext pc = null;

        public static void Main(string[] args)
        {
            adContainer = ConfigurationManager.AppSettings["ADContainer"];
            adServer = ConfigurationManager.AppSettings["ADServer"];
            adUser = ConfigurationManager.AppSettings["ADUser"];
            adPassword = ConfigurationManager.AppSettings["ADPassword"];

            Console.WriteLine("AD Identity Management Tester");
            Console.WriteLine("AD Container: " + adContainer);
            Console.WriteLine("AD Server: " + adServer);
            Console.WriteLine("AD User: " + adUser);
            Console.WriteLine("AD Password: " + adPassword);
            Console.WriteLine("");

            try
            {
                pc = InitializePrincipalContext();
                if (pc != null)
                {
                    GenerateRandomUserName();
                    IsUserNameAvailable();
                    IsGroupValid();
                    CreateAccount();
                    AddAccountRegistrationQualifiers();
                    AddAccountGroups();
                    UpdateAccount();
                    ChangePassword();
                    ResetPassword();
                    DeleteAccount();
                }
            }
            finally
            {
                Console.WriteLine("");
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static void GenerateRandomUserName()
        {
            Console.Write("Generate Random User Name: ");

            try
            {
                int nbr = new Random().Next(900000, 999999);
                testUserName = "TestUser" + nbr.ToString();

                Console.WriteLine(testUserName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
        }

        private static void IsUserNameAvailable()
        {
            Console.Write("Is User Name Available: ");

            try
            {
                bool isAvailable = false;

                using (UserPrincipal user = UserPrincipal.FindByIdentity(pc, testUserName))
                {
                    isAvailable = user == null;
                }

                Console.WriteLine(isAvailable);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
        }

        private static void IsGroupValid()
        {
            Console.Write("Is Group Valid: ");

            try
            {
                bool isValid = false;

                using (GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, groupName))
                {
                    isValid = group != null;
                }

                Console.WriteLine(isValid);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
        }

        private static void CreateAccount()
        {
            Console.Write("Create Account: ");

            try
            {
                using (UserPrincipal newUser = new UserPrincipal(pc, testUserName, "Password2018", true))
                {
                    newUser.AccountExpirationDate = null;
                    newUser.GivenName = "Test";
                    newUser.MiddleName = "User";
                    newUser.Surname = "Account";
                    newUser.EmailAddress = "test@dxc.com";
                    newUser.DisplayName = "Test Account";
                    newUser.UserPrincipalName = testUserName + GetDomainFromContainer();
                    newUser.SamAccountName = testUserName;
                    newUser.PasswordNotRequired = false;
                    newUser.PasswordNeverExpires = true;
                    newUser.VoiceTelephoneNumber = "555-555-5555";
                    newUser.Save();
                }

                Console.WriteLine("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
        }

        private static void AddAccountRegistrationQualifiers()
        {
            Console.Write("Add Account Reg Qualifiers: ");

            try
            {
                using (UserPrincipal user = UserPrincipal.FindByIdentity(pc, testUserName))
                {
                    if (user != null)
                    {
                        using (DirectoryEntry directoryEntry = (DirectoryEntry)user.GetUnderlyingObject())
                        {
                            directoryEntry.Properties["info"].Value = "some json serialzied string here";
                            directoryEntry.CommitChanges();
                        }

                        Console.WriteLine("Success");
                    }
                    else
                    {
                        Console.WriteLine("User Not Found");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
        }

        private static void AddAccountGroups()
        {
            Console.Write("Add Account Groups: ");

            try
            {
                using (UserPrincipal user = UserPrincipal.FindByIdentity(pc, testUserName))
                {
                    if (user != null)
                    {
                        using (GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, groupName))
                        {
                            group.GroupScope = GroupScope.Global;
                            group.Members.Add(user);
                            group.Save();
                        }

                        Console.WriteLine("Success");
                    }
                    else
                    {
                        Console.WriteLine("User Not Found");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
        }

        private static void UpdateAccount()
        {
            Console.Write("Update Account: ");

            try
            {
                using (UserPrincipal user = UserPrincipal.FindByIdentity(pc, testUserName))
                {
                    if (user != null)
                    {
                        user.VoiceTelephoneNumber = "444-444-4444";
                        user.Save();

                        Console.WriteLine("Success");
                    }
                    else
                    {
                        Console.WriteLine("User Not Found");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
        }

        private static void ChangePassword()
        {
            Console.Write("Change Password: ");

            try
            {
                using (UserPrincipal user = UserPrincipal.FindByIdentity(pc, testUserName))
                {
                    if (user != null)
                    {
                        user.ChangePassword("Password2018", "Password2019");
                        user.Save();

                        Console.WriteLine("Success");
                    }
                    else
                    {
                        Console.WriteLine("User Not Found");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
        }

        private static void ResetPassword()
        {
            Console.Write("Reset Password: ");

            try
            {
                using (UserPrincipal user = UserPrincipal.FindByIdentity(pc, testUserName))
                {
                    if (user != null)
                    {
                        user.SetPassword("Password2020");
                        user.Save();

                        Console.WriteLine("Success");
                    }
                    else
                    {
                        Console.WriteLine("User Not Found");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
        }

        private static void DeleteAccount()
        {
            Console.Write("Delete Account: ");

            try
            {
                using (UserPrincipal user = UserPrincipal.FindByIdentity(pc, testUserName))
                {
                    if (user != null)
                    {
                        user.Delete();

                        Console.WriteLine("Success");
                    }
                    else
                    {
                        Console.WriteLine("User Not Found");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
        }

        private static string GetDomainFromContainer()
        {
            string domain = "@";

            List<string> domainParts = new List<string>(adContainer.Split(','));
            foreach (string node in domainParts)
            {
                if (node.ToLower().StartsWith("dc="))
                {
                    domain += node.ToLower().Substring(3) + ".";
                }
            }

            if (domain.Length > 1)
            {
                domain = domain.Substring(0, domain.Length - 1);
            }

            return domain;
        }

        private static PrincipalContext InitializePrincipalContext()
        {
            PrincipalContext pc = null;

            Console.Write("Initialize Principal Context: ");

            try
            {
                ContextOptions contextOptions = ContextOptions.Negotiate;
                if (adServer.EndsWith(":636") || adServer.EndsWith(":3269"))
                {
                    contextOptions = ContextOptions.SecureSocketLayer | ContextOptions.Negotiate;
                }

                pc = new PrincipalContext(
                    ContextType.Domain,
                    adServer,
                    adContainer,
                    contextOptions,
                    adUser,
                    adPassword);

                Console.WriteLine("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }

            return pc;
        }
    }
}
