




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
    }
}}

