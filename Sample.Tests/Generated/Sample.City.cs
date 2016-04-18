




namespace Sample.Generated {
public partial class Raw {
    public sealed partial class City
    {

		private System.Int64 mId;
		public System.Int64 Id 
        {
            get { return mId; }
            private set { 
                if (mId == value) return;
                EnsureOriginal();
                mId = value; 
            }
        }

		private System.DateTime mCreated;
		public System.DateTime Created 
        {
            get { return mCreated; }
            private set { 
                if (mCreated == value) return;
                EnsureOriginal();
                mCreated = value; 
            }
        }

		private System.String mName;
		public System.String Name 
        {
            get { return mName; }
            private set { 
                if (mName == value) return;
                EnsureOriginal();
                mName = value; 
            }
        }

        public City(System.Int64 Id, System.DateTime Created, System.String Name)
        {

            this.Id = Id;

        }

    }
}}

