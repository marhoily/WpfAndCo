using Sample.Generated;

namespace Sample
{
    //public abstract class ImplantString
    //{
    //    public abstract string Vv { get; set; }
    //    public static implicit operator string(ImplantString v) => v.Vv;

    //    public sealed class Bound : ImplantString
    //    {
    //        private readonly Raw.TableSet _tablesSet;
    //        private readonly Raw.ChangeSet _changeSet;
    //        private readonly Raw.Person.PK _key;

    //        public Bound(
    //            Raw.TableSet tablesSet, 
    //            Raw.ChangeSet changeSet, 
    //            Raw.Person.PK key)
    //        {
    //            _tablesSet = tablesSet;
    //            _changeSet = changeSet;
    //            _key = key;
    //        }

    //        public Raw.Person Source => 
    //            _changeSet.Person[_key] ??
    //            _tablesSet.Person.PrimaryKey[_key];

    //        public override string Vv
    //        {
    //            get { return Source.Name; }
    //            set
    //            {
    //                if (Source.Name == value) return;
    //                Raw.Person p = _changeSet.Person.GetOrAdd(_key);
    //                p.Name = value;
    //            }
    //        }
    //    }
    //}

    public sealed class ImplantFacts
    {
        //private sealed class Person
        //{
        //    private ImplantString _age;
        //
        //    public ImplantString Age
        //    {
        //        get { return _age; }
        //        set { _age.Vv = value; }
        //    }
        //}
    }
}