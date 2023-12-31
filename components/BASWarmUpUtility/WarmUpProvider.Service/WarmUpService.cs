﻿//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HPE.HSP.UA3.Core.API.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using WarmUpProvider.Helpers;

namespace WarmUpProvider.Service
{
    public partial class WarmUpService : ServiceBase
    {
        private Timer warmUpEndpointTimer;

        public WarmUpService()
        {
            InitializeComponent();
            InitializeInstrumentationSettings();
        }

        protected override void OnStart(string[] args)
        {
            LoggerManager.Logger.LogInformational("BAS Warm Up Utilty Service Starting...");
            BASUnityContainer.Initialize();
            this.OnLoad();
            this.SetWarmUpEndpointTimer();
            LoggerManager.Logger.LogInformational("BAS Warm Up Utilty Service Started");
        }

        private void SetWarmUpEndpointTimer()
        {
            double interval = Int32.Parse(ConfigurationManager.AppSettings["WarmUpEndpointIntervalSeconds"]);
            this.warmUpEndpointTimer = new Timer(interval * 1000);
            this.warmUpEndpointTimer.AutoReset = true;
            this.warmUpEndpointTimer.Elapsed += new ElapsedEventHandler(this.WarmUpEndpoints);
            this.warmUpEndpointTimer.Start();
        }

        private void WarmUpEndpoints(object sender, ElapsedEventArgs e)
        {
            List<Task> tasks = new List<Task>();
            WarmUpHelper warmUpProvider = new WarmUpHelper();
            tasks.Add(Task.Run(() => warmUpProvider.StartUp()));
            Task.WaitAll(tasks.ToArray());
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

        protected override void OnStop()
        {
            LoggerManager.Logger.LogWarning("BAS Warm Up Utilty Service has Stopped.");
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
