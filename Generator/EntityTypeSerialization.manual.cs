using Microsoft.Data.Entity.Metadata;

namespace Generator
{
    partial class EntityTypeSerialization
    {
        private readonly IEntityType _type;

        public EntityTypeSerialization(IEntityType type)
        {
            _type = type;
        }
    }
}