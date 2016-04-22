using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            public override string ToString() => "A";
        }
        public class B : ITransformer
        {
            public X X { get; }
            public B(X x) { X = x; }
            public override string ToString() => X.ToString();
        }

        public class C : ITransformer
        {
            public X X { get; }
            public C(X x) { X = x; }
            public override string ToString() => X.ToString();
        }
        public class D : ITransformer
        {
            public Y Y { get; }
            public D(Y y) { Y = y; }
            public override string ToString() => Y.ToString();
        }

        private readonly HierarchyRoot _hierarchy =
            new HierarchyRoot("c:/dir/my.csproj", "ProjectDir") {
                new Node<A>(new X()) {
                    new Node<B> {new Node<D>()},
                    new Node<C>()
                } };

        [Fact]
        public void Build()
        {
            Approvals.Verify(RegToString(
                _hierarchy.With((X m) => m.Ys).Build()));
        }
        [Fact]
        public void Expand()
        {
            Approvals.Verify(ExpToString(
                _hierarchy.With((X m) => m.Ys).Build().Expand()));
        }
        //[Fact]
        //public void Expand2()
        //{
        //    var hierarchy =
        //        new HierarchyRoot("c:/dir/my.csproj", "ProjectDir") {
        //        new Node<A>(new X()) {
        //            new Node<B> {new Node<D>()},
        //            new Node<C>()
        //        } };
        //    Approvals.Verify(ExpToString(
        //            hierarchy.With((X m) => m.Ys).Build().Expand()));
        //}

        private static string ExpToString(HierarchyExp actual)
        {
            var s = new StringWriter();
            var t = new IndentedTextWriter(s);
            t.WriteLine($"Path: {actual.ProjectPath} | {actual.ProjectDir}");
            Nodes(t, actual.Nodes);

            return s.GetStringBuilder().ToString();
        }

        private static string RegToString(Hierarchy actual)
        {
            var s = new StringWriter();
            var t = new IndentedTextWriter(s);
            t.WriteLine($"Path: {actual.ProjectPath} | {actual.ProjectDir}");
            t.Write("Registrations: ");
            t.WriteLine(actual.Registrations
                .Select(r => $"{r.Key.Name} -> {r.Value.Name}")
                .Join());
            Nodes(t, actual.Nodes);
            return s.GetStringBuilder().ToString();
        }

        private static void Nodes(IndentedTextWriter t, IEnumerable<NodeExp> nodes)
        {
            foreach (var n in nodes)
            {
                t.WriteLine($"{n.Model.GetType().Name} -> {n.Model}");
                t.Indent++;
                Nodes(t, n.Nodes);
                t.Indent--;
            }
        }

        private static void Nodes(IndentedTextWriter t, IEnumerable<RegNode> nodes)
        {
            foreach (var n in nodes)
            {
                t.WriteLine($"{n.Tp.Name} | {n.Model?.ToString() ?? "null"} ->");
                t.Indent++;
                Nodes(t, n.Nodes);
                t.Indent--;
            }
        }
    }
}