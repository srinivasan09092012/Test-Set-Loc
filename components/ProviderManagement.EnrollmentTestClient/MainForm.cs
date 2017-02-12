using ProviderManagement.EnrollmentTestClient.EventDistribution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ProviderManagement.EnrollmentTestClient
{
    public partial class MainForm : Form
    {
        private XmlDocument payloadDocument;

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
            btnSubmit.Enabled = true;

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
                MessageBox.Show(msg);
            }

            return result;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            tbError.Text = string.Empty;
            btnSubmit.Enabled = false;            
            string state = "initializing event message properties";
            EventMessage em = new EventMessage();

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

            string serviceUrl = ConfigurationManager.AppSettings["ServiceUrlOverride"];
            if (string.IsNullOrWhiteSpace(serviceUrl))
            {
                serviceUrl = ((ListItem)this.cbEndpoint.SelectedItem).Value;
            }

            StringBuilder sb = new StringBuilder();
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";
            settings.Encoding = Encoding.Unicode;

            state = "formatting payload into binary";
            using (var writer = XmlWriter.Create(sb, settings))
            {
                this.payloadDocument.WriteContentTo(writer);
                writer.Flush();
            }

            em.EventPayLoad = Encoding.Unicode.GetBytes(sb.ToString());

            try
            {
                state = "initializing remote client";
                using (EventDistributionClient client = new EventDistributionClient())
                {
                    state = "setting remote url";                                      
                    client.Endpoint.Address = new System.ServiceModel.EndpointAddress(serviceUrl);

                    state = "opening remote client";
                    client.Open();

                    state = "invoking event distribution service";

                    client.ProcessEvent(em);
                    MessageBox.Show("Event submitted successfully!");
                }
            }
            catch (FaultException<EventDistribution.ServiceException> svcEx)
            {
                StringBuilder builder = new StringBuilder();
                foreach (string message in svcEx.Detail.ErrorMessages)
                {
                    builder.Append(message + Environment.NewLine);
                }
                tbError.Text = svcEx.ToString();
                MessageBox.Show(string.Format("Error while {0}: {1}", state, builder.ToString()));
            }
            catch (Exception ex)
            {
                tbError.Text = ex.ToString();
                MessageBox.Show(string.Format("Error while {0}: {1}", state, ex.Message));
            }
            finally
            {
                btnSubmit.Enabled = true;
            }
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

        private void TbPayloadContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.A))
            {
                tbPayloadContent.SelectAll();
                e.Handled = e.SuppressKeyPress = true;
            }
        }
    }
}
