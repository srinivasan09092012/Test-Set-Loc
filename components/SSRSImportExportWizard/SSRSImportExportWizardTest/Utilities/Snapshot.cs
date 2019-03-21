using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SSRSImportExportWizardTest.Utilities
{
    /// <summary>
    /// Generate snapshots for unit testing grafical user interface.
    /// </summary>
    public class Snapshot
    {
        private Control _control;
        private Bitmap _bitmapTemp;
        private Bitmap _bitmapTest;
        private string _testPath;
        private string _tempPath;

        /// <summary>
        /// Take a bitmap snapshot of the control saves a temporary .bmp file to disk.
        /// Reads both test and temp files to bitmap for comparison.
        /// </summary>
        /// <param name="control">The control being tested.</param>
        public Snapshot(Control control)
        {
            _control = control;
            string path = Helpers.GetPath(_control);
            _testPath = path + ".png";
            _tempPath = path + "Temp.png";
            DrawBitmap();
            WriteTempFile();
            ReadFromFile();
        }
        /// <summary>
        /// Checks that byte array hash's are equal. 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns>True if the control matches the bitmap.</returns>
        /// <remarks>There may be faster ways to compare th bitmaps.</remarks>
        public bool Matches()
        {
            if (_control.Name == "")
            {
                //Assert.Inconclusive("Child control has empty name.", _control);
                WriteSnapshotToFile();
            }
            else if (_bitmapTest == null)
            {
                WriteSnapshotToFile();
            }
            //Test to see if we have the same size of image
            else if (_bitmapTemp.Size != _bitmapTest.Size)
            {
                return UserValidatedImages();
            }
            else
            {
                //Convert each image to a byte array
                ImageConverter ic = new ImageConverter();
                byte[] btImage1 = new byte[1];
                btImage1 = (byte[])ic.ConvertTo(_bitmapTemp, btImage1.GetType());
                byte[] btImage2 = new byte[1];
                btImage2 = (byte[])ic.ConvertTo(_bitmapTest, btImage2.GetType());

                //Compute a hash for each image
                SHA256Managed shaM = new SHA256Managed();
                byte[] hash1 = shaM.ComputeHash(btImage1);
                byte[] hash2 = shaM.ComputeHash(btImage2);

                //Compare the hash values
                for (int i = 0; i < hash1.Length && i < hash2.Length; i++)
                {
                    if (hash1[i] != hash2[i])
                    {
                        return UserValidatedImages();
                    }
                }
            }
            _bitmapTemp.Dispose();
            File.Delete(_tempPath);
            return true;
        }

        /// <summary>
        /// Draws the control to a bitmap.
        /// </summary>
        /// <param name="control">The control to draw.</param>
        /// <returns>An image with the control drawn onto it.</returns>
        /// <remarks>Internally uses WM_PRINT to draw the control</remarks>
        internal void DrawBitmap()
        {
            if ( _control.Width < 1 || _control.Height < 1 )
            {
                _bitmapTemp = new Bitmap(1,1);
            } else
            {
                _bitmapTemp = new Bitmap(_control.Width, _control.Height);
                _control.DrawToBitmap(_bitmapTemp, new Rectangle(0, 0, _control.Width, _control.Height));

            }
        }

        /// <summary>
        /// Opens a dialog with user asking to validate an image.
        /// </summary>
        /// <param name="bitmap">The bitmap image.</param>
        /// <returns>True if they validate the image.</returns>
        public bool UserValidatedImages()
        {
            ValidateImage validateTempImage = new ValidateImage(_bitmapTemp.Size, _control.Name);
            PictureBox pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = _bitmapTemp
            };
            validateTempImage.Controls.Add(pictureBox);
            validateTempImage.ShowDialog();

            if (validateTempImage.IsValidated)
            {
                WriteSnapshotToFile();
            }

            return validateTempImage.IsValidated;
        }

        /// <summary>
        /// Write the image to disk. 
        /// </summary>
        internal void WriteSnapshotToFile()
        {
            if (File.Exists(_testPath))
            {
                _bitmapTest.Dispose();
                File.Delete(_testPath);
            }
            _bitmapTemp.Save(_testPath, ImageFormat.Png);
            _bitmapTemp.Dispose();
            File.Delete(_tempPath);
        }
        /// <summary>
        /// Write a temporary image to disk.
        /// </summary>
        internal void WriteTempFile()
        {
            if (File.Exists(_tempPath))
            {
                File.Delete(_tempPath);
            }
            _bitmapTemp.Save(_tempPath);
        }

        internal void ReadFromFile()
        {
            _bitmapTemp.Dispose();
            _bitmapTemp = new Bitmap(_tempPath);

            if (File.Exists(_testPath))
            {
                _bitmapTest = new Bitmap(_testPath);
            }
            else
            {
                _bitmapTest = null;
            }
        }
    }
}
