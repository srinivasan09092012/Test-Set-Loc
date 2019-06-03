using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;
using Microsoft.Web.Administration;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Management;

namespace Watchdog
{
    public class AppValidation
    {
        string slogmsg = string.Empty;
        int iRetry = 0;
        string sAppPoolname = ConfigurationManager.AppSettings["AppPoolName"].ToString();
        string oDataURL = ConfigurationManager.AppSettings["ODATAURL"].ToString();
        int iRequestTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["RequestTimeout"]);
        int iRetryCount = Convert.ToInt32(ConfigurationManager.AppSettings["Retry"]);
        public bool URLValidation()
        {
            WebResponse response = null;
            WebRequest request = null;

            try
            {
                request = WebRequest.Create(oDataURL);
                request.Timeout = iRequestTimeout;
                response = request.GetResponse();

                //slogmsg = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + Environment.NewLine + "Service Name: " + sAppPoolname + " is working fine" + Environment.NewLine;
                //LogEntry(slogmsg);
                //slogmsg = "";

            }
            catch (Exception ex)
            {
                string sException = ex.Message.ToString();
                if (sException.Contains("401"))
                    return true;//ignore the error
                else if (sException.Contains("The operation has timed out") || sException.Contains("503")) //|| sException.Contains("404"))
                {
                    if (iRetry < iRetryCount)
                    {
                        iRetry++;
                        URLValidation();
                    }
                    else
                    {
                        slogmsg = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + Environment.NewLine + "Service Name: " + sAppPoolname + " is unresponsive" + Environment.NewLine;
                        slogmsg += "Exception: " + sException;
                        LogEntry(slogmsg);
                        
                    }
                    return false;
                }
            }
            return true;
        }
        public bool RecycleApplicationPool(string serverName, string appPoolName)
        {
            if (!string.IsNullOrEmpty(serverName) && !string.IsNullOrEmpty(appPoolName))
            {
                try
                {
                    using (ServerManager manager = ServerManager.OpenRemote(serverName))
                    {
                        ApplicationPool appPool = manager.ApplicationPools.FirstOrDefault(ap => ap.Name == appPoolName);

                        if (appPool != null)
                        {
                            //Get the current state of the app pool
                            bool appPoolRunning = appPool.State == ObjectState.Started || appPool.State == ObjectState.Starting;
                            bool appPoolStopped = appPool.State == ObjectState.Stopped || appPool.State == ObjectState.Stopping;

                            //The app pool is running, so stop it first.
                            if (appPoolRunning)
                            {
                                //Wait for the app to finish before trying to stop
                                while (appPool.State == ObjectState.Starting) { System.Threading.Thread.Sleep(1000); }

                                //Stop the app if it isn't already stopped
                                if (appPool.State != ObjectState.Stopped)
                                {
                                    appPool.Stop();
                                }
                                appPoolStopped = true;
                            }

                            //Only try restart the app pool if it was running in the first place, because there may be a reason it was not started.
                            if (appPoolStopped && appPoolRunning)
                            {
                                //Wait for the app to finish before trying to start
                                while (appPool.State == ObjectState.Stopping) { System.Threading.Thread.Sleep(1000); }

                                //Start the app
                                appPool.Start();

                                slogmsg = "Service Name: " + appPoolName + " has been recycled successfully" + Environment.NewLine;
                                LogEntry(slogmsg);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    slogmsg = string.Format("Unable to restart the application pool for {0}", appPoolName) + Environment.NewLine + "Exception: " + ex.Message.ToString() + Environment.NewLine;
                    LogEntry(slogmsg);
                    //throw new Exception(string.Format("Unable to restart the application pools for {0}.{1}", serverName, appPoolName), ex.InnerException);
                    return false;
                }
            }
            
            return true;
        }
        public void GetCpuMemoryUsgae(string AppPoolName)
        {
            int i = 0;
            double cpuUsag;
            double memUseage;
            foreach (Process proc in Process.GetProcessesByName("w3wp"))
            {
                try
                {
                    string sProcessName = GetCommandLineOfProcessID(proc.Id);
                    
                    if (sProcessName == AppPoolName)
                    {
                        using (PerformanceCounter pcProcess = new PerformanceCounter("Process", "% Processor Time", proc.ProcessName))
                        using (PerformanceCounter memPerformanceCounter = new PerformanceCounter("Memory", "Available MBytes"))
                        {
                            pcProcess.NextValue();
                            memPerformanceCounter.NextValue();
                            System.Threading.Thread.Sleep(1000);
                            cpuUsag = Math.Round(pcProcess.NextValue(), 1);
                            cpuUsag = cpuUsag / Environment.ProcessorCount;
                            memUseage = proc.PrivateMemorySize64 / (1024 * 1024);
                            
                        }
                        
                        slogmsg = "CPU: " + cpuUsag + Environment.NewLine + "Memory: " + memUseage + Environment.NewLine;
                        LogEntry(slogmsg);

                        proc.Kill(); //kill w3wp process
                    }
                }
                catch (Exception ex)
                {
                }
                
            }

        }
        static string GetCommandLineOfProcessID(int processID)
        {
            var scope = new ManagementScope(@"\\localhost\root\cimv2");
            var query = new SelectQuery("SELECT * FROM Win32_Process where Name = 'w3wp.exe'");
            using (var searcher = new ManagementObjectSearcher(scope, query))
            {
                foreach (ManagementObject process in searcher.Get())
                {
                    var commandLine = process["CommandLine"].ToString();
                    var pid = process["ProcessId"].ToString();
                    if (processID.ToString() == pid)
                    {
                        // This will print the command line which will look something like that:
                        // c:\windows\system32\inetsrv\w3wp.exe -a \\.\pipe\iisipm49f1522c-f73a-4375-9236-0d63fb4ecfce -t 20 -ap "NAME_OF_THE_APP_POOL"
                        //Console.WriteLine("{0} - {1}", pid, commandLine);

                        return commandLine.Split(' ')[2].Split('"')[1];
                    }
                }
            }
            return "";
        }
        public void LogEntry(string logMessage)
        {
            string m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.WriteLine("{0} ", logMessage);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
