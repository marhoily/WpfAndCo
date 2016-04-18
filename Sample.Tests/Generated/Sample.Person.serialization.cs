





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
        public void SerializeAll(BinaryWriter writer) 
        {

		    writer.Write(Id);

		    writer.Write(CityId);

		    writer.Write(Name);

        }
        public void DeserializeAll(BinaryReader reader) 
        {

		    Id = reader.ReadInt64();

		    CityId = reader.ReadInt64();

		    Name = reader.ReadString();

        }
        public F GetChanged(BinaryWriter writer, Person old) 
        {
            F changed = 0;

            if (old.CityId != CityId)
                changed |= F.CityId;

            if (old.Name != Name)
                changed |= F.Name;

            return changed;
        }
        public void SerializeChanged(BinaryWriter writer, F changed) 
        {
            writer.WriteEnum(changed);

            if (changed.HasFlag(F.CityId))
                writer.Write(CityId);

            if (changed.HasFlag(F.Name))
                writer.Write(Name);

        }
        public void DeserializeChanged(BinaryReader reader) 
        {
            var changes = reader.ReadEnum<F>();

            if (changes.HasFlag(F.CityId))
                CityId = reader.ReadInt64();

            if (changes.HasFlag(F.Name))
                Name = reader.ReadString();

        }
    }
}}

