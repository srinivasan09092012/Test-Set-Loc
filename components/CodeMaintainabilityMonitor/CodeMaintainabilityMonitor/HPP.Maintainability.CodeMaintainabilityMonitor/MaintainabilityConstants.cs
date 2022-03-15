//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace HPP.Maintainability.CodeMaintainabilityMonitor
{
    public static class MaintainabilityConstants
    {
        public const string CommandLine = "cmd.exe";
        public const string HppName = "HP.HSP.UA3.";
        public const string Error = "Error while trying to calculate metrics for file {0}";
        public const int MaintainabilityThreshold = 60;
        public const string MetricsExe = "Metrics.exe /solution:";
        public const string MetricsOut = " /out:";
        public const string MetricsExtention = ".xml\"";
        public const string SoluionExention = "*.sln";
        public const string XmlSearchPatern = "*.xml";

        public static class DataTableColumnsAndRows
        {
            public const string File = "File";
            public const string Name = "Name";
            public const string Line = "Line";
            public const string MaintainabilityIndex = "Maintainability Index";
        }

        public static class Paths
        {
            public const string DoulbeBackslash = "\\";
            public const string InputFolder = @"C:\UA3\Source\Integration\Dev\Utilities\MaintainabilityMetricsCalculator\OutputXmlFiles";
            public const string MaintainabilityMetricsCalculator = @"cd C:\UA3\Source\Integration\Dev\Utilities\MaintainabilityMetricsCalculator\Calculator";
            public const string OutputFolder = @"C:\UA3\Source\Integration\Dev\Utilities\MaintainabilityMetricsCalculator\MethodsWithIndexBelowMaintainabilityLimit\{0}";
            public const string SourceFolder = @"c:\UA3\Source\";
            public const string OutputXmlFiles = "\"C:\\UA3\\Source\\Integration\\Dev\\Utilities\\MaintainabilityMetricsCalculator\\OutputXmlFiles\\Metrics_For_";
            public const string Underscore = "_";
        }

        public static class XmlAttributes
        {
            public const string File = "File";
            public const string Name = "Name";
            public const string Line = "Line";
            public const string Value = "Value";
        }

        public static class XmlDescendants
        {
            public const string Method = "Method";
            public const string Metrics = "Metrics";
        }

        public static class XmlElements
        {
            public const string Metric = "Metric";
        }

        public static class FiltersForFileFullName
        {
            public const string ServiceReference = "Service Reference";
            public const string Tests = "Tests";
        }

        public static class Messages
        {
            public const string EmptyMessage = "";
            public const string Processing = "Processing... please wait, next tab will reflect results ones it finish";
            public const string ProcessDone = "Done";
            public const string ProcessLoading = "Loading...";
        }
    }
}
