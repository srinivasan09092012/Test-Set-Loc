//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace UA3.CodeFactory.Core.CodeAnalysis.Builders
{
    public class MethodBuilder
    {
        private string name;
        private Dictionary<string, string> parameters = new Dictionary<string, string>();
        private string returnType;
        private MethodModifier modifier = MethodModifier.None;
        private List<StatementSyntax> statements = new List<StatementSyntax>();
        private List<KeyValuePair<string, string>> attributes = new List<KeyValuePair<string, string>>();

        internal MethodBuilder(string name)
        {
            this.name = name;
            this.Visibility = TypeVisibility.Unspecified;
        }

        public TypeVisibility Visibility { get; private set; }

        public MethodBuilder WithReturnType(string type)
        {
            this.returnType = type;
            return this;
        }

        public MethodBuilder Internal()
        {
            this.Visibility = TypeVisibility.Internal;
            return this;
        }

        public MethodBuilder Public()
        {
            this.Visibility = TypeVisibility.Public;
            return this;
        }

        public MethodBuilder Private()
        {
            this.Visibility = TypeVisibility.Private;
            return this;
        }

        public MethodBuilder Override()
        {
            this.modifier = MethodModifier.Override;
            return this;
        }

        public MethodBuilder New()
        {
            this.modifier = MethodModifier.New;
            return this;
        }

        public MethodBuilder Virtual()
        {
            this.modifier = MethodModifier.Virtual;
            return this;
        }

        public MethodBuilder Protected()
        {
            this.Visibility = TypeVisibility.Protected;
            return this;   
        }

        public MethodBuilder WithParameter(string name, string type)
        {
            this.parameters.Add(name, type);
            return this;
        }

        public MethodBuilder WithBodyStatement(string statement)
        {
            return this.WithBodyStatement(SyntaxFactory.ParseStatement(statement));
        }

        public MethodBuilder WithBodyStatement(StatementSyntax statement)
        {
            this.statements.Add(statement);
            return this;
        }

        public MethodBuilder WithAttribute(string name, string argumentExpression = null)
        {
            this.attributes.Add(new KeyValuePair<string, string>(name, argumentExpression));
            return this;
        }

        public MethodDeclarationSyntax BuildInterfaceMethod()
        {
            TypeSyntax returnType = this.BuildReturnType();
            ParameterSyntax[] parameters = this.BuildParameters();
            AttributeListSyntax[] attributes = this.BuildAttributes();

            MethodDeclarationSyntax result = SyntaxFactory.MethodDeclaration(returnType, this.name)
                .AddParameterListParameters(parameters)
                .AddAttributeLists(attributes)
                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));

            return result.NormalizeWhitespace();
        }

        public MethodDeclarationSyntax BuildClassMethod()
        {
            TypeSyntax returnType = this.BuildReturnType();
            SyntaxToken[] modifiers = this.BuildModifiers();
            ParameterSyntax[] parameters = this.BuildParameters();
            AttributeListSyntax[] attributes = this.BuildAttributes();

            MethodDeclarationSyntax result = SyntaxFactory.MethodDeclaration(returnType, this.name)
                .AddModifiers(modifiers)
                .AddParameterListParameters(parameters)
                .AddAttributeLists(attributes)
                .AddBodyStatements(this.statements.ToArray());

            return result.NormalizeWhitespace();
        }

        private AttributeListSyntax[] BuildAttributes()
        {
            return this.attributes.ToAttributes();
        }

        private ParameterSyntax[] BuildParameters()
        {
            List<ParameterSyntax> result = new List<ParameterSyntax>();

            foreach (KeyValuePair<string, string> kvp in this.parameters)
            {
                var identifier = SyntaxFactory.Identifier(kvp.Key);
                var type = SyntaxFactory.ParseTypeName(kvp.Value);

                var param = SyntaxFactory.Parameter(identifier).WithType(type);

                result.Add(param);
            }

            return result.ToArray();
        }

        private SyntaxToken[] BuildModifiers()
        {
            List<SyntaxToken> result = new List<SyntaxToken>();

            switch (this.Visibility)
            {
                case TypeVisibility.Internal:
                    result.Add(SyntaxFactory.Token(SyntaxKind.InternalKeyword));
                    break;

                case TypeVisibility.Private:
                    result.Add(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));
                    break;

                case TypeVisibility.Public:
                    result.Add(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
                    break;

                case TypeVisibility.Protected:
                    result.Add(SyntaxFactory.Token(SyntaxKind.ProtectedKeyword));
                    break;

                case TypeVisibility.Unspecified:
                default:
                    break;
            }

            switch (this.modifier)
            {
                case MethodModifier.New:
                    result.Add(SyntaxFactory.Token(SyntaxKind.NewKeyword));
                    break;

                case MethodModifier.Override:
                    result.Add(SyntaxFactory.Token(SyntaxKind.OverrideKeyword));
                    break;

                case MethodModifier.Virtual:
                    result.Add(SyntaxFactory.Token(SyntaxKind.VirtualKeyword));
                    break;

                case MethodModifier.None:
                default:
                    break;
            }

            return result.ToArray();
        }

        private TypeSyntax BuildReturnType()
        {
            TypeSyntax returnType;
            if (string.IsNullOrEmpty(this.returnType))
            {
                returnType = SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword));
            }
            else
            {
                returnType = SyntaxFactory.ParseTypeName(this.returnType);
            }

            return returnType;
        }
    }
}