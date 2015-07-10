using HP.HSP.UA3.Core.UX.Data.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Forms
{
    public partial class AutoGenModelDefForm : Form
    {
        public string BusinessModelName = string.Empty;
        public string CurrentModelDataAssembly = string.Empty;
        public string CurrentModelDataBinPath = string.Empty;
        public string CurrentModelNamespace = string.Empty;
        public List<DisplaySizeConfigurationModel> DisplaySizes = null;
        public LocalizationConfigurationModel LocalConfig = null;
        public List<ModelDefinitionModel> ModelDefinitions = null;
        public bool HasDataChanged = false;

        public AutoGenModelDefForm()
        {
            InitializeComponent();
        }

        private void AutoGenModelDefForm_Load(object sender, EventArgs e)
        {
            TypeTextbox.Text = this.CurrentModelNamespace;
            TypeTextbox.Focus();
        }

        private void AutoGenModelDefForm_Shown(object sender, EventArgs e)
        {
            DisplaySizeDropdown.Items.Clear();
            DisplaySizeDropdown.Items.Add("*");
            if(this.DisplaySizes != null)
            {
                foreach(DisplaySizeConfigurationModel dispalySize in this.DisplaySizes)
                {
                    DisplaySizeDropdown.Items.Add(dispalySize.Name);
                }
            }
            ModeDropdown.SelectedIndex = 0;
            DisplaySizeDropdown.SelectedIndex = 0;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void AutoGenModelButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if(IsFormValid())
                {
                    AutoGenerateModelDefinition();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void TypeTextbox_TextChanged(object sender, EventArgs e)
        {
            ToggleGenButton();
        }

        private void ModeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleGenButton();
        }

        private void ScopeTextbox_TextChanged(object sender, EventArgs e)
        {
            ToggleGenButton();
        }

        private void DisplaySizeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleGenButton();
        }

        private void AutoGenerateModelDefinition()
        {
            //Locate current assembly file
            string assemblyFile = string.Format(this.CurrentModelDataBinPath, ModeDropdown.SelectedItem.ToString()) + this.CurrentModelDataAssembly;
            if (File.Exists(assemblyFile))
            {
                //Dynamically load assembly file
                Assembly assembly = Assembly.LoadFrom(assemblyFile);
                GenerateTypeModelDefinition(assembly, TypeTextbox.Text);
            }
            else
            {
                throw new Exception(string.Format("Unable to laod asssembly file '{0}'.", assemblyFile));
            }
        }

        private void GenerateTypeModelDefinition(Assembly assembly, string typeString)
        {
            List<string> childTypes = new List<string>();

            ModelDefinitionModel existingModelDef = this.ModelDefinitions.Find(md =>
                   string.Compare(md.Type, typeString, true) == 0
                && string.Compare(md.Scope, ScopeTextbox.Text) == 0
                && md.DisplaySize == DisplaySizeDropdown.SelectedItem.ToString()
                );

            if (existingModelDef == null)
            {
                Type type = assembly.GetType(typeString);
                if (type != null)
                {
                    //Build base model definition
                    ModelDefinitionModel modelDef = new ModelDefinitionModel()
                    {
                        DisplaySize = DisplaySizeDropdown.SelectedItem.ToString(),
                        Id = Common.Utilities.GenerateNewID(),
                        Type = typeString,
                        Scope = ScopeTextbox.Text.Trim()
                    };

                    modelDef.ModelProperties = new List<ModelPropertyModel>();
                    PropertyInfo[] properties = type.GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        System.ComponentModel.DataAnnotations.DataType dataType =
                            System.ComponentModel.DataAnnotations.DataType.Custom;

                        Core.UX.Common.CoreEnumerations.DataAnnotations.DataRestrictionType dataRestrictionType =
                            Core.UX.Common.CoreEnumerations.DataAnnotations.DataRestrictionType.None;

                        string hintType = string.Empty;
                        int maxLength = 0;
                        bool isXmlIgnored = false;
                        string defaultText = FormatCamelCaseText(property.Name);
                        string labelContentId = string.Format("{0}.Label.Field.{1}", this.BusinessModelName, property.Name);

                        string propertyType = property.PropertyType.ToString();
                        if (!isXmlIgnored
                            && propertyType != "HP.HSP.UA3.Core.UX.Data.Common.MetadataModel"
                            && propertyType != "HP.HSP.UA3.Core.UX.Data.Common.EditorModel"
                            )
                        {
                            if (propertyType.StartsWith(this.CurrentModelNamespace))
                            {
                                childTypes.Add(propertyType);
                            }

                            bool skipProperty = false;
                            switch (propertyType)
                            {
                                case "HP.HSP.UA3.Core.UX.Data.Common.ListItemModel":
                                    dataType = System.ComponentModel.DataAnnotations.DataType.Custom;
                                    dataRestrictionType = Core.UX.Common.CoreEnumerations.DataAnnotations.DataRestrictionType.None;
                                    hintType = "ListItemSingle";
                                    break;

                                case "HP.HSP.UA3.Core.UX.Data.Common.ListModel":
                                    dataType = System.ComponentModel.DataAnnotations.DataType.Custom;
                                    dataRestrictionType = Core.UX.Common.CoreEnumerations.DataAnnotations.DataRestrictionType.None;
                                    hintType = "ListSingle";
                                    break;

                                case "System.Boolean":
                                    break;

                                case "System.DateTime":
                                    dataType = System.ComponentModel.DataAnnotations.DataType.Date;
                                    dataRestrictionType = Core.UX.Common.CoreEnumerations.DataAnnotations.DataRestrictionType.None;
                                    hintType = "Date";
                                    break;

                                case "System.Int32":
                                    dataType = System.ComponentModel.DataAnnotations.DataType.Custom;
                                    dataRestrictionType = Core.UX.Common.CoreEnumerations.DataAnnotations.DataRestrictionType.None;
                                    hintType = "Number";
                                    break;

                                case "System.Decimal":
                                case "System.Double":
                                    dataType = System.ComponentModel.DataAnnotations.DataType.Currency;
                                    dataRestrictionType = Core.UX.Common.CoreEnumerations.DataAnnotations.DataRestrictionType.Numeric;
                                    hintType = "Currency";
                                    maxLength = 10;
                                    break;

                                case "System.String":
                                    dataType = System.ComponentModel.DataAnnotations.DataType.Text;
                                    dataRestrictionType = Core.UX.Common.CoreEnumerations.DataAnnotations.DataRestrictionType.SafeCharacters;
                                    hintType = "Text";
                                    maxLength = 50;
                                    break;

                                default:
                                    skipProperty = true;
                                    break;
                            }

                            if(!skipProperty)
                            {
                                ModelPropertyModel propertyModel = new ModelPropertyModel()
                                {
                                    Id = Common.Utilities.GenerateNewID(),
                                    DataRestrictionType = dataRestrictionType,
                                    DataType = dataType,
                                    DefaultText = defaultText,
                                    DisplayType = Core.UX.Common.CoreEnumerations.DataAnnotations.DataDisplayType.Editable,
                                    HintType = hintType,
                                    IsRequired = true,
                                    LabelContentId = labelContentId,
                                    MaxLength = maxLength,
                                    Name = property.Name
                                };
                                modelDef.ModelProperties.Add(propertyModel);

                                if (GenerateLabelsCheckBox.Checked)
                                {
                                    foreach (LocaleConfigurationModel locale in this.LocalConfig.Locales)
                                    {
                                        if (locale.LocaleLabels.Find(l => string.Compare(l.ContentId, labelContentId, true) == 0) == null)
                                        {
                                            LocaleConfigurationLabelModel label = new LocaleConfigurationLabelModel()
                                            {
                                                Id = Common.Utilities.GenerateNewID(),
                                                ContentId = labelContentId,
                                                LocaleId = locale.Id,
                                                Text = defaultText,
                                                Tooltip = defaultText
                                            };
                                            locale.LocaleLabels.Add(label);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    this.ModelDefinitions.Add(modelDef);
                    this.HasDataChanged = true;
                }
                else
                {
                    throw new Exception(string.Format("Unable to load type '{0}'.", TypeTextbox.Text));
                }
            }

            if(CascasdeCheckbox.Checked && childTypes.Count > 0)
            {
                foreach(string childType in childTypes)
                {
                    GenerateTypeModelDefinition(assembly, childType);
                }
            }
        }

        private bool IsFormValid()
        {
            //Check for type
            if (string.IsNullOrEmpty(TypeTextbox.Text.Trim()))
            {
                TypeTextbox.Focus();
                MessageBox.Show("Type is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check for mode
            if(ModeDropdown.SelectedItem == null)
            {
                ModeDropdown.Focus();
                MessageBox.Show("Mode is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check for scopr
            if (string.IsNullOrEmpty(ScopeTextbox.Text.Trim()))
            {
                ScopeTextbox.Focus();
                MessageBox.Show("Scope is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check for display size
            if (DisplaySizeDropdown.SelectedItem == null)
            {
                ModeDropdown.Focus();
                MessageBox.Show("Display Size is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check for unique model definition
            ModelDefinitionModel modelDef = this.ModelDefinitions.Find(md =>
                   string.Compare(md.Type, TypeTextbox.Text.Trim(), true) == 0
                && string.Compare(md.Scope, ScopeTextbox.Text) == 0
                && md.DisplaySize == DisplaySizeDropdown.SelectedItem.ToString()
                );

            if(modelDef != null)
            {
                TypeTextbox.Focus();
                MessageBox.Show("Another model definition already exists for the specified type, scope and display size.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private string FormatCamelCaseText(string originalText)
        {
            Regex r = new Regex("([A-Z]+[a-z]+)");
            return r.Replace(originalText, m => (m.Value.Length > 3 ? m.Value : m.Value.ToLower()) + " ");
        }

        private void ToggleGenButton()
        {
            if(TypeTextbox.Text.Trim().Length > 0
                && ModeDropdown.SelectedItem != null
                && ScopeTextbox.Text.Trim().Length > 0
                && DisplaySizeDropdown.SelectedItem != null)
            {
                AutoGenModelButton.Enabled = true;
            }
            else
            {
                AutoGenModelButton.Enabled = false;
            }
        }
    }
}
