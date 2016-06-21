using System.IO;
using Generaid;
using Sample;

namespace Generator
{
    internal static class Program
    {
        private static void Main()
        {
            var builder = new ModelBuilder();
            builder.Entity<Person>();
            builder.Entity<City>();
            var proj = Path.GetFullPath(Path.Combine(
                "../../../Sample.Tests/Sample.Tests.csproj"));

            new HierarchyBuilder(proj, "Generated") {
                new NodeBuilder<Raw>(builder.Model) {
                    new NodeBuilder<ChangeSet> {new NodeBuilder<Change>()},
                    new NodeBuilder<TableSet> {new NodeBuilder<Table>()},
                    new NodeBuilder<Columns>(),
                },
                new NodeBuilder<Super>(builder.Model) {
                    new NodeBuilder<DataContext>()
                }
            }
            .With((MetaModel m) => m.GetEntityTypes())
            .Generate();
        }

    }
}
