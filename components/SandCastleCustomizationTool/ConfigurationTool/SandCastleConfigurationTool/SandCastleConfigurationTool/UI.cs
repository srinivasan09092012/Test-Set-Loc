using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace SandCastleConfigurationTool
{
    public partial class UI : Form
    {
        public UI()
        {
            InitializeComponent();
        }

        private void UI_Load(object sender, EventArgs e)
        {
            var modulesRegistered = ConfigurationManager.AppSettings["HPPModules"].Split(',').ToList();

            foreach (string module in modulesRegistered)
            {
                modulListCtrl.Items.Add(module);
            }

        }

        private void SaveChanges_Click(object sender, EventArgs e)
        {

        }

        private void modulListCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void modulListCtrl_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
