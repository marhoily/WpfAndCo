





using System;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class City
    {
        [Flags]
        public enum F
        {

		    Id = 1 << 0 ,

		    Created = 1 << 1 ,

		    Name = 1 << 2 ,

        }
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
    }
}}

