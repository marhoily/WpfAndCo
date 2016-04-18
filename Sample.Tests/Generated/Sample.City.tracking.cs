





using System;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class City
    {
        [Flags]
        public enum F
        {

		    Id = 1 << 0 ,

		    Created = 1 << 1 ,

		    Name = 1 << 2 ,

        }

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

        public F GetChanged(BinaryWriter writer) 
        {
            F changed = 0;
            if (_original == null) return changed;

            if (_original.Created != Created)
                changed |= F.Created;

            if (_original.Name != Name)
                changed |= F.Name;

            return changed;
        }
    }
}}

