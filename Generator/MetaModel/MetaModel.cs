using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public class MetaModel
    {
        public List<MetaType> MetaTypes { get; }

        public MetaModel(List<Type> entityTypes)
        {
            MetaTypes = entityTypes
                .Select(t => new MetaType(t))
                .ToList();

            foreach (var entityType in MetaTypes)
                foreach (var navigationProperty in entityType.NavigationProperties)
                    MetaTypes
                        .Single(t => t.ClrType == navigationProperty.ClrType)
                        .DependsUpon.Add(navigationProperty);
        }

    }
}