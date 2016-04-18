using Microsoft.Data.Entity.Metadata;

namespace Generator.Entry
{
    partial class EntityTypeChangeTracking
    {
        private readonly IEntityType _type;

        public EntityTypeChangeTracking(IEntityType type) {
            _type = type;
        }
    }
}