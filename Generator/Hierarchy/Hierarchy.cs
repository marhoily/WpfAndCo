using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public class Hierarchy
    {
        public string ProjectPath { get; }
        public string ProjectDir { get; }
        public List<RegNode> Nodes { get; }
        public List<RegRoot> Registrations { get; }

        public Hierarchy(string projectPath, string projectDir,
            List<RegNode> nodes, List<RegRoot> registrations)
        {
            ProjectPath = projectPath;
            ProjectDir = projectDir;
            Nodes = nodes;
            Registrations = registrations;
        }

        public HierarchyExp Expand()
        {
            return new HierarchyExp(ProjectPath, ProjectDir,
                Nodes.SelectMany(n => n.Expand1(Registrations, n.Model)));
        }
    }

    public class NodeExp
    {
        public string Name { get; }
        public object Model { get; }
        public NodeExp[] Nodes { get; }
        public NodeExp(string name, object model, IEnumerable<NodeExp> nodes)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Name = name;
            Model = model;
            Nodes = nodes.ToArray();
        }
    }

    public class HierarchyExp
    {
        public string ProjectPath { get; }
        public string ProjectDir { get; }
        public NodeExp[] Nodes { get; }

        public HierarchyExp(string projectPath,
            string projectDir, IEnumerable<NodeExp> nodes)
        {
            ProjectPath = projectPath;
            ProjectDir = projectDir;
            Nodes = nodes.ToArray();
        }
    }
}