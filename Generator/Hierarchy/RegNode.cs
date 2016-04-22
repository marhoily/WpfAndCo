using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public sealed class RegNode {
        public Type Tp { get; }
        public List<RegNode> Nodes { get; }
        public object Model { get; }

        public RegNode(Type tp, List<RegNode> nodes, object model)
        {
            Tp = tp;
            Nodes = nodes;
            Model = model;
        }

        public IEnumerable<NodeExp> Expand(List<RegRoot> registrations)
        {
            yield return new NodeExp(Tp.Name, Model, Expand(registrations, Model));
        }

        private IEnumerable<NodeExp> Expand(List<RegRoot> registrations, object model)
        {
            foreach (var n in Nodes)
            {
                yield return new NodeExp(n.Tp.Name, Model, 
                    n.Nodes.SelectMany(x => x.Expand(registrations)));
            }
        }
    }
}