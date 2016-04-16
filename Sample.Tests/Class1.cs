using System.Linq;
using ApprovalTests;
using ApprovalUtilities.Utilities;
using FluentAssertions;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Conventions;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Xunit;

namespace Sample
{
    public class Class1
    {
        [Fact]
        public void FactMethodName()
        {
            var builder = new ModelBuilder(
                new CoreConventionSetBuilder().CreateConventionSet());
            builder.Entity<Person>();
            builder.Entity<City>();
            IModel ro = builder.Model;
            var city = ro.FindEntityType(typeof(City).FullName);
            Approvals.VerifyAll(city.GetProperties()
                .Select(x =>x.WritePropertiesToString()), "");
            var property = city.GetProperties().First();
            property.ClrType.Name.Should().Be("Int64");
            property.Name.Should().Be("Id");
        }
    }
}
