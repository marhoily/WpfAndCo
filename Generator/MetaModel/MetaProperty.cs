using System;
using System.ComponentModel.DataAnnotations;
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
        public bool IsRequired => Attribute
            .IsDefined(Property, typeof(RequiredAttribute));

        private DeleteReaction? ExplicitDeleteReaction => 
            ((OnDeleteAttribute)Attribute.GetCustomAttribute(
                Property, typeof(OnDeleteAttribute)))?.Reaction;
        private DeleteReaction ImplicitDeleteReaction => 
            IsRequired ? DeleteReaction.Deny : DeleteReaction.Nullify;
        public DeleteReaction OnDelete =>
            ExplicitDeleteReaction ?? ImplicitDeleteReaction;

        public MetaProperty(PropertyInfo property)
        {
            Property = property;
        }
    }
}