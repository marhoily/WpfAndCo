




namespace Sample.Generated {
public partial class Raw {
    public sealed partial class City
    {
        private City _original;

		private System.Int64 mId;
		public System.Int64 Id 
        {
            get { return mId; }
            private set { mId = value; }
        }

		private System.DateTime mCreated;
		public System.DateTime Created 
        {
            get { return mCreated; }
            private set { mCreated = value; }
        }

		private System.String mName;
		public System.String Name 
        {
            get { return mName; }
            private set { mName = value; }
        }

        public City(System.Int64 Id, System.DateTime Created, System.String Name)
        {

            this.Id = Id;

        }

    }
}}

