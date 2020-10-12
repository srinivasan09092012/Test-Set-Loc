using FileHelpers;
using HPP.Core.API.AppConfig;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppConfigLoader
{
    public partial class Form1 : Form
    {
        List<AppSettingsResult> result = new List<AppSettingsResult>();

        public Form1()
        {
            InitializeComponent();
            CloudType.Text = "Azure";
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Browse CSV Files",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "csv",
                Filter = "CSV files (*.csv)|*.csv",
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void LoadAppConfigButton_Click(object sender, EventArgs e)
        {
            string label = string.Empty;
            IAppConfig config = null;

            if (!this.ValidateInput())
            {
                return;
            }

            if (CloudType.Text.Trim() == "Azure")
            {
                config = new AzureConfigProvider(ConnectionString.Text.Trim());
            }
            else if (CloudType.Text.Trim() == "AWS")
            {
                config = new AWSParameterStoreProvider(ConnectionString.Text.Trim());
            }

            var engine = new DelimitedFileEngine<AppSettings>();
            engine.Options.IgnoreFirstLines = 1;
            var appSettings = engine.ReadFileAsList(textBox1.Text);
            foreach (var item in appSettings)
            {
                if (!this.ValidateAppSetting(item))
                {
                    continue;
                }

                label = item.ModuleName + "_" + item.ApplicationName + "_" + txtTenantId.Text.Trim();
                try
                {
                    //config.DeleteAppSetting(item.AppSettingKey, label);
                    config.AddAppSetting(item.AppSettingKey, item.AppSettingValue, label, true);
                }
                catch (Exception ex)
                {
                    AppSettingsResult errorItem = new AppSettingsResult()
                    {
                        ApplicationName = item.ApplicationName,
                        ModuleName = item.ModuleName,
                        AppSettingKey = item.AppSettingKey,
                        AppSettingValue = item.AppSettingValue,
                        Error = ex.Message
                    };
                    result.Add(errorItem);
                }
            }

            if (result.Any())
            {
                MessageBox.Show("There are errors while loading appSettings. Please look into 'AppSettingLoad_Errors.csv' file.");
                string path = Path.GetDirectoryName(textBox1.Text);
                var errorEngine = new DelimitedFileEngine<AppSettingsResult>();
                errorEngine.HeaderText = errorEngine.GetFileHeader();
                errorEngine.WriteFile(path + @"//AppSettingLoad_Errors.csv", result);
                this.OpenFolder(Path.GetDirectoryName(textBox1.Text));
            }
            else
            {
                MessageBox.Show("AppConfig loaded successfully!!");
            }
        }

        private void OpenFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = folderPath,
                    FileName = "explorer.exe"
                };

                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show(string.Format("{0} Directory does not exist!", folderPath));
            }
        }

        private bool ValidateAppSetting(AppSettings item)
        {
            string error = string.Empty;

            if (string.IsNullOrWhiteSpace(item.ApplicationName))
            {
                error = "ApplicationName is empty; ";
            }

            if (string.IsNullOrWhiteSpace(item.ModuleName))
            {
                error = error + "ModuleName is empty; ";
            }

            if (string.IsNullOrWhiteSpace(item.AppSettingKey))
            {
                error = error + "AppSettingKey is empty;";
            }

            if (!string.IsNullOrEmpty(error))
            {
                AppSettingsResult errorItem = new AppSettingsResult()
                {
                    ApplicationName = item.ApplicationName,
                    ModuleName = item.ModuleName,
                    AppSettingKey = item.AppSettingKey,
                    AppSettingValue = item.AppSettingValue,
                    Error = error
                };
                result.Add(errorItem);

                return false;
            }

            return true;
        }

        private bool ValidateInput()
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(CloudType.Text.Trim()))
            {
                errors.AppendLine("Select Cloud");
            }

            if (string.IsNullOrWhiteSpace(txtTenantId.Text.Trim()))
            {
                errors.AppendLine("Enter valid TenantId");
            }

            if (string.IsNullOrWhiteSpace(ConnectionString.Text.Trim()))
            {
                errors.AppendLine("Enter valid Connection string");
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text.Trim()))
            {
                errors.AppendLine("Select appconfig file to load");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show($"Input validation error: {errors.ToString()}");
                return false;
            }

            return true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}