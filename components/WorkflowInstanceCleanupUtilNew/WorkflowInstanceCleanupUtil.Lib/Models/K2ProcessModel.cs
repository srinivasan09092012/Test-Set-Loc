//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;

namespace WorkflowInstanceCleanupUtil.Lib.Models
{
    public class K2ProcessModel
    {
        public string ProcessName { get; set; }

        public string ProcessFullName { get; set; }

        public List<K2ProcessModel> ChildProcesses { get; set; }
    }
}
