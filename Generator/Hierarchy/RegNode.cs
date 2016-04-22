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
            return Nodes.SelectMany(n => n.Expand(registrations));
        }
    }
}