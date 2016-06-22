
using System;
using AutoMapper;
using Sample;

namespace Sample.Generated {
    public sealed class UpdateCityHandler : IHandler<UpdateCity>
    {
        private static readonly IMapper Mapper = 
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateCity, CityRow>();
            })
            .CreateMapper();
		private readonly CityAggregate _aggregate;

		public UpdateCityHandler(
			CityAggregate aggregate)
		{
			_aggregate = aggregate;
		}
		public void Handle(UpdateCity commit)
		{
			_aggregate.ById.Add(commit.Id,
                Mapper.Map<CityRow>(commit));
		}
    }
}

