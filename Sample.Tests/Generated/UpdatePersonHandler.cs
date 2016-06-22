
using System;
using AutoMapper;
using Sample;

namespace Sample.Generated {
    public sealed class UpdatePersonHandler : IHandler<UpdatePerson>
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdatePerson, Person>();
				cfg.CreateMap<Guid, City>();

            })
            .CreateMapper();
		private readonly PersonAggregate _aggregate;

		public UpdatePersonHandler(
			PersonAggregate aggregate)
		{
			_aggregate = aggregate;
		}
		public void Handle(UpdatePerson commit)
		{
			_aggregate.ById.Add(commit.Id,
                Mapper.Map<Person>(commit));
		}
    }
}

