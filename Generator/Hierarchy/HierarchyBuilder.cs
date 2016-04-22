using System;
using System.Collections;
using System.Collections.Generic;

namespace Generator
{
    public class HierarchyBuilder : IEnumerable
    {
        private readonly string _projectPath;
        private readonly string _projectDir;
        private readonly List<RegNode> _nodes = new List<RegNode>();
        private readonly List<Converter> _registrations = new List<Converter>();

        public HierarchyBuilder(string projectPath, string projectDir)
        {
            _projectPath = projectPath;
            _projectDir = projectDir;
        }
        public void Add<T>(NodeBuilder<T> item) { _nodes.Add(item.Build()); }
        IEnumerator IEnumerable.GetEnumerator() { throw new NotImplementedException(); }

        public HierarchyBuilder With<TK, TV>(Func<TK, IEnumerable<TV>> func)
        {
            _registrations.Add(new Converter(
                typeof(TK), typeof(TV), 
                x => (IEnumerable<object>)func((TK)x)));
            return this;
        }

        public GenHierarchy Build()
        {
            return new GenHierarchy(_projectPath, 
                _projectDir, _nodes, _registrations);
        }
    }
}