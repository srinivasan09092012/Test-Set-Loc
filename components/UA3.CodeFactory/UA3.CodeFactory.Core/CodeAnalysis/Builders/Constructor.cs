//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------

namespace UA3.CodeFactory.Core.CodeAnalysis.Builders
{
    public static class Constructor
    {
        public static ConstructorBuilder WithParameter(string name, string type)
        {
            return new ConstructorBuilder().WithParameter(name, type);
        }
    }
}
