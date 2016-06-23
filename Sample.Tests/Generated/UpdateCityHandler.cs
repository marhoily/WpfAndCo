using AutoMapper;

namespace Sample.Generated {
    [IoC]
    public sealed class UpdateCityHandler : IHandler<UpdateCityCommand>
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateCityCommand, CityRow>()
                    .ForMember(dst => dst.RowVersion,
                        opt => opt.ResolveUsing(src => src.RowVersion + 1));
                cfg.CreateMap<UpdateCityCommand, CityUpdatedEvent>();
            })
            .CreateMapper();
		private readonly EventPublisher _publisher;
		private readonly CityAggregate _aggregate;

		public UpdateCityHandler(EventPublisher publisher, CityAggregate aggregate)
		{
			_publisher = publisher;
			_aggregate = aggregate;
		}
		public void Handle(UpdateCityCommand command)
		{
			_aggregate.Update(Mapper.Map<CityRow>(command));
			_publisher.Publish(Mapper.Map<CityUpdatedEvent>(command));
		}
    }
}

