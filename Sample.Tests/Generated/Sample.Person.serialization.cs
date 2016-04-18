





using System;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class Person
    {
        public void SerializeAll(BinaryWriter writer) 
        {

		    writer.Write(Id);

		    writer.Write(CityId);

		    writer.Write(Name);

        }
        public static Person Read(BinaryReader reader) 
        {

		    var Id = reader.ReadInt64();

		    var CityId = reader.ReadInt64();

		    var Name = reader.ReadString();

            return new Person(
                Id, CityId, Name);
        }
        public void SerializeChanged(BinaryWriter writer, F changed) 
        {
            writer.WriteEnum(changed);

            if (changed.HasFlag(F.CityId))
                writer.Write(CityId);

            if (changed.HasFlag(F.Name))
                writer.Write(Name);

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

