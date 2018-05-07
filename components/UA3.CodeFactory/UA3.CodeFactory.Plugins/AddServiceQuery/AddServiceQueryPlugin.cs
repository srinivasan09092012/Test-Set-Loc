//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using UA3.CodeFactory.Core.PluginModel;

namespace UA3.CodeFactory.Plugins.AddServiceQuery
{
    [CodeFactoryPlugin("Add Query", "Scaffolds a query and related code for an existing Query Service", "1.0")]
    public class AddServiceQueryPlugin : CodeFactoryPlugin<AddServiceQuerySettings>
    {
        protected override AddServiceQuerySettings CreateDefault()
        {
            return AddServiceQuerySettings.Default();
        }

        protected override ISolutionTransform CreateTransform(AddServiceQuerySettings settings)
        {
            return new AddServiceQueryTransform(settings);
        }
    }
}
