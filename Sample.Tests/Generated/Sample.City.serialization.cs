





using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class City
    {
        public enum F
        {

		    Id = 1 << 0 ,

		    Created = 1 << 1 ,

		    Name = 1 << 2 ,

        }
        public BinaryKey __Key
        {
            get
            {
                var builder = new BinaryKeyBuilder();

		        builder.Add(Id);

                return builder.Build();
            }
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

		    writer.Write(Created);

		    writer.Write(Name);

        }
        public void DeserializeAll(BinaryReader reader) 
        {

		    Id = reader.ReadInt64();

		    Created = reader.ReadDateTime();

		    Name = reader.ReadString();

        }
        public void SerializeChanged(BinaryWriter writer, City old) 
        {
            SerializeKey(writer);

            if (old.Created != Created)
		    {
                writer.Write((int)F.Created);
                writer.Write(Created);
            }


            if (old.Name != Name)
		    {
                writer.Write((int)F.Name);
                writer.Write(Name);
            }


        }
    }
}}

