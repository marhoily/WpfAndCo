




using System.Collections.Generic;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    public sealed class DataContext
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

        public void ReadDeletes(BinaryReader reader)
        {
            var count = reader.ReadInt32();
            for (var i = 0; i < count; i++)
            {
                switch (reader.ReadEnum<E>())
                {

                    case E.City: {
                            var k = City.ReadPk(reader);
                            PkCity.Remove(k);
                        }
                        break;

                    case E.Person: {
                            var k = Person.ReadPk(reader);
                            PkPerson.Remove(k);
                        }
                        break;

                }
            }
        }
    }
}}

