//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UA3.CodeFactory.Core.CodeAnalysis;

namespace UA3.CodeFactory.Tests
{
    [TestClass]
    public class RoslynSpikeTests
    {
        [TestMethod]
        [TestCategory("IgnoreOnBuild")]
        public void Roslyn_spike()
        {
            AdhocWorkspace ws = new AdhocWorkspace();
            Solution sln = ws.AddSolution(SolutionInfo.Create(SolutionId.CreateNewId(), VersionStamp.Create()));

            BasCodeGenerator generator = new BasCodeGenerator();

            TypeModel queryType = new TypeModel("QueryType", "QueryNamespace");
            TypeModel resultType = new TypeModel("QueryResultType", "QueryResultNamespace");
            TypeModel parmsType = new TypeModel("QueryParmsType", "QueryParmsTypeNamespace");

            var output = generator.GenerateQueryClass(queryType, parmsType, resultType);

            Console.WriteLine(output.ToFullString());

        }
    }
}