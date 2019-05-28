using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using Common;


namespace UI
{
    public partial class UI : Form
    {
        public UI()
        {
            InitializeComponent();
        }

        private void UI_Load(object sender, EventArgs e)
        {
            var modulesRegistered = ConfigurationManager.AppSettings["HPPModules"].Split(',').ToList();

            foreach (string module in modulesRegistered)
            {
                modulListCtrl.Items.Add(module);
            }
            
        }

        private void SaveChanges_Click(object sender, EventArgs e)
        {
            DocModuleSettingModel moduleSetting = new DocModuleSettingModel();
            XmlSerializer xs;
            string storageDrive = ConfigurationManager.AppSettings["storageDrive"];

            if (!string.IsNullOrEmpty(storageDrive))
            {
                Directory.CreateDirectory(storageDrive);

                DirectoryInfo sDrive = new DirectoryInfo(storageDrive);

                if (sDrive.Exists)
                {
                    moduleSetting.ModuleName = modulListCtrl.Text;
                    moduleSetting.webSourcePath = SourcePath.Text.EndsWith("/") || SourcePath.Text.EndsWith(@"\") ? SourcePath.Text.Remove(SourcePath.Text.Length - 1, 1) : SourcePath.Text;
                    moduleSetting.webTargetPath = TargetPath.Text.EndsWith("/") || TargetPath.Text.EndsWith(@"\") ? TargetPath.Text.Remove(TargetPath.Text.Length - 1, 1) : TargetPath.Text;
                    moduleSetting.MainPageContent = MainPageContent.Text;

                    xs = new XmlSerializer(typeof(DocModuleSettingModel));
                    TextWriter tw = new StreamWriter(sDrive + @"\" + moduleSetting.ModuleName + Common.Constant.WebSolutionStructure.Files.Extensions.xmlExtension);
                    xs.Serialize(tw, moduleSetting);
                    tw.Flush();
                    tw.Close();
                    
                    MessageBox.Show("Changes saved for " + moduleSetting.ModuleName + " module");
                }
            }
            else
            {
                MessageBox.Show("Incorrect settings cant continue, verify UI configuration for a valid path and restart the application");
                SaveChanges.Enabled = false;
            }

        }

        private void modulListCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string storageDrive = ConfigurationManager.AppSettings["storageDrive"];
            DirectoryInfo sDrive = new DirectoryInfo(storageDrive);
            XmlSerializer xs;

            DocModuleSettingModel moduleSetting = new DocModuleSettingModel();

            if (File.Exists(sDrive + @"\" + ((ComboBox)sender).Text + Common.Constant.WebSolutionStructure.Files.Extensions.xmlExtension))
            {
                xs = new XmlSerializer(typeof(DocModuleSettingModel));
                using (var sr = new StreamReader(sDrive + @"\" + ((ComboBox)sender).Text + Common.Constant.WebSolutionStructure.Files.Extensions.xmlExtension))
                {
                    moduleSetting = (DocModuleSettingModel)xs.Deserialize(sr);
                    modulListCtrl.Text = moduleSetting.ModuleName;
                    SourcePath.Text = moduleSetting.webSourcePath;
                    TargetPath.Text = moduleSetting.webTargetPath;
                    MainPageContent.Text = moduleSetting.MainPageContent;
                }

                currentStatusLbl.Text = "Settings loaded for this Module now you can start to edit";
            }
            else
            {
                MessageBox.Show("Settings not found for this entry: " + ((ComboBox)sender).Text);
                currentStatusLbl.Text = "Application ready you can start create the entry for the selected Module";
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
