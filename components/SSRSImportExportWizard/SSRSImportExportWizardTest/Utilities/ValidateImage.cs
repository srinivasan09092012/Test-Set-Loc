using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSRSImportExportWizardTest.Utilities
{
    /// <summary>
    /// Open a modal dialog window form to validate an image.
    /// </summary>
    public class ValidateImage : Form
    {
        private Button _valid;
        private Button _invalid;
        private int _width;
        private int _height;
        /// <summary>
        /// Get and set the users decision to validate the image.
        /// </summary>
        public bool IsValidated { get; set; }

        /// <summary>
        /// Open a modal dialog window form to validate an image.
        /// </summary>
        /// <param name="size">Size of the bitmap image being validated.</param>
        /// <param name="name">Name of the image being validated.</param>
        public ValidateImage(Size size, string name)
        {
            _valid = new Button();
            _invalid = new Button();
            _valid.Text = "Valid";
            _invalid.Text = "Invalid";
            Text = name + ": Is this valid?";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;
            _width = size.Width < 3 * _valid.Width ? 6 * _valid.Width : size.Width + 4 * SystemInformation.FrameBorderSize.Width;
            _height = size.Height + 2 * SystemInformation.MenuHeight + 3 * _valid.Height;
            Size = new Size(_width, _height);
            _valid.Location = new Point(Size.Width / 2 - _valid.Width,
                                       size.Height + _valid.Height);
            _invalid.Location = new Point(_valid.Left + 2 * _valid.Width,
                                       _valid.Top);

            _valid.Click += new EventHandler(_valid_Click);
            _invalid.Click += new EventHandler(_invalid_Click);
            FormClosing += Validate_FormClosing;
            HelpButton = true;
            MaximizeBox = false;
            MinimizeBox = false;
            AcceptButton = _valid;
            CancelButton = _invalid;
            Controls.Add(_valid);
            Controls.Add(_invalid);
        }
        private void _valid_Click(object sender, EventArgs e)
        {
            IsValidated = true;
            Close();
        }
        private void _invalid_Click(object sender, EventArgs e)
        {
            IsValidated = false;
        }
        private void Validate_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
