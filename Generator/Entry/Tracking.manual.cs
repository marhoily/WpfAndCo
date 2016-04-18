using Microsoft.Data.Entity.Metadata;

namespace Generator.Entry
{
    partial class Tracking
    {
        private readonly IEntityType _type;

        public Tracking(IEntityType type)
        {
            _type = type;
        }
    }
}