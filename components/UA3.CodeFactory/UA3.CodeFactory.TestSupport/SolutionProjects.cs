//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using UA3.CodeFactory.Core.CodeAnalysis;

namespace UA3.CodeFactory.TestSupport
{
    public class SolutionProjects
    {
        public ProjectModel Business { get; set; }

        public ProjectModel BusinessTests { get; set; }

        public ProjectModel Contracts { get; set; }

        public ProjectModel ContractsTests { get; set; }

        public ProjectModel Services { get; set; }

        public ProjectModel ServicesTests { get; set; }

        public ProjectModel DataAccess { get; set; }

        public ProjectModel DataAccessTests { get; set; }
    }
}