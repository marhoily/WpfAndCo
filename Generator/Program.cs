﻿using System.IO;
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
                new NodeBuilder<Crud>(builder.Model) {
                new NodeBuilder<Entity> {
                    new NodeBuilder<CreateAggregate>(),
                    new NodeBuilder<CreateCommit>(),
                    new NodeBuilder<CreateHandler>(),
                    new NodeBuilder<CreateValidator>(),
                    new NodeBuilder<UpdateCommit>(),
                }}}
            .With((MetaModel m) => m.GetEntityTypes())
            .Generate();
        }

    }
}
