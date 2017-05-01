//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Diagnostics;

namespace MainEvent.Core.Model
{
    [DebuggerDisplay("{Name}")]
    public class TableSchemaModel
    {
        public TableSchemaModel(string name)
        {
            this.Name = name;
            this.Dependencies = new List<string>();
        }

        public string Name { get; private set; }

        public List<string> Dependencies { get; private set; }
    }
}