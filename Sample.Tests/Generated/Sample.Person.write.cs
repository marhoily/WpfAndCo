using System;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class Person : IBinarySerializable
    {
        public void WriteAllProperties(BinaryWriter writer) 
        {
		    writer.Write(Id);
		    writer.Write(CityId);
		    writer.Write(Name);
        }
        public void SerializeChanged(BinaryWriter writer, F changed) 
        {
            writer.WriteEnum(changed);
            if (changed.HasFlag(F.CityId))
                writer.Write(CityId);
            if (changed.HasFlag(F.Name))
                writer.Write(Name);
        }
        public void WriteChangedProperties(BinaryWriter writer)
		{
			if (IsModified)
			{
				SerializeChanged(writer, GetChanges());
			}
		}

        public void ReadChanges(BinaryReader reader) 
        {
            var changes = reader.ReadEnum<F>();
            if (changes.HasFlag(F.CityId))
                CityId = reader.ReadInt64();
            if (changes.HasFlag(F.Name))
                Name = reader.ReadString();
        }
    }
}}

