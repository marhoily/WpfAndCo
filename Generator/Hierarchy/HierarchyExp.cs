using System.Collections.Generic;
using System.Linq;

namespace Generator
{
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