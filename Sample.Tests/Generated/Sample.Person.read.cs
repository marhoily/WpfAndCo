using System.Collections.Generic;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class Person
    {
        public static Person Read(BinaryReader reader) 
        {
		    var Id = reader.ReadInt64();
		    var CityId = reader.ReadInt64();
		    var Name = reader.ReadString();
            return new Person(
                Id, CityId, Name);
        }
    }
}}

