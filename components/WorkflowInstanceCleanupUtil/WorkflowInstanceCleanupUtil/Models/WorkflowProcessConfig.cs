//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2019. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Configuration;

namespace WorkflowInstanceCleanupUtil.Models
{
    public class WorkflowProcessConfig : ConfigurationSection
    {
        [ConfigurationProperty("WorkflowProcesses")]
        [ConfigurationCollection(typeof(Processes), AddItemName = "WorkflowProcess")]
        public Processes Processes
        {
            get
            {
                object o = this["WorkflowProcesses"];
                return o as Processes;
            }
        }

        public static WorkflowProcessConfig GetConfig()
        {
            return (WorkflowProcessConfig)ConfigurationManager.GetSection("RegisterWorkflowProcesses") ?? new WorkflowProcessConfig();
        }
    }
}
