//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UA3.CodeFactory.Core;

namespace UA3.CodeFactory.Tests
{
    [TestClass]
    public class CodeFactoryPluginProviderTests
    {
        [TestMethod]
        public void CodeFactoryPluginProvider_should_locate_available_plugins()
        {
            CodeFactoryPluginProvider prov = new CodeFactoryPluginProvider(this.GetType().Assembly);

            var metadata = prov.GetPluginMetadata();
            Assert.IsNotNull(metadata);
            Assert.IsTrue(metadata.Count > 0);

            foreach (ICodeFactoryPluginMetadata md in metadata)
            {
                var plugin = prov.GetPlugin(md);
                Assert.IsNotNull(plugin);
            }
        }
    }
}
