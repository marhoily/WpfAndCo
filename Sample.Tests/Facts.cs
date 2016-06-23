﻿using System;
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
            _eventPublisher = _container.Resolve<EventPublisher>();
            _cityAggregate = _container.Resolve<CityAggregate>();
        }

        private const string F89 = "f89929f7-2969-48d3-a535-474a6ac824dc";
        private const string D24 = "2488daeb-092c-4f93-a400-cab21fa85a95";
        private const string B70 = "0319b70d-5545-473d-9e71-ebb93a8141dc";
        private const string C04 = "0353c04a-c92c-43c5-b0a8-7c06c634c2d5";
        private readonly IContainer _container;
        private readonly EventPublisher _eventPublisher;
        private readonly CityAggregate _cityAggregate;

        [Fact]
        public void Create_ManyToOne_CorrectKey()
        {
            _eventPublisher
                .Publish(new CreateCity
                {
                    Id = new Guid(F89),
                    Name = "Minsk"
                });
            _container
                .Resolve<CreatePersonValidator>()
                .Validate(new CreatePerson
                {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    CityId = new Guid(F89)
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
                    CityId = new Guid(F89)
                })
                .ErrorMessage.Should()
                .Be($"Wrong CityId: {F89}");
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
            _cityAggregate
                .ById.Values.Single().Name
                .Should().Be("Minsk");
        }

        [Fact]
        public void Delete()
        {
            _eventPublisher.Publish(
                new CreateCity { Id = new Guid(F89), Name = "Minsk" });
            _cityAggregate.ById.Should().NotBeEmpty();

            _eventPublisher.Publish(
                new DeleteCity {Id = new Guid(F89)});
            _cityAggregate.ById.Should().BeEmpty();
        }

        [Fact]
        public void Delete_When_NotCreated()
        {
            _container
                .Resolve<DeleteCityValidator>()
                .Validate(new DeleteCity { Id = new Guid(C04) }) 
                .ErrorMessage.Should()
                .Be($"Did not find City to be Deleted: {C04}");
        }

        [Fact]
        public void Delete_When_There_Are_Dependencies()
        {
            _eventPublisher
                .Publish(new CreateCity
                {
                    Id = new Guid(F89),
                    Name = "Minsk"
                });
            _eventPublisher
                .Publish(new CreatePerson
                {
                    Id = new Guid(C04),
                    Name = "John",
                    CityId = new Guid(F89)
                });
            _container
                .Resolve<DeleteCityValidator>()
                .Validate(new DeleteCity
                {
                    Id = new Guid(F89)
                })
                .ErrorMessage.Should()
                .Be($"Can not delete City {F89} " +
                    $"because other objects depend on it: {C04}");
        }

        [Fact]
        public void Update_ManyToOne_CorrectKey()
        {
            _eventPublisher.Publish(
                new CreateCity
                {
                    Id = new Guid(F89),
                    Name = "Minsk"
                });
            _eventPublisher.Publish(
                new CreatePerson
                {
                    Id = new Guid(D24),
                    Name = "John",
                    CityId = new Guid(F89)
                });
            _container
                .Resolve<UpdatePersonValidator>()
                .Validate(new UpdatePerson
                {
                    Id = new Guid(D24),
                    Name = "John",
                    RowVersion = 1,
                    CityId = new Guid(F89)
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
                    Id = new Guid(F89),
                    Name = "Minsk"
                });
            _eventPublisher.Publish(
                new CreatePerson
                {
                    Id = new Guid(D24),
                    Name = "John",
                    CityId = new Guid(F89),
                    FavoriteCityId = new Guid(F89)
                });
            _container
                .Resolve<UpdatePersonValidator>()
                .Validate(new UpdatePerson
                {
                    Id = new Guid(D24),
                    RowVersion = 1,
                    Name = "John",
                    CityId = new Guid(F89)
                })
                .Should()
                .Be(ValidationResult.Success);
        }

        [Fact]
        public void Update_ManyToOne_Nullable_WrongKey()
        {
            _eventPublisher.Publish(
                new CreateCity
                {
                    Id = new Guid(F89),
                    Name = "Minsk"
                });
            _eventPublisher.Publish(
                new CreatePerson
                {
                    Id = new Guid(D24),
                    Name = "John",
                    CityId = new Guid(F89)
                });

            _container
                .Resolve<UpdatePersonValidator>()
                .Validate(new UpdatePerson
                {
                    Id = new Guid(D24),
                    RowVersion = 1,
                    Name = "John",
                    CityId = new Guid(F89),
                    FavoriteCityId = new Guid(B70)
                })
                .ErrorMessage.Should()
                .Be($"Wrong FavoriteCityId: {B70}");
        }

        [Fact]
        public void Update_ManyToOne_WrongKey()
        {
            _eventPublisher.Publish(
                new CreateCity
                {
                    Id = new Guid(F89),
                    Name = "Minsk"
                });
            _eventPublisher.Publish(
                new CreatePerson
                {
                    Id = new Guid(D24),
                    Name = "John",
                    CityId = new Guid(F89)
                });

            _container
                .Resolve<UpdatePersonValidator>()
                .Validate(new UpdatePerson
                {
                    Id = new Guid(D24),
                    RowVersion = 1,
                    Name = "John",
                    CityId = new Guid(B70)
                })
                .ErrorMessage.Should()
                .Be($"Wrong CityId: {B70}");
        }

        [Fact]
        public void Update_When_NotCreated()
        {
            _container
                .Resolve<UpdateCityValidator>()
                .Validate(new UpdateCity
                {
                    Id = new Guid(C04),
                    Name = "New York"
                })
                .ErrorMessage.Should()
                .Be($"Did not find City to be updated: {C04}");
        }

        [Fact]
        public void Update_When_Wrong_RowVersion()
        {
            _eventPublisher.Publish(
                new CreateCity
                {
                    Id = new Guid(F89),
                    Name = "Minsk"
                });
            _container
                .Resolve<UpdateCityValidator>()
                .Validate(new UpdateCity
                {
                    Id = new Guid(F89),
                    Name = "John",
                    RowVersion = 100
                })
                .ErrorMessage.Should()
                .Be("Can't update object v.1 with commit v.100");
        }

        // TODO: optimistic concurrency

        // TODO: events versioning
    }
}