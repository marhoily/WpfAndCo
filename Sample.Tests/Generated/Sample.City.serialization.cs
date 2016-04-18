





using System;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class City
    {
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
        public F GetChanged(BinaryWriter writer, City old) 
        {
            F changed = 0;

            if (old.Created != Created)
                changed |= F.Created;

            if (old.Name != Name)
                changed |= F.Name;

            return changed;
        }
        public void SerializeChanged(BinaryWriter writer, F changed) 
        {
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

