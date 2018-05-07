//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;

namespace UA3.CodeFactory.Core.CodeAnalysis
{
    public class ProjectModel
    {
        public ProjectModel()
        {
        }

        public ProjectId Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

        public static ProjectModel From(Project project)
        {
            return new ProjectModel()
            {
                Id = project.Id,
                Name = project.Name
            };
        }
    }
}
