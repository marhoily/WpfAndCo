using System;
using System.ComponentModel.DataAnnotations;
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
            builder.RegisterAssemblyTypes(asm)
                .Where(t => t.Name.EndsWith("Validator"));
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
        public void ManyToOne_WrongKey()
        {
            _container
                .Resolve<CreatePersonValidator>()
                .Validate(new CreatePerson
                {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    City = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc")
                })
                .ErrorMessage.Should()
                .Be("Wrong City: f89929f7-2969-48d3-a535-474a6ac824dc");
        }
        [Fact]
        public void ManyToOne_CorrectKey()
        {
            _container
                .Resolve<CreateCityHandler>()
                .Handle(new CreateCity
                {
                    Id = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc"),
                    Name = "Minsk"
                });
            _container
                .Resolve<CreatePersonValidator>()
                .Validate(new CreatePerson
                {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    City = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc")
                })
                .Should()
                .Be(ValidationResult.Success);
        }
    }
}
