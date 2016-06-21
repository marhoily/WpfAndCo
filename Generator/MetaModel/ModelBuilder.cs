using System;
using System.Collections.Generic;

namespace Generator
{
    public class ModelBuilder
    {
        private readonly List<Type> _entityTypes = new List<Type>();

        public void Entity<T>()
        {
            _entityTypes.Add(typeof(T));
        }

        public MetaModel Model => new MetaModel(_entityTypes);
    }
}