//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using UA3.CodeFactory.Core.PluginModel;

namespace UA3.CodeFactory.Tests
{
    [CodeFactoryPlugin("TestPlugin", "desc", "1.0")]
    public class TestOnlyPlugin : ICodeFactoryPlugin
    {
        public SettingsModel GetDefaultSettings()
        {
            throw new NotImplementedException();
        }

        public ISolutionTransform GetTransform(SettingsModel settings)
        {
            throw new NotImplementedException();
        }
    }
}