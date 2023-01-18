//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using GalaSoft.MvvmLight.Ioc;
using System.Collections.Generic;
using UA3.CodeFactory.Core.Services;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace UA3.CodeFactory.Core.CodeAnalysis.ItemSources
{
    public abstract class AggregateItemsSource : IItemsSource
    {
        public AggregateItemsSource()
        {
            this.SolutionProvider = SimpleIoc.Default.GetInstance<ISolutionProvider>();
        }

        public AggregateItemsSource(ISolutionProvider solutionProvider)
        {
            this.SolutionProvider = solutionProvider;
        }

        protected ISolutionProvider SolutionProvider { get; private set; }

        protected abstract IEnumerable<SolutionBasedItemSource> GetSources();

        public ItemCollection GetValues()
        {
            var sources = this.GetSources();

            ItemCollection result = new ItemCollection();

            foreach (SolutionBasedItemSource src in sources)
            {
                result.AddRange(src.GetValues());
            }

            return result;
        }
    }
}
