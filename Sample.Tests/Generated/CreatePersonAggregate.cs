using System;
using System.Collections.Generic;

namespace Sample.Generated {
    public sealed partial class CreatePersonAggregate
    {
		public readonly Dictionary<Guid, Person> 
			ById = new Dictionary<Guid, Person> ();
    }
}
