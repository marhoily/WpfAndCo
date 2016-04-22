using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
            public IEnumerable<Y> Ys => new[] {new Y("1"), new Y("2")};
            public override string ToString() => $"X: [{Ys.Join()}]";
        }
        public class Y {
            public string Name { get; }
            public Y(string name){Name = name;}
            public override string ToString() => $"Y{Name}";
        }
        public class A : ITransformer { }
        public class B : ITransformer { public B(X x) { } }
        public class C : ITransformer { public C(X x) { } }
        public class D : ITransformer { public D(Y y) { } }
        
        [Fact]
        public void Apply()
        {
            var hierarchy = new HierarchyRoot("c:/dir/my.csproj", "ProjectDir") {
                new Node<A>(new X()) {
                    new Node<B> {new Node<D>()},
                    new Node<C>()
                } };

            var s = new StringWriter();
            var t = new IndentedTextWriter(s, "        ");
            var actual = hierarchy.With((X m) => m.Ys).Build();
            t.WriteLine($"Path: {actual.ProjectPath} | {actual.ProjectDir}");
            t.Write("Registrations: ");
            t.WriteLine(actual.Registrations
                .Select(r => $"{r.Key.Name} -> {r.Value.Name}")
                .Join());
            Nodes(t, actual.Nodes);
            Approvals.Verify(s.GetStringBuilder());
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