using Microsoft.Data.Entity.Metadata;

namespace Generator.Entry
{
    partial class Read
    {
        private readonly IEntityType _type;

        public Read(IEntityType type)
        {
            _type = type;
        }
    }
}