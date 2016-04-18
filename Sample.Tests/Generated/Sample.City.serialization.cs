





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
            F changed = 0;

            if (old.Created != Created)
                changed |= F.Created;

            if (old.Name != Name)
                changed |= F.Name;

            writer.WriteEnum(changed);

            if (changed.HasFlag(F.Created))
                writer.Write(Created);

            if (changed.HasFlag(F.Name))
                writer.Write(Name);

        }
        public void DeserializeChanged(BinaryReader reader) 
        {
            var changes = reader.ReadEnum<F>();

            if (changes.HasFlag(F.Created))
                Created = reader.ReadDateTime();

            if (changes.HasFlag(F.Name))
                Name = reader.ReadString();

        }
    }
}}

