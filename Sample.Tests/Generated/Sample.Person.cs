




namespace Sample.Generated {
public partial class Raw {
    public sealed partial class Person
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

		private System.Int64 mCityId;
		public System.Int64 CityId 
        {
            get { return mCityId; }
            private set { 
                if (mCityId == value) return;
                EnsureOriginal();
                mCityId = value; 
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

        public Person(System.Int64 Id, System.Int64 CityId, System.String Name)
        {

            this.Id = Id;

        }

    }
}}

