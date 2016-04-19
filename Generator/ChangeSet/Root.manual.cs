using Microsoft.Data.Entity.Metadata;

namespace Generator.ChangeSet
{
    partial class Root
    {
        private readonly IModel _model;

        public Root(IModel model)
        {
            _model = model;
        }
    }
}