using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Transformations;
using OopFactory.X12.Validation;
using OopFactory.X12.Validation.Model;
using System;
using System.Collections.Generic;
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
        private Dictionary<string, string> x12Cache;
        private string parsedX12Text = string.Empty;

        #region Main Form Events
        public MainForm()
        {
            x12Cache = new Dictionary<string, string>();
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
            this.parsedX12Text = string.Empty;
            string sourceText = this.sourceTextBox.Text;
            if (!string.IsNullOrEmpty(sourceText))
            {
                try
                {
                    this.findCountLabel.Visible = false;

                    X12Parser parser = new X12Parser();
                    if (this.x12wsRadioButton.Checked || this.xmlcRadioButton.Checked || this.xmlncRadioButton.Checked)
                    {
                        Interchange interchange = parser.ParseMultiple(new MemoryStream(Encoding.ASCII.GetBytes(sourceText))).First();
                        if (this.x12wsRadioButton.Checked)
                        {
                            this.parsedX12Text = interchange.SerializeToX12(true);
                        }
                        else if (this.xmlcRadioButton.Checked)
                        {
                            this.parsedX12Text = interchange.Serialize(false);
                        }
                        else if (this.xmlncRadioButton.Checked)
                        {
                            this.parsedX12Text = interchange.Serialize(true);
                        }

                        this.parseRichTextBox.Text = this.parsedX12Text;
                        this.parsedWebBrowser.Visible = false;
                        this.parseRichTextBox.Visible = true;

                        this.findTextBox.Enabled = true;
                        this.findButton.Enabled = true;
                        this.findTextBox.Visible = true;
                        this.findButton.Visible = true;

                        this.findTextBox.Text = string.Empty;
                        findCountLabel.Visible = false;

                        this.parseRichTextBox.Focus();
                    }
                    else if (this.htmlRadioButton.Checked)
                    {
                        this.parseRichTextBox.Text = string.Empty;
                        this.parseRichTextBox.Visible = false;

                        this.findTextBox.Text = string.Empty;
                        this.findTextBox.Visible = false;
                        this.findButton.Visible = false;
                        findCountLabel.Visible = false;

                        var service = new X12HtmlTransformationService(new X12EdiParsingService(false, parser));
                        this.parsedX12Text = service.Transform(sourceText);
                        this.parsedWebBrowser.DocumentText = this.parsedX12Text;
                        this.parsedWebBrowser.Visible = true;

                        this.parsedWebBrowser.Focus();
                    }
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

            findCountLabel.Text = findCount == 1 ? findCount.ToString() + " match found." : findCount.ToString() + " matches found.";
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

        private void ValidateButton_Click(object sender, EventArgs args)
        {
            validateRichTextBox.Text = string.Empty;
            string sourceText = this.sourceTextBox.Text;
            if (!string.IsNullOrEmpty(sourceText))
            {
                try
                {
                    StringBuilder validationText = new StringBuilder();
                    X12Parser parser = new X12Parser();
                    string errorSegments = string.Empty;

                    using (MemoryStream ms = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(sourceText)))
                    {
                        var service = new X12AcknowledgmentService();
                        var responses = service.AcknowledgeTransactions(ms);

                        if (responses.Count > 0)
                        {
                            responses.ForEach(fgr =>
                            {
                                validationText.AppendLine("Begin Functional Group Response");

                                validationText.AppendLine(string.Format("{0}Acknowledgment Code={1}", Tab(1), fgr.AcknowledgmentCode.ToString()));
                                validationText.AppendLine(string.Format("{0}Functional Id Code={1}", Tab(1), fgr.FunctionalIdCode));
                                validationText.AppendLine(string.Format("{0}Group Control Number={1}", Tab(1), fgr.GroupControlNumber));
                                validationText.AppendLine(string.Format("{0}Sender Id={1}", Tab(1), fgr.SenderId));
                                validationText.AppendLine(string.Format("{0}Sender Id Qualifier={1}", Tab(1), fgr.SenderIdQualifier));
                                validationText.AppendLine(string.Format("{0}Version Identifier Code={1}", Tab(1), fgr.VersionIdentifierCode));

                                if (fgr.SyntaxErrorCodes.Count > 0)
                                {
                                    StringBuilder errorCodes = new StringBuilder();
                                    fgr.SyntaxErrorCodes.ForEach(se =>
                                    {
                                        errorCodes.Append(string.Format("{0}{1}", se, ", "));
                                    });
                                    errorCodes.AppendLine(string.Format("{0}Response Syntax Error Codes={1}", Tab(1), errorCodes.ToString().TrimEnd().TrimEnd(',')));
                                }

                                if (fgr.TransactionSetResponses.Count > 0)
                                {
                                    int tranSetResponseIdx = 0;
                                    fgr.TransactionSetResponses.ForEach(tsr =>
                                    {
                                        validationText.AppendLine(string.Format("{0}Begin Transaction Set Response {1}", Tab(1), tranSetResponseIdx.ToString()));
                                        validationText.AppendLine(string.Format("{0}Acknowledgment Code={1}", Tab(2), tsr.AcknowledgmentCode.ToString()));
                                        validationText.AppendLine(string.Format("{0}Transaction Set Control Number={1}", Tab(2), tsr.TransactionSetControlNumber));
                                        validationText.AppendLine(string.Format("{0}Transaction Set Identifier Code={1}", Tab(2), tsr.TransactionSetIdentifierCode));
                                        validationText.AppendLine(string.Format("{0}Implementation Convention Reference={1}", Tab(2), tsr.ImplementationConventionReference));
                                        if (tsr.SegmentErrors.Count > 0)
                                        {
                                            validationText.AppendLine(string.Format("{0}Begin Segment Errors", Tab(2)));
                                            tsr.SegmentErrors.ForEach(s =>
                                            {
                                                validationText.AppendLine(string.Format("{0}{1}", Tab(3), s.ToString()));

                                                if (s.ElementNotes.Count > 0)
                                                {
                                                    validationText.AppendLine(string.Format("{0}Element Notes", Tab(4)));
                                                    s.ElementNotes.ForEach(e =>
                                                    {
                                                        if (!string.IsNullOrEmpty(e.CopyOfBadElement))
                                                        {
                                                            validationText.AppendLine(string.Format("{0}Copy Of Bad Element={1}", Tab(5), e.CopyOfBadElement));
                                                        }
                                                        if (!string.IsNullOrEmpty(e.DataElementReferenceNumber))
                                                        {
                                                            validationText.AppendLine(string.Format("{0}Data Element Reference Number={1}", Tab(5), e.DataElementReferenceNumber));
                                                        }
                                                        if (e.PositionInSegment != null)
                                                        {
                                                            validationText.Append(this.FormatPositionInSegment(e.PositionInSegment, 5));
                                                        }

                                                        if (e.ContextErrors.Count > 0)
                                                        {
                                                            validationText.AppendLine(string.Format("{0}Context Errors", Tab(5)));
                                                            e.ContextErrors.ForEach(ce =>
                                                            {
                                                                validationText.Append(this.FormatContextErrors(ce, 6));
                                                            });
                                                        }

                                                        if (!string.IsNullOrEmpty(e.SyntaxErrorCode))
                                                        {
                                                            validationText.AppendLine(string.Format("{0}Syntax Error Code={1}", Tab(5), e.SyntaxErrorCode));
                                                        }

                                                    });
                                                }
                                                if (s.ContextErrors.Count > 0)
                                                {
                                                    validationText.AppendLine(string.Format("{0}Context Errors", Tab(4)));
                                                    s.ContextErrors.ForEach(ce =>
                                                    {
                                                        validationText.Append(this.FormatContextErrors(ce, 6));
                                                    });
                                                }
                                            });

                                            validationText.AppendLine(string.Format("{0}End Segment Errors", Tab(2)));
                                        }

                                        if (tsr.SyntaxErrorCodes.Count > 0)
                                        {
                                            StringBuilder errorCodes = new StringBuilder();
                                            tsr.SyntaxErrorCodes.ForEach(sec =>
                                            {
                                                errorCodes.Append(string.Format("{0}{1}", sec.ToString(), ", "));
                                            });

                                            validationText.AppendLine(string.Format("{0}Syntax Error Codes={1}", Tab(2), errorCodes.ToString().TrimEnd().TrimEnd(',')));
                                        }

                                        validationText.AppendLine(string.Format("{0}End Transaction Set Response {1}", Tab(1), tranSetResponseIdx.ToString()));
                                        tranSetResponseIdx++;
                                    });
                                }

                                validationText.AppendLine("End Functional Group Response");
                            });
                        }
                    }

                    if (validationText.Length > 0)
                    {
                        this.validateRichTextBox.Text = validationText.ToString();
                        this.validateRichTextBox.Focus();
                    }
                    else
                    {
                        this.validateRichTextBox.Text = "No validation errors found.";
                        this.validateRichTextBox.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "X12 Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string FormatContextErrors(ContextError ce, int tabLevel)
        {
            StringBuilder ceText = new StringBuilder();

            if (!string.IsNullOrEmpty(ce.IdentificationReference))
            {
                ceText.AppendLine(string.Format("{0}Identification Reference={1}", Tab(tabLevel), ce.IdentificationReference));
            }
            if (!string.IsNullOrEmpty(ce.LoopIdentifierCode))
            {
                ceText.AppendLine(string.Format("{0}Loop Identifier Code={1}", Tab(tabLevel), ce.LoopIdentifierCode));
            }
            if (ce.PositionInSegment != null)
            {
                ceText.Append(this.FormatPositionInSegment(ce.PositionInSegment, tabLevel));
            }
            if (!string.IsNullOrEmpty(ce.ReferenceInSegment))
            {
                ceText.AppendLine(string.Format("{0}Reference In Segment={1}", Tab(tabLevel), ce.ReferenceInSegment));
            }
            if (!string.IsNullOrEmpty(ce.SegmentIdCode))
            {
                ceText.AppendLine(string.Format("{0}Segment Id Code={1}", Tab(tabLevel), ce.SegmentIdCode));
            }
            if (ce.SegmentPositionInTransactionSet != null)
            {
                ceText.AppendLine(string.Format("{0}Segment Position In Transaction Set={1}", Tab(tabLevel), ce.SegmentPositionInTransactionSet));
            }

            return ceText.ToString();
        }

        private string FormatPositionInSegment(PositionInSegment positionInSegment, int tabLevel)
        {
            StringBuilder pisText = new StringBuilder();
            if (positionInSegment.ComponentDataElementPositionInComposite != null)
            {
                pisText.AppendLine(string.Format("{0}Component Data Element Position In Composite={1}", Tab(tabLevel), positionInSegment.ComponentDataElementPositionInComposite.ToString()));
            }
            if (positionInSegment.ElementPositionInSegment != null)
            {
                pisText.AppendLine(string.Format("{0}Element Position In Segment={1}", Tab(tabLevel), positionInSegment.ElementPositionInSegment.ToString()));
            }
            if (positionInSegment.RepeatingDataElementPosition != null)
            {
                pisText.AppendLine(string.Format("{0}Repeating Data Element Position={1}", Tab(tabLevel), positionInSegment.RepeatingDataElementPosition.ToString()));
            }

            return pisText.ToString();
        }

        private string Tab(int numberOfTabs)
        {
            string tab = string.Empty;
            string key = "t" + numberOfTabs.ToString();

            if (x12Cache.ContainsKey(key))
            {
                tab = x12Cache[key];
            }
            else
            {
                for (int i = 0; i < numberOfTabs; i++)
                {
                    tab += "\t";
                }
                x12Cache.Add(key, tab);
            }

            return tab;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.parseRichTextBox.Clear();
            this.validateRichTextBox.Clear();
            this.findTextBox.Clear();
            this.findCountLabel.Visible = false;
            this.parsedX12Text = string.Empty;
            this.sourceTextBox.Clear();
            this.validateRichTextBox.Clear();
            this.parsedWebBrowser.DocumentText = string.Empty;
            this.parsedWebBrowser.Visible = false;
        }
    }
}
