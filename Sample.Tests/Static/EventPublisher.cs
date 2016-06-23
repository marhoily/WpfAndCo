using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Autofac;

namespace Sample
{
    public interface IHandler<in T>
    {
        void Handle(T commit);
    }
    public interface IValidator<in T>
    {
        ValidationResult Validate(T commit);
    }
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
    public sealed class EventValidator
    {
        private readonly ILifetimeScope _container;

        public EventValidator(ILifetimeScope container)
        {
            _container = container;
        }

        public ValidationResult Validate<TCommit>(TCommit commit)
        {
            return _container
                .Resolve<IValidator<TCommit>>()
                .Validate(commit);
        }
    }
}
