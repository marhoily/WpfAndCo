using AutoMapper;

namespace Sample.Generated {
    [IoC]
    public sealed class CreatePersonHandler : IHandler<CreatePersonComand>
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreatePersonComand, PersonRow>();
                cfg.CreateMap<CreatePersonComand, PersonCreatedEvent>();
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
		public void Handle(CreatePersonComand comand)
		{
			_aggregate.ById.Add(comand.Id,
                Mapper.Map<PersonRow>(comand));
			_publisher.Publish(
                Mapper.Map<PersonCreatedEvent>(comand));
		}
    }
}

