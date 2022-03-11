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
        public const string XmlAttributeValue = "Value";
        public const string XmlSearchPatern = "*.xml";
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

        public static class XmlDescendants
        {
            public const string Method = "Method";
            public const string Metrics = "Metrics";
        }

        public static class FiltersForFileFullName
        {
            public const string ServiceReference = "Service Reference";
            public const string Tests = "Tests";
        }

        public static class Messages
        {
            public const string EmptyMessage = "";
            public const string Processing = "now processing...";
            public const string ProcessDone = "Done";
            public const string ProcessLiading = "Loading...";
        }
    }
}
