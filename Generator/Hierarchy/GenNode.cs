using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public sealed class GenNode
    {
        public ITransformer Transformer { get; }
        public GenNode[] GenNodes { get; }
        public GenNode(ITransformer transformer, IEnumerable<GenNode> nodes)
        {
            if (transformer == null)
                throw new ArgumentNullException(nameof(transformer));
            Transformer = transformer;
            GenNodes = nodes.ToArray();
        }
        public sealed class Proto
        {
            public Type Tp { get; }
            public List<Proto> Nodes { get; }
            public object Model { get; }

            public Proto(Type tp, List<Proto> nodes, object model)
            {
                Tp = tp;
                Nodes = nodes;
                Model = model;
            }
        }

    }
}