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
            MessageBox.Show("Tenant Configuration load complete. " + loadServicesSuccessful + " Services loaded and " + loadErrors + " errors reported.", "Tenant Load Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void LoadServices(ref int loadServicesSuccessful, ref int loadErrors)
        {
            int currentRow = 0;
            bool updated = true;
            string tenantModuleId = string.Empty;

            for (int i = 0; i < MainForm.Services.Count; i++)
            {
                tenantModuleId = ConfigurationManager.AppSettings[this.MainForm.Services[i].IocContainer + "TenantModuleId"];

                if (tenantModuleId != null)
                {
                    Service service = new Service();
                    service.MainForm = this.MainForm;
                    service.ServiceId = Guid.Parse(this.MainForm.Services[i].Id);
                    service.TenantModuleId = Guid.Parse(tenantModuleId);
                    service.Name = this.MainForm.Services[i].Name;

                    if (this.MainForm.Services[i].SecurityRightId != "")
                    {
                        service.SecurityRightItemId = Guid.Parse(this.MainForm.Services[i].SecurityRightId);
                    }
                        
                    service.LabelItemContentId = this.MainForm.Services[i].LabelConentID;
                    service.DefaultText = this.MainForm.Services[i].DefaultText;
                    service.BaseUrl = this.MainForm.Services[i].BaseURL;
                    service.IocContainer = this.MainForm.Services[i].IocContainer;

                    if (service.GetServiceId(service) == null)
                    {
                        try
                        {
                            service = service.AddService(service);
                        }
                        catch
                        {
                            service = null;
                        }

                        if (service != null)
                        {
                            this.MainForm.Services[i].Action = "Added";
                            loadServicesSuccessful++;
                        }
                        else
                        {
                            this.MainForm.Services[i].Action = "Add Error";
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
                            loadErrors++;
                        }
                    }
                }
                else
                {
                    this.MainForm.Services[i].Action = "TM_ID Error";
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
