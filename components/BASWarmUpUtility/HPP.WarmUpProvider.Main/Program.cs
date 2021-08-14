//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2020. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;

namespace HPP.WarmUpProvider.Main
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            try
            {
                // The ServiceManifest.XML file defines one or more service type names.
                // Registering a service maps a service type name to a .NET type.
                // When Service Fabric creates an instance of this service type,
                // an instance of the class is created in this host process.
                BASUnityContainer.Initialize();
                ApplicationConfigurationManager.LoadApplicationConfiguration(ConfigurationManager.AppSettings["TenantId"], new RedisCacheManager());
                InitializeInstrumentationSettings();

                ServiceRuntime.RegisterServiceAsync("HPP.WarmUpProvider.MainType",
                    context => new Service(context)).GetAwaiter().GetResult();

                ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(Service).Name);

                // Prevents this host process from terminating so services keep running.
                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
                throw;
            }
        }

        private static void InitializeInstrumentationSettings()
        {
            string appInsightInstrumentationKey = ConfigurationManager.AppSettings["InstrumentationKey"];
            if (!string.IsNullOrEmpty(appInsightInstrumentationKey))
            {
                Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration.Active.InstrumentationKey = appInsightInstrumentationKey;
            }
        }
    }
}
