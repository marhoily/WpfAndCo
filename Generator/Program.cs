using System.IO;
using Generaid;
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
            var proj = Path.GetFullPath(Path.Combine(
                "../../../Sample.Tests/Sample.Tests.csproj"));

            new HierarchyBuilder(proj, "Generated") {
                new NodeBuilder<Raw>(builder.Model) {
                    new NodeBuilder<ChangeSet> {new NodeBuilder<Change>()},
                    new NodeBuilder<TableSet> {new NodeBuilder<Table>()},
                    new NodeBuilder<Columns>(),
                    new NodeBuilder<PrimaryKey>()
                },
                new NodeBuilder<Super>(builder.Model) {
                    new NodeBuilder<DataContext>()
                }
            }
            .With((IModel m) => m.GetEntityTypes())
            .Generate();
        }

    }
}
