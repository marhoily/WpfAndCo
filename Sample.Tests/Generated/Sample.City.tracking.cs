




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
    }
}}

