using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Generator
{
    public static class EntityTypeExtensions
    {
        public static IEnumerable<IProperty> GetPrimaryKeyProperties(this IEntityType entity)
        {
            return entity.GetProperties().Where(p => p.IsPrimaryKey());
        }
        public static string GetPrimaryKeyPropertiesArgumentsList(this IEntityType entity)
        {
            return entity.GetPrimaryKeyProperties()
                .Select(p => string.Format("{0} {1}", p.ClrType.FullName, p.Name))
                .Join();
        }
        public static string GetPrimaryKeyPropertiesParametersList(this IEntityType entity)
        {
            return entity.GetPrimaryKeyProperties()
                .Select(p => p.Name)
                .Join();
        }
        public static string GetPropertiesArgumentsList(this IEntityType entity)
        {
            return entity.GetProperties()
                .Select(p => string.Format("{0} {1}", p.ClrType.FullName, p.Name))
                .Join();
        }
        public static string GetPropertiesParametersList(this IEntityType entity)
        {
            return entity.GetProperties()
                .Select(p => p.Name)
                .Join();
        }
    }
}