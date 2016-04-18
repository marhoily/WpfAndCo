using System;
using System.IO;
using Generator.DataContext;
using Generator.Entry;
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

            File.WriteAllText(
                Path.Combine(dir, "DataContext.cs"), 
                new Root(ro).TransformText());

            File.WriteAllText(
                Path.Combine(dir, "DataContext.read.cs"), 
                new Read(ro).TransformText());

            Generate(ro, dir, entityType =>
                new EntityTypeGenerator(entityType).TransformText(),
                ".cs");
            Generate(ro, dir, entityType =>
                new EntityTypeChangeTracking(entityType).TransformText(),
                ".tracking.cs");
            Generate(ro, dir, entityType =>
                new EntityTypeKey(entityType).TransformText(),
                ".key.cs");
            Generate(ro, dir, entityType => 
                new EntityTypeSerialization(entityType).TransformText(), 
                ".serialization.cs");
        }

        private static void Generate(IModel ro, string dir, Func<IEntityType, string> transform, string ext)
        {
            foreach (var entityType in ro.GetEntityTypes())
            {
                var codeFile = Path.Combine(dir, entityType.Name + ext);
                File.WriteAllText(codeFile, transform(entityType));
            }
        }
    }
}
