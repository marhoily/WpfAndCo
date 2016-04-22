using System;
using System.Collections;
using System.Collections.Generic;

namespace Generator
{
    public class HierarchyRoot : IEnumerable
    {
        private readonly string _dir;
        private readonly string _generated;
        private readonly List<RegNode> _nodes = new List<RegNode>();
        private readonly List<RegRoot> _registrations = new List<RegRoot>();

        public HierarchyRoot(string dir, string generated)
        {
            _dir = dir;
            _generated = generated;
        }
        public void Add<T>(Node<T> item) { _nodes.Add(item.Build()); }
        IEnumerator IEnumerable.GetEnumerator() { throw new NotImplementedException(); }

        public HierarchyRoot With<TK, TV>(Func<TK, IEnumerable<TV>> func)
        {
            _registrations.Add(new RegRoot(
                typeof(TK), typeof(TV), 
                x => (IEnumerable<object>)func((TK)x)));
            return this;
        }

        public Hierarchy Build()
        {
            return new Hierarchy(_nodes, _registrations);
        }
    }

    public sealed class RegRoot
    {
        private readonly Func<object, IEnumerable<object>> _convert;
        public Type Key { get; set; }
        public Type Value { get; set; }

        public RegRoot(Type key, Type value, 
            Func<object, IEnumerable<object>> convert)
        {
            _convert = convert;
            Key = key;
            Value = value;
        }

        public IEnumerable<object> Convert(object obj)
        {
            return _convert(obj);
        }
    }
}