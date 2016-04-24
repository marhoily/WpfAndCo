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
        public void Indexer_Updated() => 
            _changeSet.Person[_john.GetKey()].Should().Be(_johnMod);
        [Fact]
        public void Indexer_InTable() => 
            _changeSet.Person[_jack.GetKey()].Should().Be(_jack);
        [Fact]
        public void Indexer_Inserted() =>
            _changeSet.Person[_bob.GetKey()].Should().Be(_bob);
        [Fact]
        public void Indexer_Removed() =>
            _changeSet.Person[_alice.GetKey()].Should().BeNull();
        [Fact]
        public void Indexer_WrongIndex() =>
            _changeSet.Person[default(Raw.Person.PK)].Should().BeNull();

        [Fact]
        public void GetOrAdd_Updated() =>
            _changeSet.Person.GetOrAdd(_john.GetKey()).Should().Be(_johnMod);
        [Fact]
        public void GetOrAdd_InTable()
        {
            var result = _changeSet.Person.GetOrAdd(_jack.GetKey());
            result.Id.Should().Be(_jack.Id);
            result.Should().NotBeSameAs(_jack);
        }
        [Fact]
        public void GetOrAdd_Inserted() =>
            _changeSet.Person.GetOrAdd(_bob.GetKey()).Should().Be(_bob);
        [Fact]
        public void GetOrAdd_Removed() =>
            _changeSet.Person.GetOrAdd(_alice.GetKey()).Should().BeNull();
        [Fact]
        public void GetOrAdd_WrongIndex() =>
            _changeSet.Person.GetOrAdd(default(Raw.Person.PK)).Should().BeNull();
    }
}