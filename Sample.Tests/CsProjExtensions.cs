using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using FluentAssertions;
using Generator;

namespace Sample
{
    public static class CsProjExtensions
    {
        private static readonly XmlNamespaceManager M;
        private static readonly XNamespace Ns = "http://schemas.microsoft.com/developer/msbuild/2003";

        static CsProjExtensions()
        {
            M = new XmlNamespaceManager(new NameTable());
            M.AddNamespace("ns", Ns.NamespaceName);
        }

        public static string GetDependentUpon(this XContainer doc) 
            => doc.Element(Ns + "DependentUpon")?.Value;

        public static XElement FindByFullName(this XContainer doc, string fileName) =>
            doc.XPathSelectElement($"//ns:ItemGroup/ns:Compile[@Include='{fileName}']", M);

        public static IEnumerable<XElement> FindByDirectory(this XContainer doc, string dir)
            => doc.XPathSelectElements("//ns:ItemGroup/ns:Compile", M)
                .Where(x => x.Attribute("Include").Value.StartsWith(dir));

        public static void Insert(this XContainer doc, string prefferedFolder, params CmpNode[] genNodes)
        {
            doc.XPathSelectElements("//ns:ItemGroup[ns:Compile]", M)
                .OrderByDescending(x => x
                    .XPathSelectElements("ns:Compile[@Include]", M)
                    .Count(y => y
                        .Attribute("Include").Value
                        .StartsWith(prefferedFolder)))
                .FirstOrDefault()?
                .Add(genNodes.Select(n =>
                    new XElement(Ns + "Compile",
                        new XAttribute("Include", n.FullName),
                        new XElement(Ns + "DependentUpon", n.DependentUpon))));
        }

        public static XElement Find(this XContainer doc, CmpNode node) =>
            doc.XPathSelectElements($"//ns:ItemGroup/ns:Compile[@Include='{node.FullName}']", M)
            .SingleOrDefault(x => (x.GetDependentUpon() ?? "") == node.DependentUpon);

        public static CmpNode ToCmpNode(this XElement xElement) =>
            new CmpNode(xElement.Attribute("Include").Value, xElement.GetDependentUpon());
    }
}