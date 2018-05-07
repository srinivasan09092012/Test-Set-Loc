//-----------------------------------------------------------------------------------------
// This code is the property of DXC Technology, Copyright (c) 2018. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using MainEvent.Core.Model;
using MainEvent.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MainEvent.Tests.Core.Services
{
    [TestClass]
    public class DependencyWalkerTests
    {
        [TestMethod]
        public void DependencyWalker_should_sort_by_dependency_order()
        {
            List<TableSchemaModel> tables = new List<TableSchemaModel>();

            var primaryTable = new TableSchemaModel("primary table");

            var dep1 = new TableSchemaModel("dependent table 1");
            dep1.Dependencies.Add("primary table");

            var dep2 = new TableSchemaModel("dependent table 2");
            dep2.Dependencies.Add("dependent table 1");

            tables.Add(primaryTable);
            tables.Add(dep1);
            tables.Add(dep2);

            for (int i = 0; i < 10; i++)
            {
                tables = tables.OrderBy(p => Guid.NewGuid()).ToList();
                var inst = new DependencyWalker();
                var sorted = inst.SortByChildDependencies(tables);

                Assert.IsNotNull(sorted);
                Assert.AreEqual(tables.Count, sorted.Count);
                Assert.AreEqual(dep1.Name, sorted[1].Name);
                Assert.AreEqual(primaryTable.Name, sorted[0].Name);
                Assert.AreEqual(dep2.Name, sorted[2].Name);
            }
        }

        [TestMethod]
        public void DependencyWalker_should_sort_by_dependency_order_for_specific_tables()
        {
            List<TableSchemaModel> tables = new List<TableSchemaModel>();

            var primaryTable = new TableSchemaModel("primary table");

            var dep1 = new TableSchemaModel("dependent table 1");
            dep1.Dependencies.Add("primary table");

            var dep2 = new TableSchemaModel("dependent table 2");
            dep2.Dependencies.Add("dependent table 1");

            tables.Add(primaryTable);
            tables.Add(dep1);
            tables.Add(dep2);

            for (int i = 0; i < 10; i++)
            {
                tables = tables.OrderBy(p => Guid.NewGuid()).ToList();
                var inst = new DependencyWalker();
                var sorted = inst.SortByChildDependencies(tables, new string[] { "primary table", "dependent table 1" });

                Assert.IsNotNull(sorted);
                Assert.AreEqual(2, sorted.Count);
                Assert.AreEqual(primaryTable.Name, sorted[0].Name);
                Assert.AreEqual(dep1.Name, sorted[1].Name);
            }
        }

        [TestMethod]
        public void DependencyWalker_GetParentDependencies_should_recurse_all_parent_dependencies()
        {
            List<TableSchemaModel> tables = new List<TableSchemaModel>();

            var primaryTable = new TableSchemaModel("primary table");

            var dep1 = new TableSchemaModel("dependent table 1");
            dep1.Dependencies.Add("primary table");

            var dep2 = new TableSchemaModel("dependent table 2");
            dep2.Dependencies.Add("dependent table 1");

            tables.Add(primaryTable);
            tables.Add(dep1);
            tables.Add(dep2);

            for (int i = 0; i < 10; i++)
            {
                tables = tables.OrderBy(p => Guid.NewGuid()).ToList();
                List<string> dependencies = new DependencyWalker().GetParentDependencies(tables, "dependent table 2");

                Assert.AreEqual(2, dependencies.Count);
                Assert.AreEqual("dependent table 1", dependencies[0]);
                Assert.AreEqual("primary table", dependencies[1]);
            }
        }

        [TestMethod]
        public void DependencyWalker_GetChildDependencies_should_recurse_all_child_dependencies()
        {
            List<TableSchemaModel> tables = new List<TableSchemaModel>();

            var primaryTable = new TableSchemaModel("primary table");

            var dep1 = new TableSchemaModel("dependent table 1");
            dep1.Dependencies.Add("primary table");

            var dep2 = new TableSchemaModel("dependent table 2");
            dep2.Dependencies.Add("dependent table 1");

            tables.Add(primaryTable);
            tables.Add(dep1);
            tables.Add(dep2);

            for (int i = 0; i < 10; i++)
            {
                tables = tables.OrderBy(p => Guid.NewGuid()).ToList();
                var input = new string[] { "primary table" };

                List<string> dependencies = new DependencyWalker().GetChildDependencies(tables, input);

                Assert.AreEqual(2, dependencies.Count);
                Assert.IsTrue(dependencies.Contains("dependent table 1"));
                Assert.IsTrue(dependencies.Contains("dependent table 2"));
            }
        }
    }
}
