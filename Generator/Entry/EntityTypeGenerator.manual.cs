using Microsoft.Data.Entity.Metadata;

namespace Generator.Entry
{
    partial class EntityTypeGenerator
    {
        private readonly IEntityType _type;

        public EntityTypeGenerator(IEntityType type)
        {
            _type = type;
        }
    }
}