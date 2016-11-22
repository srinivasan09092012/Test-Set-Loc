//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel.Composition;

namespace UA3.CodeFactory.Core.PluginModel
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CodeFactoryPluginAttribute : ExportAttribute
    {
        public CodeFactoryPluginAttribute(string name, string description, string version)
            : base(typeof(ICodeFactoryPlugin))
        {
            this.Name = name;
            this.Description = description;
            this.Version = version;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Version { get; private set; }
    }
}