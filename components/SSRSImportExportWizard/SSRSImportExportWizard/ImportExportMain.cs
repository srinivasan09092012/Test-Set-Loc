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
    public partial class ImportExportMain : Form
    {
        public ImportExportMain()
        {
            InitializeComponent();
            ImportExportPanel.Controls.Clear();
            ImportExportPanel.Controls.Add(new ImportControl());
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ImportExportPanel.Controls.Clear();
            ImportExportPanel.Controls.Add(new ViewDataSource());
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportExportPanel.Controls.Clear();
            ImportExportPanel.Controls.Add(new ExportControl());
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportExportPanel.Controls.Clear();
            ImportExportPanel.Controls.Add(new ImportControl());
        }

        private void ExitMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
