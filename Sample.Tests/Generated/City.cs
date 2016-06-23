using System;
using System.Collections.Generic;

namespace Sample.Generated {
	[IoC]
    public sealed class CityAggregate
    {
		public readonly Dictionary<Guid, CityRow> 
			ById = new Dictionary<Guid, CityRow> ();
    }
	[Dto]
    public sealed class CityRow
    {
		public Guid Id { get; set; }
		public int RowVersion { get; set; }
		public String  Name { get; set; }
		public Guid  BrotherCityId { get; set; }
    }
}
