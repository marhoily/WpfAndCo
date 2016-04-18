




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

        public void ReadInserts(BinaryReader reader)
        {
            var count = reader.ReadInt32();
            for (var i = 0; i < count; i++)
            {
                switch (reader.ReadEnum<E>())
                {

                    case E.City: {
                            var entry = City.Read(reader);
                            PkCity[entry.GetKey()] = entry;
                        }
                        break;

                    case E.Person: {
                            var entry = Person.Read(reader);
                            PkPerson[entry.GetKey()] = entry;
                        }
                        break;

                }
            }
        }
        public void ReadUpdates(BinaryReader reader)
        {
            var count = reader.ReadInt32();
            for (var i = 0; i < count; i++)
            {
                switch (reader.ReadEnum<E>())
                {

                    case E.City: {
                            var k = City.ReadPk(reader);
                            City entry;
                            if (PkCity.TryGetValue(k, out entry))
                                entry.ReadChanges(reader);
                        }
                        break;

                    case E.Person: {
                            var k = Person.ReadPk(reader);
                            Person entry;
                            if (PkPerson.TryGetValue(k, out entry))
                                entry.ReadChanges(reader);
                        }
                        break;

                }
            }
        }
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

