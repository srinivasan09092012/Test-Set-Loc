using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        var inputPath = @"C:\UA3\source\mms-cms-util\components\SQATools\Coverity\HSP_UA3_Main(Dev)\Main.CoverityScan.SolutionList.txt";
        ProcessInputFile(inputPath);
    }

    static void ProcessInputFile(string inputFilePath)
    {
        var lines = new HashSet<string>(File.ReadLines(inputFilePath)); 

        var groups = new Dictionary<string, List<string>>();
        string lastModuleName = null;

        foreach (var line in lines)
        {
            var parts = line.StartsWith("#") ? line.Substring(1).Split('/') : line.Split('/');
            var moduleName = parts.Length > 2 ? parts[2] : "unknown_module"; 
            if (lastModuleName != null && lastModuleName != moduleName && groups[lastModuleName].All(x => !x.StartsWith("#")))
            {
                groups[lastModuleName].Add("#Exclude from scanning");
            }

            if (!groups.ContainsKey(moduleName))
            {
                groups[moduleName] = new List<string>();
            }

            groups[moduleName].Add(line);
            lastModuleName = moduleName;
        }

        if (lastModuleName != null && groups[lastModuleName].All(x => !x.StartsWith("#")))
        {
            groups[lastModuleName].Add("#Exclude from scanning");
        }

        foreach (var group in groups)
        {
            var fileName = group.Key.Replace("main-", "") + ".SolutionList.txt";
            // Change output Path before running
            var outputFilePath = $@"C:\Users\rmartinez3\OneDrive - Gainwell Technologies\Desktop\Solutions\ran by program\{group.Key}.SolutionList.txt";
            File.WriteAllLines(outputFilePath, group.Value);
            Console.WriteLine("Written to: " + outputFilePath);
        }
    }

}
