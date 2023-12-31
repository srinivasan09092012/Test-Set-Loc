//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System;
using System.Linq;
using UA3.CodeFactory.Core.Services;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace UA3.CodeFactory.Core.CodeAnalysis.ItemSources
{
    public class CommandHandlersProjectItemsSource : SolutionBasedItemSource
    {
        public CommandHandlersProjectItemsSource(ISolutionProvider solutionProvider)
            : base(solutionProvider)
        {
        }

        public CommandHandlersProjectItemsSource()
            : base()
        {
        }

        protected override ItemCollection GetItems(Solution solution)
        {
            ItemCollection result = new ItemCollection();
            BasConventions conventions = BasConventions.CreateFrom(solution);
            BasProjects projects = BasProjects.CreateFrom(solution);

            if (projects.BusinessProject != null)
            {
                result.Add(ProjectModel.From(projects.BusinessProject));
            }
            else
            {
                var allProjects = solution.Projects.Select(p => ProjectModel.From(p)).ToList();
                allProjects.ForEach(p => result.Add(p));
            }

            return result;
        }
    }
}
