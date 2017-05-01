//-----------------------------------------------------------------------------------------
// This code is the property of Hewlett Packard Enterprise, Copyright (c) 2016. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using MainEvent.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace MainEvent.Core.Services
{
    public class DependencyWalker
    {
        public List<string> GetChildDependencies(List<TableSchemaModel> tables, string tableName)
        {
            List<string> result = new List<string>();

            this.RecurseChildDependencies(tables, tableName, result);

            return result;
        }

        public List<string> GetChildDependencies(List<TableSchemaModel> tables, IEnumerable<string> tableNames)
        {
            List<string> result = new List<string>();

            this.RecurseChildDependencies(tables, tableNames, result);

            return result.Distinct().ToList();
        }

        public List<string> GetParentDependencies(List<TableSchemaModel> tables, string tableName)
        {
            List<string> result = new List<string>();

            this.RecurseParentDependencies(tables, tableName, result);

            return result;
        }

        public List<string> GetParentDependencies(List<TableSchemaModel> tables, IEnumerable<string> tableNames)
        {
            List<string> result = new List<string>();

            this.RecurseParentDependencies(tables, tableNames, result);

            return result.Distinct().ToList();
        }

        public List<TableSchemaModel> SortByChildDependencies(List<TableSchemaModel> items, IEnumerable<string> tableSubset)
        {
            List<TableSchemaModel> result = this.SortByChildDependencies(items);

            result.RemoveAll(p => tableSubset.Contains(p.Name) == false);

            return result;
        }

        public List<TableSchemaModel> SortByChildDependencies(List<TableSchemaModel> items)
        {
            List<TableSchemaModel> result = new List<TableSchemaModel>(items.Count);

            foreach (TableSchemaModel tbl in items)
            {
                if (tbl.Dependencies.Count == 0)
                {
                    result.Add(tbl);
                }
            }

            while (true)
            {
                bool activity = false;

                List<string> orderedNames = result.Select(p => p.Name).ToList();

                foreach (TableSchemaModel table in items)
                {
                    if (orderedNames.Contains(table.Name))
                    {
                        continue;
                    }
                    else
                    {
                        if (table.Dependencies.All(p => orderedNames.Contains(p)))
                        {
                            result.Add(table);
                            activity = true;
                            orderedNames = result.Select(p => p.Name).ToList();
                            continue;
                        }
                    }
                }

                if (!activity)
                {
                    break;
                }
            }

            return result;
        }

        private void RecurseParentDependencies(List<TableSchemaModel> tables, string tableName, List<string> dependencies)
        {
            var tbl = tables.Single(p => p.Name == tableName);

            foreach (string dep in tbl.Dependencies)
            {
                dependencies.Add(dep);
                this.RecurseParentDependencies(tables, dep, dependencies);
            }
        }

        private void RecurseParentDependencies(List<TableSchemaModel> tables, IEnumerable<string> tableNames, List<string> dependencies)
        {
            List<string> allDependencies = new List<string>();

            foreach (string tableName in tableNames)
            {
                this.RecurseChildDependencies(tables, tableName, allDependencies);
            }

            dependencies.AddRange(allDependencies.Except(tableNames).Distinct());
        }

        private void RecurseChildDependencies(List<TableSchemaModel> tables, IEnumerable<string> tableNames, List<string> dependencies)
        {
            List<string> allDependencies = new List<string>();

            foreach (string tableName in tableNames)
            {
                this.RecurseChildDependencies(tables, tableName, allDependencies);
            }

            dependencies.AddRange(allDependencies.Except(tableNames).Distinct());
        }

        private void RecurseChildDependencies(List<TableSchemaModel> tables, string tableName, List<string> dependencies)
        {
            var tbl = tables.Single(p => p.Name == tableName);

            var dependentOn = tables.Where(p => p.Dependencies.Contains(tbl.Name)).Select(p => p.Name);

            foreach (string dep in dependentOn)
            {
                if (!dependencies.Contains(dep))
                {
                    dependencies.Add(dep);
                }

                this.RecurseChildDependencies(tables, dep, dependencies);
            }
        }
    }
}