





using System;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class Person
    {
        [Flags]
        public enum F
        {

		    Id = 1 << 0 ,

		    CityId = 1 << 1 ,

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
    }
}}

