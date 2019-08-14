using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using logAnalyticsSearch1;
using Microsoft.Azure.OperationalInsights;
using Microsoft.Azure.OperationalInsights.Models;
using Microsoft.Rest.Azure.Authentication;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace QuerySample
{
    public class HPPLogFormat
    {
        public string TimeGenearted { get; set; }
        public string Source { get; set; }
        public string Computer { get; set; }
        public string TenantName { get; set; }
        public string TenantID { get; set; }
        public string RenderedDescription { get; set; }
    }
    public class UploadEventLogsToEvidenceVault
    {
        OperationalInsightsDataClient omsClient = null;
        public void Authenticate()
        {
            var aadDomainName = ConfigurationManager.AppSettings["aadDomainName"];
            var clientId = ConfigurationManager.AppSettings["clientId"];
            var clientSecret = ConfigurationManager.AppSettings["clientSecret"];
            var workspaceId = ConfigurationManager.AppSettings["workspaceId"];
            var authEndpoint = "https://login.microsoftonline.com";
            var tokenAudience = "https://api.loganalytics.io/";

            var adSettings = new ActiveDirectoryServiceSettings
            {
                AuthenticationEndpoint = new Uri(authEndpoint),
                TokenAudience = new Uri(tokenAudience),
                ValidateAuthority = true
            };

            var creds = ApplicationTokenProvider.LoginSilentAsync(aadDomainName, clientId, clientSecret, adSettings).GetAwaiter().GetResult();

            omsClient = new OperationalInsightsDataClient(creds);
            omsClient.WorkspaceId = workspaceId;
        }
        public void UploadSharedEventLogsToEvidenceVault(HppTenants tenantInfo)
        {
            //Query all the logs which do not have event sourcename starts with BAS, BATCH, HPP. ie, general appplication events only
            string queryString = string.Format(@"Event | where TimeGenerated > ago(24h) | where EventLevelName in (""Error"") 
                    | where  _ResourceId contains""{0}""
                    | where Source !contains_cs(""BAS "") and Source !contains_cs(""BATCH "") and Source !contains_cs(""HPP "")
                    | project TimeGenerated, Source, Computer, TenantID=""Shared"", TenantName=""Shared"", RenderedDescription", tenantInfo.sharedResourceGroupName);

            Console.WriteLine("\n{0}", queryString);
            Console.WriteLine("\nQuerying...Wait!");
            var results = omsClient.Query(queryString);
            List<QueryResults> queryResults = new List<QueryResults>();
            queryResults.Add(results);
            UploadEventLogs(tenantInfo, queryResults, -1, "System.Json");
        }
        
        public void UploadTenantSpecificEventLogsToEvidenceVault(HppTenants tenantInfo, int indexTenant)
        {
            List<QueryResults> queryResults = new List<QueryResults>();
            // query all HPP specific EventLogs from shared resourcegroup
            string queryString = string.Format(@"Event | where TimeGenerated > ago(24h) | where EventLevelName in (""Error"") 
                            | where _ResourceId contains ""{0}""
                            | where Source contains_cs(""BAS "") or Source contains_cs(""BATCH "") or Source contains_cs(""HPP "")
                            | where RenderedDescription contains ""{1}""
                            | project TimeGenerated, Source, Computer, TenantID=""{2}"", TenantName=""{3}"", RenderedDescription", 
                            tenantInfo.sharedResourceGroupName, tenantInfo.hppTenants[indexTenant].hppTenantID, tenantInfo.hppTenants[indexTenant].hppTenantID, tenantInfo.hppTenants[indexTenant].hppTenantName);

            Console.WriteLine("\n{0}", queryString);
            Console.WriteLine("\nQuerying...Wait!");
            var results = omsClient.Query(queryString);
            Console.WriteLine("\nQuerying Successful! and Downloading...!");
            queryResults.Add(results);

            // query all all EventLogs from Tenantspecific Resourcegroup
            queryString = string.Format(@"Event | where TimeGenerated > ago(24h) | where EventLevelName in (""Error"") 
                    | where  _ResourceId contains""{0}""
                    | project TimeGenerated, Source, Computer, TenantID=""{1}"", TenantName=""{2}"",RenderedDescription", 
                    tenantInfo.hppTenants[indexTenant].resourceGroupName, tenantInfo.hppTenants[indexTenant].hppTenantID, tenantInfo.hppTenants[indexTenant].hppTenantName);

            Console.WriteLine("\n{0}", queryString);
            Console.WriteLine("\nQuerying...Wait!");
            var results1 = omsClient.Query(queryString);
            queryResults.Add(results1);
            UploadEventLogs(tenantInfo, queryResults, indexTenant, "HPP.Json");

            UploadTenantSpecificK2EventLogsToEvidenceVault(tenantInfo, indexTenant);
            UploadTenantSpecificInRuleEventLogsToEvidenceVault(tenantInfo, indexTenant);
            UploadTenantSpecificSqlServerEventLogsToEvidenceVault(tenantInfo, indexTenant);
            UploadTenantSpecificRedisEventLogsToEvidenceVault(tenantInfo, indexTenant);
            UploadTenantSpecificIISHostToEvidenceVault(tenantInfo, indexTenant);
            UploadTenantSpecificIISLogsToEvidenceVault(tenantInfo, indexTenant);
        }
        public void UploadEventLogs(HppTenants tenantInfo, List<QueryResults> queryResultsList, int indexTenant, string fileName)
        {
            List<HPPLogFormat> hppLogFormat = new List<HPPLogFormat>();
            foreach (QueryResults queryResults in queryResultsList)
            {
                if (queryResults.Tables.Count > 0)
                {
                    HPPLogFormat log = new HPPLogFormat();
                    int i = 0;
                    foreach (var row in queryResults.Tables[0].Rows)
                    {
                        log = new HPPLogFormat();
                        log.TimeGenearted = row[0];
                        log.Source = row[1];
                        log.Computer = row[2];
                        log.TenantID = row[3];
                        log.TenantName = row[4];
                        log.RenderedDescription = row[5];
                        hppLogFormat.Add(log);
                        i++;
                    }
                }
            }
            string logJson = JsonConvert.SerializeObject(hppLogFormat.ToArray(),
                                        new JsonSerializerSettings
                                        {
                                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                                        });
            byte[] byteArray = Encoding.ASCII.GetBytes(logJson);
            var filestream = new MemoryStream(byteArray);
            Console.WriteLine("Uploading to Evidence Vault - Started...");
            UploadToEvidenceVault(filestream, tenantInfo, indexTenant, fileName);
            Console.WriteLine("Uploading to Evidence Vault - Ended.");
        }
        private void UploadToEvidenceVault(Stream filestream, HppTenants tenantInfo, int indexTenant, string fileName)
        {
            String strorageconn = "";
            string blobContainerName = "";
            if (indexTenant == -1)
            {
                strorageconn = tenantInfo.sharedStorageconnectionstring;
                blobContainerName = tenantInfo.sharedBlobContainerName;
            }
            else
            {
                strorageconn = tenantInfo.hppTenants[indexTenant].storageconnectionstring;
                blobContainerName = tenantInfo.hppTenants[indexTenant].blobContainerName;
            }
            CloudStorageAccount storageacc = CloudStorageAccount.Parse(strorageconn);

            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(blobContainerName);
            container.CreateIfNotExists();

            string blobName = String.Format("{0:D2}{1:D2}{2:D2} {3:D2}{4:D2}{5:D2}- " + fileName,
                                                                                            int.Parse(DateTime.UtcNow.Year.ToString()),
                                                                                            int.Parse(DateTime.UtcNow.Month.ToString()),
                                                                                            int.Parse(DateTime.UtcNow.Day.ToString()),
                                                                                            int.Parse(DateTime.UtcNow.Hour.ToString()),
                                                                                            int.Parse(DateTime.UtcNow.Minute.ToString()),
                                                                                            int.Parse(DateTime.UtcNow.Second.ToString()));
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(DateTime.UtcNow.Year.ToString() + @"\" + DateTime.UtcNow.ToString("MM") + @"\" + DateTime.UtcNow.ToString("dd") + @"\" + blobName);
            blockBlob.UploadFromStream(filestream);
        }
        public void UploadTenantSpecificK2EventLogsToEvidenceVault(HppTenants tenantInfo, int indexTenant)
        {
            string queryString = string.Format(@"Event | where TimeGenerated > ago(24h) | where EventLevelName in (""Error"") 
                    | where  _ResourceId contains""{0}""
                    | where Source contains_cs(""SourceCode.Logging.Extension.EventLogExtension"")
                    | project TimeGenerated, Source, Computer, TenantID=""{1}"", TenantName=""{2}"",RenderedDescription",
                    tenantInfo.hppTenants[indexTenant].resourceGroupName, tenantInfo.hppTenants[indexTenant].hppTenantID, tenantInfo.hppTenants[indexTenant].hppTenantName);
            Console.WriteLine("\n{0}", queryString);
            Console.WriteLine("\nQuerying...Wait!");
            var results = omsClient.Query(queryString);
            List<QueryResults> queryResults = new List<QueryResults>();
            queryResults.Add(results);
            UploadEventLogs(tenantInfo, queryResults, indexTenant, "K2.Json");
        }
        public void UploadTenantSpecificInRuleEventLogsToEvidenceVault(HppTenants tenantInfo, int indexTenant)
        {
            string queryString = string.Format(@"Event | where TimeGenerated > ago(24h) | where EventLevelName in (""Error"") 
                    | where  _ResourceId contains""{0}""
                    | where Source contains_cs(""InRule"")
                    | project TimeGenerated, Source, Computer, TenantID=""{1}"", TenantName=""{2}"",RenderedDescription",
                    tenantInfo.hppTenants[indexTenant].resourceGroupName, tenantInfo.hppTenants[indexTenant].hppTenantID, tenantInfo.hppTenants[indexTenant].hppTenantName);
            Console.WriteLine("\n{0}", queryString);
            Console.WriteLine("\nQuerying...Wait!");
            var results = omsClient.Query(queryString);
            List<QueryResults> queryResults = new List<QueryResults>();
            queryResults.Add(results);
            UploadEventLogs(tenantInfo, queryResults, indexTenant, "InRule.Json");
        }
        public void UploadTenantSpecificSqlServerEventLogsToEvidenceVault(HppTenants tenantInfo, int indexTenant)
        {
            string queryString = string.Format(@"Event | where TimeGenerated > ago(24h) | where EventLevelName in (""Error"") 
                    | where  _ResourceId contains""{0}""
                    | where Source contains_cs(""MSSQLSERVER"")
                    | project TimeGenerated, Source, Computer, TenantID=""{1}"", TenantName=""{2}"",RenderedDescription",
                    tenantInfo.hppTenants[indexTenant].resourceGroupName, tenantInfo.hppTenants[indexTenant].hppTenantID, tenantInfo.hppTenants[indexTenant].hppTenantName);
            Console.WriteLine("\n{0}", queryString);
            Console.WriteLine("\nQuerying...Wait!");
            var results = omsClient.Query(queryString);
            List<QueryResults> queryResults = new List<QueryResults>();
            queryResults.Add(results);
            UploadEventLogs(tenantInfo, queryResults, indexTenant, "MSSQLSERVER.Json");
        }
        public void UploadTenantSpecificRedisEventLogsToEvidenceVault(HppTenants tenantInfo, int indexTenant)
        {
            string queryString = string.Format(@"Event | where TimeGenerated > ago(24h) | where EventLevelName in (""Error"") 
                    | where  _ResourceId contains""{0}""
                    | where Source contains_cs(""MSSQLSERVER"")
                    | project TimeGenerated, Source, Computer, TenantID=""{1}"", TenantName=""{2}"",RenderedDescription",
                    tenantInfo.hppTenants[indexTenant].resourceGroupName, tenantInfo.hppTenants[indexTenant].hppTenantID, tenantInfo.hppTenants[indexTenant].hppTenantName);
            Console.WriteLine("\n{0}", queryString);
            Console.WriteLine("\nQuerying...Wait!");
            var results = omsClient.Query(queryString);
            List<QueryResults> queryResults = new List<QueryResults>();
            queryResults.Add(results);
            UploadEventLogs(tenantInfo, queryResults, indexTenant, "Redis.Json");
        }
        public void UploadTenantSpecificIISHostToEvidenceVault(HppTenants tenantInfo, int indexTenant)
        {
            //W3CIISLog
            //| where Computer contains "ux"
            //| where _ResourceId
            //| project Computer, csMethod, csUriStem, csUriQuery, sIP,  sPort, csUserName, cIP, csUserAgent , scStatus , scSubStatus, scWin32Status, TimeTaken

            string queryString = string.Format(@"Event | where TimeGenerated > ago(24h) | where EventLevelName in (""Error"") 
                    | where  _ResourceId contains""{0}""
                    | where Source contains_cs(""IIS"")
                    | project TimeGenerated, Source, Computer, TenantID=""{1}"", TenantName=""{2}"",RenderedDescription",
                    tenantInfo.sharedResourceGroupName, tenantInfo.hppTenants[indexTenant].hppTenantID, tenantInfo.hppTenants[indexTenant].hppTenantName);
            Console.WriteLine("\n{0}", queryString);
            Console.WriteLine("\nQuerying...Wait!");
            var results = omsClient.Query(queryString);
            List<QueryResults> queryResults = new List<QueryResults>();
            queryResults.Add(results);
            UploadEventLogs(tenantInfo, queryResults, indexTenant, "IISHost.Json");
        }
        public void UploadTenantSpecificIISLogsToEvidenceVault(HppTenants tenantInfo, int indexTenant)
        {
            string queryString = string.Format(@"W3CIISLog
                        | where  _ResourceId contains""{0}"" and (Computer contains ""uxi"" or Computer contains ""uxe"")
                        | project TimeGenerated, Computer, sSiteName, sIP, csMethod, csUriStem, csUriQuery, sPort, csUserName, csReferer, scStatus, TimeTaken",
                        tenantInfo.sharedResourceGroupName);
            Console.WriteLine("\n{0}", queryString);
            Console.WriteLine("\nQuerying...Wait!");
            var results = omsClient.Query(queryString);
            List<QueryResults> queryResults = new List<QueryResults>();
            queryResults.Add(results);
            UploadEventLogs(tenantInfo, queryResults, indexTenant, "IISlogs.Json");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            //1. Deserialize TenantConfig file to HppTenants Object
            HppTenants hppTenants = JsonConvert.DeserializeObject<HppTenants>(File.ReadAllText(@"TenantConfig.json"));

            UploadEventLogsToEvidenceVault uploadEventLogsToEvidenceVault = new UploadEventLogsToEvidenceVault();
            Console.WriteLine("Authenticating against Azure AD...");

            //2. Authenticate
            uploadEventLogsToEvidenceVault.Authenticate();

            //3. Upload Shared Exception Logs to EV
            Console.WriteLine("{0}. Querying Log Analytics...for Shared Resource group");
            uploadEventLogsToEvidenceVault.UploadSharedEventLogsToEvidenceVault(hppTenants);
            
            //4. Upload Tenant Specific Exception Logs to EV
            int i = 0;
            foreach (var tenantInfo in hppTenants.hppTenants)
            {
                Console.WriteLine("\n=============================================");
                Console.WriteLine("{0}. Querying Log Analytics...for Tenant = " + tenantInfo.hppTenantName);
                uploadEventLogsToEvidenceVault.UploadTenantSpecificEventLogsToEvidenceVault(hppTenants, i);
                ++i;
            }           
            Console.WriteLine("\n================END=============================");
            //Console.Read();
        }
    }
}