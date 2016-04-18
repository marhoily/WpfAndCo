using System.IO;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Sample;

namespace Generator
{
    internal static class Program
    {
        private static void Main()
        {
            var conventions = new CoreConventionSetBuilder();
            var builder = new ModelBuilder(conventions.CreateConventionSet());
            builder.Entity<Person>();
            builder.Entity<City>();
            Generate(builder.Model);
        }

        private static void Generate(IModel ro)
        {
            var dir = Path.GetFullPath(Path.Combine("../../../Sample.Tests/Generated"));
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var dataContext = new DataContextGenerator(ro)
                .TransformText();
            File.WriteAllText(Path.Combine(dir, "DataContext.cs"), dataContext);

            foreach (var entityType in ro.GetEntityTypes())
            {
                var code = new EntityTypeGenerator(entityType)
                    .TransformText();
                var codeFile = Path.Combine(dir, entityType.Name + ".cs");
                File.WriteAllText(codeFile, code);
            }
        }
    }
}
