using System;

namespace Sample.Generated {
	[CqrsEvent]
    public sealed class DeleteCityCommand
    {
		public Guid Id { get; set; }
		public int RowVersion { get; set; }
		public int SchemaVersion { get; } = 1;
    }
	[CqrsEvent]
    public sealed class CityDeletedEvent
    {
		public Guid Id { get; set; }
		public int RowVersion { get; set; }
		public int SchemaVersion { get; } = 1;
    }
}

