//--------------------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.ServiceModel;

namespace DatalistSyncUtil.Security
{
    public class ServiceApiFactory : IDisposable
    {
        private readonly List<ICommunicationObject> activeServices;
        private readonly SecurityToken authToken;

        public ServiceApiFactory(SecurityToken token)
        {
            this.activeServices = new List<ICommunicationObject>();
            this.authToken = token;
        }

        public void Dispose()
        {
            foreach (var service in this.activeServices)
            {
                try
                {
                    service.Close();
                }
                catch (CommunicationObjectFaultedException)
                {
                    service.Abort();
                }
                catch (TimeoutException)
                {
                    service.Abort();
                }
            }
        }

        public T GetService<T>(string endpointConfigurationName)
        {
            var factory = new ChannelFactory<T>(endpointConfigurationName);
            this.activeServices.Add(factory);

            return factory.CreateChannelWithIssuedToken(this.authToken);
        }
    }
}
