using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sample.Generated {
public partial class Raw {
    partial class DataContext
    {
        public void WriteInserts(BinaryWriter writer)
        {
			writer.Write(_inserts.Count);
			foreach (var item in _inserts)
				item.WriteAllProperties(writer);
        }                              
        public void WriteUpdates(BinaryWriter writer)
        {
			{
				var modified = PkCity.Values
					.Where(item => item.IsModified).ToList();
				if (modified.Count > 0)
				{
					writer.WriteEnum(E.City);
					foreach (var item in modified)
						item.WriteChangedProperties(writer);
				}
			}
			{
				var modified = PkPerson.Values
					.Where(item => item.IsModified).ToList();
				if (modified.Count > 0)
				{
					writer.WriteEnum(E.Person);
					foreach (var item in modified)
						item.WriteChangedProperties(writer);
				}
			}
			writer.Write(0);
        }                             
        public void WriteDeletes(BinaryWriter writer)
        {
			writer.Write(_removes.Count);
			foreach (var item in _removes)
				item.WriteKey(writer);
        }
    }
}}

