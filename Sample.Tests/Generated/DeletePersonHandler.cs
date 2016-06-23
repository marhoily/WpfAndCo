namespace Sample.Generated {
    [IoC]
    public sealed class DeletePersonHandler : IHandler<DeletePerson>
    {
		private readonly PersonAggregate _aggregate;

		public DeletePersonHandler(
			PersonAggregate aggregate)
		{
			_aggregate = aggregate;
		}
		public void Handle(DeletePerson commit)
		{
			_aggregate.ById.Remove(commit.Id);
		}
    }
}

