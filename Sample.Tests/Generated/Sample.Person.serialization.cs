





using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class Person
    {
        public void SerializeKey(BinaryWriter writer) 
        {

		    writer.Write(Id);

        }
        public void DeserializeKey(BinaryReader reader) 
        {

		    Id = reader.ReadInt64();

        }
        public void SerializeAll(BinaryWriter writer) 
        {

		    writer.Write(Id);

		    writer.Write(CityId);

		    writer.Write(Name);

        }
        public void SerializeChanged(BinaryWriter writer, Person old) 
        {

            if (old.Id != Id)
		        writer.Write(Id);

            if (old.CityId != CityId)
		        writer.Write(CityId);

            if (old.Name != Name)
		        writer.Write(Name);

        }
    }
}}

