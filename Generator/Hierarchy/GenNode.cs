using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public class GenNode
    {
        public ITransformer Transformer { get; }
        public GenNode[] GenNodes { get; }
        public GenNode(ITransformer transformer, IEnumerable<GenNode> nodes)
        {
            if (transformer == null)
                throw new ArgumentNullException(nameof(transformer));
            Transformer = transformer;
            GenNodes = nodes.ToArray();
        }
    }
}