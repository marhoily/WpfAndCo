using System;
using System.Reflection;
using Sample;

namespace Generator
{
    public sealed class MetaProperty
    {
        public PropertyInfo Property { get; }
        public Type ClrType => Property.PropertyType;
        public string Name => Property.Name + (IsNavigation ? "Id" : "");
        public string Type => 
            IsNavigation ? "Guid" : Property.PropertyType.Name;
        public bool IsNavigation => Attribute
            .IsDefined(Property, typeof(NavigationAttribute));

        public MetaProperty(PropertyInfo property)
        {
            Property = property;
        }
    }
}