using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static System.Reflection.BindingFlags;

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
            var ctor = GetConstrucorArg(Tp);
            var t = ChooseModel(ctor, model, registrations);
            var one = t as Result.One;
            if (one != null)
            {
                yield return new NodeExp(
                    one.Transformer, Expand2(registrations, one.Model));
            }
            else
            {
                var many = (Result.Many)t;
                foreach (var m in many.Transformers)
                    yield return new NodeExp(m, Expand2(registrations, many.Model));
            }
        }

        private IEnumerable<NodeExp> Expand2(
            List<RegRoot> registrations, object model)
        {
            foreach (var n in Nodes)
            {
                var ctor = GetConstrucorArg(n.Tp);
                var result = ChooseModel(ctor, model, registrations);
                var one = result as Result.One;
                if (one != null)
                {
                    yield return new NodeExp(one.Transformer,
                        n.Nodes.SelectMany(x => x.Expand1(
                            registrations, one.Model)));
                }
                else
                {
                    foreach (var m in ((Result.Many)result).Transformers)
                        yield return new NodeExp(m,
                            n.Nodes.SelectMany(x => x.Expand1(registrations, m)));
                }
            }
        }

        private abstract class Result
        {
            public sealed class One : Result
            {
                public object Model { get; }
                public ITransformer Transformer { get; }
                public One(ITransformer transformer, object model)
                {
                    Transformer = transformer;
                    Model = model;
                }
            }
            public sealed class Many : Result
            {
                public object Model { get; }
                public IEnumerable<ITransformer> Transformers { get; }
                public Many(IEnumerable<ITransformer> transformers, object model)
                {
                    Transformers = transformers;
                    Model = model;
                }
            }
        }
     
        private Result ChooseModel(OneArgCtor ctor, object model, List<RegRoot> registrations)
        {
            var better = Model ?? model;
            if (ctor.NoArgs) return new Result.One(ctor.Invoke(null), model);
            if (ctor.ArgType.IsInstanceOfType(better))
                return new Result.One(ctor.Invoke(better), model);
            var reg = registrations.Single(r => r.Value == ctor.ArgType);
            var args = reg.Convert(better);
            return new Result.Many(args.Select(ctor.Invoke), model);
        }

        private OneArgCtor GetConstrucorArg(Type tp)
        {
            var ctor = tp.GetConstructors(Public | Instance)
                .SingleOrDefault(c => c.GetParameters().Length <= 1);
            return ctor != null ? new OneArgCtor(ctor) : null;
        }

        private class OneArgCtor
        {
            public Type ArgType { get; }
            public bool NoArgs => ArgType == null;
            public Func<object, ITransformer> Invoke { get; }

            public OneArgCtor(ConstructorInfo ctor)
            {
                var arg = ctor.GetParameters().SingleOrDefault();
                if (arg == null)
                {
                    Invoke = _ => (ITransformer)ctor.Invoke(new object[0]);
                }
                else
                {
                    ArgType = arg.ParameterType;
                    Invoke = a => (ITransformer)ctor.Invoke(new[] { a });
                }
            }

            public override string ToString() => $"{ArgType?.Name}";
        }
    }
}