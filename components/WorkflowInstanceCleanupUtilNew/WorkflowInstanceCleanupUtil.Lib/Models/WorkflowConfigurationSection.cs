//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technology, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Configuration;

namespace WorkflowInstanceCleanupUtil.Lib.Models
{
    public class WorkflowConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("name", DefaultValue = "WorkflowConfiguration", IsRequired = false, IsKey = false)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("WorkflowEnvironments", IsDefaultCollection = false)]
        public WorkflowEnvironmentCollection WorkflowEnvironments
        {
            get
            {
                return (WorkflowEnvironmentCollection)base["WorkflowEnvironments"];
            }
        }

        [ConfigurationProperty("WorkflowProcesses", IsDefaultCollection = false)]
        public WorkflowProcessConfigCollection WorkflowProcesses
        {
            get
            {
                return (WorkflowProcessConfigCollection)base["WorkflowProcesses"];
            }
        }
    }
}
