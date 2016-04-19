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
        public static City Read(BinaryReader reader) 
        {
		    var Id = reader.ReadInt64();
		    var Created = reader.ReadDateTime();
		    var Name = reader.ReadString();
            return new City(
                Id, Created, Name);
        }
        public void SerializeChanged(BinaryWriter writer, F changed) 
        {
            writer.WriteEnum(changed);
            if (changed.HasFlag(F.Created))
                writer.Write(Created);
            if (changed.HasFlag(F.Name))
                writer.Write(Name);
        }
        public void ReadChanges(BinaryReader reader) 
        {
            var changes = reader.ReadEnum<F>();
            if (changes.HasFlag(F.Created))
                Created = reader.ReadDateTime();
            if (changes.HasFlag(F.Name))
                Name = reader.ReadString();
        }
    }
}}

