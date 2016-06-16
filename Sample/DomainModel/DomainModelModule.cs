using Autofac;

namespace Configurator
{
    public class DomainModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventStore>().SingleInstance();
            builder.RegisterType<AgentSearchAggregate>();
            builder.RegisterType<AgentConfigurationAggregate>();
        }
    }
}