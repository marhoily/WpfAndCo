using System;

namespace Sample.Generated {
	[CqrsEvent]
    public sealed class PersonUpdatedEvent
    {
		public Guid Id { get; set; }
		public int RowVersion { get; set; }
		public int SchemaVersion { get; } = 1;
		public string  Name { get; set; }
		public Guid  CityId { get; set; }
		public Guid  FavoriteCityId { get; set; }
    }
	[CqrsCommand]
    public sealed class UpdatePersonCommand
    {
		public Guid Id { get; set; }
		public int RowVersion { get; set; }
		public int SchemaVersion { get; } = 1;
		public string  Name { get; set; }
		public Guid  CityId { get; set; }
		public Guid  FavoriteCityId { get; set; }
    }
}

