//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using UA3.CodeFactory.Core.PluginModel;

namespace UA3.CodeFactory.Plugins.AddServiceCommand
{
    [CodeFactoryPlugin("Add Command", "Scaffolds a command and related code for an existing Command Service", "1.0")]
    public class AddServiceCommandPlugin : CodeFactoryPlugin<AddServiceCommandSettings>
    {
        protected override AddServiceCommandSettings CreateDefault()
        {
            return AddServiceCommandSettings.Default();
        }

        protected override ISolutionTransform CreateTransform(AddServiceCommandSettings settings)
        {
            return new AddServiceCommandTransform(settings);
        }
    }
}