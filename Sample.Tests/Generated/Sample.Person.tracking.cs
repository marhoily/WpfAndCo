





using System.IO;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class Person
    {
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

        public F GetChanged(BinaryWriter writer, Person old) 
        {
            F changed = 0;

            if (old.CityId != CityId)
                changed |= F.CityId;

            if (old.Name != Name)
                changed |= F.Name;

            return changed;
        }
    }
}}

