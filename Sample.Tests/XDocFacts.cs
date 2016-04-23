using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ApprovalTests;
using FluentAssertions;
using Generator;
using Xunit;

namespace Sample
{
    public sealed class XDocFacts
    {
        private readonly XDocument _doc = XDocument.Load("../../a.xml");

        [Fact]
        public void FindByFullName()
        {
            _doc.FindByFullName("HierarchyFacts.cs").Should().NotBeNull();
            _doc.FindByFullName("Generated\\ChangeSet.cs").Should().NotBeNull();
        }

        [Fact]
        public void FindByDirectory()
        {
            _doc.FindByDirectory("Generated")
                .Select(x => x.ToCmpNode())
                .Should().Equal(
                    new CmpNode(@"Generated\ChangeSet.cs"),
                    new CmpNode(@"Generated\CsSample.City.cs", "ChangeSet.cs"),
                    new CmpNode(@"Generated\CsSample.Person.cs", "ChangeSet.cs"),
                    new CmpNode(@"Generated\DataContext.cs"),
                    new CmpNode(@"Generated\Sample.City.cs"),
                    new CmpNode(@"Generated\Sample.City.key.cs", "Sample.City.cs"),
                    new CmpNode(@"Generated\Sample.Person.cs"),
                    new CmpNode(@"Generated\Sample.Person.key.cs", "Sample.Person.cs"),
                    new CmpNode(@"Generated\TableSample.City.cs", "Sample.City.cs"),
                    new CmpNode(@"Generated\TableSample.Person.cs", "Sample.Person.cs")
                );
        }

        [Fact]
        public void Find()
        {
            _doc.Find(
                new CmpNode(@"Generated\Sample.City.key.cs", @"Sample.City.cs"))
                .Should().NotBeNull();
            _doc.Find(
                new CmpNode(@"Generated\DataContext.cs"))
                .Should().NotBeNull();
        }

        [Fact]
        public void Modify()
        {
            _doc.Find(new CmpNode(@"Generated\DataContext.cs")).Remove();
            _doc.Insert("Generated", new CmpNode("a", "b"));
            Approvals.VerifyXml(_doc.ToString());
        }

        [Fact]
        public void Algo1()
        {
            _doc.Update("Generated", new HashSet<CmpNode>
            {
                new CmpNode(@"Generated\DataContext.cs"),
                new CmpNode(@"Generated\Sample.City.cs"),
                new CmpNode(@"Generated\Sample.City.key.cs", @"Sample.City.cs"),
                new CmpNode(@"a", @"b"),
                new CmpNode(@"c"),
            });
            Approvals.VerifyXml(_doc.ToString());
        }
    }
}