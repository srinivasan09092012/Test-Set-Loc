using HPE.HSP.UA3.Core.API.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSRSImportExportWizard
{
    public partial class ReportCompare : Form
    {
        private string navigateTo = string.Empty;

        public ReportCompare()
        {
            InitializeComponent();
        }

        public ReportCompare(string url)
        {
            this.navigateTo = url;
            InitializeComponent();
        }

        private void ReportCompare_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.navigateTo))
                {
                    throw new Exception("invalid url");
                };
                ReportCompareBrowser.Url = new Uri(@"file:///" + navigateTo);
                ReportCompareBrowser.Show();
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogFatal("Error while loading compare window", ex);
            }
        }
    }
}
