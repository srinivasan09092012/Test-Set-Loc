//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using GalaSoft.MvvmLight.Ioc;
using Microsoft.CodeAnalysis;
using UA3.CodeFactory.Core.Services;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace UA3.CodeFactory.Core.CodeAnalysis.ItemSources
{
    public abstract class SolutionBasedItemSource : IItemsSource
    {
        public SolutionBasedItemSource(ISolutionProvider solutionProvider)
        {
            this.SolutionProvider = solutionProvider;
        }

        public SolutionBasedItemSource()
        {
            this.SolutionProvider = SimpleIoc.Default.GetInstance<ISolutionProvider>();
        }

        protected ISolutionProvider SolutionProvider { get; private set; }

        public ItemCollection GetValues()
        {
            var solution = this.SolutionProvider.GetSolution();
            return this.GetItems(solution);
        }

        protected abstract ItemCollection GetItems(Solution solution);
    }
}
