using System.Collections.Generic;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class CsCity
    {
		public readonly List<City> Inserts
			= new List<City>();

		public readonly Dictionary<City.PK, City> Updates
			= new Dictionary<City.PK, City>();

		public readonly HashSet<City.PK> Deletes
			= new HashSet<City.PK>();
    }
}}

