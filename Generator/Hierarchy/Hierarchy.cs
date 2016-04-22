using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    using M = IEnumerable<Tuple<ITransformer, object>>;

    public class Hierarchy
    {
        public string ProjectPath { get; }
        public string ProjectDir { get; }
        public NodeExp[] Nodes { get; }

        public Hierarchy(string projectPath, string projectDir,
            List<RegNode> nodes, List<Converter> converters)
        {
            ProjectPath = projectPath;
            ProjectDir = projectDir;
            Nodes = nodes
                .SelectMany(n => Expand(n, converters, n.Model))
                .ToArray();
        }

        private static IEnumerable<NodeExp> Expand(
            RegNode node, List<Converter> converters, object model)
        {
            return Choose(OneArgCtor.From(node.Tp), converters, node.Model ?? model)
                .Select(o => new NodeExp(o.Item1, node.Nodes.SelectMany(
                    x => Expand(x, converters, o.Item2))));
        }

        private static M Choose(OneArgCtor ctor,
            List<Converter> converters, object model)
        {
            if (ctor.NoArgs)
            {
                yield return Tuple.Create(ctor.Invoke(null), model);
            }
            else if (ctor.ArgType.IsInstanceOfType(model))
            {
                yield return Tuple.Create(ctor.Invoke(model), model);
            }
            else
            {
                var reg = converters.Single(r => r.Value == ctor.ArgType);
                var args = reg.Convert(model);
                var many = args.Select(m => Tuple.Create(ctor.Invoke(m), m));
                foreach (var o in many) yield return o;
            }
        }
    }
}