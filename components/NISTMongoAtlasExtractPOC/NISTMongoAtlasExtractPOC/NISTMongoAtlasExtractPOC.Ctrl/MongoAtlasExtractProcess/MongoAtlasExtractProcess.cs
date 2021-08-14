//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HPE.HSP.UA3.Core.API.Logger;
using ICSharpCode.SharpZipLib.GZip;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.IO.Compression;

namespace NISTMongoAtlasExtractPOC.Ctrl
{
    public class MongoAtlasExtractProcess
    {
        //// minimum date in ISO-8601 format, without times (YYYY-MM-DD)
        private string startDate = ConfigurationManager.AppSettings.Get("startDate_YYYY-MM-DD");
        //// maximum date in ISO-8601 format, without times (YYYY-MM-DD)
        private string endDate = ConfigurationManager.AppSettings.Get("endDate_YYYY-MM-DD");

        ////***************************************************************************************************************/
        //// Job Configuration parameters
        ////***************************************************************************************************************/
        //// local output folder for capturing response content
        private string localDir = ConfigurationManager.AppSettings.Get("localRootDir");
        private string reportDir = ConfigurationManager.AppSettings.Get("reportDir");

        ////***************************************************************************************************************/
        //// Global Configuration parameters
        ////***************************************************************************************************************/
        //// Base URL for Atlas API
        private string atlasBaseUrl = ConfigurationManager.AppSettings.Get("atlasBaseUrl");
        //// Atlas Organization ID.  This is globally defined for the entire DXC HPP subscription and shared between "product" environments and "SaaS" environments
        private string atlasOrgID = ConfigurationManager.AppSettings.Get("atlasOrgID");
        private string orgName = ConfigurationManager.AppSettings.Get("atlasOrgName");
        //// Atlas Project ID.  This correlates to an environment, i.e. Dev, Test, Staging, etc.
        //// date format for output log file prefix
        private string evidenceVaultDateFormat = ConfigurationManager.AppSettings.Get("evidenceVaultDateFormat");

        ////***************************************************************************************************************/
        //// Project/environment Configuration parameters
        ////***************************************************************************************************************/
        ////
        //// Defines the org level API key.  Note: This key has read/write/update/delete capability if not restricted.
        //// ************* IMPORTANT DBA INFO:  THIS API KEY MUST BE LIMITED TO "READ" PRIVILEGES ONLY IN ATLAS *************************************
        ////
        //// Atlas 'ORG LEVEL' API Public Key for invoking the REST call.  For DBA:  this key must be defined in Atlas at the "project" level, i.e. Dev, Test, Staging SaaS, etc
        private string atlasApiPublickey = ConfigurationManager.AppSettings.Get("atlasApiPublickey");
        //// API 'ORG LEVEL' Private Key for invoking the REST call.  For DBA:  this key must be defined in Atlas at the "project" level, i.e. Dev, Test, Staging SaaS, etc
        private string atlasApiPrivatekey = ConfigurationManager.AppSettings.Get("atlasApiPrivatekey");
        ////
        //// Defines the project level API key.  Note: This key has read/write/update/delete capability if not restricted.
        //// ************* IMPORTANT DBA INFO:  THIS API KEY MUST BE LIMITED TO "READ" PRIVILEGES ONLY IN ATLAS *************************************
        ////
        //// API 'PROJECT LEVEL' Public Key for invoking the REST call.  For DBA:  this key must be defined in Atlas at the "project" level, i.e. Dev, Test, Staging SaaS, etc
        private string projectApiPublickey = ConfigurationManager.AppSettings.Get("projectApiPublickey");
        //// API 'PROJECT LEVEL' Private Key for invoking the REST call.  For DBA:  this key must be defined in Atlas at the "project" level, i.e. Dev, Test, Staging SaaS, etc
        private string projectApiPrivatekey = ConfigurationManager.AppSettings.Get("projectApiPrivatekey");
        //// label for the entire atlas subscription 
        private string atlasProjectID = ConfigurationManager.AppSettings.Get("atlasProjectID");
        //// Atlas Project Name
        private string atlasProjectName = ConfigurationManager.AppSettings.Get("atlasProjectName");

        private List<HelperClasses.FilePath> extractDirs;

