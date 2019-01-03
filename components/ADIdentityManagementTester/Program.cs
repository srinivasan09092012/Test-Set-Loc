//-----------------------------------------------------------------------------------------
// This code is the property of DXC.Technology, Copyright (c) 2018. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Configuration;
using HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces;
using HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain;
using HPE.HSP.UA3.Core.API.IdentityManagement.Providers;

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
        private static string groupName2 = "MapsProviders";
        private static IUserManagementProvider adProvider = null;
        private static IUserQueryProvider adQueryProvider = null;

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
                adProvider = new ActiveDirectoryProvider(adServer, adUser, adPassword, adContainer);
                adQueryProvider = new ActiveDirectoryQueryProvider(adServer, adUser, adPassword, adContainer);

                GenerateRandomUserName();
                IsUserNameAvailable();
                IsGroupValid();
                CreateAccount();
                AddAccountGroups();
                UpdateAccount();
                //ChangePassword();
                ResetPassword();
                DeleteAccount();
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
                isAvailable = adQueryProvider.IsGroupNameAvailable(testUserName);
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
                isValid = !adQueryProvider.IsGroupNameAvailable(groupName);
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
                UserAccount userAccount = new UserAccount()
                {
                    AccountExpirationDate = null,
                    Identity = new UserIdentity()
                    {
                        FirstName = "Test",
                        MiddleName = "User",
                        LastName = "Account",
                        EmailAddress = "test@dxc.com",
                        DisplayName = "Test Account",
                        UserName = testUserName,
                        PhoneNumber = "555-555-5555"
                    },
                    Groups = new List<string>()
                    {
                        groupName
                    },
                    RegistrationQualifiers = new List<RegistrationQualifier>()
                    {
                        new RegistrationQualifier()
                        {
                            Key = "BirthDate",
                            Value = "19691010"
                        },
                        new RegistrationQualifier()
                        {
                            Key = "Last4SSN",
                            Value = "1234"
                        }
                    },
                    PasswordNeverExpires = false,
                    IsPasswordExpired = true,
                    Password = "Password2018"
                };
                adProvider.AddUser(userAccount);
                Console.WriteLine("Success");
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
                adProvider.AddUserToGroup(testUserName, groupName2);
                Console.WriteLine("Success");
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
                UserIdentity userIdentity = new UserIdentity()
                {
                    FirstName = "Test",
                    MiddleName = "User",
                    LastName = "Account",
                    EmailAddress = "test@dxc.com",
                    DisplayName = "Test Account",
                    UserName = testUserName,
                    PhoneNumber = "444-444-4444"
                };

                List<RegistrationQualifier> registrationQualifiers = new List<RegistrationQualifier>()
                {
                    new RegistrationQualifier()
                    {
                        Key = "BirthDate",
                        Value = "19691010"
                    },
                    new RegistrationQualifier()
                    {
                        Key = "Last4SSN",
                        Value = "1234"
                    }
                };
                adProvider.UpdateUser(userIdentity, registrationQualifiers, null);
                Console.WriteLine("Success");
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
                adProvider.ChangeUserPassword(testUserName, "Password2018", "Password2019");
                Console.WriteLine("Success");
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
                adProvider.ResetUserPassword(testUserName, "Password2020");
                Console.WriteLine("Success");
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
                adProvider.DeleteUser(testUserName);
                Console.WriteLine("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
        }
    }
}
