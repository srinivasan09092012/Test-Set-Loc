//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using UA3.CodeFactory.Plugins.AddServiceQuery;

namespace UA3.CodeFactory.Plugins.Tests.AddServiceQuery
{
    [TestClass]
    public class AddServiceQueryPluginTests
    {
        [TestMethod]
        public void ddServiceQueryPlugin_GetDefaultSettings_should_return_defaults()
        {
            var plugin = new AddServiceQueryPlugin();

            var actual = plugin.GetDefaultSettings();
            var expected = AddServiceQuerySettings.Default();

            var properties = TypeDescriptor.GetProperties(actual);

            foreach (PropertyDescriptor pd in properties)
            {
                var actualValue = pd.GetValue(actual);
                var expectedValue = pd.GetValue(expected);

                Assert.AreEqual(expectedValue, actualValue);
            }
        }

        [TestMethod]
        public void ddServiceQueryPlugin_GetTransform_should_return_transform_with_provided_settings()
        {
            var settings = AddServiceQuerySettings.Default();

            var transform = new AddServiceQueryPlugin().GetTransform(settings);

            AddServiceQuerySettings actualSettings = new PrivateObject(transform).GetFieldOrProperty("Settings") as AddServiceQuerySettings;
            Assert.IsNotNull(actualSettings);
            Assert.AreSame(settings, actualSettings);
        }
    }
}
