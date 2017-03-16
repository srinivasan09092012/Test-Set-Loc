using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LoadReferenceData
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Data Load started..... ");
                if (args[1].ToLower() == "helpnodelocale")
                {
                    LoadHelpQuery(args[0], args[1]);
                }
                else
                {
                    LoadQuery(args[0], args[1]);
                }
            }
            else
            {
                //LoadSync();
                LoadAsync();
            }
        }

        private static void LoadAsync()
        {
            bool errorFlag = false;
            List<Task> tasks = new List<Task>();
            Action<string, Task> action =
            (str, t) =>
            {
                if (t.Exception != null)
                {
                    errorFlag = true;
                    Console.WriteLine();
                    Console.WriteLine("***** Task={0}, ERROR={1} *****", str, t.Exception.Message);
                    Console.WriteLine();
                }
            };

            Console.WriteLine("Data Load started..... ");
            Stopwatch watch = new Stopwatch();
            watch.Start();

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("HtmlBlock?$expand=HtmlBlockLanguages", "HtmlBlock");
            }).ContinueWith(c => action("HtmlBlock", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("SecurityCodes?$expand=Children,Attributes", "SecurityCodes");
            }).ContinueWith(c => action("SecurityCodes", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("ReferenceCodes?$expand=Children,Attributes", "ReferenceCodes");
            }).ContinueWith(c => action("ReferenceCodes", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Image?$expand=ImageLanguages", "Image");
            }).ContinueWith(c => action("Image", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Messages?$expand=Attributes", "Messages");
            }).ContinueWith(c => action("Messages", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Application", "Application");
            }).ContinueWith(c => action("Application", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("TenantModule", "TenantModule");
            }).ContinueWith(c => action("TenantModule", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Module", "Module");
            }).ContinueWith(c => action("Module", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("ModelDefintion?$expand=ModelProperties", "ModelDefintion");
            }).ContinueWith(c => action("ModelDefintion", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Menu?$expand=Children", "Menu");
            }).ContinueWith(c => action("Menu", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Labels", "Labels");
            }).ContinueWith(c => action("Labels", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Service", "Service");
            }).ContinueWith(c => action("Service", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("AppSetting", "AppSetting");
            }).ContinueWith(c => action("AppSetting", c)));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadHelpQuery("HelpNodeLocale", "HelpNodeLocale");
            }).ContinueWith(c => action("HelpNodeLocale", c)));

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Runtime"]))
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    LoadHelpQuery("DataList", "DataList");
                }).ContinueWith(c => action("DataList", c)));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("Data Load completed..... ");
            watch.Stop();
            PrintTimeTaken(watch, string.Empty);
            if(errorFlag)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("***** THERE ARE ERRORS WHILE LOADING *****");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Read();
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

        private static void LoadSync()
        {
            Console.WriteLine("Data Load started..... ");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("HtmlBlock?$expand=HtmlBlockLanguages");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "HtmlBlock");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("SecurityCodes?$expand=Children,Attributes");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "SecurityCodes");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("ReferenceCodes?$expand=Children,Attributes");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "ReferenceCodes");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("Image?$expand=ImageLanguages");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "Image");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("Application");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "Application");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("TenantModule");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "TenantModule");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("Module");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "Module");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("ModelDefintion?$expand=ModelProperties");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "ModelDefintion");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("Menu?$expand=Children");
            stopWatch.Stop();
            PrintTimeTaken(stopWatch, "Menu");
            stopWatch.Start();
            LoadTenantHelper.ExecuteODataQuery("Service");
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
