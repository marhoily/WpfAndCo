using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using Generator;
using Xunit;

namespace Sample
{
    public sealed class XDocFacts
    {
        private readonly XDocument _doc;

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
        public void Algo(XContainer proj, string projectDir, 
            List<CmpNode> genNodes)
        {
            if (AllNodesAreThere(proj, genNodes))
            {
                foreach (var xElement in proj.FindByDirectory(projectDir))
                    if (!MatchesAnyOf(xElement, genNodes))
                        xElement.Remove();
                return;
            }

        }

        private bool MatchesAnyOf(XElement xElement, List<CmpNode> genNodes)
        {
            var fullName = xElement.Attribute("Include").Value;
            var dependentUpon = xElement.GetDependentUpon();
            return genNodes.Any(x =>
                x.FullName == fullName &&
                x.DependentUpon == dependentUpon);
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