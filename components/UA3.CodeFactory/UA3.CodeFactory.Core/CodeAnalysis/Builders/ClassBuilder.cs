//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace UA3.CodeFactory.Core.CodeAnalysis.Builders
{
    public class ClassBuilder
    {
        private string name;
        private string @namespace;
        private bool partial = false;
        private List<MethodBuilder> methods = new List<MethodBuilder>();
        private List<string> usings = new List<string>();
        private string headerComment;
        private TypeVisibility visibility = TypeVisibility.Unspecified;
        private string baseClass;
        private List<KeyValuePair<string, string>> attributes = new List<KeyValuePair<string, string>>();
        private List<string> interfaces = new List<string>();
        private List<ConstructorBuilder> constructors = new List<ConstructorBuilder>();
        private List<PropertyBuilder> properties = new List<PropertyBuilder>();

        internal ClassBuilder(string name)
        {
            this.name = name;
        }

        public ClassBuilder Partial()
        {
            this.partial = true;
            return this;
        }

        public ClassBuilder WithBaseClass(string baseClass)
        {
            this.baseClass = baseClass;
            return this;
        }

        public ClassBuilder WithImplementingInterface(string interfaceTypeName)
        {
            this.interfaces.Add(interfaceTypeName);
            return this;
        }

        public ClassBuilder Public()
        {
            return this.WithVisibility(TypeVisibility.Public);
        }

        public ClassBuilder Internal()
        {
            return this.WithVisibility(TypeVisibility.Internal);
        }

        public ClassBuilder Private()
        {
            return this.WithVisibility(TypeVisibility.Private);
        }

        public ClassBuilder WithProperty(PropertyBuilder property)
        {
            this.properties.Add(property);
            return this;
        }

        public ClassBuilder InNamespace(string @namespace)
        {
            this.@namespace = @namespace;
            return this;
        }

        public ClassBuilder WithMethod(MethodBuilder method)
        {
            this.methods.Add(method);
            return this;
        }

        public ClassBuilder WithUsings(params string[] usings)
        {
            this.usings.AddRange(usings);
            return this;
        }

        public ClassBuilder WithHeaderComment(string comment)
        {
            this.headerComment = comment;
            return this;
        }

        public CompilationUnitSyntax BuildCompilationUnit()
        {
            CompilationUnitSyntax result = SyntaxFactory.CompilationUnit();

            if (this.usings.Count > 0)
            {
                var toAdd = this.usings.Select(p => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(p))).ToArray();

                if (!string.IsNullOrEmpty(this.headerComment))
                {
                    toAdd[0] = toAdd[0].WithLeadingTrivia(SyntaxFactory.TriviaList(SyntaxFactory.ParseLeadingTrivia(BasCodeGenerator.CommentHeaderString)));
                }

                result = result.AddUsings(toAdd);
            }

            MemberDeclarationSyntax[] members = this.BuildClassMembers();
            SyntaxToken[] modifiers = this.BuildModifiers();
            AttributeListSyntax[] attributes = this.BuildAttributes();
            ConstructorDeclarationSyntax[] constructors = this.BuildConstructors();

            var classDec = SyntaxFactory.ClassDeclaration(this.name)
                .AddMembers(constructors)
                .AddMembers(members)
                .AddAttributeLists(attributes)
                .AddModifiers(modifiers);

            if (!string.IsNullOrEmpty(this.baseClass))
            {
                classDec = classDec.AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(this.baseClass)));
            }

            if (this.interfaces.Count > 0)
            {
                foreach (var iface in this.interfaces)
                {
                    classDec = classDec.AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(iface)));
                }
            }

            if (!string.IsNullOrEmpty(this.@namespace))
            {
                NamespaceDeclarationSyntax ns = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(this.@namespace));
                ns = ns.AddMembers(classDec);
                result = result.AddMembers(ns);
            }
            else
            {
                result = result.AddMembers(classDec);
            }

            return result.NormalizeWhitespace();
        }

        private ConstructorDeclarationSyntax[] BuildConstructors()
        {
            List<ConstructorDeclarationSyntax> result = new List<ConstructorDeclarationSyntax>();

            foreach (ConstructorBuilder ctor in this.constructors)
            {
                ConstructorDeclarationSyntax dec = ctor.BuildConstructor(this.name);
                result.Add(dec);
            }

            return result.ToArray();
        }

        public ClassBuilder WithAttribute(string name, string argumentExpression = null)
        {
            this.attributes.Add(new KeyValuePair<string, string>(name, argumentExpression));
            return this;
        }

        public ClassBuilder WithConstructor(ConstructorBuilder constructor)
        {
            this.constructors.Add(constructor);
            return this;
        }

        private AttributeListSyntax[] BuildAttributes()
        {
            return this.attributes.ToAttributes();
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

            if (this.partial)
            {
                result.Add(SyntaxFactory.Token(SyntaxKind.PartialKeyword));
            }

            return result.ToArray();
        }

        private MemberDeclarationSyntax[] BuildClassMembers()
        {
            var methods = this.methods.Select(p => p.BuildClassMethod()).Cast<MemberDeclarationSyntax>();

            var properties = this.properties.Select(p => p.BuildPropertySyntax()).Cast<MemberDeclarationSyntax>();

            return properties.Concat(methods).ToArray();
        }

        protected ClassBuilder WithVisibility(TypeVisibility visibility)
        {
            this.visibility = visibility;
            return this;
        }
    }
}
