using System;
using System.Collections;
using System.Collections.Generic;

namespace Generator
{
    public class HierarchyRoot : IEnumerable
    {
        public HierarchyRoot(string dir, string generated)
        {
        }
        public void Add<T>(Node<T> item) { }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public HierarchyRoot With<TK, TV>(Func<TK, IEnumerable<TV>> func)
        {
            return this;
        }

        public void Build()
        {
            
        }
    }
}