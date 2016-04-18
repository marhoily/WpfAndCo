




namespace Sample.Generated {
public partial class Raw {
    public sealed partial class Person
    {
        private Person _original;

		private System.Int64 mId;
		public System.Int64 Id 
        {
            get { return mId; }
            private set { mId = value; }
        }

		private System.Int64 mCityId;
		public System.Int64 CityId 
        {
            get { return mCityId; }
            private set { mCityId = value; }
        }

		private System.String mName;
		public System.String Name 
        {
            get { return mName; }
            private set { mName = value; }
        }

        public Person(System.Int64 Id, System.Int64 CityId, System.String Name)
        {

            this.Id = Id;

        }

    }
}}

