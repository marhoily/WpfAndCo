using System;
using System.Collections.Generic;
using System.Linq;
using static Generator.GenNode;

namespace Generator
{
    using M = IEnumerable<Tuple<ITransformer, object>>;
    using C = IDictionary<Type, Func<object, IEnumerable<object>>>;

    public sealed class GenHierarchy
    {
        public string ProjectPath { get; }
        public string ProjectDir { get; }
        public GenNode[] GenNodes { get; }

        public GenHierarchy(string projectPath,
            string projectDir, List<Proto> nodes, C converters)
        {
            ProjectPath = projectPath;
            ProjectDir = projectDir;
            GenNodes = nodes
                .SelectMany(n => Expand(n, converters, n.Model))
                .ToArray();
        }

        private static IEnumerable<GenNode> Expand(Proto node, C converters, object model)
        {
            return Choose(OneArgCtor.From(node.Tp), converters, node.Model ?? model)
                .Select(o => new GenNode(o.Item1, node.Nodes.SelectMany(
                    x => Expand(x, converters, o.Item2))));
        }

        private static M Choose(OneArgCtor ctor, C converters, object model)
        {
            if (ctor.NoArgs)
                return new[] { Tuple.Create(ctor.Invoke(null), model) };
            if (ctor.ArgType.IsInstanceOfType(model))
                return new[] { Tuple.Create(ctor.Invoke(model), model) };
            return converters[ctor.ArgType](model)
                .Select(m => Tuple.Create(ctor.Invoke(m), m));
        }
    }
}