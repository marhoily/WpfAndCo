using AutoMapper;

namespace Sample.Generated {
    [IoC]
    public sealed class UpdateCityHandler : IHandler<UpdateCity>
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateCity, CityRow>()
                    .ForMember(dst => dst.RowVersion,
                        opt => opt.ResolveUsing(src => src.RowVersion + 1));
            })
            .CreateMapper();
		private readonly EventPublisher _publisher;
		private readonly CityAggregate _aggregate;

		public UpdateCityHandler(
			EventPublisher publisher,
			CityAggregate aggregate)
		{
			_publisher = publisher;
			_aggregate = aggregate;
		}
		public void Handle(UpdateCity commit)
		{
			_aggregate.ById[commit.Id] =
                Mapper.Map<CityRow>(commit);
		}
    }
}

