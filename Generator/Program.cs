using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var builder = new ModelBuilder(
                new CoreConventionSetBuilder().CreateConventionSet());
            builder.Entity<Person>();
            builder.Entity<City>();
            IModel ro = builder.Model;
            var dir = Path.GetFullPath(Path.Combine(
                "../../../Sample.Tests/Generated"));
            foreach (var entityType in ro.GetEntityTypes())
            {
                var code = new ClassContentGenerator(entityType)
                    .TransformText();
                var codeFile = Path.Combine(dir, entityType.Name + ".cs");
                File.WriteAllText(codeFile, code);
            }
        }
    }
}
