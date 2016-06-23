namespace Sample.Generated {
    [IoC]
    public sealed class DeletePersonHandler : IHandler<DeletePersonCommand>
    {
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
			_aggregate.ById.Remove(command.Id);
		}
    }
}

