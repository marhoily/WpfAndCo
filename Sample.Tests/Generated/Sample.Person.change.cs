using System;
using System.Collections.Generic;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class CsPerson
    {
		private readonly TablePerson _table;
		public CsPerson(TablePerson table) { _table = table; }

		public readonly Dictionary<Person.PK, Person> Inserts
			= new Dictionary<Person.PK, Person>();

		public readonly Dictionary<Person.PK, Person> Updates
			= new Dictionary<Person.PK, Person>();

		public readonly HashSet<Person.PK> Deletes
			= new HashSet<Person.PK>();

		public Person this[Person.PK key]
		{
			get 
			{
				Person original;
				Person inserted;
				if (!_table.PrimaryKey.TryGetValue(key, out original)) 
					return Inserts.TryGetValue(key, out inserted) ? inserted : null;
				if (Deletes.Contains(key)) return null;
				Person result;
				return Updates.TryGetValue(key, out result) ? result : original;
			}
		}
		//public Person GetOrAdd(Person.PK key)
		//{
		//	if (Deletes.Contains(key)) throw new InvalidArgumentException();
		//	Person result;
		//	if (Updates.TryGetValue(key, out result)) return result;
		//	result = new 
		//	Inserts.TryGetValue(key, out result)) return result;
		//	return null;
		//
		//}
    }
}}

