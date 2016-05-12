//--------------------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace HP.HSP.UA3.Utilities.LoadTenantDb.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();

            this.LocalizationDatalists = new List<LocalizationLocaleNode>();
            this.SecurityRoles = new List<SecurityNode>();
            this.SecurityFunctions = new List<SecurityNode>();
            this.SecurityRights = new List<SecurityNode>();
            this.SecurityRolesAttributes = new List<string>();
            this.SecurityFunctionsAttributes = new List<string>();
            this.SecurityRightsAttributes = new List<string>();
            this.XmlDoc = new XmlDocument();
        }

        public List<SecurityNode> SecurityRoles { get; set; }

        public List<string> SecurityRolesAttributes { get; set; }

        public List<SecurityNode> SecurityFunctions { get; set; }

        public List<string> SecurityFunctionsAttributes { get; set; }

        public List<SecurityNode> SecurityRights { get; set; }

        public List<string> SecurityRightsAttributes { get; set; }

        public List<LocalizationLocaleNode> LocalizationDatalists { get; set; }

        public string TenantId { get; set; }

        public string ODataEndpointAddress { get; set; }

        private Modules ConfiguredModules { get; set; }

        private XmlDocument XmlDoc { get; set; }

        private void MainFormLoad(object sender, EventArgs e)
        {
            this.nextButton.Enabled = false;

            this.configTypeComboBox.Items.Add("Localization DataLists");
            this.configTypeComboBox.Items.Add("Security Roles");
            this.configTypeComboBox.SelectedItem = "Localization DataLists";

            this.ODataEndpointAddress = ConfigurationManager.AppSettings["ODataEndpointAddress"];
        }

        private void NextButtonClick(object sender, EventArgs e)
        {
            this.LocalizationDatalists.Clear();
            this.SecurityRoles.Clear();
            this.SecurityFunctions.Clear();
            this.SecurityRights.Clear();

            this.LoadTenantModel();

            if (this.configTypeComboBox.SelectedItem.ToString() == "Security Roles")
            {
                ConfirmationSecurityDialog securityForm = new ConfirmationSecurityDialog();
                securityForm.MainForm = this;
                securityForm.ShowDialog();
                securityForm.Dispose();
            }
            else if (this.configTypeComboBox.SelectedItem.ToString() == "Localization DataLists")
            {
                ConfirmationLocalizationDatalistDialog localizationDatalistForm = new ConfirmationLocalizationDatalistDialog();
                localizationDatalistForm.MainForm = this;
                localizationDatalistForm.ShowDialog();
                localizationDatalistForm.Dispose();
            }
        }

        private void LoadTenantModel()
        {
            XmlNodeList nl = this.XmlDoc.GetElementsByTagName("Module");

            // Iterate through checked modules
            for (int i = 0; i < this.moduleListBox.Items.Count; i++)
            {
                if (this.moduleListBox.GetItemChecked(i))
                {
                    Module checkedModule = new Module();
                    checkedModule.Name = this.moduleListBox.Items[i].ToString();

                    for (int j = 0; j < this.ConfiguredModules.ModuleNames.Count; j++)
                    {
                        if (checkedModule.Name == this.ConfiguredModules.ModuleNames[j].Name)
                        {
                            checkedModule.Id = this.ConfiguredModules.ModuleNames[j].Id;
                        }
                    }

                    foreach (XmlElement xl in nl)
                    {
                        if (xl.Attributes["name"].Value == this.moduleListBox.Items[i].ToString())
                        {
                            foreach (XmlNode sd in xl.ChildNodes)
                            {
                                if (this.configTypeComboBox.SelectedItem.ToString() == "Security Roles" && sd.Name == "SecurityRoles")
                                {
                                    this.LoadSecurityNodes(sd, checkedModule);
                                }
                                else if (this.configTypeComboBox.SelectedItem.ToString() == "Localization DataLists" && sd.Name == "LocalizationConfiguration")
                                {
                                    this.LoadLocalizationDatalistNodes(sd, checkedModule);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LoadLocalizationDatalistNodes(XmlNode sd, Module checkedModule) 
        {
            foreach (XmlNode locales in sd.ChildNodes)
            {
                foreach (XmlNode locale in locales.ChildNodes)
                {
                    LocalizationLocaleNode localizationLocaleNode = new LocalizationLocaleNode();
                    localizationLocaleNode.Module.Name = checkedModule.Name;
                    localizationLocaleNode.Module.Id = checkedModule.Id;
                    localizationLocaleNode.Id = locale.Attributes["id"].Value;
                    localizationLocaleNode.LocaleId = locale.Attributes["localeId"].Value;
                    localizationLocaleNode.Name = locale.Attributes["name"].Value;
                    this.LocalizationDatalists.Add(localizationLocaleNode);

                    foreach (XmlNode datalists in locale.ChildNodes)
                    {
                        if (datalists.Name == "LocaleDataLists")
                        {
                            foreach (XmlNode datalist in datalists.ChildNodes)
                            {
                                LocalizationDatalistNode localizationDatalistNode = new LocalizationDatalistNode();
                                localizationDatalistNode.Id = datalist.Attributes["id"].Value;
                                localizationDatalistNode.Name = datalist.Attributes["name"].Value;
                                localizationDatalistNode.ContentId = datalist.Attributes["contentId"].Value;
                                localizationLocaleNode.Datalists.Add(localizationDatalistNode);

                                foreach (XmlNode datalistItems in datalist.ChildNodes)
                                {
                                    foreach (XmlNode datalistItem in datalistItems.ChildNodes)
                                    {
                                        LocalizationDatalistItemNode localizationDatalistItemNode = new LocalizationDatalistItemNode();
                                        localizationDatalistItemNode.Id = datalistItem.Attributes["id"].Value;
                                        localizationDatalistItemNode.Value = datalistItem.Attributes["value"].Value;
                                        localizationDatalistItemNode.Text = datalistItem.Attributes["text"].Value;
                                        localizationDatalistItemNode.Order = datalistItem.Attributes["order"].Value;
                                        localizationDatalistItemNode.ContentId = datalistItem.Attributes["contentId"].Value;
                                        localizationDatalistNode.DatalistItems.Add(localizationDatalistItemNode);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LoadSecurityNodes(XmlNode sd, Module checkedModule)
        {
            foreach (XmlNode role in sd.ChildNodes)
            {
                SecurityNode securityRoleNode = new SecurityNode();
                securityRoleNode.Module.Name = checkedModule.Name;
                securityRoleNode.Module.Id = checkedModule.Id;
                securityRoleNode.Id = role.Attributes["id"].Value;
                securityRoleNode.Name = role.Attributes["name"].Value;
                securityRoleNode.ContentId = role.Attributes["contentId"].Value;
                securityRoleNode.ParentLink = null;
                securityRoleNode.ParentKey = null;

                for (int i = 0; i < role.Attributes.Count; i++)
                {
                    if (!new[] { "id", "name", "contentId" }.Contains(role.Attributes[i].Name))
                    {
                        SecurityNodeAttribute securityNodeAttribute = new SecurityNodeAttribute();
                        securityNodeAttribute.Name = role.Attributes[i].Name;
                        securityNodeAttribute.Value = role.Attributes[i].Value;
                        securityRoleNode.SecurityNodeAttribute.Add(securityNodeAttribute);
                    }
                }

                this.SecurityRoles.Add(securityRoleNode);

                foreach (XmlNode functions in role.ChildNodes)
                {
                    foreach (XmlNode function in functions.ChildNodes)
                    {
                        SecurityNode securityFunctionNode = new SecurityNode();
                        securityFunctionNode.Module.Name = checkedModule.Name;
                        securityFunctionNode.Module.Id = checkedModule.Id;
                        securityFunctionNode.Id = function.Attributes["id"].Value;
                        securityFunctionNode.Name = function.Attributes["name"].Value;
                        securityFunctionNode.ContentId = function.Attributes["contentId"].Value;
                        securityFunctionNode.ParentLink = role.Attributes["contentId"].Value;
                        securityFunctionNode.ParentKey = role.Attributes["contentId"].Value;

                        for (int i = 0; i < function.Attributes.Count; i++)
                        {
                            if (!new[] { "id", "name", "contentId" }.Contains(function.Attributes[i].Name))
                            {
                                SecurityNodeAttribute securityNodeAttribute = new SecurityNodeAttribute();
                                securityNodeAttribute.Name = function.Attributes[i].Name;
                                securityNodeAttribute.Value = function.Attributes[i].Value;
                                securityFunctionNode.SecurityNodeAttribute.Add(securityNodeAttribute);
                            }
                    }

                        this.SecurityFunctions.Add(securityFunctionNode);

                        foreach (XmlNode rights in function.ChildNodes)
                        {
                            foreach (XmlNode right in rights.ChildNodes)
                            {
                                SecurityNode securityRightNode = new SecurityNode();
                                securityRightNode.Module.Name = checkedModule.Name;
                                securityRightNode.Module.Id = checkedModule.Id;
                                securityRightNode.Id = right.Attributes["id"].Value;
                                securityRightNode.Name = right.Attributes["name"].Value;
                                securityRightNode.ParentLink = function.Attributes["contentId"].Value;
                                securityRightNode.ParentKey = function.Attributes["contentId"].Value;

                                if (right.Attributes["contentId"] != null)
                                {
                                    securityRightNode.ContentId = right.Attributes["contentId"].Value;
                                }
                                else
                                {
                                    securityRightNode.ContentId = right.Attributes["name"].Value;
                                }

                                for (int i = 0; i < right.Attributes.Count; i++)
                                {
                                    if (!new[] { "id", "name", "contentId" }.Contains(right.Attributes[i].Name))
                                    {
                                        SecurityNodeAttribute securityNodeAttribute = new SecurityNodeAttribute();
                                        securityNodeAttribute.Name = right.Attributes[i].Name;
                                        securityNodeAttribute.Value = right.Attributes[i].Value;
                                        securityRightNode.SecurityNodeAttribute.Add(securityNodeAttribute);
                                    }
                                }

                                this.SecurityRights.Add(securityRightNode);
                            }
                        }
                    }
                }
            }
        }

        private void TenantConfigFileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "XML Files|*.xml";
            openFileDialog1.DefaultExt = "xml";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.Multiselect = false;

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string tenantFile = openFileDialog1.FileName;
                this.tenantConfigFileTextBox.Text = tenantFile;
                this.moduleListBox.Items.Clear();

                if (tenantFile != null)
                {
                    this.XmlDoc.Load(tenantFile);

                    XmlElement xheader = this.XmlDoc.DocumentElement;

                    if (xheader != null)
                    {
                        for (int i = 0; i < xheader.Attributes.Count; i++)
                        {
                            if (xheader.Attributes[i].Name == "id")
                            {
                                this.TenantId = xheader.Attributes[i].Value;
                            }
                        }
                    }

                    this.ConfiguredModules = this.LoadModules();

                    for (int i = 0; i < this.ConfiguredModules.ModuleNames.Count; i++)
                    {
                        this.moduleListBox.Items.Add(this.ConfiguredModules.ModuleNames[i].Name);
                        this.moduleListBox.SetItemChecked(i, true);
                    }
                }
            }
        }

        private Modules LoadModules()
        {
            Modules configuredModules = new Modules();

            XmlNodeList nl = this.XmlDoc.GetElementsByTagName("Module");

            foreach (XmlElement xl in nl)
            {
                Module module = new Module();
                module.Name = xl.Attributes["name"].Value;
                module.Id = xl.Attributes["id"].Value;
                configuredModules.ModuleNames.Add(module);
            }
                                                  
            return configuredModules;
        }

        private void TenantConfigFile_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tenantConfigFileTextBox.Text))
            {
                this.nextButton.Enabled = true;
            }
            else
            {
                this.nextButton.Enabled = false;
                this.moduleListBox.Items.Clear();
            }
        }

        private bool CheckModulesChecked()
        {
            bool rowChecked = false;

            foreach (var itemChecked in this.moduleListBox.CheckedItems)
            {
                if (this.moduleListBox.GetItemChecked(this.moduleListBox.Items.IndexOf(itemChecked)))
                {
                    rowChecked = true;
                }
            }

            if (rowChecked)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ModuleListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.CheckModulesChecked())
            {
                this.nextButton.Enabled = true;
            }
            else
            {
                this.nextButton.Enabled = false;
            }
        }
        
        public class LocalizationDatalistItemNode
        {
            public string Action { get; set; }

            public string Id { get; set; }

            public string Value { get; set; }

            public string Text { get; set; }

            public string ContentId { get; set; }

            public string Order { get; set; }
        }
        
        public class LocalizationDatalistNode
        {
            public LocalizationDatalistNode()
            {
                this.DatalistItems = new List<LocalizationDatalistItemNode>();
            }

            public List<LocalizationDatalistItemNode> DatalistItems { get; set; }

            public string Action { get; set; }

            public string Id { get; set; }

            public string Name { get; set; }

            public string ContentId { get; set; }
        }

        public class LocalizationLocaleNode
        {
            public LocalizationLocaleNode()
            {
                this.Datalists = new List<LocalizationDatalistNode>();
                this.Module = new Module();
            }

            public List<LocalizationDatalistNode> Datalists { get; set; }

            public Module Module { get; set; }

            public string Id { get; set; }

            public string Name { get; set; }

            public string LocaleId { get; set; }
        }

        public class Module
        {
            public string Id { get; set; }

            public string Name { get; set; }
        }

        public class SecurityNode
        {
            public SecurityNode()
            {
                this.SecurityNodeAttribute = new List<SecurityNodeAttribute>();
                this.Module = new Module();
            }

            public List<SecurityNodeAttribute> SecurityNodeAttribute { get; set; }

            public Module Module { get; set; }

            public string Action { get; set; }

            public string Id { get; set; }

            public string Name { get; set; }

            public string ContentId { get; set; }

            public string ParentLink { get; set; }

            public string ParentKey { get; set; }
        }

        public class SecurityNodeAttribute
        {
            public string Name { get; set; }

            public string Value { get; set; }
        }

        public class Modules
        {
            public Modules()
            {
                this.ModuleNames = new List<Module>();
            }

            public List<Module> ModuleNames { get; set; }
        }
    }
}
