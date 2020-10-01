//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2019. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WorkflowInstanceCleanupUtil.Lib.Models;
using WorkflowInstanceCleanupUtil.Lib.Workflow;

namespace WorkflowInstanceCleanupUtil
{
    public partial class WorkflowInstanceCleanUp : Form
    {
        private bool stopDeletingtheInstance = false;

        public WorkflowInstanceCleanUp()
        {
            this.InitializeComponent();
            this.BindEnvironmentDropdown();
            this.BindProcessDropdown();
        }

        private void DeleteInstances(IWorkItemCleanUpProcessor processor, DateTime startDate, DateTime endDate, string processName)
        {
            int numberOfInstanceDeleted = 0;
            List<ProcessInfo> processes = processor.GetAllProcessInstanceIds(startDate, endDate, processName);
            int scannedInstances = processes.Count;
            this.WriteMessageToUI("Number of instance found to be deleted are " + scannedInstances);
            btnStopDeleting.Visible = true;
            this.stopDeletingtheInstance = false;

            foreach (var process in processes)
            {
                if (!this.stopDeletingtheInstance)
                {
                    processor.DeleteProcessInstances(process.ID, checkBoxDeleteLogs.Checked);
                    numberOfInstanceDeleted++;
                    this.WriteMessageToUI("Number of instance found to be deleted are " + scannedInstances + ", Deleted " + numberOfInstanceDeleted + "/" + scannedInstances);
                    this.WriteMessageToListBoxUI(process.Folio);
                }
                else
                {
                    break;
                }
            }

            this.WriteMessageToUI("Deletion of instance is completed, Number of instance deleted : " + numberOfInstanceDeleted);
        }

        private void BindProcessDropdown()
        {
            IList<K2ProcessModel> processes = new K2ProcessModel().GetProcessConfiguration();
            cmbWorkflowProcess.Items.AddRange(processes.Select(t => t.ProcessName).ToArray());
        }

        private void BindEnvironmentDropdown()
        {
            IEnumerable<K2EnvironmentModel> environments = new K2EnvironmentModel().GetEnvironmentConfiguration();
            cmboxEnvironment.Items.AddRange(environments.Select(t => t.Environment).ToArray());
        }

        private void DeleteInstanceButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsFormValid())
                {
                    string environment = cmboxEnvironment.SelectedItem.ToString();
                    string processName = this.GetProcessFullName(cmbWorkflowProcess.SelectedItem.ToString());
                    DateTime startDate = dtpStartDate.Value;
                    DateTime endDate = dtpEndDate.Value;
                    this.WriteMessageToUI("Instance deletion started, Connecting to the server...");

                    using (IWorkItemCleanUpProcessor processor = this.InitializeK2Processor(environment))
                    {
                        this.WriteMessageToUI("Connected to the server successfull");
                        btnDeleteInstaces.Enabled = false;
                        this.DeleteInstances(processor, startDate, endDate, processName);
                        btnDeleteInstaces.Enabled = true;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.WriteMessageToUI("Error occurred while connecting to server");
            }
        }

        private void WriteMessageToUI(string message)
        {
            lblMessage.Text = message;
            lblMessage.Update();
            lblMessage.Refresh();
            Application.DoEvents();
        }

        private void WriteMessageToListBoxUI(string folioName)
        {
            listboxOutputLog.Items.Add("Deleted the Instance, Folio Name: " + folioName);
            listboxOutputLog.SelectedIndex = listboxOutputLog.Items.Count - 1;
            listboxOutputLog.Update();
            listboxOutputLog.Refresh();
            Application.DoEvents();
        }

        private bool IsFormValid()
        {
            bool isFormInValidState = true;
            string errorMessage = null;
            if (cmboxEnvironment.SelectedItem == null)
            {
                isFormInValidState = false;
                errorMessage = "Please select the environment\r\n ";
            }

            if (cmbWorkflowProcess.SelectedItem == null)
            {
                isFormInValidState = false;
                errorMessage += "Please select the K2 workflow process\r\n ";
            }

            if (!isFormInValidState)
            {
                MessageBox.Show(errorMessage);
            }

            return isFormInValidState;
        }

        private void BtnStopDeleting_Click(object sender, EventArgs e)
        {
            this.stopDeletingtheInstance = true;
            this.btnStopDeleting.Visible = false;
        }

        private WorkItemCleanUpProcessor InitializeK2Processor(string environmentName)
        {
            K2EnvironmentModel environment = this.GetEnvironmentConfiguration(environmentName);

            return new WorkItemCleanUpProcessor(
                environment.Server,
                uint.Parse(environment.Port),
                environment.UserName,
                environment.Password,
                "K2",
                true);
        }

        private string GetProcessFullName(string processDescription)
        {
            K2ProcessModel process = this.GetProcessConfiguration(processDescription);
            return process.ProcessFullName;
        }

        private K2ProcessModel GetProcessConfiguration(string processName)
        {
            return new K2ProcessModel().GetProcessConfiguration().Where(x => x.ProcessName == processName).FirstOrDefault();
        }

        private K2EnvironmentModel GetEnvironmentConfiguration(string environmentName)
        {
            return new K2EnvironmentModel().GetEnvironmentConfiguration().Where(x => x.Environment == environmentName).FirstOrDefault();
        }

        private void BtnSearchWorkitems_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsFormValid())
                {
                    string environment = cmboxEnvironment.SelectedItem.ToString();
                    string processName = this.GetProcessFullName(cmbWorkflowProcess.SelectedItem.ToString());
                    DateTime startDate = dtpStartDate.Value;
                    DateTime endDate = dtpEndDate.Value;
                    this.WriteMessageToUI("Connecting to the server...");

                    using (IWorkItemCleanUpProcessor processor = this.InitializeK2Processor(environment))
                    {
                        this.WriteMessageToUI("Connected to the server successfull");

                        List<ProcessInfo> processes = processor.GetAllProcessInstanceIds(startDate, endDate, processName);
                        int scannedInstances = processes.Count;
                        this.WriteMessageToUI("Connected to the server successfull, Number of instance found: " + scannedInstances);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.WriteMessageToUI("Error occurred while connecting to server");
            }
        }
    }
}