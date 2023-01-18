//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UA3.CodeFactory.Core.CodeAnalysis;
using UA3.CodeFactory.Core.CodeAnalysis.Builders;

namespace UA3.CodeFactory.Core.Tests.CodeAnalysis.Builders
{
    [TestClass]
    public class ClassBuilderTests
    {
        [TestMethod]
        public void ClassBuilder_should_build_valid_class_file()
        {
            var cls = Class.Named("TestClass")
                .WithBaseClass("TestClassBase")
                .Public()
                .Partial()
                .InNamespace("UA3.Testing")
                .WithUsings("System", "System.Runtime.Serialization")
                .WithMethod(
                    Method.Named("TestMethod")
                    .Public()
                    .WithParameter("x", "int")
                    .WithParameter("y", "int")
                    .WithReturnType("int")
                    .WithBodyStatement("return x + y;")
                    .WithAttribute("OperationContract", "Foo=true,Bar=false")
                ).WithHeaderComment(BasCodeGenerator.CommentHeaderString)
                .BuildCompilationUnit().ToString();

            Console.WriteLine(cls);

            StringAssert.Contains(cls, "public partial class TestClass");
            StringAssert.Contains(cls, "[OperationContract");
            StringAssert.Contains(cls, "public int TestMethod(int x, int y)");
        }
    }
}
