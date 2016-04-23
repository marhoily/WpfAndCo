﻿using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Core;
using Generator;
using Xunit;

namespace Sample
{
    public class HierarchyFacts
    {
        public class X
        {
            public IEnumerable<Y> Ys => new[] { new Y("1"), new Y("2") };
            public override string ToString() => $"X: [{Ys.Join()}]";
        }
        public class Y
        {
            public string Name { get; }
            public Y(string name) { Name = name; }
            public override string ToString() => $"Y{Name}";
        }

        public class A : ITransformer
        {
            public override string ToString() => "()";
        }
        public class B : ITransformer
        {
            public X X { get; }
            public B(X x) { X = x; }
            public override string ToString() => $"({X})";
        }

        public class C : ITransformer
        {
            public X X { get; }
            public C(X x) { X = x; }
            public override string ToString() => $"({X})";
        }
        public class D : ITransformer
        {
            public Y Y { get; }
            public D(Y y) { Y = y; }
            public override string ToString() => $"({Y})";
        }

        [Fact]
        public void Build()
        {
            Approvals.Verify(ToString(
                new HierarchyBuilder("c:/dir/my.csproj", "ProjectDir") {
                    new NodeBuilder<A>(new X()) {
                        new NodeBuilder<B> {
                            new NodeBuilder<D>()
                        },
                        new NodeBuilder<C>()
                    } }.With((X m) => m.Ys).Build()));
        }

        [Fact]
        public void Expand2()
        {
            Approvals.Verify(ToString(
                new HierarchyBuilder("c:/dir/my.csproj", "ProjectDir") {
                    new NodeBuilder<A>(new X()) {
                        new NodeBuilder<D> {
                            new NodeBuilder<D>()
                        }
                    } }.With((X m) => m.Ys).Build()));
        }

        [Fact]
        public void ItemsGroup()
        {
            Approvals.Verify(GetItemsGroup(
                new HierarchyBuilder("c:/dir/my.csproj", "ProjectDir") {
                    new NodeBuilder<A>(new X()) {
                        new NodeBuilder<D> {
                            new NodeBuilder<D>()
                        }
                    } }.With((X m) => m.Ys).Build()));
        }

        private static string GetItemsGroup(GenHierarchy hierarchy)
        {
            return new XElement("ItemsGroup", hierarchy.GetAllNodes()
                .Select(n => new XElement("Compile"))).ToString();
        }

        private static string ToString(GenHierarchy actual)
        {
            var s = new StringBuilder();
            s.AppendLine(actual.ProjectPath);
            foreach (var n in actual.GetAllNodes())
                s.AppendLine($"{n.ProjectDir} -> {n.Transformer}");
            return s.ToString();
        }

    }
}