using HP.HSP.UA3.Administration.UX.Common;
using HP.HSP.UA3.Utilities.LoadTenantDb.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    public partial class Confirmation_Services : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Confirmation_Services()
        {
            InitializeComponent();
        }

        public MainForm MainForm { get; set; }

        private void RefreshGrid()
        {
            List<string> row = new List<string>();

            for (int i = 0; i < MainForm.Services.Count; i++)
            {
                if (string.IsNullOrEmpty(MainForm.Services[i].Id))
                {
                    MainForm.Services[i].Id = "<< NULL OR MISSING VALUE >>";
                }

                row.Clear();
                row.Add(MainForm.Services[i].Action);
                row.Add(MainForm.Services[i].Module.Name);
                row.Add(MainForm.Services[i].Id);
                row.Add(MainForm.Services[i].Name);
                row.Add(MainForm.Services[i].BaseURL);
                row.Add(MainForm.Services[i].LabelConentID);
                row.Add(MainForm.Services[i].DefaultText);
                row.Add(MainForm.Services[i].SecurityRightId);
                row.Add(MainForm.Services[i].IocContainer);

                this.servicesGridView.Rows.Add(row.ToArray());
            }
        }

        private void ProcessLoad(object sender, EventArgs e)
        {
            int loadServicesSuccessful = 0;
            int loadErrors = 0;

            Cursor.Current = Cursors.WaitCursor;

            this.LoadServices(ref loadServicesSuccessful, ref loadErrors);
            Cursor.Current = Cursors.Default;
            log.Info("Tenant Configuration load complete. " + loadServicesSuccessful + " Services loaded and " + loadErrors + " errors reported.");
            MessageBox.Show("Tenant Configuration load complete. " + loadServicesSuccessful + " Services loaded and " + loadErrors + " errors reported.", "Tenant Load Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void LoadServices(ref int loadServicesSuccessful, ref int loadErrors)
        {
            int currentRow = 0;
            bool updated = true;
            bool added = false;
            string tenantModuleId = string.Empty;
            bool skipProcessing = false;

            for (int i = 0; i < MainForm.Services.Count; i++)
            {
                skipProcessing = false;
                tenantModuleId = ConfigurationManager.AppSettings[this.MainForm.Services[i].IocContainer + "TenantModuleId"];

                if (tenantModuleId != null)
                {
                    Service service = new Service();
                    service.MainForm = this.MainForm;
                    //Check to see if we have a valid GUID if not error off and skip process 
                    Guid testNewGuid;
                    if (!Guid.TryParse(this.MainForm.Services[i].Id, out testNewGuid))
                    {
                        string systemGeneratedGuid = Guid.NewGuid().ToString("D").ToUpper();
                        log.Warn("Warn Confirmation_Services.LoadServices Id Warning " +
                            " Service Name = " + this.MainForm.Services[i].Id +
                            " INVALID ID GUID=" + this.MainForm.Services[i].Id + 
                            " Changing to System Generated GUID=" + systemGeneratedGuid);
                        this.MainForm.Services[i].Id = systemGeneratedGuid;
                    }
                    service.ServiceId = Guid.Parse(this.MainForm.Services[i].Id);

                    service.TenantModuleId = Guid.Parse(tenantModuleId);
                    service.Name = this.MainForm.Services[i].Name;

                    if (this.MainForm.Services[i].SecurityRightId != "")
                    {
                        //Check to see if we have a valid GUID if not error off and skip process 
                        Guid testNewGuidSecurity;
                        if (!Guid.TryParse(this.MainForm.Services[i].SecurityRightId, out testNewGuidSecurity))
                        {
                            log.Error("Error Confirmation_Services.LoadServices Security Right Id Error " +
                                "Service Name = " + this.MainForm.Services[i].Name +
                                "INVALID Security Right Item GUID=" + this.MainForm.Services[i].SecurityRightId);
                            skipProcessing = true;
                            this.MainForm.Services[i].Action = "Add Error";
                            loadErrors++;
                        }
                        else
                        {
                            service.SecurityRightItemId = Guid.Parse(this.MainForm.Services[i].SecurityRightId);
                        }
                    }

                    if (!skipProcessing)
                    {
                        service.LabelItemContentId = this.MainForm.Services[i].LabelConentID;
                        service.DefaultText = this.MainForm.Services[i].DefaultText;
                        service.BaseUrl = this.MainForm.Services[i].BaseURL;
                        service.IocContainer = this.MainForm.Services[i].IocContainer;

                        if (service.GetServiceId(service) == null)
                        {
                            added = service.AddService(service);

                            if (added)
                            {
                                this.MainForm.Services[i].Action = "Added";
                                loadServicesSuccessful++;
                            }
                            else
                            {
                                this.MainForm.Services[i].Action = "Add Error";
                                log.Error("Error Confirmation_Services.LoadServices Add Error " +
                                    "Name=" + service.ToString());
                                loadErrors++;
                            }
                        }
                        else
                        {
                            try
                            {
                                updated = service.UpdateService(service);
                            }
                            catch
                            {
                                updated = false;
                            }

                            if (updated)
                            {
                                this.MainForm.Services[i].Action = "Updated";
                                loadServicesSuccessful++;
                            }
                            else
                            {
                                this.MainForm.Services[i].Action = "Update Error";
                                log.Error("Error Confirmation_Services.LoadServices Update Error " +
                                    "Name=" + service.ToString());
                                loadErrors++;
                            }
                        }
                    }
                    
                }
                else
                {
                    this.MainForm.Services[i].Action = "TM_ID Error";
                    log.Error("Error Confirmation_Services.LoadServices BAD TENANT MODULE ID - TM_ID Error " +
                        "IocContainer=" + this.MainForm.Services[i].IocContainer);
                    loadErrors++;
                }

                this.servicesGridView.Rows[currentRow].Cells[0].Value = this.MainForm.Services[i].Action;

                if (this.servicesGridView.Rows.Count > currentRow + 1)
                {
                    this.servicesGridView.CurrentCell = this.servicesGridView.Rows[currentRow + 1].Cells[0];
                    this.servicesGridView.Rows[currentRow + 1].Selected = true;
                }

                this.servicesGridView.Refresh();
                currentRow++;
            }
        }

        private void Confirmation_Services_Load(object sender, EventArgs e)
        {
            this.RefreshGrid();

            if (MainForm.Services.Count > 0)
            {
                this.loadPushButton.Enabled = true;
            }
            else
            {
                this.loadPushButton.Enabled = false;
            }
        }

        private void cancelPushButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
