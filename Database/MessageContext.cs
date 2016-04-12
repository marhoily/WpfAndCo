using JetBrains.Annotations;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace Dpb.MessageDb
{
    public interface IMessageDbLog
    {
        void CreateOptions(string dbId);
    }
    [PublicAPI]
    public class MessageContext : DbContext
    {
        private bool _calledFromScript;
        public DbSet<Message> Messages { get; protected set; }
        public DbSet<Conversation> Conversations { get; protected set; }

        /// <summary> EF migration mechanism uses this ctor </summary>
        public MessageContext() { _calledFromScript = true; }

        public MessageContext([NotNull] DbContextOptions options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_calledFromScript)
            {
                // Visual Studio 2015 | Use the LocalDb 12 instance created by Visual Studio
                //optionsBuilder.UseSqlServer(@"Server=(localdb)\msSqlLocaldb;Database=MessageDb;Trusted_Connection=True;");

                // Visual Studio 2013 | Use the LocalDb 11 instance created by Visual Studio
                optionsBuilder.UseSqlServer(@"Server=(localdb)\v11.0;Database=MessageDb;Trusted_Connection=True;");

                // SQL Express on TRUSTEDAPP1
                //optionsBuilder.UseSqlServer(@"Server=172.16.13.20\RTCLOCAL;Database=MessageDb;Trusted_Connection=True;");
            }
        }
    }
}