using System;

namespace Sample.Generated {
    public sealed class UpdateCity
    {
		public Guid Id { get; set; }
		public int RowVersion { get; set; }
		public int SchemaVersion { get; } = 1;
		public String  Name { get; set; }
		public Guid  BrotherCityId { get; set; }
    }
}

