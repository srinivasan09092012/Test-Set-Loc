//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UA3.CodeFactory.TestSupport
{
    public static class Extensions
    {
        public static ClassDeclarationSyntax GetClass(this CompilationUnitSyntax source, string className)
        {
            return source.SyntaxTree.GetRootAsync().Result
                .DescendantNodesAndSelf()
                .OfType<ClassDeclarationSyntax>()
                .Where(p => p.Identifier.ToString() == className)
                .FirstOrDefault();
        }

        public static List<string> GetUsings(this CompilationUnitSyntax source)
        {
            return source.SyntaxTree.GetRootAsync().Result
                .DescendantNodesAndSelf()
                .OfType<UsingDirectiveSyntax>()
                .Select(p => p.Name.ToFullString())
                .ToList();
        }

        public static bool HasUsings(this CompilationUnitSyntax source, params string[] usings)
        {
            var actual = source.SyntaxTree.GetRootAsync().Result
                .DescendantNodesAndSelf()
                .OfType<UsingDirectiveSyntax>()
                .Select(p => p.Name.ToFullString())
                .ToList();

            return usings.All(p => actual.Contains(p));
        }

        public static List<string> GetAttributes(this ClassDeclarationSyntax classSource)
        {
            return classSource.AttributeLists
                .SelectMany(p => p.Attributes)
                .Select(a => a.Name.ToFullString())
                .ToList();
        }

        public static string GetNamespace(this ClassDeclarationSyntax classSource)
        {
            return classSource.Ancestors()
                .OfType<NamespaceDeclarationSyntax>()
                .Select(p => p.Name.ToString())
                .FirstOrDefault();
        }

        public static bool IsInNamespace(this ClassDeclarationSyntax classSource, string expectedNamespace)
        {
            return classSource.GetNamespace() == expectedNamespace;
        }

        public static bool HasBaseType(this ClassDeclarationSyntax classSource, string baseType)
        {
            return classSource.BaseList.Types.Any(p => p.Type.ToString() == baseType);
        }

        public static MethodDeclarationSyntax GetMethod(this ClassDeclarationSyntax classSource, string methodName)
        {
            return classSource.Members.OfType<MethodDeclarationSyntax>().Where(p => p.Identifier.ToString() == methodName).FirstOrDefault();
        }

        public static MethodDeclarationSyntax GetMethod(this InterfaceDeclarationSyntax interfaceSource, string methodName)
        {
            return interfaceSource.Members.OfType<MethodDeclarationSyntax>().Where(p => p.Identifier.ToString() == methodName).FirstOrDefault();
        }

        public static bool HasMethodWithSignature(this ClassDeclarationSyntax classSource, string methodName, params Tuple<string, string>[] parameters)
        {
            bool result = false;
            var method = classSource.GetMethod(methodName);

            if (method !=  null)
            {
                result = method.HasSignature(parameters);
            }

            return result;
        }

        public static bool HasMethodWithSignature(this InterfaceDeclarationSyntax interfaceSource, string methodName, params Tuple<string, string>[] parameters)
        {
            bool result = false;
            var method = interfaceSource.GetMethod(methodName);

            if (method != null)
            {
                result = method.HasSignature(parameters);
            }

            return result;
        }

        public static bool HasSignature(this MethodDeclarationSyntax method, params Tuple<string, string>[] parameters)
        {
            var existing = method.ParameterList.Parameters.Select(p => new Tuple<string, string>(p.Identifier.ToString(), p.Type.ToString())).ToList();

            if (existing.Count != parameters.Length)
            {
                return false;
            }

            for (int i = 0; i < existing.Count; i++)
            {
                if ((existing[i].Item1 != parameters[i].Item1) || (existing[i].Item2 != parameters[i].Item2))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool HasReturnType(this MethodDeclarationSyntax method, string returnType)
        {
            return method.ReturnType.ToString() == returnType;
        }
    }
}
