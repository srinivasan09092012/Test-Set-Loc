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

            var allowMultiple = bool.Parse(ConfigurationManager.AppSettings["AllowMultiple"]);

            buttonPressureTest.Visible = allowMultiple;
            numericUpDownEventsNumbers.Visible = allowMultiple;
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
            var cursor = this.Cursor;
            Cursor.Current = Cursors.WaitCursor;
            var message = ProcessEvents(em, serviceUrl, false);
            Cursor.Current = cursor;

            MessageBox.Show(message.Item1, "Event Result", MessageBoxButtons.OK, (message.Item2) ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            tbError.Text = message.Item1;
            buttonNormalTest.Enabled = true;
            buttonPressureTest.Enabled = true;
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

            Cursor.Current = Cursors.WaitCursor;
            Task.Factory.StartNew(() =>
            {
                SubmitEventsParallel(em, serviceUrl, eventsCount, ui);

            }).ContinueWith((ante) =>
            {
                MessageBox.Show (eventsCount + " Event(s) submitted, please see details in \"Logs\"!", "Events Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonNormalTest.Enabled = true;
                buttonPressureTest.Enabled = true;

                Cursor.Current = Cursors.AppStarting;
            }, ui);
        }

        private void SubmitEventsParallel(EventMessage em, string serviceUrl, int eventsCount, TaskScheduler ui)
        {
            List<Task<string>> tasks = new List<Task<string>>();
            for (int i = 0; i < eventsCount; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => ProcessEvents(em, serviceUrl, true).Item1));
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

            StringBuilder sb = new StringBuilder();
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";
            settings.Encoding = Encoding.Unicode;

            var eventNameAttribute = ConfigurationManager.AppSettings["EventNameAttribute"];
            var eventNameSpaceAttribute = ConfigurationManager.AppSettings["EventNameSpaceAttribute"];

            em.TenantID = ConfigurationManager.AppSettings["TenantId"];
            if (string.IsNullOrWhiteSpace(em.TenantID))
            {
                em.TenantID = "081e354b-2184-47fe-b69d-3c5229d8bccf";
            }

            em.EventType = ConfigurationManager.AppSettings["EventNameOverride"];
            if (string.IsNullOrWhiteSpace(em.EventType))
            {
                var nm = this.payloadDocument.FirstChild.Attributes[eventNameAttribute].Value;
                nm = nm.Substring(nm.LastIndexOf(':') + 1);
                em.EventType = nm;
            }

            em.EventNamespace = ConfigurationManager.AppSettings["EventNamespaceOverride"];
            if (string.IsNullOrWhiteSpace(em.EventNamespace))
            {
                var nm = this.payloadDocument.FirstChild.Attributes[eventNameSpaceAttribute].Value;
                nm = nm.Substring(nm.LastIndexOf('/') + 1);
                em.EventNamespace = nm;
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

        private Tuple<string, bool> ProcessEvents(EventMessage em, string serviceUrl, bool multipleThreads)
        {
            StringBuilder builder = new StringBuilder();
            bool isSucceed = false;
            try
            {
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.Security.Mode = BasicHttpSecurityMode.Transport;
                EndpointAddress address = new EndpointAddress(serviceUrl);
                using (ChannelFactory<IEventDistribution> factory = new ChannelFactory<IEventDistribution>(binding, address))
                {
                    EventDistribution.IEventDistribution proxy = factory.CreateChannel();
                    proxy.ProcessEvent(em);

                    if (multipleThreads)
                        builder.Append("Succeed: Event submitted successfully in the thread#: '" + Thread.CurrentThread.ManagedThreadId + "'." + System.Environment.NewLine);
                    else
                        builder.Append("Event submitted successfully." + System.Environment.NewLine);

                    isSucceed = true;
                }

                ////using (EventDistributionClient client = new EventDistributionClient())
                ////{
                ////    client.Endpoint.Address = new System.ServiceModel.EndpointAddress(serviceUrl);

                ////    client.Open();

                ////    client.ProcessEvent(em);

                ////    if (multipleThreads)
                ////        builder.Append("Succeed: Event submitted successfully in the thread#: '" + Thread.CurrentThread.ManagedThreadId + "'." + System.Environment.NewLine);
                ////    else
                ////        builder.Append("Event submitted successfully." + System.Environment.NewLine);

                ////    isSucceed = true;
                ////}
            }
            catch (FaultException<EventDistribution.ServiceException> svcEx)
            {
                builder.Append("Event failed" + System.Environment.NewLine);
                foreach (string message in svcEx.Detail.ErrorMessages)
                {
                    builder.Append(message + Environment.NewLine);
                }

                if (multipleThreads)
                {
                    svcEx.Data["ThreadID"] = Thread.CurrentThread.ManagedThreadId;

                    throw svcEx;
                }
            }
            catch (FaultException<EventDistribution.BusinessValidationException> busEx)
            {
                builder.Append("Event failed" + System.Environment.NewLine);
                foreach (var message in busEx.Detail.BusinessMessages)
                {
                    builder.Append(string.Format("Business Error: {0}; Message Key: {1}" + Environment.NewLine, message.MessageDefault, message.MessageKey) + Environment.NewLine);
                }

                if (multipleThreads)
                {
                    busEx.Data["ThreadID"] = Thread.CurrentThread.ManagedThreadId;

                    throw busEx;
                }
            }
            catch (Exception ex)
            {
                builder.Append("Event failed" + System.Environment.NewLine);
                builder.Append(ex.Message + System.Environment.NewLine);

                if (multipleThreads)
                {
                    ex.Data["ThreadID"] = Thread.CurrentThread.ManagedThreadId;

                    throw ex;
                }
            }

            return new Tuple<string, bool>(builder.ToString(), isSucceed);
        }

        private void numericUpDownEventsNumbers_ValueChanged(object sender, EventArgs e)
        {
            threadCount = (int)this.numericUpDownEventsNumbers.Value;
        }
    }
}
