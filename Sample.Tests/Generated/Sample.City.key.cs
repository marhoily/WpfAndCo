using System;
using System.IO;

namespace Sample.Generated {
public partial class Raw {
    partial class City
    {
        public PK GetKey()
        {
            return new PK(Id);
        }
        public struct PK
        {
            public readonly System.Int64 Id;
            public PK(System.Int64 Id)
            {
                this.Id = Id;
            }
        }
    }
}}

