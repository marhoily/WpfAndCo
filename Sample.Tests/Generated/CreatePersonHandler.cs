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
		private readonly CreatePersonAggregate _aggregate;

		public CreatePersonHandler(
			CreatePersonAggregate aggregate)
		{
			_aggregate = aggregate;
		}
		public void Handle(CreatePerson commit)
		{
			_aggregate.ById.Add(commit.Id,
                Mapper.Map<Person>(commit));
		}
    }
}

