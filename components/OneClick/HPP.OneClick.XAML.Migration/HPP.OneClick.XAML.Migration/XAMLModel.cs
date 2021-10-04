using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPP.OneClick.XAML.Migration
{
    public class XAMLModel
    {
    }
    public class ActivityModel
    {
    }

    public class AutomatedTest
    {
        public string AssemblyFileSpec { get; set; }
        public string RunSettingsFileName { get; set; }
        public string TestCaseFilter { get; set; }
        public RunSettingsForTestRun RunSettingsForTestRun { get; set; }
        public string HasTestCaseFilter { get; set; }
        public string ExecutionPlatform { get; set; }
        public string FailBuildOnFailure { get; set; }
        public string RunName { get; set; }
    }
    public class RunSettingsForTestRun
    {
        public string ServerRunSettingsFile { get; set; }
        public string TypeRunSettings { get; set; }
        public string HasRunSettingsFile { get; set; }
    }

    public class AdvanceTestSettings
    {
        public string DisableTests { get; set; }
    }

    public class AdvanceBuildSettings
    {
        public string MSBuildArguments { get; set; }
        public string SFMSBuildArguments { get; set; }
    }
}
