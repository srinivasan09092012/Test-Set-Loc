using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.X12OOPViewer.Forms
{
    /// <summary>
    /// This utility parses the source x12 string and formats it into a more readable format.
    ///     This utility utilizes the DXC OOP Factory library through the UA3 NuGet packages.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Main Form Events
        public MainForm()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                InitializeForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void AboutX12OOPViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                ShowAbout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Private Methods
        private void InitializeForm()
        {
        }

        private void ShowAbout()
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        private void ParseButton_Click(object sender, EventArgs e)
        {
            string sourceText = this.sourceTextBox.Text;
            if (!string.IsNullOrEmpty(sourceText))
            {
                try
                {
                    string parsedText = string.Empty;
                    X12Parser parser = new X12Parser();
                    Interchange interchange = parser.ParseMultiple(new MemoryStream(Encoding.ASCII.GetBytes(sourceText))).First();
                    if (x12wsRadioButton.Checked)
                    {
                        parsedText = interchange.SerializeToX12(true);
                    }
                    else if (xmlcRadioButton.Checked)
                    {
                        parsedText = interchange.Serialize(false);
                    }
                    else if (xmlncRadioButton.Checked)
                    {
                        parsedText = interchange.Serialize(true);
                    }

                    this.parseTextBox.Text = parsedText;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "X12 Parsing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
    }
}
