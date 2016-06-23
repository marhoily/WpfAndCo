using System;

namespace Sample.Generated {
	[CqrsEvent]
    public sealed class DeletePersonCommand
    {
		public Guid Id { get; set; }
		public int RowVersion { get; set; }
		public int SchemaVersion { get; } = 1;
    }
	[CqrsEvent]
    public sealed class PersonDeletedEvent
    {
		public Guid Id { get; set; }
		public int RowVersion { get; set; }
		public int SchemaVersion { get; } = 1;
    }
}

