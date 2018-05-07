//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------

namespace UA3.CodeFactory.Core.CodeAnalysis.Builders
{
    public static class Method
    {
        public static MethodBuilder Named(string name)
        {
            return new MethodBuilder(name);
        }
    }
}
