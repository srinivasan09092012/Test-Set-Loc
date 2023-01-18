
using HPE.HSP.UA3.Core.API.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Timers;

namespace UXWarmUpService
{
    public partial class UXWarmUp : ServiceBase
    {
        private Timer warmUpEndpointTimer;
        public UXWarmUp()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            LoggerManager.Logger.LogInformational("UX Warm Up Utilty Service Starting...");
            this.SetUXWarmUpEndpointTimer();
        }

        private void SetUXWarmUpEndpointTimer()
        {
            double interval1 = Int32.Parse(ConfigurationManager.AppSettings["WarmUpEndpointIntervalSeconds"]);
            double interval = 300000;
            this.warmUpEndpointTimer = new Timer(interval);
            this.warmUpEndpointTimer.AutoReset = true;
            this.warmUpEndpointTimer.Elapsed += new ElapsedEventHandler(this.WarmUpEndpoints);
            this.warmUpEndpointTimer.Start();
        }
        private void WarmUpEndpoints(object sender, ElapsedEventArgs e)
        {
            List<string> modules = new List<string>()
            {
            "Core",
            "MemberPortal",
            "ProviderPortal",
            "IdentityManagement",
            "ProgramIntegrity",
            "ProgramIntegrityCT",
            "ProviderEnrollment",
            "ProviderManagement"
            };

            foreach (var item in modules)
            {
                string url = string.Format("WarmUp/Index?moduleName=" + item);
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost.dev.mapshc.com/");
                        client.Timeout = TimeSpan.FromSeconds(2);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        using (HttpResponseMessage response = client.GetAsync(url).Result)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                            }
                            else
                            {
                            }
                        }
                    }
                }
                catch
                {
                }
            }
        }

        protected override void OnStop()
        {
            LoggerManager.Logger.LogInformational("UX Warm Up Utilty Service Stoped...");
        }
    }
}
