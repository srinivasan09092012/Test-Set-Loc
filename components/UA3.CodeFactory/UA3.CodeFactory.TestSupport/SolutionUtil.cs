//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System.IO;
using UA3.CodeFactory.Core.CodeAnalysis;
using System;

namespace UA3.CodeFactory.TestSupport
{
    public static class SolutionUtil
    {
        public static SolutionSummary CreateTypicalBASSolution(string baseDirectory)
        {
            SolutionSummary result = null;

            AdhocWorkspace workspace = new AdhocWorkspace();

            string solutionName = "HP.HSP.UA3.TestModule.BAS.TypicalBas";
            string solutionPath = Path.Combine(baseDirectory, solutionName + ".sln");

            var solution = workspace.AddSolution(
                SolutionInfo.Create(SolutionId.CreateNewId(), VersionStamp.Create(), solutionPath));

            result = new SolutionSummary(workspace, solutionName, baseDirectory);

            solution = AddServicesProject(result, solution);

            solution = AddBusinessProject(result, solution);

            solution = AddContractsProject(result, solution);

            solution = AddDataAccessProject(result, solution);

            bool applied = result.UpdateWorkspace(solution);

            return result;
        }

        private static Solution AddProjectToSolution(Solution solution, string baseDirectory, string suffix, out ProjectId id)
        {
            id = ProjectId.CreateNewId();

            string actualSuffix = string.IsNullOrWhiteSpace(suffix) ? string.Empty : "." + suffix;

            string projectName = Path.GetFileNameWithoutExtension(solution.FilePath) + actualSuffix;

            string projectPath = Path.Combine(baseDirectory, projectName + ".csproj");

            return solution.AddProject(
                ProjectInfo.Create(id, VersionStamp.Create(), projectName, projectName, "C#", projectPath));
        }

        private static Solution AddDataAccessProject(SolutionSummary summary, Solution currentSolution)
        {
            ProjectId id = null;
            ProjectId testId = null;

            var updatedSolution = AddProjectToSolution(currentSolution, summary.BaseDirectory, "DataAccess", out id);

            var result = AddProjectToSolution(updatedSolution, summary.BaseDirectory, "DataAccess.Tests", out testId);

            summary.Projects.DataAccess = new ProjectModel()
            {
                Id = id,
                Name = result.GetProject(id).Name
            };

            summary.Projects.DataAccessTests = new ProjectModel()
            {
                Id = testId,
                Name = result.GetProject(testId).Name
            };

            result = AddDataAccessClassesToProject(result.GetProject(id));

            return result;
        }

        private static Solution AddContractsProject(SolutionSummary summary, Solution currentSolution)
        {
            ProjectId id = null;
            ProjectId testId = null;

            var updatedSolution = AddProjectToSolution(currentSolution, summary.BaseDirectory, "Contracts", out id);

            var result = AddProjectToSolution(updatedSolution, summary.BaseDirectory, "Contracts.Tests", out testId);

            summary.Projects.Contracts = new ProjectModel()
            {
                Id = id,
                Name = result.GetProject(id).Name
            };

            summary.Projects.ContractsTests = new ProjectModel()
            {
                Id = testId,
                Name = result.GetProject(testId).Name
            };

            return result;
        }

        private static Solution AddBusinessProject(SolutionSummary summary, Solution currentSolution)
        {
            ProjectId id = null;
            ProjectId testId = null;

            var updatedSolution = AddProjectToSolution(currentSolution, summary.BaseDirectory, "Business", out id);

            var result = AddProjectToSolution(updatedSolution, summary.BaseDirectory, "Business.Tests", out testId);

            summary.Projects.Business = new ProjectModel()
            {
                Id = id,
                Name = result.GetProject(id).Name
            };

            summary.Projects.BusinessTests = new ProjectModel()
            {
                Id = testId,
                Name = result.GetProject(testId).Name
            };

            return result;
        }

        private static Solution AddServicesProject(SolutionSummary summary, Solution currentSolution)
        {
            ProjectId id = null;
            ProjectId testId = null;

            var updatedSolution = AddProjectToSolution(currentSolution, summary.BaseDirectory, string.Empty, out id);

            updatedSolution = AddServicesProjectDocuments(updatedSolution.GetProject(id));

            var result = AddProjectToSolution(updatedSolution, summary.BaseDirectory, "Tests", out testId);

            summary.Projects.Services = new ProjectModel()
            {
                Id = id,
                Name = summary.SolutionName
            };

            summary.Projects.ServicesTests = new ProjectModel()
            {
                Id = testId,
                Name = summary.SolutionName
            };

            return result;
        }

        private static Solution AddServicesProjectDocuments(Project proj)
        {
            var cmdServiceClassTxt = $@"using System;
namespace {proj.Name} {{
    public class TypicalCommandService : CommandServiceBase
    {{
    }}
}}";
            var cmdServiceDoc = proj.AddDocument("TypicalCommandService.cs", cmdServiceClassTxt);

            var cmdServiceInterfaceTxt = $@"using System;
namespace {proj.Name} {{
    public interface ITypicalCommandService
    {{
    }}
}}";
            var cmdInterfaceDoc = cmdServiceDoc.Project.AddDocument("ITypicalCommandService.cs", cmdServiceInterfaceTxt);

            var queryServiceTxt = $@"using System;
namespace {proj.Name} {{
    public class TypicalQueryService : QueryServiceBase
    {{
    }}
}}";
            var queryServiceDoc = cmdInterfaceDoc.Project.AddDocument("TypicalQueryService.cs", queryServiceTxt);

            var queryInterfaceTxt = $@"@using System;
namespace {proj.Name} {{
    public interface ITypicalQueryService
    {{
    }}
}}";
            var queryInterfaceDoc = queryServiceDoc.Project.AddDocument("ITypicalQueryService.cs", queryInterfaceTxt);

            return queryInterfaceDoc.Project.Solution;
        }

        private static Solution AddDataAccessClassesToProject(Project project)
        {
            var contextClassText = $@"using System;
namespace {project.Name} {{
    public class TestDbContext : DbContextBase
    {{
        public virtual IDbSet<TestEntity> TestEntities {{ get; set; }}
    }}
}}";
            var contextDoc = project.AddDocument("TestDbContext.cs", contextClassText);

            var entityText = $@"using System;
namespace {project.Name} {{
    public class TestEntity
    {{
        public int Id {{ get; set; }}
    }}
}}";
            var entityDoc = contextDoc.Project.AddDocument("TestEntity.cs", entityText);

            var projectionWriterText = $@"using System;
namespace {project.Name} {{
    public class TestProjectionWriter : ProjectionWriterBase<TestEntity>, IHandleCommand<TestEvent>
    {{
        public void Handle(TestEvent eventInstance)
        {{
        }}
    }}
}}";
            var projectionWriterDoc = entityDoc.Project.AddDocument("TestProjectionWriter.cs", projectionWriterText);

            return projectionWriterDoc.Project.Solution;
        }
    }
}
