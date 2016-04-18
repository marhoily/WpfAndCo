using Microsoft.Data.Entity.Metadata;

namespace Generator.Entry
{
    partial class Key
    {
        private readonly IEntityType _type;

        public Key(IEntityType type)
        {
            _type = type;
        }
    }
}