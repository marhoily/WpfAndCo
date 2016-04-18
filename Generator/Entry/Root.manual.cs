using Microsoft.Data.Entity.Metadata;

namespace Generator.Entry
{
    partial class Root
    {
        private readonly IEntityType _type;

        public Root(IEntityType type)
        {
            _type = type;
        }
    }
}