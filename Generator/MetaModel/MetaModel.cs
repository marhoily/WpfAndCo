using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public class MetaModel
    {
        private readonly List<Type> _entityTypes;

        public MetaModel(List<Type> entityTypes)
        {
            _entityTypes = entityTypes;
        }

        public IEnumerable<EntityType> GetEntityTypes()
            => _entityTypes.Select(t => new EntityType(t));
    }
}