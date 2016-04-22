using System;
using System.Collections;
using System.Collections.Generic;

namespace Generator
{
    public sealed class NodeBuilder<T> : IEnumerable
    {
        private readonly object _model;
        private readonly List<RegNode> _nodes = new List<RegNode>();

        public NodeBuilder() { }
        public NodeBuilder(object model) { _model = model; }
        public void Add<TNode>(NodeBuilder<TNode> item) => _nodes.Add(item.Build());
        public RegNode Build() => new RegNode(typeof(T), _nodes, _model);
        public IEnumerator GetEnumerator() { throw new NotImplementedException(); }
    }
}