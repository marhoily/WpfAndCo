using Microsoft.EntityFrameworkCore;

namespace NesViewer.Ui
{
    public class MessageContext : DbContext
    {
        public DbSet<TextMessage> Messages { get; protected set; }
        public DbSet<Conversation> Conversations { get; protected set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Visual Studio 2015 | Use the LocalDb 12 instance created by Visual Studio
            optionsBuilder.UseSqlServer(@"Server=(localdb)\msSqlLocaldb;Database=SampleDb;Trusted_Connection=True;");

            // Visual Studio 2013 | Use the LocalDb 11 instance created by Visual Studio
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\v11.0;Database=MessageDb;Trusted_Connection=True;");
        }
    }
}