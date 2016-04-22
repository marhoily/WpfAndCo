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
            return Xxx(registrations, model, Tp);
        }

        private IEnumerable<NodeExp> Xxx(List<RegRoot> registrations, object model, Type tp)
        {
            var ctor = OneArgCtor.From(tp);
            var result = Generator.Model.Choose(ctor, Model ?? model, registrations);
            var one = result as Model.One;
            if (one != null)
            {
                yield return new NodeExp(one.Transformer, 
                    Expand2(registrations, one.Model));
            }
            else
            {
                foreach (var o in ((Model.Many) result).Ones)
                    yield return new NodeExp(o.Transformer,
                        Expand2(registrations, o.Model));
            }
        }
        private IEnumerable<NodeExp> Yyy(List<RegRoot> registrations, object model, Type tp, RegNode n)
        {
            var ctor = OneArgCtor.From(tp);
            var result = Generator.Model.Choose(ctor, Model ?? model, registrations);
            var one = result as Model.One;
            if (one != null)
            {
                yield return new NodeExp(one.Transformer,
                    n.Nodes.SelectMany(x => x.Expand1(registrations, one.Model)));
            }
            else
            {
                foreach (var o in ((Model.Many)result).Ones)
                    yield return new NodeExp(o.Transformer,
                        n.Nodes.SelectMany(x => x.Expand1(registrations, o.Model)));
            }
        }

        private IEnumerable<NodeExp> Expand2(
            List<RegRoot> registrations, object model)
        {
            return Nodes.SelectMany(n => Yyy(registrations, model, n.Tp, n));
        }
    }
}