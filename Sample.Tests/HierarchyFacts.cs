using System;
using System.Collections.Generic;
using System.Linq;
using Generator;
using Xunit;

namespace Sample
{
    public class HierarchyFacts
    {
        private Hierarchy _hierarchy;
        private const string Dir = @"c:\srcroot\WpfAndCo\Sample.Tests\Sample.Tests.csproj";

        public class X
        {
            public IEnumerable<Y> Ys { get { return Enumerable.Empty<Y>();} }
        }

        public class Y { }
        public class Z { }

        public HierarchyFacts()
        {
            var hierarchy = new HierarchyRoot(Dir, "Generated") {
                new Node<X>(new X()) {
                    new Node<Y> {new Node<Change>()},
                    new Node<Z>()
                } };

            _hierarchy = hierarchy
                .With((X m) => m.Ys)
                .Build();
        }

        [Fact]
        public void Apply()
        {

        }
    }
}