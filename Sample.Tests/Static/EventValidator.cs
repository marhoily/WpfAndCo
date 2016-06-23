using System.ComponentModel.DataAnnotations;
using Autofac;

namespace Sample
{
    [IoC]
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