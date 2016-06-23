using System.Collections.Generic;
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
            var validators = _container.Resolve
                <IEnumerable<IValidator<TCommit>>>();
            foreach (var validator in validators)
            {
                var result = validator.Validate(commit);
                if (result != ValidationResult.Success)
                    return result;
            }
            return ValidationResult.Success;
        }
    }
}