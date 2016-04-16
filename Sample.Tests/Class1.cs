using System.Linq;
using ApprovalTests;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
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
            var entityType = ro.GetEntityTypes().First();
            //entityType.GetProperties()
        }
    }
}
