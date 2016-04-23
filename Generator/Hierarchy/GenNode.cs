using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public sealed class GenNode : ILocated
    {
        public ITransformer Transformer { get; }
        public GenNode[] GenNodes { get; }
        public ILocated Owner { get; private set; }
        public string ProjectDir => Owner.ProjectDir + "/" + Transformer.GetType().Name;

        public GenNode(ITransformer transformer, IEnumerable<GenNode> nodes)
        {
            if (transformer == null)
                throw new ArgumentNullException(nameof(transformer));
            Transformer = transformer;
            GenNodes = nodes.ToArray();
        }

        public void SetOwner(ILocated owner)
        {
            Owner = owner;
            foreach (var node in GenNodes)
                node.SetOwner(this);
        }
        public sealed class Proto
        {
            public Type Tp;
            public List<Proto> Nodes;
            public object Model;
        }
    }
}