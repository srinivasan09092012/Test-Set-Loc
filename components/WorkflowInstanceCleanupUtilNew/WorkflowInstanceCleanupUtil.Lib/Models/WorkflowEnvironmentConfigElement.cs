//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technology, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Configuration;

namespace WorkflowInstanceCleanupUtil.Lib.Models
{
    public class WorkflowEnvironmentConfigElement : ConfigurationElement
    {
        public WorkflowEnvironmentConfigElement()
        {
        }

        public WorkflowEnvironmentConfigElement(string elementName)
        {
            this.Environment = elementName;
        }

        [ConfigurationProperty("Environment", IsRequired = true, IsKey = true)]
        public string Environment
        {
            get
            {
                return (string)this["Environment"];
            }
            set
            {
                this["Environment"] = value;
            }
        }

        [ConfigurationProperty("Server", IsRequired = true)]
        public string Server
        {
            get
            {
                return this["Server"] as string;
            }
        }

        [ConfigurationProperty("Port", IsRequired = true)]
        public string Port
        {
            get
            {
                return this["Port"] as string;
            }
        }

        [ConfigurationProperty("Username", IsRequired = true)]
        public string Username
        {
            get
            {
                return this["Username"] as string;
            }
        }

        [ConfigurationProperty("Password", IsRequired = true)]
        public string Password
        {
            get
            {
                return this["Password"] as string;
            }
        }
    }
}
