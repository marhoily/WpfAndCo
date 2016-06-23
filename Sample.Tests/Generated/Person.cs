using System;
using System.Collections;
using System.Collections.Generic;

namespace Sample.Generated {
	[IoC]
    public sealed class PersonAggregate : IEnumerable<PersonRow>
    {
		private readonly Dictionary<Guid, PersonRow> 
			_byId = new Dictionary<Guid, PersonRow> ();
		public void Create(PersonRow row) => _byId.Add(row.Id, row);
		public void Update(PersonRow row) => _byId[row.Id] = row;
		public void Remove(Guid id) => _byId.Remove(id);
		public PersonRow Get(Guid id) {
			PersonRow result;
			return _byId.TryGetValue(id, out result) ? result : null;
		}
	    public IEnumerator<PersonRow> GetEnumerator() => _byId.Values.GetEnumerator();
	    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
	[Dto]
    public sealed class PersonRow
    {
		public Guid Id { get; set; }
		public int RowVersion { get; set; }
		public string Name { get; set; }
		public Guid CityId { get; set; }
		public Guid FavoriteCityId { get; set; }
    }
}
