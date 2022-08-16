//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using FileHelpers;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HPE.HSP.UA3.Core.API.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SQLToTaskConverter
{
    public class Program : TenantBasConfigurationProvider
    {
        private const string DoubleSlash = "\\";
        private const string SearchPattern = "{0}_Temp*{1}";
        private const string SuffixTempName = "_Temp";
        private const string TempFileName = "Unknown_ErrorCode";
        private const string EventName = "TaskMemberError";
        private const string SearchPatternUnknownFileName = "{0}_{1}{2}{3}.json";
        private static ConcurrentBag<string> messages = new ConcurrentBag<string>();       
        private static string tenantId;

        /// <summary>
        /// This process ingests pre-defined SQL with corresponding parameter files.  Performs parameter
        /// replacement, validates the SQL for malicious statements, executes the SQL against the Managed
        /// Care database.  The results of the query executed above results in data in the form of input
        /// files that can be bulk uploaded using the TaskManagement TaskBulkUpload process.  As of now
        /// the program is set up specifically for Managed Care.  To change the process for another module
        /// the following Application Settings will need to be adjusted ModuleName, ApplicationName, 
        /// and DefaultConnectionSchema.  It will also require the module BAS package with the DataAccess dll included
        /// and small changes made to return the appropriate database context. 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string inputSQLPath = args[0];
            string workCsvPath = args[1];
            string outputJsonPath = args[2];

            SetConnectionTenant();

            List<string> inputSQLFiles = ReturnAllInputFiles(inputSQLPath, ".sql");
            List<string> inputParmFiles = ReturnAllInputFiles(inputSQLPath, ".json");
            
            foreach (var item in inputSQLFiles)
            {
                try
                {                    
                    var fileName = Path.GetFileNameWithoutExtension(item);

                    if (fileName.StartsWith("-"))
                    {
                        string message = string.Format("The following query {0} contained character to indicate it should be bypassed.", item);
                        messages.Add( message);
                        LoggerManager.Logger.LogInformational(message);
                    }
                    else
                    {
                        Stopwatch stopWatch = new Stopwatch();
                        stopWatch.Start();
                        bool outputReturned = new SQLProcessor(tenantId).ProcessManagedCareQuery(item, inputParmFiles.FirstOrDefault(i => i.Contains(fileName)), workCsvPath);
                        stopWatch.Stop();                        
                        TimeSpan ts = stopWatch.Elapsed;

                        if (outputReturned)
                        {
                            string outputCsv = string.Format("{0}\\{1}.csv", workCsvPath, fileName);
                            List<Task> inputTasks = ReturnTasks(outputCsv);
                            HashSet<TmTask> taskEccTmTaskFromTempFiles = new HashSet<TmTask>();
                            inputTasks.ForEach(x => taskEccTmTaskFromTempFiles.Add(new TmTask() { Id = x.Id, TaskMemberId = x.TaskMemberId, TaskSourceModuleCode = x.TaskSourceModuleCode, TaskTypeCode = x.TaskTypeCode, ExceptionErrorCode = x.ExceptionErrorCode, TaskComment = x.TaskComment, UseTaskTemplate = true }));

                            string outputFileName = string.Format("{0}\\{1}.json", outputJsonPath, fileName);

                            SerializeOutput(outputFileName, taskEccTmTaskFromTempFiles);

                            string message = string.Format("The following query {0} resulted in {1} records and executed in {2} time.", item, inputTasks.Count, ts.ToString());
                            messages.Add(message);
                            LoggerManager.Logger.LogInformational(message);
                            File.Delete(outputCsv);
                        }
                        else
                        {
                            string message = string.Format("The following query {0} resulted in no records and executed in {1} time.", item, ts.ToString());
                            messages.Add(message);
                            LoggerManager.Logger.LogInformational(message);
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    string message = string.Format("FATAL ERROR the following query {0} contained errors.", ex.ToString());
                    messages.Add(message);
                    LoggerManager.Logger.LogError(message);
                }
            }

            if (messages.Any())
            {
                string summaryFile = string.Format("{0}\\Summary\\{1}_{2}.json", inputSQLPath, "SQLToTaskConverter", DateTime.Now.ToString("yyyyMMddHHmmss"));
                var summary = JsonConvert.SerializeObject(messages);
                File.WriteAllText(summaryFile, summary);                
            }
        }

        static void SetConnectionTenant()
        {
            tenantId = ConfigurationManager.AppSettings["TenantId"];                
        }


        static void SerializeOutput(string fileLocation, HashSet<TmTask> taskEccTmTaskFromTempFiles, int taskIdStartValue = 0)
        {
            try
            {
                using (StreamWriter sw = CreateWriter(fileLocation))
                {
                    sw.AutoFlush = true;
                    foreach (var task in taskEccTmTaskFromTempFiles.Where(x => x != null))
                    {
                        task.Id = (++taskIdStartValue).ToString();
                        sw.WriteLine(JsonConvert.SerializeObject(task, Formatting.None));
                    }

                    Console.WriteLine(string.Format("{0} record(s) added successfully in Json file.", taskEccTmTaskFromTempFiles.Count));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in method: AppendTmTaskInJsonFile.");
                WriteInLogFile(ex);
            }
        }

        private static List<string> ReturnAllInputFiles(string inputLocation, string extention)
        {
            CheckPath(inputLocation);

            return Directory.GetFiles(inputLocation, "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(extention)).ToList();
        }

        private static void WriteInLogFile(Exception ex)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(string.Format("Error while writing TM tasks in target Json File. Method Name: AppendTmTaskInJsonFile, Error: {0}", ex.Message));

            Console.WriteLine(result.ToString(), ex);
        }

        private static StreamWriter CreateWriter(string fileName)
        {
            try
            {
                return CreateStreamWriter(fileName, true);
            }
            catch
            {
                Console.WriteLine("Exception caught in method: CreateWriter.");
                string tempFilename = GetNewTempJsonFile(fileName);
                return CreateStreamWriter(tempFilename, true);
            }
        }

        private static string GetNewTempJsonFile(string fileName)
        {
            int tempFilesCount = GetTempJsonFiles(fileName).Count() + 1;
            var fileNameWithoutExtention = GetFileNameWithoutExtension(fileName);

            if (string.IsNullOrEmpty(fileNameWithoutExtention))
            {
                string tempFileName = string.Format(
                    SearchPatternUnknownFileName,
                    EventName,
                    TempFileName,
                    SuffixTempName,
                    tempFilesCount);

                return Combine(fileName, tempFileName);
            }
            else
            {
                return fileName.Replace(fileNameWithoutExtention, fileNameWithoutExtention + SuffixTempName + tempFilesCount.ToString());
            }
        }

        private static string[] GetTempJsonFiles(string fileName)
        {
            bool dirExists = DirectoryExists(GetDirectoryName(fileName));
            if (dirExists)
            {
                string searchPattern = string.Format(SearchPattern, GetFileNameWithoutExtension(fileName), GetExtension(fileName));
                return GetFiles(GetDirectoryName(fileName), searchPattern);
            }
            else
            {
                return new string[] { };
            }
        }

        private static StreamWriter CreateStreamWriter(string file, bool append)
        {
            //StreamWriter will perform well in larger buffer of 65k.
            return new StreamWriter(file, append, Encoding.UTF8, 65536);
        }

        private static List<Task> ReturnTasks(string fileLocation)
        {
            List<Task> result = new List<Task>();
            var taskEngine = new FileHelperEngine<Task>();           

            result = taskEngine.ReadFile(fileLocation).ToList();

            return result;
        }

        private static void CheckPath(string fileLocation)
        {
            if (string.IsNullOrEmpty(fileLocation) || !DirectoryExists(fileLocation))
            {
                throw new ArgumentNullException(string.Format("File Location {0} is invalid or directory does not exist", fileLocation));
            }
        }

        private static bool Exists(string url)
        {
            return File.Exists(url);
        }

        private static bool DirectoryExists(string file)
        {
            return Directory.Exists(Path.GetDirectoryName(file));
        }

        private static string GetFileNameWithoutExtension(string file)
        {
            return Path.GetFileNameWithoutExtension(file);
        }

        private static string GetExtension(string file)
        {
            return Path.GetExtension(file);
        }

        private static string GetDirectoryName(string file)
        {
            return Path.GetDirectoryName(file) ?? DoubleSlash;
        }

        private static string[] GetFiles(string path, string searchPattern)
        {
            return Directory.GetFiles(path, searchPattern);
        }

        private static void Delete(string path)
        {
            File.Delete(path);
        }

        private static string Combine(string fileName, string tempFileName)
        {
            return Path.Combine(fileName, tempFileName);
        }
    }
}
