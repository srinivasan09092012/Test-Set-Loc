using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoadReferenceData
{
    class Program
    {
        static void Main(string[] args)
        {
            //LoadSync();
            LoadAsync();
        }

        private static void LoadAsync()
        {
            List<Task> tasks = new List<Task>();

            Console.WriteLine("Data Load started..... ");
            Stopwatch watch = new Stopwatch();
            watch.Start();

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("HtmlBlock?$expand=HtmlBlockLanguages", "HtmlBlock");
            }));
            
            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("SecurityCodes?$expand=Children,Attributes", "SecurityCodes");
            }));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("ReferenceCodes?$expand=Children,Attributes", "ReferenceCodes");
            }));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Image?$expand=ImageLanguages", "Image");
            }));
           
            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Application", "Application");
            }));
            
            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("TenantModule", "TenantModule");
            }));
            
            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Module", "Module");
            }));
            
            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("ModelDefintion?$expand=ModelProperties", "ModelDefintion");
            }));
            
            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Menu?$expand=Children", "Menu");
            }));
           
            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("Service", "Service");
            }));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadQuery("AppSetting", "AppSetting");
            }));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LoadHelpQuery("HelpNodeLocale", "HelpNodeLocale");
            }));

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("Data Load completed..... ");
            watch.Stop();
            PrintTimeTaken(watch, string.Empty);
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
