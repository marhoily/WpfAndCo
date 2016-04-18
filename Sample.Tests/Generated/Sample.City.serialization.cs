




using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class City
    {
        public void Serialize(BinaryWriter writer) 
        {

		    writer.Write(Id);

		    writer.Write(Created);

		    writer.Write(Name);

        }
    }
}}

