using System;
using System.Collections;

namespace Generator
{
    public class Node<T> : IEnumerable
    {
        public Node(object model)
        {
        }

        public Node()
        {
        }

        public void Add<U>(Node<U> item) { }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}