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

        public IEnumerable<NodeExp> Expand1(
            List<RegRoot> registrations, object model)
        {
            if (model is ITransformer) throw new ArgumentException(nameof(model));
            var ctor = OneArgCtor.From(Tp);
            var t = Generator.Model.Choose(ctor, Model ?? model, registrations);
            var one = t as Model.One;
            if (one != null)
            {
                yield return new NodeExp(
                    one.Transformer, Expand2(registrations, one.Model));
            }
            else
            {
                var many = (Model.Many)t;
                foreach (var o in many.Ones)
                    yield return new NodeExp(o.Transformer, 
                        Expand2(registrations, o.Model));
            }
        }

        private IEnumerable<NodeExp> Expand2(
            List<RegRoot> registrations, object model)
        {
            foreach (var n in Nodes)
            {
                var ctor = OneArgCtor.From(n.Tp);
                var result = Generator.Model.Choose(ctor, Model??model, registrations);
                var one = result as Model.One;
                if (one != null)
                {
                    yield return new NodeExp(one.Transformer,
                        n.Nodes.SelectMany(x => x.Expand1(
                            registrations, one.Model)));
                }
                else
                {
                    foreach (var o in ((Model.Many)result).Ones)
                        yield return new NodeExp(o.Transformer, 
                            n.Nodes.SelectMany(
                                x => x.Expand1(registrations, o.Model)));
                }
            }
        }
    }
}