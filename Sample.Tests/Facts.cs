using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Autofac;
using FluentAssertions;
using Sample.Generated;
using Xunit;
using Xunit.Sdk;

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
                    CityId = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc")
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
                    CityId = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc")
                })
                .ErrorMessage.Should()
                .Be("Wrong CityId: f89929f7-2969-48d3-a535-474a6ac824dc");
        }

        [Fact]
        public void Update_When_NotCreated()
        {
            _container
                .Resolve<UpdateCityValidator>()
                .Validate(new UpdateCity
                {
                    Id = new Guid("0353c04a-c92c-43c5-b0a8-7c06c634c2d5"),
                    Name = "New York"
                })
                .ErrorMessage.Should()
                .Be("Did not find City to be updated: 0353c04a-c92c-43c5-b0a8-7c06c634c2d5");
        }
        [Fact]
        public void Update_ManyToOne_CorrectKey()
        {
            _eventPublisher.Publish(
                new CreateCity
                {
                    Id = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc"),
                    Name = "Minsk"
                });
            _eventPublisher.Publish(
                new CreatePerson
                {
                    Id = new Guid("2488daeb-092c-4f93-a400-cab21fa85a95"),
                    Name = "John",
                    CityId = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc")
                });
            _container
                .Resolve<UpdatePersonValidator>()
                .Validate(new UpdatePerson
                {
                    Id = new Guid("2488daeb-092c-4f93-a400-cab21fa85a95"),
                    Name = "John",
                    CityId = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc")
                })
                .Should()
                .Be(ValidationResult.Success);
        }
        [Fact]
        public void Update_ManyToOne_Nullable_CorrectKey()
        {
            _eventPublisher.Publish(
                new CreateCity
                {
                    Id = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc"),
                    Name = "Minsk"
                });
            _eventPublisher.Publish(
                new CreatePerson
                {
                    Id = new Guid("2488daeb-092c-4f93-a400-cab21fa85a95"),
                    Name = "John",
                    CityId = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc"),
                    FavoriteCityId = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc")
                });
            _container
                .Resolve<UpdatePersonValidator>()
                .Validate(new UpdatePerson
                {
                    Id = new Guid("2488daeb-092c-4f93-a400-cab21fa85a95"),
                    Name = "John",
                    CityId = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc")
                })
                .Should()
                .Be(ValidationResult.Success);
        }
        [Fact]
        public void Update_ManyToOne_WrongKey()
        {
            _eventPublisher.Publish(
                new CreateCity
                {
                    Id = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc"),
                    Name = "Minsk"
                });
            _eventPublisher.Publish(
                new CreatePerson
                {
                    Id = new Guid("2488daeb-092c-4f93-a400-cab21fa85a95"),
                    Name = "John",
                    CityId = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc")
                });

            _container
                .Resolve<UpdatePersonValidator>()
                .Validate(new UpdatePerson
                {
                    Id = new Guid("2488daeb-092c-4f93-a400-cab21fa85a95"),
                    Name = "John",
                    CityId = new Guid("0319b70d-5545-473d-9e71-ebb93a8141dc")
                })
                .ErrorMessage.Should()
                .Be("Wrong CityId: 0319b70d-5545-473d-9e71-ebb93a8141dc");
        }
        [Fact]
        public void Update_ManyToOne_Nullable_WrongKey()
        {
            _eventPublisher.Publish(
                new CreateCity
                {
                    Id = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc"),
                    Name = "Minsk"
                });
            _eventPublisher.Publish(
                new CreatePerson
                {
                    Id = new Guid("2488daeb-092c-4f93-a400-cab21fa85a95"),
                    Name = "John",
                    CityId = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc")
                });

            _container
                .Resolve<UpdatePersonValidator>()
                .Validate(new UpdatePerson
                {
                    Id = new Guid("2488daeb-092c-4f93-a400-cab21fa85a95"),
                    Name = "John",
                    CityId = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc"),
                    FavoriteCityId = new Guid("0319b70d-5545-473d-9e71-ebb93a8141dc")
                })
                .ErrorMessage.Should()
                .Be("Wrong FavoriteCityId: 0319b70d-5545-473d-9e71-ebb93a8141dc");
        }
        [Fact]
        public void Delete()
        {
            _eventPublisher
                .Publish(new CreateCity
                {
                    Id = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc"),
                    Name = "Minsk"
                });
            _container.Resolve<CityAggregate>().ById.Should().NotBeEmpty();
            _eventPublisher
                .Publish(new DeleteCity
                {
                    Id = new Guid("f89929f7-2969-48d3-a535-474a6ac824dc"),
                });
            _container.Resolve<CityAggregate>().ById.Should().BeEmpty();
        }
        [Fact]
        public void Delete_When_NotCreated()
        {
            _container
                .Resolve<DeleteCityValidator>()
                .Validate(new DeleteCity
                {
                    Id = new Guid("0353c04a-c92c-43c5-b0a8-7c06c634c2d5"),
                })
                .ErrorMessage.Should()
                .Be("Did not find City to be Deleted: 0353c04a-c92c-43c5-b0a8-7c06c634c2d5");
        }

        // TODO: cascade deleting
        // TODO: null when deleting
    }
}
