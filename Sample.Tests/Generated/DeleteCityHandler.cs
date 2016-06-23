namespace Sample.Generated {
    public sealed class DeleteCityHandler : IHandler<DeleteCity>
    {
		private readonly CityAggregate _aggregate;

		public DeleteCityHandler(
			CityAggregate aggregate)
		{
			_aggregate = aggregate;
		}
		public void Handle(DeleteCity commit)
		{
			_aggregate.ById.Remove(commit.Id);
		}
    }
}

