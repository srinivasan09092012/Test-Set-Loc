using BASEventsTestingUtil.EventDistribution;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace BASEventsTestingUtil
{
    public partial class MainForm : Form
    {
        private XmlDocument payloadDocument;
        private static int threadCount = 10;


        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ListItemSection endPointSection = ConfigurationManager.GetSection("myEndPoints") as ListItemSection;
            for (int i = 0; i < endPointSection.Values.Count; i++)
            {
                this.cbEndpoint.Items.Add(new ListItem()
                {
                    Name = endPointSection.Values[i].Name + " (" + endPointSection.Values[i].Value + ")",
                    Value = endPointSection.Values[i].Value
                }
                );
            }

            this.cbEndpoint.SelectedIndex = 0;

            this.numericUpDownEventsNumbers.Value = int.Parse(ConfigurationManager.AppSettings["ThreadCount"].ToString());

            ListItemSection eventSection = ConfigurationManager.GetSection("myEvents") as ListItemSection;
            for (int i = 0; i < eventSection.Values.Count; i++)
            {
                this.cbEventName.Items.Add(new ListItem()
                {
                    Name = eventSection.Values[i].Name,
                    Value = eventSection.Values[i].Value
                }
                );
            }

            this.cbEventName.SelectedIndex = 0;
        }

        private void LoadPayload(string filePath)
        {
            string formatted = GetFormattedXml(filePath);

            tbFileName.Text = filePath;
            tbPayloadContent.Text = formatted;
            buttonNormalTest.Enabled = true;
            buttonPressureTest.Enabled = true;
            wbXML.Navigate(filePath);
        }

        private string GetFormattedXml(string filePath)
        {
            string result = string.Empty;

            try
            {
                this.payloadDocument = new XmlDocument();
                this.payloadDocument.Load(filePath);

                using (var ms = new MemoryStream())
                {
                    using (var writer = new XmlTextWriter(ms, Encoding.UTF8))
                    {
                        writer.Formatting = Formatting.Indented;
                        this.payloadDocument.WriteContentTo(writer);
                        writer.Flush();
                        ms.Flush();

                        ms.Position = 0;
                        using (var reader = new StreamReader(ms))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = string.Format("Error loading {0}: ", ex.Message);
                tbError.Text+=(msg);
            }

            return result;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            EventMessage em = new EventMessage();
            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrlOverride"];
            serviceUrl = InitializeEvents(em, serviceUrl);
            AsyncProcessEvents(em, serviceUrl, 1);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string fileDirectory = ConfigurationManager.AppSettings["FileDirectory"];
            if (string.IsNullOrWhiteSpace(fileDirectory))
            {
                fileDirectory = Environment.CurrentDirectory;
            }

            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Xml Files (*.xml)|*.xml";
                dialog.InitialDirectory = fileDirectory;
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.LoadPayload(dialog.FileName);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.payloadDocument == null)
            {
                this.payloadDocument = new XmlDocument();
            }
            this.payloadDocument.LoadXml(tbPayloadContent.Text);
            using (var sfd = new SaveFileDialog())
            {
                string fileDirectory = ConfigurationManager.AppSettings["FileDirectory"];
                if (!string.IsNullOrWhiteSpace(fileDirectory))
                {
                    sfd.InitialDirectory = fileDirectory;
                }

                sfd.Filter = "Xml Files (*.xml)|*.xml";
                sfd.FilterIndex = 1;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    this.payloadDocument.Save(sfd.FileName);
                    this.LoadPayload(sfd.FileName);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.LoadPayload(tbFileName.Text);
        }

        private void btnPresssureTest_Click(object sender, EventArgs eve)
        {
            EventMessage em = new EventMessage();
            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrlOverride"];
            serviceUrl = InitializeEvents(em, serviceUrl);
            AsyncProcessEvents(em, serviceUrl, threadCount);
        }

        private void AsyncProcessEvents(EventMessage em, string serviceUrl, int eventsCount)
        {
            var ui = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Factory.StartNew(() =>
            {
                SubmitEventsParallel(em, serviceUrl, eventsCount, ui);

            }).ContinueWith((ante) =>
            {
                MessageBox.Show (eventsCount + " Event(s) submitted, please see details in \"Logs\"!");
                buttonNormalTest.Enabled = true;
                buttonPressureTest.Enabled = true;
            }, ui);
        }

        private void SubmitEventsParallel(EventMessage em, string serviceUrl, int eventsCount, TaskScheduler ui)
        {
            List<Task<string>> tasks = new List<Task<string>>();
            for (int i = 0; i < eventsCount; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => ProcessEvents(em, serviceUrl)));
            }
            var finalTask = Task.Factory.ContinueWhenAll(tasks.ToArray(), submitEventsTasks =>
            {
                int nSuccessfulTasks = 0;
                int nFailed = 0;
                int nServiceException = 0;
                tbError.Text += (System.Environment.NewLine + "*************** Test Results ***************" + System.Environment.NewLine);
                foreach (var t in submitEventsTasks)
                {
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        tbError.Text += t.Result;
                        nSuccessfulTasks++;
                    }

                    if (t.Status == TaskStatus.Faulted)
                    {
                        nFailed++;
                        t.Exception.Handle((e) =>
                        {
                            if (e is FaultException<EventDistribution.ServiceException>)
                            {
                                tbError.Text += (String.Format("Service Exception Failure in the thread#: '" + e.Data["ThreadID"] + "'" + System.Environment.NewLine + "{0}" + System.Environment.NewLine, e.Message));
                                nServiceException++;
                            }
                            else
                            {
                                tbError.Text += (String.Format("Other Exception Failure in the thread#: '" + e.Data["ThreadID"] + "'" + System.Environment.NewLine + "{0}" + System.Environment.NewLine, e.Message));
                            }

                            return true;
                        });
                    }
                }
                tbError.Text += (String.Format(System.Environment.NewLine + "Test Results Summary" + System.Environment.NewLine + "{0} out of {1} events submitted successfully" + System.Environment.NewLine, nSuccessfulTasks, eventsCount));
                if (nFailed > 0)
                {
                    tbError.Text += (String.Format("{0} tasks failed for the following reasons:" + System.Environment.NewLine, nFailed));
                    if (nFailed == nServiceException)
                        tbError.Text += (String.Format("Service Exception: {0}" + System.Environment.NewLine, nServiceException));
                    if (nFailed != nServiceException)
                        tbError.Text += (String.Format("Other Exception: {0}" + System.Environment.NewLine, nFailed - nServiceException));
                }
            }
            , CancellationToken.None, TaskContinuationOptions.None, ui);
            finalTask.Wait();
        }

        private string InitializeEvents(EventMessage em, string serviceUrl)
        {
            tbError.Text += string.Empty;
            buttonNormalTest.Enabled = false;
            buttonPressureTest.Enabled = false;

            string state = "initializing event message properties";

            StringBuilder sb = new StringBuilder();
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";
            settings.Encoding = Encoding.Unicode;

            state = "formatting payload into binary";



            em.TenantID = ConfigurationManager.AppSettings["TenantId"];
            if (string.IsNullOrWhiteSpace(em.TenantID))
            {
                em.TenantID = "081e354b-2184-47fe-b69d-3c5229d8bccf";
            }

            em.EventType = ConfigurationManager.AppSettings["EventNameOverride"];
            if (string.IsNullOrWhiteSpace(em.EventType))
            {
                em.EventType = ((ListItem)this.cbEventName.SelectedItem).Name;
            }

            em.EventNamespace = ConfigurationManager.AppSettings["EventNamespaceOverride"];
            if (string.IsNullOrWhiteSpace(em.EventNamespace))
            {
                em.EventNamespace = ((ListItem)this.cbEventName.SelectedItem).Value;
            }

            using (var writer = XmlWriter.Create(sb, settings))
            {
                this.payloadDocument.WriteContentTo(writer);
                writer.Flush();
            }

            em.EventPayLoad = Encoding.Unicode.GetBytes(sb.ToString());
            if (string.IsNullOrWhiteSpace(serviceUrl))
            {
                serviceUrl = ((ListItem)this.cbEndpoint.SelectedItem).Value;
            }

            return serviceUrl;
        }

        private string ProcessEvents(EventMessage em, string serviceUrl)
        {
            string state = string.Empty;
            try
            {
                state = "initializing remote client";
                using (EventDistributionClient client = new EventDistributionClient())
                {
                    state = "setting remote url" + System.Environment.NewLine;
                    client.Endpoint.Address = new System.ServiceModel.EndpointAddress(serviceUrl);

                    state += "opening remote client" + System.Environment.NewLine;
                    client.Open();

                    state += "invoking event distribution service" + System.Environment.NewLine;

                    client.ProcessEvent(em);
                    return "Succeed: Event submitted successfully in the thread#: '"+Thread.CurrentThread.ManagedThreadId+"'."+System.Environment.NewLine;
                }
            }
            catch (FaultException<EventDistribution.ServiceException> svcEx)
            {
                StringBuilder builder = new StringBuilder();
                foreach (string message in svcEx.Detail.ErrorMessages)
                {
                    builder.Append(message + Environment.NewLine);
                }
                svcEx.Data["ThreadID"] = Thread.CurrentThread.ManagedThreadId;
                throw svcEx;
            }
            catch (Exception ex)
            {
                ex.Data["ThreadID"] = Thread.CurrentThread.ManagedThreadId;
                throw ex;
            }
        }

        private void numericUpDownEventsNumbers_ValueChanged(object sender, EventArgs e)
        {
            threadCount = (int)this.numericUpDownEventsNumbers.Value;
            this.buttonPressureTest.Text = "Pressure Test (" + threadCount + ") Requests";
        }
    }
}
