using System;
using System.Collections;

namespace Sample.Generated {
    public sealed partial class CreatePersonHandler
    {
		public void Handle(
			CreatePersonAggregate aggregate, 
			CreatePerson commit)
		{
			aggregate.ById.Add(commit.Id, new Person());
		}
    }
}

