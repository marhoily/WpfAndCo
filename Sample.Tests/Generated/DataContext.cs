




using System.Collections.Generic;

namespace Sample.Generated {
public partial class Raw {
    public sealed class DataContext
    {
        public readonly Dictionary<BinaryKey, object> 
            All = new Dictionary<BinaryKey, object>();
        public enum T
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

