using System;
using System.Collections.Generic;

namespace Sample.Generated {
    public sealed class PersonAggregate
    {
		public readonly Dictionary<Guid, PersonRow> 
			ById = new Dictionary<Guid, PersonRow> ();
    }
    public sealed class PersonRow
    {
		public Guid Id { get; set; }
		public int RowVersion { get; set; }
		public String  Name { get; set; }
		public Guid  CityId { get; set; }
		public Guid  FavoriteCityId { get; set; }
    }
}
