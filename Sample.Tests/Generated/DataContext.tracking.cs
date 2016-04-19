using System.Collections.Generic;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class DataContext
    {
		private readonly List<IBinarySerializable> _inserts = new List<IBinarySerializable>();
		private readonly List<IBinarySerializable> _removes = new List<IBinarySerializable>();
                    
        public void Add(City item)
        {
			_inserts.Add(item);
			PkCity[item.GetKey()] = item;
        }                              
        public void Remove(City item)
        {
			_removes.Add(item);
			PkCity.Remove(item.GetKey());
        }                              
                    
        public void Add(Person item)
        {
			_inserts.Add(item);
			PkPerson[item.GetKey()] = item;
        }                              
        public void Remove(Person item)
        {
			_removes.Add(item);
			PkPerson.Remove(item.GetKey());
        }                              
		
    }
}}

