
using System;
using AutoMapper;
using Sample;

namespace Sample.Generated {
    public sealed class DeleteCityHandler : IHandler<DeleteCity>
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DeleteCity, City>();
            })
            .CreateMapper();
		private readonly CityAggregate _aggregate;

		public DeleteCityHandler(
			CityAggregate aggregate)
		{
			_aggregate = aggregate;
		}
		public void Handle(DeleteCity commit)
		{
			_aggregate.ById.Add(commit.Id,
                Mapper.Map<City>(commit));
		}
    }
}

