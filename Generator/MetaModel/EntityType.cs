using System;
using System.Collections.Generic;

namespace Generator
{
    public sealed class EntityType 
    {
        private Type _type;

        public EntityType(Type type)
        {
            _type = type;
        }

        public string Name { get; }
        public Type ClrType { get; }
        public IEnumerable<IProperty> GetProperties()
        {
            throw new NotImplementedException();
        }
    }
}