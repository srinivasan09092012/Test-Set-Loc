using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;
using System;
using System.Drawing;
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
        private string parsedX12Text = string.Empty;

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
            this.findTextBox.Enabled = false;
            this.findButton.Enabled = false;
            this.findCountLabel.Visible = false;
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
                    this.findCountLabel.Visible = false;

                    string parsedText = string.Empty;
                    X12Parser parser = new X12Parser();
                    Interchange interchange = parser.ParseMultiple(new MemoryStream(Encoding.ASCII.GetBytes(sourceText))).First();
                    if (this.x12wsRadioButton.Checked)
                    {
                        parsedText = interchange.SerializeToX12(true);
                    }
                    else if (this.xmlcRadioButton.Checked)
                    {
                        parsedText = interchange.Serialize(false);
                    }
                    else if (this.xmlncRadioButton.Checked)
                    {
                        parsedText = interchange.Serialize(true);
                    }

                    this.parsedX12Text = parsedText;
                    this.parseRichTextBox.Text = this.parsedX12Text;

                    this.findTextBox.Enabled = true;
                    this.findButton.Enabled = true;

                    this.parseRichTextBox.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "X12 Parsing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        private void FindButton_Click(object sender, EventArgs e)
        {
            int findCount = 0;

            string findText = this.findTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(findText))
            {
                if (!string.IsNullOrEmpty(this.parseRichTextBox.Text))
                {
                    this.findCountLabel.Visible = true;

                    this.parseRichTextBox.Clear();
                    this.parseRichTextBox.Text = this.parsedX12Text;
                    this.parseRichTextBox.SelectionStart = 0;
                    findCount = this.SelectFindText(findText);
                }
            }

            findCountLabel.Text = findCount == 1 ? findCount.ToString() + " entry found." : findCount.ToString() + " entries found.";
        }

        private int SelectFindText(string findText)
        {
            int cnt = 0;

            int firstPos = this.parseRichTextBox.Find(findText, 0, RichTextBoxFinds.None);

            if (firstPos >= 0)
            {
                cnt++;
                this.parseRichTextBox.SelectionStart = firstPos;
                this.parseRichTextBox.SelectionLength = findText.Length;
                this.parseRichTextBox.SelectionBackColor = Color.Red;

                for (int ix = firstPos + findText.Length; ;)
                {
                    int jx = this.parseRichTextBox.Find(findText, ix, RichTextBoxFinds.None);
                    if (jx < 0) break;

                    cnt++;
                    this.parseRichTextBox.SelectionStart = jx;
                    this.parseRichTextBox.SelectionLength = findText.Length;
                    this.parseRichTextBox.SelectionBackColor = Color.Red;

                    ix = jx + findText.Length;
                }

                this.parseRichTextBox.SelectionStart = firstPos;
                this.parseRichTextBox.SelectionLength = 0;
                this.parseRichTextBox.Focus();
                this.parseRichTextBox.ScrollToCaret();
            }

            return cnt;
        }
    }
}
