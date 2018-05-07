//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System.IO;
using System.Linq;

namespace UA3.CodeFactory.Core.CodeAnalysis
{
    public class BasConventions
    {
        private BasConventions()
        {
        }

        public ProjectModel BusinessProject { get; private set; }

        public ProjectModel BusinessTestProject { get; private set; }

        public ProjectModel ServicesProject { get; private set; }

        public ProjectModel ServicesTestProject { get; private set; }

        public ProjectModel DataAccessProject { get; private set; }

        public ProjectModel DataAccessTestProject { get; private set; }

        public ProjectModel ContractsProject { get; private set; }

        public ProjectModel ContractsTestProject { get; private set; }

        public ProjectItemLocation EventClassLocation { get; private set; }

        public ProjectItemLocation CommandClassLocation { get; private set; }

        public ProjectItemLocation ProjectionWriterClassLocation { get; private set; }

        public ProjectItemLocation CommandHandlerClassLocation { get; private set; }

        public ProjectItemLocation CommandHandlerTestClassLocation { get; private set; }

        public ProjectItemLocation ProjectionWriterTestClassLocation { get; private set; }

        public ProjectItemLocation QueryClassLocation { get; private set; }

        public ProjectItemLocation QueryParmsClassLocation { get; private set; }

        public ProjectItemLocation QueryExecutorClassLocation { get; private set; }

        public ProjectItemLocation QueryResultClassLocation { get; private set; }

        public ProjectItemLocation QueryEntityClassLocation { get; private set; }

        public ProjectItemLocation QueryExecutorTestClassLocation { get; private set; }

        public string EventClassNamespace { get; private set; }

        public string CommandClassNamespace { get; private set; }

        public string ProjectionWriterNamespace { get; private set; }

        public string CommandHandlerClassNamespace { get; private set; }

        public string EntityClassNamespace { get; private set; }

        public string QueryClassNamespace { get; private set; }

        public string QueryParmsClassNamespace { get; private set; }

        public string QueryExecutorClassNamespace { get; private set; }

        public string QueryResultClassNamespace { get; private set; }

        public string QueryEntityTypeNamespace { get; private set; }

        public string QueryExecutorTestClassNamespace { get; private set; }

        internal BasConventions BuildNamespaces()
        {
            this.EventClassNamespace = $"{this.ContractsProject.Name}.Events";
            this.CommandClassNamespace = $"{this.ContractsProject.Name}.Commands";
            this.EntityClassNamespace = $"{this.DataAccessProject.Name}.Entities";
            this.ProjectionWriterNamespace = $"{this.DataAccessProject.Name}.EventHandlers";
            this.CommandHandlerClassNamespace = $"{this.BusinessProject.Name}.CommandHandlers";
            this.QueryClassNamespace = $"{this.ContractsProject.Name}.Queries";
            this.QueryParmsClassNamespace = $"{this.ContractsProject.Name}.Queries.Parms";
            this.QueryExecutorClassNamespace = $"{this.DataAccessProject.Name}.QueryHandlers";
            this.QueryResultClassNamespace = $"{this.ContractsProject.Name}.ViewDTO";
            this.QueryEntityTypeNamespace = $"{this.ContractsProject.Name}.Domain";
            this.QueryExecutorTestClassNamespace = $"{this.DataAccessTestProject.Name}.QueryHandlers";

            return this;
        }

        internal BasConventions BuildLocations(Solution solution)
        {
            this.EventClassLocation = new ProjectItemLocation(this.ContractsProject.Id, "Events");
            this.CommandClassLocation = new ProjectItemLocation(this.ContractsProject.Id, "Commands");
            this.ProjectionWriterClassLocation = new ProjectItemLocation(this.DataAccessProject.Id, "EventHandlers");
            this.CommandHandlerClassLocation = new ProjectItemLocation(this.BusinessProject.Id, "CommandHandlers");
            this.CommandHandlerTestClassLocation = new ProjectItemLocation(this.BusinessTestProject.Id, "CommandHandlers");
            this.ProjectionWriterTestClassLocation = new ProjectItemLocation(this.DataAccessTestProject.Id, "EventHandlers");
            this.QueryClassLocation = new ProjectItemLocation(this.ContractsProject.Id, "Queries");
            this.QueryParmsClassLocation = new ProjectItemLocation(this.ContractsProject.Id, "Queries", "Parms");
            this.QueryExecutorClassLocation = new ProjectItemLocation(this.DataAccessProject.Id, "QueryHandlers");
            this.QueryResultClassLocation = new ProjectItemLocation(this.ContractsProject.Id, "ViewDTO");
            this.QueryEntityClassLocation = new ProjectItemLocation(this.ContractsProject.Id, "Domain");
            this.QueryExecutorTestClassLocation = new ProjectItemLocation(this.DataAccessTestProject.Id, "QueryHandlers");

            return this;
        }

        public static BasConventions CreateFrom(Solution solution, string solutionName = null)
        {
            string slnName = solutionName ?? Path.GetFileNameWithoutExtension(solution.FilePath);

            return new BasConventions()
            {
                BusinessProject = GetProject(solution, slnName, "Business"),
                BusinessTestProject = GetProject(solution, slnName, "Business.Tests"),
                ContractsProject = GetProject(solution, slnName, "Contracts"),
                ContractsTestProject = GetProject(solution, slnName, "Contracts.Tests"),
                DataAccessProject = GetProject(solution, slnName, "DataAccess"),
                DataAccessTestProject = GetProject(solution, slnName, "DataAccess.Tests"),
                ServicesProject = GetProject(solution, slnName, string.Empty),
                ServicesTestProject = GetProject(solution, slnName, "Tests"),
            }.BuildLocations(solution).BuildNamespaces();
        }

        private static ProjectModel GetProject(Solution solution, string solutionName, string suffix)
        {
            string projName = string.IsNullOrWhiteSpace(suffix) ? solutionName : $"{solutionName}.{suffix}";            

            var proj = solution.Projects.FirstOrDefault(p => p.Name == projName);

            if (proj != null)
            {
                return new ProjectModel()
                {
                    Id = proj.Id,
                    Name = proj.Name
                };
            }            

            return null;
        }
    }
}
