//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technology, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Configuration;

namespace WorkflowInstanceCleanupUtil.Lib.Models
{
    public class WorkflowEnvironmentCollection : ConfigurationElementCollection
    {

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return "WorkflowEnvironment";
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new WorkflowEnvironmentConfigElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new WorkflowEnvironmentConfigElement(elementName);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((WorkflowEnvironmentConfigElement)element).Environment;
        }

        new public WorkflowEnvironmentConfigElement this[string Name]
        {
            get
            {
                return (WorkflowEnvironmentConfigElement)BaseGet(Name);
            }
        }

        public List<K2EnvironmentModel> ToEnvironmentList()
        {
            List<K2EnvironmentModel> environments = new List<K2EnvironmentModel>();
            foreach (WorkflowEnvironmentConfigElement item in this)
            {
                K2EnvironmentModel environment = new K2EnvironmentModel();
                environment.Environment = item.Environment;
                environment.Server = item.Server;
                environment.UserName = item.Username;
                environment.Password = item.Password;
                environment.Port = item.Port;
                environments.Add(environment);
            }

            return environments;
        }
    }
}
