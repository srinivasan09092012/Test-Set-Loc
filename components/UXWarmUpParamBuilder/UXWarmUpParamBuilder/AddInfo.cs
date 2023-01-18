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
    public partial class AddInfo : Form
    {
        List<string> moduleName = new List<string>();
        DataGridView _dataGridView;
        public AddInfo(DataGridView dataGridView)
        {
            InitializeComponent();
            label5.Visible = false;
            _dataGridView = dataGridView;
            this.LoadModuleName();
            comboBoxParamType.Items.Add("NoParam");
            comboBoxParamType.Items.Add("DataTypeParam");
            comboBoxParamType.Items.Add("ModelParam");
            comboBoxParamType.Items.Add("MixedParam");

            foreach (var item in moduleName)
            {
                comboBoxModuleName.Items.Add(item);
            }
        }

        private void LoadModuleName()
        {
            moduleName.Add("Administration");
            moduleName.Add("Core");
            moduleName.Add("DrugRebate");
            moduleName.Add("DrugRebatePortal");
            moduleName.Add("IdentityManagement");
            moduleName.Add("Integration");
            moduleName.Add("ManagedCare");
            moduleName.Add("ManagedCarePortal");
            moduleName.Add("MemberManagement");
            moduleName.Add("MemberPortal");
            moduleName.Add("PlanManagement");
            moduleName.Add("ProgramIntegrity");
            moduleName.Add("ProgramIntegrityCT");
            moduleName.Add("ProviderCredentialing");
            moduleName.Add("ProviderEnrollment");
            moduleName.Add("ProviderManagement");
            moduleName.Add("ProviderPortal");
            moduleName.Add("TPLCaseTracking");
            moduleName.Add("TPLPolicy");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void paramType_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (this._dataGridView.Rows.Count > 1)
            {
                string paramType = this.comboBoxParamType.Text.ToString();
                string moduleName = this.comboBoxModuleName.Text.ToString();
                string routeUrl = this.textBoxAddRoute.Text.ToString();
                string param = this.textBoxAddParam.Text.ToString();
                bool statusValue = checkBoxAddStatus.Checked;
                string paramResult = string.Concat(param.Where(c => !char.IsWhiteSpace(c)));

                int newCount = this._dataGridView.Rows.Count + 1;

                this._dataGridView.Rows.Insert(0, new object[] { newCount, moduleName, paramType, routeUrl, paramResult, UXWarmUpParamBuilder.Properties.Resources.Edit, UXWarmUpParamBuilder.Properties.Resources.Test, UXWarmUpParamBuilder.Properties.Resources.NotDone, statusValue });
                this.Close();
            }
            else
            {
                string title = "Warning!!!";
                MessageBox.Show("Browse the Module File and then add the new Rows", title);
            }

        }

        private void buttonValidAddParam_Click(object sender, EventArgs e)
        {
            string actualValue = this.textBoxAddParam.Text;
            string value = string.Empty;
            if (!string.IsNullOrEmpty(actualValue))
            {
                string result = string.Concat(actualValue.Where(c => !char.IsWhiteSpace(c)));
                try
                {
                    value = JValue.Parse(result).ToString(Formatting.Indented);

                }
                catch
                {
                    value = result;
                }
                var test = this.IsValidJson(value);
                if (test)
                {
                    label5.Text = "Valid JSON";
                    label5.ForeColor = Color.Green;
                    label5.Visible = true;
                }
                else
                {
                    label5.Text = "Invalid JSON";
                    label5.ForeColor = Color.Red;
                    label5.Visible = true;
                }
            }
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
