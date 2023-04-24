using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HPPLocalNugetMgrVSIXProject.HPP
{
    /// <summary>
    ///     Primary purpose of this form is to display information if a run-time
    ///     exception occurs.  But it can be used to display warning messages 
    ///     and such if needed.
    /// </summary>
    public partial class FormOutput : Form
    {
        public FormOutput()
        {
            InitializeComponent();
        }

        public void SetOutputText(string text)
        {
            this.OutputTextBox.Text = text;
        }

        private void FormOutput_Load(object sender, EventArgs e)
        {

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            try
            {

                this.OutputTextBox.Width = this.Width - (this.OutputTextBox.Left * 3);
                this.OutputTextBox.Height = this.Height - (this.OutputTextBox.Top * 3);
            }
            catch
            {
                // Do nothing
            }
        }

        private void OutputTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
