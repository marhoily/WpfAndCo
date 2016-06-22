using System;
using System.Collections.Generic;

namespace Sample.Generated {
    public sealed class CityAggregate
    {
		public readonly Dictionary<Guid, City> 
			ById = new Dictionary<Guid, City> ();
    }
}
