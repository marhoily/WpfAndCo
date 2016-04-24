using System.Collections.Generic;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class CsPerson
    {
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
				if (Deletes.Contains(key)) return null;
				Person result;
				if (Updates.TryGetValue(key, out result)) return result;
				if (Inserts.TryGetValue(key, out result)) return result;
				return null;
			}
		}
    }
}}

