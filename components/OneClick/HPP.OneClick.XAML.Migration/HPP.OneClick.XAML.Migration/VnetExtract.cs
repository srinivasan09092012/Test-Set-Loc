using HPP.OneClick.XAML.Migration.Log4Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HPP.OneClick.XAML.Migration
{
    public class VnetExtract
    {
        private IOneClickLog log;
        public IOneClickLog Log
        {
            get
            {
                return this.log;
            }
        }
        public VnetExtract()
        {
            this.log = new OneClickLog(MethodBase.GetCurrentMethod().DeclaringType);
        }
        public void DownloadVnetData()
        {
            this.log.Info("Download Vnet Data called.");
            var sessionState = InitialSessionState.CreateDefault();
            this.log.Info("Execution policy Unrestricted.");
            sessionState.ExecutionPolicy = Microsoft.PowerShell.ExecutionPolicy.Unrestricted;

            this.log.Info("Download Vnet Data called.");
            using (PowerShell powershell = PowerShell.Create(sessionState))
            {
                this.log.Info("Find bin folder path.");
                string filename = System.Configuration.ConfigurationManager.AppSettings["PowershellFileName"];
                var GetDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                powershell.AddScript(string.Concat(GetDirectory,"\\", filename));
           
                var results = powershell.Invoke();

                if (powershell.HadErrors)
                {
                    foreach (var error in powershell.Streams.Error)
                    {
                        this.log.Info("Error : "+ error);
                    }
                }
                else
                {
                    foreach (var package in results)
                    {
                        Console.WriteLine(package);
                        this.log.Info("Success pkg list : " + package);
                    }
                    this.log.Info("Add VNet API Call : ");
                    try
                    {
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri("http://10.143.58.77/OneClick/");
                            string a = "Test";
                            var responseTask = client.PostAsJsonAsync<string>("AddVNet", a).Result;
                        }
                    }
                    catch(Exception ex)
                    {
                        this.log.Info("VNet API Call  error : " + ex.Message);
                    }
                }
            }
        }
    }
}
