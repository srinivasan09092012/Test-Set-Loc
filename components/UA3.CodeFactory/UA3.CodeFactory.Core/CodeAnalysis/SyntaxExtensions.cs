//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UA3.CodeFactory.Core.CodeAnalysis
{
    public static class SyntaxExtensions
    {
        public static SyntaxNode ReplaceNodes(this Document document, SyntaxNode old, SyntaxNode @new)
        {
            return document.GetSyntaxRootAsync().Result.ReplaceNode(old, @new);
        }

        public static MethodDeclarationSyntax Public(this MethodDeclarationSyntax method)
        {
            return method.WithModifiers(
                SyntaxTokenList.Create(
                    SyntaxFactory.Token(SyntaxKind.PublicKeyword)
                )
            );
        }

        public static ClassDeclarationSyntax GetClass(this Document document)
        {
            var root = document.GetSyntaxRootAsync().Result;

            if (root == null)
            {
                return null;
            }
            else
            {
                return root.DescendantNodesAndSelf().OfType<ClassDeclarationSyntax>().FirstOrDefault();
            }
        }

        public static InterfaceDeclarationSyntax GetInterface(this Document document)
        {
            return document.GetSyntaxRootAsync().Result.DescendantNodesAndSelf().OfType<InterfaceDeclarationSyntax>().FirstOrDefault();
        }

        public static MethodDeclarationSyntax WithEmptyBody(this MethodDeclarationSyntax method)
        {
            return method.WithBody(SyntaxFactory.Block());
        }

        public static MethodDeclarationSyntax WithParameters(this MethodDeclarationSyntax syntax, Dictionary<string, TypeSyntax> parameters)
        {
            var sepParamList = new SeparatedSyntaxList<ParameterSyntax>();

            foreach (KeyValuePair<string, TypeSyntax> kvp in parameters)
            {
                var paramSyntax = SyntaxFactory.Parameter(
                        SyntaxFactory.Identifier(kvp.Key))
                    .WithType(kvp.Value);

                sepParamList = sepParamList.Add(paramSyntax);
            }

            return syntax.WithParameterList(SyntaxFactory.ParameterList(sepParamList));
        }

        public static bool InheritsFrom(this ClassDeclarationSyntax node, string typeName)
        {
            if (node == null || node.BaseList == null || node.BaseList.Types == null || node.BaseList.Types.Count == 0)
                return false;
            else
                return node.BaseList.Types.Any(p => p.Type.ToString().Equals(typeName, StringComparison.InvariantCultureIgnoreCase));
        }

        public static SeparatedSyntaxList<ParameterSyntax> CreateParameterList(this MethodDeclarationSyntax method, string[] parameterTypeNames)
        {
            SeparatedSyntaxList<ParameterSyntax> result = new SeparatedSyntaxList<ParameterSyntax>();

            foreach (string p in parameterTypeNames)
            {
                result.Add(SyntaxFactory.Parameter(SyntaxFactory.Identifier(p)));
            }

            return result;
        }

        public static bool TryGetMethodByName(this TypeDeclarationSyntax node, string methodName, out MethodDeclarationSyntax method)
        {
            method = node.Members.OfType<MethodDeclarationSyntax>().Where(p => p.Identifier.Text.Equals(methodName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            return (method != null);
        }

        public static bool TryAddOrReplaceMethodByName(this ClassDeclarationSyntax node, MethodDeclarationSyntax newMethod, out ClassDeclarationSyntax updatedClass)
        {
            bool result = false;
            updatedClass = null;
            string name = newMethod.Identifier.Text;
            MethodDeclarationSyntax oldMethod = null;

            if (node.TryGetMethodByName(name, out oldMethod))
            {
                updatedClass = node.ReplaceNode(oldMethod, newMethod);
                result = true;
            }

            return result;
        }
    }
}