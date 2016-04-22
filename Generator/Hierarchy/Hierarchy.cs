using System.Collections.Generic;

namespace Generator
{
    public class Hierarchy
    {
        public List<RegNode> Nodes { get; set; }
        public List<RegRoot> Registrations { get; set; }

        public Hierarchy(List<RegNode> nodes, List<RegRoot> registrations)
        {
            Nodes = nodes;
            Registrations = registrations;
        }
    }
}