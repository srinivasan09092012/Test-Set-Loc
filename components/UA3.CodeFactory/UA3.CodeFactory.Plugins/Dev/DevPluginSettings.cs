//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using UA3.CodeFactory.Core.PluginModel;
using UA3.CodeFactory.Core.UX;

namespace UA3.CodeFactory.Plugins.Dev
{
    [ExcludeFromCodeCoverage]
    public class DevPluginSettings : SettingsModel
    {
        [DisplayName("Guid String")]
        [Required]
        [Editor(typeof(TypeEditor), typeof(TypeEditor))]
        public string GuidString { get; set; }
    }
}