        public MongoAtlasExtractProcess(ref List<HelperClasses.FilePath> extractDirs)
        {
                this.extractDirs = extractDirs;

                // Determine end date if no value specified for end
                if (this.endDate == string.Empty)
            {
                //// only run for yesterday at latest to capture complete day
                DateTime endDT = DateTime.Now.AddDays(-1);
                this.endDate = endDT.Year + "-" + endDT.ToString("MM") + "-" + endDT.ToString("dd");
            }

            // Determine start date if no value specified for start
            if (this.startDate == string.Empty)
            {
                int counter = 0;
                bool dirLocated;
                do
                {
                    // determine most recent directory date
                    dirLocated = false;
                    counter = counter - 1;
                    DateTime pastDate = DateTime.Now.AddDays(counter);
                    string dirPastDate = this.localDir + pastDate.Year + "\\" + pastDate.ToString("MM") + "\\" + pastDate.ToString("dd") + "\\";
                    if (Directory.Exists(dirPastDate) == true)
                    {
                        dirLocated = true;
                        pastDate.AddDays(1);
                        this.startDate = pastDate.Year + "-" + pastDate.ToString("MM") + "-" + pastDate.ToString("dd");
                    }
                }
                while ((dirLocated = false) && (counter >= -30));

                //// default to -30 days from current date if no prior directory found in that range
                //// logs removed from Atlas after 30 days; play it safe and get everything
                if (this.startDate == string.Empty)
                {
                    DateTime startDT = DateTime.Now.AddDays(-30);
                    this.startDate = startDT.Year + "-" + startDT.ToString("MM") + "-" + startDT.ToString("dd");
                }
            }
        }

