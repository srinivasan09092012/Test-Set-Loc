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
using UA3.CodeFactory.Core.CodeAnalysis.Builders;

namespace UA3.CodeFactory.Core.CodeAnalysis
{
    public class BasCodeGenerator : IBasCodeGenerator
    {
        public CompilationUnitSyntax GenerateCommandClass(TypeModel classType)
        {
            return Class.Named(classType.Name)
                .InNamespace(classType.Namespace)
                .Public()
                .WithAttribute("DataContract")
                .WithBaseClass("Command")
                .WithHeaderComment(CommentHeaderString)
                .WithUsings(BasNamespaces.CqrsBase, "System.Runtime.Serialization")
                .BuildCompilationUnit();
        }

        public CompilationUnitSyntax GenerateCommandHandlerClass(TypeModel handlerType, TypeModel commandType)
        {
            return Class.Named(handlerType.Name)
                .InNamespace(handlerType.Namespace)
                .WithImplementingInterface($"IHandleCommand<{commandType.Name}>")
                .WithUsings("System", commandType.Namespace, "HP.HSP.UA3.Core.BAS.CQRS.Base", "HP.HSP.UA3.Core.BAS.CQRS.Interfaces")
                .WithHeaderComment(CommentHeaderString)
                .Public()
                .WithMethod(
                    Method.Named("Handle")
                    .Public()
                    .WithReturnType("Event")
                    .WithParameter("command", commandType.Name)
                    .WithBodyStatement("//TODO: Add real command handler code here")
                    .WithBodyStatement("throw new NotImplementedException();")
                ).BuildCompilationUnit();
        }

        public CompilationUnitSyntax GenerateCommandHandlerTestClass(TypeModel testType, TypeModel handlerType, TypeModel commandType)
        {
            return Class.Named($"{handlerType.Name}Tests")
                .InNamespace(testType.Namespace)
                .WithUsings("System", handlerType.Namespace, "HP.HSP.UA3.Core.BAS.CQRS.Base", "Microsoft.VisualStudio.TestTools.UnitTesting")
                .WithHeaderComment(CommentHeaderString)
                .WithAttribute("TestClass")
                .Public()
                .WithMethod(
                    Method.Named($"{handlerType.Name}_Handle_should_handle_{commandType.Name}")
                    .Public()
                    .WithReturnType("void")
                    .WithAttribute("TestMethod")
                    .WithBodyStatement("//TODO: Add real command handler test code here")
                    .WithBodyStatement(@"Assert.Fail(""This test is not yet implemented"");")
                ).BuildCompilationUnit();
        }

        public MethodDeclarationSyntax GenerateCommandServiceClassMethod(string methodName, TypeModel commandType, TypeModel eventType)
        {
            return Method.Named(methodName)
                .Public()
                .WithReturnType(eventType.Name)
                .WithParameter("command", commandType.Name)
                .WithBodyStatement($"return this.TryRoute(command) as {eventType.Name};")
                .BuildClassMethod()
                .NormalizeWhitespace()
                .WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed);
        }

