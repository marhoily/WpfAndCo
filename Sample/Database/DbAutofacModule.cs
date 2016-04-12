using Autofac;
using Microsoft.Data.Entity;

namespace Sample
{
    public interface IDbConfig
    {
        string CommunicationDb { get; set; }
    }

    public sealed class MessageDbAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => 
                UseSqlServer(context.Resolve<IDbConfig>()));
        }

        private static MessageContext UseSqlServer(IDbConfig dbConfig)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MessageContext>();
            optionsBuilder.UseSqlServer(dbConfig.CommunicationDb);
            return new MessageContext(optionsBuilder.Options);
        }
    }
}