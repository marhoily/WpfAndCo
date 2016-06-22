using System;
using System.Collections.Generic;

namespace Sample.Generated {
    public sealed class PersonAggregate
    {
		public readonly Dictionary<Guid, Person> 
			ById = new Dictionary<Guid, Person> ();
    }
}
