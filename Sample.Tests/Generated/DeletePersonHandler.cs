using AutoMapper;

namespace Sample.Generated {
    [IoC]
    public sealed class DeletePersonHandler : IHandler<DeletePersonCommand>
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DeletePersonCommand, PersonDeletedEvent>();
            })
            .CreateMapper();
		private readonly EventPublisher _publisher;
		private readonly PersonAggregate _aggregate;

		public DeletePersonHandler(
			EventPublisher publisher,
			PersonAggregate aggregate)
		{
			_publisher = publisher;
			_aggregate = aggregate;
		}
		public void Handle(DeletePersonCommand command)
		{
			_aggregate.Remove(command.Id);
			_publisher.Publish(Mapper.Map<PersonDeletedEvent>(command));
		}
    }
}

