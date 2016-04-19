using System;
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

            File.WriteAllText(
                Path.Combine(dir, "ChangeSet.cs"),
                new ChangeSet.Root(ro).TransformText());

            File.WriteAllText(
                Path.Combine(dir, "DataContext.cs"),
                new DataContext.Root(ro).TransformText());

            File.WriteAllText(
                Path.Combine(dir, "DataContext.tracking.cs"),
                new DataContext.Tracking(ro).TransformText());

            File.WriteAllText(
                Path.Combine(dir, "DataContext.read.cs"),
                new DataContext.Read(ro).TransformText());

            File.WriteAllText(
                Path.Combine(dir, "DataContext.write.cs"),
                new DataContext.Write(ro).TransformText());

            Generate(ro, dir, entityType =>
                new ChangeSetEntry.Root(entityType).TransformText(),
                "Cs{0}.cs");

            Generate(ro, dir, entityType =>
                new Entry.Root(entityType).TransformText(),
                "{0}.cs");
            Generate(ro, dir, entityType =>
                new Entry.Tracking(entityType).TransformText(),
                "{0}.tracking.cs");
            Generate(ro, dir, entityType =>
                new Entry.Key(entityType).TransformText(),
                "{0}.key.cs");
            Generate(ro, dir, entityType =>
                new Entry.Read(entityType).TransformText(),
                "{0}.read.cs");
            Generate(ro, dir, entityType =>
                new Entry.Write(entityType).TransformText(), 
                "{0}.write.cs");
        }

        private static void Generate(IModel ro, string dir, Func<IEntityType, string> transform, string ext)
        {
            foreach (var entityType in ro.GetEntityTypes())
            {
                var codeFile = Path.Combine(dir, string.Format(ext, entityType.Name));
                File.WriteAllText(codeFile, transform(entityType));
            }
        }
    }
}
