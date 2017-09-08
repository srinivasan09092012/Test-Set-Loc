using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LoadReferenceData
{
    class Program
    {
        private static bool overallErrors = false;

        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Data Load started for " + args[1] + "' ..... ");
                try
                {
                    if (args[1].ToLower().Contains("helpnodelocale"))
                    {
                        LoadHelpQuery(args[0], args[1]);
                    }
                    else
                    {
                        LoadQuery(args[0], args[1]);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("*** Task={0}, ERROR={1} ***", args[1], e.Message);
                    overallErrors = true;
                }
            }
            else
            {
                string configTenantID = ConfigurationManager.AppSettings["TenantID"];
                List<string> tenantList = new List<string>();

                if (string.IsNullOrEmpty(configTenantID))
                {
                    //tenantList = call ODATA to get list of tenants
                }
                else
                {
                    Guid validTenandID;
                    if (Guid.TryParse(configTenantID, out validTenandID))
                    {
                        tenantList.Add(configTenantID);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Guid for TenantID '" + configTenantID + "' in app.config.");
                    }
                }

                foreach (string tenantID in tenantList)
                {
                    //LoadSync();
                    LoadAsync(tenantID);
                }
            }
            Console.WriteLine("Data Load completed ..... ");
            if (overallErrors)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*** THERE ARE ERRORS WHILE LOADING  ***");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Read();
            }
        }

        private static void LoadAsync(string tenantID)
        {
            Console.WriteLine("Data Load started for TenantID '" + tenantID + "' ..... ");
            Stopwatch watch = new Stopwatch();
            watch.Start();

            bool errorFlag = false;
            List<Task> tasks = new List<Task>();
            Action<string, Task> action =
            (str, t) =>
            {
                if (t.Exception != null)
                {
                    errorFlag = true;
                    Console.WriteLine("*** Task={0}, ERROR={1} / {2} ***", str, t.Exception.Message, t.Exception.InnerException.Message);
                }
            };

            LoadQuery("Tenants", "Tenants");

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("HtmlBlock(TenantID=" + tenantID + ")?$expand=HtmlBlockLanguages", "HtmlBlock");
            }).ContinueWith(c => action("HtmlBlock", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("SecurityCodes(TenantID=" + tenantID + ")?$expand=Children,Attributes", "SecurityCodes");
            }).ContinueWith(c => action("SecurityCodes", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("ReferenceCodes(" + tenantID + ")?$expand=Children,Attributes", "ReferenceCodes");
            }).ContinueWith(c => action("ReferenceCodes", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Image(TenantID=" + tenantID + ")?$expand=ImageLanguages", "Image");
            }).ContinueWith(c => action("Image", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Messages(TenantID=" + tenantID + ")?$expand=Attributes", "Messages");
            }).ContinueWith(c => action("Messages", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Application", "Application");
            }).ContinueWith(c => action("Application", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("TenantModule(TenantID=" + tenantID + ")", "TenantModule");
            }).ContinueWith(c => action("TenantModule", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Module", "Module");
            }).ContinueWith(c => action("Module", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("ModelDefinition(TenantID=" + tenantID + ")?$expand=ModelProperties", "ModelDefinition");
            }).ContinueWith(c => action("ModelDefinition", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Menu(TenantID=" + tenantID + ")?$expand=Children", "Menu");
            }).ContinueWith(c => action("Menu", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Labels(TenantID=" + tenantID + ")", "Labels");
            }).ContinueWith(c => action("Labels", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Service(TenantID=" + tenantID + ")", "Service");
            }).ContinueWith(c => action("Service", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("AppSetting(TenantID=" + tenantID + ")", "AppSetting");
            }).ContinueWith(c => action("AppSetting", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadHelpQuery("HelpNodeLocale(TenantID=" + tenantID + ")", "HelpNodeLocale");
            }).ContinueWith(c => action("HelpNodeLocale", c)));

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Runtime"]))
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    LoadQuery("DataList(TenantID=" + tenantID + ")", "DataList");
                }).ContinueWith(c => action("DataList", c)));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("Data Load completed for TenantID '" + tenantID + "' ..... ");
            watch.Stop();
            PrintTimeTaken(watch, "Tenant");
            if(errorFlag)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*** THERE ARE ERRORS WHILE LOADING THIS TENANT ***");
                Console.ForegroundColor = ConsoleColor.Gray;
                overallErrors = true;
            }
        }

        private static void LoadQuery(string query, string queryType)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery(query);
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, queryType);
            stopWatch.Reset();
        }

        private static void LoadHelpQuery(string query, string queryType)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            LoadHelpNodeHelper.ExecuteODataQuery(query);
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, queryType);
            stopWatch.Reset();
        }

        private static void LoadSync(string tenantID)
        {
            Console.WriteLine("Data Load started..... ");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("HtmlBlock(TenantID=" + tenantID + ")?$expand=HtmlBlockLanguages");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "HtmlBlock");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("SecurityCodes(TenantID=" + tenantID + ")?$expand=Children,Attributes");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "SecurityCodes");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("ReferenceCodes(" + tenantID + ")?$expand=Children,Attributes");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "ReferenceCodes");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("Image(TenantID=" + tenantID + ")?$expand=ImageLanguages");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "Image");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("Application");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "Application");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("TenantModule(TenantID=" + tenantID + ")");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "TenantModule");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("Module");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "Module");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("ModelDefinition(TenantID=" + tenantID + ")?$expand=ModelProperties");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "ModelDefinition");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("Menu(TenantID=" + tenantID + ")?$expand=Children");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "Menu");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("Service(TenantID=" + tenantID + ")");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "Service");
            Console.WriteLine("Data Load completed..... ");
            Console.Read();
        }

        private static void PrintTimeTaken(Stopwatch watch, string query)
        {
            TimeSpan ts = watch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("Time Taken for " + query + ": " + elapsedTime);
            watch.Reset();
        }
    }
}
