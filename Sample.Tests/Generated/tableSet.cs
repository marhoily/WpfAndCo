using System.Collections.Generic;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class TablesSet
    {
        public readonly TableCity City = new TableCity();
        public readonly TablePerson Person = new TablePerson();

		public void Apply(ChangeSet changes)
		{
			City.Apply(changes.City);
			Person.Apply(changes.Person);
		}
    }
}}

