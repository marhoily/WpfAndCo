using System;
using System.Collections.Generic;
using System.Text;
using ApprovalTests;
using Generator;
using Xunit;

namespace Sample
{
    public class HierarchyFacts
    {
        #region ' Models '

        private sealed class X
        {
            public IEnumerable<Y> Ys => new[] { new Y("1"), new Y("2") };
            public override string ToString() => "X";
        }

        private sealed class Y
        {
            private readonly string _name;
            public Y(string name) { _name = name; }
            public override string ToString() => $"Y{_name}";
        }

        private sealed class A : ITransformer
        {
            public override string ToString() => "()";
            public string Name => "A.cs";
            public string TransformText() { throw new Exception(); }
        }

        private sealed class B : ITransformer
        {
            private readonly X _x;
            public B(X x) { _x = x; }
            public override string ToString() => $"({_x})";
            public string Name => $"{_x}.b.cs";
            public string TransformText() { throw new Exception(); }
        }

        private sealed class C : ITransformer
        {
            private readonly X _x;
            public C(X x) { _x = x; }
            public override string ToString() => $"({_x})";
            public string Name => $"{_x}.c.cs";
            public string TransformText() { throw new Exception(); }
        }

        private sealed class D : ITransformer
        {
            private readonly Y _y;
            public D(Y y) { _y = y; }
            public override string ToString() => $"({_y})";
            public string Name => $"{_y}.d.cs";
            public string TransformText() { throw new Exception(); }
        }

        #endregion

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
                    } } }.With((X m) => m.Ys).Build()));
        }


        private static string ToString(GenHierarchy actual)
        {
            var s = new StringBuilder();
            s.AppendLine(actual.ProjectPath);
            foreach (var n in actual.GetAllNodes())
                s.AppendLine($"{n.FullName} -> {n.DependentUpon}");
            return s.ToString();
        }
    }
}