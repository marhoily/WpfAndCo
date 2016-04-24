using System.Collections.Generic;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class ChangeSet
    {
		public ChangeSet(TableSet tables)
		{
			City = new CsCity(tables.City);
			Person = new CsPerson(tables.Person);
		}
        public enum E
        {
		    City,
		    Person,
        }
        public readonly CsCity City;
        public void Add(City item)
        {
			City.Inserts[item.GetKey()] = item;
        }                              
        public void Update(City item)
        {
			City.Updates[item.GetKey()] = item;
        }                              
        public void Remove(City.PK key)
        {
			City.Deletes.Add(key);
        }
				                             
        public readonly CsPerson Person;
        public void Add(Person item)
        {
			Person.Inserts[item.GetKey()] = item;
        }                              
        public void Update(Person item)
        {
			Person.Updates[item.GetKey()] = item;
        }                              
        public void Remove(Person.PK key)
        {
			Person.Deletes.Add(key);
        }
				                             
    }
}}

