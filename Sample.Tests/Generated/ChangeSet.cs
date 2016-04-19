using System.Collections.Generic;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    public sealed partial class ChangeSet
    {
        public enum E
        {
		    City,
		    Person,
        }
        public readonly CsCity City = new CsCity();
        public readonly CsPerson Person = new CsPerson();
    }
}}

