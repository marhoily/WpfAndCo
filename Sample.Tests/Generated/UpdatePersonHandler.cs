using AutoMapper;

namespace Sample.Generated {
    [IoC]
    public sealed class UpdatePersonHandler : IHandler<UpdatePerson>
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdatePerson, PersonRow>()
                    .ForMember(dst => dst.RowVersion,
                        opt => opt.ResolveUsing(src => src.RowVersion + 1));
            })
            .CreateMapper();
		private readonly EventPublisher _publisher;
		private readonly PersonAggregate _aggregate;

		public UpdatePersonHandler(
			EventPublisher publisher,
			PersonAggregate aggregate)
		{
			_publisher = publisher;
			_aggregate = aggregate;
		}
		public void Handle(UpdatePerson commit)
		{
			_aggregate.ById[commit.Id] =
                Mapper.Map<PersonRow>(commit);
		}
    }
}

