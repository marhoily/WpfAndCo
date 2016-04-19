using System;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class City : IBinarySerializable
    {
        public void WriteAllProperties(BinaryWriter writer) 
        {
		    writer.Write(Id);
		    writer.Write(Created);
		    writer.Write(Name);
        }
        public void SerializeChanged(BinaryWriter writer, F changed) 
        {
            writer.WriteEnum(changed);
            if (changed.HasFlag(F.Created))
                writer.Write(Created);
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

    }
}}