        public MethodDeclarationSyntax GenerateCommandServiceInterfaceMethod(string methodName, TypeModel commandType, TypeModel eventType)
        {
            return Method.Named(methodName)
                 .WithReturnType(eventType.Name)
                 .WithAttribute("OperationContract")
                 .WithAttribute("FaultContract", "typeof(ServiceException)")
                 .WithParameter("command", commandType.Name)
                 .BuildInterfaceMethod()
                 .NormalizeWhitespace()
                 .WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed);
        }

        public SyntaxTrivia GenerateCommentHeader()
        {
            return SyntaxFactory.Comment(CommentHeaderString);
        }

        public CompilationUnitSyntax GenerateEventClass(TypeModel eventType)
        {
            return Class.Named(eventType.Name)
                .InNamespace(eventType.Namespace)
                .WithBaseClass("Event")
                .WithAttribute("DataContract")
                .WithUsings("HP.HSP.UA3.Core.BAS.CQRS.Base", "System.Runtime.Serialization")
                .WithHeaderComment(CommentHeaderString)
                .Public()
                .BuildCompilationUnit();
        }

        public CompilationUnitSyntax GenerateProjectionWriterClass(TypeModel projectionWriterType, TypeModel eventType, TypeModel entityType)
        {
            return Class.Named(projectionWriterType.Name)
                .InNamespace(projectionWriterType.Namespace)
                .WithBaseClass($"ProjectionWriterBase<{entityType.Name}>")
                .WithImplementingInterface($"IHandleEvent<{entityType.Name}>")
                .WithUsings("System", eventType.Namespace, "HP.HSP.UA3.Core.BAS.CQRS.Interfaces", "HP.HSP.UA3.Core.BAS.CQRS.Base", entityType.Namespace)
                .WithHeaderComment(CommentHeaderString)
                .Public()
                .WithMethod(
                    Method.Named("Handle")
                    .Public()
                    .WithReturnType("void")
                    .WithParameter("@event", entityType.Name)
                    .WithBodyStatement("//TODO: Add real projection writer code here")
                    .WithBodyStatement("throw new NotImplementedException();")
                ).BuildCompilationUnit();
        }

        public MethodDeclarationSyntax GenerateProjectionWriterMethod(TypeModel eventType)
        {
            return Method.Named("Handle")
                .Public()
                .WithReturnType("void")
                .WithParameter("@event", eventType.Name)
                .WithBodyStatement("//TODO: Add real projection writer code here")
                .WithBodyStatement("throw new NotImplementedException();")
                .BuildClassMethod();
        }

        public CompilationUnitSyntax GenerateProjectionWriterTestClass(TypeModel testType, TypeModel projectionWriterType, TypeModel eventType)
        {
            return Class.Named($"{projectionWriterType.Name}Tests")
                .InNamespace(testType.Namespace)
                .WithUsings("System", projectionWriterType.Namespace, "HP.HSP.UA3.Core.BAS.CQRS.Base", "Microsoft.VisualStudio.TestTools.UnitTesting")
                .WithHeaderComment(CommentHeaderString)
                .WithAttribute("TestClass")
                .Public()
                .WithMethod(
                    Method.Named($"{projectionWriterType.Name}_Handle_should_handle_{eventType.Name}")
                    .Public()
                    .WithAttribute("TestMethod")
                    .WithReturnType("void")
                    .WithBodyStatement("//TODO: Add real projection writer test code here")
                    .WithBodyStatement(@"Assert.Fail(""This test is not yet implemented"");")
                ).BuildCompilationUnit();
        }

        public MethodDeclarationSyntax GenerateProjectionWriterTestMethod(TypeModel testType, TypeModel eventType)
        {
            return Method.Named($"{testType.Name}_Handle_should_handle_{eventType.Name}")
                    .Public()
                    .WithAttribute("TestMethod")
                    .WithReturnType("void")
                    .WithBodyStatement("//TODO: Add real projection writer test code here")
                    .WithBodyStatement(@"Assert.Fail(""This test is not yet implemented"");")
                    .BuildClassMethod();
        }

        public CompilationUnitSyntax GenerateQueryClass(TypeModel queryType, TypeModel parmsType, TypeModel queryResultType)
        {
            return Class.Named(queryType.Name)
                .InNamespace(queryType.Namespace)
                .WithBaseClass($"Query<{queryResultType.Name}>")
                .WithAttribute("DataContract")
                .WithAttribute("KnownType", $"typeof({queryResultType.Name})")
                .WithAttribute("KnownType", $"typeof({parmsType.Name})")                
                .WithUsings(BasNamespaces.CqrsBase, "System.Runtime.Serialization", queryResultType.Namespace, parmsType.Namespace)
                .WithHeaderComment(CommentHeaderString)
                .Public()                
                .WithProperty(Property.Named("Where")
                    .WithType(parmsType.Name)
                    .Public()
                    .WithAttribute("DataMember")
                ).BuildCompilationUnit();
        }

        public CompilationUnitSyntax GenerateQueryParametersClass(TypeModel resultType)
        {
            return Class.Named(resultType.Name)
                .InNamespace(resultType.Namespace)
                .WithAttribute("DataContract")
                .WithUsings(BasNamespaces.CqrsBase, "System.Runtime.Serialization")
                .WithHeaderComment(CommentHeaderString)
                .Public()
                .BuildCompilationUnit();
        }

        public MethodDeclarationSyntax GenerateQueryServiceClassMethod(string methodName, TypeModel queryType)
        {
            return Method.Named(methodName)
                .Public()
                .WithReturnType(queryType.Name)
                .WithParameter("query", queryType.Name)
                .WithBodyStatement($"return this.TryRoute(query);")
                .BuildClassMethod()
                .NormalizeWhitespace()
                .WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed);
        }

        public MethodDeclarationSyntax GenerateQueryServiceInterfaceMethod(string methodName, TypeModel queryType)
        {
            return Method.Named(methodName)
                 .WithReturnType(queryType.Name)
                 .WithAttribute("OperationContract")
                 .WithAttribute("FaultContract", "typeof(ServiceException)")
                 .WithParameter("query", queryType.Name)
                 .BuildInterfaceMethod()
                 .NormalizeWhitespace()
                 .WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed);
        }

        public CompilationUnitSyntax GenerateQueryExecutorClass(TypeModel executorType, TypeModel inputType, TypeModel queryType, TypeModel parmsType)
        {
            return Class.Named(executorType.Name)
                .Public()
                .InNamespace(executorType.Namespace)
                .WithUsings("System", "System.Linq", inputType.Namespace, queryType.Namespace, parmsType.Namespace, BasNamespaces.CqrsBase, BasNamespaces.CqrsInterfaces)
                .WithHeaderComment(CommentHeaderString)
                .WithBaseClass($"QueryExecutorBase<{inputType.Name}, {queryType.Name}>")
                .WithMethod(
                    Method.Named("ApplySorting")
                        .Protected()
                        .Override()
                        .WithParameter("baseQuery", $"IQueryable<{inputType.Name}>")
                        .WithParameter("query", queryType.Name)
                        .WithBodyStatement("//TODO: implement sorting logic here")
                        .WithBodyStatement("throw new NotImplementedException();")
                        .WithReturnType($"IQueryable<{inputType.Name}>"))
                .WithMethod(
                    Method.Named("ApplyWhereclause")
                        .Protected()
                        .Override()
                        .WithParameter("baseQuery", $"IQueryable<{inputType.Name}>")
                        .WithParameter("query", queryType.Name)
                        .WithBodyStatement("//TODO: implement where clause logic here")
                        .WithBodyStatement("throw new NotImplementedException();")
                        .WithReturnType($"IQueryable<{inputType.Name}>"))
                .WithMethod(
                    Method.Named("AssignResults")
                        .Protected()
                        .Override()
                        .WithParameter("baseQuery", $"IQueryable<{inputType.Name}>")
                        .WithParameter("query", queryType.Name)
                        .WithBodyStatement("//TODO: implement actual results assignment logic here")
                        .WithBodyStatement("throw new NotImplementedException();")
                        .WithReturnType("void"))
                .WithMethod(
                    Method.Named("GetBaseQuery")
                        .Protected()
                        .Override()
                        .WithParameter("dataContext", "IDbContextBase")
                        .WithParameter("query", queryType.Name)
                        .WithBodyStatement("//TODO: implement actual base query logic here")
                        .WithBodyStatement("throw new NotImplementedException();")
                        .WithReturnType($"IQueryable<{inputType.Name}>")
                ).BuildCompilationUnit();
        }

        public CompilationUnitSyntax GenerateQueryResultClass(TypeModel resultType)
        {
            return Class.Named(resultType.Name)
                .InNamespace(resultType.Namespace)
                .WithAttribute("DataContract")
                .WithUsings("HP.HSP.UA3.Core.BAS.CQRS.Base", "System.Runtime.Serialization")
                .WithHeaderComment(CommentHeaderString)
                .Public()
                .BuildCompilationUnit();
        }

        public CompilationUnitSyntax GenerateQueryExecutorTestClass(TypeModel testType, TypeModel executorType, TypeModel queryType)
        {
            return Class.Named($"{executorType.Name}Tests")
                .InNamespace(testType.Namespace)
                .WithUsings("System", executorType.Namespace, BasNamespaces.CqrsBase, "Microsoft.VisualStudio.TestTools.UnitTesting", queryType.Namespace)
                .WithHeaderComment(CommentHeaderString)
                .WithAttribute("TestClass")
                .Public()
                .WithMethod(
                    Method.Named($"{executorType.Name}_Handle_should_handle_{queryType.Name}")
                    .Public()
                    .WithReturnType("void")
                    .WithAttribute("TestMethod")
                    .WithBodyStatement("//TODO: Add real query executor test code here")
                    .WithBodyStatement(@"Assert.Fail(""This test is not yet implemented"");")
                ).BuildCompilationUnit();
        }

        public static string CommentHeaderString
        {
            get
            {
                string year = DateTime.Now.Year.ToString();

                return $@"//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) {year}. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------";
            }
        }
    }
}