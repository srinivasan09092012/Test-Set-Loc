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
    public partial class AddReplacement : Form
    {
        DataGridView _dataGridView;
        public AddReplacement(DataGridView dataGridView)
        {
            _dataGridView = dataGridView;
            InitializeComponent();
        }

        private void buttonAddRep_Click(object sender, EventArgs e)
        {
            string key = textBoxRepKey.Text;
            string value = textBoxRepValue.Text;
            _dataGridView.Rows.Insert(0, new object[] { key, value, UXWarmUpParamBuilder.Properties.Resources.Edit, UXWarmUpParamBuilder.Properties.Resources.CopyNew });
            this.Close();
        }
    }
}
