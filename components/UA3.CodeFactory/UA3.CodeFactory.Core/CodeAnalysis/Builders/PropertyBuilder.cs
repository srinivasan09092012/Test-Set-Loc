//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace UA3.CodeFactory.Core.CodeAnalysis.Builders
{
    public class PropertyBuilder
    {
        private List<KeyValuePair<string, string>> attributes = new List<KeyValuePair<string, string>>();

        internal PropertyBuilder(string name)
        {
            this.Name = name;
        }

        protected string Name { get; private set; }

        protected string Type { get; private set; }

        protected TypeVisibility Visibility { get; private set; }

        protected bool IsReadOnly { get; private set; }

        public PropertyBuilder WithType(string type)
        {
            this.Type = type;
            return this;
        }

        public PropertyBuilder Public()
        {
            this.Visibility = TypeVisibility.Public;
            return this;
        }

        public PropertyBuilder Internal()
        {
            this.Visibility = TypeVisibility.Internal;
            return this;
        }

        public PropertyBuilder Private()
        {
            this.Visibility = TypeVisibility.Private;
            return this;
        }

        public PropertyBuilder Protected()
        {
            this.Visibility = TypeVisibility.Protected;
            return this;
        }
        
        public PropertyBuilder ReadOnly()
        {
            this.IsReadOnly = true;
            return this;
        }

        public PropertyDeclarationSyntax BuildPropertySyntax()
        {
            PropertyDeclarationSyntax result = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(this.Type), this.Name);

            switch(this.Visibility)
            {
                case TypeVisibility.Internal:
                    result = result.AddModifiers(SyntaxFactory.Token(SyntaxKind.InternalKeyword));
                    break;
                case TypeVisibility.Public:
                    result = result.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
                    break;
                case TypeVisibility.Private:
                    result = result.AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));
                    break;
                case TypeVisibility.Protected:
                    result = result.AddModifiers(SyntaxFactory.Token(SyntaxKind.ProtectedKeyword));
                    break;
                default:
                    break;
            }

            result = result.AddAccessorListAccessors(
                SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)
                    ));

            if (!this.IsReadOnly)
            {
                result = result.AddAccessorListAccessors(
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                    .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)
                    ));
            }

            var attributes = this.BuildAttributes();

            if (attributes.Length > 0)
            {
                result = result.AddAttributeLists(attributes);
            }

            return result.NormalizeWhitespace();
        }

        public PropertyBuilder WithAttribute(string name, string argumentExpression = null)
        {
            this.attributes.Add(new KeyValuePair<string, string>(name, argumentExpression));
            return this;
        }

        private AttributeListSyntax[] BuildAttributes()
        {
            return this.attributes.ToAttributes();
        }
    }
}
