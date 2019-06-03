using HPE.HSP.UA3.Core.API.Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace Watchdog.Providers
{
    public class CPUAndMemUsageProvider
    {
        private static float processMemInKB;
        private static double processMemoryInGB;

        public static Tuple<double, double, float, double> GetProcessCPUAndMemoryUsagePercent(ILogger logger, Process process)
        {
            double cpuPercent = 0.0d;
            double memPercent = 0.0d;
            logger.LogInformational("Process ID :" + process.Id);
            try
            {
                string instanceName = GetInstanceNameForProcess(process.Id, process.ProcessName);
                using (PerformanceCounter processPT = new PerformanceCounter("Process", "% Processor Time", instanceName))
                using (PerformanceCounter processMemory = new PerformanceCounter("Process", "Working Set - Private", instanceName))
                {
                    double cpuUsag = Math.Round(processPT.NextValue(), 1);
                    cpuPercent = cpuUsag / Environment.ProcessorCount;
                    double totalMemoryInMB = GetTotalPhysicalMemoryInMB(logger);
                    logger.LogInformational("Total Memory MB :" + totalMemoryInMB);
                    processMemInKB = processMemory.NextValue() / 1024;
                    logger.LogInformational("Process Memory KB :" + processMemInKB);
                    processMemoryInGB = Math.Round(processMemInKB / (1000000), 2);
                    logger.LogInformational("Process Memory GB :" + processMemoryInGB);
                    if (totalMemoryInMB > 0)
                    {
                        memPercent = Math.Round((processMemoryInGB / totalMemoryInMB) * 100, 3);
                    }                    
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured while getting process health detail : " + process.ProcessName, ex);
            }

            return new Tuple<double, double, float, double>(cpuPercent, memPercent, processMemInKB, processMemoryInGB);
        }

        private static string GetInstanceNameForProcess(int processId,string processName)
        {
            string processInstance = string.Empty;
            PerformanceCounterCategory cat = new PerformanceCounterCategory("Process");
            List<string> instances = cat.GetInstanceNames().Where(t => t.Contains(processName)).ToList();
            foreach (string instance in instances)
            {
                using (PerformanceCounter cnt = new PerformanceCounter("Process", "ID Process", instance, true))
                {
                    try
                    {
                        int val = (int)cnt.RawValue;
                        if (val == processId)
                        {
                            processInstance = instance;
                            return processInstance;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            return processInstance;
        }

        private static double GetTotalPhysicalMemoryInMB(ILogger logger)
        {
            double totalMemoryInMB = 0.0d;

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection results = searcher.Get();

            double totalMemoryInKB;

            foreach (ManagementObject result in results)
            {
                totalMemoryInKB = Convert.ToDouble(result["TotalVisibleMemorySize"]);
                logger.LogInformational("Total Memory in KB : " + totalMemoryInKB);
                totalMemoryInMB = Math.Round((totalMemoryInKB / (1024 )), 2);
            }

            return totalMemoryInMB;
        }
    }
}
