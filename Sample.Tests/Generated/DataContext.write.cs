using System.Collections.Generic;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class DataContext
    {
        public void WriteInserts(BinaryWriter writer)
        {
			foreach (var item in _inserts)
				item.ToString();
        }                              
        public void WriteUpdates(BinaryWriter writer)
        {                             
        }                             
        public void WriteDeletes(BinaryWriter writer)
        {
        }
    }
}}

