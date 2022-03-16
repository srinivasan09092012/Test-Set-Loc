//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2022. All rights reserved.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace HPP.Maintainability.CodeMaintainabilityMonitor
{
    public partial class CodeMaintainabilityMonitor : Telerik.WinControls.UI.RadForm
    {
        private List<string> Modules;
        private List<string> Branches;

        #region Constructors
        public CodeMaintainabilityMonitor()
        {
            this.InitializeComponent();
            this.Modules = new List<string>();
            this.Branches = new List<string>();
        }
        #endregion

        public int LimitIndex
        {
            get
            {
                int limitIndex = MaintainabilityConstants.MaintainabilityThreshold;

                if (int.TryParse(textBoxIndexLimit.Text, out limitIndex))
                {
                    return limitIndex;
                }

                return limitIndex;
            }
        }

        #region Private Methods
        private async void generateButton_Click(object sender, EventArgs e)
        {
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = true;
            this.ProcessingLabel.Text = MaintainabilityConstants.Messages.Processing;
            this.ProcessingLabel.Refresh();
            var progressReport = new Progress<int>(ProgressStatus);


            DisableOrEnableButtons(false);

            await Task.Run(() =>
            {
                this.GenerateFiles(progressReport);
            });

            DisableOrEnableButtons(true);
            FilterAndSortXmlDataStep.Execute(this.radioButtonGetMethodsUnderIndex.Checked, LimitIndex);
            this.PupulateExportFiles();
            this.ProcessingLabel.Text = MaintainabilityConstants.Messages.ProcessDone;
            this.ShowDataInGridSecondTab();
        }

        private void ProgressStatus(int indexCount)
        {
            var percentage = (double)indexCount / this.checkedListBox1.CheckedItems.Count;
            percentage = percentage * 100;
            var percentageInt = (int)Math.Round(percentage, 0);
            progressBar1.Value = percentageInt;
            this.ProcessingLabel.Text = string.Format("{0} Progress: {1}%, Files Selected: {2}, Files Processed: {3}", MaintainabilityConstants.Messages.Processing, percentageInt, this.checkedListBox1.CheckedItems.Count, indexCount);
        }

        private async Task GenerateFiles(IProgress<int> progress = null)
        {
            List<string> strignList = new List<string>();
            int indexCount = 0;
            foreach (var selected in this.checkedListBox1.CheckedItems)
            {
                string outputFile = selected.ToString().Split('\\').Last();
                string[] pathStructure = selected.ToString().Split('\\');
                outputFile = outputFile.Replace(MaintainabilityConstants.HppName, "");
                outputFile = outputFile.Replace(".sln", "");
                outputFile = outputFile.Replace(".", "_");
                strignList.Add(string.Format("{0}{1}{2}{3}{4}_{5}_{6}{7}",
                    MaintainabilityConstants.MetricsExe,
                    selected,
                    MaintainabilityConstants.MetricsOut,
                    MaintainabilityConstants.Paths.OutputXmlFiles,
                    pathStructure[4],
                    pathStructure[5],
                    outputFile,
                    MaintainabilityConstants.MetricsExtention));
            }

            foreach (string selectedModules in strignList)
            {
                this.RunCommandLine(selectedModules);
                indexCount = this.CalculateProgress(progress, indexCount);
            }

            this.ProcessingLabel.Text = MaintainabilityConstants.Messages.ProcessDone;
            this.ProcessingLabel.Refresh();
        }

        private int CalculateProgress(IProgress<int> progress, int indexCount)
        {
            if (progress != null)
            {
                indexCount++;
                progress.Report(indexCount);
            }

            return indexCount;
        }

        private void RunCommandLine(string strignList)
        {           
            Process p = new Process();
            p.StartInfo.FileName = MaintainabilityConstants.CommandLine;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.Start();
            p.StandardInput.WriteLine(MaintainabilityConstants.Paths.MaintainabilityMetricsCalculator);
            p.StandardInput.WriteLine(strignList);
            p.StandardInput.Flush();
            p.StandardInput.Close();
            p.WaitForExit();
            p.Close();
        }

        private void DisableOrEnableButtons(bool toogle)
        {
            buttonExit.Enabled = toogle;
            buttonExport.Enabled = toogle;
            buttonLoad.Enabled = toogle;
            buttonSelectAll.Enabled = toogle;
            buttonUnselectAll.Enabled = toogle;
            generateButton.Enabled = toogle;
        }

        public void ShowDataInGridSecondTab()
        {
            if(this.tabControl1.Controls.Count == 1)
            {
                this.tabControl1.Controls.Add(this.tabPage2);
                tabControl1.SelectedIndex = 1;
            }
        }

        private void PupulateExportFiles()
        {
            comboBox1.Items.Clear();
            string[] files = Directory.GetFiles(string.Format(MaintainabilityConstants.Paths.OutputFolder, string.Empty));
            List<string> names = new List<string>();
            string name;
            foreach (string file in files)
            {
                name = Path.GetFileName(file);
                comboBox1.Items.Add(name);
            }
        }

        private async void buttonLoad_Click(object sender, EventArgs e)
        {
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = false;
            this.checkedListBox1.Items.Clear();
            this.generateButton.Enabled = false;
            string[] filePaths = await this.GetFileDirectory();
            this.PopulateCheckedListBox(filePaths);
            this.buttonSelectAll.Enabled = true;
            this.buttonUnselectAll.Enabled = true;
            this.saveToolStripMenuItem.Enabled = false;
            this.ProcessingLabel.Text = MaintainabilityConstants.Messages.EmptyMessage;
        }

        private void PopulateCheckedListBox(string[] filePaths)
        {
            foreach (string file in filePaths)
            {
                this.checkedListBox1.Items.Add(file, false);
            }
        }

        private async Task<string[]> GetFileDirectory()
        {
            if (this.comboBoxModule.SelectedIndex == -1 && this.comboBoxBranch.SelectedIndex == -1)
            {
                return await Task.Run(() => Directory.GetFiles(MaintainabilityConstants.Paths.SourceFolder, MaintainabilityConstants.SoluionExention, SearchOption.AllDirectories));
            }
            else if (this.comboBoxBranch.SelectedIndex == -1)
            {
                string path = string.Format("{0}{1}\\", MaintainabilityConstants.Paths.SourceFolder, this.comboBoxModule.SelectedItem.ToString());
                return await Task.Run(() => Directory.GetFiles(path, MaintainabilityConstants.SoluionExention,SearchOption.AllDirectories));
            }
            else
            {
                string path = string.Format("{0}{1}\\{2}\\", MaintainabilityConstants.Paths.SourceFolder, this.comboBoxModule.SelectedItem, this.comboBoxBranch.SelectedItem);
                return await Task.Run(() =>  Directory.GetFiles(path, MaintainabilityConstants.SoluionExention,SearchOption.AllDirectories));
            }
        }

        private string[] GetModuleSolution()
        {
            if (this.comboBoxModule.SelectedIndex == -1 && this.comboBoxBranch.SelectedIndex == -1)
            {
                return Directory.GetFiles(MaintainabilityConstants.Paths.SourceFolder, MaintainabilityConstants.SoluionExention, SearchOption.AllDirectories);
            }
            else if(this.comboBoxBranch.SelectedIndex == -1)
            {
                return Directory.GetFiles(string.Format("{0}{1}\\",
                   MaintainabilityConstants.Paths.SourceFolder,
                   this.comboBoxModule.SelectedItem),
                   MaintainabilityConstants.SoluionExention,
                   SearchOption.AllDirectories);
            }
            else
            {
                return Directory.GetFiles(string.Format("{0}{1}\\{2}\\",
                   MaintainabilityConstants.Paths.SourceFolder,
                   this.comboBoxModule.SelectedItem,
                   this.comboBoxBranch.SelectedItem),
                   MaintainabilityConstants.SoluionExention,
                   SearchOption.AllDirectories);
            }
        }

        private void CodeMaintainabilityMonitor_Load(object sender, EventArgs e)
        {
            progressBar1.Maximum = 100;
            var directories = Directory.GetDirectories(MaintainabilityConstants.Paths.SourceFolder);
            foreach (var file in directories)
            {
                DirectoryInfo dir_info = new DirectoryInfo(file);
                string directory = dir_info.Name;
                if (!this.Modules.Contains(directory))
                {
                    this.Modules.Add(directory);
                }
            }

            this.comboBoxModule.Items.AddRange(this.Modules.ToArray());

            if (File.Exists(MaintainabilityConstants.Paths.SettingsPreferenceFile))
            {
                if (new FileInfo(MaintainabilityConstants.Paths.SettingsPreferenceFile).Length <= 0)
                {
                    this.deleteToolStripMenuItem.Enabled = false;
                    this.loadToolStripMenuItem.Enabled = false;
                }

                if (this.comboBox1.Items.Count <= 0)
                {
                    this.saveToolStripMenuItem.Enabled = false;
                }
            }
            else
            {
                this.deleteToolStripMenuItem.Enabled = false;
                this.loadToolStripMenuItem.Enabled = false;
                this.saveToolStripMenuItem.Enabled = false;
            }
        }

        private void comboBoxModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.generateButton.Enabled = false;
            this.buttonSelectAll.Enabled = false;
            this.buttonUnselectAll.Enabled = false;
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = false;
            this.ProcessingLabel.Text = MaintainabilityConstants.Messages.EmptyMessage;
            this.comboBoxBranch.Items.Clear();
            this.comboBoxBranch.Refresh();
            this.Branches = new List<string>();
            this.comboBoxBranch.Items.AddRange(this.Branches.ToArray());
            this.checkedListBox1.Items.Clear();
            var directories = Directory.GetDirectories(string.Format("{0}{1}{2}", MaintainabilityConstants.Paths.SourceFolder, this.comboBoxModule.SelectedItem.ToString(), MaintainabilityConstants.Paths.DoulbeBackslash));
            foreach (var file in directories)
            {
                DirectoryInfo dir_info = new DirectoryInfo(file);
                string directory = dir_info.Name;
                if (!Modules.Contains(directory))
                {
                    this.Branches.Add(directory);
                }
            }

            this.comboBoxBranch.Items.AddRange(Branches.ToArray());
        }

        private void comboBoxBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = false;
            this.generateButton.Enabled = false;
            this.buttonSelectAll.Enabled = false;
            this.buttonUnselectAll.Enabled = false;
            this.ProcessingLabel.Text = MaintainabilityConstants.Messages.EmptyMessage;
            this.checkedListBox1.Items.Clear();
        }

        private void checkedListBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.ValidateForSelectedItems();
        }

        private void ValidateForSelectedItems()
        {
            if (this.checkedListBox1.SelectedItems.Count > 0)
            {
                int itemsCheckedCount = 0;
                for (int i = 0; i < this.checkedListBox1.CheckedItems.Count; i++)
                {
                    itemsCheckedCount++;
                }

                if (itemsCheckedCount > 0)
                {
                    this.generateButton.Enabled = true;
                    this.saveToolStripMenuItem.Enabled = true;
                }
                else
                {
                    this.generateButton.Enabled = false;
                    this.saveToolStripMenuItem.Enabled = false;
                }

            }

            this.ProcessingLabel.Text = MaintainabilityConstants.Messages.EmptyMessage;
        }
        #endregion

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkedListBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                this.ValidateForSelectedItems();
            }
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = false;
            this.ProcessingLabel.Text = MaintainabilityConstants.Messages.EmptyMessage;
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, true);
            }

            this.generateButton.Enabled = true;
            this.saveToolStripMenuItem.Enabled = true;
        }

        private async void buttonLoad_MouseDown(object sender, MouseEventArgs e)
        {
            this.ProcessingLabel.Text = MaintainabilityConstants.Messages.ProcessLoading;
        }

        private void buttonUnselectAll_Click(object sender, EventArgs e)
        {
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = false;
            this.ProcessingLabel.Text = MaintainabilityConstants.Messages.EmptyMessage;
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, false);
            }

            this.generateButton.Enabled = false;
            this.saveToolStripMenuItem.Enabled = false;
        }

        protected void GetXMLData()
        {
            List<Method> methods = new List<Method>();
            string filePath = string.Format(MaintainabilityConstants.Paths.OutputFolder, comboBox1.Text);
            XElement xml = XElement.Load(filePath);
            IEnumerable<XElement> xmlMethods = xml.Descendants(MaintainabilityConstants.XmlDescendants.Method).ToList();

            foreach (XElement method in xmlMethods)
            {
                string name, file, line, maintainabilityIndex;
                GetMethodObjectInfo(method, out name, out file, out line, out maintainabilityIndex);

                methods.Add(BuildMethodObject(name, file, line, maintainabilityIndex));
            }

            DataTable dt = BuildDataTable();

            foreach (var method in methods)
            {
                FillDataTableRows(dt, method);
            }

            dataGridView1.DataSource = dt;
        }

        private static void FillDataTableRows(DataTable dt, Method method)
        {
            DataRow dtrow = dt.NewRow();
            dtrow[MaintainabilityConstants.DataTableColumnsAndRows.Name] = method.Name;
            dtrow[MaintainabilityConstants.DataTableColumnsAndRows.File] = method.File;
            dtrow[MaintainabilityConstants.DataTableColumnsAndRows.Line] = method.Line;
            dtrow[MaintainabilityConstants.DataTableColumnsAndRows.MaintainabilityIndex] = method.Metrics.First().Value;
            dt.Rows.Add(dtrow);
        }

        private static DataTable BuildDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(MaintainabilityConstants.DataTableColumnsAndRows.Name, typeof(string));
            dt.Columns.Add(MaintainabilityConstants.DataTableColumnsAndRows.File, typeof(string));
            dt.Columns.Add(MaintainabilityConstants.DataTableColumnsAndRows.Line, typeof(string));
            dt.Columns.Add(MaintainabilityConstants.DataTableColumnsAndRows.MaintainabilityIndex, typeof(string));
            return dt;
        }

        private static void GetMethodObjectInfo(XElement method, out string name, out string file, out string line, out string maintainabilityIndex)
        {
            XElement metrics = method.Descendants(MaintainabilityConstants.XmlDescendants.Metrics).First();
            name = method.Attribute(MaintainabilityConstants.XmlAttributes.Name).Value;
            file = method.Attribute(MaintainabilityConstants.XmlAttributes.File).Value;
            line = method.Attribute(MaintainabilityConstants.XmlAttributes.Line).Value;
            maintainabilityIndex = metrics.Element(MaintainabilityConstants.XmlElements.Metric).Attribute(MaintainabilityConstants.XmlAttributes.Value).Value;
        }

        private static Method BuildMethodObject(string name, string file, string line, string maintainabilityIndex)
        {
            return new Method
            {
                Name = name,
                File = file,
                Line = line,
                Metrics = new List<Metric>
                    {
                        new Metric
                        {
                            Value = maintainabilityIndex
                        }
                    }
            };
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            app.Visible = true;

            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;

            worksheet.Name = "Exported from gridView";

            for (int i = 1; i < this.dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = this.dataGridView1.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < this.dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = this.dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetXMLData();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveSelectedSettings();
            this.loadToolStripMenuItem.Enabled = true;
            this.deleteToolStripMenuItem.Enabled = true;
        }

        private void SaveSelectedSettings()
        {
            List<string> solutions = new List<string>();

            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                solutions.Add(itemChecked.ToString());
            }

            object moduleSettings = this.BuildModuleSettingPreferences(solutions);

            using (StreamWriter file = File.CreateText(MaintainabilityConstants.Paths.SettingsPreferenceFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, moduleSettings);
            }
        }

        private object BuildModuleSettingPreferences(List<string> solutions)
        {
            return new Module
            {
                ModuleName = this.comboBoxModule.Text,
                ModulePreferences = new ModulePreferences
                {
                    BranchName = this.comboBoxBranch.Text,
                    Solutions = solutions
                }
            };
        }

        private async void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = false;
            this.checkedListBox1.Items.Clear();
            if (File.Exists(MaintainabilityConstants.Paths.SettingsPreferenceFile))
            {
                if (new FileInfo(MaintainabilityConstants.Paths.SettingsPreferenceFile).Length > 0)
                {
                    using (StreamReader read = new StreamReader(MaintainabilityConstants.Paths.SettingsPreferenceFile))
                    {
                        await this.LoadSavedPreferences(read);
                    }
                }

                this.generateButton.Enabled = true;
                this.buttonSelectAll.Enabled = true;
                this.buttonUnselectAll.Enabled = true;
            }
        }

        private async Task LoadSavedPreferences(StreamReader read)
        {
            string json = read.ReadToEnd();
            Module moduleSettings = JsonConvert.DeserializeObject<Module>(json);
            this.comboBoxModule.SelectedItem = moduleSettings.ModuleName.ToString();
            this.comboBoxBranch.SelectedItem = moduleSettings.ModulePreferences.BranchName.ToString();
            string[] filePaths = await this.GetFileDirectory();

            foreach (string file in filePaths)
            {
                this.checkedListBox1.Items.Add(file, false);
            }

            for (int selected = 0; selected < filePaths.Length; selected++)
            {
                if (moduleSettings.ModulePreferences.Solutions.Contains(this.checkedListBox1.Items[selected].ToString()))
                {
                    this.checkedListBox1.SetItemChecked(selected, true);
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(MaintainabilityConstants.Paths.SettingsPreferenceFile))
            {
                FileStream fileStream = File.Open(MaintainabilityConstants.Paths.SettingsPreferenceFile, FileMode.Open);
                fileStream.SetLength(0);
                fileStream.Close();
                this.loadToolStripMenuItem.Enabled = false;
            }
        }

        private void exitAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
