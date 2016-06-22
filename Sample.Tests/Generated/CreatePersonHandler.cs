using AutoMapper;

namespace Sample.Generated {
    public sealed class CreatePersonHandler
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreatePerson, Person>();
            })
            .CreateMapper();

		public void Handle(
			CreatePersonAggregate aggregate, 
			CreatePerson commit)
		{
			aggregate.ById.Add(commit.Id,
                Mapper.Map<Person>(commit));
		}
    }
}

