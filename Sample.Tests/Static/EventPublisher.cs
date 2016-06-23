using System.Collections.Generic;
using Autofac;

namespace Sample
{
    [IoC]
    public sealed class EventPublisher
    {
        private readonly ILifetimeScope _container;

        public EventPublisher(ILifetimeScope container)
        {
            _container = container;
        }

        public void Publish<TCommit>(TCommit commit)
        {
            var handlers = _container
                .Resolve<IEnumerable<IHandler<TCommit>>>();
            foreach (var handler in handlers)
                handler.Handle(commit);
        }
    }
}
