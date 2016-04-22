using System;
using System.Collections;
using System.Collections.Generic;

namespace Generator
{
    public class Node<T> : IEnumerable
    {
        private readonly List<RegNode> _nodes = new List<RegNode>();
        private readonly object _model;

        public Node(object model)
        {
            _model = model;
        }

        public Node()
        {
        }
        public void Add<U>(Node<U> item) { }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public RegNode Build()
        {
            return new RegNode(typeof(T), _nodes, _model);
        }
    }
}