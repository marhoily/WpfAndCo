using System;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class Person
    {
        [Flags]
        public enum F
        {
		    Id = 1 << 0 ,
		    CityId = 1 << 1 ,
		    Name = 1 << 2 ,
        }

        private Person _original;

        public Person Clone()
        {
            return new Person(
                Id, CityId, Name);
        }
        private void EnsureOriginal()
        {
            if (_original == null) _original = Clone();
        }

        public F GetChanged(BinaryWriter writer) 
        {
            F changed = 0;
            if (_original == null) return changed;
            if (_original.CityId != CityId)
                changed |= F.CityId;
            if (_original.Name != Name)
                changed |= F.Name;
            return changed;
        }
    }
}}

