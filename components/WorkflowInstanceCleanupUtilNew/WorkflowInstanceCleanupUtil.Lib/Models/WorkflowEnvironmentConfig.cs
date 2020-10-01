//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2019. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Configuration;

namespace WorkflowInstanceCleanupUtil.Lib.Models
{
    public class WorkflowEnvironmentConfig : ConfigurationSection
    {
        [ConfigurationProperty("WorkflowEnvironments")]
        [ConfigurationCollection(typeof(Environments), AddItemName = "WorkflowEnvironment")]
        public Environments Environments
        {
            get
            {
                object o = this["WorkflowEnvironments"];
                return o as Environments;
            }
        }

        public static WorkflowEnvironmentConfig GetConfig()
        {
            return (WorkflowEnvironmentConfig)ConfigurationManager.GetSection("RegisterWorkflowEnvironment") ?? new WorkflowEnvironmentConfig();
        }
    }
}
