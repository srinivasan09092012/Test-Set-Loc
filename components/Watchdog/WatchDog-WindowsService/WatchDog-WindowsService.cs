using System;
using System.Configuration;
using System.IO;
using System.Management;
using System.ServiceProcess;
using System.Threading;
using System.Timers;
using Watchdog;

namespace WatchDog_WindowsService
{
    public partial class WatchDog : ServiceBase
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        public WatchDog()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {            
            this.WriteToFile("Windows Service has been started");
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 60000;
            timer.Enabled = true;
        }

        protected override void OnStop()
        {            
            this.WriteToFile("Service stopped");
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            timer.Enabled = false;
            DateTime serverStartTime = new DateTime();
            while (true)
            {
                string query = "SELECT * FROM Win32_OperatingSystem";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                foreach (ManagementObject managementObj in searcher.Get())
                {
                    string serverBootUpTime = managementObj.Properties["LastBootUpTime"].Value.ToString();
                    serverStartTime = ManagementDateTimeConverter.ToDateTime(serverBootUpTime).AddMinutes(15);
                }
                bool iis = IsIISRunning();
                if (DateTime.Now > serverStartTime && iis == true)
                {
                    this.WriteToFile("Starting Watch-Dog :- " + DateTime.Now);
                    WatchDogProgram.Main(null);
                    this.WriteToFile("Watch-Dog Program Ended :- " + DateTime.Now);
                    Thread.Sleep(10000);
                }               
            }            
        }

        private static bool IsIISRunning()
        {
            bool isRunning = false;

            ServiceController controller = new ServiceController("W3SVC");

            if (controller.Status == ServiceControllerStatus.Running)
            {
                isRunning = true;
            }

            return isRunning;
        }

        private void WriteToFile(string text)
        {
            string path = ConfigurationManager.AppSettings["FilePath"].ToString();                
            string filename = "Windows-Service-Log.txt";
            if (!Directory.Exists(path))                
                Directory.CreateDirectory(path);                                        
            using (StreamWriter writer = new StreamWriter(path + filename, true))
            {
                writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                writer.Close();
            }
        }
    }
}
