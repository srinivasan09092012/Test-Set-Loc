//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;

namespace UA3.CodeFactory.TestSupport
{
    public class SolutionSummary
    {
        public SolutionSummary(AdhocWorkspace workspace, string solutionName, string baseDirectory)
        {
            this.Projects = new SolutionProjects();
            this.Workspace = workspace;
            this.SolutionName = solutionName;
            this.BaseDirectory = baseDirectory;
        }

        public SolutionProjects Projects { get; private set; }

        public AdhocWorkspace Workspace { get; private set; }

        public Solution Solution
        {
            get
            {
                if (this.Workspace != null && this.Workspace.CurrentSolution != null)
                {
                    return this.Workspace.CurrentSolution;
                }

                return null;
            }
        }

        public string BaseDirectory { get; private set; }

        public string SolutionName { get; private set; }

        public bool UpdateWorkspace(Solution newSolution)
        {
            return this.Workspace.TryApplyChanges(newSolution);
        }
    }
}