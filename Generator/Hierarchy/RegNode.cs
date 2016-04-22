using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    using M = IEnumerable<Tuple<ITransformer, object>>;

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
            return Choose(OneArgCtor.From(Tp), Model ?? model, registrations)
                .Select(o => new NodeExp(o.Item1, Nodes.SelectMany(
                    x => x.Expand(registrations, o.Item2))));
        }

        private static M Choose(OneArgCtor ctor,
            object model, IEnumerable<RegRoot> registrations)
        {
            if (ctor.NoArgs) {
                yield return Tuple.Create(ctor.Invoke(null), model);
            }
            else if (ctor.ArgType.IsInstanceOfType(model)) {
                yield return Tuple.Create(ctor.Invoke(model), model);
            }
            else {
                var reg = registrations.Single(r => r.Value == ctor.ArgType);
                var args = reg.Convert(model);
                var many = args.Select(m => Tuple.Create(ctor.Invoke(m), m));
                foreach (var o in many) yield return o;
            }
        }
    }
}