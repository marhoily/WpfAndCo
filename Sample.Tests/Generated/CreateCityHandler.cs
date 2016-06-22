using AutoMapper;

namespace Sample.Generated {
    public sealed class CreateCityHandler
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateCity, City>();
            })
            .CreateMapper();
		private readonly CreateCityAggregate _aggregate;

		public CreateCityHandler(
			CreateCityAggregate aggregate)
		{
			_aggregate = aggregate;
		}
		public void Handle(CreateCity commit)
		{
			_aggregate.ById.Add(commit.Id,
                Mapper.Map<City>(commit));
		}
    }
}

