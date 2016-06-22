using System;
using System.Collections;

namespace Sample.Generated {
    public sealed partial class CreateCityHandler
    {
		public void Handle(
			CreateCityAggregate aggregate, 
			CreateCity commit)
		{
			aggregate.ById.Add(commit.Id, new City());
		}
    }
}

