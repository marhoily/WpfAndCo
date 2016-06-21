using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public static class EntityTypeExtensions
    {
        public static string GetPropertiesArgumentsList(this EntityType entity)
            => entity.GetProperties()
                .Select(p => $"{p.ClrType.FullName} {p.Name}")
                .Join();

        public static string GetPropertiesParametersList(this EntityType entity)
            => entity.GetProperties()
                .Select(p => p.Name)
                .Join();
    }

    public interface IProperty
    {
        Type ClrType { get; }
        string Name { get; }
    }
}