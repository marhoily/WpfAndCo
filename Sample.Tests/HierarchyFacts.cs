using System;
using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using Generator;
using Newtonsoft.Json;
using Xunit;

namespace Sample
{
    public class HierarchyFacts
    {
        public class X
        {
            public IEnumerable<Y> Ys { get { return Enumerable.Empty<Y>();} }
        }

        public class Y { }
        public class Z { }
        
        [Fact]
        public void Apply()
        {
            var hierarchy = new HierarchyRoot("c:/dir/my.csproj", "Generated") {
                new Node<X>(new X()) {
                    new Node<Y> {new Node<Change>()},
                    new Node<Z>()
                } };

            Approvals.Verify(JsonConvert.SerializeObject(
                hierarchy.With((X m) => m.Ys).Build()));
        }
    }
}