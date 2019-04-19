namespace WorkflowInstanceCleanupUtil
{
    partial class WorkflowInstanceCleanUp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cmboxEnvironment = new System.Windows.Forms.ComboBox();
            this.cmbWorkflowProcess = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnStopDeleting = new System.Windows.Forms.Button();
            this.listboxOutputLog = new System.Windows.Forms.ListBox();
            this.checkBoxDeleteLogs = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDeleteInstaces = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the Environment";
            // 
            // cmboxEnvironment
            // 
            this.cmboxEnvironment.FormattingEnabled = true;
            this.cmboxEnvironment.Location = new System.Drawing.Point(270, 38);
            this.cmboxEnvironment.Name = "cmboxEnvironment";
            this.cmboxEnvironment.Size = new System.Drawing.Size(476, 28);
            this.cmboxEnvironment.TabIndex = 1;
            this.cmboxEnvironment.Text = "Select the Environment";
            // 
            // cmbWorkflowProcess
            // 
            this.cmbWorkflowProcess.FormattingEnabled = true;
            this.cmbWorkflowProcess.Location = new System.Drawing.Point(270, 97);
            this.cmbWorkflowProcess.Name = "cmbWorkflowProcess";
            this.cmbWorkflowProcess.Size = new System.Drawing.Size(476, 28);
            this.cmbWorkflowProcess.TabIndex = 2;
            this.cmbWorkflowProcess.Text = "Select Workflow Process";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select the workflow process";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(151, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Start date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(157, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "End date";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(270, 162);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(148, 26);
            this.dtpStartDate.TabIndex = 6;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(270, 222);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(148, 26);
            this.dtpEndDate.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMessage);
            this.groupBox1.Controls.Add(this.btnStopDeleting);
            this.groupBox1.Controls.Add(this.listboxOutputLog);
            this.groupBox1.Controls.Add(this.checkBoxDeleteLogs);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnDeleteInstaces);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpEndDate);
            this.groupBox1.Controls.Add(this.cmboxEnvironment);
            this.groupBox1.Controls.Add(this.dtpStartDate);
            this.groupBox1.Controls.Add(this.cmbWorkflowProcess);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(31, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(891, 507);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Delete K2 Process Instances";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(25, 346);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 20);
            this.lblMessage.TabIndex = 13;
            // 
            // btnStopDeleting
            // 
            this.btnStopDeleting.Location = new System.Drawing.Point(365, 280);
            this.btnStopDeleting.Name = "btnStopDeleting";
            this.btnStopDeleting.Size = new System.Drawing.Size(127, 40);
            this.btnStopDeleting.TabIndex = 12;
            this.btnStopDeleting.Text = "Stop Deleting";
            this.btnStopDeleting.UseVisualStyleBackColor = true;
            this.btnStopDeleting.Visible = false;
            this.btnStopDeleting.Click += new System.EventHandler(this.BtnStopDeleting_Click);
            // 
            // listboxOutputLog
            // 
            this.listboxOutputLog.FormattingEnabled = true;
            this.listboxOutputLog.ItemHeight = 20;
            this.listboxOutputLog.Location = new System.Drawing.Point(29, 377);
            this.listboxOutputLog.Name = "listboxOutputLog";
            this.listboxOutputLog.Size = new System.Drawing.Size(839, 124);
            this.listboxOutputLog.TabIndex = 11;
            // 
            // checkBoxDeleteLogs
            // 
            this.checkBoxDeleteLogs.AutoSize = true;
            this.checkBoxDeleteLogs.Checked = true;
            this.checkBoxDeleteLogs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDeleteLogs.Location = new System.Drawing.Point(270, 280);
            this.checkBoxDeleteLogs.Name = "checkBoxDeleteLogs";
            this.checkBoxDeleteLogs.Size = new System.Drawing.Size(22, 21);
            this.checkBoxDeleteLogs.TabIndex = 10;
            this.checkBoxDeleteLogs.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(142, 279);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Delete logs";
            // 
            // btnDeleteInstaces
            // 
            this.btnDeleteInstaces.Location = new System.Drawing.Point(528, 280);
            this.btnDeleteInstaces.Name = "btnDeleteInstaces";
            this.btnDeleteInstaces.Size = new System.Drawing.Size(218, 40);
            this.btnDeleteInstaces.TabIndex = 8;
            this.btnDeleteInstaces.Text = "Delete the Instances";
            this.btnDeleteInstaces.UseVisualStyleBackColor = true;
            this.btnDeleteInstaces.Click += new System.EventHandler(this.DeleteInstanceButton_Click);
            // 
            // WorkflowInstanceCleanUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 582);
            this.Controls.Add(this.groupBox1);
            this.Name = "WorkflowInstanceCleanUp";
            this.Text = "K2 workflow instance clean up";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboxEnvironment;
        private System.Windows.Forms.ComboBox cmbWorkflowProcess;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDeleteInstaces;
        private System.Windows.Forms.CheckBox checkBoxDeleteLogs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listboxOutputLog;
        private System.Windows.Forms.Button btnStopDeleting;
        private System.Windows.Forms.Label lblMessage;
    }
}

