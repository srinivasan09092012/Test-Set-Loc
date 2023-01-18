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
    public class ChildProcessConfigCollection : ConfigurationElementCollection
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
                return "ChildProcess";
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new WorkflowProcessConfigElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new WorkflowProcessConfigElement(elementName);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((WorkflowProcessConfigElement)element).ProcessName;
        }

        new public WorkflowProcessConfigElement this[string Name]
        {
            get
            {
                return (WorkflowProcessConfigElement)BaseGet(Name);
            }
        }

        public List<K2ProcessModel> ToChildList()
        {
            List<K2ProcessModel> children = new List<K2ProcessModel>();

            foreach (WorkflowProcessConfigElement item in this)
            {
                K2ProcessModel child = new K2ProcessModel();
                child.ProcessName = item.ProcessName;
                child.ProcessFullName = item.ProcessFullName;
                child.ChildProcesses = new List<K2ProcessModel>();
                children.Add(child);
            }

            return children;
        }
    }
}
