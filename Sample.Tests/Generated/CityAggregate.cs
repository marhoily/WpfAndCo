using System;
using System.Collections.Generic;

namespace Sample.Generated {
    public sealed class CityAggregate
    {
		public readonly Dictionary<Guid, City> 
			ById = new Dictionary<Guid, City> ();
    }
    public sealed class CityRow
    {
		public Guid Id { get; set; }
		public String  Name { get; set; }
		public Guid  BrotherCityId { get; set; }
    }
}
