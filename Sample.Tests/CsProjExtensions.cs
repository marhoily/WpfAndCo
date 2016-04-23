using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Sample
{
    public static class CsProjExtensions
    {
        private static readonly XmlNamespaceManager Resolver;

        static CsProjExtensions()
        {
            Resolver = new XmlNamespaceManager(new NameTable());
            Resolver.AddNamespace("ns", "http://schemas.microsoft.com/developer/msbuild/2003");
        }

        public static XElement FindByFullName(this XContainer doc, string fileName) =>
            doc.XPathSelectElement(
                $"//ns:ItemGroup/ns:Compile[@Include='{fileName}']",
                Resolver);

        public static IEnumerable<XElement> FindByDirectory(this XContainer doc, string dir)
            => doc
                .XPathSelectElements("//ns:ItemGroup/ns:Compile", Resolver)
                .Where(x => x.Attribute("Include").Value.StartsWith(dir));
    }
}