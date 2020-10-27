//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HPE.HSP.UA3.Core.API.Logger;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Fabric;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WarmUpProvider.Helpers;

namespace WarmUpProvider.Main.Service
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class Service : StatelessService
    {
        public Service(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            long iterations = 0;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                double interval = Int32.Parse(ConfigurationManager.AppSettings["WarmUpEndpointIntervalSeconds"]);
                //List<Task> tasks = new List<Task>();
                //WarmUpHelper warmUpProvider = new WarmUpHelper();
                //tasks.Add(Task.Run(() => warmUpProvider.StartUp()));
                //Task.WaitAll(tasks.ToArray());

                //Setting this so the service can access EndpointInformation.json file
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

                this.OnLoad();
                ServiceEventSource.Current.ServiceMessage(this.Context, "Working-{0}", ++iterations);
                await Task.Delay(TimeSpan.FromSeconds(interval), cancellationToken);
            }
        }

        private void OnLoad()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    WarmUpHelper warmUpProvider = new WarmUpHelper();
                    warmUpProvider.StartUp();
                }
                catch (Exception ex)
                {
                    LoggerManager.Logger.LogWarning("BAS Warming Up Endpoints failed!!!", ex);
                }
            });
        }
    }
}
