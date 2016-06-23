using System;

namespace Sample.Generated {
	[CqrsEvent]
    public sealed class CreatePerson
    {
		public Guid Id { get; set; }
		public int RowVersion { get; } = 1;
		public int SchemaVersion { get; } = 1;
		public string  Name { get; set; }
		public Guid  CityId { get; set; }
		public Guid  FavoriteCityId { get; set; }
    }
}

