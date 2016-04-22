﻿using System;
using System.Linq;
using FluentAssertions;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Sample.Generated;
using Xunit;

namespace Sample
{
    public class ChangeSetFacts
    {
        private static readonly DateTime Inst = new DateTime(2000,1,1);

        [Fact]
        public void Apply()
        {
            var changeSet = new Raw.ChangeSet();
            changeSet.Add(new Raw.City(123, Inst, "Minsk"));
            changeSet.Add(new Raw.Person(1, 123, "John"));
            changeSet.Add(new Raw.Person(2, 123, "Jack"));
            var dataContext = new Raw.DataContext();
            dataContext.Apply(changeSet);
            var city = dataContext.City.PrimaryKey[new Raw.City.PK(123)];
            city.Name.Should().Be("Minsk");
            var clone = city.Clone();
            clone.Name = "Minsk City";
            var changeSet2 = new Raw.ChangeSet();
            changeSet2.Update(clone);
            changeSet2.Remove(new Raw.Person.PK(1));
            dataContext.Apply(changeSet2);
            var city2 = dataContext.City.PrimaryKey[new Raw.City.PK(123)];
            city2.Name.Should().Be("Minsk City");
        }
    }
}