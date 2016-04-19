using System.Collections.Generic;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class CsPerson
    {
		public readonly List<Person> Inserts
			= new List<Person>();

		public readonly Dictionary<Person.PK, Person> Updates
			= new Dictionary<Person.PK, Person>();

		public readonly HashSet<Person.PK> Deletes
			= new HashSet<Person.PK>();
    }
}}

