using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public sealed class MetaType
    {
        public MetaType(Type type)
        {
            ClrType = type;
        }

        public string Name => ClrType.Name;
        public Type ClrType { get; }

        public IEnumerable<MetaProperty> NavigationProperties =>
            GetProperties().Where(p => p.IsNavigation);

        public IEnumerable<MetaProperty> GetProperties() => ClrType
            .GetProperties()
            .Select(p => new MetaProperty(p));
    }
}