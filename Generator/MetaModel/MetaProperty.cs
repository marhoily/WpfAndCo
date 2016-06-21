using System;
using System.Reflection;

namespace Generator
{
    public sealed class MetaProperty
    {
        public PropertyInfo Property { get; }
        public Type ClrType => Property.PropertyType;
        public string Name => Property.Name;

        public MetaProperty(PropertyInfo property)
        {
            Property = property;
        }
    }
}