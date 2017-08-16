//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HPE.HSP.UA3.Core.API.BusinessRules.Interface;
using HPE.HSP.UA3.Core.API.BusinessRules.Interface.Domain;
using HPE.HSP.UA3.Core.API.BusinessRules.Providers.InRule;
using HPE.HSP.UA3.Core.API.BusinessRules.Providers.Tests.Domain;
using HPE.HSP.UA3.Core.API.Logger.Loggers;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InRuleConnectivityTester
{
    public partial class MainForm : Form
    {
        private CoreLogger logger = new CoreLogger();

        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                InitializeConfiguration();
                ResetForm();
            }
            catch (Exception ex)
            {
               this.ProcessException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.ResetForm();
                this.ProcessRules();
            }
            catch (Exception ex)
            {
                this.ProcessException(ex);
                this.Cursor = Cursors.Default;
            }
        }

        private void InitializeConfiguration()
        {
            this.ProcessingModeComboBox.Text = ConfigurationManager.AppSettings["InRule.ProcessingMode"].ToString();
            this.ServiceUrlTextBox.Text = ConfigurationManager.AppSettings["InRule.ServiceUrl"].ToString();
            this.UserNameTextBox.Text = ConfigurationManager.AppSettings["InRule.UserName"].ToString();
            this.PasswordTextBox.Text = ConfigurationManager.AppSettings["InRule.Password"].ToString();
            this.ApplicationTextBox.Text = ConfigurationManager.AppSettings["InRule.ApplicationId"].ToString();
            this.TenantIdTextBox.Text = ConfigurationManager.AppSettings["TenantId"].ToString();
        }

        private async void ProcessRules()
        {
            string processingMode = this.ProcessingModeComboBox.Text;
            await Task.Run(() => this.ExecuteRules(processingMode));
            this.Cursor = Cursors.Default;
        }

        private void ExecuteRules(string processingMode)
        {
            Stopwatch perfTimer = new Stopwatch();
            IBusinessRulesProvider provider = null;

            try
            {
                RuleExecutionRequest request = this.BuildRequest();
                switch (processingMode)
                {
                    case "InProcess":
                        provider = new InRuleInProcessProvider(this.ServiceUrlTextBox.Text, this.UserNameTextBox.Text, this.PasswordTextBox.Text, this.ApplicationTextBox.Text);
                        break;

                    default:
                        provider = new InRuleRemoteServiceProvider(this.ServiceUrlTextBox.Text, this.UserNameTextBox.Text, this.PasswordTextBox.Text, this.ApplicationTextBox.Text);
                        break;
                }
                perfTimer.Start();
                RuleExecutionResponse response = provider.ExecuteRules(request);
                this.ResultsTextBox.Invoke((Action)delegate { this.ResultsTextBox.Text = JsonConvert.SerializeObject(response.Entity); });
            }
            catch (Exception ex)
            {
                this.ResultsTextBox.Invoke((Action)delegate { this.ResultsTextBox.Text = ex.ToString(); });
            }
            finally
            {
                perfTimer.Stop();
                this.ExecutionTimeLabel.Invoke((Action)delegate { this.ExecutionTimeLabel.Text = perfTimer.ElapsedMilliseconds.ToString("#,##0") + " ms"; });
            }
        }

        private RuleExecutionRequest BuildRequest()
        {
            RuleExecutionRequest request = new RuleExecutionRequest()
            {
                TenantId = this.TenantIdTextBox.Text,
                RuleSetId = "ValidationTestModel",
                RuleSetVersionLabel = string.Empty,
                RuleSetRevisionNumber = 0,
                RuleExecution = RuleExecutionType.Entity,
                Entity = new TestModel()
                {
                    Name = "Informational",
                    Priority = 1
                },
                Parameters = null,
                LoggingLevel = LoggingLevelType.Verbose,
            };

            return request;
        }

        private void ResetForm()
        {
            this.ExecutionTimeLabel.Text = "0 ms";
            this.ResultsTextBox.Text = string.Empty;
        }

        private void ProcessException(Exception ex)
        {
            this.ResultsTextBox.Text = ex.ToString();
            this.logger.LogFatal(ex.Message, ex);
        }
    }
}
