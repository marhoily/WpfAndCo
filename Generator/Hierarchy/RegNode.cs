using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public sealed class RegNode
    {
        public Type Tp { get; }
        public List<RegNode> Nodes { get; }
        public object Model { get; }

        public RegNode(Type tp, List<RegNode> nodes, object model)
        {
            Tp = tp;
            Nodes = nodes;
            Model = model;
        }

        public IEnumerable<NodeExp> Expand(
            List<RegRoot> registrations, object model)
        {
            return RegModel.Choose(OneArgCtor.From(Tp), Model ?? model, registrations)
                .Select(o => new NodeExp(o.Transformer, Nodes.SelectMany(
                    x => x.Expand(registrations, o.Model1))));
        }
    }
}