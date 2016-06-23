using AutoMapper;

namespace Sample.Generated {
    [IoC]
    public sealed class DeleteCityHandler : IHandler<DeleteCityCommand>
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DeleteCityCommand, CityDeletedEvent>();
            })
            .CreateMapper();
		private readonly EventPublisher _publisher;
		private readonly CityAggregate _aggregate;

		public DeleteCityHandler(
			EventPublisher publisher,
			CityAggregate aggregate)
		{
			_publisher = publisher;
			_aggregate = aggregate;
		}
		public void Handle(DeleteCityCommand command)
		{
			_aggregate.Remove(command.Id);
			_publisher.Publish(Mapper.Map<CityDeletedEvent>(command));
		}
    }
}

