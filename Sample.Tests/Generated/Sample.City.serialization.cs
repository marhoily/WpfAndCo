





using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class City
    {
        public void SerializeKey(BinaryWriter writer) 
        {

		    writer.Write(Id);

        }
        public void SerializeAll(BinaryWriter writer) 
        {

		    writer.Write(Id);

		    writer.Write(Created);

		    writer.Write(Name);

        }
        public void SerializeChanged(BinaryWriter writer, City old) 
        {

            if (old.Id != Id)
		        writer.Write(Id);

            if (old.Created != Created)
		        writer.Write(Created);

            if (old.Name != Name)
		        writer.Write(Name);

        }
    }
}}

