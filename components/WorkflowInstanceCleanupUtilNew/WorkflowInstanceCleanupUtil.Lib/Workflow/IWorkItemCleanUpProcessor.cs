//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2019. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using WorkflowInstanceCleanupUtil.Lib.Models;

namespace WorkflowInstanceCleanupUtil.Lib.Workflow
{
    public interface IWorkItemCleanUpProcessor : IDisposable
    {
        List<ProcessInfo> GetAllProcessInstanceIds(DateTime startDate, DateTime endDate, string processName);

        bool DeleteProcessInstances(int processId, bool deleteLogs = true);
    }
}