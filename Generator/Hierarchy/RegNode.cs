using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
                var result = ChooseModel(n.Tp, model, registrations);
                var one = result as Result.One;
                if (one != null)
                {
                    yield return new NodeExp(n.Tp.Name, one.Model,
                        n.Nodes.SelectMany(x => x.Expand1(
                            registrations, one.Model)));
                }
                else
                {
                    foreach (var m in ((Result.Many)result).Models)
                        yield return new NodeExp(n.Tp.Name, m,
                            n.Nodes.SelectMany(x => x.Expand1(registrations, m)));
                }
            }
        }

        private abstract class Result
        {
            public sealed class One : Result
            {
                public object Model { get; }
                public One(object model) { Model = model; }
            }
            public sealed class Many : Result
            {
                public IEnumerable<object> Models { get; }
                public Many(IEnumerable<object> models) { Models = models; }
            }
        }
        private Result ChooseModel(Type tp, object model, List<RegRoot> registrations)
        {
            var better = Model ?? model;
            if (better.GetType() == tp) return new Result.One(better);
            var c = tp.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Single();
            var p = c.GetParameters()[0].ParameterType;
            var reg = registrations.Single(r => r.Key == p);
            return new Result.Many(reg.Convert(better));
        }
    }
}