//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UA3.CodeFactory.Core.CodeAnalysis.Builders;

namespace UA3.CodeFactory.Core.Tests.CodeAnalysis.Builders
{
    [TestClass]
    public class MethodBuilderTests
    {
        [TestMethod]
        public void MethodBuilder_should_create_valid_class_method_syntax()
        {
            var builder = Method.Named("TestMethod")
                .Public()
                .WithParameter("x", "int")
                .WithParameter("y", "int")
                .WithReturnType("int")
                .WithBodyStatement("return x + y;")
                .WithAttribute("OperationContract", "Foo=true,Bar=false");

            var cls = builder.BuildClassMethod();

            var iface = builder.BuildInterfaceMethod();

            Console.WriteLine(cls.ToString());
            Console.WriteLine(iface.ToString());
        }

        [TestMethod]
        public void Adding_method_to_class_should_be_formatted()
        {
            var cls = SyntaxFactory.ClassDeclaration("TestClass")
                .NormalizeWhitespace();

            Console.WriteLine(cls.ToString());

            var builder = Method.Named("TestMethod")
                .Public()
                .WithParameter("x", "int")
                .WithParameter("y", "int")
                .WithReturnType("int")
                .WithBodyStatement("return x + y;")
                .WithAttribute("OperationContract", "Foo=true,Bar=false");

            cls = cls.AddMembers(builder.BuildClassMethod());
            Console.WriteLine(cls.ToString());

            cls = cls.NormalizeWhitespace();
            Console.WriteLine(cls.ToString());
        }
    }
}
