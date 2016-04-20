using System;
using System.Collections;
using System.Collections.Generic;

namespace Generator
{
    public sealed class RegNode {
        public Type Tp { get; set; }
        public List<RegNode> Nodes { get; set; }
        public object Model { get; set; }

        public RegNode(Type tp, List<RegNode> nodes, object model)
        {
            Tp = tp;
            Nodes = nodes;
            Model = model;
        }
    }
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