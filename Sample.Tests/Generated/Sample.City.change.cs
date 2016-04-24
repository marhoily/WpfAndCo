using System;
using System.Collections.Generic;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class CsCity
    {
		private readonly TableCity _table;
		public CsCity(TableCity table) { _table = table; }

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
				City original;
				City inserted;
				if (!_table.PrimaryKey.TryGetValue(key, out original)) 
					return Inserts.TryGetValue(key, out inserted)
						? inserted
					    : null;
				if (Deletes.Contains(key)) return null;
				City result;
				return Updates.TryGetValue(key, out result) 
					? result 
					: original;
			}
		}
		//public City GetOrAdd(City.PK key)
		//{
		//	if (Deletes.Contains(key)) throw new InvalidArgumentException();
		//	City result;
		//	if (Updates.TryGetValue(key, out result)) return result;
		//	result = new 
		//	Inserts.TryGetValue(key, out result)) return result;
		//	return null;
		//
		//}
    }
}}

