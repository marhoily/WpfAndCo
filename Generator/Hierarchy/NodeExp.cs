using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public class NodeExp
    {
        public string Name { get; }
        public object Model { get; }
        public NodeExp[] Nodes { get; }
        public NodeExp(string name, object model, IEnumerable<NodeExp> nodes)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Name = name;
            Model = model;
            Nodes = nodes.ToArray();
        }
    }
}