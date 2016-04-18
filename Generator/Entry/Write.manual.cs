using Microsoft.Data.Entity.Metadata;

namespace Generator.Entry
{
    partial class Write
    {
        private readonly IEntityType _type;

        public Write(IEntityType type)
        {
            _type = type;
        }
    }
}