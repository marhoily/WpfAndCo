using System.Collections.Generic;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class ChangeSet
    {
        public enum E
        {
		    City,
		    Person,
        }
        public readonly CsCity City = new CsCity();

        public void Add(City item)
        {
			City.Inserts.Add(item);
        }                              
        public void Update(City item)
        {
			City.Updates[item.GetKey()] = item;
        }                              
        public void Remove(City item)
        {
			City.Deletes.Add(item.GetKey());
        }                              
        public readonly CsPerson Person = new CsPerson();

        public void Add(Person item)
        {
			Person.Inserts.Add(item);
        }                              
        public void Update(Person item)
        {
			Person.Updates[item.GetKey()] = item;
        }                              
        public void Remove(Person item)
        {
			Person.Deletes.Add(item.GetKey());
        }                              
    }
}}

