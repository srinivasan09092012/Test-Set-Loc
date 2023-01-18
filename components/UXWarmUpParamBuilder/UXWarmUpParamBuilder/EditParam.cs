using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public partial class EditParam : Form
    {
        DataGridViewRow _row;
        public EditParam(string jsonValue, DataGridViewRow currentrow)
        {
            InitializeComponent();
            labelJsonMsg.Visible = false;
            string value = string.Empty;
            string result = string.Concat(jsonValue.Where(c => !char.IsWhiteSpace(c)));
            try
            {
                value = JValue.Parse(result).ToString(Formatting.Indented);

            }
            catch
            {
                value = result;
            }
            richTextBoxJson.Text = value;
            _row = currentrow;
        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            string actualValue = richTextBoxJson.Text;
            if (!string.IsNullOrEmpty(richTextBoxJson.Text))
            {
                string value = string.Empty;
                string result = string.Concat(actualValue.Where(c => !char.IsWhiteSpace(c)));
                try
                {
                    value = JValue.Parse(result).ToString(Formatting.Indented);

                }
                catch
                {
                    value = result;
                }
                richTextBoxJson.Text = value;
                var test = this.IsValidJson(value);
                if (test)
                {
                    labelJsonMsg.Text = "Valid JSON";
                    labelJsonMsg.ForeColor = Color.Green;
                    labelJsonMsg.Visible = true;
                }
                else
                {
                    labelJsonMsg.Text = "Invalid JSON";
                    labelJsonMsg.ForeColor = Color.Red;
                    labelJsonMsg.Visible = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string actualValue = richTextBoxJson.Text;
            string result = string.Concat(actualValue.Where(c => !char.IsWhiteSpace(c)));
            _row.Cells["JsonParameters"].Value = result;
            richTextBoxJson.Clear();
            this.Close();
        }

        private bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
