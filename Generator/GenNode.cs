using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Generator
{
    public sealed class GenNode : INodeOwner
    {
        private ITransformer Transformer { get; }
        private GenNode NodeOwner => Owner as GenNode;
        private GenNode[] GenNodes { get; }
        private INodeOwner Owner { get; set; }
        private string Name => Transformer.Name;

        int INodeOwner.Level => Owner.Level + 1;
        public string ProjectDir => Owner.ProjectDir;
        public string DependentUpon => NodeOwner?.Name;
        public string FullName => ProjectDir + "\\" + Name;

        public GenNode(ITransformer transformer, IEnumerable<GenNode> nodes)
        {
            if (transformer == null)
                throw new ArgumentNullException(nameof(transformer));
            Transformer = transformer;
            GenNodes = nodes.ToArray();
        }

        internal void SetOwner(INodeOwner owner)
        {
            Owner = owner;
            foreach (var node in GenNodes)
                node.SetOwner(this);
        }

        internal sealed class Proto
        {
            public Type Tp;
            public List<Proto> Nodes;
            public object Model;
        }

        internal IEnumerable<GenNode> GetDescendantsAndSelf()
        {
            yield return this;
            foreach (var child in GenNodes)
                foreach (var d in child.GetDescendantsAndSelf())
                    yield return d;
        }

        public void Generate(string projectRoot)
        {
            var file = Path.Combine(projectRoot, FullName);
            File.WriteAllText(file, Transformer.TransformText());
        }
    }
}