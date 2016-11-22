using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UA3.CodeFactory.Core.CodeAnalysis;
using UA3.CodeFactory.TestSupport;

namespace UA3.CodeFactory.Tests.CodeAnalysis
{
    [TestClass]
    public class BasConventionsTests
    {
        [TestMethod]
        public void BasConventions_should_derive_conventions_from_existing_solution()
        {
            var solution = SolutionUtil.CreateTypicalBASSolution("c:\\");

            BasConventions conventions = BasConventions.CreateFrom(solution.Workspace.CurrentSolution);

            var properties = TypeDescriptor.GetProperties(conventions);

            foreach (PropertyDescriptor pd in properties)
            {
                object val = pd.GetValue(conventions);
                string valString = "<null>";

                if (val != null)
                {
                    if (val is ProjectModel)
                    {
                        var pm = val as ProjectModel;
                        valString = $"ProjectModel (Id: {pm.Id}, Name: {pm.Name})";
                    }
                    else if (val is ProjectItemLocation)
                    {
                        var loc = val as ProjectItemLocation;
                        var folderString = string.Join(", ", loc.Folders.ToArray());
                        valString = $"ProjectItemLocation (ProjectId: {loc.Project}, Folders: {folderString}";
                    }                        
                    else
                    {
                        valString = val.ToString();
                    }
                }
                else
                {
                    Assert.Fail("Property {0} was null", pd.Name);
                }

                Console.WriteLine("{0} | {1}", pd.Name, valString);
            }
        }
    }
}
