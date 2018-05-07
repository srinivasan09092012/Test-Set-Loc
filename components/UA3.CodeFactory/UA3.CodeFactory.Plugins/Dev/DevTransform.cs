//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using UA3.CodeFactory.Core.CodeAnalysis;
using UA3.CodeFactory.Core.PluginModel;

namespace UA3.CodeFactory.Plugins.Dev
{
    [ExcludeFromCodeCoverage]
    public class DevTransform : SolutionTransformBase<DevPluginSettings>
    {
        public DevTransform(DevPluginSettings settings, IBasCodeGenerator codeGenerator)
            : base(settings, codeGenerator)
        {
        }

        public DevTransform(DevPluginSettings settings)
            : this(settings, new BasCodeGenerator())
        {
        }

        protected override void ExecuteTransform(SolutionContext context)
        {
            MessageBox.Show($"GuidString = {this.Settings.GuidString}");
        }
    }
}
