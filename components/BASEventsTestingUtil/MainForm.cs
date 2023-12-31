﻿using BASEventsTestingUtil.EventDistribution;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
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
        private string selectedFolderForMultipleFiles = string.Empty;


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

            ListItemSection tenantIdsSection = ConfigurationManager.GetSection("myTenants") as ListItemSection;
            for (int i = 0; i < tenantIdsSection.Values.Count; i++)
            {
                this.tenantIds.Items.Add(new ListItem()
                {
                    Name = tenantIdsSection.Values[i].Name + " (" + tenantIdsSection.Values[i].Value + ")",
                    Value = tenantIdsSection.Values[i].Value
                }
                );
            }

            this.tenantIds.SelectedIndex = 0;

            ListItemSection moduleIdsSection = ConfigurationManager.GetSection("myModules") as ListItemSection;
            for (int i = 0; i < moduleIdsSection.Values.Count; i++)
            {
                this.moduleIDs.Items.Add(new ListItem()
                    {
                        Name = moduleIdsSection.Values[i].Name,
                        Value = moduleIdsSection.Values[i].Value
                    }
                );
            }

            this.moduleIDs.SelectedIndex = 0;

            this.numericUpDownEventsNumbers.Value = int.Parse(ConfigurationManager.AppSettings["ThreadCount"].ToString());

            bool allowMultiple = false;
            bool.TryParse(ConfigurationManager.AppSettings["AllowMultiple"], out allowMultiple);

            buttonPressureTest.Visible = allowMultiple;
            numericUpDownEventsNumbers.Visible = allowMultiple;
            moduleIDTooltip.SetToolTip(this.moduleIDs, "Used only when Publishing to Event Distribution Service");
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
                        writer.Formatting = System.Xml.Formatting.Indented;
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
                tbError.Text += (msg);
            }

            return result;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            EventMessage em = new EventMessage();
            

            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrlOverride"];

            if (!string.IsNullOrWhiteSpace(this.selectedFolderForMultipleFiles))
            {
                foreach (string file in Directory.GetFiles(this.selectedFolderForMultipleFiles, "*.xml"))
                {
                    this.LoadPayload(file);
                    em.CommitID = Guid.NewGuid().ToString();
                    serviceUrl = this.InitializeEvents(em, serviceUrl);
                    Cursor cursor = this.Cursor;
                    Cursor.Current = Cursors.WaitCursor;
                    Tuple<string, bool> tuple = this.ProcessEvents(em, serviceUrl, false);
                    Cursor.Current = cursor;
                    using (StreamWriter streamWriter = new StreamWriter(this.selectedFolderForMultipleFiles + "\\" + ConfigurationManager.AppSettings["LogFileNameForMultipleFileProcess"], true))
                    {
                        streamWriter.WriteLine((object)DateTime.Now);
                        streamWriter.WriteLine(tuple.Item2 ? "Event Executed Successfully" : "Event Failed - " + tuple.Item1);
                    }
                }
            }
            else
            {
                em.CommitID = Guid.NewGuid().ToString();
                serviceUrl = InitializeEvents(em, serviceUrl);
                var cursor = this.Cursor;
                Cursor.Current = Cursors.WaitCursor;
                var message = ProcessEvents(em, serviceUrl, false);
                Cursor.Current = cursor;

                MessageBox.Show(message.Item1, "Event Result", MessageBoxButtons.OK, (message.Item2) ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                tbError.Text = message.Item1;
            }
            buttonNormalTest.Enabled = true;
            buttonPressureTest.Enabled = true;
            this.selectedFolderForMultipleFiles = string.Empty;
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

            bool useAsyncForMultiEvents = false;
            bool.TryParse(ConfigurationManager.AppSettings["UseAsyncForMultiEvents"], out useAsyncForMultiEvents);

            serviceUrl = InitializeEvents(em, serviceUrl);

            if (useAsyncForMultiEvents)
            {
                AsyncProcessEvents(em, serviceUrl, threadCount);
            }
            else
            {
                var cursor = this.Cursor;
                Cursor.Current = Cursors.WaitCursor;
                tbError.Text += (System.Environment.NewLine + "*************** Test Results ***************" + System.Environment.NewLine);

                for (int i = 0; i < threadCount; i++)
                {
                    var message = ProcessEvents(em, serviceUrl, false);
                    Cursor.Current = cursor;
                    tbError.Text += message.Item1 + " for thread " + i + System.Environment.NewLine;
                }

                buttonNormalTest.Enabled = true;
                buttonPressureTest.Enabled = true;
            }
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
                MessageBox.Show(eventsCount + " Event(s) submitted, please see details in \"Logs\"!", "Events Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            var eventNameAttribute = ConfigurationManager.AppSettings["EventNameAttribute"];
            var eventNameSpaceAttribute = ConfigurationManager.AppSettings["EventNameSpaceAttribute"];

            em.TenantID = ConfigurationManager.AppSettings["TenantId"];
            if (string.IsNullOrWhiteSpace(em.TenantID))
            {
                em.TenantID = ((ListItem)this.tenantIds.SelectedItem).Value;
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

            XmlNodeList eventFilterMetadataNodeList = this.payloadDocument.GetElementsByTagName("EventFilterMetadata");
            if (eventFilterMetadataNodeList == null || eventFilterMetadataNodeList.Count == 0)
            {
                eventFilterMetadataNodeList = this.payloadDocument.GetElementsByTagName("a:EventFilterMetadata");
            }
            em.EventFilterMetadata = eventFilterMetadataNodeList == null ? null : eventFilterMetadataNodeList.Count == 0 ? null : eventFilterMetadataNodeList[0].InnerXml == string.Empty ? null : eventFilterMetadataNodeList[0].OuterXml.ToString();


            XmlNodeList INGroupIdList = this.payloadDocument.GetElementsByTagName("INGroupId");
            if (INGroupIdList == null || INGroupIdList.Count == 0)
            {
                INGroupIdList = this.payloadDocument.GetElementsByTagName("a:INGroupId");
            }
            em.INGroupId = INGroupIdList == null ? Guid.Empty : INGroupIdList.Count == 0 ? Guid.Empty : INGroupIdList[0].InnerXml == string.Empty ? Guid.Empty : Guid.Parse(INGroupIdList[0].InnerXml);

            XmlNodeList EventSequenceId = this.payloadDocument.GetElementsByTagName("EventSequenceId");
            if (EventSequenceId == null || EventSequenceId.Count == 0)
            {
                EventSequenceId = this.payloadDocument.GetElementsByTagName("a:EventSequenceId");
            }
            em.EventSequenceId = EventSequenceId == null ? null : EventSequenceId.Count == 0 ? null : EventSequenceId[0].InnerXml == string.Empty ? null : EventSequenceId[0].InnerXml;

            XmlNodeList IsLastFromGroup = this.payloadDocument.GetElementsByTagName("IsLastFromGroup");
            if (IsLastFromGroup == null || IsLastFromGroup.Count == 0)
            {
                IsLastFromGroup = this.payloadDocument.GetElementsByTagName("a:IsLastFromGroup");
            }
            em.IsLastFromGroup = IsLastFromGroup == null ? false : IsLastFromGroup.Count == 0 ? false : IsLastFromGroup[0].InnerXml == string.Empty ? false : bool.Parse(IsLastFromGroup[0].InnerXml);

            XmlNodeList AggregateRootID = this.payloadDocument.GetElementsByTagName("AggregateRootID");
            if (AggregateRootID == null || AggregateRootID.Count == 0)
            {
                AggregateRootID = this.payloadDocument.GetElementsByTagName("a:AggregateRootID");
            }
            em.AggregateRootID = AggregateRootID == null ? null : AggregateRootID.Count == 0 ? null : AggregateRootID[0].InnerXml == string.Empty ? null : AggregateRootID[0].InnerXml;

            XmlNodeList AggregateRootType = this.payloadDocument.GetElementsByTagName("AggregateRootType");
            if (AggregateRootType == null || AggregateRootType.Count == 0)
            {
                AggregateRootType = this.payloadDocument.GetElementsByTagName("a:AggregateRootType");
            }
            em.AggregateRootType = AggregateRootType == null ? null : AggregateRootType.Count == 0 ? null : AggregateRootType[0].InnerXml == string.Empty ? null : AggregateRootType[0].InnerXml;


            StringBuilder sb = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = ("  "),
                Encoding = Encoding.UTF8,
                OmitXmlDeclaration = true
            };

            using (var writer = XmlWriter.Create(sb, settings))
            {
                this.payloadDocument.WriteContentTo(writer);
                writer.Flush();
            }

            em.EventPayLoad = Encoding.UTF8.GetBytes(sb.ToString());
            if (string.IsNullOrWhiteSpace(serviceUrl))
            {
                serviceUrl = ((ListItem)this.cbEndpoint.SelectedItem).Value;
            }
            
            if (!string.IsNullOrWhiteSpace(((ListItem)this.moduleIDs.SelectedItem).Value))
            {
                em.ModuleName = ((ListItem)this.moduleIDs.SelectedItem).Value;

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
                if (serviceUrl.ToLower().StartsWith("https:"))
                {
                    binding.Security.Mode = BasicHttpSecurityMode.Transport;
                }

                binding.ReceiveTimeout = new TimeSpan(0, 20, 0);
                binding.SendTimeout = new TimeSpan(0, 20, 0);
                EndpointAddress address = new EndpointAddress(serviceUrl);
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnbrowseFolder_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(this.folderBrowserDialog1.SelectedPath))
                this.selectedFolderForMultipleFiles = this.folderBrowserDialog1.SelectedPath;
            this.buttonNormalTest.Enabled = true;
            this.buttonPressureTest.Enabled = true;
        }
    }
}
