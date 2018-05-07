//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace UA3.CodeFactory.Core.CodeAnalysis
{
    public interface IBasCodeGenerator
    {
        CompilationUnitSyntax GenerateCommandClass(TypeModel classType);

        CompilationUnitSyntax GenerateCommandHandlerClass(TypeModel handlerType, TypeModel commandType);

        CompilationUnitSyntax GenerateCommandHandlerTestClass(TypeModel testType, TypeModel handlerType, TypeModel commandType);

        MethodDeclarationSyntax GenerateCommandServiceClassMethod(string methodName, TypeModel commandType, TypeModel eventType);

        MethodDeclarationSyntax GenerateCommandServiceInterfaceMethod(string methodName, TypeModel commandType, TypeModel eventType);

        SyntaxTrivia GenerateCommentHeader();

        CompilationUnitSyntax GenerateEventClass(TypeModel eventType);

        CompilationUnitSyntax GenerateProjectionWriterClass(TypeModel projectionWriterType, TypeModel eventType, TypeModel entityType);

        MethodDeclarationSyntax GenerateProjectionWriterMethod(TypeModel eventType);

        CompilationUnitSyntax GenerateProjectionWriterTestClass(TypeModel testType, TypeModel projectionWriterType, TypeModel eventType);

        MethodDeclarationSyntax GenerateProjectionWriterTestMethod(TypeModel testType, TypeModel eventType);

        CompilationUnitSyntax GenerateQueryClass(TypeModel queryType, TypeModel parmsType, TypeModel queryResultType);

        CompilationUnitSyntax GenerateQueryParametersClass(TypeModel parametersType);

        CompilationUnitSyntax GenerateQueryExecutorTestClass(TypeModel testType, TypeModel executorType, TypeModel queryType);

        CompilationUnitSyntax GenerateQueryResultClass(TypeModel resultType);

        MethodDeclarationSyntax GenerateQueryServiceClassMethod(string methodName, TypeModel queryType);

        MethodDeclarationSyntax GenerateQueryServiceInterfaceMethod(string methodName, TypeModel queryType);

        CompilationUnitSyntax GenerateQueryExecutorClass(TypeModel executorType, TypeModel inputType, TypeModel queryType, TypeModel parmsType);
    }
}