        public void FetchExtracts()
        {
            string fileName = string.Empty;
            string apiEndpoint = string.Empty;

            //// define a custom object that captures all properties of the current MongoDB Atlas project
            MongoProject project = new MongoProject(this.atlasProjectID, this.atlasProjectName);
            //// define a connection manager for the MongoDB Atlas API at "org" level
            AtlasConnector atlasOrgConnector = new AtlasConnector(this.atlasBaseUrl, this.atlasApiPublickey, this.atlasApiPrivatekey);
            //// define a connection manager for the MongoDB Atlas API at "project" level
            AtlasConnector atlasProjectConnector = new AtlasConnector(this.atlasBaseUrl, this.projectApiPublickey, this.projectApiPrivatekey);

            //// Place holder for current date as loop iterates thru multiple dates 
            string currentJobDate = string.Empty;
            //// Collection of all date values between StartDate and EndDate
            StringCollection jobDates = new StringCollection();
            //// Determine range of dates that job must run across
            DateTime minDateTime = DateTime.Parse(this.startDate);
            DateTime maxDateTime = DateTime.Parse(this.endDate);
            //// Check for current date; must only be run for prior dates
            if (maxDateTime.Date >= DateTime.Today)
            {
                throw new Exception("Invalid date range.  Start/End Date must not be current date.");
            }
            //// Check for illogical dates
            if (maxDateTime < minDateTime)
            {
                throw new Exception("Invalid Start/End Date Combination");
            }
            //// add initial date to list
            //// jobDates.Add(StartDate);

            int daysDiff = ((TimeSpan)(maxDateTime - minDateTime)).Days;
            for (int i = 0; i <= daysDiff; i++)
            {
                currentJobDate = minDateTime.AddDays(i).ToString("yyyy-MM-dd");
                jobDates.Add(currentJobDate);
            }

            LoggerManager.Logger.LogInformational("Mongo Atlas Extract Initiated for timeframe: " + this.startDate + " - " + this.endDate + " ************************************");

            ////***************************************************************************************************************/
            //// Capture logs that exist at an "org" level, i.e. the entire DXC HPP MongoDB Atlas Subscription
            ////***************************************************************************************************************/
            if (ConfigurationManager.AppSettings.Get("capture_org_audit_events") == "true")
            {
                LoggerManager.Logger.LogInformational("Capture Org Audit Events Initiated ************************************");
                foreach (string jobDate in jobDates)
                {
                    //// Get all portal activity entries for the organization associated with {ORG-ID}.
                    //// API reference: https://docs.atlas.mongodb.com/reference/api/events-orgs-get-all/
                    //// NOTE: The following filename MAY be changed if desired to conform to standards; it is arbitrary
                    fileName = this.orgName + "AtlasPortalOrgEvents.json";
                    apiEndpoint = "orgs/" + this.atlasOrgID + "/events";
                    apiEndpoint = "orgs/" + this.atlasOrgID + "/events?pretty=true&minDate=" + jobDate + "T00:00:01&maxDate=" + jobDate + "T23:59:59";
                    this.OutputApiResponseToFile(atlasOrgConnector, apiEndpoint, fileName, this.localDir, jobDate + " GMT");
                }
            }
            //// Get information about the Atlas users associated with {ORG-ID}.
            //// API reference: https://docs.atlas.mongodb.com/reference/api/organization-users-get-all-users/
            //// NOTE: The following filename MAY be changed if desired to conform to standards; it is arbitrary
            if (ConfigurationManager.AppSettings.Get("capture_enable-organization-users-get-all-users") == "true")
            {
                LoggerManager.Logger.LogInformational("Capture Org Users Initiated ************************************");
                fileName = this.orgName + "_" + "AtlasPortalUsers.json";
                apiEndpoint = "orgs/" + this.atlasOrgID + "/users?pretty=true";
                this.OutputApiResponseToFile(atlasOrgConnector, apiEndpoint, fileName, this.localDir + this.reportDir, string.Empty);
            }
            //// Get information about the API keys associated with {ORG-ID}.
            //// API reference: https://docs.atlas.mongodb.com/reference/api/apiKeys-orgs-get-all/
            //// NOTE: The following filename MAY be changed if desired to conform to standards; it is arbitrary
            if (ConfigurationManager.AppSettings.Get("capture_org_apiKeys") == "true")
            {
                LoggerManager.Logger.LogInformational("Capture Org Users Initiated ************************************");
                fileName = this.orgName + "_" + "ApiKeys.json";
                apiEndpoint = "orgs/" + this.atlasOrgID + "/apiKeys?pretty=true";
                this.OutputApiResponseToFile(atlasOrgConnector, apiEndpoint, fileName, this.localDir + this.reportDir, string.Empty);

                //// Get the whitelist entries for each Org API key
                //// API reference:  https://docs.atlas.mongodb.com/reference/api/apiKeys-org-whitelist-get-all/
                List<MongoAtlasApiKey> atlasOrgApiKeys = new List<MongoAtlasApiKey>();
                atlasOrgApiKeys = this.GetAllApiKeysForOrg(atlasOrgConnector);

                foreach (MongoAtlasApiKey apiKey in atlasOrgApiKeys)
                {
                    LoggerManager.Logger.LogInformational("Capture API Key whitelist for Org key " + apiKey.PublicKey + " Users Initiated ************************************");
                    fileName = this.orgName + "_" + "ApiKeyWhiteList_" + apiKey.PublicKey + ".json";
                    apiEndpoint = "orgs/" + this.atlasOrgID + "/apiKeys/" + apiKey.ID + "/whitelist?pretty=true";
                    this.OutputApiResponseToFile(atlasOrgConnector, apiEndpoint, fileName, this.localDir + this.reportDir, string.Empty);
                }
            }

            ////***************************************************************************************************************/
            //// Capture project level logs
            ////***************************************************************************************************************/
            if (ConfigurationManager.AppSettings.Get("capture_project_audit_events") == "true")
            {
                LoggerManager.Logger.LogInformational("Capture Project Audit Events Initiated ************************************");
                foreach (string jobDate in jobDates)
                {
                    //// Get all portal activity entries for the project associated with {GROUP-ID}.
                    //// API reference: https://docs.atlas.mongodb.com/reference/api/events-projects-get-all/
                    //// NOTE: The following filename MAY be changed if desired to conform to standards; it is arbitrary
                    fileName = this.atlasProjectName + "_" + "AtlasPortalProjectEvents.json";
                    apiEndpoint = "groups/" + this.atlasProjectID + "/events?pretty=true&minDate=" + jobDate + "T00:00:01&maxDate=" + jobDate + "T23:59:59";
                    this.OutputApiResponseToFile(atlasProjectConnector, apiEndpoint, fileName, this.localDir, jobDate + " GMT");
                }
            }
            //// Get all database users and roles for the project associated with {GROUP-ID}.
            //// API reference: https://docs.atlas.mongodb.com/reference/api/database-users-get-all-users/
            //// NOTE: The following filename MAY be changed if desired to conform to standards; it is arbitrary
            if (ConfigurationManager.AppSettings.Get("capture_project_database-users-get-all-users") == "true")
            {
                LoggerManager.Logger.LogInformational("Capture Project database Users Initiated ************************************");
                fileName = this.atlasProjectName + "_" + "UsersAndRoles.json";
                apiEndpoint = "groups/" + this.atlasProjectID + "/databaseUsers?pretty=true";
                this.OutputApiResponseToFile(atlasProjectConnector, apiEndpoint, fileName, this.localDir + this.reportDir, string.Empty);
            }

            //// Get all Atlas Portal user team information for the project
            //// API reference: https://docs.atlas.mongodb.com/reference/api/project-get-teams/
            if (ConfigurationManager.AppSettings.Get("capture_project-get-teams") == "true")
            {
                LoggerManager.Logger.LogInformational("Capture Project Team details Initiated ************************************");
                //// Collection of all org atlas portal teams for the project
                List<MongoAtlasPortalTeam> atlasProjectPortalTeams = new List<MongoAtlasPortalTeam>();
                atlasProjectPortalTeams = this.GetAllTeamsForProject(atlasProjectConnector, this.atlasProjectID);

                foreach (MongoAtlasPortalTeam atlasTeam in atlasProjectPortalTeams)
                {
                    //// NOTE:  The following filename MAY be changed if desired to conform to standards; it is arbitrary
                    fileName = this.orgName + "_" + "AtlasPortalTeam" + "_" + this.atlasProjectName + "_" + atlasTeam.TeamName + ".json";
                    apiEndpoint = "orgs/" + this.atlasOrgID + "/teams/" + atlasTeam.TeamID + "/users?pretty=true";
                    this.OutputApiResponseToFile(atlasProjectConnector, apiEndpoint, fileName, this.localDir + this.reportDir, string.Empty);
                }
            }

            //// Get all project whitelist entries
            //// API reference: https://docs.atlas.mongodb.com/reference/api/whitelist-get-all/
            //// NOTE: The following filename MAY be changed if desired to conform to standards; it is arbitrary
            if (ConfigurationManager.AppSettings.Get("capture_whitelist-get-all") == "true")
            {
                LoggerManager.Logger.LogInformational("Capture Project Whitelist Initiated ************************************");
                fileName = this.atlasProjectName + "_" + "ProjectWhitelistEntries.json";
                apiEndpoint = "groups/" + this.atlasProjectID + "/whitelist?pretty=true";
                this.OutputApiResponseToFile(atlasOrgConnector, apiEndpoint, fileName, this.localDir + this.reportDir, string.Empty);
            }

            //// *** NOTE:  no current functionality to capture project API key whitelist entries.  mongo support ticket created to request enhancement
            //// Get all project API key entries
            //// API reference: https://docs.atlas.mongodb.com/reference/api/projectApiKeys/get-all-apiKeys-in-one-project/
            //// NOTE: The following filename MAY be changed if desired to conform to standards; it is arbitrary
            if (ConfigurationManager.AppSettings.Get("capture_project_apiKeys") == "true")
            {
                LoggerManager.Logger.LogInformational("Capture Project Whitelist Initiated ************************************");
                fileName = this.atlasProjectName + "_" + "ApiKeys.json";
                apiEndpoint = "groups/" + this.atlasProjectID + "/apiKeys?pretty=true";
                this.OutputApiResponseToFile(atlasProjectConnector, apiEndpoint, fileName, this.localDir + this.reportDir, string.Empty);
            }
            
            ////***************************************************************************************************************/
            //// Capture replica set (i.e. "process") level logs
            ////***************************************************************************************************************/
            if (ConfigurationManager.AppSettings.Get("capture_cluster_extracts") == "true")
            {
                LoggerManager.Logger.LogInformational("Capture Project Replicaset Details Initiated ************************************");
                //// Collection of all project replica set members
                List<MongoHostProcess> hostProcesses = new List<MongoHostProcess>();
                hostProcesses = this.GetAllProcessesForProject(atlasProjectConnector, project.ProjectID, project.ClusterName);

                foreach (MongoHostProcess hostProcess in hostProcesses)
                {
                    foreach (string jobDate in jobDates)
                    {
                        if (ConfigurationManager.AppSettings.Get("capture_cluster_database-history") == "true")
                        {
                            LoggerManager.Logger.LogInformational("Capture Project " + this.atlasProjectName + " Capture project " + this.atlasProjectName + "  access history Initiated ************************************");
                            //// Retrieve the access logs of a process by hostname
                            //// API reference:  https://docs.atlas.mongodb.com/reference/api/access-tracking-get-database-history-hostname/
                            //// NOTE: The following filename MAY be changed if desired to conform to standards; it is arbitrary
                            fileName = hostProcess.Hostname + "_" + "DBAccessHistory.json";
                            string startDateMS = this.GetUnixEpochOffset(DateTime.Parse(jobDate + " 12:00:01 AM")).ToUnixTimeMilliseconds().ToString();
                            string endDateMS = this.GetUnixEpochOffset(DateTime.Parse(jobDate + " 11:59:59 PM")).ToUnixTimeMilliseconds().ToString();
                            apiEndpoint = "groups/" + project.ProjectID + "/dbAccessHistory/processes/" + hostProcess.Hostname + "?pretty=true&start=" + startDateMS + "&end=" + endDateMS;
                            this.OutputApiResponseToFile(atlasProjectConnector, apiEndpoint, fileName, this.localDir, jobDate + " GMT");
                        }

                        if (ConfigurationManager.AppSettings.Get("capture_cluster_audit_logs") == "true")
                        {
                            LoggerManager.Logger.LogInformational("Capture Project " + this.atlasProjectName + " Cluster audit logs Initiated ************************************");
                            //// Retrieve a compressed (.gz) log file that contains a range of audit log messages for a particular host in an Atlas cluster.
                            //// IMPORTANT:  This call requires PROJECT OWNER access for the API key.  The key must be handled securely at all times for projects!
                            //// API reference: https://docs.atlas.mongodb.com/reference/api/logs/
                            //// NOTE:  The following filename MUST NOT be changed if desired to conform to standards; refer to the atlas documentation
                            fileName = "mongodb-audit-log.gz";
                            //// startDate/endDate represent the number of seconds that have elapsed since the UNIX epoch that specifies starting point for the range of log messages to retrieve. Default is 24 hours prior to the current timestamp.
                            string startDate = this.GetUnixEpochOffset(DateTime.Parse(jobDate + " 12:00:01 AM")).ToUnixTimeSeconds().ToString();
                            string endDate = this.GetUnixEpochOffset(DateTime.Parse(jobDate + " 11:59:59 PM")).ToUnixTimeSeconds().ToString();
                            apiEndpoint = "groups/" + project.ProjectID + "/clusters/" + hostProcess.Hostname + "/logs/" + fileName + "?pretty=true&startDate=" + startDate + "&endDate=" + endDate;
                            //// Must wait until after apiEndpoint is defined to add process-specific prefix to file name
                            fileName = hostProcess.Hostname + "_" + "mongodb-audit-log.gz";
                            this.OutputApiResponseToFile(atlasProjectConnector, apiEndpoint, fileName, this.localDir, jobDate + " GMT", true);
                        }
                    }
                }

                LoggerManager.Logger.LogInformational("Mongo Atlas Extract Completed ************************************");
            }
        }

