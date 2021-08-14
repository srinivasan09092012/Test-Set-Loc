//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace UA3.CodeFactory.Core
{
    public interface ICodeFactoryPluginMetadata
    {
        string Name { get; }

        string Description { get; }

        string Version { get; }
    }
}
