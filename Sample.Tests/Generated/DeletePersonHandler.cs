
using System;
using AutoMapper;
using Sample;

namespace Sample.Generated {
    public sealed class DeletePersonHandler : IHandler<DeletePerson>
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DeletePerson, Person>();
            })
            .CreateMapper();
		private readonly PersonAggregate _aggregate;

		public DeletePersonHandler(
			PersonAggregate aggregate)
		{
			_aggregate = aggregate;
		}
		public void Handle(DeletePerson commit)
		{
			_aggregate.ById.Add(commit.Id,
                Mapper.Map<Person>(commit));
		}
    }
}

