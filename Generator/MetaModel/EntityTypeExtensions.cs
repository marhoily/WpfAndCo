using System.Linq;

namespace Generator
{
    public static class EntityTypeExtensions
    {
        public static string GetPropertiesArgumentsList(this MetaType meta)
            => meta.GetProperties()
                .Select(p => $"{p.Type} {p.Name}")
                .Join();

        public static string GetPropertiesParametersList(this MetaType meta)
            => meta.GetProperties()
                .Select(p => p.Name)
                .Join();
    }
}