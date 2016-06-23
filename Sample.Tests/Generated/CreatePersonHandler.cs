using AutoMapper;

namespace Sample.Generated {
    public sealed class CreatePersonHandler : IHandler<CreatePerson>
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreatePerson, PersonRow>();
            })
            .CreateMapper();
		private readonly PersonAggregate _aggregate;

		public CreatePersonHandler(
			PersonAggregate aggregate)
		{
			_aggregate = aggregate;
		}
		public void Handle(CreatePerson commit)
		{
			_aggregate.ById.Add(commit.Id,
                Mapper.Map<PersonRow>(commit));
		}
    }
}

