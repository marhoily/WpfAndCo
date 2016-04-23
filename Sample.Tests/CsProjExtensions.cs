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

        public static XElement FindFile(this XContainer doc, string fileName) =>
            doc.XPathSelectElement(
                $"//ns:ItemGroup/ns:Compile[@Include='{fileName}']",
                Resolver);
    }
}