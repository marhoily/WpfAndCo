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

        public IEnumerable<NodeExp> Expand1(List<RegRoot> registrations, object model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            yield return new NodeExp(Tp.Name,
                model, Expand2(registrations, model));
        }

        private IEnumerable<NodeExp> Expand2(List<RegRoot> registrations, object model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            foreach (var n in Nodes)
            {
                var betterModel = Model ?? model;
                yield return new NodeExp(n.Tp.Name, betterModel, 
                    n.Nodes.SelectMany(x => x.Expand1(
                        registrations, betterModel)));
            }
        }
    }
}