        private DateTimeOffset GetUnixEpochOffset(DateTime dateTime)
        {
            // test site: https://www.epochconverter.com/
            DateTimeOffset dto = new DateTimeOffset(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, TimeSpan.Zero);
            return dto;
        }

        private void OutputApiResponseToFile(AtlasConnector connector, string apiEndpoint, string fileName, string dir, string jobDate, bool decompress = false)
        {   
            if (jobDate == string.Empty)
            {
                jobDate = DateTime.Now.Year + "-" + DateTime.Now.ToString("MM") + "-" + DateTime.Now.ToString("dd");
            }

            // Remove GMT suffix if found, as will skew the date formats
            jobDate = jobDate.Replace(" GMT", string.Empty);

            string dateYYYY = DateTime.Parse(jobDate).Year.ToString();
            string dateMM = DateTime.Parse(jobDate).ToString("MM");
            string datedd = DateTime.Parse(jobDate).ToString("dd");

            int bufferSize = 1024;
            byte[] buffer = new byte[bufferSize];
            int bytesRead = 0;

            try
            {
                Stream logFileResponse = connector.GetRequestStream(apiEndpoint);
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                dir = dir + dateYYYY + "\\" + dateMM + "\\" + datedd + "\\";
                string filePath = dir + fileName;
                LoggerManager.Logger.LogInformational("Attempting to write output file to " + filePath);
                //// Create the directory
                Directory.CreateDirectory(dir);
                //// Read from response and write to file
                FileStream fileStream = File.Create(filePath);
                while ((bytesRead = logFileResponse.Read(buffer, 0, bufferSize)) != 0)
                {
                    fileStream.Write(buffer, 0, bytesRead);
                }

                fileStream.Close();

                if (decompress == true)
                {
                    this.DecompressGZip(filePath, filePath.Replace(".gz", ".json"));

                    // update fileName
                    fileName = fileName.Replace(".gz", ".json");
                }

                //// gather subfolders for use when uploading to Evidence
                this.extractDirs.Add(new HelperClasses.FilePath(fileName, filePath, dir.Replace(this.localDir, string.Empty)));

                LoggerManager.Logger.LogInformational("File successfully written in " + stopwatch.ElapsedMilliseconds.ToString() + " MS to " + fileName);
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogInformational("File write attempt failed for " + fileName);
                throw ex;
            }
        }

