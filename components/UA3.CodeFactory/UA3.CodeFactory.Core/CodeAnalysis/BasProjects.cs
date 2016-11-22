using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UA3.CodeFactory.Core.CodeAnalysis
{
    public class BasProjects
    {
        private BasProjects()
        {
        }

        public Project BusinessProject { get; private set; }

        public Project BusinessTestProject { get; private set; }

        public Project ServicesProject { get; private set; }

        public Project ServicesTestProject { get; private set; }

        public Project DataAccessProject { get; private set; }

        public Project DataAccessTestProject { get; private set; }

        public Project ContractsProject { get; private set; }

        public Project ContractsTestProject { get; private set; }

        public static BasProjects CreateFrom(Solution solution, string solutionName = null)
        {
            BasConventions conventions = BasConventions.CreateFrom(solution, solutionName);

            return new BasProjects()
            {
                BusinessProject = solution.GetProject(conventions.BusinessProject.Id),
                BusinessTestProject = solution.GetProject(conventions.BusinessTestProject.Id),
                ServicesProject = solution.GetProject(conventions.ServicesProject.Id),
                ServicesTestProject = solution.GetProject(conventions.ServicesTestProject.Id),
                DataAccessProject = solution.GetProject(conventions.DataAccessProject.Id),
                DataAccessTestProject = solution.GetProject(conventions.DataAccessTestProject.Id),
                ContractsProject = solution.GetProject(conventions.ContractsProject.Id),
                ContractsTestProject = solution.GetProject(conventions.ContractsTestProject.Id)
            };
        }
    }
}
