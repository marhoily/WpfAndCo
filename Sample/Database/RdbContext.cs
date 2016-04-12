using Microsoft.Data.Entity;

namespace Sample
{
    public class RdbContext : DbContext
    {
        public DbSet<Person> People { get; protected set; }
        public DbSet<City> Cities { get; protected set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Visual Studio 2015 | Use the LocalDb 12 instance created by Visual Studio
            optionsBuilder.UseSqlServer(@"Server=(localdb)\msSqlLocaldb;Database=R;Trusted_Connection=True;");

            // Visual Studio 2013 | Use the LocalDb 11 instance created by Visual Studio
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\v11.0;Database=MessageDb;Trusted_Connection=True;");
        }
    }
}