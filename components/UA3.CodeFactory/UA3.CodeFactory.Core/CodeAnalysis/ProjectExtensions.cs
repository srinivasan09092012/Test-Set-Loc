using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UA3.CodeFactory.Core.CodeAnalysis
{
    public static class ProjectExtensions
    {
        public static Document GetDocument(this Project project, string name)
        {
            return project.Documents.FirstOrDefault(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
