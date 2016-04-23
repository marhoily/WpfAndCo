using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public sealed class GenNode : ILocated
    {
        public ITransformer Transformer { get; }
        public string ProjectDir => Owner.ProjectDir;
        public GenNode[] GenNodes { get; }
        public ILocated Owner { get; private set; }
        public int Level => Owner.Level + 1;
        public string FullName => ProjectDir + "\\" + Transformer.Name;

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

        public IEnumerable<GenNode> GetDescendantsAndSelf()
        {
            yield return this;
            foreach (var child in GenNodes)
                foreach (var d in child.GetDescendantsAndSelf())
                    yield return d;
        }
    }
}