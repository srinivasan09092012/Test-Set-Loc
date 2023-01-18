using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UA3.CodeFactory.Core.CodeAnalysis
{
    public class TypeModel
    {
        public TypeModel(string name, string @namespace)
        {
            this.Name = name;
            this.Namespace = @namespace;
        }

        public string Name { get; private set; }
        public string Namespace { get; private set; }
    }
}
