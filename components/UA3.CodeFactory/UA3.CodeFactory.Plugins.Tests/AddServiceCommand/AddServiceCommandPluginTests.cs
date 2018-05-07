//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using UA3.CodeFactory.Plugins.AddServiceCommand;

namespace UA3.CodeFactory.Plugins.Tests.AddServiceCommand
{
    [TestClass]
    public class AddServiceCommandPluginTests
    {
        [TestMethod]
        public void AddServiceCommandPlugin_GetDefaultSettings_should_return_defaults()
        {
            var plugin = new AddServiceCommandPlugin();

            var actual = plugin.GetDefaultSettings();
            var expected = AddServiceCommandSettings.Default();

            var properties = TypeDescriptor.GetProperties(actual);

            foreach (PropertyDescriptor pd in properties)
            {
                var actualValue = pd.GetValue(actual);
                var expectedValue = pd.GetValue(expected);

                Assert.AreEqual(expectedValue, actualValue);
            }
        }

        [TestMethod]
        public void AddServiceCommandPlugin_GetTransform_should_return_transform_with_provided_settings()
        {
            var settings = AddServiceCommandSettings.Default();

            var transform = new AddServiceCommandPlugin().GetTransform(settings);

            AddServiceCommandSettings actualSettings = new PrivateObject(transform).GetFieldOrProperty("Settings") as AddServiceCommandSettings;
            Assert.IsNotNull(actualSettings);
            Assert.AreSame(settings, actualSettings);
        }
    }
}
