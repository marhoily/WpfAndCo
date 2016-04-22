using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public class Hierarchy
    {
        public string ProjectPath { get; }
        public string ProjectDir { get; }
        public NodeExp[] Nodes { get; }

        public Hierarchy(string projectPath, string projectDir,
            List<RegNode> nodes, List<RegRoot> registrations)
        {
            ProjectPath = projectPath;
            ProjectDir = projectDir;
            Nodes = nodes
                .SelectMany(n => n.Expand(registrations, n.Model))
                .ToArray();
        }
    }
}