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
            DateTime startDate = DateTime.UtcNow.AddMonths(Convert.ToByte(ConfigurationManager.AppSettings["LookBackNumberOfMonth"]));
            DateTime endDate = DateTime.UtcNow.AddDays(1);
            List<K2ProcessModel> processes;
            List<K2EnvironmentModel> environments;

            ReadConfiguration(out environments, out processes);

            foreach (var environment in environments)
            {
                Console.WriteLine(string.Format("Processing {0} environment", environment.Environment));


                using (IWorkItemCleanUpProcessor processor = InitializeK2Processor(environment))
                {
                    foreach (var process in processes)
                    {
                        DeleteProcessInstances(processor, startDate, endDate, process);
                    }
                }
            }
        }

        private static void ReadConfiguration(out List<K2EnvironmentModel> environments, out List<K2ProcessModel> processes)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            WorkflowConfigurationSection workflowConfig = config.GetSection("WorkflowConfiguration") as WorkflowConfigurationSection;
            environments = workflowConfig.WorkflowEnvironments.ToEnvironmentList();
            processes = workflowConfig.WorkflowProcesses.ToProcessList();
        }

        private static WorkItemCleanUpProcessor InitializeK2Processor(K2EnvironmentModel environment)
        {
            return new WorkItemCleanUpProcessor(
                environment.Server,
                uint.Parse(environment.Port),
                environment.UserName,
                environment.Password,
                "K2",
                true);
        }

        private static void DeleteProcessInstances(IWorkItemCleanUpProcessor processor, DateTime startDate, DateTime endDate, K2ProcessModel process)
        {
            DeleteInstances(processor, startDate, endDate, process);

            foreach (var child in process.ChildProcesses)
            {
                DeleteInstances(processor, startDate, endDate, child);
            }
        }

        private static void DeleteInstances(IWorkItemCleanUpProcessor processor, DateTime startDate, DateTime endDate, K2ProcessModel processInfo)
        {
            Console.WriteLine(string.Format("Processing {0} WorkFlow Process", processInfo.ProcessFullName));
            int numberOfInstanceDeleted = 0;
            List<ProcessInfo> processes = processor.GetAllProcessInstanceIds(startDate, endDate, processInfo.ProcessFullName);

            foreach (var process in processes)
            {
                processor.DeleteProcessInstances(process.ID, true);
                numberOfInstanceDeleted++;
                Console.WriteLine(string.Format("Deleted {0}", process.ID));
            }

            Console.WriteLine(string.Format("Processed {0} WorkFlow Process", processInfo.ProcessFullName));
        }
    }
}
