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
            var proj = Path.GetFullPath(Path.Combine(
                "../../../Sample.Tests/Sample.Tests.csproj"));
            Raw.Generate(builder.Model, proj).Generate();
        }

    }
}
