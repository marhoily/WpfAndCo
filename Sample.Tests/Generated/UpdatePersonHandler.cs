using AutoMapper;

namespace Sample.Generated {
    [IoC]
    public sealed class UpdatePersonHandler : IHandler<UpdatePersonCommand>
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdatePersonCommand, PersonRow>()
                    .ForMember(dst => dst.RowVersion,
                        opt => opt.ResolveUsing(src => src.RowVersion + 1));
                cfg.CreateMap<UpdatePersonCommand, PersonUpdatedEvent>();
            })
            .CreateMapper();
		private readonly EventPublisher _publisher;
		private readonly PersonAggregate _aggregate;

		public UpdatePersonHandler(EventPublisher publisher, PersonAggregate aggregate)
		{
			_publisher = publisher;
			_aggregate = aggregate;
		}
		public void Handle(UpdatePersonCommand command)
		{
			_aggregate.Update(Mapper.Map<PersonRow>(command));
			_publisher.Publish(Mapper.Map<PersonUpdatedEvent>(command));
		}
    }
}

