





using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class Person
    {
        public enum F
        {

		    Id = 1 << 0 ,

		    CityId = 1 << 1 ,

		    Name = 1 << 2 ,


        }
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
        public void DeserializeAll(BinaryReader reader) 
        {

		    Id = reader.ReadInt64();

		    CityId = reader.ReadInt64();

		    Name = reader.ReadString();

        }
        public void SerializeChanged(BinaryWriter writer, Person old) 
        {
            SerializeKey(writer);

            if (old.CityId != CityId)
		        writer.Write(CityId);

            if (old.Name != Name)
		        writer.Write(Name);

        }
    }
}}

