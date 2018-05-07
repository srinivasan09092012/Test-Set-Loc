//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace UA3.CodeFactory.Core.CodeAnalysis.Builders
{
    internal static class InternalExtensions
    {
        internal static AttributeListSyntax[] ToAttributes(this List<KeyValuePair<string, string>> source)
        {
            List<AttributeListSyntax> result = new List<AttributeListSyntax>();

            foreach (KeyValuePair<string, string> kvp in source)
            {
                var attribute = SyntaxFactory.Attribute(SyntaxFactory.ParseName(kvp.Key));

                if (!string.IsNullOrEmpty(kvp.Value))
                {
                    var actual = kvp.Value;
                    if (!actual.StartsWith("(") || !actual.EndsWith(")"))
                    {
                        actual = $"({actual})";
                    }

                    var list = SyntaxFactory.ParseAttributeArgumentList(actual);
                    attribute = attribute.WithArgumentList(list);
                }

                result.Add(SyntaxFactory.AttributeList().AddAttributes(attribute));
            }

            return result.ToArray();
        }
    }
}
