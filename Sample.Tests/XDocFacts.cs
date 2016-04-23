using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly XDocument _doc;

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
        public void Algo(XContainer proj, 
            string projectDir, HashSet<CmpNode> newNodes)
        {
            var oldNodes = new HashSet<CmpNode>(
                proj.FindByDirectory(projectDir).Select(FromElement));

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

        private bool MatchesAnyOf(XElement xElement, List<CmpNode> genNodes)
        {
            var fullName = xElement.Attribute("Include").Value;
            var dependentUpon = xElement.GetDependentUpon();
            return genNodes.Any(x =>
                x.FullName == fullName &&
                x.DependentUpon == dependentUpon);
        }
        private CmpNode FromElement(XElement xElement)
        {
            return new CmpNode(
                xElement.Attribute("Include").Value, 
                xElement.GetDependentUpon());
        }

        private static bool AllNodesAreThere(XContainer proj, IEnumerable<CmpNode> genNodes)
        {
            return genNodes.All(x => {
                var node = proj.FindByFullName(x.FullName);
                return node != null && x.DependentUpon == node.GetDependentUpon();
            });
        }
    }
}