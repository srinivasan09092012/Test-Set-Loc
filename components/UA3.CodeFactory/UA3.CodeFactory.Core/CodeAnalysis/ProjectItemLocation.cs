//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UA3.CodeFactory.Core.CodeAnalysis
{
    public class ProjectItemLocation
    {
        public ProjectItemLocation(ProjectId project, params string[] folders)
        {
            this.Project = project;
            this.Folders = new List<string>();

            if (folders != null && folders.Any())
            {
                this.Folders.AddRange(folders);
            }
        }

        public ProjectItemLocation(ProjectId project)
            : this(project, Array.Empty<string>())
        {
        }

        public ProjectId Project { get; set; }

        public List<string> Folders { get; set; }
    }
}
