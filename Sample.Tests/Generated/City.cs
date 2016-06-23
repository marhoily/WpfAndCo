using System;
using System.Collections;
using System.Collections.Generic;

namespace Sample.Generated {
	[IoC]
    public sealed class CityAggregate : IEnumerable<CityRow>
    {
		private readonly Dictionary<Guid, CityRow> 
			_byId = new Dictionary<Guid, CityRow> ();
		public void Create(CityRow row) => _byId.Add(row.Id, row);
		public void Update(CityRow row) => _byId[row.Id] = row;
		public void Remove(Guid id) => _byId.Remove(id);
		public CityRow Get(Guid id) {
			CityRow result;
			return _byId.TryGetValue(id, out result) ? result : null;
		}
	    public IEnumerator<CityRow> GetEnumerator() => _byId.Values.GetEnumerator();
	    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
	[Dto]
    public sealed class CityRow
    {
		public Guid Id { get; set; }
		public int RowVersion { get; set; }
		public string Name { get; set; }
		public Guid BrotherCityId { get; set; }
    }
}
