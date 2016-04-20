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
            public IEnumerable<Y> Ys {
                get { return new[] {new Y("1"), new Y("2")}; }
            }
        }
        public class Y {
            public string Name { get; set; }
            public Y(string name){Name = name;}
        }
        public class A : ITransformer { }
        public class B : ITransformer { public B(X x) { } }
        public class C : ITransformer { public C(X x) { } }
        public class D : ITransformer { public D(Y y) { } }
        
        [Fact]
        public void Apply()
        {
            var hierarchy = new HierarchyRoot("c:/dir/my.csproj", "Generated") {
                new Node<A>(new X()) {
                    new Node<B> {new Node<D>()},
                    new Node<C>()
                } };

            Approvals.Verify(JsonConvert.SerializeObject(
                hierarchy.With((X m) => m.Ys).Build(),
                Formatting.Indented));
        }
    }
}