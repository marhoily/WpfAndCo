using Microsoft.Data.Entity.Metadata;

namespace Generator.ChangeSet
{
    partial class Tracking
    {
        private readonly IModel _model;

        public Tracking(IModel model)
        {
            _model = model;
        }
    }
}