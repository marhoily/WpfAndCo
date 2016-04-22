using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public class NodeExp
    {
        public ITransformer Transformer { get; }
        public NodeExp[] Nodes { get; }
        public NodeExp(ITransformer transformer, IEnumerable<NodeExp> nodes)
        {
            if (transformer == null)
                throw new ArgumentNullException(nameof(transformer));
            Transformer = transformer;
            Nodes = nodes.ToArray();
        }
    }
}