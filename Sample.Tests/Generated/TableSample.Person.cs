using System.Collections.Generic;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class TablePerson
    {
        public readonly Dictionary<Person.PK, Person> 
            PkPerson = new Dictionary<Person.PK, Person>();
		public void Insert(Person item){}
		public void Update(Person item){}
		public void Delete(Person.PK key){}
    }
}}

