//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
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
    public static class SolutionExtensions
    {
        public static IEnumerable<Document> FindDocuments(this Solution solution, Func<Document, bool> criteria)
        {
            foreach (Project p in solution.Projects)
            {
                foreach (Document d in p.Documents)
                {
                    if (criteria(d))
                    {
                        yield return d;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        public static Document FindDocumentWithFilePath(this Solution solution, string filePath)
        {
            return solution.FindDocuments(doc => doc.FilePath.Equals(filePath, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }

        public static IQueryable<Document> AllDocuments(this Solution solution)
        {
            return (from p in solution.Projects
                    from d in p.Documents
                    select d).AsQueryable();
        }

        public static IQueryable<Document> AllClassDocuments(this Solution solution)
        {
            return (from p in solution.Projects
                    from d in p.Documents
                    let cls = d.GetClass()
                    where cls != null
                    select d).AsQueryable();
        }

        public static List<ClassModel> GetProjectionWriterClasses(this Solution solution)
        {
            var query =
                from doc in solution.AllDocuments()
                let cls = doc.GetClass()
                where cls != null && cls.BaseList != null &&
                cls.BaseList.Types.Any(p => p.Type.ToString().ToLowerInvariant().Contains("projectionwriterbase"))
                select new ClassModel(cls.Identifier.ToString(), doc.Id);

            return query.ToList();
        }

        public static List<ClassModel> GetCommandServiceClasses(this Solution solution)
        {
            var query =
                from c in
                (from doc in solution.AllDocuments()
                 let cls = doc.GetClass()
                 where cls != null && cls.InheritsFrom("CommandServiceBase")
                 select new
                 {
                     ClassDocumentId = doc.Id,
                     ClassTypeName = cls.Identifier.ToString(),
                     ExpectedInterfaceName = "I" + cls.Identifier.ToString()
                 }
                )
                join i in
                (
                    from doc in solution.AllDocuments()
                    let iface = doc.GetInterface()
                    where iface != null
                    select new
                    {
                        InterfaceDocumentId = doc.Id,
                        InterfaceTypeName = iface.Identifier.ToString()
                    }
                )
                on c.ExpectedInterfaceName equals i.InterfaceTypeName into joined
                from ci in joined.DefaultIfEmpty()
                select new ClassModel(c.ClassTypeName, c.ClassDocumentId)
                {
                    InterfaceDocumentId = ci == null ? null : ci.InterfaceDocumentId,
                    InterfaceTypeName = ci == null ? null : ci.InterfaceTypeName
                };

            return query.ToList();
        }

        public static List<ClassModel> GetQueryServiceClasses(this Solution solution)
        {
            var query =
                from c in
                (from doc in solution.AllDocuments()
                 let cls = doc.GetClass()
                 where cls != null && cls.InheritsFrom("QueryServiceBase")
                 select new
                 {
                     ClassDocumentId = doc.Id,
                     ClassTypeName = cls.Identifier.ToString(),
                     ExpectedInterfaceName = "I" + cls.Identifier.ToString()
                 }
                )
                join i in
                (
                    from doc in solution.AllDocuments()
                    let iface = doc.GetInterface()
                    where iface != null
                    select new
                    {
                        InterfaceDocumentId = doc.Id,
                        InterfaceTypeName = iface.Identifier.ToString()
                    }
                )
                on c.ExpectedInterfaceName equals i.InterfaceTypeName into joined
                from ci in joined.DefaultIfEmpty()
                select new ClassModel(c.ClassTypeName, c.ClassDocumentId)
                {
                    InterfaceDocumentId = ci == null ? null : ci.InterfaceDocumentId,
                    InterfaceTypeName = ci == null ? null : ci.InterfaceTypeName
                };

            return query.ToList();
        }
    }
}
