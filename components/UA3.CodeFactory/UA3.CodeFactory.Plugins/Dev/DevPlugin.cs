//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;
using UA3.CodeFactory.Core.PluginModel;

namespace UA3.CodeFactory.Plugins.Dev
{
#if DEBUG
    [CodeFactoryPlugin("Dev", "Developer / Test Plugin Only", "1.0")]
#endif
    [ExcludeFromCodeCoverage]
    public class DevPlugin : CodeFactoryPlugin<DevPluginSettings>
    {
        protected override DevPluginSettings CreateDefault()
        {
            return new DevPluginSettings();
        }

        protected override ISolutionTransform CreateTransform(DevPluginSettings settings)
        {
            return new DevTransform(settings);
        }
    }
}