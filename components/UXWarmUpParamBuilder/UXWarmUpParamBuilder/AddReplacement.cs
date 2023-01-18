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
        Dictionary<string, string> _tokenKeyValue = new Dictionary<string, string>();
        public AddReplacement(DataGridView dataGridView, Dictionary<string, string> tokenKeyValue)
        {
            _dataGridView = dataGridView;
            _tokenKeyValue = tokenKeyValue;
            InitializeComponent();
        }

        private void buttonAddRep_Click(object sender, EventArgs e)
        {
            string key = textBoxRepKey.Text;
            string value = textBoxRepValue.Text;
            if (!_tokenKeyValue.ContainsKey(key))
            {
                _dataGridView.Rows.Insert(0, new object[] { key, value, UXWarmUpParamBuilder.Properties.Resources.Edit, UXWarmUpParamBuilder.Properties.Resources.CopyNew });
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
