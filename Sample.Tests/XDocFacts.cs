using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using ApprovalTests;
using FluentAssertions;
using Generator;
using Xunit;

namespace Sample
{
    public sealed class XDocFacts
    {
        private readonly XDocument _doc;

        private static readonly XmlNamespaceManager Resolver;

        static XDocFacts()
        {
            Resolver = new XmlNamespaceManager(new NameTable());
            Resolver.AddNamespace("ns", "http://schemas.microsoft.com/developer/msbuild/2003");
        }

        [Fact]
        public void ItemsGroup()
        {
            Approvals.Verify(GetItemsGroup(
                new CmpNode("name", "depeU")).ToString());
        }

        private static XElement GetItemsGroup(params CmpNode[] genNodes) =>
            new XElement("ItemGroup", genNodes.Select(n =>
                new XElement("Compile",
                    new XAttribute("Include", n.FullName),
                    new XElement("DependentUpon", n.DependentUpon))));

        public XDocFacts()
        {
            var fullPath = Path.GetFullPath(Path.Combine(
                Environment.CurrentDirectory,
                "../../Sample.Tests.csproj"));
            _doc = XDocument.Load(fullPath);
        }

        [Fact]
        public void FindByFullName()
        {
            _doc.FindByFullName("HierarchyFacts.cs")
                .Should().NotBeNull();
            _doc.FindByFullName("Generated\\ChangeSet.cs")
                .Should().NotBeNull();
        }

        [Fact]
        public void FindByDirectory()
        {
            _doc.FindByDirectory("Generated")
                .Count().Should().Be(10);
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
        public void Algo1()
        {
            var xDocument = XDocument.Load(@"c:\srcroot\Sample.Tests.csproj");

            Algo(xDocument, "Generated", new HashSet<CmpNode>
            {
                new CmpNode(@"Generated\DataContext.cs"),
                new CmpNode(@"Generated\Sample.City.cs"),
                new CmpNode(@"Generated\Sample.City.key.cs", @"Sample.City.cs"),
            });
        }
        public void Algo(XContainer proj, 
            string projectDir, HashSet<CmpNode> newNodes)
        {
            var oldNodes = new HashSet<CmpNode>(
                proj.FindByDirectory(projectDir).Select(x => x.ToCmpNode()));

            var toAdd = newNodes.Except(oldNodes).ToList();
            var toRemove = oldNodes.Except(newNodes).ToList();

            if (toAdd.Count == 0 && toRemove.Count == 0) return;

            XElement lastParent = null;
            foreach (var cmpNode in toRemove)
            {
                var element = proj.Find(cmpNode);
                if (lastParent != null && lastParent.IsEmpty)
                    lastParent.Remove();
                lastParent = element.Parent;
            }

            if (lastParent == null) {}
                
        }

    }
}