using System;

namespace Sample.Generated {
	[CqrsEvent]
    public sealed class UpdateCity
    {
		public Guid Id { get; set; }
		public int RowVersion { get; set; }
		public int SchemaVersion { get; } = 1;
		public string  Name { get; set; }
		public Guid  BrotherCityId { get; set; }
    }
}

