using System.Collections.Generic;

namespace Generator
{
    public class Hierarchy
    {
        public List<RegNode> Nodes { get; }
        public List<RegRoot> Registrations { get; }

        public Hierarchy(List<RegNode> nodes, List<RegRoot> registrations)
        {
            Nodes = nodes;
            Registrations = registrations;
        }
    }
}