namespace Sample.Generated {
    [IoC]
    public sealed class DeleteCityHandler : IHandler<DeleteCityCommand>
    {
		private readonly EventPublisher _publisher;
		private readonly CityAggregate _aggregate;

		public DeleteCityHandler(
			EventPublisher publisher,
			CityAggregate aggregate)
		{
			_publisher = publisher;
			_aggregate = aggregate;
		}
		public void Handle(DeleteCityCommand command)
		{
			_aggregate.ById.Remove(command.Id);
		}
    }
}

