using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public class NodeExp
    {
        public ITransformer Model { get; }
        public NodeExp[] Nodes { get; }
        public NodeExp(ITransformer model, IEnumerable<NodeExp> nodes)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Model = model;
            Nodes = nodes.ToArray();
        }
    }
}