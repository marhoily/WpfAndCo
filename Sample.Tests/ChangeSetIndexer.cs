using FluentAssertions;
using Sample.Generated;
using Xunit;

namespace Sample
{
    public sealed class ChangeSetIndexer
    {
        private readonly Raw.Person _john = new Raw.Person(1, 123, "John");
        private readonly Raw.Person _jack = new Raw.Person(2, 123, "Jack");
        private readonly Raw.Person _alice = new Raw.Person(3, 123, "Alice");
        private readonly Raw.Person _bob = new Raw.Person(5, 123, "Bob");
        private readonly Raw.Person _johnMod;
        private readonly Raw.ChangeSet _changeSet;

        public ChangeSetIndexer()
        {
            _johnMod = _john.Clone();
            _johnMod.Name = "Marry!";

            var dataContext = new Raw.TableSet();
            dataContext.Apply(
                new Raw.ChangeSet(dataContext) {_john, _jack, _alice});
            _changeSet = new Raw.ChangeSet(dataContext);
            _changeSet.Remove(_alice.GetKey());
            _changeSet.Update(_johnMod);
            _changeSet.Add(_bob);
        }

        [Fact]
        public void Updated() => 
            _changeSet.Person[_john.GetKey()].Should().Be(_johnMod);
        [Fact]
        public void InTable() => 
            _changeSet.Person[_jack.GetKey()].Should().Be(_jack);
        [Fact]
        public void Inserted() =>
            _changeSet.Person[_bob.GetKey()].Should().Be(_bob);
        [Fact]
        public void Removed() =>
            _changeSet.Person[_alice.GetKey()].Should().BeNull();
        [Fact]
        public void WrongIndex() =>
            _changeSet.Person[default(Raw.Person.PK)].Should().BeNull();
    }
}