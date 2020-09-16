//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
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
                BASUnityContainer.Initialize();
                InitializeInstrumentationSettings();
                LoggerManager.Logger.LogInformational("~~~~~~~~~~~~~~Starting to warm up provider configurations~~~~~~~~~~~~~~");
                WarmUpHelper warmUpHelper = new WarmUpHelper();
                warmUpHelper.StartUp();
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogWarning("Exception thrown : ", ex);
            }

            LoggerManager.Logger.LogInformational("~~~~~~~~~~~~~~Warming up of provider completed~~~~~~~~~~~~~~");
            System.Console.ReadLine();
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
