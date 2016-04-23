using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using ApprovalTests;
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


        private static string ToString(GenHierarchy actual)
        {
            var s = new StringWriter();
            var t = new IndentedTextWriter(s);
            t.WriteLine(actual.ProjectPath);
            Nodes(t, actual.GenNodes);
            return s.GetStringBuilder().ToString();
        }

        private static void Nodes(IndentedTextWriter t, IEnumerable<GenNode> nodes)
        {
            foreach (var n in nodes)
            {
                t.WriteLine($"{n.ProjectDir} -> {n.Transformer}");
                Nodes(t, n.GenNodes);
            }
        }

    }
}