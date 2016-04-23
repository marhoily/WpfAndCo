﻿using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Generator;

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

        public static string GetDependentUpon(this XContainer doc) =>
            doc.Element("DependentUpon")?.Value;

        public static XElement FindByFullName(this XContainer doc, string fileName) =>
            doc.XPathSelectElement(
                $"//ns:ItemGroup/ns:Compile[@Include='{fileName}']",
                Resolver);

        public static IEnumerable<XElement> FindByDirectory(this XContainer doc, string dir)
            => doc
                .XPathSelectElements("//ns:ItemGroup/ns:Compile", Resolver)
                .Where(x => x.Attribute("Include").Value.StartsWith(dir));

        public static XElement Find(this XContainer doc, CmpNode node) =>
            doc.XPathSelectElement(
                $"//ns:ItemGroup/ns:Compile[@Include='{node.FullName}' and DependentUpon='{node.DependentUpon}']",
                Resolver);

        public static CmpNode ToCmpNode(this XElement xElement) =>
            new CmpNode(
                xElement.Attribute("Include").Value,
                xElement.GetDependentUpon());
    }
}