        private List<MongoAtlasApiKey> GetAllApiKeysForOrg(AtlasConnector connector)
        {
            //// API reference: https://docs.atlas.mongodb.com/reference/api/apiKeys-orgs-get-all/
            List<MongoAtlasApiKey> apiKeys = new List<MongoAtlasApiKey>();

            string hostsResponse = connector.GetRequest("orgs/" + this.atlasOrgID + "/apiKeys");
            BsonDocument hostsObj = BsonDocument.Parse(hostsResponse);

            BsonArray hostsArr = hostsObj["results"].AsBsonArray;
            foreach (var hostObj in hostsArr)
            {
                string id = hostObj["id"].AsString;
                string publicKey = hostObj["publicKey"].AsString;

                // create host object
                MongoAtlasApiKey thisApiKey = new MongoAtlasApiKey(id, publicKey);
                
                // add to collection
                apiKeys.Add(thisApiKey);
            }

            return apiKeys;
        }

        private List<MongoHostProcess> GetAllProcessesForProject(AtlasConnector connector, string atlas_projectid, string atlas_clustername)
        {
            List<MongoHostProcess> hostProcesses = new List<MongoHostProcess>();
            //// API Reference: https://docs.atlas.mongodb.com/reference/api/clusters-get-all/
            string hostsResponse = connector.GetRequest("groups/" + atlas_projectid + "/processes/?clustername=" + atlas_clustername);
            BsonDocument hostsObj = BsonDocument.Parse(hostsResponse);

            BsonArray hostsArr = hostsObj["results"].AsBsonArray;
            foreach (var hostObj in hostsArr)
            {
                DateTime created = DateTime.Parse(hostObj["created"].AsString);
                string hostname = hostObj["hostname"].AsString;
                DateTime lastPing = DateTime.Parse(hostObj["lastPing"].AsString);
                int port = hostObj["port"].AsInt32;
                string typeName = hostObj["typeName"].AsString;
                string version = hostObj["version"].AsString;

                // create host object
                MongoHostProcess thisHostProcess = new MongoHostProcess(created, hostname, lastPing, port, typeName, version);

                // add to collection
                hostProcesses.Add(thisHostProcess);
            }

            return hostProcesses;
        }

