using System.Collections.Generic;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class DataContext
    {
        public enum E
        {
		    City,
		    Person,
        }
        public readonly Dictionary<City.PK, City> 
            PkCity = new Dictionary<City.PK, City>();
        public readonly Dictionary<Person.PK, Person> 
            PkPerson = new Dictionary<Person.PK, Person>();
    }
}}

