using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UXWarmUpParamBuilder
{
    public partial class EditReplacement : Form
    {
        DataGridViewRow _dataGridViewRow;
        Dictionary<string, string> _tokenKeyValue = new Dictionary<string, string>();
        string sameKey = string.Empty;
        public EditReplacement(DataGridViewRow dataGridViewRow, Dictionary<string, string> tokenKeyValue)
        {
            InitializeComponent();
            _tokenKeyValue = tokenKeyValue;
            _dataGridViewRow = dataGridViewRow;
            this.UpdateEditForm();
        }

        private void UpdateEditForm()
        {
            string key = _dataGridViewRow.Cells["TokenKey"].EditedFormattedValue.ToString();
            string value = _dataGridViewRow.Cells["TokenValue"].EditedFormattedValue.ToString();
            textBoxEditkey.Text = key;
            sameKey = key;
            textBoxEditValue.Text = value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string key = textBoxEditkey.Text;
            string value = textBoxEditValue.Text;

            if (sameKey.Equals(key))
            {
                _dataGridViewRow.Cells["TokenKey"].Value = key;
                _dataGridViewRow.Cells["TokenValue"].Value = value;
                this.Close();
            }
            else
            {
                if (!_tokenKeyValue.ContainsKey(key))
                {
                    _dataGridViewRow.Cells["TokenKey"].Value = key;
                    _dataGridViewRow.Cells["TokenValue"].Value = value;
                    this.Close();
                }
                else
                {
                    string title = "Error!!!";
                    MessageBox.Show("The key with same name already exists", title);
                }
            }
        }
    }
}