        private List<MongoAtlasPortalTeam> GetAllTeamsForProject(AtlasConnector connector, string atlas_projectid)
        {
            List<MongoAtlasPortalTeam> atlasTeams = new List<MongoAtlasPortalTeam>();

            //// API reference:  https://docs.atlas.mongodb.com/reference/api/teams-get-all/
            //// GET /orgs/{ORG-ID}/teams

            string teamsResponse = connector.GetRequest("orgs/" + this.atlasOrgID + "/teams");
            BsonDocument teamsObj = BsonDocument.Parse(teamsResponse);

            BsonArray teamsArr = teamsObj["results"].AsBsonArray;
            foreach (var teamObj in teamsArr)
            {
                string teamID = teamObj["id"].AsString;
                string teamName = teamObj["name"].AsString;

                // create team object
                MongoAtlasPortalTeam thisTeam = new MongoAtlasPortalTeam(teamID, teamName);

                // add to collection
                atlasTeams.Add(thisTeam);
            }

            return atlasTeams;
        }

        private void DecompressGZip(string fileRoot, string destRoot)
        {
            using (FileStream fileStream = new FileStream(fileRoot, FileMode.Open, FileAccess.Read))
            using (GZipInputStream zipStream = new GZipInputStream(fileStream))
            using (StreamReader sr = new StreamReader(zipStream))
            {
                string data = sr.ReadToEnd();
                File.WriteAllText(destRoot, data);
               ////File.Delete(fileRoot); // needs additional analysis; did not seem to work correctly?
            }
        }
    }
}
