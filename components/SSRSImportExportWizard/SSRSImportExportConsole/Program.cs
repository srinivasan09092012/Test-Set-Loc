using HPE.HSP.UA3.Core.API.Logger;
using SSRSImportExportConsole.ReportServer2010;
using System;
using System.Net;
using System.Web.Services.Protocols;

namespace SSRSImportExportConsole
{
    class Program
    {
        public static string ReportURL { get; set; }

        public static string UserName { get; set; }

        public static string Password { get; set; }

        public static string UploadPath { get; set; }

        public static ReportingService2010 ReportServer { get; set; }

        static void Main(string[] args)
        {
            if(args.Length == 4)
            {
                ReportURL = args[0].Trim();
                UserName = args[1].Trim();
                Password = args[2].Trim();
                UploadPath = args[3].Trim();
                ReportServer = new ReportingService2010();
            }
            else
            {
                LoggerManager.Logger.LogFatal("The target url, username, password and download path are required. Process terminated");
                return;
            }

            if (string.IsNullOrEmpty(ReportURL) || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(UploadPath))
            {
                LoggerManager.Logger.LogFatal("The target url, username, password and download path are required");
            }
            else
            {
                if (ConnectReportServer())
                {
                    new ImportReportItems(ReportServer, UploadPath);
                    new UpdateDataSource(ReportServer, string.Empty);
                }
            }
        }

        private static bool ConnectReportServer()
        {
            bool returnFlag = false;
            ReportServer.Url = ReportURL;
            NetworkCredential cred = new NetworkCredential(UserName, Password);
            ReportServer.Credentials = cred;
            try
            {
                CatalogItem[] items = ReportServer.ListChildren(@"/", false);
                return true;
            }
            catch (SoapException)
            {
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogFatal("Error while connecting:" + ex.ToString());
            }

            return returnFlag;
        }
    }
}
