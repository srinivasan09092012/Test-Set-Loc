using Microsoft.Win32;
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
        private System.Timers.Timer timer = new System.Timers.Timer();
        private const string IIS_REG_KEY = @"Software\Microsoft\InetStp";

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
                
                //Start watchdog only 15 minutes after server boot time and if IIS is installed then that also should be in running state
                if (DateTime.Now > serverStartTime)
                {
                    if (!IsIISInstalled())
                    {
                        //If IIS is not installed then start the watchdog
                        RunWatchDogApplication();
                    }
                    else
                    {
                        ServiceController controller = new ServiceController("W3SVC");
                        ////If IIS is installed then check if IIS is up and running before starting watchdog.
                        if (controller != null)
                        {
                            if (controller.Status != ServiceControllerStatus.Running)
                            {
                                this.WriteToFile("W3SVC service is in " + controller.Status + " state. " + DateTime.Now);
                            }

                            RunWatchDogApplication();
                        }
                        else
                        {
                            this.WriteToFile("Watch-Dog can not start. W3SVC service is not available. " + DateTime.Now);
                        }
                    }
                }               
            }            
        }

        private void RunWatchDogApplication()
        {
            this.WriteToFile("Starting Watch-Dog :- " + DateTime.Now);
            WatchDogProgram.Main(null);
            this.WriteToFile("Watch-Dog Program Ended :- " + DateTime.Now);
            Thread.Sleep(10000);
        }

        private void WriteToFile(string text)
        {
            string path = ConfigurationManager.AppSettings["FilePath"].ToString();                
            string filename = "Windows-Service-Log.txt";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (StreamWriter writer = new StreamWriter(path + filename, true))
            {
                writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                writer.Close();
            }
        }

        public bool IsIISInstalled()
        {
            try
            {
                using (RegistryKey iisKey = Registry.LocalMachine.OpenSubKey(IIS_REG_KEY))
                {
                    return (int)iisKey.GetValue("MajorVersion") >= 6;
                }
            }
            catch(Exception ex)
            {
                this.WriteToFile("Error occured while validating IIS installation : " + ex.Message);
                return false;
            }
        }
    }
}
