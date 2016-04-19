using System.Collections.Generic;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class DataContext
    {
		private readonly List<object> _inserts = new List<object>();
		private readonly List<object> _removes = new List<object>();
                    
        public void Add(City item)
        {
			_inserts.Add(item);
			PkCity[item.GetKey()] = item;
        }                              
                    
        public void Add(Person item)
        {
			_inserts.Add(item);
			PkPerson[item.GetKey()] = item;
        }                              
		
    }
}}

