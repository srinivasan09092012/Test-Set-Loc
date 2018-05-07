//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using Microsoft.IdentityModel.Protocols.WSTrust.Bindings;
using System;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace DatalistSyncUtil.Security
{
    public class AuthController
    {
        private SecurityToken authToken;

        public SecurityToken GeToken(string userName, string userPassword)
        {
            this.AuthInSts(userName, userPassword);
            return this.authToken;
        }

        private void AuthInSts(string userName, string userPassword)
        {
            ServicePointManager.ServerCertificateValidationCallback =
                (sender, certificate, chain, sslPolicyErrors) => true;
            var rst = new Microsoft.IdentityModel.Protocols.WSTrust.RequestSecurityToken
            {
                RequestType = RequestTypes.Issue,
                AppliesTo = new EndpointAddress("http://localhost:28981"),
                KeyType = KeyTypes.Bearer,
            };

            using (var trustChannelFactory = 
                new Microsoft.IdentityModel.Protocols.WSTrust.WSTrustChannelFactory(
                    new UserNameWSTrustBinding(SecurityMode.TransportWithMessageCredential),
            new EndpointAddress(new Uri("https://adfs.dev.ua3.eslabs.ssn.hp.com/adfs/services/trust/13/usernamemixed/"))))
            {
                trustChannelFactory.Credentials.UserName.UserName = userName;
                trustChannelFactory.Credentials.UserName.Password = userPassword;
                trustChannelFactory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
                trustChannelFactory.TrustVersion = TrustVersion.WSTrust13;
                var channel = (Microsoft.IdentityModel.Protocols.WSTrust.WSTrustChannel)trustChannelFactory.CreateChannel();
                try
                {
                    this.authToken = channel.Issue(rst);
                }
                catch (MessageSecurityException ex)
                {
                    channel.Abort();
                    throw new SecurityTokenException(ex.InnerException.Message, ex);
                }
            }
        }
    }
}
