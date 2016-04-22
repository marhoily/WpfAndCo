using System;
using System.Collections;
using System.Collections.Generic;

namespace Generator
{
    public class Node<T> : IEnumerable
    {
        private readonly object _model;
        private readonly List<RegNode> _nodes = new List<RegNode>();

        public Node() { }
        public Node(object model) { _model = model; }
        public void Add<TNode>(Node<TNode> item) { _nodes.Add(item.Build()); }

        public RegNode Build() { return new RegNode(typeof(T), _nodes, _model); }
        public IEnumerator GetEnumerator() { throw new NotImplementedException(); }
    }
}