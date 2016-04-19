namespace Sample.Generated {
public partial class Raw {
    public sealed partial class City
    {
		public readonly System.Int64 Id;
		public readonly System.DateTime Created;
		public readonly System.String Name;
        public City(System.Int64 Id, System.DateTime Created, System.String Name)
        {
            this.Id = Id;
        }
        public City Clone()
        {
            return new City(
                Id, Created, Name);
        }

    }
}}

