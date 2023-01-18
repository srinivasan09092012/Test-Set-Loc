//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using WorkflowInstanceCleanupUtil.Models;
using K2Mgt = SourceCode.Workflow.Management;

namespace WorkflowInstanceCleanupUtil.Workflow
{
    public class WorkItemCleanUpProcessor : IWorkItemCleanUpProcessor
    {
        private K2Mgt.WorkflowManagementServer workflowInstance;

        public WorkItemCleanUpProcessor(string server, uint port, string user, string password, string securityLabel, bool primaryLogin)
        {
            try
            {
                this.workflowInstance = new K2Mgt.WorkflowManagementServer(server, port, user, password, securityLabel, primaryLogin);
                this.workflowInstance.Open();
            }
            catch (System.Net.Sockets.SocketException exception)
            {
                throw new Exception("Error connecting to server - " + exception.Message);
            }
            catch (Exception exception)
            {
                throw new Exception("Error occured - " + exception.Message);
            }
        }

        public List<ProcessInfo> GetAllProcessInstanceIds(DateTime startDate, DateTime endDate, string processFullName)
        {
            K2Mgt.ProcessInstances procinsts = this.workflowInstance.GetProcessInstancesAll(startDate, endDate, processFullName, string.Empty, string.Empty);
            List<ProcessInfo> list = new List<ProcessInfo>();

            for (int i = 0; i < procinsts.Count; i++)
            {
                list.Add(new ProcessInfo(procinsts[i].ID, procinsts[i].Folio));
            }

            return list;
        }

        public bool DeleteProcessInstances(int processId, bool deleteLogs = true)
        {
            return this.workflowInstance.DeleteProcessInstances(processId, deleteLogs);
        }

        #region IDisposable
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.workflowInstance != null)
                {
                    if (this.workflowInstance.Connection.IsConnected)
                    {
                        this.workflowInstance.Connection.Close();
                    }

                    this.workflowInstance = null;
                }
            }
        }
        #endregion
    }
}
