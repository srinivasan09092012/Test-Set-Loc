//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technology, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Configuration;

namespace WorkflowInstanceCleanupUtil.Lib.Models
{
    public class WorkflowProcessConfigElement : ConfigurationElement
    {
        public WorkflowProcessConfigElement()
        {
        }

        public WorkflowProcessConfigElement(string elementName)
        {
            this.ProcessName = elementName;
        }

        [ConfigurationProperty("ProcessName", IsRequired = true, IsKey = true)]
        public string ProcessName
        {
            get
            {
                return (string)this["ProcessName"];
            }
            set
            {
                this["ProcessName"] = value;
            }
        }

        [ConfigurationProperty("ProcessFullName", IsRequired = true)]
        public string ProcessFullName
        {
            get
            {
                return (string)this["ProcessFullName"];
            }
            set
            {
                this["ProcessFullName"] = value;
            }
        }

        [ConfigurationProperty("ChildProcesses", IsDefaultCollection = false)]
        public ChildProcessConfigCollection ChildProcesses
        {
            get
            {
                return (ChildProcessConfigCollection)base["ChildProcesses"];
            }
        }
    }
}
