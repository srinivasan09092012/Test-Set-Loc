using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using WorkflowInstanceCleanupUtil.Lib.Models;
using WorkflowInstanceCleanupUtil.Lib.Workflow;

namespace WorkflowInstanceCleanupUtil.Job
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<string> environments = GetEnvironments();
            List<string> processNames = GetProcessFullName();
            DateTime startDate = DateTime.UtcNow.AddMonths(Convert.ToByte(ConfigurationManager.AppSettings["LookBackNumberOfMonth"]));
            DateTime endDate = DateTime.UtcNow.AddDays(1);

            foreach (var environment in environments)
            {
                Console.WriteLine(string.Format("Processing {0} environment", environment));


                using (IWorkItemCleanUpProcessor processor = InitializeK2Processor(environment))
                {
                    foreach (var processName in processNames)
                    {
                        Console.WriteLine(string.Format("Processing {0} WorkFlow Process", processName));
                        DeleteInstances(processor, startDate, endDate, processName);
                        Console.WriteLine(string.Format("Processed {0} WorkFlow Process", processName));
                    }
                }
            }
        }

        private static List<string> GetEnvironments()
        {
            List<string> environments = new List<string>();
            IEnumerable<K2EnvironmentModel> k2environments = new K2EnvironmentModel().GetEnvironmentConfiguration();
            environments.AddRange(k2environments.Select(t => t.Environment).ToArray());

            return environments;
        }

        private static List<string> GetProcessFullName()
        {
            K2ProcessModel k2ProcessModel = new K2ProcessModel();
            List<string> processNames = new List<string>();
            List<string> configProcessNames = GetProcesses();
            foreach (var item in configProcessNames)
            {
                K2ProcessModel process = GetProcessConfiguration(item, k2ProcessModel);
                processNames.Add(process.ProcessFullName);
            }

            return processNames;
        }

        private static List<string> GetProcesses()
        {
            List<string> configProcessNames = new List<string>();
            IList<K2ProcessModel> processes = new K2ProcessModel().GetProcessConfiguration();
            configProcessNames.AddRange(processes.Select(t => t.ProcessName).ToArray());

            return configProcessNames;
        }


        private static K2ProcessModel GetProcessConfiguration(string processName, K2ProcessModel k2ProcessModel)
        {
            return k2ProcessModel.GetProcessConfiguration().Where(x => x.ProcessName == processName).FirstOrDefault();
        }

        private static WorkItemCleanUpProcessor InitializeK2Processor(string environmentName)
        {
            K2EnvironmentModel environment = GetEnvironmentConfiguration(environmentName);

            return new WorkItemCleanUpProcessor(
                environment.Server,
                uint.Parse(environment.Port),
                environment.UserName,
                environment.Password,
                "K2",
                true);
        }
        private static K2EnvironmentModel GetEnvironmentConfiguration(string environmentName)
        {
            return new K2EnvironmentModel().GetEnvironmentConfiguration().Where(x => x.Environment == environmentName).FirstOrDefault();
        }


        private static void DeleteInstances(IWorkItemCleanUpProcessor processor, DateTime startDate, DateTime endDate, string processName)
        {
            int numberOfInstanceDeleted = 0;
            List<ProcessInfo> processes = processor.GetAllProcessInstanceIds(startDate, endDate, processName);

            foreach (var process in processes)
            {
                processor.DeleteProcessInstances(process.ID, true);
                numberOfInstanceDeleted++;
                Console.WriteLine(string.Format("Deleted {0}", process.ID));
            }
        }
    }
}
