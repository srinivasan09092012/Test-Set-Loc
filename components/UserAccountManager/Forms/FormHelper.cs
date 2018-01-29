using System;
using System.Windows.Forms;

namespace UserAccountManager.Forms
{
    public static class FormHelper
    {
        public static DialogResult DisplayMessage(string msg, MessageBoxIcon type)
        {
            DialogResult result = new DialogResult();

            switch (type)
            {
                case MessageBoxIcon.Error:
                    MessageBox.Show(msg, "Error", MessageBoxButtons.OK, type);
                    break;

                case MessageBoxIcon.Warning:
                    MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, type);
                    break;

                case MessageBoxIcon.Question:
                    result = MessageBox.Show(msg, "Question", MessageBoxButtons.YesNo, type);
                    break;

                default:
                    MessageBox.Show(msg, "Information", MessageBoxButtons.OK, type);
                    break;
            }

            return result;
        }

        public static void DisplayMessage(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
