using System.Collections.Generic;

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
    }
}