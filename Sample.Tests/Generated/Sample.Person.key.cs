using System;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class Person
    {
        public PK GetKey()
        {
            return new PK(Id);
        }
        public struct PK
        {
            public readonly System.Int64 Id;
            public PK(System.Int64 Id)
            {
                this.Id = Id;
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
        public static PK ReadPk(BinaryReader reader) 
        {
		    var Id = reader.ReadInt64();
            return new PK(Id);            
        }
    }
}}

