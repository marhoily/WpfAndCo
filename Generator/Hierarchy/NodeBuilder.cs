using System;
using System.Collections;
using System.Collections.Generic;
using static Generator.GenNode;

namespace Generator
{
    public sealed class NodeBuilder<T> : IEnumerable
    {
        private readonly object _model;
        private readonly List<Proto> _nodes = new List<Proto>();

        public NodeBuilder() { }
        public NodeBuilder(object model) { _model = model; }
        public void Add<TNode>(NodeBuilder<TNode> item) => _nodes.Add(item.Build());
        public Proto Build() => new Proto(typeof(T), _nodes, _model);
        public IEnumerator GetEnumerator() { throw new NotImplementedException(); }
    }
}