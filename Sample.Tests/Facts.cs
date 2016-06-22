using System;
using System.Linq;
using System.Reflection;
using Autofac;
using FluentAssertions;
using Sample.Generated;
using Xunit;

namespace Sample
{
    public sealed class Facts
    {
        private readonly IContainer _container;

        public Facts()
        {
            var builder = new ContainerBuilder();
            var asm = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(asm)
                .Where(t => t.Name.EndsWith("Aggregate"))
                .SingleInstance();
            builder.RegisterAssemblyTypes(asm)
                .Where(t => t.Name.EndsWith("Handler"));
            _container = builder.Build();
        }

        [Fact]
        public void CreateAggregateWorks()
        {
            _container
                .Resolve<CreateCityHandler>()
                .Handle(new CreateCity
                {
                    Id = Guid.NewGuid(),
                    Name = "Minsk"
                });
            _container.Resolve<CreateCityAggregate>()
                .ById.Values.Single().Name
                .Should().Be("Minsk");
        }
        [Fact]
        public void ManyToOneIntegrityCheck()
        {
            _container
                .Resolve<CreatePersonHandler>()
                .Handle(new CreatePerson
                {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    City = Guid.NewGuid()
                });
            _container.Resolve<CreatePersonAggregate>()
                .ById.Values.Single().Name
                .Should().Be("John");
        }
    }
}
