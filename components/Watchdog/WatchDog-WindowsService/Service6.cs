using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Watchdog;
using HPE.HSP.UA3.Core.API.Logger;

namespace WatchDog_WindowsService
{
    public partial class Service6 : ServiceBase
    {
        public Service6()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            System.Diagnostics.Debugger.Launch();
            WatchDogProgram monitor = new WatchDogProgram();
            WatchDogProgram.Main(null);
        }

        protected override void OnStop()
        {
        }
    }
}
