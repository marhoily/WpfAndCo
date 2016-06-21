using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public sealed class EntityType
    {
        public EntityType(Type type)
        {
            ClrType = type;
        }

        public string Name => ClrType.Name;
        public Type ClrType { get; }

        public IEnumerable<MetaProperty> GetProperties() => ClrType
            .GetProperties()
            .Select(p => new MetaProperty(p));
    }
}