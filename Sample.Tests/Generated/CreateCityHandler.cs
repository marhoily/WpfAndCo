using AutoMapper;

namespace Sample.Generated {
    [IoC]
    public sealed class CreateCityHandler : IHandler<CreateCityCommand>
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateCityCommand, CityRow>();
                cfg.CreateMap<CreateCityCommand, CityCreatedEvent>();
            })
            .CreateMapper();
		private readonly EventPublisher _publisher;
		private readonly CityAggregate _aggregate;

		public CreateCityHandler(
			EventPublisher publisher,
			CityAggregate aggregate)
		{
			_publisher = publisher;
			_aggregate = aggregate;
		}
		public void Handle(CreateCityCommand command)
		{
			_aggregate.ById.Add(command.Id, Mapper.Map<CityRow>(command));
			_publisher.Publish(Mapper.Map<CityCreatedEvent>(command));
		}
    }
}

