using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
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
    }
}