//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HPE.HSP.UA3.Core.API.Logger;
using System;
using System.Configuration;
using WarmUpProvider.Helpers;

namespace WarmUpProvider.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                System.Console.WriteLine("Starting warming up endpoints........");
                BASUnityContainer.Initialize();
                InitializeInstrumentationSettings();
                new WarmUpHelper().StartUp();
                System.Console.WriteLine("Warming up endpoints completed. All logs are available under 'Logs\\Utility\\Warm Up Provider Console' folder.");
                System.Console.ReadLine();
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogWarning("Exception thrown : ", ex);
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
