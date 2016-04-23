using System;
using System.Collections;
using System.Collections.Generic;

namespace Generaid
{
    using C = Dictionary<Type, Func<object, IEnumerable<object>>>;

    public sealed class HierarchyBuilder : IEnumerable
    {
        private readonly string _projectPath;
        private readonly string _projectDir;
        private readonly List<GenNode.Proto> _nodes = new List<GenNode.Proto>();
        private readonly C _registrations = new C();

        public HierarchyBuilder(string projectPath, string projectDir)
        {
            _projectPath = projectPath;
            _projectDir = projectDir;
        }
        public void Add<T>(NodeBuilder<T> item) => _nodes.Add(item.Build());
        public HierarchyBuilder With<TK, TV>(Func<TK, IEnumerable<TV>> func)
        {
            _registrations.Add(typeof(TV), 
                x => (IEnumerable<object>)func((TK)x));
            return this;
        }
        public void Generate() => new GenHierarchy(
            _projectPath, _projectDir, _nodes, _registrations).Generate();
        IEnumerator IEnumerable.GetEnumerator() { throw new Exception(); }
    }
}