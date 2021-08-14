//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;

namespace UserAccountMigration.Providers
{
    public abstract class BaseServiceDataProvider
    {
        protected string behaviorConfiguration = string.Empty;
        protected string binding = string.Empty;
        protected string bindingConfiguration = string.Empty;
        protected string endpointAddress = string.Empty;
        protected string tenantId = string.Empty;

        public BaseServiceDataProvider(
            string behaviorConfiguration,
            string binding,
            string bindingConfiguration,
            string endpointAddress,
            string tenantId)
        {
            this.behaviorConfiguration = behaviorConfiguration;
            this.binding = binding;
            this.bindingConfiguration = bindingConfiguration;
            this.endpointAddress = endpointAddress;
            this.tenantId = tenantId;
        }

        public SecurityToken ClaimToken { get; set; }

        public virtual ChannelFactory<T> InitializeChannelFactory<T>()
        {
            ChannelFactory<T> channelFactory = null;
            EndpointAddress endpoint = new EndpointAddress(this.endpointAddress);

            switch (this.binding.ToLower())
            {
                case "basichttpbinding":
                    BasicHttpBinding binding = new BasicHttpBinding(this.bindingConfiguration);
                    channelFactory = new ChannelFactory<T>(binding, endpoint);
                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    break;

                case "ws2007federationhttpbinding":
                    WS2007FederationHttpBinding ws2007Binding = new WS2007FederationHttpBinding(this.bindingConfiguration);
                    channelFactory = new ChannelFactory<T>(ws2007Binding, endpoint);
                    channelFactory.Credentials.UseIdentityConfiguration = true;
                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                    BootstrapContext bootstrapContext = ClaimsPrincipal.Current.Identities.First().BootstrapContext as BootstrapContext;

                    if (bootstrapContext != null)
                    {
                        if (bootstrapContext.SecurityToken != null)
                        {
                            this.ClaimToken = bootstrapContext.SecurityToken;
                        }
                        else if (!string.IsNullOrEmpty(bootstrapContext.Token))
                        {
                            var handlers = FederatedAuthentication.FederationConfiguration.IdentityConfiguration.SecurityTokenHandlers;
                            this.ClaimToken = handlers.ReadToken(new XmlTextReader(new StringReader(bootstrapContext.Token)));
                        }
                    }
                    else
                    {
                        throw new SecurityTokenExpiredException("Access Denied. Token might be invalidated or expired");
                    }
                    break;

                case "custombinding":
                    CustomBinding customBinding = new CustomBinding();
                    channelFactory = new ChannelFactory<T>(customBinding, endpoint);
                    customBinding.Elements.Add(new MtomMessageEncodingBindingElement(MessageVersion.Soap12, System.Text.Encoding.UTF8));
                    customBinding.Elements.Add(new HttpTransportBindingElement());
                    break;

                case "wshttpbinding":
                    WSHttpBinding securedBinding = new WSHttpBinding(this.bindingConfiguration);
                    channelFactory = new ChannelFactory<T>(securedBinding, endpoint);
                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    break;

                default:
                    throw new ArgumentException(string.Format("Unsupported service binding specified '{0}'.", this.binding));
            }

            return channelFactory;
        }

        public virtual void SetClientCredentials(ChannelFactory channelFactory)
        {
            this.SetClientCredentials(channelFactory, false);
        }

        public virtual void SetClientCredentials(ChannelFactory channelFactory, bool forceSystemCredentials)
        {
            ClientCredentials defaultCredentials = channelFactory.Endpoint.Behaviors.Find<ClientCredentials>();
            channelFactory.Endpoint.Behaviors.Remove(defaultCredentials);

            ClientCredentials clientCredentials = new ClientCredentials();
            channelFactory.Endpoint.Behaviors.Add(clientCredentials);
        }

        public virtual T CreateChannel<T>(ChannelFactory channelFactory)
        {
            T proxy;
            ChannelFactory<T> channel = channelFactory as ChannelFactory<T>;

            switch (this.binding.ToLower())
            {
                case "basichttpbinding":
                case "wshttpbinding":
                    proxy = channel.CreateChannel();
                    break;
                case "ws2007federationhttpbinding":
                    proxy = channel.CreateChannelWithIssuedToken(this.ClaimToken);
                    break;
                case "custombinding":
                    proxy = channel.CreateChannel();
                    break;

                default:
                    throw new ArgumentException(string.Format("Unsupported service binding specified '{0}'.", this.binding));
            }

            return proxy;
        }

        public void Invoke<T>(Action<T> action)
        {
            ChannelFactory<T> factory = this.InitializeChannelFactory<T>();
            this.SetClientCredentials(factory);
            T client = this.CreateChannel<T>(factory);

            try
            {
                action(client);
                ((IClientChannel)client).Close();
                factory.Close();
            }
            catch (Exception ex)
            {
                IClientChannel clientInstance = (IClientChannel)client;
                if (clientInstance.State == CommunicationState.Faulted)
                {
                    clientInstance.Abort();
                    factory.Abort();
                }
                else if (clientInstance.State != CommunicationState.Closed)
                {
                    clientInstance.Close();
                    factory.Close();
                }

                throw ex;
            }
        }
    }
}