namespace Sample.Generated {
public partial class Raw {
    public sealed partial class City
    {
		public System.Int64 Id;
		public System.DateTime Created;
		public System.String Name;
        public City(System.Int64 Id, System.DateTime Created, System.String Name)
        {
            this.Id = Id;
            this.Created = Created;
            this.Name = Name;
        }
        public City Clone()
        {
            return new City(Id, Created, Name);
        }

    }
}}

