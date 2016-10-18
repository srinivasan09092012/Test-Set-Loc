using ProviderManagement.EnrollmentTestClient.EventDistribution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
            this.cbEndpoint.Items.Add(new ListItem()
            {
                Name = "DEV  (http://206.122.21.177:8010/HP.HSP.UA3.ProviderMgmt/R1.0/ProviderEventService.svc)",
                Value = "http://206.122.21.177:8010/HP.HSP.UA3.ProviderMgmt/R1.0/ProviderEventService.svc"
            });
            this.cbEndpoint.Items.Add(new ListItem()
            {
                Name = "TEST (http://localhost:40520/ProviderEventService.svc)",
                Value = "http://localhost:40520/ProviderEventService.svc"
            });
            this.cbEndpoint.SelectedIndex = 0;            
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Xml Files (*.xml)|*.xml";
                dialog.InitialDirectory = Environment.CurrentDirectory;
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.LoadPayload(dialog.FileName);
                }
            }
        }

        private void LoadPayload(string filePath)
        {
            string formatted = GetFormattedXml(filePath);

            tbFileName.Text = filePath;
            tbPayloadContent.Text = formatted;
            btnSubmit.Enabled = true;
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
            btnSubmit.Enabled = false;            
            string state = "initializing event message properties";
            EventMessage em = new EventMessage();

            em.EventNamespace = ConfigurationManager.AppSettings["EventNamespace"];
            MessageBox.Show("EventNamespace 1 = " + em.EventNamespace);

            em.EventType = ConfigurationManager.AppSettings["EventName"];
            em.TenantID = ConfigurationManager.AppSettings["TenantId"];

            if (string.IsNullOrWhiteSpace(em.EventNamespace))
            {
                em.EventNamespace = "HP.HSP.UA3.ProviderEnrollment.BAS.EnrollmentSvc.Contracts.Events";
            }

            MessageBox.Show("EventNamespace 2 = " + em.EventNamespace);
            if (string.IsNullOrWhiteSpace(em.EventType))
            {
                em.EventType = "EnrollmentApproved";
            }

            if (string.IsNullOrWhiteSpace(em.TenantID))
            {
                em.TenantID = "081e354b-2184-47fe-b69d-3c5229d8bccf";
            }

            string serviceUrl = "";
            string urlOverride = ConfigurationManager.AppSettings["ServiceUrlOverride"];
            if (!string.IsNullOrWhiteSpace(urlOverride))
            {
                serviceUrl = urlOverride;
            }
            else
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
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error while {0}: {1}", state, ex.Message));
            }
            finally
            {
                btnSubmit.Enabled = true;
            }
        }
    }
}
