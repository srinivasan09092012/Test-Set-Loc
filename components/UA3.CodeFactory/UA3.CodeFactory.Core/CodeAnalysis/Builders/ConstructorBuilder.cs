//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
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
    public class ConstructorBuilder
    {
        private Dictionary<string, string> parameters = new Dictionary<string, string>();
        private TypeVisibility visibility = TypeVisibility.Private;
        private bool invokesBase = false;
        private List<string> baseParams = new List<string>();
        private List<StatementSyntax> statements = new List<StatementSyntax>();

        public ConstructorBuilder()
            : base()
        {
        }

        public ConstructorBuilder WithParameter(string name, string type)
        {
            this.parameters.Add(name, type);
            return this;
        }

        public ConstructorBuilder Internal()
        {
            this.visibility = TypeVisibility.Internal;
            return this;
        }

        public ConstructorBuilder Public()
        {
            this.visibility = TypeVisibility.Public;
            return this;
        }

        public ConstructorBuilder Private()
        {
            this.visibility = TypeVisibility.Private;
            return this;
        }

        public ConstructorBuilder InvokesBase(params string[] argumentExpressions)
        {
            if (argumentExpressions != null)
            {
                this.baseParams.AddRange(argumentExpressions);
            }

            return this;
        }

        public ConstructorBuilder WithBodyStatement(string statement)
        {
            return this.WithBodyStatement(SyntaxFactory.ParseStatement(statement));
        }

        public ConstructorBuilder WithBodyStatement(StatementSyntax statement)
        {
            this.statements.Add(statement);
            return this;
        }

        public ConstructorDeclarationSyntax BuildConstructor(string typeName)
        {
            var ctor = SyntaxFactory.ConstructorDeclaration(SyntaxFactory.ParseToken(typeName));

            if (this.parameters.Count > 0)
            {
                ParameterSyntax[] parameters = this.BuildParameters();

                ctor = ctor.AddParameterListParameters(parameters);
            }

            var modifiers = this.BuildModifiers();

            ctor = ctor.AddModifiers(modifiers);

            if (this.invokesBase)
            {
                var baseArgs = this.BuildBaseArguments();
                var initializer = SyntaxFactory.ConstructorInitializer(SyntaxKind.BaseConstructorInitializer)
                    .AddArgumentListArguments(baseArgs);

                ctor = ctor.WithInitializer(initializer);
            }

            if (this.statements.Count > 0)
            {
                ctor = ctor.AddBodyStatements(this.statements.ToArray());
            }

            return ctor.NormalizeWhitespace();
        }

        private ArgumentSyntax[] BuildBaseArguments()
        {
            List<ArgumentSyntax> result = new List<ArgumentSyntax>();

            foreach (string paramName in this.baseParams)
            {
                var param = SyntaxFactory.Argument(SyntaxFactory.ParseExpression(paramName));
                result.Add(param);
            }

            return result.ToArray();
        }

        private SyntaxToken[] BuildModifiers()
        {
            List<SyntaxToken> result = new List<SyntaxToken>();

            switch (this.visibility)
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

                case TypeVisibility.Unspecified:
                default:
                    break;
            }

            return result.ToArray();
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
    }
}
