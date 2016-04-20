using System.Collections.Generic;

namespace Generator
{
    public class Hierarchy
    {
        public List<RegNode> Nodes { get; set; }
        public List<Registration> Registrations { get; set; }

        public Hierarchy(List<RegNode> nodes, List<Registration> registrations)
        {
            Nodes = nodes;
            Registrations = registrations;
        }
    }
}