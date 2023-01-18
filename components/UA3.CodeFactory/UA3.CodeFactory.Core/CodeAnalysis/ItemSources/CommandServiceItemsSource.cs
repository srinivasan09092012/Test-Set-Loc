//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using UA3.CodeFactory.Core.Services;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace UA3.CodeFactory.Core.CodeAnalysis.ItemSources
{
    public class CommandServiceItemsSource : SolutionBasedItemSource
    {
        public CommandServiceItemsSource(ISolutionProvider solutionProvider)
            : base(solutionProvider)
        {
        }

        public CommandServiceItemsSource()
            : base()
        {
        }

        protected override ItemCollection GetItems(Solution solution)
        {
            var items = solution.GetCommandServiceClasses();

            var result = new ItemCollection();

            items.ForEach(p => result.Add(p));

            return result;
        }
    }
}
