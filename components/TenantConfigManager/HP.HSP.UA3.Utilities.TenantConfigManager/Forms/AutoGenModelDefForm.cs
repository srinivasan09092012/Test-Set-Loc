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
        public TenantConfigurationModel CoreTenantConfig = null;
        public string CoreModelDataAssembly = string.Empty;
        public string CoreModelDataBinPath = string.Empty;
        public string CoreModelNamespace = string.Empty;
        public string CurrentModelDataAssembly = string.Empty;
        public string CurrentModelDataBinPath = string.Empty;
        public string CurrentModelNamespace = string.Empty;
        public List<DisplaySizeConfigurationModel> DisplaySizes = null;
        public LocalizationConfigurationModel LocalConfig = null;
        public List<ModelDefinitionModel> ModelDefinitions = null;
        public bool HasDataChanged = false;

        private bool _isExternalDefinition = false;

        public AutoGenModelDefForm()
        {
            InitializeComponent();
        }

        private void AutoGenModelDefForm_Load(object sender, EventArgs e)
        {
            TypeDropdown.SelectedIndex = 0;
            TypeDropdown.Enabled = (this.CoreModelNamespace != this.CurrentModelNamespace);
            ClassTextbox.Text = this.CurrentModelNamespace;
            TypeDropdown.Focus();
            if (TypeDropdown.Enabled)
            {
                LoadCoreModelDefintions();
            }
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

        private void TypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (TypeDropdown.SelectedIndex == 0)
                {
                    _isExternalDefinition = false;
                    ClassTextbox.Visible = true;
                    ClassDropdown.Visible = false;
                }
                else
                {
                    _isExternalDefinition = true;
                    ClassTextbox.Visible = false;
                    ClassDropdown.Visible = true;
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

        private void AutoGenerateModelDefinition()
        {
            //Locate current assembly file
            string assemblyFile = string.Format(this.CurrentModelDataBinPath, ModeDropdown.SelectedItem.ToString()) + this.CurrentModelDataAssembly;
            
            if(_isExternalDefinition)
            {
                assemblyFile = string.Format(this.CoreModelDataBinPath, ModeDropdown.SelectedItem.ToString()) + this.CoreModelDataAssembly;
            }
 
            if (File.Exists(assemblyFile))
            {
                //Dynamically load assembly file
                Assembly assembly = Assembly.LoadFrom(assemblyFile);

                string classType = ClassTextbox.Text;
                if (_isExternalDefinition)
                {
                    classType = ClassDropdown.SelectedItem.ToString().Split('|')[0].Trim();
                }
                GenerateTypeModelDefinition(assembly, classType);
            }
            else
            {
                throw new Exception(string.Format("Unable to load asssembly file '{0}'.", assemblyFile));
            }
        }

        private void GenerateTypeModelDefinition(Assembly assembly, string classType)
        {
            List<string> childTypes = new List<string>();

            ModelDefinitionModel existingModelDef = this.ModelDefinitions.Find(md =>
                   string.Compare(md.Type, classType, true) == 0
                && string.Compare(md.Scope, ScopeTextbox.Text) == 0
                && md.DisplaySize == DisplaySizeDropdown.SelectedItem.ToString()
                );

            if (existingModelDef == null)
            {
                ModelDefinitionModel externalModelDef = null;
                if (_isExternalDefinition)
                {
                    string[] mdItems = ClassDropdown.SelectedItem.ToString().Split('|');
                    externalModelDef = this.CoreTenantConfig.Modules[0].ModelDefinitionConfiguration.Find(md =>
                           string.Compare(md.Type, mdItems[0].Trim(), true) == 0
                        && string.Compare(md.Scope, mdItems[1].Trim()) == 0
                        && md.DisplaySize == mdItems[2].Trim()
                        );
                }

                if(externalModelDef != null)
                {
                    ModelDefinitionModel modelDef = (ModelDefinitionModel)externalModelDef.Clone();
                    modelDef.Scope = ScopeTextbox.Text;
                    modelDef.DisplaySize = DisplaySizeDropdown.SelectedItem.ToString();
                    foreach (ModelPropertyModel property in modelDef.ModelProperties)
                    {
                        string newLabelContentId = property.LabelContentId.Replace("Core", this.BusinessModelName);
                        if (GenerateLabelsCheckBox.Checked)
                        {
                            foreach (LocaleConfigurationModel locale in this.LocalConfig.Locales)
                            {
                                if (locale.LocaleLabels.Find(l => string.Compare(l.ContentId, property.LabelContentId, true) == 0) == null)
                                {
                                    LocaleConfigurationModel coreLocalConfig = this.CoreTenantConfig.Modules[0].LocalizationConfiguration.Locales.Find(lc => lc.LocaleId == locale.LocaleId);
                                    LocaleConfigurationLabelModel coreLabel = coreLocalConfig.LocaleLabels.Find(l => l.ContentId == property.LabelContentId);

                                    LocaleConfigurationLabelModel label;
                                    if(coreLabel != null)
                                    {
                                        label = (LocaleConfigurationLabelModel)coreLabel.Clone();
                                        label.ContentId = newLabelContentId;
                                    }
                                    else
                                    {
                                        label = new LocaleConfigurationLabelModel()
                                        {
                                            Id = Common.Utilities.GenerateNewID(),
                                            ContentId = property.LabelContentId,
                                            LocaleId = locale.Id,
                                            Text = property.DefaultText,
                                            Tooltip = property.DefaultText
                                        };
                                    }
                                    locale.LocaleLabels.Add(label);
                                }
                            }
                        }
                        property.LabelContentId = newLabelContentId;
                    }

                    this.ModelDefinitions.Add(modelDef);
                    this.HasDataChanged = true;
                }
                else
                {
                    Type type = assembly.GetType(classType);
                    if (type != null)
                    {
                        //Build base model definition
                        ModelDefinitionModel modelDef = new ModelDefinitionModel()
                        {
                            DisplaySize = DisplaySizeDropdown.SelectedItem.ToString(),
                            Id = Common.Utilities.GenerateNewID(),
                            Type = classType,
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

                                if (!skipProperty)
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
                        throw new Exception(string.Format("Unable to load type '{0}'.", ClassTextbox.Text));
                    }
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
            if (string.IsNullOrEmpty(ClassTextbox.Text.Trim()))
            {
                ClassTextbox.Focus();
                MessageBox.Show("Type is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check for type of Core or current module only
            if (!ClassTextbox.Text.StartsWith(this.CoreModelNamespace) && !ClassTextbox.Text.StartsWith(this.CurrentModelNamespace))
            {
                ClassTextbox.Focus();
                MessageBox.Show(string.Format("Type must start with '{0}' or '{1}'.", this.CurrentModelNamespace, this.CoreModelNamespace), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check for mode
            if(ModeDropdown.SelectedItem == null)
            {
                ModeDropdown.Focus();
                MessageBox.Show("Mode is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check for scope
            if (string.IsNullOrEmpty(ScopeTextbox.Text.Trim()))
            {
                ScopeTextbox.Focus();
                MessageBox.Show("Scope is a required field.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_isExternalDefinition && ScopeTextbox.Text == "*")
            {
                ScopeTextbox.Focus();
                MessageBox.Show("Scope can not be generic for a Core model definition.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                   string.Compare(md.Type, ClassTextbox.Text.Trim(), true) == 0
                && string.Compare(md.Scope, ScopeTextbox.Text) == 0
                && md.DisplaySize == DisplaySizeDropdown.SelectedItem.ToString()
                );

            if(modelDef != null)
            {
                ClassTextbox.Focus();
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

        private void LoadCoreModelDefintions()
        {
            foreach(ModelDefinitionModel modelDefinition in this.CoreTenantConfig.Modules[0].ModelDefinitionConfiguration)
            {
                ClassDropdown.Items.Add(modelDefinition.Type + " | " + modelDefinition.Scope + " | " + modelDefinition.DisplaySize);
            }
        }

        private void ToggleGenButton()
        {
            if(ClassTextbox.Text.Trim().Length > 0
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
