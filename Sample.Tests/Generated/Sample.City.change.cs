using System.Collections.Generic;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class CsCity
    {
		public readonly Dictionary<City.PK, City> Inserts
			= new Dictionary<City.PK, City>();

		public readonly Dictionary<City.PK, City> Updates
			= new Dictionary<City.PK, City>();

		public readonly HashSet<City.PK> Deletes
			= new HashSet<City.PK>();

		public City this[City.PK key]
		{
			get 
			{
				if (Deletes.Contains(key)) return null;
				City result;
				if (Updates.TryGetValue(key, out result)) return result;
				if (Inserts.TryGetValue(key, out result)) return result;
				return null;
			}
		}
    }
}}

