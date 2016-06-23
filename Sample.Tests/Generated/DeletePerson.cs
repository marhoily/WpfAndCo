using System;

namespace Sample.Generated {
	[CqrsEvent]
    public sealed class DeletePerson
    {
		public Guid Id { get; set; }
		public int RowVersion { get; set; }
		public int SchemaVersion { get; } = 1;
    }
}

