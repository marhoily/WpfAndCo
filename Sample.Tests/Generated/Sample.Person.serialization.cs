




using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class Person
    {
        public void Serialize(BinaryWriter writer) 
        {

		    writer.Write(Id);

		    writer.Write(CityId);

		    writer.Write(Name);

        }
    }
}}

