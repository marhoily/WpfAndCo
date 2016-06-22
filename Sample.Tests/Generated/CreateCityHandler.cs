using AutoMapper;

namespace Sample.Generated {
    public sealed class CreateCityHandler
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreatePerson, Person>();
            })
            .CreateMapper();

		public void Handle(
			CreateCityAggregate aggregate, 
			CreateCity commit)
		{
			aggregate.ById.Add(commit.Id,
                Mapper.Map<City>(commit));
		}
    }
}

