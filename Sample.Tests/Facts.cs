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
        private readonly EventPublisher _eventPublisher;

        public Facts()
        {
            var builder = new ContainerBuilder();
            var asm = Assembly.GetExecutingAssembly();

            builder.RegisterType<EventPublisher>()
                .SingleInstance();
            builder.RegisterAssemblyTypes(asm)
                .Where(t => t.Name.EndsWith("Aggregate"))
                .SingleInstance();
            builder.RegisterAssemblyTypes(asm)
                .Where(t => t.Name.EndsWith("Handler"))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(asm)
                .Where(t => t.Name.EndsWith("Validator"));
            _container = builder.Build();
            _eventPublisher = _container
                .Resolve<EventPublisher>();
        }

        [Fact]
        public void CreateAggregateWorks()
        {
            _eventPublisher
                .Publish(new CreateCity
                {
                    Id = Guid.NewGuid(),
                    Name = "Minsk"
                });
            _container.Resolve<CityAggregate>()
                .ById.Values.Single().Name
                .Should().Be("Minsk");
        }

        [Fact]
        public void Create_ManyToOne_CorrectKey()
        {
            _eventPublisher
                .Publish(new CreateCity
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
        [Fact]
        public void Create_ManyToOne_WrongKey()
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
        public void Update_ManyToOne_CorrectKey()
        {
            _eventPublisher
                .Publish(new UpdateCity
                {
                    Id = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc"),
                    Name = "Minsk"
                });
            _container
                .Resolve<UpdatePersonValidator>()
                .Validate(new UpdatePerson
                {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    City = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc")
                })
                .Should()
                .Be(ValidationResult.Success);
        }
        [Fact]
        public void Update_ManyToOne_WrongKey()
        {
            _container
                .Resolve<UpdatePersonValidator>()
                .Validate(new UpdatePerson
                {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    City = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc")
                })
                .ErrorMessage.Should()
                .Be("Wrong City: f89929f7-2969-48d3-a535-474a6ac824dc");
        }
    }
}
