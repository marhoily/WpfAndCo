using System.Collections.Generic;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class City
    {
        public static City Read(BinaryReader reader) 
        {
		    var Id = reader.ReadInt64();
		    var Created = reader.ReadDateTime();
		    var Name = reader.ReadString();
            return new City(
                Id, Created, Name);
        }
    }
}}

