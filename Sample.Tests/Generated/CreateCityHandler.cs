using AutoMapper;

namespace Sample.Generated {
    [IoC]
    public sealed class CreateCityHandler : IHandler<CreateCityComand>
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateCityComand, CityRow>();
                cfg.CreateMap<CreateCityComand, CityCreatedEvent>();
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
		public void Handle(CreateCityComand comand)
		{
			_aggregate.ById.Add(comand.Id,
                Mapper.Map<CityRow>(comand));
			_publisher.Publish(
                Mapper.Map<CityCreatedEvent>(comand));
		}
    }
}

