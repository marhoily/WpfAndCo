using System;
using AutoMapper;
using Sample;

namespace Sample.Generated {
    public sealed class CreateCityHandler : IHandler<CreateCity>
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateCity, City>();
            })
            .CreateMapper();
		private readonly CityAggregate _aggregate;

		public CreateCityHandler(
			CityAggregate aggregate)
		{
			_aggregate = aggregate;
		}
		public void Handle(CreateCity commit)
		{
			_aggregate.ById.Add(commit.Id,
                Mapper.Map<City>(commit));
		}
    }
}

