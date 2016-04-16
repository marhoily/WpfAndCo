using Microsoft.Data.Entity.Metadata;

namespace Sample
{
    partial class ClassContentGenerator
    {
        private readonly IEntityType _type;

        public ClassContentGenerator(IEntityType type)
        {
            _type = type;
        }
    }
}