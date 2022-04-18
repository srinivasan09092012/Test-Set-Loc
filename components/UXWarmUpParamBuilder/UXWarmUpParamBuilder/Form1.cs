using FileHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UXWarmUpParamBuilder
{
    public partial class Form1 : Form
    {
        BindingList<WarmUpParam> mainList;
        string paramValue = string.Empty;
        DataGridViewRow currentrow;
        Form prompt = new Form();
        Dictionary<string, string> envDomain = new Dictionary<string, string>();
        Dictionary<string, string> replaceTokenValue = new Dictionary<string, string>();
        public Form1()
        {
            InitializeComponent();
            mainList = new BindingList<WarmUpParam>();
            this.LoadEnvironment();
        }

        private void LoadEnvironment()
        {
            comboBoxEnv.Items.Add("Dev");
            comboBoxEnv.Items.Add("Test");
            comboBoxEnv.Items.Add("Local");
            comboBoxEnv.SelectedIndex = 0;
            envDomain.Add("Dev", @"https://tenant1foraks.dev.mapshc.com/");
            envDomain.Add("Test", @"https://tenant1foraks.test.mapshc.com/");
            envDomain.Add("Local", @"https://localhost.dev.mapshc.com/");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewModuleParam_buttonCol(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex != -1)
            {
                var cell = dataGridViewModuleParam[e.ColumnIndex, e.RowIndex];
                DataGridViewRow currentrow = cell.DataGridView.CurrentRow;
                var paramvalue = cell.DataGridView.CurrentRow.Cells["JsonParameters"].EditedFormattedValue;
                EditParam param = new EditParam(paramvalue.ToString(), currentrow);
                param.Show();
            }

            if (e.ColumnIndex == 6 && e.RowIndex != -1)
            {
                object selectedValue = comboBoxEnv.SelectedItem;
                if (selectedValue != null)
                {
                    string domain = envDomain[Convert.ToString(selectedValue)];
                    var cell = dataGridViewModuleParam[e.ColumnIndex, e.RowIndex];
                    DataGridViewRow row = cell.DataGridView.CurrentRow;
                    if (this.VerifyAction(row, domain))
                    {
                        row.Cells["Result"].Value = UXWarmUpParamBuilder.Properties.Resources.Accept;
                    }
                    else
                    {
                        row.Cells["Result"].Value = UXWarmUpParamBuilder.Properties.Resources.Reject;
                    }
                }
                else
                {
                    MessageBox.Show("Select the Environment",
                    "window title",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                }
            }
        }

        private void dataGridViewToken_buttonCol(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex != -1)
            {
                var cell = dataGridViewToken[e.ColumnIndex, e.RowIndex];
                DataGridViewRow currentrow = cell.DataGridView.CurrentRow;
                EditReplacement edit = new EditReplacement(currentrow);
                edit.Show();
            }

            if (e.ColumnIndex == 3 && e.RowIndex != -1)
            {
                var cell = dataGridViewToken[e.ColumnIndex, e.RowIndex];
                DataGridViewRow currentrow = cell.DataGridView.CurrentRow;
                string key = currentrow.Cells["TokenKey"].EditedFormattedValue.ToString();
                Clipboard.Clear();
                Clipboard.SetText(key);
            }
        }

        private bool VerifyAction(DataGridViewRow row, string domain)
        {
            WarmUpTesting warmUpTesting = new WarmUpTesting();
            WarmUpPayloadModel WarmUpPayloadModel = new WarmUpPayloadModel();
            WarmUpPayloadModel.ModuleName = row.Cells["ModuleName"].Value.ToString();
            WarmUpPayloadModel.Param = this.ReplaceTokenValue( row.Cells["JsonParameters"].Value.ToString());
            WarmUpPayloadModel.ParamType = row.Cells["ParamType"].Value.ToString();
            WarmUpPayloadModel.RouteUrl = row.Cells["RouteUrl"].Value.ToString();
            if (WarmUpPayloadModel.ParamType.Equals("ModelParam"))
            {
                return warmUpTesting.RunWarmUpForPost(WarmUpPayloadModel, domain);
            }
            else
            {
                return warmUpTesting.RunWarmUp(WarmUpPayloadModel, domain);
            }
        }

        private void LoadActionGrid(BindingList<WarmUpParam> data)
        {
            dataGridViewModuleParam.Rows.Clear();
            dataGridViewModuleParam.Refresh();
            foreach (var item in data)
            {
                dataGridViewModuleParam.Rows.Add(new object[] {item.Id, item.ModuleName, item.ParamType, item.RouteUrl, item.Param, UXWarmUpParamBuilder.Properties.Resources.Edit, UXWarmUpParamBuilder.Properties.Resources.Test, UXWarmUpParamBuilder.Properties.Resources.NotDone, Convert.ToBoolean(item.Status) });
            }
        }

        private void LoadToeknGrid(BindingList<TokenizedValue> data)
        {
            dataGridViewToken.Rows.Clear();
            dataGridViewToken.Refresh();
            dataGridViewToken.Columns["EditToken"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewToken.Columns["Copy"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (var item in data)
            {
                dataGridViewToken.Rows.Add(new object[] { item.TokenKey, item.TokenValue, UXWarmUpParamBuilder.Properties.Resources.Edit, UXWarmUpParamBuilder.Properties.Resources.CopyNew });
            }
        }

        public BindingList<WarmUpParam> LoadActionCsv(string csvFile)
        {
            BindingList<WarmUpParam> result = new BindingList<WarmUpParam>();
            try
            {
                var query = from s in File.ReadAllLines(csvFile)
                            let data = s.Split('|')
                            select new WarmUpParam
                            {
                                Id = Convert.ToInt32(data[0]),
                                ModuleName = data[1],
                                ParamRequired = data[2],
                                ControllerName = data[3],
                                RouteUrl = data[4],
                                Param = data[5],
                                ParamType = data[6],
                                Status = data[7]
                            };

                result = this.ToBindingList(query.OrderBy(s=>s.RouteUrl));
                mainList = result;
            }
            catch
            {
                this.DisplayErrorMessage("File data is Incorrect");
            }

            return result;
        }

        public BindingList<TokenizedValue> LoadToeknCsv(string csvFile)
        {
            BindingList<TokenizedValue> result = new BindingList<TokenizedValue>();
            try
            {
                var query = from s in File.ReadAllLines(csvFile)
                            let data = s.Split('|')
                            select new TokenizedValue
                            {
                                TokenKey = data[0],
                                TokenValue = data[1]
                            };

                result = this.ToBindingList(query.OrderBy(s=>s.TokenKey));
            }
            catch
            {
                this.DisplayErrorMessage("File data is Incorrect");
            }

            return result;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //OpenFileDialog op = new OpenFileDialog();
            //op.ShowDialog();
            //textBox1.Text = op.FileName;
            //string extension = Path.GetExtension(textBox1.Text);
            //if (extension.Equals(@".csv"))
            //{
            //    this.LoadGrid(this.LoadCsv(textBox1.Text));
            //}
            //else
            //{
            //    this.DisplayErrorMessage("Invalid File Extension");
            //}
        }

        public string ShowDialog(string text, string caption, string paramText)
        {
            prompt.Width = 500;
            prompt.Height = 500;
            prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
            prompt.Text = caption;
            prompt.StartPosition = FormStartPosition.CenterScreen;
            prompt.Controls.Clear();
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Name = "paramText", Left = 50, Top = 50, Width = 400, Multiline = true, Height = 300, Text = paramText };
            Button confirmation = new Button() { Text = "Save", Left = 350, Width = 100, Top = 390, DialogResult = DialogResult.OK };
            // confirmation.Click += (sender, e) => { prompt.Close(); };
            confirmation.Click += new System.EventHandler(this.paramSave_Click);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? this.paramValue = textBox.Text : this.paramValue = "";
        }

        private void paramSave_Click(object sender, System.EventArgs e)
        {
            string s = ((TextBox)prompt.Controls["paramText"]).Text;
            currentrow.Cells["Param"].Value = s;
            ((TextBox)prompt.Controls["paramText"]).Clear();
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewModuleParam.Rows.Count > 1)
            {
                AddInfo info = new AddInfo(this.dataGridViewModuleParam);
                info.Show();
            }
            else
            {
                string title = "Warning!!!";
                MessageBox.Show("Browse the Module File and then add the new Rows", title);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridViewModuleParam.Rows.Count > 0)
            {
                var engine = new FileHelperEngine<WarmUpParam>();
                var WarmUpParamList = new List<WarmUpParam>();

                foreach (DataGridViewRow row in dataGridViewModuleParam.Rows)
                {
                    WarmUpParam warmUpParam = new WarmUpParam();
                    warmUpParam.Id = Convert.ToInt32(row.Cells["Id"].EditedFormattedValue.ToString());
                    warmUpParam.ModuleName = row.Cells["ModuleName"].EditedFormattedValue.ToString();
                    warmUpParam.RouteUrl = row.Cells["RouteUrl"].EditedFormattedValue.ToString();
                    warmUpParam.Param = row.Cells["JsonParameters"].EditedFormattedValue.ToString();
                    warmUpParam.Status = row.Cells["Status"].EditedFormattedValue.ToString();
                    warmUpParam.ParamType = row.Cells["ParamType"].EditedFormattedValue.ToString();
                    WarmUpParamList.Add(warmUpParam);
                }

                SaveFileDialog saveFileDialogActions = new SaveFileDialog();
                saveFileDialogActions.Filter = ".csv|*.csv";
                saveFileDialogActions.Title = "Save File";
                saveFileDialogActions.ShowDialog();

                if (saveFileDialogActions.FileName != "")
                {
                    engine.WriteFile(saveFileDialogActions.FileName, WarmUpParamList);
                    List<TokenizedValue> tokenizedValueList = this.ConvertTokenDataToModel();
                    var engineForToken = new FileHelperEngine<TokenizedValue>();
                    string tokenFileName = saveFileDialogActions.FileName.Remove(saveFileDialogActions.FileName.Length - 4) + "TokenizedData";
                    tokenFileName = tokenFileName + ".csv";
                    engineForToken.WriteFile(tokenFileName, tokenizedValueList);
                    string message = "File Saved Successfully";
                    string title = "Save";
                    MessageBox.Show(message, title);
                }
            }
            else
            {
                this.DisplayErrorMessage("No Data to Export");
            }

        }

        private void DisplayErrorMessage(string message)
        {
            string title = "Error!!!";
            MessageBox.Show(message, title);
        }

        public BindingList<T> ToBindingList<T>(IEnumerable<T> data)
        {
            BindingList<T> result = null;
            if (data != null)
            {
                result = new BindingList<T>();
                foreach (T item in data)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        private List<WarmUpParam> GetData()
        {
            List<WarmUpParam> warmUpParamList = new List<WarmUpParam>();
            foreach (DataGridViewRow row in dataGridViewModuleParam.Rows)
            {
                WarmUpParam warmUpParam = new WarmUpParam();
                warmUpParam.ModuleName = row.Cells["ModuleName"].EditedFormattedValue.ToString();
                warmUpParam.ParamRequired = row.Cells["ParamRequired"].EditedFormattedValue.ToString();
                warmUpParam.ControllerName = row.Cells["ControllerName"].EditedFormattedValue.ToString();
                warmUpParam.RouteUrl = row.Cells["RouteUrl"].EditedFormattedValue.ToString();
                warmUpParam.Param = row.Cells["Param"].EditedFormattedValue.ToString();
                warmUpParam.Status = row.Cells["StatusButton"].EditedFormattedValue.ToString();
                warmUpParamList.Add(warmUpParam);
            }
            return warmUpParamList;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnActionBrw_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.ShowDialog();
            textBoxActionPath.Text = op.FileName;
            // textBox1.Text = op.FileName;
            string extension = Path.GetExtension(textBoxActionPath.Text);
            if (!string.IsNullOrEmpty(extension))
            {
                if (extension.Equals(@".csv"))
                {
                    this.LoadBothGrid(textBoxActionPath.Text);
                }
                else
                {
                    this.DisplayErrorMessage("Invalid File Extension");
                }
            }
        }

        private void LoadBothGrid(string path)
        {
            string fullTokenPath = path.Split('.').First();
            fullTokenPath = fullTokenPath + "TokenizedData" + ".csv";
            bool existStatus = File.Exists(fullTokenPath);
            if (existStatus)
            {
                textBoxRepBrw.Text = fullTokenPath;
                this.LoadToeknGrid(this.LoadToeknCsv(fullTokenPath));
                this.LoadActionGrid(this.LoadActionCsv(path));
            }
            else
            {
                this.DisplayErrorMessage("Tokenized Data File Not Found");
            }

        }

        private void buttonAddToken_Click(object sender, EventArgs e)
        {
            AddReplacement add = new AddReplacement(this.dataGridViewToken);
            add.Show();
        }

        private void LoadTokenValues()
        {
            foreach (DataGridViewRow row in dataGridViewToken.Rows)
            {
                if (!replaceTokenValue.ContainsKey(row.Cells["TokenKey"].EditedFormattedValue.ToString()))
                {
                    replaceTokenValue.Add(row.Cells["TokenKey"].EditedFormattedValue.ToString(), row.Cells["TokenValue"].EditedFormattedValue.ToString());
                }
            }
        }

        private string ReplaceTokenValue(string param)
        {
            string paramResult = string.Concat(param.Where(c => !char.IsWhiteSpace(c)));
            foreach (DataGridViewRow row in dataGridViewToken.Rows)
            {
                if (!replaceTokenValue.ContainsKey(row.Cells["TokenKey"].EditedFormattedValue.ToString()))
                {
                    if (paramResult.Contains(row.Cells["TokenKey"].EditedFormattedValue.ToString()))
                    {
                        paramResult = paramResult.Replace(row.Cells["TokenKey"].EditedFormattedValue.ToString(), row.Cells["TokenValue"].EditedFormattedValue.ToString());
                    }
                    replaceTokenValue.Add(row.Cells["TokenKey"].EditedFormattedValue.ToString(), row.Cells["TokenValue"].EditedFormattedValue.ToString());
                }
                else
                {
                    if (paramResult.Contains(row.Cells["TokenKey"].EditedFormattedValue.ToString()))
                    {
                        paramResult = paramResult.Replace(row.Cells["TokenKey"].EditedFormattedValue.ToString(), row.Cells["TokenValue"].EditedFormattedValue.ToString());
                    }
                }
            }
            return paramResult;
        }

        private List<TokenizedValue> ConvertTokenDataToModel()
        {
            List<TokenizedValue> tokenizedValueList = new List<TokenizedValue>();

            foreach (DataGridViewRow row in dataGridViewToken.Rows)
            {
                TokenizedValue tokenizedValue = new TokenizedValue();
                tokenizedValue.TokenKey = row.Cells["TokenKey"].EditedFormattedValue.ToString();
                tokenizedValue.TokenValue = row.Cells["TokenValue"].EditedFormattedValue.ToString();
                tokenizedValueList.Add(tokenizedValue);
            }

            return tokenizedValueList;
        }
    }
}
