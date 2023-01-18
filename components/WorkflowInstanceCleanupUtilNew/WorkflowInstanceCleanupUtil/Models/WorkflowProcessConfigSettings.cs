//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Configuration;

namespace WorkflowInstanceCleanupUtil.Models
{
    public class WorkflowProcess : ConfigurationElement
    {
        [ConfigurationProperty("ProcessName", IsRequired = true)]
        public string ProcessName
        {
            get
            {
                return this["ProcessName"] as string;
            }
        }

        [ConfigurationProperty("ProcessFullName", IsRequired = false)]
        public string ProcessFullName
        {
            get
            {
                return this["ProcessFullName"] as string;
            }
        }
    }
}
