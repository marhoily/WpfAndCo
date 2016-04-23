using System;
using System.Collections;
using System.Collections.Generic;

namespace Generaid
{
    public sealed class NodeBuilder<T> : IEnumerable
    {
        private readonly object _model;
        private readonly List<GenNode.Proto> _nodes = new List<GenNode.Proto>();

        public NodeBuilder() { }
        public NodeBuilder(object model) { _model = model; }
        public void Add<TNode>(NodeBuilder<TNode> item) => _nodes.Add(item.Build());
        internal GenNode.Proto Build() => new GenNode.Proto { Tp = typeof(T), Nodes = _nodes, Model = _model};
        IEnumerator IEnumerable.GetEnumerator() { throw new Exception(); }
    }
}