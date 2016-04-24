using System.Collections.Generic;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class TablePerson
    {
        public readonly Dictionary<Person.PK, Person> 
            PrimaryKey = new Dictionary<Person.PK, Person>();
		public void Insert(Person item)
		{
			PrimaryKey.Add(item.GetKey(), item);
		}
		public void Update(Person item)
		{
			PrimaryKey[item.GetKey()] = item;
		}
		public void Delete(Person.PK key)
		{
			PrimaryKey.Remove(key);
		}
		public void Apply(CsPerson changes)
		{
			foreach (var item in changes.Inserts) Insert(item.Value);
			foreach (var item in changes.Updates) Update(item.Value);
			foreach (var item in changes.Deletes) Delete(item);
		}
    }
}}

