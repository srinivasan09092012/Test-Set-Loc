
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;


namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    public partial class Confirmation_LocalizationModel : Form
    {
        public Confirmation_LocalizationModel()
        {
            InitializeComponent();
        }
        public MainForm MainForm { get; set; }

        private void Confirmation_LocalizationModel_Load(object sender, EventArgs e)
        {
          this.RefreshGrid();

            if (MainForm.ModelDefinitions.Count > 0 &&
                MainForm.ModelProperties.Count > 0)
            {
                this.loadPushButton.Enabled = true;
            }
            else
            {
                this.loadPushButton.Enabled = false;
            }
        }

        private void definitionssGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void RefreshGrid()
        {
            List<string> row = new List<string>();

            for (int i = 0; i < MainForm.ModelDefinitions.Count; i++)
            {
                if (string.IsNullOrEmpty(MainForm.ModelDefinitions[i].Id))
                {
                    MainForm.ModelDefinitions[i].Id = "<< NULL OR MISSING VALUE >>";
                }

                row.Clear();
                row.Add(MainForm.ModelDefinitions[i].Action);
                row.Add(MainForm.ModelDefinitions[i].Module.Name);
                row.Add(MainForm.ModelDefinitions[i].Id);
                row.Add(MainForm.ModelDefinitions[i].Type);
                row.Add(MainForm.ModelDefinitions[i].Scope);
                row.Add(MainForm.ModelDefinitions[i].DisplaySize);

                this.definitionsGridView.Rows.Add(row.ToArray());
            }

            for (int i = 0; i < MainForm.ModelProperties.Count; i++)
            {
                if (string.IsNullOrEmpty(MainForm.ModelProperties[i].Id))
                {
                    MainForm.ModelProperties[i].Id = "<< NULL OR MISSING VALUE >>";
                }

                row.Clear();
                row.Add(MainForm.ModelProperties[i].Action);
                row.Add(MainForm.ModelProperties[i].Module.Name);
                row.Add(MainForm.ModelProperties[i].Id);
                row.Add(MainForm.ModelProperties[i].ParentLink);
                row.Add(MainForm.ModelProperties[i].Name);
                row.Add(MainForm.ModelProperties[i].DataType);
                row.Add(MainForm.ModelProperties[i].DataRestrictionType);
                row.Add(MainForm.ModelProperties[i].DisplayType);
                row.Add(MainForm.ModelProperties[i].IgnoreDirtyData);
                row.Add(MainForm.ModelProperties[i].IsRequired);
                row.Add(MainForm.ModelProperties[i].MaxLength);
                row.Add(MainForm.ModelProperties[i].LabelContentId);
                row.Add(MainForm.ModelProperties[i].DefaultText);
                row.Add(MainForm.ModelProperties[i].HintType);
                row.Add(MainForm.ModelProperties[i].AccessKey);
                row.Add(MainForm.ModelProperties[i].Height);
                row.Add(MainForm.ModelProperties[i].CompareTo);
                row.Add(MainForm.ModelProperties[i].CompareToContent);
                row.Add(MainForm.ModelProperties[i].ViewRight);
                row.Add(MainForm.ModelProperties[i].EditRight);

                this.propertiesGridView.Rows.Add(row.ToArray());
            }
        }

        private void ProcessLoad(object sender, EventArgs e)
        {
            string coreTenantModuleId;
            int loadModelDefintionSuccessful = 0;
            int loadErrors = 0;

            Cursor.Current = Cursors.WaitCursor;
            coreTenantModuleId = ConfigurationManager.AppSettings["CoreTenantModuleId"];

            if (coreTenantModuleId != null)
            {
                this.LoadModelDefintionAndProperties(coreTenantModuleId, ref loadModelDefintionSuccessful, ref loadErrors);
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Tenant Configuration load complete. " + loadModelDefintionSuccessful + " DataList Items loaded and " + loadErrors + " errors reported.", "Tenant Load Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void LoadModelDefintionAndProperties(string tenantModuleId, ref int loadModelDefintionSuccessful, ref int loadErrors)
        {
            string moduleName;
            string fileName;
            string errorFileName;
            string[] lines;
            string newLine;
            string errorLine;
            string oldErrorFileName = "";
            
            for (int i = 0; i < MainForm.ModelDefinitions.Count; i++)
            {
                errorFileName = @"C:\" + MainForm.ModelDefinitions[i].Module.Name + ".txt";
                if (oldErrorFileName != errorFileName)
                {
                    if (File.Exists(errorFileName))
                    File.Delete(errorFileName);
                    using (StreamWriter ef = File.AppendText(errorFileName))
                    {
                        errorLine = MainForm.ModelDefinitions[i].Module.Name + " Discrepencies Between Tenant COnfig XML and TFS";
                        ef.WriteLine(errorLine);
                        errorLine = " ";
                        ef.WriteLine(errorLine);
                        ef.WriteLine(errorLine);
                    }
                    oldErrorFileName = errorFileName;
                }
                
                moduleName = MainForm.ModelDefinitions[i].Type.Substring(11, MainForm.ModelDefinitions[i].Type.Length - 11);
                if (MainForm.ModelDefinitions[i].Type.IndexOf("Data") + 5 < MainForm.ModelDefinitions[i].Type.Length)
                {
                    fileName = @"C:\UA3\Source\" +
                                moduleName.Substring(0, moduleName.IndexOf(".")) +
                                @"\Dev\UX\" +
                                MainForm.ModelDefinitions[i].Type.Substring(0, MainForm.ModelDefinitions[i].Type.IndexOf("Data") + 4) +
                                @"\" +
                                MainForm.ModelDefinitions[i].Type.Substring(MainForm.ModelDefinitions[i].Type.IndexOf("Data") + 5,
                                                                           MainForm.ModelDefinitions[i].Type.Length -
                                                                           MainForm.ModelDefinitions[i].Type.IndexOf("Data") - 5).Replace(".", @"\")
                                + ".cs";
                }
                else
                {
                    fileName = @"C:\UA3\Source\noname.txt";
                }


                if (File.Exists(fileName))
                {
                    this.CheckOutModelFromTfs(fileName);
                    lines = System.IO.File.ReadAllLines(fileName);
                    using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
                        foreach (string line in lines)
                        {
                            if (line.Contains("public class"))
                            {
                                /* newLine = "    [Scope: " + MainForm.ModelDefinitions[i].Scope + "]";
                                sw.WriteLine(newLine);
                                newLine = "    [DisplaySize: " + MainForm.ModelDefinitions[i].DisplaySize + "]";
                                sw.WriteLine(newLine); */ 
                                this.MainForm.ModelDefinitions[i].Action = "Added";
                                this.definitionsGridView.Rows[i].Cells[0].Value = this.MainForm.ModelDefinitions[i].Action;
                                loadModelDefintionSuccessful++;
                                this.definitionsGridView.CurrentCell = this.definitionsGridView.Rows[i].Cells[0];
                                this.definitionsGridView.Rows[i].Selected = true;
                                this.definitionsGridView.Refresh();
                            }
                            else if (line.Contains("{ get; set; }"))
                            {
                                string attributeName = line.Substring(16, line.Length - 16);
                                attributeName = attributeName.Substring(attributeName.IndexOf(" ") + 1, attributeName.Length - attributeName.IndexOf(" ") - 1);
                                attributeName = attributeName.Substring(0, attributeName.IndexOf("{") - 1);
                                for (int j = 0; j < MainForm.ModelProperties.Count; j++)
                                {
                                    if (MainForm.ModelDefinitions[i].Type == MainForm.ModelProperties[j].ParentLink &&
                                        attributeName == MainForm.ModelProperties[j].Name)
                                    {
                                        newLine = "        [ModelProperties(Name = \"" + MainForm.ModelProperties[j].Name + "\",";
                                        sw.WriteLine(newLine);
                                        if (MainForm.ModelProperties[j].DataRestrictionType != null)
                                        {
                                            newLine = "             DataRestrictionType = CoreEnumerations.DataAnnotations.DataRestrictionType." + MainForm.ModelProperties[j].DataRestrictionType + ",";
                                            sw.WriteLine(newLine);
                                        }
                                        if (MainForm.ModelProperties[j].DisplayType != null)
                                        {
                                            newLine = "             DisplayType = CoreEnumerations.DataAnnotations.DataDisplayType." + MainForm.ModelProperties[j].DisplayType + ",";
                                            sw.WriteLine(newLine);
                                        }
                                        if (MainForm.ModelProperties[j].IgnoreDirtyData != null)
                                        {
                                            newLine = "             IgnoreDirtyData = " + MainForm.ModelProperties[j].IgnoreDirtyData + ",";
                                            sw.WriteLine(newLine);
                                        }
                                        if (MainForm.ModelProperties[j].IsRequired != null)
                                        {
                                            newLine = "             IsRequired = " + MainForm.ModelProperties[j].IsRequired + ",";
                                            sw.WriteLine(newLine);
                                        }
                                        if (MainForm.ModelProperties[j].MaxLength != null)
                                        {
                                            newLine = "             MaxLength = " + MainForm.ModelProperties[j].MaxLength + ",";
                                            sw.WriteLine(newLine);
                                        }
                                        if (MainForm.ModelProperties[j].LabelContentId != null)
                                        {
                                            newLine = "             LabelContentId = \"" + MainForm.ModelProperties[j].LabelContentId + "\",";
                                            sw.WriteLine(newLine);
                                        }
                                        if (MainForm.ModelProperties[j].DefaultText != null)
                                        {
                                            newLine = "             DefaultText = \"" + MainForm.ModelProperties[j].DefaultText + "\",";
                                            sw.WriteLine(newLine);
                                        }
                                        if (MainForm.ModelProperties[j].HintType != null)
                                        {
                                            newLine = "             HintType = \"" + MainForm.ModelProperties[j].HintType + "\",";
                                            sw.WriteLine(newLine);
                                        }
                                        if (MainForm.ModelProperties[j].AccessKey != null)
                                        {
                                            newLine = "             AccessKey = \"" + MainForm.ModelProperties[j].AccessKey + "\",";
                                            sw.WriteLine(newLine);
                                        }
                                        if (MainForm.ModelProperties[j].Height != null)
                                        {
                                            newLine = "             Height = \"" + MainForm.ModelProperties[j].Height + "\",";
                                            sw.WriteLine(newLine);
                                        }
                                        if (MainForm.ModelProperties[j].CompareTo != null)
                                        {
                                            newLine = "             CompareTo = \"" + MainForm.ModelProperties[j].CompareTo + "\",";
                                            sw.WriteLine(newLine);
                                        }
                                        if (MainForm.ModelProperties[j].CompareToContent != null)
                                        {
                                            newLine = "             CompareToContent = \"" + MainForm.ModelProperties[j].CompareToContent + "\",";
                                            sw.WriteLine(newLine);
                                        }
                                        if (MainForm.ModelProperties[j].ViewRight != null)
                                        {
                                            newLine = "             ViewRight = \"" + MainForm.ModelProperties[j].ViewRight + "\",";
                                            sw.WriteLine(newLine);
                                        }
                                        if (MainForm.ModelProperties[j].EditRight != null)
                                        {
                                            newLine = "             EditRight = \"" + MainForm.ModelProperties[j].EditRight + "\",";
                                            sw.WriteLine(newLine);
                                        }
                                        newLine = "             DataType = DataType." + MainForm.ModelProperties[j].DataType + ")]";
                                        sw.WriteLine(newLine);
                                        this.MainForm.ModelProperties[j].Action = "Added";
                                        this.propertiesGridView.Rows[j].Cells[0].Value = this.MainForm.ModelProperties[j].Action;
                                        this.propertiesGridView.CurrentCell = this.propertiesGridView.Rows[j].Cells[0];
                                        this.propertiesGridView.Rows[j].Selected = true;
                                        this.propertiesGridView.Refresh();
                                    }
                                }
                            }
                            sw.WriteLine(line);
                        }
                }
                else
                {
                    using (StreamWriter ef = File.AppendText(errorFileName))
                    {
                        if (MainForm.ModelDefinitions[i].Type.IndexOf("Data") + 5 < MainForm.ModelDefinitions[i].Type.Length)
                        {
                            errorLine = "Model Does Not Exist In TFS : " +
                                    MainForm.ModelDefinitions[i].Type.Substring(MainForm.ModelDefinitions[i].Type.IndexOf("Data") + 5,
                                                                               MainForm.ModelDefinitions[i].Type.Length -
                                                                               MainForm.ModelDefinitions[i].Type.IndexOf("Data") - 5).Replace(".", @"\")
                                    + ".cs";
                        }
                        else
                        {
                            errorLine = "Model Does Not Exist In TFS : UNDEFINED .cs";
                        }
                        ef.WriteLine(errorLine);
                        for (int j = 0; j < MainForm.ModelProperties.Count; j++)
                        {
                            if (MainForm.ModelDefinitions[i].Type == MainForm.ModelProperties[j].ParentLink)
                            {
                                errorLine = "   Property for Model Does Not Exist In TFS : " +
                                            MainForm.ModelDefinitions[i].Type.Substring(MainForm.ModelDefinitions[i].Type.IndexOf("Data") + 5,
                                                                                       MainForm.ModelDefinitions[i].Type.Length -
                                                                                       MainForm.ModelDefinitions[i].Type.IndexOf("Data") - 5).Replace(".", @"\")
                                            + " -- "
                                            + MainForm.ModelProperties[j].Name;
                                ef.WriteLine(errorLine);
                                this.MainForm.ModelProperties[j].Action = "NOT FOUND";
                                this.propertiesGridView.Rows[j].Cells[0].Value = this.MainForm.ModelProperties[j].Action;
                                this.propertiesGridView.CurrentCell = this.propertiesGridView.Rows[j].Cells[0];
                                this.propertiesGridView.Rows[j].Selected = true;
                                this.propertiesGridView.Refresh();
                            }
                        }
                        errorLine = " ";
                        ef.WriteLine(errorLine);
                        ef.WriteLine(errorLine);
                    }

                    this.MainForm.ModelDefinitions[i].Action = "NOT FOUND";
                    this.definitionsGridView.Rows[i].Cells[0].Value = this.MainForm.ModelDefinitions[i].Action;
                    loadErrors++;
                    this.definitionsGridView.CurrentCell = this.definitionsGridView.Rows[i].Cells[0];
                    this.definitionsGridView.Rows[i].Selected = true;
                    this.definitionsGridView.Refresh();
                }
            }
            for (int i = 0; i < MainForm.ModelDefinitions.Count; i++)
            {
                errorFileName = @"C:\" + MainForm.ModelDefinitions[i].Module.Name + ".txt";
                using (StreamWriter ef = File.AppendText(errorFileName))
                {
                    oldErrorFileName = errorFileName;
                    for (int j = 0; j < MainForm.ModelProperties.Count; j++)
                    {
                        if (MainForm.ModelDefinitions[i].Type == MainForm.ModelProperties[j].ParentLink &&
                            MainForm.ModelProperties[j].Action == null)
                        {
                            if (MainForm.ModelDefinitions[i].Type.IndexOf("Data") + 5 < MainForm.ModelDefinitions[i].Type.Length)
                            {
                                errorLine = "   Property for Model Does Not Exist In TFS : " +
                                    MainForm.ModelDefinitions[i].Type.Substring(MainForm.ModelDefinitions[i].Type.IndexOf("Data") + 5,
                                                                               MainForm.ModelDefinitions[i].Type.Length -
                                                                               MainForm.ModelDefinitions[i].Type.IndexOf("Data") - 5).Replace(".", @"\")
                                    + " -- "
                                    + MainForm.ModelProperties[j].Name;
                            }
                            else
                            {
                                errorLine = "   Property for Model Does Not Exist In TFS : UNDEFINED -- " 
                                + MainForm.ModelProperties[j].Name;
                            }
                            ef.WriteLine(errorLine);
                            this.MainForm.ModelProperties[j].Action = "NOT FOUND";
                            this.propertiesGridView.Rows[j].Cells[0].Value = this.MainForm.ModelProperties[j].Action;
                            this.propertiesGridView.CurrentCell = this.propertiesGridView.Rows[j].Cells[0];
                            this.propertiesGridView.Rows[j].Selected = true;
                            this.propertiesGridView.Refresh();
                        }
                    }
                }
            }
        }

        private void CheckOutModelFromTfs(string fileName)
        { 
            var workspaceInfo = Workstation.Current.GetLocalWorkspaceInfo(fileName);
            using (var server = new TfsTeamProjectCollection(workspaceInfo.ServerUri))
            {
                var workspace = workspaceInfo.GetWorkspace(server);
                workspace.PendEdit(fileName);
            }
        }

        private void cancelPushButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void propertiesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
