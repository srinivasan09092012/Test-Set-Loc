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
        public Form1()
        {
            InitializeComponent();
            mainList = new BindingList<WarmUpParam>();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_buttonCol(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                if (dataGridViewModuleParam[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell cell)
                {
                    if (cell.Value == null || cell.Value == cell.OwningColumn.DefaultCellStyle.NullValue)
                    {
                        cell.Value = "InActive";
                    }
                    else
                    {
                        cell.Value = cell.OwningColumn.DefaultCellStyle.NullValue;
                    }
                }
            }

            if (e.ColumnIndex == 7)
            {
                if (dataGridViewModuleParam[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell cell)
                {
                    currentrow = cell.DataGridView.CurrentRow;
                    var paramvalue = cell.DataGridView.CurrentRow.Cells["Param"].EditedFormattedValue;
                    this.ShowDialog("EditParam", "", paramvalue.ToString());
                }
            }

            if (e.ColumnIndex == 9)
            {
                if (dataGridViewModuleParam[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell cell)
                {
                    DataGridViewRow row = cell.DataGridView.CurrentRow;
                    if (this.VerifyAction(row))
                    {
                        cell.Value = "Passed";
                        cell.Style.BackColor = Color.Green;
                        cell.Style.ForeColor = Color.Green;
                    }
                    else
                    {
                        cell.Value = "Failed";
                        cell.Style.BackColor = Color.Red;
                    }
                }
            }
        }

        private bool VerifyAction(DataGridViewRow row)
        {
            WarmUpTesting warmUpTesting = new WarmUpTesting();
            WarmUpPayloadModel WarmUpPayloadModel = new WarmUpPayloadModel();
            WarmUpPayloadModel.ModuleName = row.Cells["ModuleName"].Value.ToString();
            WarmUpPayloadModel.Param = row.Cells["Param"].Value.ToString();
            WarmUpPayloadModel.ParamType = row.Cells["ParamType"].Value.ToString();
            WarmUpPayloadModel.RouteUrl = row.Cells["RouteUrl"].Value.ToString();
            if (WarmUpPayloadModel.ParamType.Equals("ModelParam"))
            {
                return warmUpTesting.RunWarmUpForPost(WarmUpPayloadModel);
            }
            else
            {
                return warmUpTesting.RunWarmUp(WarmUpPayloadModel);
            }
        }

        private void LoadGrid(BindingList<WarmUpParam> data)
        {
            dataGridViewModuleParam.Rows.Clear();
            dataGridViewModuleParam.Refresh();
           //// dataGridViewModuleParam.DataSource = data;
            foreach (var item in data)
            {
                dataGridViewModuleParam.Rows.Add( new object[] { item.ParamType, item.RouteUrl, item.Param });
            }
           //// dataGridViewModuleParam.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_buttonCol);

            //var buttonColumnEdit = new DataGridViewButtonColumn()
            //{
            //    Name = "Edit",
            //    HeaderText = "Edit",
            //    UseColumnTextForButtonValue = false,
            //    DefaultCellStyle = new DataGridViewCellStyle()
            //    {
            //        NullValue = "Edit"
            //    }
            //};
            //if (dataGridViewModuleParam.Columns["Edit"] == null)
            //{
            //    this.dataGridViewModuleParam.Columns.Add(buttonColumnEdit);
            //}

            //var buttonColumn = new DataGridViewButtonColumn()
            //{
            //    Name = "StatusButton",
            //    HeaderText = "Status",
            //    UseColumnTextForButtonValue = false,
            //    DefaultCellStyle = new DataGridViewCellStyle()
            //    {
            //        NullValue = "Active"
            //    }
            //};
            //if (dataGridViewModuleParam.Columns["StatusButton"] == null)
            //{
            //    this.dataGridViewModuleParam.Columns.Add(buttonColumn);
            //}

            //var buttonColumnTest = new DataGridViewButtonColumn()
            //{
            //    Name = "Test",
            //    HeaderText = "PerformTest",
            //    UseColumnTextForButtonValue = false,
            //    DefaultCellStyle = new DataGridViewCellStyle()
            //    {
            //        NullValue = "Test"
            //    }
            //};
            //if (dataGridViewModuleParam.Columns["Test"] == null)
            //{
            //    this.dataGridViewModuleParam.Columns.Add(buttonColumnTest);
            //}
        }

        public BindingList<WarmUpParam> LoadCsv(string csvFile)
        {
            BindingList<WarmUpParam> result = new BindingList<WarmUpParam>();
            try
            {
                var query = from s in File.ReadAllLines(csvFile)
                            let data = s.Split('|')
                            select new WarmUpParam
                            {
                                ModuleName = data[0],
                                ParamRequired = data[1],
                                ControllerName = data[2],
                                RouteUrl = data[3],
                                Param = data[4],
                                ParamType = data[5]
                            };

                result = this.ToBindingList(query);
                mainList = result;
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

        private void button1_Click(object sender, EventArgs e)
        {
            AddInfo info = new AddInfo();
            info.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var engine = new FileHelperEngine<WarmUpParam>();

            var WarmUpParamList = new List<WarmUpParam>();
            foreach (DataGridViewRow row in dataGridViewModuleParam.Rows)
            {
                if (row.Cells["StatusButton"].EditedFormattedValue.ToString().Equals("Active"))
                {
                    WarmUpParam warmUpParam = new WarmUpParam();
                    warmUpParam.ModuleName = row.Cells["ModuleName"].EditedFormattedValue.ToString();
                    warmUpParam.ParamRequired = row.Cells["ParamRequired"].EditedFormattedValue.ToString();
                    warmUpParam.ControllerName = row.Cells["ControllerName"].EditedFormattedValue.ToString();
                    warmUpParam.RouteUrl = row.Cells["RouteUrl"].EditedFormattedValue.ToString();
                    warmUpParam.Param = row.Cells["Param"].EditedFormattedValue.ToString();
                    WarmUpParamList.Add(warmUpParam);
                }
            }

            engine.WriteFile("Output.csv", WarmUpParamList);
            string message = "File Saved Successfully";
            string title = "Save";
            MessageBox.Show(message, title);
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

        private void dataGridViewModuleParam_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnActionBrw_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.ShowDialog();
            textBox1.Text = op.FileName;
            string extension = Path.GetExtension(textBox1.Text);
            if (extension.Equals(@".csv"))
            {
                this.LoadGrid(this.LoadCsv(textBox1.Text));
            }
            else
            {
                this.DisplayErrorMessage("Invalid File Extension");
            }
        }
    }
}
