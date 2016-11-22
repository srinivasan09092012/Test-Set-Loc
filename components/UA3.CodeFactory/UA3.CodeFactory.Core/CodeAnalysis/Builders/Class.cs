//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
namespace UA3.CodeFactory.Core.CodeAnalysis.Builders
{
    public static class Class
    {
        public static ClassBuilder Named(string name)
        {
            return new ClassBuilder(name);
        }
    }
}