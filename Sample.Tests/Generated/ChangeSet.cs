using System;
using System.Collections;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class ChangeSet : IEnumerable
    {
        public enum E
        {
		    Person,
		    City,
        }

        IEnumerator IEnumerable.GetEnumerator() { throw new NotSupportedException(); }
    }
}}

