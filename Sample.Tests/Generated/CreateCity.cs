using System;

namespace Sample.Generated {
	[CqrsEvent]
    public sealed class CreateCity
    {
		public Guid Id { get; set; }
		public int RowVersion { get; } = 1;
		public int SchemaVersion { get; } = 1;
		public string  Name { get; set; }
		public Guid  BrotherCityId { get; set; }
    }
}

