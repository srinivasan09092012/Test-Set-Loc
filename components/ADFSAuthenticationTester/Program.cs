//-----------------------------------------------------------------------------------------
// This code is the property of DXC.Technology, Copyright (c) 2018. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HPE.HSP.UA3.Core.API.AuthManagement.Providers;
using System;
using System.Configuration;
using System.IdentityModel.Tokens;

namespace ADFSAuthenticationTester
{
    public class Program
    {
        private static string domain = null;
        private static string endpointAddress = null;
        private static string relayIdentity = null;

        public static void Main(string[] args)
        {
            domain = ConfigurationManager.AppSettings["ADFSDomain"];
            endpointAddress = ConfigurationManager.AppSettings["ADFSEndPointAddress"];
            relayIdentity = ConfigurationManager.AppSettings["ADFSRelayIdentity"];

            Console.WriteLine("ADFS Authentication Tester");
            Console.WriteLine("ADFS Domain: " + domain);
            Console.WriteLine("ADFS Endpoint Address: " + endpointAddress);
            Console.WriteLine("ADFS Relay Identity: " + relayIdentity);
            Console.WriteLine("");

            while (true)
            {
                Console.Write("Enter your user name: ");
                string userName = Console.ReadLine();
                Console.Write("Enter your password: ");
                string password = Console.ReadLine();

                Console.WriteLine("Authenticating user credentials...");
                AuthenticateUser(userName, password);
                Console.WriteLine("");
            }
        }

        public static void AuthenticateUser(string userName, string password)
        {
            try
            {
                ADFSClaimsProvider provider = new ADFSClaimsProvider(
                    domain,
                    endpointAddress,
                    relayIdentity
                    );

                SecurityToken claimsToken = provider.GenerateClaimsToken(userName, password);
                if (claimsToken != null)
                {
                    string securityToken = ((System.IdentityModel.Tokens.GenericXmlSecurityToken)claimsToken).TokenXml.OuterXml;
                    Console.WriteLine("Authentication Succeeded: " + securityToken);
                }
                else
                {
                    Console.WriteLine("Authentication Failed!");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Authentication Failed: " + ex.Message);
            }
        }
    }
}
