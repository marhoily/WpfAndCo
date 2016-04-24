using FluentAssertions;
using Sample.Generated;
using Xunit;

namespace Sample
{
    public abstract class ImplantString
    {
        public abstract string Vv { get; set; }
        public static implicit operator string(ImplantString v) => v.Vv;
        public static implicit operator ImplantString(string v) => new Free(v);

        public sealed class Bound : ImplantString
        {
            private readonly Raw.ChangeSet _changeSet;
            private readonly Raw.Person.PK _key;

            public Bound(Raw.ChangeSet changeSet, Raw.Person.PK key)
            {
                _changeSet = changeSet;
                _key = key;
            }

            public override string Vv
            {
                get { return _changeSet.Person[_key].Name; }
                set
                {
                    if (_changeSet.Person[_key].Name != value)
                        _changeSet.Person.GetOrAdd(_key).Name = value;
                }
            }
        }

        public sealed class Free : ImplantString
        {
            public override string Vv { get; set; }
            public Free(string vv) { Vv = vv; }
        }
    }

    public sealed class ImplantFacts
    {
        private sealed class Person
        {
            private static int _pkCounter;

            public Person(Raw.ChangeSet changeSet)
            {
                var person = new Raw.Person(++_pkCounter, 0, null);
                changeSet.Add(person);
                _age = new ImplantString.Bound(changeSet, person.GetKey());
            }

            private readonly ImplantString _age;

            public ImplantString Age
            {
                get { return _age; }
                set { _age.Vv = value; }
            }
        }

        [Fact]
        public void Sample()
        {
            var changeSet = new Raw.ChangeSet(new Raw.TableSet());
            var person = new Person(changeSet);
            person.Age = "blah";
            person.Age.Vv.Should().Be("blah");
        }
    }
}