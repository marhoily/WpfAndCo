





using System.IO;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class City
    {
        private City _original;

        public City Clone()
        {
            return new City(
                Id, Created, Name);
        }
        private void EnsureOriginal()
        {
            if (_original == null) _original = Clone();
        }

        public F GetChanged(BinaryWriter writer, City old) 
        {
            F changed = 0;

            if (old.Created != Created)
                changed |= F.Created;

            if (old.Name != Name)
                changed |= F.Name;

            return changed;
        }
    }
}}

