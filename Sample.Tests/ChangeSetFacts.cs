﻿using System;
using FluentAssertions;
using Sample.Generated;
using Xunit;

namespace Sample
{
    public sealed class ChangeSetFacts
    {
        private static readonly DateTime Inst = new DateTime(2000, 1, 1);

        [Fact]
        public void Apply()
        {
            var dataContext = new Raw.TableSet();
            dataContext.Apply(new Raw.ChangeSet(dataContext)
            {
                new Raw.City(123, Inst, "Minsk"),
                new Raw.Person(1, 123, "John"),
                new Raw.Person(2, 123, "Jack")
            });
            var city = dataContext.City.PrimaryKey[new Raw.City.PK(123)];
            city.Name.Should().Be("Minsk");
            var clone = city.Clone();
            clone.Name = "Minsk City";
            var changeSet2 = new Raw.ChangeSet(dataContext);
            changeSet2.Update(clone);
            changeSet2.Remove(new Raw.Person.PK(1));
            dataContext.Apply(changeSet2);
            var city2 = dataContext.City.PrimaryKey[new Raw.City.PK(123)];
            city2.Name.Should().Be("Minsk City");
        }


    }
}