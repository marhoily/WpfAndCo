namespace Sample.Generated {
public partial class Raw {
    public sealed partial class Person
    {
		public System.Int64 Id;
		public System.Int64 CityId;
		public System.String Name;
        public Person(System.Int64 Id, System.Int64 CityId, System.String Name)
        {
            this.Id = Id;
            this.CityId = CityId;
            this.Name = Name;
        }
        public Person Clone()
        {
            return new Person(Id, CityId, Name);
        }

    }
}}

