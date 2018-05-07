//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UA3.CodeFactory.Core.CodeAnalysis;
using UA3.CodeFactory.TestSupport;

namespace UA3.CodeFactory.Tests.CodeAnalysis
{
    [TestClass]
    public class BasCodeGeneratorTests
    {
        [TestMethod]
        public void BasCodeGenerator_GenerateCommandClass_should_generate_command_class()
        {
            BasCodeGenerator inst = new BasCodeGenerator();
            TypeModel input = new TypeModel("NewCommand", "NewCommandNamespace");

            CompilationUnitSyntax result = inst.GenerateCommandClass(input);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.HasUsings(BasNamespaces.CqrsBase, "System.Runtime.Serialization"));

            var classDec = result.GetClass("NewCommand");
            Assert.IsNotNull(classDec);
            Assert.IsTrue(classDec.IsInNamespace("NewCommandNamespace"));

            var attributes = classDec.GetAttributes();
            Assert.IsNotNull(attributes);
            CollectionAssert.Contains(attributes, "DataContract");

            var namespaces = result.GetUsings();
            Assert.IsNotNull(namespaces);
            CollectionAssert.Contains(namespaces, "System.Runtime.Serialization");
            CollectionAssert.Contains(namespaces, BasNamespaces.CqrsBase);
        }

        [TestMethod]
        public void BasCodeGenerator_GenerateEventClass_should_generate_event_class()
        {
            BasCodeGenerator inst = new BasCodeGenerator();
            TypeModel input = new TypeModel("NewEvent", "NewEventNamespace");

            CompilationUnitSyntax result = inst.GenerateCommandClass(input);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.HasUsings(BasNamespaces.CqrsBase, "System.Runtime.Serialization"));

            var classDec = result.GetClass("NewEvent");
            Assert.IsNotNull(classDec);
            Assert.IsTrue(classDec.IsInNamespace("NewEventNamespace"));

            var attributes = classDec.GetAttributes();
            Assert.IsNotNull(attributes);
            CollectionAssert.Contains(attributes, "DataContract");            
        }

        [TestMethod]
        public void BasCodeGenerator_GenerateCommandHandlerClass_should_generate_command_handler_class()
        {
            BasCodeGenerator inst = new BasCodeGenerator();

            TypeModel handlerType = new TypeModel("HandlerType", "HandlerNamespace");
            TypeModel cmdType = new TypeModel("CommandType", "CommandNamespace");

            CompilationUnitSyntax result = inst.GenerateCommandHandlerClass(handlerType, cmdType);
            Assert.IsNotNull(result);

            Assert.IsTrue(result.HasUsings(BasNamespaces.CqrsBase, BasNamespaces.CqrsInterfaces, "CommandNamespace"));

            ClassDeclarationSyntax classDec = result.GetClass("HandlerType");
            Assert.IsNotNull(classDec);            

            Assert.IsTrue(classDec.IsInNamespace("HandlerNamespace"));
            Assert.IsTrue(classDec.HasBaseType("IHandleCommand<CommandType>"));

            MethodDeclarationSyntax method = classDec.GetMethod("Handle");
            Assert.IsNotNull(method);
            Assert.IsTrue(method.HasSignature(new Tuple<string, string>("command", "CommandType")));
            Assert.IsTrue(method.HasReturnType("Event"));
        }

        [TestMethod]
        public void BasCodeGenerator_GenerateCommandHandlerTestClass_should_generate_command_handler_test_class()
        {
            BasCodeGenerator inst = new BasCodeGenerator();

            TypeModel testType = new TypeModel("HandlerTypeTests", "Tests.HandlerNamespace");
            TypeModel handlerType = new TypeModel("HandlerType", "HandlerNamespace");
            TypeModel cmdType = new TypeModel("CommandType", "CommandNamespace");

            CompilationUnitSyntax result = inst.GenerateCommandHandlerTestClass(testType, handlerType, cmdType);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.HasUsings(BasNamespaces.CqrsBase, "Microsoft.VisualStudio.TestTools.UnitTesting", "HandlerNamespace"));

            ClassDeclarationSyntax classDec = result.GetClass("HandlerTypeTests");
            Assert.IsNotNull(classDec);
            Assert.IsTrue(classDec.IsInNamespace("Tests.HandlerNamespace"));

            MethodDeclarationSyntax method = classDec.GetMethod("HandlerType_Handle_should_handle_CommandType");
            Assert.IsNotNull(method);
            Assert.IsTrue(method.HasReturnType("void"));
        }

        [TestMethod]
        public void BasCodeGenerator_GenerateProjectionWriterTestClass_should_generate_projection_writer_test_class()
        {
            BasCodeGenerator inst = new BasCodeGenerator();

            TypeModel testType = new TypeModel("ProjectionWriterTypeTests", "Tests.ProjectionWriterType");
            TypeModel writerType = new TypeModel("ProjectionWriterType", "ProjectionWriterNamespace");
            TypeModel eventType = new TypeModel("EventType", "EventNamespace");

            CompilationUnitSyntax result = inst.GenerateProjectionWriterTestClass(testType, writerType, eventType);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.HasUsings(BasNamespaces.CqrsBase, "Microsoft.VisualStudio.TestTools.UnitTesting", "ProjectionWriterNamespace"));

            ClassDeclarationSyntax classDec = result.GetClass("ProjectionWriterTypeTests");
            Assert.IsNotNull(classDec);
            Assert.IsTrue(classDec.IsInNamespace("Tests.ProjectionWriterType"));

            MethodDeclarationSyntax method = classDec.GetMethod("ProjectionWriterType_Handle_should_handle_EventType");
            Assert.IsNotNull(method);
            Assert.IsTrue(method.HasReturnType("void"));
        }

        [TestMethod]
        public void BasCodeGenerator_GenerateProjectionWriterMethod_should_generate_handle_method()
        {
            BasCodeGenerator inst = new BasCodeGenerator();

            TypeModel eventType = new TypeModel("EventType", "EventNamespace");

            MethodDeclarationSyntax method = inst.GenerateProjectionWriterMethod(eventType);
            Assert.IsNotNull(method);
            Assert.IsTrue(method.HasReturnType("void"));
            Assert.IsTrue(method.HasSignature(new Tuple<string, string>("@event", "EventType")));
        }
    }
}
