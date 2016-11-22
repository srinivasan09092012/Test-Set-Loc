//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using UA3.CodeFactory.Core.Services;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace UA3.CodeFactory.Core.CodeAnalysis.ItemSources
{
    public class EntityClassItemsSource : SolutionBasedItemSource
    {
        public EntityClassItemsSource(ISolutionProvider solutionProvider)
            : base(solutionProvider)
        {
        }

        public EntityClassItemsSource()
            : base()
        {
        }

        protected override ItemCollection GetItems(Solution solution)
        {
            ItemCollection result = new ItemCollection();

            var contextTypes =
                (
            from doc in solution.AllDocuments()
            let cls = doc.GetClass()
            where cls != null &&
            (cls.InheritsFrom("DbContext") || cls.InheritsFrom("DbContextBase"))
            select new
            {
                ClassDeclaration = cls,
                DocumentId = doc.Id
            }).ToList();

            foreach (var c in contextTypes)
            {
                string contextTypeName = c.ClassDeclaration.Identifier.ToString();
                List<string> setTypeNames = new List<string>();

                var setProperties = (from prop in c.ClassDeclaration.DescendantNodes().OfType<PropertyDeclarationSyntax>()
                                     where prop.Type.ToFullString().Contains("DbSet<")
                                     select prop).ToList();

                foreach (var setProp in setProperties)
                {
                    string entityTypeName = (setProp.Type as GenericNameSyntax).TypeArgumentList.Arguments[0].ToFullString();
                    setTypeNames.Add(entityTypeName);
                }

                var toAdd = (from p in solution.AllDocuments()
                             let cls = p.GetClass()
                             where cls != null && setTypeNames.Contains(cls.Identifier.ToString())
                             select new ClassModel(cls.Identifier.ToString(), p.Id)).ToList();

                toAdd.ForEach(p => result.Add(p));
            }

            return result;
        }
    }
}