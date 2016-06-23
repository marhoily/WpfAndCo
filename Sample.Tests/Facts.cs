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
        private const string F89 = "f89929f7-2969-48d3-a535-474a6ac824dc";
        private const string D24 = "d248daeb-092c-4f93-a400-cab21fa85a95";
        private const string B70 = "b709b70d-5545-473d-9e71-ebb93a8141dc";
        private const string C04 = "c043c04a-c92c-43c5-b0a8-7c06c634c2d5";
        private readonly EventPublisher _publisher;
        private readonly CityAggregate _cityAggregate;
        private readonly EventValidator _validator;

        public Facts()
        {
            var builder = new ContainerBuilder();
            var asm = Assembly.GetExecutingAssembly();

            builder.RegisterType<EventPublisher>().SingleInstance();
            builder.RegisterType<EventValidator>().SingleInstance();
            builder.RegisterAssemblyTypes(asm)
                .Where(t => t.Name.EndsWith("Aggregate"))
                .SingleInstance();
            builder.RegisterAssemblyTypes(asm)
                .Where(t => t.Name.EndsWith("Handler"))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(asm)
                .Where(t => t.Name.EndsWith("Validator"))
                .AsImplementedInterfaces();
            var container = builder.Build();
            _publisher = container.Resolve<EventPublisher>();
            _validator = container.Resolve<EventValidator>();
            _cityAggregate = container.Resolve<CityAggregate>();
        }

        [Fact]
        public void Create_ManyToOne_CorrectKey()
        {
            _publisher.Publish(new CreateCityCommand
                {
                    Id = new Guid(F89),
                    Name = "Minsk"
                });
            _validator.Validate(new CreatePersonCommand
            {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    CityId = new Guid(F89)
                })
                .Should().Be(ValidationResult.Success);
        }

        [Fact]
        public void Create_ManyToOne_WrongKey()
        {
            _validator.Validate(new CreatePersonCommand
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
            _publisher.Publish(new CreateCityCommand
            {
                    Id = Guid.NewGuid(),
                    Name = "Minsk"
                });
            _cityAggregate.ById.Values.Single()
                .Name.Should().Be("Minsk");
        }

        [Fact]
        public void Delete()
        {
            _publisher.Publish(
                new CreateCityCommand { Id = new Guid(F89), Name = "Minsk" });
            _cityAggregate.ById.Should().NotBeEmpty();

            _publisher.Publish(new DeleteCityCommand {Id = new Guid(F89)});
            _cityAggregate.ById.Should().BeEmpty();
        }

        [Fact]
        public void Delete_When_NotCreated()
        {
            _validator.Validate(new DeleteCityCommand { Id = new Guid(C04) }) 
                .ErrorMessage.Should()
                .Be($"Did not find City to be deleted: {C04}");
        }

        [Fact]
        public void Delete_When_There_Are_Dependencies()
        {
            _publisher.Publish(new CreateCityCommand
            {
                    Id = new Guid(F89),
                    Name = "Minsk"
                });
            _publisher.Publish(new CreatePersonCommand
            {
                    Id = new Guid(C04),
                    Name = "John",
                    CityId = new Guid(F89)
                });
            _validator.Validate(new DeleteCityCommand
            {
                    Id = new Guid(F89), RowVersion = 1
                })
                .ErrorMessage.Should()
                .Be($"Can not delete City {F89} " +
                    $"because other objects depend on it: {C04}");
        }

        [Fact]
        public void Update_ManyToOne_CorrectKey()
        {
            _publisher.Publish(new CreateCityCommand
            {
                    Id = new Guid(F89),
                    Name = "Minsk"
                });
            _publisher.Publish(new CreatePersonCommand
            {
                    Id = new Guid(D24),
                    Name = "John",
                    CityId = new Guid(F89)
                });
            _validator.Validate(new UpdatePersonCommand
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
            _publisher.Publish(new CreateCityCommand
            {
                    Id = new Guid(F89),
                    Name = "Minsk"
                });
            _publisher.Publish(new CreatePersonCommand
            {
                    Id = new Guid(D24),
                    Name = "John",
                    CityId = new Guid(F89),
                    FavoriteCityId = new Guid(F89)
                });
            _validator.Validate(new UpdatePersonCommand
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
            _publisher.Publish(new CreateCityCommand
            {
                    Id = new Guid(F89),
                    Name = "Minsk"
                });
            _publisher.Publish(new CreatePersonCommand
            {
                    Id = new Guid(D24),
                    Name = "John",
                    CityId = new Guid(F89)
                });

            _validator.Validate(new UpdatePersonCommand
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
            _publisher.Publish(new CreateCityCommand
            {
                    Id = new Guid(F89),
                    Name = "Minsk"
                });
            _publisher.Publish(new CreatePersonCommand
            {
                    Id = new Guid(D24),
                    Name = "John",
                    CityId = new Guid(F89)
                });

            _validator.Validate(new UpdatePersonCommand
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
            _validator.Validate(new UpdateCityCommand
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
            _publisher.Publish(new CreateCityCommand
            {
                    Id = new Guid(F89),
                    Name = "Minsk"
                });
            _validator.Validate(new UpdateCityCommand
                {
                    Id = new Guid(F89),
                    Name = "John",
                    RowVersion = 100
                })
                .ErrorMessage.Should()
                .Be("Can't update object v.1 with command v.100");
        }

        [Fact]
        public void Update_Should_Increment_RowVersoion()
        {
            _publisher.Publish(new CreateCityCommand
                {
                    Id = new Guid(F89),
                    Name = "Minsk"
                });
            _publisher.Publish(new UpdateCityCommand
            {
                    Id = new Guid(F89),
                    Name = "Moscow",
                    RowVersion = 1
                });
            _cityAggregate.ById[new Guid(F89)].RowVersion.Should().Be(2);
        }

        [Fact]
        public void Delete_When_Wrong_RowVersion()
        {
            _publisher.Publish(
                new CreateCityCommand
                {
                    Id = new Guid(F89),
                    Name = "Minsk"
                });
            _validator.Validate(new DeleteCityCommand
            {
                    Id = new Guid(F89),
                    RowVersion = 100
                }).ErrorMessage.Should()
                .Be("Can't delete object v.1 with command v.100");
        }
        //todo: CommandSender, EventPublisher
        //todo: aggregates should properly encapsulate state
        //todo: test that commands send events
        //todo: versioning through interfaces
        //todo: validate grandchildren
        //todo: segregate integrity?
        //todo: delete: cascade/null/deny
    }
}