//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using UA3.CodeFactory.Core.Services;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace UA3.CodeFactory.Core.CodeAnalysis.ItemSources
{
    public class ProjectionWriterClassItemSource : SolutionBasedItemSource
    {
        public ProjectionWriterClassItemSource(ISolutionProvider solutionProvider)
            : base(solutionProvider)
        {
        }

        public ProjectionWriterClassItemSource()
            : base()
        {
        }

        protected override ItemCollection GetItems(Solution solution)
        {
            var items = solution.GetProjectionWriterClasses();

            var result = new ItemCollection();

            items.ForEach(p => result.Add(p));

            return result;
        }
    }
}