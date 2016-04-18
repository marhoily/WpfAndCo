using Microsoft.Data.Entity.Metadata;

namespace Generator.Entry
{
    partial class EntityTypeKey
    {
        private readonly IEntityType _type;

        public EntityTypeKey(IEntityType type) {
            _type = type;
        }
    }
}