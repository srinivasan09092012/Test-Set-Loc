using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UA3.CodeFactory.Core.UX
{
    public class Node
    {
        public Node()
        {
            this.Children = new ObservableCollection<Node>();
        }

        public ObservableCollection<Node> Children { get; set; }

        public string Name { get; set; }        
    }
}
