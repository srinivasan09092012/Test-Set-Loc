﻿using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace UXWarmUpService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
            ServiceInstaller serviceInstaller = (ServiceInstaller)sender;

            using (ServiceController sc = new ServiceController(serviceInstaller.ServiceName))
            {
                sc.Start();
            }
        }
    }
}
