//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using UA3.CodeFactory.Core.CodeAnalysis;
using UA3.CodeFactory.Plugins.AddServiceCommand;
using UA3.CodeFactory.TestSupport;

namespace UA3.CodeFactory.Plugins.Tests.AddServiceCommand
{
    [TestClass]
    public class AddCommandOperationTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void AddCommandOperation_should_add_method_to_CommandServiceBase_class()
        {
            var summary = SolutionUtil.CreateTypicalBASSolution(this.TestContext.DeploymentDirectory);

            var settings = new AddServiceCommandSettings();
            settings.CommandHandlersProject = summary.Projects.Business;
            settings.CommandTypeName = "NewCommand";
            settings.ContractsProject = summary.Projects.Contracts;
            settings.EventTypeName = "NewEvent";
            settings.NewProjectionWriterClassName = "NewProjectionWriter";
            settings.ProjectionWriterOption = ProjectionWriterOptions.CreateNew;
            settings.ServiceOperationName = "NewOperation";
            settings.CommandService = new ClassModel("TypicalCommandService",
                summary.Solution.GetProject(summary.Projects.Services.Id).Documents.FirstOrDefault(p => p.Name == "TypicalCommandService.cs").Id)
            {
                InterfaceDocumentId = summary.Solution.GetProject(summary.Projects.Services.Id).Documents.FirstOrDefault(p => p.Name == "ITypicalCommandService.cs").Id
            };
            settings.NewProjectionWriterEntityClass = new ClassModel("Provider", null);

            var context = new SolutionContext(summary.Solution);
            var op = new AddServiceCommandTransform(settings);

            op.Execute(context);

            var allDocs = context.CurrentSolution.Projects.SelectMany(p => p.Documents).ToList();

            allDocs.ForEach(doc =>
            {
                Console.WriteLine("".PadRight(32, '*'));
                Console.WriteLine(doc.GetTextAsync().Result);
                Console.WriteLine("".PadRight(32, '*'));
            });
        }
    }
}