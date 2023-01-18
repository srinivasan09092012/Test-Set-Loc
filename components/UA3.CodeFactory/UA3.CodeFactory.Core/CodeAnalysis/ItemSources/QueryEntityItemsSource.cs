//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Collections.Generic;
using UA3.CodeFactory.Core.Services;

namespace UA3.CodeFactory.Core.CodeAnalysis.ItemSources
{
    public class QueryEntityItemsSource : AggregateItemsSource
    {
        public QueryEntityItemsSource(ISolutionProvider solutionProvider)
            : base(solutionProvider)
        {
        }

        public QueryEntityItemsSource()
            : base()
        {
        }

        protected override IEnumerable<SolutionBasedItemSource> GetSources()
        {
            return new SolutionBasedItemSource[]
            {
                new EntityClassItemsSource(this.SolutionProvider),
                new DomainEntityItemsSource(this.SolutionProvider)
            };
        }
    }
}
