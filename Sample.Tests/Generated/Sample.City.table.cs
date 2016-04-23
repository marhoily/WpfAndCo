using System.Collections.Generic;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class TableCity
    {
        public readonly Dictionary<City.PK, City> 
            PrimaryKey = new Dictionary<City.PK, City>();
		public void Insert(City item)
		{
			PrimaryKey.Add(item.GetKey(), item);
		}
		public void Update(City item)
		{
			PrimaryKey[item.GetKey()] = item;
		}
		public void Delete(City.PK key)
		{
			PrimaryKey.Remove(key);
		}
		public void Apply(CsCity changes)
		{
			foreach (var item in changes.Inserts) Insert(item);
			foreach (var item in changes.Updates) Update(item.Value);
			foreach (var item in changes.Deletes) Delete(item);
		}
    }
}}

