using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
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
            public override string ToString() => "X";
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
            public string Name => "A.cs";
        }
        public class B : ITransformer
        {
            public X X { get; }
            public B(X x) { X = x; }
            public override string ToString() => $"({X})";
            public string Name => $"{X}.b.cs";
        }

        public class C : ITransformer
        {
            public X X { get; }
            public C(X x) { X = x; }
            public override string ToString() => $"({X})";
            public string Name => $"{X}.c.cs";
        }
        public class D : ITransformer
        {
            public Y Y { get; }
            public D(Y y) { Y = y; }
            public override string ToString() => $"({Y})";
            public string Name => $"{Y}.d.cs";
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
                    } } }.With((X m) => m.Ys).Build()));
        }

        [Fact]
        public void ItemsGroup()
        {
            Approvals.Verify(GetItemsGroup(
                new HierarchyBuilder("c:/dir/my.csproj", "ProjectDir") {
                    new NodeBuilder<A>(new X()) {
                        new NodeBuilder<D> {
                            new NodeBuilder<D>()
                    } } }.With((X m) => m.Ys).Build()));
        }

        private static string GetItemsGroup(GenHierarchy hierarchy)
        {
            return new XElement("ItemsGroup", hierarchy.GetAllNodes()
                .Select(n => new XElement("Compile", 
                    new XAttribute("Include", n.FullName),
                    new XElement("DependentUpon",
                        n.Level > 1 ? n.Name : ""))))
                        
                        .ToString();

            /*
  <ItemGroup>
    <Compile Include="ChangeSetFacts.cs" />
    <Compile Include="Generated\ChangeSet.cs" />
    <Compile Include="Generated\CsSample.City.cs">
      <DependentUpon>ChangeSet.cs</DependentUpon>
    </Compile>
    <Compile Include="Generated\CsSample.Person.cs">
      <DependentUpon>ChangeSet.cs</DependentUpon>
    </Compile>
    <Compile Include="Generated\DataContext.cs" />
    <Compile Include="Generated\Sample.City.cs" />
    <Compile Include="Generated\Sample.City.key.cs">
      <DependentUpon>Sample.City.cs</DependentUpon>
    </Compile>
    <Compile Include="Generated\Sample.Person.cs" />
    <Compile Include="Generated\Sample.Person.key.cs">
      <DependentUpon>Sample.Person.cs</DependentUpon>
    </Compile>
    <Compile Include="Generated\TableSample.City.cs">
      <DependentUpon>Sample.City.cs</DependentUpon>
    </Compile>
    <Compile Include="Generated\TableSample.Person.cs">
      <DependentUpon>Sample.Person.cs</DependentUpon>
    </Compile>
    <Compile Include="HierarchyFacts.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
             */
        }

        private static string ToString(GenHierarchy actual)
        {
            var s = new StringBuilder();
            s.AppendLine(actual.ProjectPath);
            foreach (var n in actual.GetAllNodes())
                s.AppendLine($"{n.Level}: {n.FullName}");
            return s.ToString();
        }

    }
}