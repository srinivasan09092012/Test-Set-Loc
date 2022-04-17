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
        public EditReplacement(DataGridViewRow dataGridViewRow)
        {
            InitializeComponent();
            _dataGridViewRow = dataGridViewRow;
            this.UpdateEditForm();
        }

        private void UpdateEditForm()
        {
            textBoxEditkey.Text.Clone();
            string key = _dataGridViewRow.Cells["TokenKey"].EditedFormattedValue.ToString();
            string value = _dataGridViewRow.Cells["TokenValue"].EditedFormattedValue.ToString();
            textBoxEditkey.Text = key;
            textBoxEditValue.Text = value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string key = textBoxEditkey.Text;
            string value = textBoxEditValue.Text;
            _dataGridViewRow.Cells["TokenKey"].Value = key;
            _dataGridViewRow.Cells["TokenValue"].Value = value;
            this.Close();
        }
    }
}
