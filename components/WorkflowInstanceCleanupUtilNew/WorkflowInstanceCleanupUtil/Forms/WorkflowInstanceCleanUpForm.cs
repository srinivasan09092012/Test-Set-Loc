//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2019. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using WorkflowInstanceCleanupUtil.Lib.Models;
using WorkflowInstanceCleanupUtil.Lib.Workflow;

namespace WorkflowInstanceCleanupUtil
{
    public partial class WorkflowInstanceCleanUp : Form
    {
        private bool stopDeletingtheInstance = false;
        private List<K2ProcessModel> processes;
        private List<K2EnvironmentModel> environments;

        public WorkflowInstanceCleanUp()
        {
            this.InitializeComponent();
            this.ReadConfiguration();
            this.BindEnvironmentDropdown();
            this.BindProcessDropdown();
        }

        private void ReadConfiguration()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            WorkflowConfigurationSection workflowConfig = config.GetSection("WorkflowConfiguration") as WorkflowConfigurationSection;
            this.environments = workflowConfig.WorkflowEnvironments.ToEnvironmentList();
            this.processes = workflowConfig.WorkflowProcesses.ToProcessList();
        }

        private void BindProcessDropdown()
        {
            cmbWorkflowProcess.Items.AddRange(this.processes.Select(t => t.ProcessName).ToArray());
        }

        private void BindEnvironmentDropdown()
        {
            cmboxEnvironment.Items.AddRange(this.environments.Select(t => t.Environment).ToArray());
        }

        private void DeleteInstanceButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsFormValid())
                {
                    string environment = cmboxEnvironment.SelectedItem.ToString();
                    K2ProcessModel process = this.GetProcessInfo(cmbWorkflowProcess.SelectedItem.ToString());
                    DateTime startDate = dtpStartDate.Value;
                    DateTime endDate = dtpEndDate.Value;
                    this.WriteMessageToUI("Instance deletion started, Connecting to the server...");

                    using (IWorkItemCleanUpProcessor processor = this.InitializeK2Processor(environment))
                    {
                        this.WriteMessageToUI("Connected to the server successfull");
                        btnDeleteInstaces.Enabled = false;
                        this.DeleteProcessInstances(processor, startDate, endDate, process);
                        btnDeleteInstaces.Enabled = true;
                        btnStopDeleting.Visible = false;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.WriteMessageToUI("Error occurred while connecting to server");
            }
        }

        private void DeleteProcessInstances(IWorkItemCleanUpProcessor processor, DateTime startDate, DateTime endDate, K2ProcessModel process)
        {
            this.DeleteInstances(processor, startDate, endDate, process);

            foreach (var child in process.ChildProcesses)
            {
                this.DeleteInstances(processor, startDate, endDate, child);
            }
        }

        private void DeleteInstances(IWorkItemCleanUpProcessor processor, DateTime startDate, DateTime endDate, K2ProcessModel processInfo)
        {
            int numberOfInstanceDeleted = 0;
            List<ProcessInfo> processes = processor.GetAllProcessInstanceIds(startDate, endDate, processInfo.ProcessFullName);
            int scannedInstances = processes.Count;
            this.WriteMessageToUI(string.Format("{0} - Number of instance found to be deleted are {1}", processInfo.ProcessName, scannedInstances));
            btnStopDeleting.Visible = true;
            this.stopDeletingtheInstance = false;

            foreach (var process in processes)
            {
                if (this.stopDeletingtheInstance)
                {
                    break;
                }

                processor.DeleteProcessInstances(process.ID, checkBoxDeleteLogs.Checked);
                numberOfInstanceDeleted++;
                this.WriteMessageToUI(string.Format("{0} - Number of instance found to be deleted are {1}, Deleted {2}/{3}", processInfo.ProcessName, scannedInstances, numberOfInstanceDeleted, scannedInstances));
                this.WriteMessageToListBoxUI(process.Folio);
            }

            this.WriteMessageToUI(string.Format("{0} - Deletion of instance is completed, Number of instance deleted : {1}", processInfo.ProcessName, numberOfInstanceDeleted));
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

        private K2ProcessModel GetProcessInfo(string processDescription)
        {
            return this.processes.Where(x => x.ProcessName == processDescription).First();
        }

        private K2EnvironmentModel GetEnvironmentConfiguration(string environmentName)
        {
            return this.environments.Where(x => x.Environment == environmentName).First();
        }

        private void BtnSearchWorkitems_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsFormValid())
                {
                    string environment = cmboxEnvironment.SelectedItem.ToString();
                    string processName = this.GetProcessInfo(cmbWorkflowProcess.SelectedItem.ToString()).ProcessFullName;
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