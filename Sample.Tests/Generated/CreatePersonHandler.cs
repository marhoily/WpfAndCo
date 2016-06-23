using AutoMapper;

namespace Sample.Generated {
    [IoC]
    public sealed class CreatePersonHandler : IHandler<CreatePersonCommand>
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreatePersonCommand, PersonRow>();
                cfg.CreateMap<CreatePersonCommand, PersonCreatedEvent>();
            })
            .CreateMapper();
		private readonly EventPublisher _publisher;
		private readonly PersonAggregate _aggregate;

		public CreatePersonHandler(
			EventPublisher publisher,
			PersonAggregate aggregate)
		{
			_publisher = publisher;
			_aggregate = aggregate;
		}
		public void Handle(CreatePersonCommand command)
		{
			_aggregate.Create(Mapper.Map<PersonRow>(command));
			_publisher.Publish(Mapper.Map<PersonCreatedEvent>(command));
		}
    }
}

