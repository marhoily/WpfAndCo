using System;
using System.Collections.Generic;

namespace Generator
{
    public sealed class RegNode {
        public Type Tp { get; set; }
        public List<RegNode> Nodes { get; set; }
        public object Model { get; set; }

        public RegNode(Type tp, List<RegNode> nodes, object model)
        {
            Tp = tp;
            Nodes = nodes;
            Model = model;
        }
    }
}