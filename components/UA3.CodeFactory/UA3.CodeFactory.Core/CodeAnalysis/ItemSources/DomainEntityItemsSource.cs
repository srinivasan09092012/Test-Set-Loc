//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System.Linq;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using UA3.CodeFactory.Core.Services;
using UA3.CodeFactory.Core.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace UA3.CodeFactory.Core.CodeAnalysis.ItemSources
{
    public class DomainEntityItemsSource : SolutionBasedItemSource
    {
        public DomainEntityItemsSource(ISolutionProvider solutionProvider) 
            : base(solutionProvider)
        {
        }

        protected override ItemCollection GetItems(Solution solution)
        {
            BasConventions conventions = BasConventions.CreateFrom(solution);

            ItemCollection result = new ItemCollection();

            var query = (from p in solution.AllClassDocuments()
                         let cls = p.GetClass()
                         let ns = cls.Ancestors(false).OfType<NamespaceDeclarationSyntax>().FirstOrDefault()
                         where ns != null && ns.Name.ToString() == conventions.QueryEntityTypeNamespace
                         select new ClassModel(cls.Identifier.ToString(), p.Id)).ToList();

            query.ForEach(p => result.Add(p));

            return result;
        }
    }
}
